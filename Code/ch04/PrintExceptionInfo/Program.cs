using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PrintExceptionInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DivideByZero();
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("ToString(): " + ex.ToString());
                Console.WriteLine("Message: " + ex.Message);
                Console.WriteLine("Source: " + ex.Source);
                Console.WriteLine("HelpLink: " + ex.HelpLink);
                Console.WriteLine("TargetSite: " + ex.TargetSite);
                Console.WriteLine("Inner Exception: " + ex.InnerException);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                Console.WriteLine("Data:");
                if (ex.Data != null)
                {
                    foreach (DictionaryEntry de in ex.Data)
                    {
                        Console.WriteLine("\t{0}: {1}", de.Key, de.Value);
                    }
                }
            }
            Console.ReadKey();
        }

        private static void DivideByZero()
        {
            int divisor = 0;
            Console.WriteLine("{0}", 13 / divisor);
        }
    }
}
