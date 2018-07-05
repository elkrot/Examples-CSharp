using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageResize
{
    class Program
    {
        private static void PrintUsage()
        {
            Console.WriteLine("Usage: ImageResize.exe inputfile width height");
        }
        
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                PrintUsage();
                return;
            }

            if (!File.Exists(args[0]))
            {
                PrintUsage();
                return;
            }

            int newWidth = int.Parse(args[1]);
            int newHeight = int.Parse(args[2]);

            string outputFile = string.Format("{0}_{1}x{2}{3}",
                Path.GetFileNameWithoutExtension(args[0]), newWidth, newHeight, Path.GetExtension(args[0]));
            
            Bitmap resizedBmp = null;
            try
            {
                Image image = Image.FromFile(args[0]);
                
                resizedBmp = new Bitmap(image, new Size(newWidth, newHeight));
                
                resizedBmp.Save(outputFile, image.RawFormat);
            }
            finally
            {
                if (resizedBmp!=null)
                {
                    resizedBmp.Dispose();
                }
            }
        }

    }
}
