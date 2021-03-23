using System;
using System.Drawing;
using System.Windows.Forms;
using ARC;
using ARC.Config.Sub;

namespace DJITelloMovementPanel {

  public partial class FormMain : ARC.UCForms.FormPluginMaster {

    ARC.UCForms.FormCameraDevice _camera;

    Tello.Tello _tello;

    Color _origBtnColor = Color.White;

    public FormMain() {

      InitializeComponent();

      ThemeRenderer.ControlsToSkipTheming.Add(button13);

      _tello = new Tello.Tello();
    }

    private void FormDroneMovementPanel_Load(object sender, EventArgs e) {

      _origBtnColor = btnForward.BackColor;

      EZBManager.MovementManager.LocomotionStyle = MovementManager.LocomotionStyleEnum.Drone;
      ARC.FormMain.MovementPanel = this;
      EZBManager.MovementManager.OnMovement2 += Movement_OnMovement2;
      EZBManager.MovementManager.OnSpeedChanged += MovementManager_OnSpeedChanged;

      EZBManager.FormMain.OnBehaviorControlRemoved += FormMain_OnBehaviorControlRemoved;

      _tello.OnConnectionChanged += _tello_OnConnectionChanged;
      _tello.OnDebug += _tello_OnDebug;
      _tello.OnNewBitmap += _tello_OnNewBitmap;
    }

    private void FormDroneMovementPanel_FormClosing(object sender, FormClosingEventArgs e) {

      ARC.FormMain.MovementPanel = null;
      EZBManager.MovementManager.OnMovement2 -= Movement_OnMovement2;

      EZBManager.FormMain.OnBehaviorControlRemoved -= FormMain_OnBehaviorControlRemoved;

      _tello.Dispose();

      detachCamera();
    }

    private void FormMain_OnBehaviorControlRemoved(Control removedControl) {

      if (IsClosing)
        return;

      if (_camera == removedControl) {

        Invokers.SetAppendText(tbLog, true, "Camera device was removed! Stopping camera");

        detachCamera();
      }
    }

    private void _tello_OnNewBitmap(object sender, Bitmap e) {

      if (_camera != null)
        _camera.Camera.SetCaptureImage(e);
    }

    private void _tello_OnDebug(object sender, string e) {

      if (IsClosing)
        return;

      Invokers.SetAppendText(tbLog, true, e);
    }

    private void _tello_OnConnectionChanged(object sender, bool e) {

      if (IsClosing)
        return;

      Invokers.SetAppendText(tbLog, true, $"connected: {e}");

      if (e) {

        Invokers.SetText(btnConnect, "Disconnect");
      } else {

        Invokers.SetText(btnConnect, "Connect");
      }
    }

    void attachCamera() {

      detachCamera();

      string cameraDeviceName = _cf.STORAGE[ConfigTitles.CAMERA_DEVICE_NAME].ToString();

      if (cameraDeviceName == string.Empty)
        throw new Exception("Add a camera device to the project and select it from this robot skill's config menu.");

      var camera = (ARC.UCForms.FormCameraDevice)EZBManager.FormMain.GetControlByNameAllPages(cameraDeviceName);

      if (camera == null)
        throw new Exception($"Camera device not found ({cameraDeviceName}). Add a camera device and select it from this config menu.");

      if (!camera.Camera.IsActive)
        throw new Exception("Camera must be started first. Select CUSTOM and press START in the camera device.");

      _camera = camera;
    }

    void detachCamera() {

      _camera = null;
    }

    private void _arDone_OnDebug(object sender, string e) {

      if (IsClosing)
        return;

      Invokers.SetAppendText(tbLog, true, e);
    }

    private void _arDone_OnImagecomplete(Bitmap bitmap) {

      if (IsClosing)
        return;

      if (_camera != null)
        _camera.Camera.SetCaptureImage(bitmap);
    }

    public override void SetConfiguration(PluginV1 cf) {

      base.SetConfiguration(cf);

      cf.STORAGE.AddIfNotExist(ConfigTitles.CAMERA_DEVICE_NAME, string.Empty);
      cf.STORAGE.AddIfNotExist(ConfigTitles.CAMERA_ENABLE_DEBUG_STATS, false);

      _tello.Decoder.DebugModeEnabled = Convert.ToBoolean(cf.STORAGE[ConfigTitles.CAMERA_ENABLE_DEBUG_STATS]);
    }

    public override void ConfigPressed() {

      using (var options = new ConfigForm()) {

        options.SetConfiguration(_cf);

        if (options.ShowDialog() == DialogResult.OK)
          SetConfiguration(options.GetConfiguration());
      }
    }

    private void stop(object sender, EventArgs e) {

      EZBManager.MovementManager.GoStop();
    }

    private void forward(object sender, EventArgs e) {

      EZBManager.MovementManager.GoForward();
    }

    private void left(object sender, EventArgs e) {

      EZBManager.MovementManager.GoLeft();
    }

    private void reverse(object sender, EventArgs e) {

      EZBManager.MovementManager.GoReverse();
    }

    private void right(object sender, EventArgs e) {

      EZBManager.MovementManager.GoRight();
    }

    private void MovementManager_OnSpeedChanged(int speedLeft, int speedRight) {

      Invokers.SetValue(trackBar1, speedLeft);
    }

    private void Movement_OnMovement2(MovementManager.MovementDirectionEnum direction, byte speedLeft, byte speedRight) {

      if (IsClosing)
        return;

      int sLeft = (int)EZ_B.Functions.Map(speedLeft, 0, 255, 0, 100);
      int sRight = (int)EZ_B.Functions.Map(speedRight, 0, 255, 0, 100);

      int forwardBackSpeed = Math.Max(sLeft, sRight);

      int yawSpeedAndDirection = Math.Abs(sLeft - sRight);

      if (sLeft < sRight)
        yawSpeedAndDirection = -yawSpeedAndDirection;

      switch (direction) {

        case MovementManager.MovementDirectionEnum.Stop:

          Invokers.SetBackColor(btnForward, _origBtnColor);
          Invokers.SetBackColor(btnRight, _origBtnColor);
          Invokers.SetBackColor(btnReverse, _origBtnColor);
          Invokers.SetBackColor(btnLeft, _origBtnColor);
          Invokers.SetBackColor(btnStop, Color.Red);
          Invokers.SetBackColor(btnUp, _origBtnColor);
          Invokers.SetBackColor(btnDown, _origBtnColor);
          Invokers.SetBackColor(btnRollRight, _origBtnColor);
          Invokers.SetBackColor(btnRollLeft, _origBtnColor);

          _tello.Stop();
          break;
        case MovementManager.MovementDirectionEnum.Forward:

          Invokers.SetBackColor(btnForward, Color.Green);
          Invokers.SetBackColor(btnRight, _origBtnColor);
          Invokers.SetBackColor(btnReverse, _origBtnColor);
          Invokers.SetBackColor(btnLeft, _origBtnColor);
          Invokers.SetBackColor(btnStop, _origBtnColor);
          Invokers.SetBackColor(btnUp, _origBtnColor);
          Invokers.SetBackColor(btnDown, _origBtnColor);
          Invokers.SetBackColor(btnRollRight, _origBtnColor);
          Invokers.SetBackColor(btnRollLeft, _origBtnColor);

          _tello.Move(0, forwardBackSpeed, 0, yawSpeedAndDirection);
          break;
        case MovementManager.MovementDirectionEnum.Right:

          Invokers.SetBackColor(btnForward, _origBtnColor);
          Invokers.SetBackColor(btnRight, Color.Green);
          Invokers.SetBackColor(btnReverse, _origBtnColor);
          Invokers.SetBackColor(btnLeft, _origBtnColor);
          Invokers.SetBackColor(btnStop, _origBtnColor);
          Invokers.SetBackColor(btnUp, _origBtnColor);
          Invokers.SetBackColor(btnDown, _origBtnColor);
          Invokers.SetBackColor(btnRollRight, _origBtnColor);
          Invokers.SetBackColor(btnRollLeft, _origBtnColor);

          _tello.Move(0, 0, 0, sRight);
          break;
        case MovementManager.MovementDirectionEnum.Reverse:

          Invokers.SetBackColor(btnForward, _origBtnColor);
          Invokers.SetBackColor(btnRight, _origBtnColor);
          Invokers.SetBackColor(btnReverse, Color.Green);
          Invokers.SetBackColor(btnLeft, _origBtnColor);
          Invokers.SetBackColor(btnStop, _origBtnColor);
          Invokers.SetBackColor(btnUp, _origBtnColor);
          Invokers.SetBackColor(btnDown, _origBtnColor);
          Invokers.SetBackColor(btnRollRight, _origBtnColor);
          Invokers.SetBackColor(btnRollLeft, _origBtnColor);

          _tello.Move(0, -forwardBackSpeed, 0, yawSpeedAndDirection);
          break;
        case MovementManager.MovementDirectionEnum.Left:

          Invokers.SetBackColor(btnForward, _origBtnColor);
          Invokers.SetBackColor(btnRight, _origBtnColor);
          Invokers.SetBackColor(btnReverse, _origBtnColor);
          Invokers.SetBackColor(btnLeft, Color.Green);
          Invokers.SetBackColor(btnStop, _origBtnColor);
          Invokers.SetBackColor(btnUp, _origBtnColor);
          Invokers.SetBackColor(btnDown, _origBtnColor);
          Invokers.SetBackColor(btnRollRight, _origBtnColor);
          Invokers.SetBackColor(btnRollLeft, _origBtnColor);

          _tello.Move(0, 0, 0, -sLeft);
          break;

        case MovementManager.MovementDirectionEnum.Up:

          Invokers.SetBackColor(btnForward, _origBtnColor);
          Invokers.SetBackColor(btnRight, _origBtnColor);
          Invokers.SetBackColor(btnReverse, _origBtnColor);
          Invokers.SetBackColor(btnLeft, _origBtnColor);
          Invokers.SetBackColor(btnStop, _origBtnColor);
          Invokers.SetBackColor(btnUp, Color.Green);
          Invokers.SetBackColor(btnDown, _origBtnColor);
          Invokers.SetBackColor(btnRollRight, _origBtnColor);
          Invokers.SetBackColor(btnRollLeft, _origBtnColor);

          _tello.Move(0, 0, 25, 0);
          break;

        case MovementManager.MovementDirectionEnum.Down:

          Invokers.SetBackColor(btnForward, _origBtnColor);
          Invokers.SetBackColor(btnRight, _origBtnColor);
          Invokers.SetBackColor(btnReverse, _origBtnColor);
          Invokers.SetBackColor(btnLeft, _origBtnColor);
          Invokers.SetBackColor(btnStop, _origBtnColor);
          Invokers.SetBackColor(btnUp, _origBtnColor);
          Invokers.SetBackColor(btnDown, Color.Green);
          Invokers.SetBackColor(btnRollRight, _origBtnColor);
          Invokers.SetBackColor(btnRollLeft, _origBtnColor);

          _tello.Move(0, 0, -25, 0);
          break;

        case MovementManager.MovementDirectionEnum.RollRight:

          Invokers.SetBackColor(btnForward, _origBtnColor);
          Invokers.SetBackColor(btnRight, _origBtnColor);
          Invokers.SetBackColor(btnReverse, _origBtnColor);
          Invokers.SetBackColor(btnLeft, _origBtnColor);
          Invokers.SetBackColor(btnStop, _origBtnColor);
          Invokers.SetBackColor(btnUp, _origBtnColor);
          Invokers.SetBackColor(btnDown, _origBtnColor);
          Invokers.SetBackColor(btnRollRight, Color.Green);
          Invokers.SetBackColor(btnRollLeft, _origBtnColor);

          _tello.Move(25, 0, 0, 0);
          break;

        case MovementManager.MovementDirectionEnum.RollLeft:

          Invokers.SetBackColor(btnForward, _origBtnColor);
          Invokers.SetBackColor(btnRight, _origBtnColor);
          Invokers.SetBackColor(btnReverse, _origBtnColor);
          Invokers.SetBackColor(btnLeft, _origBtnColor);
          Invokers.SetBackColor(btnStop, _origBtnColor);
          Invokers.SetBackColor(btnUp, _origBtnColor);
          Invokers.SetBackColor(btnDown, _origBtnColor);
          Invokers.SetBackColor(btnRollRight, _origBtnColor);
          Invokers.SetBackColor(btnRollLeft, Color.Green);

          _tello.Move(-25, 0, 0, 0);
          break;

        case MovementManager.MovementDirectionEnum.Land:
          Invokers.SetBackColor(btnForward, _origBtnColor);
          Invokers.SetBackColor(btnRight, _origBtnColor);
          Invokers.SetBackColor(btnReverse, _origBtnColor);
          Invokers.SetBackColor(btnLeft, _origBtnColor);
          Invokers.SetBackColor(btnStop, Color.Red);
          Invokers.SetBackColor(btnUp, _origBtnColor);
          Invokers.SetBackColor(btnDown, _origBtnColor);
          Invokers.SetBackColor(btnRollRight, _origBtnColor);
          Invokers.SetBackColor(btnRollLeft, _origBtnColor);

          _tello.Land();
          break;

        case MovementManager.MovementDirectionEnum.Takeoff:

          Invokers.SetBackColor(btnForward, _origBtnColor);
          Invokers.SetBackColor(btnRight, _origBtnColor);
          Invokers.SetBackColor(btnReverse, _origBtnColor);
          Invokers.SetBackColor(btnLeft, _origBtnColor);
          Invokers.SetBackColor(btnStop, Color.Red);
          Invokers.SetBackColor(btnUp, _origBtnColor);
          Invokers.SetBackColor(btnDown, _origBtnColor);
          Invokers.SetBackColor(btnRollRight, _origBtnColor);
          Invokers.SetBackColor(btnRollLeft, _origBtnColor);

          _tello.TakeOff();
          break;
        case MovementManager.MovementDirectionEnum.Emergency:

          Invokers.SetBackColor(btnForward, _origBtnColor);
          Invokers.SetBackColor(btnRight, _origBtnColor);
          Invokers.SetBackColor(btnReverse, _origBtnColor);
          Invokers.SetBackColor(btnLeft, _origBtnColor);
          Invokers.SetBackColor(btnStop, Color.Red);
          Invokers.SetBackColor(btnUp, _origBtnColor);
          Invokers.SetBackColor(btnDown, _origBtnColor);
          Invokers.SetBackColor(btnRollRight, _origBtnColor);
          Invokers.SetBackColor(btnRollLeft, _origBtnColor);

          _tello.EmergencyStop();
          break;
      }
    }

    private void textBox1_KeyUp(object sender, KeyEventArgs e) {

      stop(this, new EventArgs());
    }

    private void textBox1_KeyDown(object sender, KeyEventArgs e) {

      if (EZBManager.MovementManager.GetCurrentDirection != MovementManager.MovementDirectionEnum.Stop)
        return;

      if (e.KeyCode == Keys.Up)
        forward(this, new EventArgs());
      else if (e.KeyCode == Keys.Right)
        right(this, new EventArgs());
      else if (e.KeyCode == Keys.Down)
        reverse(this, new EventArgs());
      else if (e.KeyCode == Keys.Left)
        left(this, new EventArgs());
      else if (e.KeyCode == Keys.A)
        up(this, new EventArgs());
      else if (e.KeyCode == Keys.Z)
        down(this, new EventArgs());
      else if (e.KeyCode == Keys.W)
        rollRight(this, new EventArgs());
      else if (e.KeyCode == Keys.Q)
        rollLeft(this, new EventArgs());
      else
        stop(this, new EventArgs());
    }

    private void button12_Click(object sender, EventArgs e) {

      try {

        if (_tello.IsConnected) {

          detachCamera();

          _tello.Disconnect();
        } else {

          tbLog.Clear();

          btnConnect.Text = "Connecting";

          attachCamera();

          _tello.Connect(_camera.Camera.CaptureWidth, _camera.Camera.CaptureHeight);
        }
      } catch (Exception ex) {

        btnConnect.Text = "Connect";

        Invokers.SetAppendText(tbLog, true, ex.Message);
      }
    }

    private void button9_Click(object sender, EventArgs e) {

      EZBManager.MovementManager.Takeoff();
    }

    private void button11_Click(object sender, EventArgs e) {

      EZBManager.MovementManager.Land();
    }

    private void up(object sender, EventArgs e) {

      EZBManager.MovementManager.GoUp();
    }

    private void down(object sender, EventArgs e) {

      EZBManager.MovementManager.GoDown();
    }

    private void rollLeft(object sender, EventArgs e) {

      EZBManager.MovementManager.GoRollLeft();
    }

    private void rollRight(object sender, EventArgs e) {

      EZBManager.MovementManager.GoRollRight();
    }

    public override void SendCommand(string windowCommand, params string[] values) {

      base.SendCommand(windowCommand, values);
    }

    public override object[] GetSupportedControlCommands() {

      return new object[] {
      };
    }

    private void button13_Click(object sender, EventArgs e) {

      EZBManager.MovementManager.GoEmergency();
    }

    private void trackBar1_ValueChanged(object sender, EventArgs e) {

      EZBManager.MovementManager.SetSpeed((byte)trackBar1.Value);
    }
  }
}
