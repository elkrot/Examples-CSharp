// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// person.cs
using System;
class Person
{
    private string myName ="N/A";
    private int myAge = 0;

    // Объявление свойства Name с типом string:
    public string Name
    {
        get 
        {
           return myName; 
        }
        set 
        {
           myName = value; 
        }
    }

    // Объявление свойства Age с типом int:
    public int Age
    {
        get 
        { 
           return myAge; 
        }
        set 
        { 
           myAge = value; 
        }
    }

    public override string ToString()
    {
        return "Name = " + Name + ", Age = " + Age;
    }

    public static void Main()
    {
        Console.WriteLine("Simple Properties");

        // Создание нового объекта Person:
        Person person = new Person();

        // Печать значений имени и возраста, связанных с данным объектом:
        Console.WriteLine("Person details - {0}", person);

        // Задание некоторых значений объекта Рerson:
        person.Name = "Joe";
        person.Age = 99;
        Console.WriteLine("Person details - {0}", person);

        // Увеличение значения свойства Age:
        person.Age += 1;
        Console.WriteLine("Person details - {0}", person);
    }
}

