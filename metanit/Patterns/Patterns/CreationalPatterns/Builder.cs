// -----------------------------------------------------------------------
// <copyright file="Builder.cs" company="Co">
// Copyright Kolodiyazjny Sergey
// </copyright>
// -----------------------------------------------------------------------

/*
 Строитель (Builder) - шаблон проектирования, который инкапсулирует создание объекта и позволяет разделить его на различные этапы.
Когда использовать паттерн Строитель?

    Когда процесс создания нового объекта не должен зависеть от того, из каких частей этот объект состоит и как эти части связаны между собой

    Когда необходимо обеспечить получение различных вариаций объекта в процессе его создания

 
 */
namespace Patterns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    //мука
    class Flour
    {
        // какого сорта мука
        public string Sort { get; set; }
    }
    // соль
    class Salt
    { }
    // пищевые добавки
    class Additives
    {
        public string Name { get; set; }
    }

    class Bread
    {
        // пшеничная мука
        public Flour WheatFlour { get; set; }
        // ржаная мука
        public Flour RyeFlour { get; set; }
        // соль
        public Salt Salt { get; set; }
        // пищевые добавки
        public Additives Additives { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (WheatFlour != null)
                sb.Append("Пшеничная мука " + WheatFlour.Sort + "\n");
            if (RyeFlour != null)
                sb.Append("Ржаная мука " + RyeFlour.Sort + " \n");
            if (Salt != null)
                sb.Append("Соль \n");
            if (Additives != null)
                sb.Append("Добавки: " + Additives.Name + " \n");
            return sb.ToString();
        }
    }
    // абстрактный класс строителя
    abstract class BreadBuilder
    {
        public Bread Bread { get; private set; }
        public BreadBuilder()
        {
            Bread = new Bread();
        }
        public abstract void SetWheatFlour();
        public abstract void SetRyeFlour();
        public abstract void SetSalt();
        public abstract void SetAdditives();
    }
    // пекарь
    class Baker
    {
        public void Bake(BreadBuilder breadBuilder)
        {
            breadBuilder.SetWheatFlour();
            breadBuilder.SetRyeFlour();
            breadBuilder.SetSalt();
            breadBuilder.SetAdditives();
        }
    }
    // строитель для ржаного хлеба
    class RyeBreadBuilder : BreadBuilder
    {
        public override void SetWheatFlour()
        {
            // не используется
        }

        public override void SetRyeFlour()
        {
            this.Bread.RyeFlour = new Flour { Sort = "1 сорт" };
        }

        public override void SetSalt()
        {
            this.Bread.Salt = new Salt();
        }

        public override void SetAdditives()
        {
            // не используется
        }
    }
    // строитель для пшеничного хлеба
    class WheatBreadBuilder : BreadBuilder
    {
        public override void SetWheatFlour()
        {
            this.Bread.WheatFlour = new Flour { Sort = "высший сорт" };
        }

        public override void SetRyeFlour()
        {
            // не используется
        }

        public override void SetSalt()
        {
            this.Bread.Salt = new Salt();
        }

        public override void SetAdditives()
        {
            this.Bread.Additives = new Additives { Name = "улучшитель хлебопекарный" };
        }
    }
}
