using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;

namespace NicInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface nic in nics)
            {
                //basically, there are just a bunch of properties to query
                Console.WriteLine("ID: {0}", nic.Id);
                Console.WriteLine("Name: {0}", nic.Name);
                Console.WriteLine("Description: {0}", nic.Description);
                Console.WriteLine("Type: {0}", nic.NetworkInterfaceType);
                Console.WriteLine("Status: {0}", nic.OperationalStatus);
                Console.WriteLine("Speed: {0}", nic.Speed);
                Console.WriteLine("Supports Multicast: {0}", nic.SupportsMulticast);
                Console.WriteLine("Receive-only: {0}", nic.IsReceiveOnly);
                Console.WriteLine("Physical Address: {0}", nic.GetPhysicalAddress());
                IPInterfaceProperties props = nic.GetIPProperties();
                PrintIPCollection("DHCP Servers: ", props.DhcpServerAddresses);
                PrintIPCollection("DNS Servers: ", props.DnsAddresses);
                                
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        private static void PrintIPCollection(string title, IPAddressCollection ipAddresses)
        {
            Console.Write(title);
            foreach (IPAddress addr in ipAddresses)
            {
                Console.Write(addr + " ");
            }
            Console.WriteLine();
        }
    }
}
