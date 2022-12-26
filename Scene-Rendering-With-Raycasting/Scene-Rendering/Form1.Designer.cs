namespace SceneRendering
{
    partial class Form1
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
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.xNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.zNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.yNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.FOVTrackBar = new System.Windows.Forms.TrackBar();
            this.clearSceneButton = new System.Windows.Forms.Button();
            this.paintObjectsCheckBox = new System.Windows.Forms.CheckBox();
            this.kaLabel = new System.Windows.Forms.Label();
            this.modifyNormalMapcheckBox = new System.Windows.Forms.CheckBox();
            this.loadNormalMapButton = new System.Windows.Forms.Button();
            this.changeLightColorButton = new System.Windows.Forms.Button();
            this.loadObjFileButton = new System.Windows.Forms.Button();
            this.interpolationGroupBox = new System.Windows.Forms.GroupBox();
            this.normalRadioButton = new System.Windows.Forms.RadioButton();
            this.colorRadioButton = new System.Windows.Forms.RadioButton();
            this.ColorGroupBox = new System.Windows.Forms.GroupBox();
            this.bitmapColorRadioButton = new System.Windows.Forms.RadioButton();
            this.constColorRadioButton = new System.Windows.Forms.RadioButton();
            this.paintTriangulationCheckBox = new System.Windows.Forms.CheckBox();
            this.animationCheckBox = new System.Windows.Forms.CheckBox();
            this.zLabel = new System.Windows.Forms.Label();
            this.zTrackBar = new System.Windows.Forms.TrackBar();
            this.kdLabel = new System.Windows.Forms.Label();
            this.ksLabel = new System.Windows.Forms.Label();
            this.mLabel = new System.Windows.Forms.Label();
            this.mTrackBar = new System.Windows.Forms.TrackBar();
            this.ksTrackBar = new System.Windows.Forms.TrackBar();
            this.kdTrackBar = new System.Windows.Forms.TrackBar();
            this.kaTrackBar = new System.Windows.Forms.TrackBar();
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.surfaceColorDialog = new System.Windows.Forms.ColorDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lightColorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FOVTrackBar)).BeginInit();
            this.interpolationGroupBox.SuspendLayout();
            this.ColorGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ksTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kaTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(1179, 1055);
            this.Canvas.TabIndex = 0;
            this.Canvas.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Canvas);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1423, 1055);
            this.splitContainer1.SplitterDistance = 1179;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.xNumericUpDown);
            this.groupBox1.Controls.Add(this.zNumericUpDown);
            this.groupBox1.Controls.Add(this.yNumericUpDown);
            this.groupBox1.Controls.Add(this.FOVTrackBar);
            this.groupBox1.Controls.Add(this.clearSceneButton);
            this.groupBox1.Controls.Add(this.paintObjectsCheckBox);
            this.groupBox1.Controls.Add(this.kaLabel);
            this.groupBox1.Controls.Add(this.modifyNormalMapcheckBox);
            this.groupBox1.Controls.Add(this.loadNormalMapButton);
            this.groupBox1.Controls.Add(this.changeLightColorButton);
            this.groupBox1.Controls.Add(this.loadObjFileButton);
            this.groupBox1.Controls.Add(this.interpolationGroupBox);
            this.groupBox1.Controls.Add(this.ColorGroupBox);
            this.groupBox1.Controls.Add(this.paintTriangulationCheckBox);
            this.groupBox1.Controls.Add(this.animationCheckBox);
            this.groupBox1.Controls.Add(this.zLabel);
            this.groupBox1.Controls.Add(this.zTrackBar);
            this.groupBox1.Controls.Add(this.kdLabel);
            this.groupBox1.Controls.Add(this.ksLabel);
            this.groupBox1.Controls.Add(this.mLabel);
            this.groupBox1.Controls.Add(this.mTrackBar);
            this.groupBox1.Controls.Add(this.ksTrackBar);
            this.groupBox1.Controls.Add(this.kdTrackBar);
            this.groupBox1.Controls.Add(this.kaTrackBar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 1055);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameter adjustment";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 902);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 20);
            this.label4.TabIndex = 32;
            this.label4.Text = "FOV:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 1006);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 20);
            this.label3.TabIndex = 31;
            this.label3.Text = "Z:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 975);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 20);
            this.label2.TabIndex = 30;
            this.label2.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 947);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 20);
            this.label1.TabIndex = 29;
            this.label1.Text = "X:";
            // 
            // xNumericUpDown
            // 
            this.xNumericUpDown.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.xNumericUpDown.Location = new System.Drawing.Point(50, 940);
            this.xNumericUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.xNumericUpDown.Name = "xNumericUpDown";
            this.xNumericUpDown.Size = new System.Drawing.Size(150, 27);
            this.xNumericUpDown.TabIndex = 28;
            this.xNumericUpDown.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // zNumericUpDown
            // 
            this.zNumericUpDown.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.zNumericUpDown.Location = new System.Drawing.Point(50, 1004);
            this.zNumericUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.zNumericUpDown.Name = "zNumericUpDown";
            this.zNumericUpDown.Size = new System.Drawing.Size(150, 27);
            this.zNumericUpDown.TabIndex = 27;
            this.zNumericUpDown.Value = new decimal(new int[] {
            1650,
            0,
            0,
            0});
            // 
            // yNumericUpDown
            // 
            this.yNumericUpDown.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.yNumericUpDown.Location = new System.Drawing.Point(49, 973);
            this.yNumericUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.yNumericUpDown.Name = "yNumericUpDown";
            this.yNumericUpDown.Size = new System.Drawing.Size(150, 27);
            this.yNumericUpDown.TabIndex = 26;
            this.yNumericUpDown.Value = new decimal(new int[] {
            850,
            0,
            0,
            0});
            // 
            // FOVTrackBar
            // 
            this.FOVTrackBar.Location = new System.Drawing.Point(49, 894);
            this.FOVTrackBar.Maximum = 120;
            this.FOVTrackBar.Minimum = 30;
            this.FOVTrackBar.Name = "FOVTrackBar";
            this.FOVTrackBar.Size = new System.Drawing.Size(130, 56);
            this.FOVTrackBar.TabIndex = 25;
            this.FOVTrackBar.Value = 30;
            // 
            // clearSceneButton
            // 
            this.clearSceneButton.Location = new System.Drawing.Point(1, 859);
            this.clearSceneButton.Name = "clearSceneButton";
            this.clearSceneButton.Size = new System.Drawing.Size(210, 29);
            this.clearSceneButton.TabIndex = 24;
            this.clearSceneButton.Text = "Clear Scene";
            this.clearSceneButton.UseVisualStyleBackColor = true;
            this.clearSceneButton.Click += new System.EventHandler(this.clearSceneButton_Click);
            // 
            // paintObjectsCheckBox
            // 
            this.paintObjectsCheckBox.AutoSize = true;
            this.paintObjectsCheckBox.Checked = true;
            this.paintObjectsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.paintObjectsCheckBox.Location = new System.Drawing.Point(10, 661);
            this.paintObjectsCheckBox.Name = "paintObjectsCheckBox";
            this.paintObjectsCheckBox.Size = new System.Drawing.Size(115, 24);
            this.paintObjectsCheckBox.TabIndex = 23;
            this.paintObjectsCheckBox.Text = "Paint objects";
            this.paintObjectsCheckBox.UseVisualStyleBackColor = true;
            this.paintObjectsCheckBox.CheckedChanged += new System.EventHandler(this.paintObjectsCheckBox_CheckedChanged);
            // 
            // kaLabel
            // 
            this.kaLabel.AutoSize = true;
            this.kaLabel.Location = new System.Drawing.Point(6, 140);
            this.kaLabel.Name = "kaLabel";
            this.kaLabel.Size = new System.Drawing.Size(27, 20);
            this.kaLabel.TabIndex = 21;
            this.kaLabel.Text = "ka:";
            // 
            // modifyNormalMapcheckBox
            // 
            this.modifyNormalMapcheckBox.AutoSize = true;
            this.modifyNormalMapcheckBox.Location = new System.Drawing.Point(10, 691);
            this.modifyNormalMapcheckBox.Name = "modifyNormalMapcheckBox";
            this.modifyNormalMapcheckBox.Size = new System.Drawing.Size(222, 24);
            this.modifyNormalMapcheckBox.TabIndex = 19;
            this.modifyNormalMapcheckBox.Text = "Use Modifed Normal Vectors";
            this.modifyNormalMapcheckBox.UseVisualStyleBackColor = true;
            this.modifyNormalMapcheckBox.CheckedChanged += new System.EventHandler(this.modifyNormalMapcheckBox_CheckedChanged);
            // 
            // loadNormalMapButton
            // 
            this.loadNormalMapButton.Location = new System.Drawing.Point(6, 824);
            this.loadNormalMapButton.Name = "loadNormalMapButton";
            this.loadNormalMapButton.Size = new System.Drawing.Size(216, 29);
            this.loadNormalMapButton.TabIndex = 18;
            this.loadNormalMapButton.Text = "Load Normal Map";
            this.loadNormalMapButton.UseVisualStyleBackColor = true;
            this.loadNormalMapButton.Click += new System.EventHandler(this.loadNormalMapButton_Click);
            // 
            // changeLightColorButton
            // 
            this.changeLightColorButton.Location = new System.Drawing.Point(6, 780);
            this.changeLightColorButton.Name = "changeLightColorButton";
            this.changeLightColorButton.Size = new System.Drawing.Size(216, 29);
            this.changeLightColorButton.TabIndex = 17;
            this.changeLightColorButton.Text = "Change Light Color";
            this.changeLightColorButton.UseVisualStyleBackColor = true;
            this.changeLightColorButton.Click += new System.EventHandler(this.changeLightColorButton_Click);
            // 
            // loadObjFileButton
            // 
            this.loadObjFileButton.Location = new System.Drawing.Point(6, 735);
            this.loadObjFileButton.Name = "loadObjFileButton";
            this.loadObjFileButton.Size = new System.Drawing.Size(216, 29);
            this.loadObjFileButton.TabIndex = 16;
            this.loadObjFileButton.Text = "Load OBJ FIle";
            this.loadObjFileButton.UseVisualStyleBackColor = true;
            this.loadObjFileButton.Click += new System.EventHandler(this.loadObjFileButton_Click);
            // 
            // interpolationGroupBox
            // 
            this.interpolationGroupBox.Controls.Add(this.normalRadioButton);
            this.interpolationGroupBox.Controls.Add(this.colorRadioButton);
            this.interpolationGroupBox.Location = new System.Drawing.Point(2, 348);
            this.interpolationGroupBox.Name = "interpolationGroupBox";
            this.interpolationGroupBox.Size = new System.Drawing.Size(235, 98);
            this.interpolationGroupBox.TabIndex = 1;
            this.interpolationGroupBox.TabStop = false;
            this.interpolationGroupBox.Text = "Interpolation";
            // 
            // normalRadioButton
            // 
            this.normalRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.normalRadioButton.AutoSize = true;
            this.normalRadioButton.Checked = true;
            this.normalRadioButton.Location = new System.Drawing.Point(18, 26);
            this.normalRadioButton.Name = "normalRadioButton";
            this.normalRadioButton.Size = new System.Drawing.Size(209, 24);
            this.normalRadioButton.TabIndex = 6;
            this.normalRadioButton.TabStop = true;
            this.normalRadioButton.Text = "Interpolate Normal Vectors";
            this.normalRadioButton.UseVisualStyleBackColor = true;
            this.normalRadioButton.CheckedChanged += new System.EventHandler(this.normalRadioButton_CheckedChanged);
            // 
            // colorRadioButton
            // 
            this.colorRadioButton.AutoSize = true;
            this.colorRadioButton.Location = new System.Drawing.Point(18, 56);
            this.colorRadioButton.Name = "colorRadioButton";
            this.colorRadioButton.Size = new System.Drawing.Size(143, 24);
            this.colorRadioButton.TabIndex = 7;
            this.colorRadioButton.Text = "Interpolate Color";
            this.colorRadioButton.UseVisualStyleBackColor = true;
            this.colorRadioButton.CheckedChanged += new System.EventHandler(this.colorRadioButton_CheckedChanged);
            // 
            // ColorGroupBox
            // 
            this.ColorGroupBox.Controls.Add(this.bitmapColorRadioButton);
            this.ColorGroupBox.Controls.Add(this.constColorRadioButton);
            this.ColorGroupBox.Location = new System.Drawing.Point(2, 452);
            this.ColorGroupBox.Name = "ColorGroupBox";
            this.ColorGroupBox.Size = new System.Drawing.Size(234, 102);
            this.ColorGroupBox.TabIndex = 15;
            this.ColorGroupBox.TabStop = false;
            this.ColorGroupBox.Text = "Color";
            // 
            // bitmapColorRadioButton
            // 
            this.bitmapColorRadioButton.AutoSize = true;
            this.bitmapColorRadioButton.Checked = true;
            this.bitmapColorRadioButton.Location = new System.Drawing.Point(21, 26);
            this.bitmapColorRadioButton.Name = "bitmapColorRadioButton";
            this.bitmapColorRadioButton.Size = new System.Drawing.Size(156, 24);
            this.bitmapColorRadioButton.TabIndex = 14;
            this.bitmapColorRadioButton.TabStop = true;
            this.bitmapColorRadioButton.Text = "Color From Bitmap";
            this.bitmapColorRadioButton.UseVisualStyleBackColor = true;
            this.bitmapColorRadioButton.Click += new System.EventHandler(this.bitmapColorRadioButton_Click);
            // 
            // constColorRadioButton
            // 
            this.constColorRadioButton.AutoSize = true;
            this.constColorRadioButton.Location = new System.Drawing.Point(21, 56);
            this.constColorRadioButton.Name = "constColorRadioButton";
            this.constColorRadioButton.Size = new System.Drawing.Size(128, 24);
            this.constColorRadioButton.TabIndex = 13;
            this.constColorRadioButton.Text = "Constant Color";
            this.constColorRadioButton.UseVisualStyleBackColor = true;
            this.constColorRadioButton.Click += new System.EventHandler(this.constColorRadioButton_Click);
            // 
            // paintTriangulationCheckBox
            // 
            this.paintTriangulationCheckBox.AutoSize = true;
            this.paintTriangulationCheckBox.Checked = true;
            this.paintTriangulationCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.paintTriangulationCheckBox.Location = new System.Drawing.Point(10, 571);
            this.paintTriangulationCheckBox.Name = "paintTriangulationCheckBox";
            this.paintTriangulationCheckBox.Size = new System.Drawing.Size(154, 24);
            this.paintTriangulationCheckBox.TabIndex = 11;
            this.paintTriangulationCheckBox.Text = "Paint Triangulation";
            this.paintTriangulationCheckBox.UseVisualStyleBackColor = true;
            this.paintTriangulationCheckBox.CheckedChanged += new System.EventHandler(this.paintTriangulationCheckBox_CheckedChanged);
            // 
            // animationCheckBox
            // 
            this.animationCheckBox.AutoSize = true;
            this.animationCheckBox.Checked = true;
            this.animationCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.animationCheckBox.Location = new System.Drawing.Point(10, 601);
            this.animationCheckBox.Name = "animationCheckBox";
            this.animationCheckBox.Size = new System.Drawing.Size(100, 24);
            this.animationCheckBox.TabIndex = 10;
            this.animationCheckBox.Text = "Animation";
            this.animationCheckBox.UseVisualStyleBackColor = true;
            this.animationCheckBox.CheckedChanged += new System.EventHandler(this.animationCheckBox_CheckedChanged);
            // 
            // zLabel
            // 
            this.zLabel.AutoSize = true;
            this.zLabel.Location = new System.Drawing.Point(12, 264);
            this.zLabel.Name = "zLabel";
            this.zLabel.Size = new System.Drawing.Size(19, 20);
            this.zLabel.TabIndex = 9;
            this.zLabel.Text = "z:";
            // 
            // zTrackBar
            // 
            this.zTrackBar.Location = new System.Drawing.Point(6, 290);
            this.zTrackBar.Maximum = 2500;
            this.zTrackBar.Name = "zTrackBar";
            this.zTrackBar.Size = new System.Drawing.Size(234, 56);
            this.zTrackBar.TabIndex = 8;
            this.zTrackBar.TickFrequency = 10;
            this.zTrackBar.Value = 700;
            this.zTrackBar.ValueChanged += new System.EventHandler(this.zTrackBar_ValueChanged);
            // 
            // kdLabel
            // 
            this.kdLabel.AutoSize = true;
            this.kdLabel.Location = new System.Drawing.Point(3, 23);
            this.kdLabel.Name = "kdLabel";
            this.kdLabel.Size = new System.Drawing.Size(28, 20);
            this.kdLabel.TabIndex = 5;
            this.kdLabel.Text = "kd:";
            // 
            // ksLabel
            // 
            this.ksLabel.AutoSize = true;
            this.ksLabel.Location = new System.Drawing.Point(6, 82);
            this.ksLabel.Name = "ksLabel";
            this.ksLabel.Size = new System.Drawing.Size(25, 20);
            this.ksLabel.TabIndex = 4;
            this.ksLabel.Text = "ks:";
            // 
            // mLabel
            // 
            this.mLabel.AutoSize = true;
            this.mLabel.Location = new System.Drawing.Point(8, 202);
            this.mLabel.Name = "mLabel";
            this.mLabel.Size = new System.Drawing.Size(25, 20);
            this.mLabel.TabIndex = 10;
            this.mLabel.Text = "m:";
            // 
            // mTrackBar
            // 
            this.mTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mTrackBar.Location = new System.Drawing.Point(10, 228);
            this.mTrackBar.Maximum = 100;
            this.mTrackBar.Minimum = 1;
            this.mTrackBar.Name = "mTrackBar";
            this.mTrackBar.Size = new System.Drawing.Size(224, 56);
            this.mTrackBar.TabIndex = 2;
            this.mTrackBar.TickFrequency = 10;
            this.mTrackBar.Value = 50;
            this.mTrackBar.ValueChanged += new System.EventHandler(this.mTrackBar_ValueChanged);
            // 
            // ksTrackBar
            // 
            this.ksTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ksTrackBar.Location = new System.Drawing.Point(4, 104);
            this.ksTrackBar.Maximum = 100;
            this.ksTrackBar.Name = "ksTrackBar";
            this.ksTrackBar.Size = new System.Drawing.Size(227, 56);
            this.ksTrackBar.TabIndex = 1;
            this.ksTrackBar.TickFrequency = 10;
            this.ksTrackBar.Value = 50;
            this.ksTrackBar.ValueChanged += new System.EventHandler(this.ksTrackBar_ValueChanged);
            // 
            // kdTrackBar
            // 
            this.kdTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kdTrackBar.Location = new System.Drawing.Point(1, 46);
            this.kdTrackBar.Maximum = 100;
            this.kdTrackBar.Name = "kdTrackBar";
            this.kdTrackBar.Size = new System.Drawing.Size(230, 56);
            this.kdTrackBar.TabIndex = 0;
            this.kdTrackBar.TickFrequency = 10;
            this.kdTrackBar.Value = 50;
            this.kdTrackBar.ValueChanged += new System.EventHandler(this.kdTrackBar_ValueChanged);
            // 
            // kaTrackBar
            // 
            this.kaTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kaTrackBar.Location = new System.Drawing.Point(6, 166);
            this.kaTrackBar.Maximum = 100;
            this.kaTrackBar.Name = "kaTrackBar";
            this.kaTrackBar.Size = new System.Drawing.Size(225, 56);
            this.kaTrackBar.TabIndex = 20;
            this.kaTrackBar.TickFrequency = 10;
            this.kaTrackBar.Value = 10;
            this.kaTrackBar.ValueChanged += new System.EventHandler(this.kaTrackBar_ValueChanged);
            // 
            // animationTimer
            // 
            this.animationTimer.Enabled = true;
            this.animationTimer.Interval = 50;
            this.animationTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1423, 1055);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FOVTrackBar)).EndInit();
            this.interpolationGroupBox.ResumeLayout(false);
            this.interpolationGroupBox.PerformLayout();
            this.ColorGroupBox.ResumeLayout(false);
            this.ColorGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ksTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kaTrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox Canvas;
        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        private Label mLabel;
        private TrackBar mTrackBar;
        private TrackBar ksTrackBar;
        private TrackBar kdTrackBar;
        private Label kdLabel;
        private Label ksLabel;
        private RadioButton colorRadioButton;
        private RadioButton normalRadioButton;
        private System.Windows.Forms.Timer animationTimer;
        private Label zLabel;
        private TrackBar zTrackBar;
        private CheckBox animationCheckBox;
        private CheckBox paintTriangulationCheckBox;
        private ColorDialog surfaceColorDialog;
        private RadioButton bitmapColorRadioButton;
        private RadioButton constColorRadioButton;
        private GroupBox interpolationGroupBox;
        private GroupBox ColorGroupBox;
        private Button loadObjFileButton;
        private OpenFileDialog openFileDialog1;
        private ColorDialog lightColorDialog;
        private Button changeLightColorButton;
        private Button loadNormalMapButton;
        private CheckBox modifyNormalMapcheckBox;
        private TrackBar kaTrackBar;
        private Label kaLabel;
        private CheckBox paintObjectsCheckBox;
        private Button clearSceneButton;
        private NumericUpDown xNumericUpDown;
        private NumericUpDown zNumericUpDown;
        private NumericUpDown yNumericUpDown;
        private TrackBar FOVTrackBar;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}