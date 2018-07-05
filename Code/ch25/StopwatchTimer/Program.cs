using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StopwatchTimer
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            Decimal total = 0;
            int limit = 1000000;
            for (int i = 0; i < limit; ++i)
            {
                total = total + (Decimal)Math.Sqrt(i);
            }
            timer.Stop();
            Console.WriteLine("Sum of sqrts: {0}",total);
            Console.WriteLine("Elapsed milliseconds: {0}", timer.ElapsedMilliseconds);
            Console.WriteLine("Elapsed time: {0}", timer.Elapsed);

            using (new AutoStopwatch())
            {
                Decimal total2 = 0;
                int limit2 = 1000000;
                for (int i = 0; i < limit2; ++i)
                {
                    total2 = total2 + (Decimal)Math.Sqrt(i);
                }
            }

            Console.ReadKey();
        }
    }
}
