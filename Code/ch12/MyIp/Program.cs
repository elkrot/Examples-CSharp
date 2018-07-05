using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MyIp
{
    class Program
    {
        static void Main(string[] args)
        {
            string hostname = Dns.GetHostName();
            Console.WriteLine("Hostname: {0}", hostname);
            IPAddress[] addresses = Dns.GetHostAddresses(hostname);
            foreach (IPAddress addr in addresses)
            {
                Console.WriteLine("IP Address: {0} ({1})", addr.ToString(), addr.AddressFamily);
            }
            Console.ReadKey();
        }
    }
}
