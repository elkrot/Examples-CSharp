using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ScreenCapture
{
    public partial class Form1 : Form
    {
        Image _image = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCapture_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            //wait for window to be hidden so that we don't see our
            //own window in the screen capture
            Thread.Sleep(500);
            _image = CaptureScreen();
            pictureBox.Image = _image;
            this.Visible = true;
            buttonCopy.Enabled = true;
        }

        private Image CaptureScreen()
        {
            //combine bounds of all screens
            Rectangle bounds = GetScreenBounds();
            
            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(bounds.Location, new Point(0, 0), bounds.Size);
            }
            return bitmap;
        }

        private Rectangle GetScreenBounds()
        {
            Rectangle rect = new Rectangle();
            foreach (Screen screen in Screen.AllScreens)
            {
                rect = Rectangle.Union(rect, screen.Bounds);
            }
            return rect;
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (_image != null)
            {
                Clipboard.SetImage(_image);
            }
        }
    }
}
