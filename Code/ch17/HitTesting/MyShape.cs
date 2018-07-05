using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HitTesting
{
    abstract class MyShape
    {
        public abstract void Draw(Graphics graphics);
        public abstract bool HitTest(Point location);
        public abstract string ToString();
    }
}
