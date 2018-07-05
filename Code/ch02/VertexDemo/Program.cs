using System;
using System.Collections.Generic;
using System.Text;

namespace VertexDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Vertex3d v = new Vertex3d(1.0, 2.0, 3.0);
            Vertex3d v2 = new Vertex3d(4.0, 5.0, 6.0);
            Vertex3d v3 = new Vertex3d(7.0, 8.0, 9.0);

            Console.WriteLine("ToString(): " + v.ToString());
            Console.WriteLine("ToString() with custom formatting: " + v.ToString("[x:X, y:Y]", null));

            TypeFormatter formatter = new TypeFormatter();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(formatter, "Custom formatter: {0:(X Y)}; {1:[X, Y, Z]}", v, v2);

            Console.WriteLine(sb.ToString());

            v.Id = 1;
            v2.Id = 2;
            v3.Id = 3;

            List<Vertex3d> vertices = new List<Vertex3d>();
            vertices.Add(v3);
            vertices.Add(v2);
            vertices.Add(v);

            PrintList("Before sort:", vertices);

            vertices.Sort();
            
            PrintList("After sort:", vertices);

            v[1] = 13.0;
            Console.WriteLine("Change Y to 13 via index: " + v.ToString());
            v["Y"] = 2.0;
            Console.WriteLine("Change Y to 2 via string: " + v.ToString());

            Vertex3d sum = v + v2;
            Console.WriteLine("{0} + {1} = {2}", v, v2, v + v2);

            Vertex3d vd = new Vertex3d(1.5, 2.5, 3.5);
            //Error: Cannot implicitly convert type 'Vertex3d' to 'Vertex3i'. 
            //       An explicit conversion exists (are you missing a cast?)
            //Vertex3i vi = vd;
            Vertex3i vi = (Vertex3i)vd;

            Console.WriteLine("vi = (Vertex3i)vd;: vd: {0}, vi: {1}", vd, vi);

            vd = vi;
            Console.WriteLine("vd = vi: vd: {0}, vi: {1}", vd, vi);

            //Nullable types
            Vertex3d vn = new Vertex3d(1, 2, 3);
            vn.Id = 3;      //ok
            vn.Id = null;   //ok
            try
            {
                Console.WriteLine("ID: {0}", vn.Id.Value);//throws
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Oops--you can't get a null value!");
            }

            if (vn.Id.HasValue)
            {
                Console.WriteLine("ID: {0}", vn.Id.Value);
            }
            
            Console.ReadLine();
        }

        private static void PrintList(string message, List<Vertex3d> vertices)
        {
            Console.WriteLine(message);
            foreach (Vertex3d v in vertices)
            {
                Console.WriteLine(v.ToString());
            }
        }
    }
}
