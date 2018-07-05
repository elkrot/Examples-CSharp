using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace DrawImages
{
    public partial class Form1 : Form
    {
        private InterpolationMode _interpolationMode = InterpolationMode.Default;

        public Form1()
        {
            InitializeComponent();

            drawPanel.Paint += new PaintEventHandler(drawPanel_Paint);

            radioButtonInterpolationBicubic.Tag = InterpolationMode.Bicubic;
            radioButtonInterpolationBilinear.Tag = InterpolationMode.Bilinear;
            radioButtonInterpolationDefault.Tag = InterpolationMode.Default;
            radioButtonInterpolationHigh.Tag = InterpolationMode.High;
            radioButtonInterpolationHighQualityBicubic.Tag = InterpolationMode.HighQualityBicubic;
            radioButtonInterpolationHighQualityBilinear.Tag = InterpolationMode.HighQualityBilinear;
            radioButtonInterpolationLow.Tag = InterpolationMode.Low;
            radioButtonInterpolationNeighbor.Tag = InterpolationMode.NearestNeighbor;
        }

        void drawPanel_Paint(object sender, PaintEventArgs e)
        {
            Image smallImage = Properties.Resources.Elements_Small;
            Image largeImage = Properties.Resources.Elements_Large;

            //draw normally
            e.Graphics.DrawImage(smallImage, 10, 10);

            //draw resized--interpolating pixels
            e.Graphics.InterpolationMode = _interpolationMode;
            e.Graphics.DrawImage(smallImage, 250, 100, 400, 400);

            //draw a subsection
            Rectangle sourceRect= new Rectangle(400,400,200,200);
            Rectangle destRect = new Rectangle(10,200,sourceRect.Width, sourceRect.Height);
            e.Graphics.DrawImage(Properties.Resources.Elements_Large, destRect, sourceRect, GraphicsUnit.Pixel);

            //draw same subsection with black as transparent
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorKey(Color.Black, Color.Black, ColorAdjustType.Bitmap);
            destRect.Offset(200, 150);
            e.Graphics.DrawImage(largeImage, destRect, sourceRect.X, sourceRect.Y,
                sourceRect.Width, sourceRect.Height, GraphicsUnit.Pixel, imageAttributes);
        }

        private void OnInterpolationChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button != null)
            {
                _interpolationMode = (InterpolationMode)button.Tag;
                Refresh();
            }
        }
    }
}
