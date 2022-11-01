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
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.kdLabel = new System.Windows.Forms.Label();
            this.ksLabel = new System.Windows.Forms.Label();
            this.mLabel = new System.Windows.Forms.Label();
            this.mTrackBar = new System.Windows.Forms.TrackBar();
            this.ksTrackBar = new System.Windows.Forms.TrackBar();
            this.kdTrackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.groupBox1.Text = "groupBox1";
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
    }
}