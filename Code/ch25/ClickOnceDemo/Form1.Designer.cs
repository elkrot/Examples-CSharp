namespace ClickOnceDemo
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
            this.buttonWritetoFS = new System.Windows.Forms.Button();
            this.buttonWriteToIS = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonWritetoFS
            // 
            this.buttonWritetoFS.Location = new System.Drawing.Point(66, 12);
            this.buttonWritetoFS.Name = "buttonWritetoFS";
            this.buttonWritetoFS.Size = new System.Drawing.Size(155, 23);
            this.buttonWritetoFS.TabIndex = 0;
            this.buttonWritetoFS.Text = "Try to write to file system";
            this.buttonWritetoFS.UseVisualStyleBackColor = true;
            this.buttonWritetoFS.Click += new System.EventHandler(this.buttonWritetoFS_Click);
            // 
            // buttonWriteToIS
            // 
            this.buttonWriteToIS.Location = new System.Drawing.Point(66, 41);
            this.buttonWriteToIS.Name = "buttonWriteToIS";
            this.buttonWriteToIS.Size = new System.Drawing.Size(155, 23);
            this.buttonWriteToIS.TabIndex = 1;
            this.buttonWriteToIS.Text = "Write to isolate storage";
            this.buttonWriteToIS.UseVisualStyleBackColor = true;
            this.buttonWriteToIS.Click += new System.EventHandler(this.buttonWriteToIS_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 104);
            this.Controls.Add(this.buttonWriteToIS);
            this.Controls.Add(this.buttonWritetoFS);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "ClickOnce Demo";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonWritetoFS;
        private System.Windows.Forms.Button buttonWriteToIS;
    }
}

