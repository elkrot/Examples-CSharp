// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Collections.Generic;
using System.Text;

namespace AnonymousDelegate_Sample
{

    // Определение метода делегата
    delegate decimal CalculateBonus(decimal sales);

    // Определение типа Employee.
    class Employee
    {
        public string name;
        public decimal sales;
        public decimal bonus;
        public CalculateBonus calculation_algorithm;
    }

    class Program
    {

        // Этот класс определит двух делегатов, выполняющих расчет.
        // Первый будет именованным методом, а второй -- анонимным делегатом.

        // Это -- именованный метод.
        // Он определяет одну возможную реализацию алгоритма расчета бонуса.

        static decimal CalculateStandardBonus(decimal sales)
        {
            return sales / 10;
        }

        static void Main(string[] args)
        {

            // Значение, используемое при расчете бонуса.
            // Примечание. Эта локальная переменная станет "зафиксированной внешней переменной".
            decimal multiplier = 2;

            // Этот делегат определяется как именованный метод.
            CalculateBonus standard_bonus = new CalculateBonus(CalculateStandardBonus);

            // Этот делегат анонимный -- для него нет именованного метода.
            // Он определяет альтернативный алгоритм расчета бонуса.
            CalculateBonus enhanced_bonus = delegate(decimal sales) { return multiplier * sales / 10; };

            // Объявление нескольких объектов Employee.
            Employee[] staff = new Employee[5];

            // Заполнение массива Employees.
            for (int i = 0; i < 5; i++)
                staff[i] = new Employee();

            // Присвоение начальных значений объектам Employees.
            staff[0].name = "Mr Apple";
            staff[0].sales = 100;
            staff[0].calculation_algorithm = standard_bonus;

            staff[1].name = "Ms Banana";
            staff[1].sales = 200;
            staff[1].calculation_algorithm = standard_bonus;

            staff[2].name = "Mr Cherry";
            staff[2].sales = 300;
            staff[2].calculation_algorithm = standard_bonus;

            staff[3].name = "Mr Date";
            staff[3].sales = 100;
            staff[3].calculation_algorithm = enhanced_bonus;

            staff[4].name = "Ms Elderberry";
            staff[4].sales = 250;
            staff[4].calculation_algorithm = enhanced_bonus;

            // Расчет бонуса для всех объектов Employees
            foreach (Employee person in staff)
                PerformBonusCalculation(person);

            // Отображение подробных сведений обо всех объектах Employees
            foreach (Employee person in staff)
                DisplayPersonDetails(person);


        }

        public static void PerformBonusCalculation(Employee person)
        {

            // Этот метод применяет делегата, сохраненного в персональном объекте,
            // для выполнения расчета.
            // Примечание: этому методу известна локальная переменная коэффициента, несмотря на то, что
            // переменная находится вне области действия метода.
            // Переменная коэффициента -- это "зафиксированная внешняя переменная".
            person.bonus = person.calculation_algorithm(person.sales);
        }

        public static void DisplayPersonDetails(Employee person)
        {
            Console.WriteLine(person.name);
            Console.WriteLine(person.bonus);
            Console.WriteLine("---------------");
        }
    }
}


