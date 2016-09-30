using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NamedAndOptional
{
    // В этой программе демонстрируется объявление метода с именованными
    // и необязательными параметрами, а также вызов метода при
    // явном использовании именованных и необязательных параметров.
    class Program
    {
        // Метод с именованными и необязательными параметрами
        public static void Search(string name, int age = 21, string city = "Pueblo")
        {
            Console.WriteLine("Name = {0} - Age = {1} - City = {2}", name, age, city);
        }

        static void Main(string[] args)
        {
            // Стандартный вызов
            Search("Sue", 22, "New York");

            // Пропустить параметр city.
            Search("Mark", 23);

            // Явно задать имя параметра city.
            Search("Lucy", city: "Cairo");

            // Использовать именованные параметры в обратном порядке.
            Search("Pedro", age: 45, city: "Saigon");
        }
    }
}
