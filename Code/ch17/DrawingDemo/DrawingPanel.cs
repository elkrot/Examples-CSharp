using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawingDemo
{
    class DrawingPanel : Panel
    {
        private SmoothingMode _smoothingMode = SmoothingMode.None;
        private Font _textFont = new Font("Verdana", 18.0f);
        private int _textAngle = 0;
        private System.Drawing.Text.TextRenderingHint _textRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;

        public SmoothingMode SmoothingMode
        {
            get
            {
                return _smoothingMode;
            }
            set
            {
                _smoothingMode = value;
                Refresh();
            }
        }

        public Font TextFont
        {
            get
            {
                return _textFont;
            }
            set
            {
                _textFont = value;
                Refresh();
            }
        }

        public int TextAngle
        {
            get
            {
                return _textAngle;
            }
            set
            {
                _textAngle = value;
                Refresh();
            }
        }

        public System.Drawing.Text.TextRenderingHint TextRenderingHint
        {
            get
            {
                return _textRenderingHint;
            }
            set
            {
                _textRenderingHint = value;
                Refresh();
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            e.Graphics.SmoothingMode = _smoothingMode;

            //a filled shape with a border is actually two shapes
            e.Graphics.FillRectangle(Brushes.Red, 10, 10, 50, 50);
            e.Graphics.DrawRectangle(Pens.Black, 10, 10, 50, 50);

            e.Graphics.FillEllipse(Brushes.Green, 100, 10, 100, 50);
            e.Graphics.DrawEllipse(Pens.DarkGreen, 100, 10, 100, 50);

            //a circle is just an ellipse with equal width and height
            e.Graphics.FillEllipse(Brushes.Blue, 250, 10, 50, 50);
            e.Graphics.DrawEllipse(Pens.DarkBlue, 250, 10, 50, 50);

            //a single line
            e.Graphics.DrawLine(Pens.SteelBlue, 350, 10, 400, 60);

            //a series of connected lines
            Point[] linesPoints = new Point[] { 
                new Point(10,100),
                new Point(20, 110),
                new Point(35, 150),
                new Point(75, 105),
                new Point(30, 125)};

            e.Graphics.DrawLines(Pens.SpringGreen, linesPoints);

            //a polygon (closed series of lines)
            Point[] polygonPoints = new Point[] { 
                new Point(100,100),
                new Point(110,110),
                new Point(130,150),
                new Point(140,125),
                new Point(125,105)};

            e.Graphics.FillPolygon(Brushes.Tan, polygonPoints);
            e.Graphics.DrawPolygon(Pens.DarkGoldenrod, polygonPoints);

            //a curve that goes through each point
            //aka cardinal spline
            Point[] curvePoints = new Point[] { 
                new Point(200,100),
                new Point(210,110),
                new Point(230,150),
                new Point(240,125),
                new Point(225,105)};

            e.Graphics.DrawCurve(Pens.Purple, curvePoints);

            Point[] closedCurvePoints = new Point[] { 
                new Point(300,100),
                new Point(310,110),
                new Point(330,150),
                new Point(340,125),
                new Point(325,105)};

            e.Graphics.FillClosedCurve(Brushes.LightCoral, closedCurvePoints);
            e.Graphics.DrawClosedCurve(Pens.PowderBlue, closedCurvePoints);

            e.Graphics.FillPie(Brushes.LawnGreen, 10, 200, 100, 50, 180, 135);
            e.Graphics.DrawPie(Pens.DarkOliveGreen, 10, 200, 100, 50, 180, 135);

            e.Graphics.DrawArc(Pens.Plum, 150, 200, 100, 50, 180, 135);

            e.Graphics.TranslateTransform(100, 300);
            e.Graphics.RotateTransform(_textAngle);
            e.Graphics.TextRenderingHint = TextRenderingHint;
            e.Graphics.DrawString(_textFont.Name, _textFont, Brushes.DarkMagenta, 0, 0);
            e.Graphics.ResetTransform();


        }
    }
}
