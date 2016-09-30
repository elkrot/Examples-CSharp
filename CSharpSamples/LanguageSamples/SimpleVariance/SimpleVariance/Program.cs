// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleVariance
{
    class Animal { }
    class Cat: Animal { }

    class Program
    {
        // Чтобы понять, что делают новые коды CoVariance и ContraVariance,
        // попробуйте удалить/добавить слова из/в следующие 2 строки кода:
        delegate T Func1<out T>();
        delegate void Action1<in T>(T a);
        
        static void Main(string[] args)
        {
            Func1<Cat> cat = () => new Cat();
            Func1<Animal> animal = cat;

            Action1<Animal> act1 = (ani) => { Console.WriteLine(ani); };
            Action1<Cat> cat1 = act1;

            Console.WriteLine(animal());
            cat1(new Cat());
        }

        
    }
}
