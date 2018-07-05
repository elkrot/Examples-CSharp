using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RadiansAndDegrees
{
    public partial class RadiansAndDegreesForm : Form
    {
        string _radians, _degrees;

        public RadiansAndDegreesForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawAxes(e.Graphics);
            DrawValues(e.Graphics);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Refresh();
        }

        private void DrawAxes(Graphics graphics)
        {
            int midX = ClientSize.Width / 2;
            int midY = ClientSize.Height / 2;

            int border = 10;

            graphics.DrawLine(Pens.Black, new Point(0 + border, midY), new Point(ClientSize.Width - border, midY));
            graphics.DrawLine(Pens.Black, new Point(midX, 0 + border), new Point(midX, ClientSize.Height - border));
        }

        private void DrawValues(Graphics graphics)
        {
            if (!string.IsNullOrEmpty(_radians))
            {
                graphics.DrawString(_radians, DefaultFont, Brushes.Blue, 5, 0);
            }

            if (!string.IsNullOrEmpty(_degrees))
            {
                graphics.DrawString(_degrees, DefaultFont, Brushes.Red, 5, 15);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Point center = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
            Point mouse = e.Location;

            double radians = Math.Atan2(mouse.Y - center.Y, mouse.X - center.X);
            double degrees = RadiansToDegrees(radians);
            radians = DegreesToRadians(degrees);

            _radians = string.Format("{0:F3} radians", radians);
            _degrees = string.Format("{0:F3} degrees", degrees);

            base.OnMouseMove(e);
            Refresh();
        }

        private double RadiansToDegrees(double radians)
        {
            return radians * 360.0 / ( 2.0 * Math.PI) ;
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * (2.0 * Math.PI) / 360.0;
        }
    }
}
