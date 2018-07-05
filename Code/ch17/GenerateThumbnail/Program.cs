using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace GenerateThumbnail
{
    class Program
    {
        private static void PrintUsage()
        {
            Console.WriteLine("Usage: GenerateThumbnail.exe inputfile maxSize");
        }

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                PrintUsage();
                return;
            }

            if (!File.Exists(args[0]))
            {
                PrintUsage();
                return;
            }

            string outputFile = Path.GetFileNameWithoutExtension(args[0]) + "_thumb" + Path.GetExtension(args[0]);
            
            int maxSize = 0;
            if (!int.TryParse(args[1], out maxSize))
            {
                PrintUsage();
                return;
            }
                        
            try
            {
                Image image = Image.FromFile(args[0]);
                
                Size size = CalculateThumbSize(image.Size, maxSize);

                Image thumbnail = image.GetThumbnailImage(size.Width, size.Height, ThumbnailAbortCallback, IntPtr.Zero);
                thumbnail.Save(outputFile);     
           
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine(ex);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex);
            }
        }

        //called by image processor to know if it should stop the resizing
        //could be useful with large images
        private static bool ThumbnailAbortCallback()
        {
            return false;
        }

        //get the proportional size of the resulting image, based on the maxSize the user passed in
        private static Size CalculateThumbSize(Size size, int maxSize)
        {
            if (size.Width > size.Height)
            {
                return new Size(maxSize, (int)(((double)size.Height / (double)size.Width) * maxSize));
            }
            else
            {
                return new Size((int)(((double)size.Width / (double)size.Height) * maxSize), maxSize);
            }
        }
    }
}
