﻿// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

static class Samples {
    static int[] numbers = new [] { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
    static string[] strings = new [] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    
    class Person {
      public string Name {get; set;}
      public int Level {get; set;}
    }
    
    static Person[] persons = new Person[] {
        new Person {Name="Jesper", Level=3},
        new Person {Name="Lene", Level=3},
        new Person {Name="Jonathan", Level=5},
        new Person {Name="Sagiv", Level=3},
        new Person {Name="Jacqueline", Level=3},
        new Person {Name="Ellen", Level=3},
        new Person {Name="Gustavo", Level=9}
        };
    
    public static void Sample1() {
        // использовать Where() для нахождения элементов, удовлетворяющих условию       
        var fnums = numbers.Where(n => n < 5);
    
        Console.WriteLine("Numbers < 5");
        foreach(int x in fnums) {
            Console.WriteLine(x);
        }
    }    

    public static void Sample2() {
        // использовать First() для обнаружения одного элемента, удовлетворяющего определенному условию       
        string v = strings.First(s => s[0] == 'o');
    
        Console.WriteLine("string starting with 'o': {0}", v);
    }        
    
    public static void Sample3() {
        // использовать Select() для преобразования каждого элемента в новое значение
        var snums = numbers.Select(n => strings[n]);
        
        Console.WriteLine("Numbers");
        foreach(string s in snums) {
            Console.WriteLine(s);
        }           
    }
    
    public static void Sample4()
    {
        // использовать конструкторы анонимных типов для создания результатов с несколькими значениями во всплывающем окне
        var q = strings.Select(s => new {Head = s.Substring(0,1), Tail = s.Substring(1)});
        foreach(var p in q) {
            Console.WriteLine("Head = {0}, Tail = {1}", p.Head, p.Tail);
        }
    }
    
    public static void Sample5() { 
        // Объединить Select() и Where() для создания полного запроса
        var q = numbers.Where(n => n < 5).Select(n => strings[n]);
        
        Console.WriteLine("Numbers < 5");
        foreach(var x in q) {
            Console.WriteLine(x);
        }       
    }
    
    public static void Sample6() {
        // Операторы последовательности, образующие запросы первого рода, не будут выполняться до тех пор, пока не будут перечислены.
        int i = 0;
        var q = numbers.Select(n => ++i);
        // Необходимо учесть, что локальная переменная i не увеличивается, пока не вычислены все элементы (как побочный эффект).
        foreach(var v in q) {
          Console.WriteLine("v = {0}, i = {1}", v, i);          
        }
        Console.WriteLine();
        
        // Методы, подобные ToList(), вызывают немедленное выполнение запроса и кэширование результатов
        int i2 = 0;
        var q2 = numbers.Select(n => ++i2).ToList();
        // Локальная переменная i2 получила полное приращение еще до перехода к итерации результатов
        foreach(var v in q2) {
          Console.WriteLine("v = {0}, i2 = {1}", v, i2);
        }        
    }
    
    public static void Sample7() {
        // использовать GroupBy(), для построения разделов групп из подобных элементов
        var q = strings.GroupBy(s => s[0]); // <- группировка по первому знаку в каждой строке
        
        foreach(var g in q) {
          Console.WriteLine("Group: {0}", g.Key);
          foreach(string v in g) {
            Console.WriteLine("\tValue: {0}", v);
          }
        }
    }
    
    public static void Sample8() {
        // использовать GroupBy() и выполнить агрегирование, например Count(), Min(), Max(), Sum(), Average(), для вычисления значений в разделе
        var q = strings.GroupBy(s => s[0]).Select(g => new {FirstChar = g.Key, Count = g.Count()});
        
        foreach(var v in q) {
          Console.WriteLine("There are {0} string(s) starting with the letter {1}", v.Count, v.FirstChar);
        }
    }
    
    public static void Sample9() {
        // использовать OrderBy()/OrderByDescending() для определения порядка вывода результатов
        var q = strings.OrderBy(s => s);  // упорядочить строки по именам
        
        foreach(string s in q) {
          Console.WriteLine(s);
        }
    }
    
    public static void Sample9a() {  
        // использовать ThenBy()/ThenByDescending() для получения дополнительных сведений о порядке
        var q = persons.OrderBy(p => p.Level).ThenBy(p => p.Name);
        
        foreach(var p in q) {
          Console.WriteLine("{0}  {1}", p.Level, p.Name);
        }
    }
    
    public static void Sample10() {
        // использовать выражения запроса для упрощения
        var q = from p in persons 
                orderby p.Level, p.Name
                group p.Name by p.Level into g
                select new {Level = g.Key, Persons = g};
        
        foreach(var g in q) {
            Console.WriteLine("Level: {0}", g.Level);
            foreach(var p in g.Persons) {
                Console.WriteLine("Person: {0}", p);
            }
        }                
    }
}
