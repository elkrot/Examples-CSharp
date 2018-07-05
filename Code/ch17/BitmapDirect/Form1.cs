using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace BitmapDirect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            labelSize.Text = string.Format("{0:N0} x {1:N0}", 
                pictureBoxSource.Image.Width, pictureBoxSource.Image.Height);

        }

        private void buttonCopySetPixel_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            
            Bitmap sourceImg = new Bitmap(pictureBoxSource.Image);

            Bitmap destImg = new Bitmap(sourceImg.Width, sourceImg.Height);
            watch.Start();
            for (int row = 0; row < sourceImg.Height; row++)
            {
                for (int col = 0; col < sourceImg.Width; col++)
                {
                    Color color = sourceImg.GetPixel(col, row);
                    color = Color.FromArgb(color.R / 2, color.G / 2, color.B / 2);
                    destImg.SetPixel(col, row, color);
                }
            }
            watch.Stop();
            pictureBoxDest.Image = destImg;
            UpdateStatus(watch.Elapsed);
        }

        private void UpdateStatus(TimeSpan span)
        {
            labelStatus.Text = string.Format("Copied in {0}", span);
        }

        /// <summary>
        /// Handles the Click event of the buttonCopyDirect control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonCopyDirect_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();

            Bitmap sourceImg = new Bitmap(pictureBoxSource.Image);

            Bitmap destImg = new Bitmap(sourceImg.Width, sourceImg.Height);
            watch.Start();
            Rectangle dataRect = new Rectangle(0,0, sourceImg.Width, sourceImg.Height);
            BitmapData sourceData = sourceImg.LockBits(dataRect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData destData = destImg.LockBits(dataRect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            IntPtr sourcePtr = sourceData.Scan0;
            IntPtr destPtr = destData.Scan0;
            byte[] buffer = new byte[sourceData.Stride];

            for (int row = 0; row < sourceImg.Height; row++)
            {
                // yes, we could copy the whole bitmap in one go,
                // but want to demonstrate the point
                System.Runtime.InteropServices.Marshal.Copy(
                    sourcePtr, buffer, 0, sourceData.Stride);
                
                //fiddle with the bits
                for (int i = 0; i < buffer.Length; i+=4)
                {
                    //each pixel is represented by 4 bytes
                    //last byte is transparency, which we'll ignore
                    buffer[i + 0] /= 2;
                    buffer[i + 1] /= 2;
                    buffer[i + 2] /= 2;
                }                

                System.Runtime.InteropServices.Marshal.Copy(
                    buffer, 0, destPtr, destData.Stride);
                sourcePtr = new IntPtr(sourcePtr.ToInt64() + sourceData.Stride);
                destPtr = new IntPtr(destPtr.ToInt64() + destData.Stride);
            }
            sourceImg.UnlockBits(sourceData);
            destImg.UnlockBits(destData);
            watch.Stop();
            pictureBoxDest.Image = destImg;
            UpdateStatus(watch.Elapsed);
        }
    }
}
