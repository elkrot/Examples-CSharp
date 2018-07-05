using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TcpClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("127.0.0.1", 1330);
            bool done = false;
            Console.WriteLine("Type 'bye' to end connection");
            while (!done)
            {
                Console.Write("Enter a message to send to server: ");
                string message = Console.ReadLine();

                SendMessage(client, message);

                string response = ReadResponse(client);
                Console.WriteLine("Response: " + response);
                done = response.Equals("BYE");
            }
        }

        private static void SendMessage(TcpClient client, string message)
        {
            //make sure the other end encodes with the same format!
            byte[] bytes = Encoding.Unicode.GetBytes(message);
            client.GetStream().Write(bytes, 0, bytes.Length);
        }

        private static string ReadResponse(TcpClient client)
        {
            byte[] buffer = new byte[256];
            int totalRead = 0;
            //read bytes until there are none left
            do
            {
                int read = client.GetStream().Read(buffer, totalRead,
                    buffer.Length - totalRead);
                totalRead += read;
            } while (client.GetStream().DataAvailable);
            return Encoding.Unicode.GetString(buffer, 0, totalRead);
        }
    }
}
