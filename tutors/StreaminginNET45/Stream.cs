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
        using (FileStream fs = File.Create(@"C:\files\testfile3.txt"))
        {

            // position is 0
            long pos = fs.Position;

            // sets the position to 1
            fs.Position = 1; 
            
            byte[] arrbytes = { 100, 101 };
            //writes the content of arrbytes into current position - which is 1
            fs.Write(arrbytes, 0, arrbytes.Length);
            //position is now 3 as its advanced by write
            pos = fs.Position;

            fs.Position = 0;
            byte[] readdata1 = ReadBytes(fs);

        }
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
            chunkBytesRead = stream.Read(dataToRead, totalBytesRead, 
                dataToRead.Length - totalBytesRead);
            totalBytesRead = totalBytesRead + chunkBytesRead;
        }

        return dataToRead;
    }
}

