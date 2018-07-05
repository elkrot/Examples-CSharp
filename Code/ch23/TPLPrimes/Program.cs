using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TPLPrimes
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxPrimes = 1000000;
            int maxNumber = 20000000;
            long primesFound = 0;
            Console.WriteLine("Iterative");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (UInt32 i = 0; i < maxNumber; ++i)
            {
                if (IsPrime(i))
                {
                    Interlocked.Increment(ref primesFound);
                    if (primesFound > maxPrimes)
                    {
                        Console.WriteLine("Last prime found: {0:N0}", i);
                        break;
                    }
                }
            }
            watch.Stop();
            Console.WriteLine("Found {0:N0} primes in {1}", 
                              primesFound, watch.Elapsed);

            watch.Reset();
            primesFound = 0;
            Console.WriteLine("Parallel");
            watch.Start();
            //in order to stop the loop, there is an overload that takes Action<int, ParallelLoopState>
            Parallel.For(0, maxNumber, (i, loopState) =>
            {
                if (IsPrime((UInt32)i))
                {
                    Interlocked.Increment(ref primesFound);
                    if (primesFound > maxPrimes)
                    {
                        Console.WriteLine("Last prime found: {0:N0}", i);
                        loopState.Stop();
                    }
                }

            });
            watch.Stop();
            Console.WriteLine("Found {0:N0} primes in {1}", 
                              primesFound, watch.Elapsed);
            
            Console.ReadKey();
        }

        public static bool IsPrime(UInt32 number)
        {
            //check for evenness
            if (number % 2 == 0)
            {
                if (number == 2)
                    return true;
                return false;
            }
            //don't need to check past the square root
            UInt32 max = (UInt32)Math.Sqrt(number);
            for (UInt32 i = 3; i <= max; i += 2)
            {
                if ((number % i) == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
