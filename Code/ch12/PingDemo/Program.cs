using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingReply reply = ping.Send("yahoo.com");
            Console.WriteLine("address: {0}", reply.Address);
            Console.WriteLine("options: don't fragment: {0}, TTL: {1}", reply.Options.DontFragment, reply.Options.Ttl);
            Console.WriteLine("rountrip: {0}ms", reply.RoundtripTime);
            Console.WriteLine("status: {0}", reply.Status);

            Console.ReadKey();
        }
    }
}
