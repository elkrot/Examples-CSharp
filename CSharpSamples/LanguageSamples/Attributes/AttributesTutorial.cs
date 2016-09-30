// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// AttributesTutorial.cs
// Пример использования атрибутов класса и метода.

using System;
using System.Reflection;
using System.Collections;

// Класс IsTested -- это определенный пользователем  класс особых атрибутов.
// Его можно применить к любому объявлению, включая
//  - типы (структура, класс, перечисление, делегат)
//  - члены (методы, поля, события, свойства, индексаторы)
// Он используется без аргументов.
public class IsTestedAttribute : Attribute
{
    public override string ToString()
    {
        return "Is Tested";
    }
}

// Класс AuthorAttribute -- это определенный пользователем класс атрибута.
// Его можно применить только к объявлениям классов и структур.
// Он принимает один неименованный строковый аргумент (имя автора).
// У него есть один необязательный именованный аргумент Version, тип которого -- int.
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class AuthorAttribute : Attribute
{
    // Этот конструктор определяет неименованные аргументы для класса атрибута.
    public AuthorAttribute(string name)
    {
        this.name = name;
        this.version = 0;
    }

    // Это свойство доступно только для чтения (у него нет метода доступа SET),
    // поэтому его нельзя применять к данному атрибуту в качестве именованного аргумента.
    public string Name 
    {
        get 
        {
            return name;
        }
    }

    // Это свойство доступно для чтения и записи (у него есть метод доступа SET),
    // поэтому его можно использовать как именованный аргумент, когда данный
    // класс применяется как класс атрибута.
    public int Version
    {
        get 
        {
            return version;
        }
        set 
        {
            version = value;
        }
    }

    public override string ToString()
    {
        string value = "Author : " + Name;
        if (version != 0)
        {
            value += " Version : " + Version.ToString();
        }
        return value;
    }

    private string name;
    private int version;
}

// Здесь выполняется присоединение определенного пользователем атрибута AuthorAttribute к 
// классу Account. Неименованный строковый аргумент передается в 
// конструктор класса AuthorAttribute при создании атрибутов.
[Author("Joe Programmer")]
class Account
{
    // Присоединение пользовательского атрибута IsTestedAttribute к данному методу.
    [IsTested]
    public void AddOrder(Order orderToAdd)
    {
        orders.Add(orderToAdd);
    }

    private ArrayList orders = new ArrayList();
}

// Присоединение пользовательских атрибутов AuthorAttribute и IsTestedAttribute 
// к данному классу.
// Обратите внимание на использование именованного аргумента 'Version' для AuthorAttribute.
[Author("Jane Programmer", Version = 2), IsTested()]
class Order
{
    // сюда можно добавить нужные материалы ...
}

class MainClass
{
   private static bool IsMemberTested(MemberInfo member)
   {
        foreach (object attribute in member.GetCustomAttributes(true))
        {
            if (attribute is IsTestedAttribute)
            {
               return true;
            }
        }
      return false;
   }

    private static void DumpAttributes(MemberInfo member)
    {
        Console.WriteLine("Attributes for : " + member.Name);
        foreach (object attribute in member.GetCustomAttributes(true))
        {
            Console.WriteLine(attribute);
        }
    }

    public static void Main()
    {
        // отображение атрибутов для класса Account
        DumpAttributes(typeof(Account));

        // отображение списка протестированных членов
        foreach (MethodInfo method in (typeof(Account)).GetMethods())
        {
            if (IsMemberTested(method))
            {
               Console.WriteLine("Member {0} is tested!", method.Name);
            }
            else
            {
               Console.WriteLine("Member {0} is NOT tested!", method.Name);
            }
        }
        Console.WriteLine();

        // отображение атрибутов для класса Order
        DumpAttributes(typeof(Order));

        // отображение атрибутов для методов класса Order
        foreach (MethodInfo method in (typeof(Order)).GetMethods())
        {
           if (IsMemberTested(method))
           {
               Console.WriteLine("Member {0} is tested!", method.Name);
           }
           else
           {
               Console.WriteLine("Member {0} is NOT tested!", method.Name);
           }
        }
        Console.WriteLine();
    }
}


