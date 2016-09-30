// -----------------------------------------------------------------------
// <copyright file="FactoryMethod.cs" company="Co">
// Copyright Kolodiyazjny Sergey
// </copyright>
// -----------------------------------------------------------------------

/*
 Фабричный метод (Factory Method) - это паттерн, который определяет интерфейс для создания объектов некоторого класса, 
 но непосредственное решение о том, объект какого класса создавать происходит в подклассах. То есть паттерн предполагает, 
 что базовый класс делегирует создание объектов классам-наследникам.
    
    Когда заранее неизвестно, объекты каких типов необходимо создавать

    Когда система должна быть независимой от процесса создания новых объектов и расширяемой: в нее можно легко вводить новые классы, объекты которых система должна создавать.

    Когда создание новых объектов необходимо делегировать из базового класса классам наследникам
 */

namespace Patterns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    // абстрактный класс строительной компании
    abstract class Developer
    {
        public string Name { get; set; }

        public Developer(string n)
        {
            Name = n;
        }
        // фабричный метод
        abstract public House Create();
    }
    // строит панельные дома
    class PanelDeveloper : Developer
    {
        public PanelDeveloper(string n)
            : base(n)
        { }

        public override House Create()
        {
            return new PanelHouse();
        }
    }
    // строит деревянные дома
    class WoodDeveloper : Developer
    {
        public WoodDeveloper(string n)
            : base(n)
        { }

        public override House Create()
        {
            return new WoodHouse();
        }
    }

    abstract class House
    { }

    class PanelHouse : House
    {
        public PanelHouse()
        {
            Console.WriteLine("Панельный дом построен");
        }
    }
    class WoodHouse : House
    {
        public WoodHouse()
        {
            Console.WriteLine("Деревянный дом построен");
        }
    }
}
