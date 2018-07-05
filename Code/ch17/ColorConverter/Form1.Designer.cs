namespace ColorConverter
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.colorSliderB = new ColorSlider();
            this.colorSliderG = new ColorSlider();
            this.colorSliderR = new ColorSlider();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.colorSliderV = new ColorSlider();
            this.colorSliderS = new ColorSlider();
            this.colorSliderH = new ColorSlider();
            this.labelResult = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.colorSliderB);
            this.groupBox1.Controls.Add(this.colorSliderG);
            this.groupBox1.Controls.Add(this.colorSliderR);
            this.groupBox1.Location = new System.Drawing.Point(12, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 138);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RGB";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "B";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "G";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "R";
            // 
            // colorSliderB
            // 
            this.colorSliderB.EndColor = System.Drawing.Color.Empty;
            this.colorSliderB.Location = new System.Drawing.Point(36, 89);
            this.colorSliderB.Maximum = 255;
            this.colorSliderB.Name = "colorSliderB";
            this.colorSliderB.Size = new System.Drawing.Size(225, 28);
            this.colorSliderB.StartColor = System.Drawing.Color.Empty;
            this.colorSliderB.TabIndex = 2;
            this.colorSliderB.Value = 0;
            this.colorSliderB.ValueChanged += new System.EventHandler<System.EventArgs>(this.OnRGBValuesChanged);
            // 
            // colorSliderG
            // 
            this.colorSliderG.EndColor = System.Drawing.Color.Empty;
            this.colorSliderG.Location = new System.Drawing.Point(36, 55);
            this.colorSliderG.Maximum = 255;
            this.colorSliderG.Name = "colorSliderG";
            this.colorSliderG.Size = new System.Drawing.Size(225, 28);
            this.colorSliderG.StartColor = System.Drawing.Color.Empty;
            this.colorSliderG.TabIndex = 1;
            this.colorSliderG.Value = 0;
            this.colorSliderG.ValueChanged += new System.EventHandler<System.EventArgs>(this.OnRGBValuesChanged);
            // 
            // colorSliderR
            // 
            this.colorSliderR.EndColor = System.Drawing.Color.Empty;
            this.colorSliderR.Location = new System.Drawing.Point(36, 19);
            this.colorSliderR.Maximum = 255;
            this.colorSliderR.Name = "colorSliderR";
            this.colorSliderR.Size = new System.Drawing.Size(225, 28);
            this.colorSliderR.StartColor = System.Drawing.Color.Empty;
            this.colorSliderR.TabIndex = 0;
            this.colorSliderR.Value = 0;
            this.colorSliderR.ValueChanged += new System.EventHandler<System.EventArgs>(this.OnRGBValuesChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.colorSliderV);
            this.groupBox2.Controls.Add(this.colorSliderS);
            this.groupBox2.Controls.Add(this.colorSliderH);
            this.groupBox2.Location = new System.Drawing.Point(12, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 138);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "HSV";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "V";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "S";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "H";
            // 
            // colorSliderV
            // 
            this.colorSliderV.EndColor = System.Drawing.Color.Empty;
            this.colorSliderV.Location = new System.Drawing.Point(36, 89);
            this.colorSliderV.Maximum = 100;
            this.colorSliderV.Name = "colorSliderV";
            this.colorSliderV.Size = new System.Drawing.Size(225, 28);
            this.colorSliderV.StartColor = System.Drawing.Color.Empty;
            this.colorSliderV.TabIndex = 2;
            this.colorSliderV.Value = 0;
            this.colorSliderV.ValueChanged += new System.EventHandler<System.EventArgs>(this.OnHsvValuesChanged);
            // 
            // colorSliderS
            // 
            this.colorSliderS.EndColor = System.Drawing.Color.Empty;
            this.colorSliderS.Location = new System.Drawing.Point(36, 55);
            this.colorSliderS.Maximum = 100;
            this.colorSliderS.Name = "colorSliderS";
            this.colorSliderS.Size = new System.Drawing.Size(225, 28);
            this.colorSliderS.StartColor = System.Drawing.Color.Empty;
            this.colorSliderS.TabIndex = 1;
            this.colorSliderS.Value = 0;
            this.colorSliderS.ValueChanged += new System.EventHandler<System.EventArgs>(this.OnHsvValuesChanged);
            // 
            // colorSliderH
            // 
            this.colorSliderH.EndColor = System.Drawing.Color.Empty;
            this.colorSliderH.Location = new System.Drawing.Point(36, 19);
            this.colorSliderH.Maximum = 360;
            this.colorSliderH.Name = "colorSliderH";
            this.colorSliderH.Size = new System.Drawing.Size(225, 28);
            this.colorSliderH.StartColor = System.Drawing.Color.Empty;
            this.colorSliderH.TabIndex = 0;
            this.colorSliderH.Value = 0;
            this.colorSliderH.ValueChanged += new System.EventHandler<System.EventArgs>(this.OnHsvValuesChanged);
            // 
            // labelResult
            // 
            this.labelResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelResult.Location = new System.Drawing.Point(298, 111);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(100, 100);
            this.labelResult.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 323);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "RGB to HSV";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private ColorSlider colorSliderR;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ColorSlider colorSliderB;
        private ColorSlider colorSliderG;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private ColorSlider colorSliderV;
        private ColorSlider colorSliderS;
        private ColorSlider colorSliderH;
        private System.Windows.Forms.Label labelResult;
    }
}

