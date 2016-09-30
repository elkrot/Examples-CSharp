// -----------------------------------------------------------------------
// <copyright file="Strategy.cs" company="Co">
// Copyright Kolodiyazjny Sergey
// </copyright>
// -----------------------------------------------------------------------
/*
 
 Паттерн Стратегия (Strategy) представляет шаблон проектирования, который определяет набор алгоритмов, 
 инкапсулирует каждый из них и обеспечивает их взаимозаменяемость. В зависимости от ситуации мы можем легко заменить 
 один используемый алгоритм другим. При этом замена алгоритма происходит независимо от объекта, который использует данный алгоритм.

 Когда использовать стратегию?

   Когда есть несколько родственных классов, которые отличаются поведением. Можно задать один основной класс, а разные варианты поведения 
   вынести в отдельные классы и при необходимости их применять

    Когда необходимо обеспечить выбор из нескольких вариантов алгоритмов, которые можно легко менять в зависимости от условий

    Когда необходимо менять поведение объектов на стадии выполнения программы

    Когда класс, применяющий определенную функциональность, ничего не должен знать о ее реализации
 
 */
namespace Patterns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    interface IMovable
    {
        void Move();
    }

    class PetrolMove : IMovable
    {
        public void Move()
        {
            Console.WriteLine("Перемещение на бензине");
        }
    }

    class ElectricMove : IMovable
    {
        public void Move()
        {
            Console.WriteLine("Перемещение на электричестве");
        }
    }
    class Car
    {
        protected int passengers; // кол-во пассажиров
        protected string model; // модель автомобиля

        public Car(int num, string model, IMovable mov)
        {
            this.passengers = num;
            this.model = model;
            Movable = mov;
        }
        public IMovable Movable { private get; set; }
        public void Move()
        {
            Movable.Move();
        }
    }
}
