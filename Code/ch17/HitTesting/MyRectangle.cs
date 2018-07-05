using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HitTesting
{
    class MyRectangle : MyShape
    {
        private int _left, _top, _width, _height;

        public MyRectangle(int left, int top, int width, int height)
        {
            _left = left;
            _top = top;
            _width = width;
            _height = height;
        }

        public override void Draw(System.Drawing.Graphics graphics)
        {
            graphics.DrawRectangle(Pens.Black, _left, _top, _width, _height);
        }

        public override bool HitTest(System.Drawing.Point location)
        {
            return location.X >= _left && location.X <= _left + _width
                && location.Y >= _top && location.Y <= _top + _height;
        }

        public override string ToString()
        {
            return "MyRectangle";
        }
    }
}
