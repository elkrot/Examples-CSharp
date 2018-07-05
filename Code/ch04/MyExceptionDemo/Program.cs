using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyExceptionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MyException myEx = new MyException(13.0, "My very own exception!");
            Console.WriteLine(myEx.ToString());
            Console.WriteLine("Value: " + myEx.ExceptionData);
            Console.ReadKey();
        }
    }
}
