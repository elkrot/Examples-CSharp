using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //ByteTransmission();

            MessageTransmission();
            
        }

        static void ByteTransmission()
        {
            using (NamedPipeClientStream client = 
                new NamedPipeClientStream("pipe1"))
            {
                Console.WriteLine("press to connect to server");
                Console.ReadLine();
                
                client.Connect();
                
                Console.WriteLine("connection established to server");

                Console.WriteLine("press enter to read byte written by server");
                Console.ReadLine();
                
                Console.WriteLine(client.ReadByte());

                Console.WriteLine("press enter to write byte to pipe");
                Console.ReadLine();
                
                client.WriteByte(145);
                
                Console.WriteLine("byte written to pipe");
            }

            Console.WriteLine("end");
            Console.ReadLine();
        }

        static void MessageTransmission()
        {
            using (var s = new NamedPipeClientStream("n1"))
            {
                Console.WriteLine("press enter to connect to server");
                Console.ReadLine();
                
                s.Connect();

                Console.WriteLine("press enter to write message to pipe");
                Console.ReadLine();
                
                byte[] msg = Encoding.UTF8.GetBytes
                    ("this is a test message from client: using named pipes to transmit messages");
                s.Write(msg, 0, msg.Length);

                Console.WriteLine("message written to pipe");
                Console.WriteLine("press enter to exit");
                Console.ReadLine();
            }
        }
    }
}
