// -----------------------------------------------------------------------
// <copyright file="Flyweight.cs" company="Co">
// Copyright Kolodiyazjny Sergey
// </copyright>
// -----------------------------------------------------------------------
/*
 Паттерн Приспособленец (Flyweight) - структурный шаблон проектирования, который позволяет использовать разделяемые 
 объекты сразу в нескольких контекстах. Данный паттерн используется преимущественно для оптимизации работы с памятью.
 */
namespace Patterns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    abstract class House
    {
        protected int stages; // количество этажей

        public abstract void Build(double longitude, double latitude);
    }

    class PanelHousex : House
    {
        public PanelHousex()
        {
            stages = 16;
        }

        public override void Build(double longitude, double latitude)
        {
            Console.WriteLine("Построен панельный дом из 16 этажей; координаты: {0} широты и {1} долготы",
                latitude, longitude);
        }
    }
    class BrickHouse : House
    {
        public BrickHouse()
        {
            stages = 5;
        }

        public override void Build(double longitude, double latitude)
        {
            Console.WriteLine("Построен кирпичный дом из 5 этажей; координаты: {0} широты и {1} долготы",
                latitude, longitude);
        }
    }

    class HouseFactory
    {
        Dictionary<string, House> houses = new Dictionary<string, House>();
        public HouseFactory()
        {
            houses.Add("Panel", new PanelHousex());
            houses.Add("Brick", new BrickHouse());
        }

        public House GetHouse(string key)
        {
            if (houses.ContainsKey(key))
                return houses[key];
            else
                return null;
        }
    }
}
