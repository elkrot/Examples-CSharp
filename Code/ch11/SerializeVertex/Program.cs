using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

namespace SerializeVertex
{
    class Program
    {
        static void Main(string[] args)
        {
            Vertex3d v = new Vertex3d(1.0, 2.0, 3.0);
            v.Id = null;
            Vertex3d v2;

            byte[] bytes = new byte[1024];
            string result = "";
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                SoapFormatter formatter = new SoapFormatter();
                formatter.Serialize(ms, v);
                result = Encoding.UTF8.GetString(bytes, 0, (int)ms.Position);

                ms.Position = 0;
                v2 = (Vertex3d)formatter.Deserialize(ms);
            }

            Console.WriteLine("Vertex: {0}", v);
            Console.WriteLine("Serialized to SOAP: " + Environment.NewLine + result);
            Console.WriteLine("Deserialized: {0}", v2);
            
            Console.ReadKey();
        }
    }
}
