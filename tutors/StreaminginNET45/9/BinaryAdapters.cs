using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryAdapters
{
    class Program
    {
        static void Main(string[] args)
        {
            int intVal = 3;
            string stringVal = "abcde";
            
            using (FileStream fs = new FileStream
                (@"c:\files\binary.txt", FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(intVal);
                    bw.Write(stringVal);
                }
            }

            using (FileStream fs = new FileStream
                (@"c:\files\binary.txt", FileMode.Open))
            {
                using (BinaryReader bw = new BinaryReader(fs))
                {
                    int var = bw.ReadInt32();
                    byte b = bw.ReadByte();
                    string var2 = bw.ReadString();
                }
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
