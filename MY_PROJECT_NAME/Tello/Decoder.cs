using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;

namespace DJITelloMovementPanel.Tello {

  public class Decoder : EZ_B.DisposableBase {

    public event EventHandler<Bitmap> OnNewBitmap;
    public event EventHandler<string> OnDebug;
    public event EventHandler OnStarted;
    public event EventHandler OnStopped;

    private CancellationTokenSource _cancellationTokenSource;
    private Process _ffmpegProcess;
    private NamedPipeServerStream _outputImagePipe;
    private byte [] _readBuffer = new byte[1000000];
    List<byte> _imageBuffer = new List<byte>();

    public bool DebugModeEnabled {
      get;
      set;
    }

    public bool IsRunning {
      get {

        try {

          return _ffmpegProcess != null;
        } catch {

          return false;
        }
      }
    }

    public Decoder() {

    }

    protected override void DisposeOverride() {

      Stop();
    }

    public void Start(int width, int height) {

      Stop();

      if (_ffmpegProcess != null)
        throw new Exception("ffmpeg still running. didn't shut down?");

      if (_outputImagePipe != null)
        throw new Exception("output pipe still open");

      _imageBuffer.Clear();

      _cancellationTokenSource = new CancellationTokenSource();

      _outputImagePipe = new NamedPipeServerStream(
        "imagePipe",
        PipeDirection.In,
        NamedPipeServerStream.MaxAllowedServerInstances,
        PipeTransmissionMode.Byte,
        PipeOptions.Asynchronous,
        1000000,
        1000000);

      _outputImagePipe.BeginWaitForConnection(endWaitForConnection, null);

      string videoInArgs = "-i udp://0.0.0.0:11111";
      string videoOutputArgs = $@"-hide_banner -tune zerolatency -filter:v fps=15 -y -c:v png -compression_level 50 -s {width}x{height} -f image2pipe \\.\pipe\imagePipe";

      _ffmpegProcess = new Process {

        StartInfo = {

          FileName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\ffmpeg.exe",
          Arguments = $"{videoInArgs} {videoOutputArgs}",
          UseShellExecute = false,
          CreateNoWindow = true,
          RedirectStandardInput = true,
          RedirectStandardError = true,
          RedirectStandardOutput = true
        }
      };

      _ffmpegProcess.ErrorDataReceived += (s, ee) => {

        if (DebugModeEnabled)
          OnDebug?.Invoke(this, ee.Data);
      };

      _ffmpegProcess.OutputDataReceived += (s, ee) => {

        if (DebugModeEnabled)
          OnDebug?.Invoke(this, ee.Data);
      };

      _ffmpegProcess.Exited += (s, ee) => OnDebug?.Invoke(this, "ffmpeg closed");

      _ffmpegProcess.EnableRaisingEvents = false;

      _ffmpegProcess.Start();

      _ffmpegProcess.BeginOutputReadLine();
      _ffmpegProcess.BeginErrorReadLine();

      OnDebug?.Invoke(this, $"Camera decoder ({width}x{height})");

      OnStarted?.Invoke(this, new EventArgs());
    }

    public void Stop() {

      bool launchEvent = false;

      if (_cancellationTokenSource != null) {

        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
        _cancellationTokenSource = null;

        launchEvent = true;
      }

      if (_ffmpegProcess != null) {

        if (!_ffmpegProcess.HasExited)
          _ffmpegProcess.Kill();

        _ffmpegProcess.Close();
        _ffmpegProcess.Dispose();
        _ffmpegProcess = null;

        launchEvent = true;
      }

      if (_outputImagePipe != null) {

        _outputImagePipe.Dispose();
        _outputImagePipe = null;

        launchEvent = true;
      }

      if (launchEvent)
        OnStopped?.Invoke(this, new EventArgs());
    }

    private void endWaitForConnection(IAsyncResult iar) {

      try {

        _outputImagePipe?.EndWaitForConnection(iar);

        if (_outputImagePipe == null || !_outputImagePipe.IsConnected) {

          OnDebug.Invoke(this, "Pipe didn't connect");

          Stop();

          return;
        }

        beginReadFromPipe();
      } catch (Exception ex) {

        OnDebug?.Invoke(this, ex.Message);

        Stop();
      }
    }

    private void beginReadFromPipe() {

      try {

        _outputImagePipe?.BeginRead(_readBuffer, 0, _readBuffer.Length, endReadFromPipe, null);
      } catch (Exception ex) {

        OnDebug?.Invoke(this, ex.Message);

        Stop();
      }
    }

    private void endReadFromPipe(IAsyncResult iar) {

      try {

        if (_outputImagePipe == null || _cancellationTokenSource == null || _cancellationTokenSource.IsCancellationRequested || !iar.IsCompleted)
          return;

        int bytesRead = _outputImagePipe.EndRead(iar);

        if (bytesRead == 0) {

          OnDebug?.Invoke(this, "No bytes in buffer (pipe closed)");

          Stop();

          return;
        }

        _imageBuffer.AddRange(_readBuffer.Take(bytesRead));

        while (true) {

          if (_imageBuffer.Count < 100)
            break;

          bool foundHeader = false;

          for (int i = 0; i < _imageBuffer.Count - 8; i++)
            if (_imageBuffer[i] == 137 &&
                _imageBuffer[i + 1] == 80 &&
                _imageBuffer[i + 2] == 78 &&
                _imageBuffer[i + 3] == 71 &&
                _imageBuffer[i + 4] == 13 &&
                _imageBuffer[i + 5] == 10 &&
                _imageBuffer[i + 6] == 26 &&
                _imageBuffer[i + 7] == 10) {

              _imageBuffer.RemoveRange(0, i);

              foundHeader = true;

              break;
            }

          if (!foundHeader)
            break;

          int endIndex = -1;

          for (int i = 7; i < _imageBuffer.Count - 8; i++)
            if (_imageBuffer[i] == 137 &&
              _imageBuffer[i + 1] == 80 &&
              _imageBuffer[i + 2] == 78 &&
              _imageBuffer[i + 3] == 71 &&
              _imageBuffer[i + 4] == 13 &&
              _imageBuffer[i + 5] == 10 &&
              _imageBuffer[i + 6] == 26 &&
              _imageBuffer[i + 7] == 10) {

              endIndex = i;

              break;
            }

          if (endIndex == -1)
            break;

          var imageData = _imageBuffer.Take(endIndex).ToArray();

          _imageBuffer.RemoveRange(0, endIndex);

          using (var ms = new MemoryStream(imageData))
          using (var bm = new Bitmap(ms))
            OnNewBitmap?.Invoke(this, bm);
        }

        beginReadFromPipe();
      } catch (Exception ex) {

        OnDebug?.Invoke(this, ex.Message);

        Stop();
      }
    }
  }
}