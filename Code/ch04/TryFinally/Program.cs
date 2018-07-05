using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace TryFinally
{
    class Program
    {
        static void Main(string[] args)
        {
            FinallyOutOfBlock();
            Console.WriteLine();

            FinallyFromAReturn();
            Console.WriteLine();

            try
            {
                FinallyFromAnException();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Caught exception...");
            }
            Console.WriteLine();

            FinallyFromEnvironmentExit();

            Console.WriteLine("Done...should never get here");
            
        }
        private static void FinallyOutOfBlock()
        {
            try
            {
                Console.WriteLine("FinallyOutOfBlock: Inside try");
            }
            finally
            {
                Console.WriteLine("!Finally!");
            }
            Console.WriteLine("After try/finally");
        }

        private static void FinallyFromAReturn()
        {
            try
            {
                Console.WriteLine("FinallyFromReturn: Inside try");
                return;
            }
            finally
            {
                Console.WriteLine("!Finally!");
            }
        }

        private static void FinallyFromAnException()
        {
            try
            {
                Console.WriteLine("FinallyFromAnException: Inside try");
                throw new InvalidOperationException();
            }
            finally
            {
                Console.WriteLine("!Finally!");
            }
        }

        private static void FinallyFromEnvironmentExit()
        {
            try
            {
                Console.WriteLine("FinallyFromEnvironmentExit: Inside try");
                Console.WriteLine("About to call EnvironmentExit()");
                Environment.Exit(1);
            }
            finally
            {
                Console.WriteLine("!Finally! (you should not see this one)");
            }
        }

        

       

        
    }
}
