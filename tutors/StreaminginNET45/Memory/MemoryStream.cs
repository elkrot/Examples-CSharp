using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            #region default constructor

            byte[] arr1 = { 1, 2, 3 };
            byte[] arr2 = new byte[253];

            //will automatically grow in size
            MemoryStream ms = new MemoryStream();
            
            //sets initial capacity to 256 bytes
            ms.Write(arr1, 0, arr1.Length);
            
            //length is still <=256
            ms.Write(arr2, 0, arr2.Length);

            //capacity is doubled to 512 bytes
            ms.WriteByte(1);

            #endregion

            #region provide capacity

            //set initial capacity to 1 byte
            MemoryStream ms1 = new MemoryStream(1); 
            ms1.WriteByte(234);

            //capacity is increased to 256 bytes
            ms1.WriteByte(234);

            #endregion

            #region provide an array

            byte[] arr = { 66, 77, 88 };

            //stream cannot be resized anymore
            MemoryStream ms2 = new MemoryStream(arr);

            //reads 66, position = 1 
            int x = ms2.ReadByte();

            //replaces 77 with 55
            ms2.WriteByte(55);

            //reset the position
            ms2.Position = 0; 
            byte[] data = ReadBytes(ms2);

            #endregion

            #region Misc
            byte[] arr3 = { 66, 77, 88 };

            //set CanWrite to false
            MemoryStream ms3 = new MemoryStream(arr,false); 
            
            //To do: read data
            byte[] a = ms3.ToArray();
            
            #endregion
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

