namespace StripHtml
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
            this.textBoxHtml = new System.Windows.Forms.TextBox();
            this.textBoxStripped = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source (HTML):";
            // 
            // textBoxHtml
            // 
            this.textBoxHtml.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHtml.Location = new System.Drawing.Point(103, 5);
            this.textBoxHtml.Multiline = true;
            this.textBoxHtml.Name = "textBoxHtml";
            this.textBoxHtml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxHtml.Size = new System.Drawing.Size(385, 92);
            this.textBoxHtml.TabIndex = 1;
            // 
            // textBoxStripped
            // 
            this.textBoxStripped.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStripped.Location = new System.Drawing.Point(103, 129);
            this.textBoxStripped.Multiline = true;
            this.textBoxStripped.Name = "textBoxStripped";
            this.textBoxStripped.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxStripped.Size = new System.Drawing.Size(385, 92);
            this.textBoxStripped.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Stripped:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 97);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "&Go";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 231);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxStripped);
            this.Controls.Add(this.textBoxHtml);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "StripHtml";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxHtml;
        private System.Windows.Forms.TextBox textBoxStripped;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}

