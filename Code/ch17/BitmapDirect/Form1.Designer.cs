namespace BitmapDirect
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
            this.pictureBoxSource = new System.Windows.Forms.PictureBox();
            this.buttonCopySetPixel = new System.Windows.Forms.Button();
            this.buttonCopyDirect = new System.Windows.Forms.Button();
            this.pictureBoxDest = new System.Windows.Forms.PictureBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDest)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxSource
            // 
            this.pictureBoxSource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxSource.Image = global::BitmapDirect.Properties.Resources.Elements_Large;
            this.pictureBoxSource.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxSource.Name = "pictureBoxSource";
            this.pictureBoxSource.Size = new System.Drawing.Size(200, 150);
            this.pictureBoxSource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSource.TabIndex = 0;
            this.pictureBoxSource.TabStop = false;
            // 
            // buttonCopySetPixel
            // 
            this.buttonCopySetPixel.Location = new System.Drawing.Point(219, 65);
            this.buttonCopySetPixel.Name = "buttonCopySetPixel";
            this.buttonCopySetPixel.Size = new System.Drawing.Size(99, 23);
            this.buttonCopySetPixel.TabIndex = 1;
            this.buttonCopySetPixel.Text = "Copy (SetPixel)";
            this.buttonCopySetPixel.UseVisualStyleBackColor = true;
            this.buttonCopySetPixel.Click += new System.EventHandler(this.buttonCopySetPixel_Click);
            // 
            // buttonCopyDirect
            // 
            this.buttonCopyDirect.Location = new System.Drawing.Point(231, 111);
            this.buttonCopyDirect.Name = "buttonCopyDirect";
            this.buttonCopyDirect.Size = new System.Drawing.Size(75, 23);
            this.buttonCopyDirect.TabIndex = 2;
            this.buttonCopyDirect.Text = "Copy (Direct)";
            this.buttonCopyDirect.UseVisualStyleBackColor = true;
            this.buttonCopyDirect.Click += new System.EventHandler(this.buttonCopyDirect_Click);
            // 
            // pictureBoxDest
            // 
            this.pictureBoxDest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxDest.Location = new System.Drawing.Point(323, 12);
            this.pictureBoxDest.Name = "pictureBoxDest";
            this.pictureBoxDest.Size = new System.Drawing.Size(200, 150);
            this.pictureBoxDest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDest.TabIndex = 3;
            this.pictureBoxDest.TabStop = false;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(106, 165);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 13);
            this.labelStatus.TabIndex = 4;
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(231, 13);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(35, 13);
            this.labelSize.TabIndex = 5;
            this.labelSize.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 186);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.pictureBoxDest);
            this.Controls.Add(this.buttonCopyDirect);
            this.Controls.Add(this.buttonCopySetPixel);
            this.Controls.Add(this.pictureBoxSource);
            this.Name = "Form1";
            this.Text = "Bitmap Direct";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDest)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxSource;
        private System.Windows.Forms.Button buttonCopySetPixel;
        private System.Windows.Forms.Button buttonCopyDirect;
        private System.Windows.Forms.PictureBox pictureBoxDest;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelSize;
    }
}

