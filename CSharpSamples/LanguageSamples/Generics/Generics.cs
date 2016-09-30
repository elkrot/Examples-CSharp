// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
// (C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Generics_CSharp
{
    //Ввод параметра T в угловых скобках.
    public class MyList<T> : IEnumerable<T>
    {
        protected Node head;
        protected Node current = null;

        // Вложенный тип также является универсальным шаблоном для T
        protected class Node
        {
            public Node next;
            //T в качестве типа данных частного члена.
            private T data;
            //T, применяемый в не универсальном конструкторе.
            public Node(T t)
            {
                next = null;
                data = t;
            }
            public Node Next
            {
                get { return next; }
                set { next = value; }
            }
            //T, как тип возврата свойства.
            public T Data
            {
                get { return data; }
                set { data = value; }
            }
        }

        public MyList()
        {
            head = null;
        }

        //T, как тип параметра метода.
        public void AddHead(T t)
        {
            Node n = new Node(t);
            n.Next = head;
            head = n;
        }

        // Реализация GetEnumerator, возвращающего IEnumerator<T>, чтобы включить
        // итерации по всем элементам списка. Следует заметить, что в C# 2.0 
        // реализация методов Current и MoveNext не является обязательной.
        // Компилятор создаст класс, реализующий IEnumerator<T>.
        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;

            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        // Реализация этого метода необходима, поскольку 
        // IEnumerable<T> наследует IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    public class SortedList<T> : MyList<T> where T : IComparable<T>
    {
        // Простой, неоптимизированный алгоритм сортировки, который 
        // упорядочивает элементы списка по возрастанию:
        public void BubbleSort()
        {
            if (null == head || null == head.Next)
                return;

            bool swapped;
            do
            {
                Node previous = null;
                Node current = head;
                swapped = false;

                while (current.next != null)
                {
                    //  Поскольку нам необходимо вызвать этот метод, класс SortedList
                    //  ограничен интерфейсом IEnumerable<T>
                    if (current.Data.CompareTo(current.next.Data) > 0)
                    {
                        Node tmp = current.next;
                        current.next = current.next.next;
                        tmp.next = current;

                        if (previous == null)
                        {
                            head = tmp;
                        }
                        else
                        {
                            previous.next = tmp;
                        }
                        previous = tmp;
                        swapped = true;
                    }

                    else
                    {
                        previous = current;
                        current = current.next;
                    }

                }// закончить, когда
            } while (swapped);
        }
    }

    // Простой класс реализует интерфейс IComparable<T>,
    // используя себя как аргумент типа. Это -
    // типовой шаблон проектирования объектов, которые
    // хранятся в универсальных списках.
    public class Person : IComparable<Person>
    {
        string name;
        int age;

        public Person(string s, int i)
        {
            name = s;
            age = i;
        }

        // Это приведет к тому, что элементы списка
        // будут отсортированы по возрастному значению.
        public int CompareTo(Person p)
        {
            return age - p.age;
        }

        public override string ToString()
        {
            return name + ":" + age;
        }

        // Должен реализовать метод Equals.
        public bool Equals(Person p)
        {
            return (this.age == p.age);
        }
    }

    class Generics
    {
        static void Main(string[] args)
        {
            //Объявление нового универсального класса SortedList и создание его экземпляра.
            //Person -- это аргумент типа.
            SortedList<Person> list = new SortedList<Person>();

            //Создание значений имени и возраста для инициализации объектов Person.
            string[] names = new string[] { "Franscoise", "Bill", "Li", "Sandra", "Gunnar", "Alok", "Hiroyuki", "Maria", "Alessandro", "Raul" };
            int[] ages = new int[] { 45, 19, 28, 23, 18, 9, 108, 72, 30, 35 };

            //Заполнение списка.
            for (int x = 0; x < names.Length; x++)
            {
                list.AddHead(new Person(names[x], ages[x]));
            }

            Console.WriteLine("Unsorted List:");
            //Печать неупорядоченного списка.
            foreach (Person p in list)
            {
                Console.WriteLine(p.ToString());
            }

            //Сортировка списка.
            list.BubbleSort();

            Console.WriteLine(String.Format("{0}Sorted List:", Environment.NewLine));
            //Печать упорядоченного списка.
            foreach (Person p in list)
            {
                Console.WriteLine(p.ToString());
            }

            Console.WriteLine("Done");
        }
    }

}
