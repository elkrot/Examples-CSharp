namespace AutoWaitCursor
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
            this.buttonAutoWait = new System.Windows.Forms.Button();
            this.buttonOldWait = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonResetCursor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonAutoWait
            // 
            this.buttonAutoWait.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonAutoWait.Location = new System.Drawing.Point(65, 90);
            this.buttonAutoWait.Name = "buttonAutoWait";
            this.buttonAutoWait.Size = new System.Drawing.Size(127, 23);
            this.buttonAutoWait.TabIndex = 2;
            this.buttonAutoWait.Text = "Auto Wait w/Exception";
            this.buttonAutoWait.UseVisualStyleBackColor = true;
            this.buttonAutoWait.Click += new System.EventHandler(this.buttonAutoWait_Click);
            // 
            // buttonOldWait
            // 
            this.buttonOldWait.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonOldWait.Location = new System.Drawing.Point(65, 52);
            this.buttonOldWait.Name = "buttonOldWait";
            this.buttonOldWait.Size = new System.Drawing.Size(127, 23);
            this.buttonOldWait.TabIndex = 1;
            this.buttonOldWait.Text = "Old Wait w/Exception";
            this.buttonOldWait.UseVisualStyleBackColor = true;
            this.buttonOldWait.Click += new System.EventHandler(this.buttonOldWait_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.Location = new System.Drawing.Point(50, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "If an exception is thrown before cursor is restored, it will stick.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonResetCursor
            // 
            this.buttonResetCursor.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonResetCursor.Location = new System.Drawing.Point(65, 131);
            this.buttonResetCursor.Name = "buttonResetCursor";
            this.buttonResetCursor.Size = new System.Drawing.Size(127, 23);
            this.buttonResetCursor.TabIndex = 3;
            this.buttonResetCursor.Text = "Reset Cursor";
            this.buttonResetCursor.UseVisualStyleBackColor = true;
            this.buttonResetCursor.Click += new System.EventHandler(this.buttonResetCursor_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 166);
            this.Controls.Add(this.buttonResetCursor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOldWait);
            this.Controls.Add(this.buttonAutoWait);
            this.Name = "Form1";
            this.Text = "Auto WaitCursor Demo";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAutoWait;
        private System.Windows.Forms.Button buttonOldWait;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonResetCursor;
    }
}

