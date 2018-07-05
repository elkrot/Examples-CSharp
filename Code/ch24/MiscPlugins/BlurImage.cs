using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MiscPlugins
{
    public class BlurImage : PluginInterfaces.IImagePlugin
    {
        #region IImagePlugin Members

        public System.Drawing.Image RunPlugin(System.Drawing.Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            Bitmap newBitmap = new Bitmap(image.Width, image.Height);

            int radius = 4;
            //take a simple average of all pixels in radius

            for (int row = 0; row < bitmap.Height; ++row)
            {
                for (int col = 0; col < bitmap.Width; ++col)
                {
                    int startRow = Math.Max(0, row - radius);
                    int endRow = Math.Min(bitmap.Height - 1, row + radius);

                    int startCol = Math.Max(0, col - radius);
                    int endCol = Math.Min(bitmap.Width - 1, col + radius);

                    double r = 0, g = 0, b = 0;
                    int count = 0;
                    for (int cr = startRow; cr <= endRow; ++cr)
                    {
                        for (int cc = startCol; cc <= endCol; ++cc)
                        {
                            Color pixel = bitmap.GetPixel(cc, cr);
                            r += pixel.R;
                            g += pixel.G;
                            b += pixel.B;
                            ++count;
                        }
                    }
                    newBitmap.SetPixel(col, row, Color.FromArgb(255, (int)(r / count), (int)(g / count), (int)(b / count)));
                }
            }
            return newBitmap;

        }

        public string Name
        {
            get { return "Blur Image (REALLY, REALLY SLOW!)"; }
        }

        #endregion
    }
}
