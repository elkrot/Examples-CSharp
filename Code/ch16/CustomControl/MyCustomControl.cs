using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CustomControl
{
    public enum RGBSelection
    {
        R,G,B
    };

    public partial class MyCustomControl : UserControl
    {
        //the controls
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.NumericUpDown numericUpDownValue;
        private System.Windows.Forms.TrackBar trackBarValue;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        
        private void InitializeComponent()
        {
            this.labelLabel = new System.Windows.Forms.Label();
            this.numericUpDownValue = new System.Windows.Forms.NumericUpDown();
            this.trackBarValue = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarValue)).BeginInit();
            this.SuspendLayout();
            // labelLabel
            this.labelLabel.AutoSize = true;
            this.labelLabel.Location = new System.Drawing.Point(4, 4);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(71, 13);
            this.labelLabel.TabIndex = 0;
            this.labelLabel.Text = "Dummy Label";
            // numericUpDownValue
            this.numericUpDownValue.Location = new System.Drawing.Point(175, 20);
            this.numericUpDownValue.Name = "numericUpDownValue";
            this.numericUpDownValue.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownValue.TabIndex = 1;
            // trackBarValue
            this.trackBarValue.Location = new System.Drawing.Point(7, 20);
            this.trackBarValue.Name = "trackBarValue";
            this.trackBarValue.Size = new System.Drawing.Size(162, 45);
            this.trackBarValue.TabIndex = 2;
            // MyCustomControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trackBarValue);
            this.Controls.Add(this.numericUpDownValue);
            this.Controls.Add(this.labelLabel);
            this.Name = "MyCustomControl";
            this.Size = new System.Drawing.Size(216, 65);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public RGBSelection ColorPart {get;set;}
            
        public string Label
        {
            get
            {
                return labelLabel.Text;
            }
            set
            {
                labelLabel.Text = value;
            }
        }

        public int Value
        {
            get
            {
                return (int)numericUpDownValue.Value;
            }
            set
            {
                numericUpDownValue.Value = (int)value;
            }
        }

        public event EventHandler<EventArgs> ValueChanged;
        
        public MyCustomControl()
        {
            InitializeComponent();

            numericUpDownValue.Minimum = 0;
            numericUpDownValue.Maximum = 255;
            trackBarValue.Minimum = 0;
            trackBarValue.Maximum = 255;

            trackBarValue.TickFrequency = 10;

            numericUpDownValue.ValueChanged += numericUpDownValue_ValueChanged;
            trackBarValue.ValueChanged += trackBarValue_ValueChanged;
        }

        void trackBarValue_ValueChanged(object sender, EventArgs e)
        {
            if (sender != this)
            {
                numericUpDownValue.Value = trackBarValue.Value;

                OnValueChanged();
            }
        }

        void numericUpDownValue_ValueChanged(object sender, EventArgs e)
        {
            if (sender != this)
            {
                trackBarValue.Value = (int)numericUpDownValue.Value;
                
                OnValueChanged();
            }
        }

        protected void OnValueChanged()
        {
            Refresh();
            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = new Rectangle(numericUpDownValue.Left, 5, numericUpDownValue.Width, numericUpDownValue.Bounds.Top - 10);
            
            int r = 0, g = 0, b = 0;
            switch (ColorPart)
            {
                case RGBSelection.R:
                    r = Value;
                    break;
                case RGBSelection.G:
                    g = Value;
                    break;
                case RGBSelection.B:
                    b = Value;
                    break;
            }
            Color c = Color.FromArgb(r,g,b);
            using (Brush brush = new SolidBrush(c))
            {
                e.Graphics.FillEllipse(brush, rect);
            }
        }
    }
}
