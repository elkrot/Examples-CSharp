using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleToFile
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: ConsoleToFile filename output1 output2 output3 ...");
                return;
            }
            //write each command line argument to the file
            string destFilename = args[0];
            using (StreamWriter writer = File.CreateText(destFilename))
            {
                for (int i = 1; i < args.Length; i++)
                {
                    writer.WriteLine(args[i]);
                }
            }
            Console.WriteLine("Wrote args to file {0}", destFilename);

            //just read back the file and dump it to the console
            using (StreamReader reader = File.OpenText(destFilename))
            {
                string line = null;
                do
                {
                    line = reader.ReadLine();
                    Console.WriteLine(line);
                } while (line != null);
            }
        }
    }
}
