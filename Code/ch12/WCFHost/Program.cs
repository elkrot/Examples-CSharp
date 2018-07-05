using System;
using System.ServiceModel;
using FileServiceLib;

namespace WCFHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FileService Host");

            using (ServiceHost serviceHost = new ServiceHost(typeof(FileServiceLib.FileService)))
            {
                serviceHost.Open();

                Console.ReadLine();
            }
        }
    }
}
