using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {

            #region Demo1
            using (FileStream fs = new FileStream(@"c:\files\data.txt",
                FileMode.Create, FileAccess.Write))
            {
                fs.WriteByte(100);
                fs.Position = 0;
                if (fs.CanRead)
                    fs.ReadByte();
            }
            #endregion

            #region Demo2
            using (FileStream fs1 = new FileStream(@"C:\files\data.txt", 
                FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (FileStream fs2 = new FileStream(@"C:\files\data.txt", 
                    FileMode.Open, FileAccess.Read, FileShare.None))
                {
                }
            }
            #endregion




            Console.WriteLine("Done");
            Console.ReadLine();
            

        }

        static byte[] ReadBytes(Stream stream)
        {
            // dataToRead will hold the data read from the stream
            byte[] dataToRead = new byte[stream.Length];
          
            //this is the total number of bytes read. this will be incremented 
            //and eventually will equal the bytes size held by the stream
            int totalBytesRead = 0;
            
            //this is the number of bytes read in each iteration (i.e. chunk size)
            int chunkBytesRead = 1;
            
            while (totalBytesRead < dataToRead.Length && chunkBytesRead > 0)
            {
                chunkBytesRead = stream.Read(dataToRead, totalBytesRead, dataToRead.Length - totalBytesRead);
                totalBytesRead = totalBytesRead + chunkBytesRead;
            }
            
            return dataToRead;
        }
    }

