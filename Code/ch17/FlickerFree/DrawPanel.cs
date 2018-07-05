using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FlickerFree
{
    class DrawPanel : Panel
    {
        private List<MyShape> _shapes = new List<MyShape>();
        private MyShape _draggedShape = null;
        Point prevPoint;

        public new bool DoubleBuffered
        {
            get
            {
                return base.DoubleBuffered;
            }
            set
            {
                base.DoubleBuffered = value;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_draggedShape != null)
            {
                Point offset = new Point(e.Location.X - prevPoint.X, e.Location.Y - prevPoint.Y);
                Point location = _draggedShape.Location;
                location.Offset(offset);
                _draggedShape.Location = location;
                prevPoint = e.Location;
                Refresh();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            //go through backwards to hit test the shapes on top
            for (int i = _shapes.Count - 1; i >= 0; i--)
            {
                if (_shapes[i].HitTest(e.Location))
                {
                    prevPoint = e.Location;
                    _draggedShape = _shapes[i];
                    return;
                }
            }

            //create a new shape
            MyShape myShape = new MyShape(e.Location);
            _shapes.Add(myShape);
            
            Refresh();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _draggedShape = null;
            Refresh();
        }
        
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            _shapes.Clear();
            Refresh();
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            PaintBackground(e.Graphics);

            DrawShapes(e.Graphics);
        }
        
        private void DrawShapes(Graphics graphics)
        {
            foreach (MyShape shape in _shapes)
            {
                if (_draggedShape != shape || true)
                {
                    shape.Draw(graphics);
                }
            }
        }

        private void PaintBackground(Graphics graphics)
        {
            using (Brush brush = new LinearGradientBrush(ClientRectangle, Color.LightBlue, Color.LightGreen, 135))
            {
                graphics.FillRectangle(brush, ClientRectangle);
            }
        }
    }
}
