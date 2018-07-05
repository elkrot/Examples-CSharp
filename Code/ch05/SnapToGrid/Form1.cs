using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SnapToGrid
{
    public partial class Form1 : Form
    {
        private Point _firstPoint = Point.Empty;
        private Point _secondPoint = Point.Empty;

        public Form1()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            _firstPoint = _secondPoint = SnapInput(e.Location);
            Capture = true;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Capture = false;
            Refresh();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Capture)
            {
                _secondPoint = SnapInput(e.Location);
                Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Capture)
            {
                Point topLeft = new Point(Math.Min(_firstPoint.X, _secondPoint.X), Math.Min(_firstPoint.Y, _secondPoint.Y));
                Point bottomRight = new Point(Math.Max(_firstPoint.X, _secondPoint.X), Math.Max(_firstPoint.Y, _secondPoint.Y));
                Size size = new Size(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
                Rectangle rect = new Rectangle(topLeft, size);

                e.Graphics.DrawRectangle(Pens.Black, rect);
            }
        }
        private static Point SnapInput(Point point)
        {
            int gridSize = 25;
            return new Point(SnapInput(point.X, gridSize), SnapInput(point.Y, gridSize));
        }

        private static Int32 SnapInput(double input, Int32 multiple)
        {
            return (((Int32)(input + (multiple / 2.0))) / multiple) * multiple;
        }
    }
}
