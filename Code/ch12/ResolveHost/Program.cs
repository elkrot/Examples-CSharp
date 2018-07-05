using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ResolveHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] hosts =
            {
                "www.microsoft.com",
                "www.live.com",
                "www.google.com",
                "www.yahoo.com",
            };

            foreach (string host in hosts)
            {
                Console.WriteLine("{0}", host);
                //note that a host can have multiple IP addresses
                IPAddress[] addresses = Dns.GetHostAddresses(host);
                foreach (IPAddress addr in addresses)
                {
                    Console.Write("\t{0}", addr);
                }
                Console.WriteLine();
                
            }
            Console.ReadKey();
        }
    }
}
