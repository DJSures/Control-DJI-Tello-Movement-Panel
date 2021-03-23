using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DJITelloMovementPanel.Tello {

  public class Tello : EZ_B.DisposableBase {

    UdpClient _udpClient;
    Decoder   _decoder;
    IPEndPoint _ep;
    System.Timers.Timer _timer;
    System.Diagnostics.Stopwatch _sw;

    public event EventHandler<string> OnDebug;
    public event EventHandler<bool> OnConnectionChanged;
    public event EventHandler<Bitmap> OnNewBitmap;


    public bool IsConnected {
      get {
        return _udpClient != null;
      }
    }

    public Decoder Decoder {
      get => _decoder;
    }

    public Tello() {

      _ep = new IPEndPoint(IPAddress.Parse("192.168.10.1"), 8889);

      _decoder = new Decoder();
      _decoder.OnNewBitmap += _decoder_OnNewBitmap;
      _decoder.OnDebug += _decoder_OnDebug;
      _decoder.OnStarted += _decoder_OnStarted;
      _decoder.OnStopped += _decoder_OnStopped;

      _sw = new System.Diagnostics.Stopwatch();

      _timer = new System.Timers.Timer();
      _timer.Elapsed += _timer_Elapsed;
      _timer.Interval = 1000;
    }

    protected override void DisposeOverride() {

      Disconnect();

      _decoder.Dispose();

      _timer.Dispose();
    }

    private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {

      if (_sw.ElapsedMilliseconds > 3000)
        WriteString("battery?");
    }

    public void WriteString(string str) {

      try {

        if (!IsConnected)
          throw new Exception("Not connected");

        var b = Encoding.UTF8.GetBytes(str);

        _udpClient.Send(b, b.Length);

        _sw.Restart();
      } catch (Exception ex) {

        OnDebug?.Invoke(this, ex.Message);

        Disconnect();
      }
    }

    public void Connect(int videoWidth, int videoHeight) {

      if (IsConnected)
        Disconnect();

      _udpClient = new UdpClient();
      _udpClient.Client.Blocking = true;
      _udpClient.EnableBroadcast = true;

      _udpClient.Connect(_ep);

      _udpClient.BeginReceive(dataReceived, _udpClient);

      WriteString("command");

      WriteString("streamon");

      _decoder.Start(videoWidth, videoHeight);

      _timer.Start();

      OnConnectionChanged?.Invoke(this, true);
    }

    private void dataReceived(IAsyncResult ar) {

      try {

        UdpClient c = (UdpClient)ar.AsyncState;

        if (c.Client == null) {

          OnDebug?.Invoke(this, "Client disconnected");

          return;
        }

        var receivedBytes = c.EndReceive(ar, ref _ep);

        var s = Encoding.UTF8.GetString(receivedBytes);

        OnDebug?.Invoke(this, $"Received: {s}");

        c.BeginReceive(dataReceived, ar.AsyncState);
      } catch (ObjectDisposedException) {

      } catch (Exception ex) {

        OnDebug?.Invoke(this, "on data received:" + ex.Message);
      }
    }

    private void _decoder_OnStopped(object sender, EventArgs e) {

      OnDebug?.Invoke(this, "decoder: stopped");
    }

    private void _decoder_OnStarted(object sender, EventArgs e) {

      OnDebug?.Invoke(this, "decoder: started");
    }

    private void _decoder_OnDebug(object sender, string e) {

      OnDebug?.Invoke(this, $"decoder: {e}");
    }

    private void _decoder_OnNewBitmap(object sender, System.Drawing.Bitmap e) {

      OnNewBitmap?.Invoke(this, e);
    }

    public void Disconnect() {

      if (_decoder.IsRunning)
        _decoder.Stop();

      _timer.Stop();

      _sw.Reset();

      if (_udpClient != null) {

        try {

          var b = Encoding.ASCII.GetBytes("streamoff");

          _udpClient.Send(b, b.Length);
        } catch {
        }

        _udpClient.Close();
        _udpClient.Dispose();
        _udpClient = null;

        OnDebug?.Invoke(this, "Disconnected");

        OnConnectionChanged?.Invoke(this, false);
      }
    }

    public void Stop() {

      WriteString($"rc 0 0 0 0");
    }

    public void Move(int leftRight, int forwardBackward, int upDown, int yaw) {

      WriteString($"rc {leftRight} {forwardBackward} {upDown} {yaw}");
    }

    public void TakeOff() {

      WriteString("takeoff");
    }

    public void Land() {

      WriteString("land");
    }

    public void EmergencyStop() {

      WriteString("emergency");
    }
  }
}
