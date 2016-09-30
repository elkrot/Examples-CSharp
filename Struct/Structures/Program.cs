using Structures.CircularLinkedList;
using Structures.Deque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Связные списки
            SimpleAlgorithmsApp.LinkedList<string> linkedList = new SimpleAlgorithmsApp.LinkedList<string>();
            // добавление элементов
            linkedList.Add("Tom");
            linkedList.Add("Alice");
            linkedList.Add("Bob");
            linkedList.Add("Sam");

            // выводим элементы
            foreach (var item in linkedList)
            {
                Console.WriteLine(item);
            }
            // удаляем элемент
            linkedList.Remove("Alice");
            foreach (var item in linkedList)
            {
                Console.WriteLine(item);
            }
            // проверяем наличие элемента
            bool isPresent = linkedList.Contains("Sam");
            Console.WriteLine(isPresent == true ? "Sam присутствует" : "Sam отсутствует");

            // добавляем элемент в начало            
            linkedList.AppendFirst("Bill");
            #endregion

            #region Двусвязные списки
            SimpleAlgorithmsApp.DoublyLinkedList<string> dlinkedList = new SimpleAlgorithmsApp.DoublyLinkedList<string>();
            // добавление элементов
            dlinkedList.Add("Bob");
            dlinkedList.Add("Bill");
            dlinkedList.Add("Tom");
            dlinkedList.AddFirst("Kate");
            foreach (var item in dlinkedList)
            {
                Console.WriteLine(item);
            }
            // удаление
            dlinkedList.Remove("Bill");

            // перебор с последнего элемента
            foreach (var t in dlinkedList.BackEnumerator())
            {
                Console.WriteLine(t);
            }
            #endregion

            #region Стек
            NodeStack<string> stack = new NodeStack<string>();
            stack.Push("Tom");
            stack.Push("Alice");
            stack.Push("Bob");
            stack.Push("Kate");

            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            string header = stack.Peek();
            Console.WriteLine($"Верхушка стека: {header}");
            Console.WriteLine();

            header = stack.Pop();
            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
            #endregion

            #region Очередь
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("Kate");
            queue.Enqueue("Sam");
            queue.Enqueue("Alice");
            queue.Enqueue("Tom");

            foreach (string item in queue)
                Console.WriteLine(item);
            Console.WriteLine();

            Console.WriteLine();
            string firstItem = queue.Dequeue();
            Console.WriteLine($"Извлеченный элемент: {firstItem}");
            Console.WriteLine();

            foreach (string item in queue)
                Console.WriteLine(item);
            #endregion

            #region Дек
            Deque<string> usersDeck = new Deque<string>();
            usersDeck.AddFirst("Alice");
            usersDeck.AddLast("Kate");
            usersDeck.AddLast("Tom");

            foreach (string s in usersDeck)
                Console.WriteLine(s);

            string removedItem = usersDeck.RemoveFirst();
            Console.WriteLine("\n Удален: {0} \n", removedItem);

            foreach (string s in usersDeck)
                Console.WriteLine(s);
            #endregion

            #region Кольцевой односвязный список
            CircularLinkedList<string> circularList = new CircularLinkedList<string>();

            circularList.Add("Tom");
            circularList.Add("Bob");
            circularList.Add("Alice");
            circularList.Add("Jack");
            foreach (var item in circularList)
            {
                Console.WriteLine(item);
            }

            circularList.Remove("Bob");
            Console.WriteLine("\n После удаления: \n");
            foreach (var item in circularList)
            {
                Console.WriteLine(item);
            }
            #endregion


            #region Кольцевой двусвязный список
            Structures.CircularDoublyLL.CircularDoublyLinkedList<string> circularListd = new Structures.CircularDoublyLL.CircularDoublyLinkedList<string>();
            circularListd.Add("Tom");
            circularListd.Add("Bob");
            circularListd.Add("Alice");
            circularListd.Add("Sam");

            foreach (var item in circularListd)
            {
                Console.WriteLine(item);
            }

            circularListd.Remove("Bob");
            Console.WriteLine("\n После удаления: \n");
            foreach (var item in circularListd)
            {
                Console.WriteLine(item);
            }
            #endregion
            Console.ReadKey();
        }
    }
}
