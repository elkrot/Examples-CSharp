using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnhandledException
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            throw new InvalidOperationException("Oops");
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Caught unhandled exception");
            Console.WriteLine(e.ExceptionObject.ToString());
        }
    }
}
