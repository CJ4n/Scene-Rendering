namespace Filling_Triangular_Mesh
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
            this.changeLightColorButton = new System.Windows.Forms.Button();
            this.selectObjButton = new System.Windows.Forms.Button();
            this.interpolationGroupBox = new System.Windows.Forms.GroupBox();
            this.normalRadioButton = new System.Windows.Forms.RadioButton();
            this.colorRadioButton = new System.Windows.Forms.RadioButton();
            this.ColorGroupBox = new System.Windows.Forms.GroupBox();
            this.textureColorRadioButton = new System.Windows.Forms.RadioButton();
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
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.surfaceColorDialog = new System.Windows.Forms.ColorDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lightColorDialog = new System.Windows.Forms.ColorDialog();
            this.modifyNormalVectorsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.interpolationGroupBox.SuspendLayout();
            this.ColorGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ksTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(1172, 951);
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
            this.splitContainer1.Size = new System.Drawing.Size(1414, 951);
            this.splitContainer1.SplitterDistance = 1172;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.modifyNormalVectorsButton);
            this.groupBox1.Controls.Add(this.changeLightColorButton);
            this.groupBox1.Controls.Add(this.selectObjButton);
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
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 951);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameter adjustment";
            // 
            // changeLightColorButton
            // 
            this.changeLightColorButton.Location = new System.Drawing.Point(10, 712);
            this.changeLightColorButton.Name = "changeLightColorButton";
            this.changeLightColorButton.Size = new System.Drawing.Size(200, 29);
            this.changeLightColorButton.TabIndex = 17;
            this.changeLightColorButton.Text = "Change Light Color";
            this.changeLightColorButton.UseVisualStyleBackColor = true;
            this.changeLightColorButton.Click += new System.EventHandler(this.changeLightColorButton_Click);
            // 
            // selectObjButton
            // 
            this.selectObjButton.Location = new System.Drawing.Point(10, 654);
            this.selectObjButton.Name = "selectObjButton";
            this.selectObjButton.Size = new System.Drawing.Size(200, 29);
            this.selectObjButton.TabIndex = 16;
            this.selectObjButton.Text = "Select OBJ FIle";
            this.selectObjButton.UseVisualStyleBackColor = true;
            this.selectObjButton.Click += new System.EventHandler(this.selectObjButton_Click);
            // 
            // interpolationGroupBox
            // 
            this.interpolationGroupBox.Controls.Add(this.normalRadioButton);
            this.interpolationGroupBox.Controls.Add(this.colorRadioButton);
            this.interpolationGroupBox.Location = new System.Drawing.Point(3, 333);
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
            this.normalRadioButton.Size = new System.Drawing.Size(205, 24);
            this.normalRadioButton.TabIndex = 6;
            this.normalRadioButton.TabStop = true;
            this.normalRadioButton.Text = "Interpolate normal vectors";
            this.normalRadioButton.UseVisualStyleBackColor = true;
            this.normalRadioButton.CheckedChanged += new System.EventHandler(this.normalRadioButton_CheckedChanged);
            // 
            // colorRadioButton
            // 
            this.colorRadioButton.AutoSize = true;
            this.colorRadioButton.Location = new System.Drawing.Point(18, 56);
            this.colorRadioButton.Name = "colorRadioButton";
            this.colorRadioButton.Size = new System.Drawing.Size(141, 24);
            this.colorRadioButton.TabIndex = 7;
            this.colorRadioButton.Text = "Interpolate color";
            this.colorRadioButton.UseVisualStyleBackColor = true;
            this.colorRadioButton.CheckedChanged += new System.EventHandler(this.colorRadioButton_CheckedChanged);
            // 
            // ColorGroupBox
            // 
            this.ColorGroupBox.Controls.Add(this.textureColorRadioButton);
            this.ColorGroupBox.Controls.Add(this.constColorRadioButton);
            this.ColorGroupBox.Location = new System.Drawing.Point(2, 452);
            this.ColorGroupBox.Name = "ColorGroupBox";
            this.ColorGroupBox.Size = new System.Drawing.Size(234, 102);
            this.ColorGroupBox.TabIndex = 15;
            this.ColorGroupBox.TabStop = false;
            this.ColorGroupBox.Text = "Color";
            // 
            // textureColorRadioButton
            // 
            this.textureColorRadioButton.AutoSize = true;
            this.textureColorRadioButton.Location = new System.Drawing.Point(21, 26);
            this.textureColorRadioButton.Name = "textureColorRadioButton";
            this.textureColorRadioButton.Size = new System.Drawing.Size(154, 24);
            this.textureColorRadioButton.TabIndex = 14;
            this.textureColorRadioButton.Text = "Color from Texture";
            this.textureColorRadioButton.UseVisualStyleBackColor = true;
            this.textureColorRadioButton.CheckedChanged += new System.EventHandler(this.textureColorRadioButton_CheckedChanged);
            // 
            // constColorRadioButton
            // 
            this.constColorRadioButton.AutoSize = true;
            this.constColorRadioButton.Checked = true;
            this.constColorRadioButton.Location = new System.Drawing.Point(21, 56);
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
            this.paintTriangulationCheckBox.Size = new System.Drawing.Size(152, 24);
            this.paintTriangulationCheckBox.TabIndex = 11;
            this.paintTriangulationCheckBox.Text = "Paint triangulation";
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
            this.zLabel.Location = new System.Drawing.Point(0, 264);
            this.zLabel.Name = "zLabel";
            this.zLabel.Size = new System.Drawing.Size(19, 20);
            this.zLabel.TabIndex = 9;
            this.zLabel.Text = "z:";
            // 
            // zTrackBar
            // 
            this.zTrackBar.Location = new System.Drawing.Point(1, 287);
            this.zTrackBar.Maximum = 2500;
            this.zTrackBar.Name = "zTrackBar";
            this.zTrackBar.Size = new System.Drawing.Size(234, 56);
            this.zTrackBar.TabIndex = 8;
            this.zTrackBar.Value = 700;
            this.zTrackBar.ValueChanged += new System.EventHandler(this.zTrackBar_ValueChanged);
            // 
            // kdLabel
            // 
            this.kdLabel.AutoSize = true;
            this.kdLabel.Location = new System.Drawing.Point(-2, 52);
            this.kdLabel.Name = "kdLabel";
            this.kdLabel.Size = new System.Drawing.Size(28, 20);
            this.kdLabel.TabIndex = 5;
            this.kdLabel.Text = "kd:";
            // 
            // ksLabel
            // 
            this.ksLabel.AutoSize = true;
            this.ksLabel.Location = new System.Drawing.Point(0, 114);
            this.ksLabel.Name = "ksLabel";
            this.ksLabel.Size = new System.Drawing.Size(25, 20);
            this.ksLabel.TabIndex = 4;
            this.ksLabel.Text = "ks:";
            // 
            // mLabel
            // 
            this.mLabel.AutoSize = true;
            this.mLabel.Location = new System.Drawing.Point(-2, 191);
            this.mLabel.Name = "mLabel";
            this.mLabel.Size = new System.Drawing.Size(25, 20);
            this.mLabel.TabIndex = 3;
            this.mLabel.Text = "m:";
            // 
            // mTrackBar
            // 
            this.mTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mTrackBar.Location = new System.Drawing.Point(0, 228);
            this.mTrackBar.Maximum = 100;
            this.mTrackBar.Minimum = 1;
            this.mTrackBar.Name = "mTrackBar";
            this.mTrackBar.Size = new System.Drawing.Size(232, 56);
            this.mTrackBar.TabIndex = 2;
            this.mTrackBar.TickFrequency = 10;
            this.mTrackBar.Value = 50;
            this.mTrackBar.ValueChanged += new System.EventHandler(this.mTrackBar_ValueChanged);
            // 
            // ksTrackBar
            // 
            this.ksTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ksTrackBar.Location = new System.Drawing.Point(0, 155);
            this.ksTrackBar.Maximum = 100;
            this.ksTrackBar.Name = "ksTrackBar";
            this.ksTrackBar.Size = new System.Drawing.Size(232, 56);
            this.ksTrackBar.TabIndex = 1;
            this.ksTrackBar.TickFrequency = 10;
            this.ksTrackBar.Value = 50;
            this.ksTrackBar.ValueChanged += new System.EventHandler(this.ksTrackBar_ValueChanged);
            // 
            // kdTrackBar
            // 
            this.kdTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kdTrackBar.Location = new System.Drawing.Point(-2, 75);
            this.kdTrackBar.Maximum = 100;
            this.kdTrackBar.Name = "kdTrackBar";
            this.kdTrackBar.Size = new System.Drawing.Size(228, 56);
            this.kdTrackBar.TabIndex = 0;
            this.kdTrackBar.TickFrequency = 10;
            this.kdTrackBar.Value = 50;
            this.kdTrackBar.ValueChanged += new System.EventHandler(this.kdTrackBar_ValueChanged);
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
            // modifyNormalVectorsButton
            // 
            this.modifyNormalVectorsButton.Location = new System.Drawing.Point(10, 765);
            this.modifyNormalVectorsButton.Name = "modifyNormalVectorsButton";
            this.modifyNormalVectorsButton.Size = new System.Drawing.Size(200, 29);
            this.modifyNormalVectorsButton.TabIndex = 18;
            this.modifyNormalVectorsButton.Text = "Modify Normal Vectors";
            this.modifyNormalVectorsButton.UseVisualStyleBackColor = true;
            this.modifyNormalVectorsButton.Click += new System.EventHandler(this.modifyNormalVectorsButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1414, 951);
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
            this.interpolationGroupBox.ResumeLayout(false);
            this.interpolationGroupBox.PerformLayout();
            this.ColorGroupBox.ResumeLayout(false);
            this.ColorGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ksTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdTrackBar)).EndInit();
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
        private RadioButton textureColorRadioButton;
        private RadioButton constColorRadioButton;
        private GroupBox interpolationGroupBox;
        private GroupBox ColorGroupBox;
        private Button selectObjButton;
        private OpenFileDialog openFileDialog1;
        private ColorDialog lightColorDialog;
        private Button changeLightColorButton;
        private Button modifyNormalVectorsButton;
    }
}