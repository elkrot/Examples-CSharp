namespace WebBrowser
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.buttonGo = new System.Windows.Forms.Button();
            this.radioButtonUrl = new System.Windows.Forms.RadioButton();
            this.radioButtonHTML = new System.Windows.Forms.RadioButton();
            this.textBoxHTML = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(12, 63);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(659, 332);
            this.webBrowser1.TabIndex = 0;
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Location = new System.Drawing.Point(78, 9);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(514, 20);
            this.textBoxUrl.TabIndex = 2;
            this.textBoxUrl.Text = "http://www.bing.com";
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(598, 9);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(73, 45);
            this.buttonGo.TabIndex = 3;
            this.buttonGo.Text = "&Go";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // radioButtonUrl
            // 
            this.radioButtonUrl.AutoSize = true;
            this.radioButtonUrl.Checked = true;
            this.radioButtonUrl.Location = new System.Drawing.Point(13, 13);
            this.radioButtonUrl.Name = "radioButtonUrl";
            this.radioButtonUrl.Size = new System.Drawing.Size(47, 17);
            this.radioButtonUrl.TabIndex = 4;
            this.radioButtonUrl.TabStop = true;
            this.radioButtonUrl.Text = "URL";
            this.radioButtonUrl.UseVisualStyleBackColor = true;
            this.radioButtonUrl.CheckedChanged += new System.EventHandler(this.OnRadioCheckedChanged);
            // 
            // radioButtonHTML
            // 
            this.radioButtonHTML.AutoSize = true;
            this.radioButtonHTML.Location = new System.Drawing.Point(13, 37);
            this.radioButtonHTML.Name = "radioButtonHTML";
            this.radioButtonHTML.Size = new System.Drawing.Size(55, 17);
            this.radioButtonHTML.TabIndex = 5;
            this.radioButtonHTML.TabStop = true;
            this.radioButtonHTML.Text = "HTML";
            this.radioButtonHTML.UseVisualStyleBackColor = true;
            this.radioButtonHTML.CheckedChanged += new System.EventHandler(this.OnRadioCheckedChanged);
            // 
            // textBoxHTML
            // 
            this.textBoxHTML.Enabled = false;
            this.textBoxHTML.Location = new System.Drawing.Point(78, 37);
            this.textBoxHTML.Name = "textBoxHTML";
            this.textBoxHTML.Size = new System.Drawing.Size(514, 20);
            this.textBoxHTML.TabIndex = 6;
            this.textBoxHTML.Text = "<b>An example</b>with<i>HTML</i>";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 407);
            this.Controls.Add(this.textBoxHTML);
            this.Controls.Add(this.radioButtonHTML);
            this.Controls.Add(this.radioButtonUrl);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.textBoxUrl);
            this.Controls.Add(this.webBrowser1);
            this.Name = "Form1";
            this.Text = "My Web Browser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.RadioButton radioButtonUrl;
        private System.Windows.Forms.RadioButton radioButtonHTML;
        private System.Windows.Forms.TextBox textBoxHTML;
    }
}

