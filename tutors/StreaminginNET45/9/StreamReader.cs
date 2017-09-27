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
        byte[] data = Encoding.UTF8.GetBytes("ABCDE");
        using (Stream ms = new MemoryStream(data))
        {
            using (StreamReader sr = new StreamReader(ms))
            {
                char[] c = new char[3];
                sr.Read(c, 0, c.Length); 
                Console.WriteLine(c);

                ms.Position = 4;
                sr.DiscardBufferedData();
                Console.WriteLine((char)sr.Read());
            }
        }

        Console.WriteLine("Done");
        Console.ReadLine();
    }
}

