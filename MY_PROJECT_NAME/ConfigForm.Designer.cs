namespace DJITelloMovementPanel {
  partial class ConfigForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.cbCameraDevice = new System.Windows.Forms.ComboBox();
      this.ucHelpHover1 = new ARC.UCForms.UC.UCHelpHover();
      this.label1 = new System.Windows.Forms.Label();
      this.cbEnableVideoDebugStats = new System.Windows.Forms.CheckBox();
      this.ucHelpHover2 = new ARC.UCForms.UC.UCHelpHover();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnCancel);
      this.panel1.Controls.Add(this.btnSave);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 212);
      this.panel1.Margin = new System.Windows.Forms.Padding(4);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(508, 62);
      this.panel1.TabIndex = 0;
      // 
      // btnCancel
      // 
      this.btnCancel.Dock = System.Windows.Forms.DockStyle.Left;
      this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnCancel.Location = new System.Drawing.Point(100, 0);
      this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(99, 62);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnSave
      // 
      this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
      this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnSave.Location = new System.Drawing.Point(0, 0);
      this.btnSave.Margin = new System.Windows.Forms.Padding(4);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(100, 62);
      this.btnSave.TabIndex = 0;
      this.btnSave.Text = "&Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // cbCameraDevice
      // 
      this.cbCameraDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbCameraDevice.FormattingEnabled = true;
      this.cbCameraDevice.Location = new System.Drawing.Point(141, 12);
      this.cbCameraDevice.Name = "cbCameraDevice";
      this.cbCameraDevice.Size = new System.Drawing.Size(255, 24);
      this.cbCameraDevice.TabIndex = 7;
      // 
      // ucHelpHover1
      // 
      this.ucHelpHover1.BackColor = System.Drawing.Color.Transparent;
      this.ucHelpHover1.Location = new System.Drawing.Point(399, 14);
      this.ucHelpHover1.Margin = new System.Windows.Forms.Padding(0);
      this.ucHelpHover1.Name = "ucHelpHover1";
      this.ucHelpHover1.SetHelpText = "";
      this.ucHelpHover1.Size = new System.Drawing.Size(22, 22);
      this.ucHelpHover1.TabIndex = 6;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(4, 14);
      this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(130, 22);
      this.label1.TabIndex = 5;
      this.label1.Text = "Camera Device:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // cbEnableVideoDebugStats
      // 
      this.cbEnableVideoDebugStats.AutoSize = true;
      this.cbEnableVideoDebugStats.Location = new System.Drawing.Point(141, 46);
      this.cbEnableVideoDebugStats.Name = "cbEnableVideoDebugStats";
      this.cbEnableVideoDebugStats.Size = new System.Drawing.Size(186, 20);
      this.cbEnableVideoDebugStats.TabIndex = 8;
      this.cbEnableVideoDebugStats.Text = "Enable Video Debug Stats";
      this.cbEnableVideoDebugStats.UseVisualStyleBackColor = true;
      // 
      // ucHelpHover2
      // 
      this.ucHelpHover2.BackColor = System.Drawing.Color.Transparent;
      this.ucHelpHover2.Location = new System.Drawing.Point(399, 44);
      this.ucHelpHover2.Margin = new System.Windows.Forms.Padding(0);
      this.ucHelpHover2.Name = "ucHelpHover2";
      this.ucHelpHover2.SetHelpText = "This is resource hungry. Don\'t use it unless you have a reason.\r\n\r\nThis will disp" +
    "lay details for every rendered frame in the log";
      this.ucHelpHover2.Size = new System.Drawing.Size(22, 22);
      this.ucHelpHover2.TabIndex = 9;
      // 
      // ConfigForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(508, 274);
      this.Controls.Add(this.ucHelpHover2);
      this.Controls.Add(this.cbEnableVideoDebugStats);
      this.Controls.Add(this.cbCameraDevice);
      this.Controls.Add(this.ucHelpHover1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.panel1);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(4);
      this.Name = "ConfigForm";
      this.ShowIcon = false;
      this.Text = "Configuration";
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.ComboBox cbCameraDevice;
    private ARC.UCForms.UC.UCHelpHover ucHelpHover1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox cbEnableVideoDebugStats;
    private ARC.UCForms.UC.UCHelpHover ucHelpHover2;
  }
}