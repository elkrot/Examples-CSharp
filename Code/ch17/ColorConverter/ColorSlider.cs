using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ColorConverter
{
    public partial class ColorSlider : UserControl, IDisposable
    {
        private Color _startColor, _endColor;
        private Rectangle _colorRect;
        private const int ColorRectMargin = 5;

        private LinearGradientBrush _brush = null;
        private bool _dragging = false;
        private Color[] _customColors;

        public int Maximum
        {
            get
            {
                return (int)numericUpDown1.Maximum;
            }
            set
            {
                numericUpDown1.Maximum = value;
            }
        }
        public Color StartColor
        {
            get
            {
                return _startColor;
            }
            set
            {
                _endColor = value;
                CreateBrush();
            }
        }

        public Color EndColor 
        {
            get
            {
                return _endColor;
            }
            set
            {
                _endColor = value;
                CreateBrush();
            }
        }

        public Color[] CustomColors
        {
            get
            {
                return _customColors;
            }
            set
            {
                _customColors = value;
            }
        }

        public int Value
        {
            get
            {
                return (int)numericUpDown1.Value;
            }
            set
            {
                numericUpDown1.Value = value;
            }
        }

        public event EventHandler<EventArgs> ValueChanged;
        protected void OnValueChanged()
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }

        public ColorSlider()
        {
            InitializeComponent();

            StartColor = Color.Black;
            EndColor = Color.White;

            numericUpDown1.ValueChanged += new EventHandler(numericUpDown1_ValueChanged);

            this.DoubleBuffered = true;
        }

        public void SetColors(Color startColor, Color endColor)
        {
            _startColor = startColor;
            _endColor = endColor;
            CreateBrush();
            Refresh();
        }

        private void CreateBrush()
        {
            if (_brush != null)
            {
                _brush.Dispose();
            }
            _brush = new LinearGradientBrush(_colorRect, StartColor, EndColor, 0.0f);
        }

        void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Refresh();
            OnValueChanged();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (!ClientRectangle.IsEmpty)
            {
                _colorRect = new Rectangle(ColorRectMargin, ColorRectMargin,
                    ClientRectangle.Width - numericUpDown1.Width - ColorRectMargin * 3,
                    ClientSize.Height - ColorRectMargin * 2);
            }
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawColorRect(e.Graphics);

            DrawSliderWidget(e.Graphics);
        }

        private void DrawSliderWidget(Graphics graphics)
        {
            GraphicsState state = graphics.Save();
            Point[] outerPoints = new Point[]
            {
                new Point(0, -10),
                new Point(5, 0),
                new Point(0, 5),
                new Point(-5,0),
                new Point (0, -10)
            };

            Point[] innerPoints = new Point[]
            {
                new Point(0, -5),
                new Point(2, 0),
                new Point(0, 2),
                new Point(-2,0),
                new Point (0, -5)
            };
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(outerPoints);
            path.AddPolygon(innerPoints);
            graphics.TranslateTransform(_colorRect.Left + (float)(_colorRect.Width * numericUpDown1.Value) / Maximum, _colorRect.Bottom);
            graphics.FillPath(Brushes.DarkGray, path);
            //graphics.DrawPolygon(Pens.Black, points);
            graphics.Restore(state);
        }
        
        // Mouse events

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (_colorRect.Contains(e.Location) && e.Button == MouseButtons.Left)
            {
                _dragging = true;
                UpdateSliderPos(e.Location);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_dragging && e.Button == MouseButtons.Left)
            {
                _dragging = false;
                UpdateSliderPos(e.Location);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_dragging)
            {
                UpdateSliderPos(e.Location);
            }
        }
        
        private void UpdateSliderPos(Point location)
        {
            double percent = (double)(location.X - _colorRect.Left) / _colorRect.Width;
            if (percent > 1.0)
                percent = 1.0;
            else if (percent < 0.0)
                percent = 0.0;
            numericUpDown1.Value = (int)(percent * Maximum);
        }

        private void DrawColorRect(Graphics graphics)
        {
            if (CustomColors == null)
            {
                if (_brush == null)
                {
                    CreateBrush();
                }
                graphics.FillRectangle(_brush, _colorRect);
                
            }
            else
            {
                //draw a vertical slice for each custom color
                double sliceWidth = (double)_colorRect.Width / CustomColors.Length;
                double left = _colorRect.Left;
                for (int i = 0; i < CustomColors.Length; i++)
                {
                    using (Brush brush = new SolidBrush(CustomColors[i]))
                    {
                        graphics.FillRectangle(brush, (int)left, _colorRect.Top, (int)((sliceWidth < 1) ? 1:sliceWidth), _colorRect.Height );
                    }
                    left += sliceWidth;
                }
            }
            graphics.DrawRectangle(Pens.Black, _colorRect);
        }

    }
}
