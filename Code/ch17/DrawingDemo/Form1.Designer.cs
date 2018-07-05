namespace DrawingDemo
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelOptions = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonSmoothingNone = new System.Windows.Forms.RadioButton();
            this.radioButtonSmoothingDefault = new System.Windows.Forms.RadioButton();
            this.radioButtonSmoothingAntiAlias = new System.Windows.Forms.RadioButton();
            this.radioButtonSmoothingHighSpeed = new System.Windows.Forms.RadioButton();
            this.radioButtonSmoothingHighQuality = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonTextSmoothingDefault = new System.Windows.Forms.RadioButton();
            this.radioButtonTextSmoothingAntiAlias = new System.Windows.Forms.RadioButton();
            this.radioButtonTextSmoothingAntiAliasGridFit = new System.Windows.Forms.RadioButton();
            this.radioButtonTextSmoothingClearTypeGridFit = new System.Windows.Forms.RadioButton();
            this.radioButtonTextSmoothingSingleBitPerPixel = new System.Windows.Forms.RadioButton();
            this.radioButtonTextSmoothingSingleBitPerPixelGridFit = new System.Windows.Forms.RadioButton();
            this.drawingPanel = new DrawingDemo.DrawingPanel();
            this.panelOptions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelOptions
            // 
            this.panelOptions.BackColor = System.Drawing.SystemColors.Control;
            this.panelOptions.Controls.Add(this.groupBox2);
            this.panelOptions.Controls.Add(this.label1);
            this.panelOptions.Controls.Add(this.numericUpDown1);
            this.panelOptions.Controls.Add(this.button1);
            this.panelOptions.Controls.Add(this.groupBox1);
            this.panelOptions.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelOptions.Location = new System.Drawing.Point(0, 0);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.Size = new System.Drawing.Size(184, 459);
            this.panelOptions.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonSmoothingHighQuality);
            this.groupBox1.Controls.Add(this.radioButtonSmoothingHighSpeed);
            this.groupBox1.Controls.Add(this.radioButtonSmoothingAntiAlias);
            this.groupBox1.Controls.Add(this.radioButtonSmoothingDefault);
            this.groupBox1.Controls.Add(this.radioButtonSmoothingNone);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(96, 136);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Smoothing";
            // 
            // radioButtonSmoothingNone
            // 
            this.radioButtonSmoothingNone.AutoSize = true;
            this.radioButtonSmoothingNone.Checked = true;
            this.radioButtonSmoothingNone.Location = new System.Drawing.Point(7, 20);
            this.radioButtonSmoothingNone.Name = "radioButtonSmoothingNone";
            this.radioButtonSmoothingNone.Size = new System.Drawing.Size(51, 17);
            this.radioButtonSmoothingNone.TabIndex = 0;
            this.radioButtonSmoothingNone.TabStop = true;
            this.radioButtonSmoothingNone.Text = "None";
            this.radioButtonSmoothingNone.UseVisualStyleBackColor = true;
            this.radioButtonSmoothingNone.CheckedChanged += new System.EventHandler(this.OnSmoothingChanged);
            // 
            // radioButtonSmoothingDefault
            // 
            this.radioButtonSmoothingDefault.AutoSize = true;
            this.radioButtonSmoothingDefault.Location = new System.Drawing.Point(7, 43);
            this.radioButtonSmoothingDefault.Name = "radioButtonSmoothingDefault";
            this.radioButtonSmoothingDefault.Size = new System.Drawing.Size(59, 17);
            this.radioButtonSmoothingDefault.TabIndex = 1;
            this.radioButtonSmoothingDefault.Text = "Default";
            this.radioButtonSmoothingDefault.UseVisualStyleBackColor = true;
            this.radioButtonSmoothingDefault.CheckedChanged += new System.EventHandler(this.OnSmoothingChanged);
            // 
            // radioButtonSmoothingAntiAlias
            // 
            this.radioButtonSmoothingAntiAlias.AutoSize = true;
            this.radioButtonSmoothingAntiAlias.Location = new System.Drawing.Point(7, 66);
            this.radioButtonSmoothingAntiAlias.Name = "radioButtonSmoothingAntiAlias";
            this.radioButtonSmoothingAntiAlias.Size = new System.Drawing.Size(67, 17);
            this.radioButtonSmoothingAntiAlias.TabIndex = 3;
            this.radioButtonSmoothingAntiAlias.Text = "Anti-alias";
            this.radioButtonSmoothingAntiAlias.UseVisualStyleBackColor = true;
            this.radioButtonSmoothingAntiAlias.CheckedChanged += new System.EventHandler(this.OnSmoothingChanged);
            // 
            // radioButtonSmoothingHighSpeed
            // 
            this.radioButtonSmoothingHighSpeed.AutoSize = true;
            this.radioButtonSmoothingHighSpeed.Location = new System.Drawing.Point(7, 89);
            this.radioButtonSmoothingHighSpeed.Name = "radioButtonSmoothingHighSpeed";
            this.radioButtonSmoothingHighSpeed.Size = new System.Drawing.Size(81, 17);
            this.radioButtonSmoothingHighSpeed.TabIndex = 4;
            this.radioButtonSmoothingHighSpeed.Text = "High Speed";
            this.radioButtonSmoothingHighSpeed.UseVisualStyleBackColor = true;
            this.radioButtonSmoothingHighSpeed.CheckedChanged += new System.EventHandler(this.OnSmoothingChanged);
            // 
            // radioButtonSmoothingHighQuality
            // 
            this.radioButtonSmoothingHighQuality.AutoSize = true;
            this.radioButtonSmoothingHighQuality.Location = new System.Drawing.Point(7, 112);
            this.radioButtonSmoothingHighQuality.Name = "radioButtonSmoothingHighQuality";
            this.radioButtonSmoothingHighQuality.Size = new System.Drawing.Size(82, 17);
            this.radioButtonSmoothingHighQuality.TabIndex = 5;
            this.radioButtonSmoothingHighQuality.Text = "High Quality";
            this.radioButtonSmoothingHighQuality.UseVisualStyleBackColor = true;
            this.radioButtonSmoothingHighQuality.CheckedChanged += new System.EventHandler(this.OnSmoothingChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 154);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Font...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(12, 200);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(101, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Text Angle:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonTextSmoothingSingleBitPerPixelGridFit);
            this.groupBox2.Controls.Add(this.radioButtonTextSmoothingSingleBitPerPixel);
            this.groupBox2.Controls.Add(this.radioButtonTextSmoothingClearTypeGridFit);
            this.groupBox2.Controls.Add(this.radioButtonTextSmoothingAntiAliasGridFit);
            this.groupBox2.Controls.Add(this.radioButtonTextSmoothingAntiAlias);
            this.groupBox2.Controls.Add(this.radioButtonTextSmoothingDefault);
            this.groupBox2.Location = new System.Drawing.Point(15, 226);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(163, 159);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Text smoothing";
            // 
            // radioButtonTextSmoothingDefault
            // 
            this.radioButtonTextSmoothingDefault.AutoSize = true;
            this.radioButtonTextSmoothingDefault.Checked = true;
            this.radioButtonTextSmoothingDefault.Location = new System.Drawing.Point(7, 20);
            this.radioButtonTextSmoothingDefault.Name = "radioButtonTextSmoothingDefault";
            this.radioButtonTextSmoothingDefault.Size = new System.Drawing.Size(96, 17);
            this.radioButtonTextSmoothingDefault.TabIndex = 0;
            this.radioButtonTextSmoothingDefault.TabStop = true;
            this.radioButtonTextSmoothingDefault.Text = "System Default";
            this.radioButtonTextSmoothingDefault.UseVisualStyleBackColor = true;
            this.radioButtonTextSmoothingDefault.CheckedChanged += new System.EventHandler(this.OnTextRenderingChanged);
            // 
            // radioButtonTextSmoothingAntiAlias
            // 
            this.radioButtonTextSmoothingAntiAlias.AutoSize = true;
            this.radioButtonTextSmoothingAntiAlias.Location = new System.Drawing.Point(7, 43);
            this.radioButtonTextSmoothingAntiAlias.Name = "radioButtonTextSmoothingAntiAlias";
            this.radioButtonTextSmoothingAntiAlias.Size = new System.Drawing.Size(67, 17);
            this.radioButtonTextSmoothingAntiAlias.TabIndex = 1;
            this.radioButtonTextSmoothingAntiAlias.Text = "Anti-alias";
            this.radioButtonTextSmoothingAntiAlias.UseVisualStyleBackColor = true;
            this.radioButtonTextSmoothingAntiAlias.CheckedChanged += new System.EventHandler(this.OnTextRenderingChanged);
            // 
            // radioButtonTextSmoothingAntiAliasGridFit
            // 
            this.radioButtonTextSmoothingAntiAliasGridFit.AutoSize = true;
            this.radioButtonTextSmoothingAntiAliasGridFit.Location = new System.Drawing.Point(7, 66);
            this.radioButtonTextSmoothingAntiAliasGridFit.Name = "radioButtonTextSmoothingAntiAliasGridFit";
            this.radioButtonTextSmoothingAntiAliasGridFit.Size = new System.Drawing.Size(103, 17);
            this.radioButtonTextSmoothingAntiAliasGridFit.TabIndex = 2;
            this.radioButtonTextSmoothingAntiAliasGridFit.Text = "Anti-alias Grid Fit";
            this.radioButtonTextSmoothingAntiAliasGridFit.UseVisualStyleBackColor = true;
            this.radioButtonTextSmoothingAntiAliasGridFit.CheckedChanged += new System.EventHandler(this.OnTextRenderingChanged);
            // 
            // radioButtonTextSmoothingClearTypeGridFit
            // 
            this.radioButtonTextSmoothingClearTypeGridFit.AutoSize = true;
            this.radioButtonTextSmoothingClearTypeGridFit.Location = new System.Drawing.Point(7, 89);
            this.radioButtonTextSmoothingClearTypeGridFit.Name = "radioButtonTextSmoothingClearTypeGridFit";
            this.radioButtonTextSmoothingClearTypeGridFit.Size = new System.Drawing.Size(109, 17);
            this.radioButtonTextSmoothingClearTypeGridFit.TabIndex = 3;
            this.radioButtonTextSmoothingClearTypeGridFit.Text = "ClearType Grid Fit";
            this.radioButtonTextSmoothingClearTypeGridFit.UseVisualStyleBackColor = true;
            this.radioButtonTextSmoothingClearTypeGridFit.CheckedChanged += new System.EventHandler(this.OnTextRenderingChanged);
            // 
            // radioButtonTextSmoothingSingleBitPerPixel
            // 
            this.radioButtonTextSmoothingSingleBitPerPixel.AutoSize = true;
            this.radioButtonTextSmoothingSingleBitPerPixel.Location = new System.Drawing.Point(7, 112);
            this.radioButtonTextSmoothingSingleBitPerPixel.Name = "radioButtonTextSmoothingSingleBitPerPixel";
            this.radioButtonTextSmoothingSingleBitPerPixel.Size = new System.Drawing.Size(110, 17);
            this.radioButtonTextSmoothingSingleBitPerPixel.TabIndex = 4;
            this.radioButtonTextSmoothingSingleBitPerPixel.Text = "Single-bit per pixel";
            this.radioButtonTextSmoothingSingleBitPerPixel.UseVisualStyleBackColor = true;
            this.radioButtonTextSmoothingSingleBitPerPixel.CheckedChanged += new System.EventHandler(this.OnTextRenderingChanged);
            // 
            // radioButtonTextSmoothingSingleBitPerPixelGridFit
            // 
            this.radioButtonTextSmoothingSingleBitPerPixelGridFit.AutoSize = true;
            this.radioButtonTextSmoothingSingleBitPerPixelGridFit.Location = new System.Drawing.Point(7, 135);
            this.radioButtonTextSmoothingSingleBitPerPixelGridFit.Name = "radioButtonTextSmoothingSingleBitPerPixelGridFit";
            this.radioButtonTextSmoothingSingleBitPerPixelGridFit.Size = new System.Drawing.Size(143, 17);
            this.radioButtonTextSmoothingSingleBitPerPixelGridFit.TabIndex = 5;
            this.radioButtonTextSmoothingSingleBitPerPixelGridFit.Text = "Single-bit per pixel Grid fit";
            this.radioButtonTextSmoothingSingleBitPerPixelGridFit.UseVisualStyleBackColor = true;
            this.radioButtonTextSmoothingSingleBitPerPixelGridFit.CheckedChanged += new System.EventHandler(this.OnTextRenderingChanged);
            // 
            // drawingPanel
            // 
            this.drawingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawingPanel.Location = new System.Drawing.Point(184, 0);
            this.drawingPanel.Name = "drawingPanel";
            this.drawingPanel.Size = new System.Drawing.Size(431, 459);
            this.drawingPanel.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.drawingPanel.TabIndex = 1;
            this.drawingPanel.TextAngle = 0;
            this.drawingPanel.TextFont = new System.Drawing.Font("Verdana", 18F);
            this.drawingPanel.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(615, 459);
            this.Controls.Add(this.drawingPanel);
            this.Controls.Add(this.panelOptions);
            this.Name = "Form1";
            this.Text = "Drawing Demo";
            this.panelOptions.ResumeLayout(false);
            this.panelOptions.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelOptions;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonSmoothingDefault;
        private System.Windows.Forms.RadioButton radioButtonSmoothingNone;
        private DrawingPanel drawingPanel;
        private System.Windows.Forms.RadioButton radioButtonSmoothingHighSpeed;
        private System.Windows.Forms.RadioButton radioButtonSmoothingAntiAlias;
        private System.Windows.Forms.RadioButton radioButtonSmoothingHighQuality;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonTextSmoothingDefault;
        private System.Windows.Forms.RadioButton radioButtonTextSmoothingSingleBitPerPixel;
        private System.Windows.Forms.RadioButton radioButtonTextSmoothingClearTypeGridFit;
        private System.Windows.Forms.RadioButton radioButtonTextSmoothingAntiAliasGridFit;
        private System.Windows.Forms.RadioButton radioButtonTextSmoothingAntiAlias;
        private System.Windows.Forms.RadioButton radioButtonTextSmoothingSingleBitPerPixelGridFit;
    }
}

