using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace MutexDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Mutex mutex = new Mutex(false, "MutexDemo");
            Process me = Process.GetCurrentProcess();
            string outputFile = "MutexDemoOutput.txt";
            
            while (!Console.KeyAvailable)
            {
                mutex.WaitOne();
                Console.WriteLine("Process {0} gained control", me.Id);
                using (FileStream fs = new FileStream(outputFile, FileMode.OpenOrCreate))
                using (TextWriter writer = new StreamWriter(fs))
                {
                    fs.Seek(0, SeekOrigin.End);
                    string output = string.Format("Process {0} writing timestamp {1}", me.Id, DateTime.Now.ToLongTimeString());
                    writer.WriteLine(output);
                    Console.WriteLine(output);
                }
                Console.WriteLine("Process {0} releasing control", me.Id);
                mutex.ReleaseMutex();
                Thread.Sleep(1000);
            }
        }
    }
}
