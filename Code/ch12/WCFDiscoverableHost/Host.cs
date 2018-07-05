using System;
using System.ServiceModel;

namespace WCFDiscoverableHost
{
    class Host
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FileService Host (Discoverable)");

            using (ServiceHost serviceHost = new ServiceHost(typeof(FileServiceLib.FileService)))
            {
                serviceHost.Open();

                Console.ReadLine();
            }
        }
    }
}
