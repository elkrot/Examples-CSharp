using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HitTesting
{
    public partial class Form1 : Form
    {
        private List<MyShape> _shapes = new List<MyShape>();
        public Form1()
        {
            InitializeComponent();

            _shapes.Add(new MyRectangle(10, 10, 100, 50));
            _shapes.Add(new MyCircle(new Point(150, 200), 50));
            _shapes.Add(new MyEllipse(new Point(150, 50), 35, 15));

        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (MyShape shape in _shapes)
            {
                shape.Draw(e.Graphics);
            }

            //draw center cross
            e.Graphics.DrawLine(Pens.Black, ClientSize.Width / 2, ClientSize.Height / 2 - 10,
                ClientSize.Width / 2, ClientSize.Height / 2 + 10);
            e.Graphics.DrawLine(Pens.Black, ClientSize.Width / 2 - 10, ClientSize.Height / 2 ,
                ClientSize.Width / 2 + 10, ClientSize.Height / 2);
            
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            labelDistance.Text = string.Format("{0:N2} from center", DistanceFromCenter(e.Location));

            foreach (MyShape shape in _shapes)
            {
                if (shape.HitTest(e.Location))
                {
                    labelOver.Text = "Over " + shape.ToString();
                    return;
                }
            }
            labelOver.Text = "";
        }

        double DistanceFromCenter(Point location)
        {
            Point center = new Point(ClientSize.Width / 2, ClientSize.Height / 2);

            /*
             * Distance calculation is basically the Pythagorean theorem:
             * 
             * d = sqrt(dx^2 + dy^2) where dx and dy are the differences between the points
             */
            int dx = location.X - center.X;
            int dy = location.Y - center.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
