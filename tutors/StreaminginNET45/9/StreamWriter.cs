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
        using (Stream ms = new MemoryStream())
        {
            using (StreamWriter sw = new StreamWriter(ms))
            {
                sw.Write('A');
                sw.Write('B');

                long x = sw.BaseStream.Position;
                sw.Flush();
                x = sw.BaseStream.Position;
            }
        }

       

        Console.WriteLine("Done");
        Console.ReadLine();
    }
}

