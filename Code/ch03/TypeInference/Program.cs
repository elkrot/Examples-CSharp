using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HowToCSharp.ch03.TypeInference
{
    class MyType { }

    class Program
    {
        static void Main(string[] args)
        {
            var x = 13;
            var myObj = new MyType();
            var myNums = new double[] { 1.0, 1.5, 2.0, 2.5, 3.0 };
            //not allowed to initialize to null
            //var myNullObj = null;
            //but setting it to null after definition is ok
            var myNullObj = new MyType();
            myNullObj = null;
            
            Console.WriteLine("x, Type: {0}", x.GetType());
            Console.WriteLine("myObj, Type: {0}", myObj.GetType());
            Console.WriteLine("myNums, Type: {0}", myNums.GetType());

            Console.ReadLine();
        }
    }
}
