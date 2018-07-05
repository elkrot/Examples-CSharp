using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BluifyPlugin
{
    public class HalfBlue : PluginInterfaces.IImagePlugin
    {
        #region IImagePlugin Members

        public System.Drawing.Image RunPlugin(System.Drawing.Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            //make everything that has blue more blue
            for (int row = 0; row < bitmap.Height; ++row)
            {
                for (int col = 0; col < bitmap.Width; ++col)
                {
                    Color color = bitmap.GetPixel(col, row);
                    if (color.B > 0)
                    {
                        color = Color.FromArgb(color.A, color.R, color.G, color.B / 2);
                    }
                    bitmap.SetPixel(col, row, color);

                }
            }
            return bitmap;
        }

        public string Name
        {
            get { return "Half Blue"; }
        }

        #endregion
    }
}
