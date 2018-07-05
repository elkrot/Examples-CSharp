using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace WebDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: WebDownloader url outputfile");
                return;
            }

            string url = args[0];
            string outputfile = args[1];

            using (WebClient client = new WebClient())
            {
                //could also do: byte[] bytes = client.DownloadData(url);
                try
                {
                    client.DownloadFile(url, outputfile);
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
