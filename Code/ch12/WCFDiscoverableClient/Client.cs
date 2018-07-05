using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Discovery;

namespace WCFDiscoverableClient
{
    class Client
    {
        static void Main(string[] args)
        {
            DiscoveryClient client = new DiscoveryClient(new UdpDiscoveryEndpoint());

            //find all the endpoints available -- you can also call this method asynchronously
            FindCriteria criteria = new FindCriteria(typeof(FileServiceLib.IFileService));
            FindResponse response = client.Find(criteria);

            //bind to one of them
            FileServiceClient svcClient = null;
            foreach (var endpoint in response.Endpoints)
            {
                svcClient = new FileServiceClient();
                svcClient.Endpoint.Address = endpoint.Address;
                break;
            }
            //call the service
            if (svcClient != null)
            {
                string[] dirs = svcClient.GetSubDirectories(@"C:\");
                foreach (string dir in dirs)
                {
                    Console.WriteLine(dir);
                }
            }
            Console.ReadLine();
        }
    }
}
