using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReverseLinkedList
{
    class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }
        public Node(T val) { this.Value = val; this.Next = null; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Node<int> list = BuildList(10);
            
            PrintList<int>(list);
            ReverseList<int>(ref list);
            PrintList<int>(list);

            Console.ReadKey();
        }

        private static Node<int> BuildList(int maxValue)
        {
            Node<int> head = new Node<int>(0);
            Node<int> tail = head;
            for (int i = 1; i <= maxValue; i++)
            {
                tail.Next = new Node<int>(i);
                tail = tail.Next;
            }
            return head;
        }

        private static void PrintList<T>(Node<T> head)
        {
            Node<T> current = head;
            while (current != null)
            {
                Console.Write("{0} ", current.Value);
                current = current.Next;
            }
            Console.WriteLine();
        }

        private static void ReverseList<T>(ref Node<T> head)
        {
            Node<T> tail = head;
            //track the next node
            Node<T> p = head.Next;
            //make the old head the end of the line
            tail.Next = null;
            while (p != null)
            {
                //get the next one
                Node<T> n = p.Next;
                //set it to our current end
                p.Next = tail;
                //reset the end
                tail = p;
                p = n;
            }
            //the new head is where the tail is
            head = tail;
        }
    }
}
