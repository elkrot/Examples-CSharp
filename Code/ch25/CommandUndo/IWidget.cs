using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CommandUndo
{
    interface IWidget
    {
        void Draw(Graphics graphics);
        bool HitTest(Point point);
        Point Location { get; set; }
        Size Size { get; set; }
        Rectangle BoundingBox { get; }
    }
}
