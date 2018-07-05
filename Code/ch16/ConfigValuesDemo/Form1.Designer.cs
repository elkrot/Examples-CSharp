namespace ConfigValuesDemo
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAppConfigValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxUserConfigValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "App Config Value:";
            // 
            // textBoxAppConfigValue
            // 
            this.textBoxAppConfigValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAppConfigValue.Location = new System.Drawing.Point(111, 9);
            this.textBoxAppConfigValue.Name = "textBoxAppConfigValue";
            this.textBoxAppConfigValue.ReadOnly = true;
            this.textBoxAppConfigValue.Size = new System.Drawing.Size(161, 20);
            this.textBoxAppConfigValue.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "User Config Value:";
            // 
            // textBoxUserConfigValue
            // 
            this.textBoxUserConfigValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUserConfigValue.Location = new System.Drawing.Point(111, 40);
            this.textBoxUserConfigValue.Name = "textBoxUserConfigValue";
            this.textBoxUserConfigValue.Size = new System.Drawing.Size(161, 20);
            this.textBoxUserConfigValue.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 85);
            this.Controls.Add(this.textBoxUserConfigValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxAppConfigValue);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Config Values Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAppConfigValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxUserConfigValue;
    }
}

