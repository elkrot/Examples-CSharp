using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Transforms
{
    public partial class Form1 : Form
    {
        Font font = new Font("Verdana", 16.0f);
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle ellipseRect = new Rectangle(25, 25, 100, 50);

            // Rotation, angles are in degrees
            e.Graphics.RotateTransform(-15);
            e.Graphics.FillEllipse(Brushes.Red, ellipseRect);
            e.Graphics.ResetTransform();

            // Translation
            e.Graphics.TranslateTransform(0, 100);
            e.Graphics.FillEllipse(Brushes.Blue, ellipseRect);
            e.Graphics.ResetTransform();

            // Translation + Rotation
            // notice the order! it's important
            e.Graphics.TranslateTransform(100, 100);
            e.Graphics.RotateTransform(-15);
            e.Graphics.FillEllipse(Brushes.Purple, ellipseRect);
            e.Graphics.ResetTransform();
            
            // Scale (and translation)
            e.Graphics.TranslateTransform(0, 200);
            //make it twice as long and 3/4 as wide
            e.Graphics.ScaleTransform(2.0f, 0.75f);
            e.Graphics.FillEllipse(Brushes.Green, ellipseRect);
            e.Graphics.ResetTransform();
            
            // we can also use any arbitrary matrix 
            // to transform the graphics
            Matrix matrix = new Matrix();
            matrix.Translate(0, 300);
            matrix.Shear(0.5f, 0.25f);
            e.Graphics.Transform = matrix;
            e.Graphics.DrawString("Hello, Shear", font, Brushes.Black, 0, 0);
            e.Graphics.ResetTransform();
        }
    }
}
