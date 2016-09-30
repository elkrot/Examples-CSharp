// -----------------------------------------------------------------------
// <copyright file="Facade.cs" company="Co">
// Copyright Kolodiyazjny Sergey
// </copyright>
// -----------------------------------------------------------------------

/*
 Фасад (Facade) представляет шаблон проектирования, который позволяет скрыть сложность системы с помощью
  предоставления упрощенного интерфейса для взаимодействия с ней.
Когда использовать фасад?

    Когда имеется сложная система, и необходимо упростить с ней работу. Фасад позволит определить одну 
  точку взаимодействия между клиентом и системой.

    Когда надо уменьшить количество зависимостей между клиентом и сложной системой. Фасадные объекты
  позволяют отделить, изолировать компоненты системы от клиента и развивать и работать с ними независимо.

    Когда нужно определить подсистемы компонентов в сложной системе. Создание фасадов для компонентов
  каждой отдельной подсистемы позволит упростить взаимодействие между ними и повысить их независимость друг от друга.

 */
namespace Patterns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    // текстовый редактор
    class TextEditor
    {
        public void CreateCode()
        {
            Console.WriteLine("Написание кода");
        }
        public void Save()
        {
            Console.WriteLine("Сохранение кода");
        }
    }
    class Compiller
    {
        public void Compile()
        {
            Console.WriteLine("Компиляция приложения");
        }
    }
    class CLR
    {
        public void Execute()
        {
            Console.WriteLine("Выполнение приложения");
        }
        public void Finish()
        {
            Console.WriteLine("Завершение работы приложения");
        }
    }

    class VisualStudioFacade
    {
        TextEditor textEditor;
        Compiller compiller;
        CLR clr;
        public VisualStudioFacade(TextEditor te, Compiller compil, CLR clr)
        {
            this.textEditor = te;
            this.compiller = compil;
            this.clr = clr;
        }
        public void Start()
        {
            textEditor.CreateCode();
            textEditor.Save();
            compiller.Compile();
            clr.Execute();
        }
        public void Stop()
        {
            clr.Finish();
        }
    }

    class Programmer
    {
        public void CreateApplication(VisualStudioFacade facade)
        {
            facade.Start();
            facade.Stop();
        }
    }
}
