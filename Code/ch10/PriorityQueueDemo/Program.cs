using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriorityQueueDemo
{
    class Program
    {
        enum Priority
        {
            //make order such that sorting puts high-pri first
            High,
            Middle,
            Low
        };

        static void Main(string[] args)
        {
            PriorityQueue<Priority, string> priQueue = new PriorityQueue<Priority, string>();

            priQueue.Enqueue(Priority.Low, "Task 1");
            priQueue.Enqueue(Priority.Low, "Task 2");
            priQueue.Enqueue(Priority.Middle, "Task 3");
            priQueue.Enqueue(Priority.Middle, "Task 4");
            priQueue.Enqueue(Priority.High, "Task 5");
            priQueue.Enqueue(Priority.Low, "Task 6");
            priQueue.Enqueue(Priority.High, "Task 7");

            //expected order: 5,7,3,4,1,2,6
            Console.WriteLine("Enumerate all:");
            foreach(string item in priQueue)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            Console.WriteLine("Dequeue all");
            while (priQueue.Count > 0)
            {
                string s = priQueue.Dequeue();
                Console.WriteLine(s);
            }

            Console.ReadKey();
        }
    }
}
