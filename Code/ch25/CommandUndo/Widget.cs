using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CommandUndo
{
    class Widget : IWidget
    {
        private Rectangle _rect;

        public Widget(Point location)
        {
            _rect = new Rectangle(location, new Size(50, 75));
        }

        #region IWidget Members

        public void Draw(System.Drawing.Graphics graphics)
        {
            graphics.DrawRectangle(Pens.Black, _rect);
            int thirdHeight = _rect.Top + _rect.Height / 3;
            graphics.DrawLine(Pens.Black, _rect.Left, thirdHeight, _rect.Right, thirdHeight);
        }

        public bool HitTest(System.Drawing.Point point)
        {
            return _rect.Contains(point);
        }

        public Point Location
        {
            get
            {
                return _rect.Location;
            }
            set
            {
                _rect.Location = value;
            }
        }

        public Size Size
        {
            get
            {
                return _rect.Size;
            }
            set
            {
                _rect.Size = value;
            }
        }
        
        public Rectangle BoundingBox
        {
            get
            {
                return _rect;
            }
        }

        #endregion
    }
}
