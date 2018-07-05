using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

namespace TcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress localhost = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new System.Net.Sockets.TcpListener(localhost, 1330);
            listener.Start();
            
            while (true)
            {
                Console.WriteLine("Waiting for connection");
                //AcceptTcpClient waits for a connection from the client
                TcpClient client = listener.AcceptTcpClient();
                //start a new thread to handle this connection so we can go back 
                //to waiting for another client
                Thread thread = new Thread(new ParameterizedThreadStart(HandleClientThread));
                thread.Start(client);
            }

        }

        static void HandleClientThread(object obj)
        {
            TcpClient client = obj as TcpClient;
            
            bool done = false;
            while (!done)
            {
                string received = ReadMessage(client);
                Console.WriteLine("Received: {0}", received);
                done = received.Equals("bye");
                if (done) SendResponse(client, "BYE");
                else SendResponse(client, "OK");
                
            }
            client.Close();
            Console.WriteLine("Connection closed");
        }

        private static string ReadMessage(TcpClient client)
        {
            byte[] buffer = new byte[256];
            int totalRead = 0;
            //read bytes until stream indicates there are no more
            do
            {
                int read = client.GetStream().Read(buffer, totalRead, buffer.Length - totalRead);
                totalRead += read;
            } while (client.GetStream().DataAvailable);
             
            return Encoding.Unicode.GetString(buffer, 0, totalRead);
        }

        private static void SendResponse(TcpClient client, string message)
        {
            //make sure the other end decodes with the same format!
            byte[] bytes = Encoding.Unicode.GetBytes(message);
            client.GetStream().Write(bytes, 0, bytes.Length);
        }
    }
}
