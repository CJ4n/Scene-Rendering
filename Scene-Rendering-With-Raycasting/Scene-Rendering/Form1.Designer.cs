﻿namespace SceneRendering
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
            this.dayAndNightCheckBox = new System.Windows.Forms.CheckBox();
            this.fogCheckBox = new System.Windows.Forms.CheckBox();
            this.light3reflectorCheckBox = new System.Windows.Forms.CheckBox();
            this.light2CheckBox = new System.Windows.Forms.CheckBox();
            this.light1CheckBox = new System.Windows.Forms.CheckBox();
            this.oscilationCheckBox = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.animateLightCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.xNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.zNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.yNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.FOVTrackBar = new System.Windows.Forms.TrackBar();
            this.paintObjectsCheckBox = new System.Windows.Forms.CheckBox();
            this.kaLabel = new System.Windows.Forms.Label();
            this.changeLightColorButton = new System.Windows.Forms.Button();
            this.interpolationGroupBox = new System.Windows.Forms.GroupBox();
            this.interpolateConstradioButton = new System.Windows.Forms.RadioButton();
            this.interpolateNormalRadioButton = new System.Windows.Forms.RadioButton();
            this.interpolateColorRadioButton = new System.Windows.Forms.RadioButton();
            this.ColorGroupBox = new System.Windows.Forms.GroupBox();
            this.constColorRadioButton = new System.Windows.Forms.RadioButton();
            this.paintTriangulationCheckBox = new System.Windows.Forms.CheckBox();
            this.animateObjectCheckBox = new System.Windows.Forms.CheckBox();
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
            this.groupBox1.Controls.Add(this.dayAndNightCheckBox);
            this.groupBox1.Controls.Add(this.fogCheckBox);
            this.groupBox1.Controls.Add(this.light3reflectorCheckBox);
            this.groupBox1.Controls.Add(this.light2CheckBox);
            this.groupBox1.Controls.Add(this.light1CheckBox);
            this.groupBox1.Controls.Add(this.oscilationCheckBox);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.animateLightCheckBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.xNumericUpDown);
            this.groupBox1.Controls.Add(this.zNumericUpDown);
            this.groupBox1.Controls.Add(this.yNumericUpDown);
            this.groupBox1.Controls.Add(this.FOVTrackBar);
            this.groupBox1.Controls.Add(this.paintObjectsCheckBox);
            this.groupBox1.Controls.Add(this.kaLabel);
            this.groupBox1.Controls.Add(this.changeLightColorButton);
            this.groupBox1.Controls.Add(this.interpolationGroupBox);
            this.groupBox1.Controls.Add(this.ColorGroupBox);
            this.groupBox1.Controls.Add(this.paintTriangulationCheckBox);
            this.groupBox1.Controls.Add(this.animateObjectCheckBox);
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
            // dayAndNightCheckBox
            // 
            this.dayAndNightCheckBox.AutoSize = true;
            this.dayAndNightCheckBox.Checked = true;
            this.dayAndNightCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dayAndNightCheckBox.Location = new System.Drawing.Point(8, 812);
            this.dayAndNightCheckBox.Name = "dayAndNightCheckBox";
            this.dayAndNightCheckBox.Size = new System.Drawing.Size(127, 24);
            this.dayAndNightCheckBox.TabIndex = 40;
            this.dayAndNightCheckBox.Text = "Day and Night";
            this.dayAndNightCheckBox.UseVisualStyleBackColor = true;
            this.dayAndNightCheckBox.CheckedChanged += new System.EventHandler(this.dayAndNightCheckBox_CheckedChanged);
            // 
            // fogCheckBox
            // 
            this.fogCheckBox.AutoSize = true;
            this.fogCheckBox.Checked = true;
            this.fogCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fogCheckBox.Location = new System.Drawing.Point(10, 541);
            this.fogCheckBox.Name = "fogCheckBox";
            this.fogCheckBox.Size = new System.Drawing.Size(56, 24);
            this.fogCheckBox.TabIndex = 39;
            this.fogCheckBox.Text = "Fog";
            this.fogCheckBox.UseVisualStyleBackColor = true;
            this.fogCheckBox.CheckedChanged += new System.EventHandler(this.fogCheckBox_CheckedChanged);
            // 
            // light3reflectorCheckBox
            // 
            this.light3reflectorCheckBox.AutoSize = true;
            this.light3reflectorCheckBox.Checked = true;
            this.light3reflectorCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.light3reflectorCheckBox.Location = new System.Drawing.Point(9, 781);
            this.light3reflectorCheckBox.Name = "light3reflectorCheckBox";
            this.light3reflectorCheckBox.Size = new System.Drawing.Size(146, 24);
            this.light3reflectorCheckBox.TabIndex = 38;
            this.light3reflectorCheckBox.Text = "Light 3 (reflector)";
            this.light3reflectorCheckBox.UseVisualStyleBackColor = true;
            this.light3reflectorCheckBox.CheckedChanged += new System.EventHandler(this.light3reflectorCheckBox_CheckedChanged);
            // 
            // light2CheckBox
            // 
            this.light2CheckBox.AutoSize = true;
            this.light2CheckBox.Checked = true;
            this.light2CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.light2CheckBox.Location = new System.Drawing.Point(10, 751);
            this.light2CheckBox.Name = "light2CheckBox";
            this.light2CheckBox.Size = new System.Drawing.Size(76, 24);
            this.light2CheckBox.TabIndex = 37;
            this.light2CheckBox.Text = "Light 2";
            this.light2CheckBox.UseVisualStyleBackColor = true;
            this.light2CheckBox.CheckedChanged += new System.EventHandler(this.light2CheckBox_CheckedChanged);
            // 
            // light1CheckBox
            // 
            this.light1CheckBox.AutoSize = true;
            this.light1CheckBox.Checked = true;
            this.light1CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.light1CheckBox.Location = new System.Drawing.Point(10, 721);
            this.light1CheckBox.Name = "light1CheckBox";
            this.light1CheckBox.Size = new System.Drawing.Size(76, 24);
            this.light1CheckBox.TabIndex = 36;
            this.light1CheckBox.Text = "Light 1";
            this.light1CheckBox.UseVisualStyleBackColor = true;
            this.light1CheckBox.CheckedChanged += new System.EventHandler(this.light1CheckBox_CheckedChanged);
            // 
            // oscilationCheckBox
            // 
            this.oscilationCheckBox.AutoSize = true;
            this.oscilationCheckBox.Checked = true;
            this.oscilationCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.oscilationCheckBox.Location = new System.Drawing.Point(10, 691);
            this.oscilationCheckBox.Name = "oscilationCheckBox";
            this.oscilationCheckBox.Size = new System.Drawing.Size(97, 24);
            this.oscilationCheckBox.TabIndex = 35;
            this.oscilationCheckBox.Text = "Oscilation";
            this.oscilationCheckBox.UseVisualStyleBackColor = true;
            this.oscilationCheckBox.CheckedChanged += new System.EventHandler(this.oscilationCheckBox_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 844);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(151, 28);
            this.comboBox1.TabIndex = 34;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // animateLightCheckBox
            // 
            this.animateLightCheckBox.AutoSize = true;
            this.animateLightCheckBox.Checked = true;
            this.animateLightCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.animateLightCheckBox.Location = new System.Drawing.Point(9, 630);
            this.animateLightCheckBox.Name = "animateLightCheckBox";
            this.animateLightCheckBox.Size = new System.Drawing.Size(121, 24);
            this.animateLightCheckBox.TabIndex = 33;
            this.animateLightCheckBox.Text = "Animate light";
            this.animateLightCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 921);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 20);
            this.label4.TabIndex = 32;
            this.label4.Text = "FOV:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 1025);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 20);
            this.label3.TabIndex = 31;
            this.label3.Text = "Z:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 994);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 20);
            this.label2.TabIndex = 30;
            this.label2.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 966);
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
            this.xNumericUpDown.Location = new System.Drawing.Point(53, 959);
            this.xNumericUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.xNumericUpDown.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            -2147483648});
            this.xNumericUpDown.Name = "xNumericUpDown";
            this.xNumericUpDown.Size = new System.Drawing.Size(150, 27);
            this.xNumericUpDown.TabIndex = 28;
            this.xNumericUpDown.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.xNumericUpDown.ValueChanged += new System.EventHandler(this.CameraPositionChanged);
            // 
            // zNumericUpDown
            // 
            this.zNumericUpDown.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.zNumericUpDown.Location = new System.Drawing.Point(53, 1023);
            this.zNumericUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.zNumericUpDown.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            -2147483648});
            this.zNumericUpDown.Name = "zNumericUpDown";
            this.zNumericUpDown.Size = new System.Drawing.Size(150, 27);
            this.zNumericUpDown.TabIndex = 27;
            this.zNumericUpDown.Value = new decimal(new int[] {
            1650,
            0,
            0,
            0});
            this.zNumericUpDown.ValueChanged += new System.EventHandler(this.CameraPositionChanged);
            // 
            // yNumericUpDown
            // 
            this.yNumericUpDown.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.yNumericUpDown.Location = new System.Drawing.Point(52, 992);
            this.yNumericUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.yNumericUpDown.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            -2147483648});
            this.yNumericUpDown.Name = "yNumericUpDown";
            this.yNumericUpDown.Size = new System.Drawing.Size(150, 27);
            this.yNumericUpDown.TabIndex = 26;
            this.yNumericUpDown.Value = new decimal(new int[] {
            850,
            0,
            0,
            0});
            this.yNumericUpDown.ValueChanged += new System.EventHandler(this.CameraPositionChanged);
            // 
            // FOVTrackBar
            // 
            this.FOVTrackBar.Location = new System.Drawing.Point(53, 913);
            this.FOVTrackBar.Maximum = 120;
            this.FOVTrackBar.Minimum = 30;
            this.FOVTrackBar.Name = "FOVTrackBar";
            this.FOVTrackBar.Size = new System.Drawing.Size(130, 56);
            this.FOVTrackBar.TabIndex = 25;
            this.FOVTrackBar.Value = 90;
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
            // changeLightColorButton
            // 
            this.changeLightColorButton.Location = new System.Drawing.Point(6, 878);
            this.changeLightColorButton.Name = "changeLightColorButton";
            this.changeLightColorButton.Size = new System.Drawing.Size(216, 29);
            this.changeLightColorButton.TabIndex = 17;
            this.changeLightColorButton.Text = "Change Light Color";
            this.changeLightColorButton.UseVisualStyleBackColor = true;
            this.changeLightColorButton.Click += new System.EventHandler(this.changeLightColorButton_Click);
            // 
            // interpolationGroupBox
            // 
            this.interpolationGroupBox.Controls.Add(this.interpolateConstradioButton);
            this.interpolationGroupBox.Controls.Add(this.interpolateNormalRadioButton);
            this.interpolationGroupBox.Controls.Add(this.interpolateColorRadioButton);
            this.interpolationGroupBox.Location = new System.Drawing.Point(6, 332);
            this.interpolationGroupBox.Name = "interpolationGroupBox";
            this.interpolationGroupBox.Size = new System.Drawing.Size(235, 126);
            this.interpolationGroupBox.TabIndex = 1;
            this.interpolationGroupBox.TabStop = false;
            this.interpolationGroupBox.Text = "Interpolation";
            // 
            // interpolateConstradioButton
            // 
            this.interpolateConstradioButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.interpolateConstradioButton.AutoSize = true;
            this.interpolateConstradioButton.Location = new System.Drawing.Point(17, 36);
            this.interpolateConstradioButton.Name = "interpolateConstradioButton";
            this.interpolateConstradioButton.Size = new System.Drawing.Size(165, 24);
            this.interpolateConstradioButton.TabIndex = 8;
            this.interpolateConstradioButton.Text = "Interpolate Constant";
            this.interpolateConstradioButton.UseVisualStyleBackColor = true;
            // 
            // interpolateNormalRadioButton
            // 
            this.interpolateNormalRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.interpolateNormalRadioButton.AutoSize = true;
            this.interpolateNormalRadioButton.Checked = true;
            this.interpolateNormalRadioButton.Location = new System.Drawing.Point(17, 66);
            this.interpolateNormalRadioButton.Name = "interpolateNormalRadioButton";
            this.interpolateNormalRadioButton.Size = new System.Drawing.Size(209, 24);
            this.interpolateNormalRadioButton.TabIndex = 6;
            this.interpolateNormalRadioButton.TabStop = true;
            this.interpolateNormalRadioButton.Text = "Interpolate Normal Vectors";
            this.interpolateNormalRadioButton.UseVisualStyleBackColor = true;
            this.interpolateNormalRadioButton.CheckedChanged += new System.EventHandler(this.normalRadioButton_CheckedChanged);
            // 
            // interpolateColorRadioButton
            // 
            this.interpolateColorRadioButton.AutoSize = true;
            this.interpolateColorRadioButton.Location = new System.Drawing.Point(19, 96);
            this.interpolateColorRadioButton.Name = "interpolateColorRadioButton";
            this.interpolateColorRadioButton.Size = new System.Drawing.Size(143, 24);
            this.interpolateColorRadioButton.TabIndex = 7;
            this.interpolateColorRadioButton.Text = "Interpolate Color";
            this.interpolateColorRadioButton.UseVisualStyleBackColor = true;
            this.interpolateColorRadioButton.CheckedChanged += new System.EventHandler(this.colorRadioButton_CheckedChanged);
            // 
            // ColorGroupBox
            // 
            this.ColorGroupBox.Controls.Add(this.constColorRadioButton);
            this.ColorGroupBox.Location = new System.Drawing.Point(0, 458);
            this.ColorGroupBox.Name = "ColorGroupBox";
            this.ColorGroupBox.Size = new System.Drawing.Size(234, 74);
            this.ColorGroupBox.TabIndex = 15;
            this.ColorGroupBox.TabStop = false;
            this.ColorGroupBox.Text = "Color";
            // 
            // constColorRadioButton
            // 
            this.constColorRadioButton.AutoSize = true;
            this.constColorRadioButton.Checked = true;
            this.constColorRadioButton.Location = new System.Drawing.Point(21, 26);
            this.constColorRadioButton.Name = "constColorRadioButton";
            this.constColorRadioButton.Size = new System.Drawing.Size(128, 24);
            this.constColorRadioButton.TabIndex = 13;
            this.constColorRadioButton.TabStop = true;
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
            // animateObjectCheckBox
            // 
            this.animateObjectCheckBox.AutoSize = true;
            this.animateObjectCheckBox.Checked = true;
            this.animateObjectCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.animateObjectCheckBox.Location = new System.Drawing.Point(10, 601);
            this.animateObjectCheckBox.Name = "animateObjectCheckBox";
            this.animateObjectCheckBox.Size = new System.Drawing.Size(146, 24);
            this.animateObjectCheckBox.TabIndex = 10;
            this.animateObjectCheckBox.Text = "Animation object";
            this.animateObjectCheckBox.UseVisualStyleBackColor = true;
            this.animateObjectCheckBox.CheckedChanged += new System.EventHandler(this.animationCheckBox_CheckedChanged);
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
            this.zTrackBar.Maximum = 10000;
            this.zTrackBar.Minimum = -4000;
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
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
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
        private RadioButton interpolateColorRadioButton;
        private RadioButton interpolateNormalRadioButton;
        private System.Windows.Forms.Timer animationTimer;
        private Label zLabel;
        private TrackBar zTrackBar;
        private CheckBox animateObjectCheckBox;
        private CheckBox paintTriangulationCheckBox;
        private ColorDialog surfaceColorDialog;
        private RadioButton constColorRadioButton;
        private GroupBox interpolationGroupBox;
        private GroupBox ColorGroupBox;
        private OpenFileDialog openFileDialog1;
        private ColorDialog lightColorDialog;
        private Button changeLightColorButton;
        private TrackBar kaTrackBar;
        private Label kaLabel;
        private CheckBox paintObjectsCheckBox;
        private NumericUpDown xNumericUpDown;
        private NumericUpDown zNumericUpDown;
        private NumericUpDown yNumericUpDown;
        private TrackBar FOVTrackBar;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private CheckBox animateLightCheckBox;
        private ComboBox comboBox1;
        private RadioButton interpolateConstradioButton;
        private CheckBox light3reflectorCheckBox;
        private CheckBox light2CheckBox;
        private CheckBox light1CheckBox;
        private CheckBox oscilationCheckBox;
        private CheckBox fogCheckBox;
        private CheckBox dayAndNightCheckBox;
    }
}