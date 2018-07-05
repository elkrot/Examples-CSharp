using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rethrow
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RethrowWithNoPreservation();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Stack trace from rethrow (no stack preservation):");
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine();

            try
            {
                RethrowWithPreservation();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Stack trace from rethrow (stack preservation):");
                Console.WriteLine(ex.StackTrace);
            }

            Console.ReadKey();
        }

        private static void DoSomething()
        {
            throw new ArgumentException("This function has no arguments!");
        }

        private static void RethrowWithNoPreservation()
        {
            try
            {
                DoSomething();
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }

        private static void RethrowWithPreservation()
        {
            try
            {
                DoSomething();
            }
            catch (ArgumentException ex)
            {
                throw;
            }

        }
    }
}
