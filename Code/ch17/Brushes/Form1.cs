using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Brushes
{
    public partial class Form1 : Form
    {
        Brush[] brushes;
        
        const int boxSize = 175;
        Rectangle ellipseRect = new Rectangle(0, 0, boxSize, boxSize);
        GraphicsPath path = new GraphicsPath();
            
        public Form1()
        {
            InitializeComponent();

            path.AddRectangle(ellipseRect);

            brushes = new Brush[]
            {
                new SolidBrush(Color.Red),
                new HatchBrush(HatchStyle.Cross, Color.Green, Color.Transparent),
                new TextureBrush(Properties.Resources.Elements),
                new LinearGradientBrush(ellipseRect, Color.LightGoldenrodYellow, Color.ForestGreen, 45),
                new PathGradientBrush(path)
            };

            (brushes[4] as PathGradientBrush).SurroundColors = new Color[] { Color.ForestGreen, Color.AliceBlue, Color.Aqua };
            (brushes[4] as PathGradientBrush).CenterColor = Color.Fuchsia;
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
                        
            ellipseRect.Inflate(-10, -10);

            for (int i=0;i<brushes.Length;i++)
            {
                e.Graphics.TranslateTransform(
                    (i % 3) * boxSize,
                    (i / 3) * boxSize);
                e.Graphics.FillEllipse(brushes[i], ellipseRect);
                e.Graphics.ResetTransform();
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                foreach(Brush brush in brushes)
                {
                    brush.Dispose();
                }
                path.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
