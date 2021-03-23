namespace DJITelloMovementPanel {
  partial class FormMain {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
      this.btnStop = new System.Windows.Forms.Button();
      this.btnForward = new System.Windows.Forms.Button();
      this.btnLeft = new System.Windows.Forms.Button();
      this.btnReverse = new System.Windows.Forms.Button();
      this.btnRight = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.btnDown = new System.Windows.Forms.Button();
      this.button9 = new System.Windows.Forms.Button();
      this.button11 = new System.Windows.Forms.Button();
      this.btnConnect = new System.Windows.Forms.Button();
      this.button13 = new System.Windows.Forms.Button();
      this.btnUp = new System.Windows.Forms.Button();
      this.btnRollRight = new System.Windows.Forms.Button();
      this.btnRollLeft = new System.Windows.Forms.Button();
      this.tbLog = new System.Windows.Forms.TextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.trackBar1 = new System.Windows.Forms.TrackBar();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
      this.SuspendLayout();
      // 
      // btnStop
      // 
      this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnStop.Location = new System.Drawing.Point(35, 49);
      this.btnStop.Name = "btnStop";
      this.btnStop.Size = new System.Drawing.Size(70, 30);
      this.btnStop.TabIndex = 3;
      this.btnStop.Text = "Stop";
      this.btnStop.UseVisualStyleBackColor = true;
      this.btnStop.Click += new System.EventHandler(this.stop);
      // 
      // btnForward
      // 
      this.btnForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnForward.Image = ((System.Drawing.Image)(resources.GetObject("btnForward.Image")));
      this.btnForward.Location = new System.Drawing.Point(55, 18);
      this.btnForward.Name = "btnForward";
      this.btnForward.Size = new System.Drawing.Size(30, 30);
      this.btnForward.TabIndex = 5;
      this.btnForward.UseVisualStyleBackColor = true;
      this.btnForward.Click += new System.EventHandler(this.forward);
      // 
      // btnLeft
      // 
      this.btnLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnLeft.Image = ((System.Drawing.Image)(resources.GetObject("btnLeft.Image")));
      this.btnLeft.Location = new System.Drawing.Point(6, 49);
      this.btnLeft.Name = "btnLeft";
      this.btnLeft.Size = new System.Drawing.Size(30, 30);
      this.btnLeft.TabIndex = 6;
      this.btnLeft.UseVisualStyleBackColor = true;
      this.btnLeft.Click += new System.EventHandler(this.left);
      // 
      // btnReverse
      // 
      this.btnReverse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnReverse.Image = ((System.Drawing.Image)(resources.GetObject("btnReverse.Image")));
      this.btnReverse.Location = new System.Drawing.Point(55, 81);
      this.btnReverse.Name = "btnReverse";
      this.btnReverse.Size = new System.Drawing.Size(30, 30);
      this.btnReverse.TabIndex = 7;
      this.btnReverse.UseVisualStyleBackColor = true;
      this.btnReverse.Click += new System.EventHandler(this.reverse);
      // 
      // btnRight
      // 
      this.btnRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnRight.Image = ((System.Drawing.Image)(resources.GetObject("btnRight.Image")));
      this.btnRight.Location = new System.Drawing.Point(104, 49);
      this.btnRight.Name = "btnRight";
      this.btnRight.Size = new System.Drawing.Size(30, 30);
      this.btnRight.TabIndex = 8;
      this.btnRight.UseVisualStyleBackColor = true;
      this.btnRight.Click += new System.EventHandler(this.right);
      // 
      // textBox1
      // 
      this.textBox1.BackColor = System.Drawing.Color.Black;
      this.textBox1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.textBox1.Font = new System.Drawing.Font("Book Antiqua", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBox1.ForeColor = System.Drawing.Color.White;
      this.textBox1.Location = new System.Drawing.Point(6, 117);
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(127, 22);
      this.textBox1.TabIndex = 9;
      this.textBox1.Text = "Click For Arrow Key Control";
      this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
      this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
      // 
      // btnDown
      // 
      this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnDown.Location = new System.Drawing.Point(186, 32);
      this.btnDown.Name = "btnDown";
      this.btnDown.Size = new System.Drawing.Size(79, 26);
      this.btnDown.TabIndex = 15;
      this.btnDown.Text = "Down (Z)";
      this.btnDown.UseVisualStyleBackColor = true;
      this.btnDown.Click += new System.EventHandler(this.down);
      // 
      // button9
      // 
      this.button9.BackColor = System.Drawing.SystemColors.Control;
      this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.button9.ForeColor = System.Drawing.SystemColors.ControlText;
      this.button9.Location = new System.Drawing.Point(271, 32);
      this.button9.Name = "button9";
      this.button9.Size = new System.Drawing.Size(105, 26);
      this.button9.TabIndex = 16;
      this.button9.Text = "Take-Off";
      this.button9.UseVisualStyleBackColor = false;
      this.button9.Click += new System.EventHandler(this.button9_Click);
      // 
      // button11
      // 
      this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.button11.Location = new System.Drawing.Point(271, 60);
      this.button11.Name = "button11";
      this.button11.Size = new System.Drawing.Size(105, 26);
      this.button11.TabIndex = 18;
      this.button11.Text = "Land";
      this.button11.UseVisualStyleBackColor = true;
      this.button11.Click += new System.EventHandler(this.button11_Click);
      // 
      // btnConnect
      // 
      this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnConnect.Location = new System.Drawing.Point(271, 3);
      this.btnConnect.Name = "btnConnect";
      this.btnConnect.Size = new System.Drawing.Size(105, 26);
      this.btnConnect.TabIndex = 23;
      this.btnConnect.Text = "Connect";
      this.btnConnect.UseVisualStyleBackColor = true;
      this.btnConnect.Click += new System.EventHandler(this.button12_Click);
      // 
      // button13
      // 
      this.button13.BackColor = System.Drawing.Color.Red;
      this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.button13.Location = new System.Drawing.Point(271, 89);
      this.button13.Name = "button13";
      this.button13.Size = new System.Drawing.Size(105, 26);
      this.button13.TabIndex = 25;
      this.button13.Text = "Emergency Stop";
      this.button13.UseVisualStyleBackColor = false;
      this.button13.Click += new System.EventHandler(this.button13_Click);
      // 
      // btnUp
      // 
      this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnUp.Location = new System.Drawing.Point(186, 3);
      this.btnUp.Name = "btnUp";
      this.btnUp.Size = new System.Drawing.Size(79, 26);
      this.btnUp.TabIndex = 14;
      this.btnUp.Text = "Up (A)";
      this.btnUp.UseVisualStyleBackColor = true;
      this.btnUp.Click += new System.EventHandler(this.up);
      // 
      // btnRollRight
      // 
      this.btnRollRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnRollRight.Location = new System.Drawing.Point(108, 18);
      this.btnRollRight.Name = "btnRollRight";
      this.btnRollRight.Size = new System.Drawing.Size(26, 23);
      this.btnRollRight.TabIndex = 27;
      this.btnRollRight.Text = "W";
      this.btnRollRight.UseVisualStyleBackColor = true;
      this.btnRollRight.Click += new System.EventHandler(this.rollRight);
      // 
      // btnRollLeft
      // 
      this.btnRollLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnRollLeft.Location = new System.Drawing.Point(6, 18);
      this.btnRollLeft.Name = "btnRollLeft";
      this.btnRollLeft.Size = new System.Drawing.Size(26, 23);
      this.btnRollLeft.TabIndex = 28;
      this.btnRollLeft.Text = "Q";
      this.btnRollLeft.UseVisualStyleBackColor = true;
      this.btnRollLeft.Click += new System.EventHandler(this.rollLeft);
      // 
      // tbLog
      // 
      this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tbLog.Location = new System.Drawing.Point(384, 0);
      this.tbLog.Multiline = true;
      this.tbLog.Name = "tbLog";
      this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.tbLog.Size = new System.Drawing.Size(224, 148);
      this.tbLog.TabIndex = 30;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.trackBar1);
      this.panel1.Controls.Add(this.btnRollLeft);
      this.panel1.Controls.Add(this.btnStop);
      this.panel1.Controls.Add(this.btnForward);
      this.panel1.Controls.Add(this.btnRollRight);
      this.panel1.Controls.Add(this.btnLeft);
      this.panel1.Controls.Add(this.button13);
      this.panel1.Controls.Add(this.btnReverse);
      this.panel1.Controls.Add(this.btnConnect);
      this.panel1.Controls.Add(this.btnRight);
      this.panel1.Controls.Add(this.button11);
      this.panel1.Controls.Add(this.textBox1);
      this.panel1.Controls.Add(this.button9);
      this.panel1.Controls.Add(this.btnUp);
      this.panel1.Controls.Add(this.btnDown);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(384, 148);
      this.panel1.TabIndex = 31;
      // 
      // trackBar1
      // 
      this.trackBar1.LargeChange = 10;
      this.trackBar1.Location = new System.Drawing.Point(140, 3);
      this.trackBar1.Maximum = 255;
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
      this.trackBar1.Size = new System.Drawing.Size(45, 142);
      this.trackBar1.TabIndex = 29;
      this.trackBar1.TickFrequency = 25;
      this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
      // 
      // FormMain
      // 
      this.ClientSize = new System.Drawing.Size(608, 148);
      this.Controls.Add(this.tbLog);
      this.Controls.Add(this.panel1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(555, 148);
      this.Name = "FormMain";
      this.Text = "AR Drone";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDroneMovementPanel_FormClosing);
      this.Load += new System.EventHandler(this.FormDroneMovementPanel_Load);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnStop;
    private System.Windows.Forms.Button btnForward;
    private System.Windows.Forms.Button btnLeft;
    private System.Windows.Forms.Button btnReverse;
    private System.Windows.Forms.Button btnRight;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button btnDown;
    private System.Windows.Forms.Button button9;
    private System.Windows.Forms.Button button11;
    private System.Windows.Forms.Button btnConnect;
    private System.Windows.Forms.Button button13;
    private System.Windows.Forms.Button btnUp;
    private System.Windows.Forms.Button btnRollRight;
    private System.Windows.Forms.Button btnRollLeft;
    private System.Windows.Forms.TextBox tbLog;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TrackBar trackBar1;
  }
}