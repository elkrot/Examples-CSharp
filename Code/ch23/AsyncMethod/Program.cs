using System;
using System.Threading;

namespace AsyncMethod
{
    class Program
    {
        // async method calls must be done through a delegate
        delegate double DoWorkDelegate(int maxValue);

        static void Main(string[] args)
        {
            DoWorkDelegate del = DoWork;

            //two ways to be notified of when method ends:
            // 1. callback method
            // 2. call EndInvoke
            IAsyncResult res = del.BeginInvoke(100000000, DoWorkDone, null);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Doing other work...{0}", i);
                Thread.Sleep(1000);
            }
            
            //wait for end
            double sum = del.EndInvoke(res);
            Console.WriteLine("Sum: {0}", sum);

            Console.ReadKey();
        }

        static double DoWork(int maxValue)
        {
            Console.WriteLine("In DoWork");
            double sum = 0.0;
            for (int i = 1; i < maxValue; ++i)
            {
                sum += Math.Sqrt(i);
            }
            return sum;
        }

        static void DoWorkDone(object state)
        {
            //didn't pass in any state

            Console.WriteLine("Computation done");
        }
    }
}
