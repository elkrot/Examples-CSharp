namespace CustomControl
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
            this.colorControl = new System.Windows.Forms.PictureBox();
            this.blueControl = new CustomControl.MyCustomControl();
            this.greenControl = new CustomControl.MyCustomControl();
            this.redControl = new CustomControl.MyCustomControl();
            ((System.ComponentModel.ISupportInitialize)(this.colorControl)).BeginInit();
            this.SuspendLayout();
            // 
            // colorControl
            // 
            this.colorControl.Location = new System.Drawing.Point(234, 12);
            this.colorControl.Name = "colorControl";
            this.colorControl.Size = new System.Drawing.Size(100, 195);
            this.colorControl.TabIndex = 1;
            this.colorControl.TabStop = false;
            // 
            // blueControl
            // 
            this.blueControl.ColorPart = CustomControl.RGBSelection.B;
            this.blueControl.Label = "My Custom Control";
            this.blueControl.Location = new System.Drawing.Point(12, 142);
            this.blueControl.Name = "blueControl";
            this.blueControl.Size = new System.Drawing.Size(216, 65);
            this.blueControl.TabIndex = 3;
            this.blueControl.Value = 0;
            this.blueControl.ValueChanged += new System.EventHandler<System.EventArgs>(this.OnValuesChanged);
            // 
            // greenControl
            // 
            this.greenControl.ColorPart = CustomControl.RGBSelection.G;
            this.greenControl.Label = "My Custom Control";
            this.greenControl.Location = new System.Drawing.Point(12, 77);
            this.greenControl.Name = "greenControl";
            this.greenControl.Size = new System.Drawing.Size(216, 65);
            this.greenControl.TabIndex = 2;
            this.greenControl.Value = 0;
            this.greenControl.ValueChanged += new System.EventHandler<System.EventArgs>(this.OnValuesChanged);
            // 
            // redControl
            // 
            this.redControl.ColorPart = CustomControl.RGBSelection.R;
            this.redControl.Label = "My Custom Control";
            this.redControl.Location = new System.Drawing.Point(12, 12);
            this.redControl.Name = "redControl";
            this.redControl.Size = new System.Drawing.Size(216, 65);
            this.redControl.TabIndex = 0;
            this.redControl.Value = 0;
            this.redControl.ValueChanged += new System.EventHandler<System.EventArgs>(this.OnValuesChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 255);
            this.Controls.Add(this.blueControl);
            this.Controls.Add(this.greenControl);
            this.Controls.Add(this.colorControl);
            this.Controls.Add(this.redControl);
            this.Name = "Form1";
            this.Text = "Demo a custom user control";
            ((System.ComponentModel.ISupportInitialize)(this.colorControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MyCustomControl redControl;
        private System.Windows.Forms.PictureBox colorControl;
        private MyCustomControl greenControl;
        private MyCustomControl blueControl;
    }
}

