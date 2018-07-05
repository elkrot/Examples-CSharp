using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MiscPlugins
{
    public class Invert : PluginInterfaces.IImagePlugin
    {
        #region IImagePlugin Members

        public System.Drawing.Image RunPlugin(System.Drawing.Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            Bitmap newBitmap = new Bitmap(image);
            //make everything that has blue more blue
            for (int row = 0; row < bitmap.Height; ++row)
            {
                for (int col = 0; col < bitmap.Width; ++col)
                {
                    newBitmap.SetPixel(bitmap.Width - col - 1, bitmap.Height - row - 1,
                        bitmap.GetPixel(col, row));
                }
            }
            bitmap.Dispose();
            return newBitmap;
        }

        public string Name
        {
            get { return "Invert image"; }
        }

        #endregion
    }
}
