using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CustomThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(ThreadProc));
            thread.IsBackground = true;//so it ends when Main ends
            thread.Start(Int32.MaxValue);//argument to thread proc

            while (!Console.KeyAvailable)
            {
                Console.WriteLine("Thread ID: {0}, waiting for key press", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
            }

            System.Threading.Timer timer = new Timer(TimerThreadProc, 13, 1000, 10000);
        }

        static private void TimerThreadProc(object state)
        {
            int val = (int)state;
            //do thread work
        }
    

        static void ThreadProc(object state)
        {
            Int32 end = (Int32)state;
            //simulate work
            for (int i = 0; i < end; ++i)
            {
                if (i % 100000000 == 0)
                {
                    Console.WriteLine("Thread ID: {0}, i: {1}", Thread.CurrentThread.ManagedThreadId, i);
                }
            }
        }
    }
}
