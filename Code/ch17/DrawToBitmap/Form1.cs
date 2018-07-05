using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawToBitmap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Render(e.Graphics);            
        }

        private void Render(Graphics graphics)
        {
            graphics.FillEllipse(Brushes.Red, 10, 10, 100, 50);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Bitmap bitmap = new Bitmap(ClientSize.Width, ClientSize.Height))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                Render(graphics);
                Clipboard.SetImage(bitmap);
            }
        }
    }
}
