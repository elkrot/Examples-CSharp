using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

class Program
{
    static void Main(string[] args)
    {
        //ReadWrite();
        Compare();

        Console.WriteLine("Done");
        Console.ReadLine();
    }

    static void ReadWrite()
    {
        using (MemoryMappedFile mmf =
            MemoryMappedFile.CreateFromFile
            (@"C:\files\data.txt", FileMode.CreateNew, "map1", 1000))
        using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
        {
            accessor.Write(0, (byte)88);
            Console.WriteLine(accessor.ReadByte(0));

            byte[] data = Encoding.UTF8.GetBytes("test data");
            accessor.WriteArray(1, data, 0, data.Length);

        }
    }

    static void Compare()
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();
        using (FileStream fs = new FileStream
            (@"c:\files\file1.txt", FileMode.Create))
        {
            for (int i = 0; i <= 200000; i++)
            {
                fs.Position = 10;
                fs.WriteByte(100);
                fs.Position = 5;
                fs.WriteByte(100);
                fs.Position = 15;
                fs.WriteByte(100);
                fs.Position = 3;
                fs.WriteByte(100);
                

            }
        }
        sw.Stop();
        Console.WriteLine("FileStream: " + sw.Elapsed);
        sw.Reset();

        sw.Start();
        using (MemoryMappedFile mmf =
           MemoryMappedFile.CreateFromFile
           (@"C:\files\file2.txt", System.IO.FileMode.Create, "map1", 1000))
        {
            using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
            {
                for (int i = 0; i <= 200000; i++)
                {
                    accessor.Write(10,(byte)100);
                    accessor.Write(5, (byte)100);
                    accessor.Write(15, (byte)100);
                    accessor.Write(3, (byte)100);
                }
            }
        }
        sw.Stop();
        Console.WriteLine("Memory-Mapped File: " + sw.Elapsed);
        sw.Reset();
    }

   
}

 
