using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObserverPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            DataGenerator generator = new DataGenerator();

            DataObserver observer1 = new DataObserver("O1");
            DataObserver observer2 = new DataObserver("O2");

            generator.Subscribe(observer1);
            generator.Subscribe(observer2);

            generator.Run();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
