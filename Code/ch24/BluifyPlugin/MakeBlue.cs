using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluginInterfaces;
using System.Drawing;

namespace BluifyPlugin
{
    public class MakeBlue : IImagePlugin
    {
        #region IImagePlugin Members

        public System.Drawing.Image RunPlugin(System.Drawing.Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            //make everything that has blue more blue
            for (int row=0; row < bitmap.Height; ++row)
            {
                for (int col=0;col<bitmap.Width; ++col)
                {
                    Color color = bitmap.GetPixel(col, row);
                    if (color.B > 0)
                    {
                        color = Color.FromArgb(color.A, color.R, color.G, 255);
                    }
                    bitmap.SetPixel(col, row, color);

                }
            }
            return bitmap;
        }

        public string Name
        {
            get
            {
                return "Make Blue";
            }
        }

        #endregion
    }
}
