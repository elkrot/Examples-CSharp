using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUIDs
{
    class Program
    {
        static void Main(string[] args)
        {
            Guid g = Guid.NewGuid();
            Console.WriteLine("GUID: {0}", g);

            Console.WriteLine();

            //parsing
            var guids = new Tuple<string,string>[]
            { 
                Tuple.Create("d261edd3-4562-41cb-ba7e-b176157951d8", "D"),
                Tuple.Create("d261edd3456241cbba7eb176157951d8", "N"),
                Tuple.Create("{d261edd3-4562-41cb-ba7e-b176157951d8}", "B"),
                Tuple.Create("(d261edd3-4562-41cb-ba7e-b176157951d8)", "P"),
                Tuple.Create("{0xd261edd3,0x4562,0x41cb,{0xba,0x7e,0xb1,0x76,0x15,0x79,0x51,0xd8}}", "X"),
            };

            foreach (var t in guids)
            {
                Console.WriteLine("Parse {0} ==> {1}", t.Item1, Guid.ParseExact(t.Item1, t.Item2));
                Console.WriteLine();
            }

            
            Console.ReadKey();

        }
    }
}
