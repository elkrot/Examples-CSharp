using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Diagnostics;

namespace MemoryMappedFileDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //these are small numbers for demo purposes, but memory-mapped files really
            //take off when you want to edit files that are too large to fit in memory
            Int64 offset = 256;
            Int64 length = 64;

            using (FileStream fs = File.Open("DataFile.txt", FileMode.Open, FileAccess.ReadWrite))
            using (MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(fs,"map",fs.Length, 
                MemoryMappedFileAccess.ReadWrite, null,
                HandleInheritability.Inheritable,false))
            using (MemoryMappedViewAccessor acc = mmf.CreateViewAccessor(offset, length, MemoryMappedFileAccess.ReadWrite))
            {
                //now you can use the acc to treat the file like an array, essentially
                for (Int64 i = 0; i < acc.Capacity; i++)
                {
                    //convert char to byte because in .Net chars are two bytes wide,
                    acc.Write(i, (byte)(((i % 2) == 0) ? 'E' : 'O'));

                }
            }

            Process.Start("DataFile.txt");
          
        }
    }
}
