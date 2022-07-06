namespace gk_lab_final
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.PhongShading = new System.Windows.Forms.RadioButton();
            this.GourardShading = new System.Windows.Forms.RadioButton();
            this.FlatShading = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.LightSourceEnableButton = new System.Windows.Forms.Button();
            this.LightSourceDisableButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.LightSourceY = new System.Windows.Forms.NumericUpDown();
            this.LightSourceZ = new System.Windows.Forms.NumericUpDown();
            this.LightSourceX = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.LightSourcePicker = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TargetY = new System.Windows.Forms.NumericUpDown();
            this.TargetZ = new System.Windows.Forms.NumericUpDown();
            this.TargetX = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CameraZ = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.CameraY = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.CameraX = new System.Windows.Forms.NumericUpDown();
            this.CameraPicker = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.PauseButton = new System.Windows.Forms.Button();
            this.RestartButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LightSourceY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LightSourceZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LightSourceX)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetX)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraX)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar.Location = new System.Drawing.Point(2, 470);
            this.trackBar.Maximum = 150;
            this.trackBar.Minimum = 1;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(1072, 45);
            this.trackBar.TabIndex = 0;
            this.trackBar.Value = 120;
            this.trackBar.Scroll += new System.EventHandler(this.AlphaChanged);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer.Location = new System.Drawing.Point(2, 3);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.mainPictureBox);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.RestartButton);
            this.splitContainer.Panel2.Controls.Add(this.PauseButton);
            this.splitContainer.Panel2.Controls.Add(this.groupBox6);
            this.splitContainer.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer.Size = new System.Drawing.Size(1072, 461);
            this.splitContainer.SplitterDistance = 812;
            this.splitContainer.TabIndex = 2;
            // 
            // mainPictureBox
            // 
            this.mainPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPictureBox.Location = new System.Drawing.Point(0, 0);
            this.mainPictureBox.Name = "mainPictureBox";
            this.mainPictureBox.Size = new System.Drawing.Size(809, 461);
            this.mainPictureBox.TabIndex = 0;
            this.mainPictureBox.TabStop = false;
            this.mainPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPictureBoxPaint);
            this.mainPictureBox.Resize += new System.EventHandler(this.MainPictureBoxResize);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.PhongShading);
            this.groupBox6.Controls.Add(this.GourardShading);
            this.groupBox6.Controls.Add(this.FlatShading);
            this.groupBox6.Location = new System.Drawing.Point(9, 332);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(112, 102);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "shading models";
            // 
            // PhongShading
            // 
            this.PhongShading.AutoSize = true;
            this.PhongShading.Location = new System.Drawing.Point(10, 69);
            this.PhongShading.Name = "PhongShading";
            this.PhongShading.Size = new System.Drawing.Size(60, 19);
            this.PhongShading.TabIndex = 2;
            this.PhongShading.TabStop = true;
            this.PhongShading.Text = "Phong";
            this.PhongShading.UseVisualStyleBackColor = true;
            this.PhongShading.CheckedChanged += new System.EventHandler(this.ShadingChanged);
            // 
            // GourardShading
            // 
            this.GourardShading.AutoSize = true;
            this.GourardShading.Location = new System.Drawing.Point(10, 44);
            this.GourardShading.Name = "GourardShading";
            this.GourardShading.Size = new System.Drawing.Size(68, 19);
            this.GourardShading.TabIndex = 1;
            this.GourardShading.TabStop = true;
            this.GourardShading.Text = "Gourard";
            this.GourardShading.UseVisualStyleBackColor = true;
            this.GourardShading.CheckedChanged += new System.EventHandler(this.ShadingChanged);
            // 
            // FlatShading
            // 
            this.FlatShading.AutoSize = true;
            this.FlatShading.Checked = true;
            this.FlatShading.Location = new System.Drawing.Point(10, 19);
            this.FlatShading.Name = "FlatShading";
            this.FlatShading.Size = new System.Drawing.Size(42, 19);
            this.FlatShading.TabIndex = 0;
            this.FlatShading.TabStop = true;
            this.FlatShading.Text = "flat";
            this.FlatShading.UseVisualStyleBackColor = true;
            this.FlatShading.CheckedChanged += new System.EventHandler(this.ShadingChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.LightSourceEnableButton);
            this.groupBox4.Controls.Add(this.LightSourceDisableButton);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.LightSourcePicker);
            this.groupBox4.Location = new System.Drawing.Point(3, 167);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(246, 159);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "light sources";
            // 
            // LightSourceEnableButton
            // 
            this.LightSourceEnableButton.Location = new System.Drawing.Point(135, 53);
            this.LightSourceEnableButton.Name = "LightSourceEnableButton";
            this.LightSourceEnableButton.Size = new System.Drawing.Size(102, 45);
            this.LightSourceEnableButton.TabIndex = 10;
            this.LightSourceEnableButton.Text = "enable";
            this.LightSourceEnableButton.UseVisualStyleBackColor = true;
            this.LightSourceEnableButton.Click += new System.EventHandler(this.EnableLightSource);
            // 
            // LightSourceDisableButton
            // 
            this.LightSourceDisableButton.Location = new System.Drawing.Point(135, 104);
            this.LightSourceDisableButton.Name = "LightSourceDisableButton";
            this.LightSourceDisableButton.Size = new System.Drawing.Size(102, 45);
            this.LightSourceDisableButton.TabIndex = 9;
            this.LightSourceDisableButton.Text = "disable";
            this.LightSourceDisableButton.UseVisualStyleBackColor = true;
            this.LightSourceDisableButton.Click += new System.EventHandler(this.DisableLightSource);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.LightSourceY);
            this.groupBox5.Controls.Add(this.LightSourceZ);
            this.groupBox5.Controls.Add(this.LightSourceX);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Location = new System.Drawing.Point(6, 45);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(117, 106);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "move";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "Z";
            // 
            // LightSourceY
            // 
            this.LightSourceY.DecimalPlaces = 1;
            this.LightSourceY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.LightSourceY.Location = new System.Drawing.Point(30, 48);
            this.LightSourceY.Name = "LightSourceY";
            this.LightSourceY.Size = new System.Drawing.Size(81, 23);
            this.LightSourceY.TabIndex = 8;
            this.LightSourceY.ValueChanged += new System.EventHandler(this.LightSourcePositionChanged);
            // 
            // LightSourceZ
            // 
            this.LightSourceZ.DecimalPlaces = 1;
            this.LightSourceZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.LightSourceZ.Location = new System.Drawing.Point(30, 77);
            this.LightSourceZ.Name = "LightSourceZ";
            this.LightSourceZ.Size = new System.Drawing.Size(81, 23);
            this.LightSourceZ.TabIndex = 10;
            this.LightSourceZ.ValueChanged += new System.EventHandler(this.LightSourcePositionChanged);
            // 
            // LightSourceX
            // 
            this.LightSourceX.DecimalPlaces = 1;
            this.LightSourceX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.LightSourceX.Location = new System.Drawing.Point(30, 19);
            this.LightSourceX.Name = "LightSourceX";
            this.LightSourceX.Size = new System.Drawing.Size(81, 23);
            this.LightSourceX.TabIndex = 6;
            this.LightSourceX.ValueChanged += new System.EventHandler(this.LightSourcePositionChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 15);
            this.label8.TabIndex = 9;
            this.label8.Text = "Y";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 15);
            this.label9.TabIndex = 7;
            this.label9.Text = "X";
            // 
            // LightSourcePicker
            // 
            this.LightSourcePicker.FormattingEnabled = true;
            this.LightSourcePicker.Location = new System.Drawing.Point(6, 22);
            this.LightSourcePicker.Name = "LightSourcePicker";
            this.LightSourcePicker.Size = new System.Drawing.Size(238, 23);
            this.LightSourcePicker.TabIndex = 0;
            this.LightSourcePicker.SelectedIndexChanged += new System.EventHandler(this.LightSourceChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.CameraPicker);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 158);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "camera";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.TargetY);
            this.groupBox3.Controls.Add(this.TargetZ);
            this.groupBox3.Controls.Add(this.TargetX);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(129, 45);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(117, 106);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "move target";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Z";
            // 
            // TargetY
            // 
            this.TargetY.DecimalPlaces = 1;
            this.TargetY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.TargetY.Location = new System.Drawing.Point(27, 47);
            this.TargetY.Name = "TargetY";
            this.TargetY.Size = new System.Drawing.Size(81, 23);
            this.TargetY.TabIndex = 8;
            this.TargetY.ValueChanged += new System.EventHandler(this.TargetPositionChanged);
            // 
            // TargetZ
            // 
            this.TargetZ.DecimalPlaces = 1;
            this.TargetZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.TargetZ.Location = new System.Drawing.Point(27, 76);
            this.TargetZ.Name = "TargetZ";
            this.TargetZ.Size = new System.Drawing.Size(81, 23);
            this.TargetZ.TabIndex = 10;
            this.TargetZ.ValueChanged += new System.EventHandler(this.TargetPositionChanged);
            // 
            // TargetX
            // 
            this.TargetX.DecimalPlaces = 1;
            this.TargetX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.TargetX.Location = new System.Drawing.Point(27, 18);
            this.TargetX.Name = "TargetX";
            this.TargetX.Size = new System.Drawing.Size(81, 23);
            this.TargetX.TabIndex = 6;
            this.TargetX.ValueChanged += new System.EventHandler(this.TargetPositionChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Y";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "X";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.CameraZ);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.CameraY);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.CameraX);
            this.groupBox2.Location = new System.Drawing.Point(6, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(117, 106);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "move camera";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Z";
            // 
            // CameraZ
            // 
            this.CameraZ.DecimalPlaces = 1;
            this.CameraZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.CameraZ.Location = new System.Drawing.Point(30, 76);
            this.CameraZ.Name = "CameraZ";
            this.CameraZ.Size = new System.Drawing.Size(81, 23);
            this.CameraZ.TabIndex = 4;
            this.CameraZ.ValueChanged += new System.EventHandler(this.CameraPositionChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Y";
            // 
            // CameraY
            // 
            this.CameraY.DecimalPlaces = 1;
            this.CameraY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.CameraY.Location = new System.Drawing.Point(30, 47);
            this.CameraY.Name = "CameraY";
            this.CameraY.Size = new System.Drawing.Size(81, 23);
            this.CameraY.TabIndex = 2;
            this.CameraY.ValueChanged += new System.EventHandler(this.CameraPositionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "X";
            // 
            // CameraX
            // 
            this.CameraX.DecimalPlaces = 1;
            this.CameraX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.CameraX.Location = new System.Drawing.Point(30, 18);
            this.CameraX.Name = "CameraX";
            this.CameraX.Size = new System.Drawing.Size(81, 23);
            this.CameraX.TabIndex = 0;
            this.CameraX.ValueChanged += new System.EventHandler(this.CameraPositionChanged);
            // 
            // CameraPicker
            // 
            this.CameraPicker.FormattingEnabled = true;
            this.CameraPicker.Location = new System.Drawing.Point(6, 22);
            this.CameraPicker.Name = "CameraPicker";
            this.CameraPicker.Size = new System.Drawing.Size(238, 23);
            this.CameraPicker.TabIndex = 0;
            this.CameraPicker.SelectedIndexChanged += new System.EventHandler(this.CameraChanged);
            // 
            // PauseButton
            // 
            this.PauseButton.Location = new System.Drawing.Point(138, 338);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(102, 45);
            this.PauseButton.TabIndex = 11;
            this.PauseButton.Text = "pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            // 
            // RestartButton
            // 
            this.RestartButton.Location = new System.Drawing.Point(138, 389);
            this.RestartButton.Name = "RestartButton";
            this.RestartButton.Size = new System.Drawing.Size(102, 45);
            this.RestartButton.TabIndex = 12;
            this.RestartButton.Text = "restart";
            this.RestartButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 508);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.trackBar);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.FormShown);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LightSourceY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LightSourceZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LightSourceX)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TargetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetX)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.PictureBox mainPictureBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox CameraPicker;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton PhongShading;
        private System.Windows.Forms.RadioButton GourardShading;
        private System.Windows.Forms.RadioButton FlatShading;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button LightSourceEnableButton;
        private System.Windows.Forms.Button LightSourceDisableButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox LightSourcePicker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown LightSourceY;
        private System.Windows.Forms.NumericUpDown LightSourceZ;
        private System.Windows.Forms.NumericUpDown LightSourceX;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown TargetY;
        private System.Windows.Forms.NumericUpDown TargetZ;
        private System.Windows.Forms.NumericUpDown TargetX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown CameraZ;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown CameraY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown CameraX;
        private System.Windows.Forms.Button RestartButton;
        private System.Windows.Forms.Button PauseButton;
    }
}
