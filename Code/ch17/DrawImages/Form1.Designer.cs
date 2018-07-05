namespace DrawImages
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.drawPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonInterpolationDefault = new System.Windows.Forms.RadioButton();
            this.radioButtonInterpolationLow = new System.Windows.Forms.RadioButton();
            this.radioButtonInterpolationHigh = new System.Windows.Forms.RadioButton();
            this.radioButtonInterpolationBilinear = new System.Windows.Forms.RadioButton();
            this.radioButtonInterpolationBicubic = new System.Windows.Forms.RadioButton();
            this.radioButtonInterpolationNeighbor = new System.Windows.Forms.RadioButton();
            this.radioButtonInterpolationHighQualityBilinear = new System.Windows.Forms.RadioButton();
            this.radioButtonInterpolationHighQualityBicubic = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(153, 612);
            this.panel1.TabIndex = 0;
            // 
            // drawPanel
            // 
            this.drawPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawPanel.Location = new System.Drawing.Point(153, 0);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(797, 612);
            this.drawPanel.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioButtonInterpolationHighQualityBicubic);
            this.groupBox1.Controls.Add(this.radioButtonInterpolationHighQualityBilinear);
            this.groupBox1.Controls.Add(this.radioButtonInterpolationNeighbor);
            this.groupBox1.Controls.Add(this.radioButtonInterpolationBicubic);
            this.groupBox1.Controls.Add(this.radioButtonInterpolationBilinear);
            this.groupBox1.Controls.Add(this.radioButtonInterpolationHigh);
            this.groupBox1.Controls.Add(this.radioButtonInterpolationLow);
            this.groupBox1.Controls.Add(this.radioButtonInterpolationDefault);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(135, 215);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Interpolation Mode";
            // 
            // radioButtonInterpolationDefault
            // 
            this.radioButtonInterpolationDefault.AutoSize = true;
            this.radioButtonInterpolationDefault.Checked = true;
            this.radioButtonInterpolationDefault.Location = new System.Drawing.Point(6, 19);
            this.radioButtonInterpolationDefault.Name = "radioButtonInterpolationDefault";
            this.radioButtonInterpolationDefault.Size = new System.Drawing.Size(59, 17);
            this.radioButtonInterpolationDefault.TabIndex = 0;
            this.radioButtonInterpolationDefault.TabStop = true;
            this.radioButtonInterpolationDefault.Text = "Default";
            this.radioButtonInterpolationDefault.UseVisualStyleBackColor = true;
            this.radioButtonInterpolationDefault.CheckedChanged += new System.EventHandler(this.OnInterpolationChanged);
            // 
            // radioButtonInterpolationLow
            // 
            this.radioButtonInterpolationLow.AutoSize = true;
            this.radioButtonInterpolationLow.Location = new System.Drawing.Point(6, 42);
            this.radioButtonInterpolationLow.Name = "radioButtonInterpolationLow";
            this.radioButtonInterpolationLow.Size = new System.Drawing.Size(45, 17);
            this.radioButtonInterpolationLow.TabIndex = 1;
            this.radioButtonInterpolationLow.Text = "Low";
            this.radioButtonInterpolationLow.UseVisualStyleBackColor = true;
            this.radioButtonInterpolationLow.CheckedChanged += new System.EventHandler(this.OnInterpolationChanged);
            // 
            // radioButtonInterpolationHigh
            // 
            this.radioButtonInterpolationHigh.AutoSize = true;
            this.radioButtonInterpolationHigh.Location = new System.Drawing.Point(6, 65);
            this.radioButtonInterpolationHigh.Name = "radioButtonInterpolationHigh";
            this.radioButtonInterpolationHigh.Size = new System.Drawing.Size(47, 17);
            this.radioButtonInterpolationHigh.TabIndex = 2;
            this.radioButtonInterpolationHigh.Text = "High";
            this.radioButtonInterpolationHigh.UseVisualStyleBackColor = true;
            this.radioButtonInterpolationHigh.CheckedChanged += new System.EventHandler(this.OnInterpolationChanged);
            // 
            // radioButtonInterpolationBilinear
            // 
            this.radioButtonInterpolationBilinear.AutoSize = true;
            this.radioButtonInterpolationBilinear.Location = new System.Drawing.Point(7, 89);
            this.radioButtonInterpolationBilinear.Name = "radioButtonInterpolationBilinear";
            this.radioButtonInterpolationBilinear.Size = new System.Drawing.Size(59, 17);
            this.radioButtonInterpolationBilinear.TabIndex = 3;
            this.radioButtonInterpolationBilinear.Text = "Bilinear";
            this.radioButtonInterpolationBilinear.UseVisualStyleBackColor = true;
            this.radioButtonInterpolationBilinear.CheckedChanged += new System.EventHandler(this.OnInterpolationChanged);
            // 
            // radioButtonInterpolationBicubic
            // 
            this.radioButtonInterpolationBicubic.AutoSize = true;
            this.radioButtonInterpolationBicubic.Location = new System.Drawing.Point(7, 113);
            this.radioButtonInterpolationBicubic.Name = "radioButtonInterpolationBicubic";
            this.radioButtonInterpolationBicubic.Size = new System.Drawing.Size(60, 17);
            this.radioButtonInterpolationBicubic.TabIndex = 4;
            this.radioButtonInterpolationBicubic.Text = "Bicubic";
            this.radioButtonInterpolationBicubic.UseVisualStyleBackColor = true;
            this.radioButtonInterpolationBicubic.CheckedChanged += new System.EventHandler(this.OnInterpolationChanged);
            // 
            // radioButtonInterpolationNeighbor
            // 
            this.radioButtonInterpolationNeighbor.AutoSize = true;
            this.radioButtonInterpolationNeighbor.Location = new System.Drawing.Point(7, 137);
            this.radioButtonInterpolationNeighbor.Name = "radioButtonInterpolationNeighbor";
            this.radioButtonInterpolationNeighbor.Size = new System.Drawing.Size(108, 17);
            this.radioButtonInterpolationNeighbor.TabIndex = 5;
            this.radioButtonInterpolationNeighbor.Text = "Nearest Neighbor";
            this.radioButtonInterpolationNeighbor.UseVisualStyleBackColor = true;
            this.radioButtonInterpolationNeighbor.CheckedChanged += new System.EventHandler(this.OnInterpolationChanged);
            // 
            // radioButtonInterpolationHighQualityBilinear
            // 
            this.radioButtonInterpolationHighQualityBilinear.AutoSize = true;
            this.radioButtonInterpolationHighQualityBilinear.Location = new System.Drawing.Point(7, 161);
            this.radioButtonInterpolationHighQualityBilinear.Name = "radioButtonInterpolationHighQualityBilinear";
            this.radioButtonInterpolationHighQualityBilinear.Size = new System.Drawing.Size(119, 17);
            this.radioButtonInterpolationHighQualityBilinear.TabIndex = 6;
            this.radioButtonInterpolationHighQualityBilinear.Text = "High Quality Bilinear";
            this.radioButtonInterpolationHighQualityBilinear.UseVisualStyleBackColor = true;
            this.radioButtonInterpolationHighQualityBilinear.CheckedChanged += new System.EventHandler(this.OnInterpolationChanged);
            // 
            // radioButtonInterpolationHighQualityBicubic
            // 
            this.radioButtonInterpolationHighQualityBicubic.AutoSize = true;
            this.radioButtonInterpolationHighQualityBicubic.Location = new System.Drawing.Point(7, 185);
            this.radioButtonInterpolationHighQualityBicubic.Name = "radioButtonInterpolationHighQualityBicubic";
            this.radioButtonInterpolationHighQualityBicubic.Size = new System.Drawing.Size(120, 17);
            this.radioButtonInterpolationHighQualityBicubic.TabIndex = 7;
            this.radioButtonInterpolationHighQualityBicubic.Text = "High Quality Bicubic";
            this.radioButtonInterpolationHighQualityBicubic.UseVisualStyleBackColor = true;
            this.radioButtonInterpolationHighQualityBicubic.CheckedChanged += new System.EventHandler(this.OnInterpolationChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 612);
            this.Controls.Add(this.drawPanel);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Draw Images";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel drawPanel;
        private System.Windows.Forms.RadioButton radioButtonInterpolationBicubic;
        private System.Windows.Forms.RadioButton radioButtonInterpolationBilinear;
        private System.Windows.Forms.RadioButton radioButtonInterpolationHigh;
        private System.Windows.Forms.RadioButton radioButtonInterpolationLow;
        private System.Windows.Forms.RadioButton radioButtonInterpolationDefault;
        private System.Windows.Forms.RadioButton radioButtonInterpolationHighQualityBicubic;
        private System.Windows.Forms.RadioButton radioButtonInterpolationHighQualityBilinear;
        private System.Windows.Forms.RadioButton radioButtonInterpolationNeighbor;
    }
}

