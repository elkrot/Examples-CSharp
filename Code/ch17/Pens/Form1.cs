using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Pens
{
    public partial class Form1 : Form
    {
        Point[] points = new Point[] 
            { 
                new Point(5,10),
                new Point(50, 10),
                new Point (10, 50)
            };
        LinearGradientBrush gradientBrush = new LinearGradientBrush(new Point(0, 0), new Point(50, 50), 
            Color.Red, Color.Yellow);
        HatchBrush hatchBrush = new HatchBrush(HatchStyle.DashedVertical, Color.Green, Color.Transparent);
        Pen[] pens;

        public Form1()
        {
            InitializeComponent();
            
            pens = new Pen[]
            {
                new Pen(Color.Red),
                new Pen(Color.Green, 4),    //width 4 pen
                new Pen(Color.Purple, 2),   //dash-dot pen
                new Pen(gradientBrush, 6),  //gradient pen
                new Pen(gradientBrush,6),   //rounded join and end cap
                new Pen(hatchBrush, 6)      //hatch pen
            };

            pens[2].DashStyle = DashStyle.DashDot;

            pens[4].EndCap = LineCap.Round;
            pens[4].LineJoin = LineJoin.Round;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            const int boxWidth = 100;
            const int boxHeight = 100;

            for (int i = 0; i < pens.Length; i++)
            {
                e.Graphics.TranslateTransform(
                    (i % 4) * boxWidth,
                    (i / 4) * boxHeight);
                e.Graphics.DrawLines(pens[i], points);
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
                foreach(Pen pen in pens)
                {
                    pen.Dispose();
                }
                hatchBrush.Dispose();
                gradientBrush.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
