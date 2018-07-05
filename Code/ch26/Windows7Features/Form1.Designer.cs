namespace Windows7Features
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            this.buttonStdOFD = new System.Windows.Forms.Button();
            this.buttonWin7OFD = new System.Windows.Forms.Button();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.checkBoxBatteryPresent = new System.Windows.Forms.CheckBox();
            this.checkBoxMonitorOn = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelPowerPersonality = new System.Windows.Forms.Label();
            this.checkBoxUpsPresent = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelPowerSource = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonStdOFD
            // 
            this.buttonStdOFD.Location = new System.Drawing.Point(13, 13);
            this.buttonStdOFD.Name = "buttonStdOFD";
            this.buttonStdOFD.Size = new System.Drawing.Size(189, 23);
            this.buttonStdOFD.TabIndex = 0;
            this.buttonStdOFD.Text = "Standard Open File Dialog...";
            this.buttonStdOFD.UseVisualStyleBackColor = true;
            this.buttonStdOFD.Click += new System.EventHandler(this.buttonStdOFD_Click);
            // 
            // buttonWin7OFD
            // 
            this.buttonWin7OFD.Location = new System.Drawing.Point(13, 43);
            this.buttonWin7OFD.Name = "buttonWin7OFD";
            this.buttonWin7OFD.Size = new System.Drawing.Size(189, 23);
            this.buttonWin7OFD.TabIndex = 1;
            this.buttonWin7OFD.Text = "Win7 Open File Dialog...";
            this.buttonWin7OFD.UseVisualStyleBackColor = true;
            this.buttonWin7OFD.Click += new System.EventHandler(this.buttonWin7OFD_Click);
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Location = new System.Drawing.Point(260, 12);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.Size = new System.Drawing.Size(303, 222);
            this.textBoxInfo.TabIndex = 2;
            // 
            // checkBoxBatteryPresent
            // 
            this.checkBoxBatteryPresent.AutoSize = true;
            this.checkBoxBatteryPresent.Enabled = false;
            this.checkBoxBatteryPresent.Location = new System.Drawing.Point(25, 114);
            this.checkBoxBatteryPresent.Name = "checkBoxBatteryPresent";
            this.checkBoxBatteryPresent.Size = new System.Drawing.Size(97, 17);
            this.checkBoxBatteryPresent.TabIndex = 3;
            this.checkBoxBatteryPresent.Text = "Battery present";
            this.checkBoxBatteryPresent.UseVisualStyleBackColor = true;
            // 
            // checkBoxMonitorOn
            // 
            this.checkBoxMonitorOn.AutoSize = true;
            this.checkBoxMonitorOn.Enabled = false;
            this.checkBoxMonitorOn.Location = new System.Drawing.Point(25, 138);
            this.checkBoxMonitorOn.Name = "checkBoxMonitorOn";
            this.checkBoxMonitorOn.Size = new System.Drawing.Size(76, 17);
            this.checkBoxMonitorOn.TabIndex = 4;
            this.checkBoxMonitorOn.Text = "Monitor on";
            this.checkBoxMonitorOn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Power Personality:";
            // 
            // labelPowerPersonality
            // 
            this.labelPowerPersonality.AutoSize = true;
            this.labelPowerPersonality.Location = new System.Drawing.Point(126, 94);
            this.labelPowerPersonality.Name = "labelPowerPersonality";
            this.labelPowerPersonality.Size = new System.Drawing.Size(33, 13);
            this.labelPowerPersonality.TabIndex = 6;
            this.labelPowerPersonality.Text = "[N/A]";
            // 
            // checkBoxUpsPresent
            // 
            this.checkBoxUpsPresent.AutoSize = true;
            this.checkBoxUpsPresent.Location = new System.Drawing.Point(25, 162);
            this.checkBoxUpsPresent.Name = "checkBoxUpsPresent";
            this.checkBoxUpsPresent.Size = new System.Drawing.Size(86, 17);
            this.checkBoxUpsPresent.TabIndex = 7;
            this.checkBoxUpsPresent.Text = "UPS present";
            this.checkBoxUpsPresent.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Power source:";
            // 
            // labelPowerSource
            // 
            this.labelPowerSource.AutoSize = true;
            this.labelPowerSource.Location = new System.Drawing.Point(129, 186);
            this.labelPowerSource.Name = "labelPowerSource";
            this.labelPowerSource.Size = new System.Drawing.Size(33, 13);
            this.labelPowerSource.TabIndex = 9;
            this.labelPowerSource.Text = "[N/A]";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 246);
            this.Controls.Add(this.labelPowerSource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxUpsPresent);
            this.Controls.Add(this.labelPowerPersonality);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxMonitorOn);
            this.Controls.Add(this.checkBoxBatteryPresent);
            this.Controls.Add(this.textBoxInfo);
            this.Controls.Add(this.buttonWin7OFD);
            this.Controls.Add(this.buttonStdOFD);
            this.Name = "Form1";
            this.Text = "Windows 7 Features Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStdOFD;
        private System.Windows.Forms.Button buttonWin7OFD;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.CheckBox checkBoxBatteryPresent;
        private System.Windows.Forms.CheckBox checkBoxMonitorOn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelPowerPersonality;
        private System.Windows.Forms.CheckBox checkBoxUpsPresent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelPowerSource;
    }
}

