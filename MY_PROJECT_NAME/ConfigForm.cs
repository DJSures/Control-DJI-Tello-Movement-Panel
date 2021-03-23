using System;
using System.Windows.Forms;

namespace DJITelloMovementPanel {

  public partial class ConfigForm : Form {

    private ARC.Config.Sub.PluginV1 _cf;

    public ConfigForm() {

      InitializeComponent();
    }

    public void SetConfiguration(ARC.Config.Sub.PluginV1 config) {

      try {

        _cf = config;

        cbEnableVideoDebugStats.Checked = Convert.ToBoolean(_cf.STORAGE[ConfigTitles.CAMERA_ENABLE_DEBUG_STATS]);

        cbCameraDevice.BeginUpdate();

        var cameraDevices = ARC.EZBManager.FormMain.GetControlByType(typeof(ARC.UCForms.FormCameraDevice));

        foreach (ARC.UCForms.FormCameraDevice camera in cameraDevices)
          cbCameraDevice.Items.Add(camera.Text);

        cbCameraDevice.SelectedItem = _cf.STORAGE[ConfigTitles.CAMERA_DEVICE_NAME].ToString();

        cbCameraDevice.EndUpdate();

      } catch (Exception ex) {

        MessageBox.Show(ex.Message, "Error loading configuration");
      }
    }

    public ARC.Config.Sub.PluginV1 GetConfiguration() {
    
      return _cf;
    }

    private void btnSave_Click(object sender, EventArgs e) {

      try {

        if (cbCameraDevice.SelectedIndex == -1)
          throw new Exception("A camera device has not been selected");

        _cf.STORAGE[ConfigTitles.CAMERA_DEVICE_NAME] = cbCameraDevice.SelectedItem.ToString();

        _cf.STORAGE[ConfigTitles.CAMERA_ENABLE_DEBUG_STATS] = cbEnableVideoDebugStats.Checked;

      } catch (Exception ex) {

        MessageBox.Show(ex.Message, "Error saving configuration");

        return;
      }

      DialogResult = DialogResult.OK;
    }

    private void btnCancel_Click(object sender, EventArgs e) {

      DialogResult = DialogResult.Cancel;
    }
  }
}
