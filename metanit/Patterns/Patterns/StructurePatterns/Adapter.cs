// -----------------------------------------------------------------------
// <copyright file="Adapter.cs" company="Co">
// Copyright Kolodiyazjny Sergey
// </copyright>
// -----------------------------------------------------------------------
/*
 Паттерн Адаптер (Adapter) предназначен для преобразования интерфейса одного класса в интерфейс другого. 
 Благодаря реализации данного паттерна мы можем использовать вместе классы с несовместимыми интерфейсами.

    Когда необходимо использовать имеющийся класс, но его интерфейс не соответствует потребностям

    Когда надо использовать уже существующий класс совместно с другими классами, интерфейсы которых не совместимы

 */
namespace Patterns.Adapter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    interface ITransport
    {
        void Drive();
    }
    // класс машины
    class Auto : ITransport
    {
        public void Drive()
        {
            Console.WriteLine("Машина едет по дороге");
        }
    }
    class Driver
    {
        public void Travel(ITransport transport)
        {
            transport.Drive();
        }
    }
    // интерфейс животного
    interface IAnimal
    {
        void Move();
    }
    // класс верблюда
    class Camel : IAnimal
    {
        public void Move()
        {
            Console.WriteLine("Верблюд идет по пескам пустыни");
        }
    }
    // Адаптер от Camel к ITransport
    class CamelToTransportAdapter : ITransport
    {
        Camel camel;
        public CamelToTransportAdapter(Camel c)
        {
            camel = c;
        }

        public void Drive()
        {
            camel.Move();
        }
    }
}
