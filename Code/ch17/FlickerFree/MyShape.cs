using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FlickerFree
{
    class MyShape
    {
        static Random random = new Random();

        private Rectangle _bounds;
        private Color _fillColor = Color.Red;
        public Point Location
        {
            get
            {
                return _bounds.Location;
            }
            set
            {
                _bounds.Location = value;
            }
        }
        public MyShape(Point location)
        {
            _bounds = new Rectangle(location, new Size(50, 50));
            _fillColor = CreateRandomColor();
        }

        public static Color CreateRandomColor()
        {
            return Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
        }

        public bool HitTest(Point location)
        {
            return _bounds.Contains(location);
        }

        public void Draw(Graphics graphics)
        {
            using (Brush brush = new SolidBrush(_fillColor))
            {
                graphics.FillRectangle(brush, _bounds);
                graphics.DrawRectangle(Pens.Black, _bounds);
            }
        }
    }
}
