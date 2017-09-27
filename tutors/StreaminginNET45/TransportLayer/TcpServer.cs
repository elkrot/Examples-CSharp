using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        TcpListener tcpListener = new TcpListener(ip, 5200);
        tcpListener.Start();

        Console.WriteLine("Waiting for a client to connect...");

        //blocks until a client connects
        Socket socketForClient = tcpListener.AcceptSocket();
        
        Console.WriteLine("Client connected");

        //Read data sent from client
        NetworkStream networkStream = new NetworkStream(socketForClient);
        int bytesReceived, totalReceived = 0;
        byte[] receivedData = new byte[1000];
        do
        {
            bytesReceived = networkStream.Read
                (receivedData, 0, receivedData.Length);
            totalReceived += bytesReceived;
        }
        while (bytesReceived != 0);
        Console.WriteLine("Total bytes read: " + totalReceived.ToString());
        
        socketForClient.Close();
        Console.WriteLine("Client disconnected...");

        Console.ReadLine();
    }
}

