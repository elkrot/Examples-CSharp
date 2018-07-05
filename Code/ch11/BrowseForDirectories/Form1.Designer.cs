namespace BrowseForDirectories
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
            this.buttonBrowse1 = new System.Windows.Forms.Button();
            this.textBoxFilename1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonBrowse1
            // 
            this.buttonBrowse1.Location = new System.Drawing.Point(410, 12);
            this.buttonBrowse1.Name = "buttonBrowse1";
            this.buttonBrowse1.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse1.TabIndex = 0;
            this.buttonBrowse1.Text = "Browse...";
            this.buttonBrowse1.UseVisualStyleBackColor = true;
            this.buttonBrowse1.Click += new System.EventHandler(this.buttonBrowse1_Click);
            // 
            // textBoxFilename1
            // 
            this.textBoxFilename1.Location = new System.Drawing.Point(12, 15);
            this.textBoxFilename1.Name = "textBoxFilename1";
            this.textBoxFilename1.Size = new System.Drawing.Size(392, 20);
            this.textBoxFilename1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 180);
            this.Controls.Add(this.textBoxFilename1);
            this.Controls.Add(this.buttonBrowse1);
            this.Name = "Form1";
            this.Text = "Browse for Directories";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBrowse1;
        private System.Windows.Forms.TextBox textBoxFilename1;
    }
}

