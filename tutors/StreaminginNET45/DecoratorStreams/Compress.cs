using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Compress
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Compress
            using (FileStream fs = new FileStream
                (@"c:\files\data.txt", FileMode.Open))
            {
                byte[] dataToRead = ReadBytes(fs);
                using (FileStream fs1 = new FileStream
                    (@"c:\files\data1.txt", FileMode.CreateNew))
                using (Stream gs = new GZipStream(fs1, CompressionMode.Compress))
                    gs.Write(dataToRead, 0, dataToRead.Length);
            }
            Console.WriteLine("compression completed");
            Console.WriteLine();
            #endregion

            #region Decompress
            Console.WriteLine("press enter to decompress...");
            Console.ReadLine();
            using (FileStream fs = new FileStream
                (@"c:\files\data1.txt", FileMode.Open))
            using (Stream gs = new GZipStream(fs, CompressionMode.Decompress))
            {
                StreamReader sr = new StreamReader(gs);
                string data = sr.ReadToEnd();
                Console.WriteLine(data);
            }
            #endregion

            Console.WriteLine("Done");
            Console.ReadLine();

           
        }

        static byte[] ReadBytes(Stream s)
        {
            byte[] dataToRead = new byte[s.Length];
            int totalBytesRead = 0;
            int chunkBytesRead = 1;
            while (totalBytesRead < dataToRead.Length && chunkBytesRead > 0)
            {
                chunkBytesRead = s.Read(dataToRead, totalBytesRead, dataToRead.Length - totalBytesRead);
                totalBytesRead = totalBytesRead + chunkBytesRead;
            }

            return dataToRead;
        }
    }
}
