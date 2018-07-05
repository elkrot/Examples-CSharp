using System;using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HitTesting
{
    class MyCircle : MyShape
    {
        private Point _center;
        private int _radius;

        public MyCircle(Point center, int radius)
        {
            _center = center;
            _radius = radius;
        }

        public override void Draw(System.Drawing.Graphics graphics)
        {
            graphics.DrawEllipse(Pens.Black, _center.X - _radius, _center.Y - _radius,
                _radius * 2, _radius * 2);
        }

        public override bool HitTest(System.Drawing.Point location)
        {
            /* X^2 + Y^2 = R^2 is the formula for a circle.
             * where R is the radius
             * A point is in the circle if X^2 + Y^2 <= R^2
             * 
             * This all assumes the location is 0,0 so be sure to normalize to that
             */
            Point normalized = new Point(location.X - _center.X, location.Y - _center.Y);

            return normalized.X * normalized.X + normalized.Y * normalized.Y <= (_radius * _radius);
        }

        public override string ToString()
        {
            return "MyCircle";
        }
    }
}
