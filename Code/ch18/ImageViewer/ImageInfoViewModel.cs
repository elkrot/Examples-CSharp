using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.IO;
using System.ComponentModel;

namespace ImageViewer
{
    public class ImageInfoViewModel
    {
        private BitmapImage _image;

        public BitmapImage Image
        {
            get
            {
                return _image;
            }
        }
        public string FileName
        {
            get
            {
                return Path.GetFileName(_image.UriSource.LocalPath);
            }
        }
        public int Width
        {
            get
            {
                return (int)_image.PixelWidth;
            }
        }

        public int Height
        {
            get
            {
                return (int)_image.PixelHeight;
            }
        }

        public ICollection<KeyValuePair<string, object>> AllProperties
        {
            get
            {
                return CreateProperties();
            }
        }

        public ImageInfoViewModel(BitmapImage image)
        {
            _image = image;
        }

        private IDictionary<string, object> CreateProperties()
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties["Width"] = _image.PixelWidth;
            properties["Height"] = _image.PixelHeight;
            properties["DpiX"] = _image.DpiX;
            properties["DpiY"] = _image.DpiY;
            properties["BitsPerPixel"] = _image.Format.BitsPerPixel;
            properties["Format"] = _image.Format.ToString();
            return properties;
        }
       
    }
}
