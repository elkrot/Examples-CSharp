// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// complex.cs
using System;

public struct Complex 
{
   public int real;
   public int imaginary;

   public Complex(int real, int imaginary) 
   {
      this.real = real;
      this.imaginary = imaginary;
   }

   // Объявление оператора для перегрузки (+), типов, 
   // которые можно добавить (два объекта Complex), и 
   // return type (Complex):
   public static Complex operator +(Complex c1, Complex c2) 
   {
      return new Complex(c1.real + c2.real, c1.imaginary + c2.imaginary);
   }
   // Переопределение метода ToString для отображения комплексного числа в подходящем формате:
   public override string ToString()
   {
      return(String.Format("{0} + {1}i", real, imaginary));
   }

   public static void Main() 
   {
      Complex num1 = new Complex(2,3);
      Complex num2 = new Complex(3,4);

      // Добавление двух объектов Complex (num1 и num2) при помощи
      // перегруженного оператора "плюс"
      Complex sum = num1 + num2;

     // Печать чисел и суммы с использованием переопределенного метода ToString:
      Console.WriteLine("First complex number:  {0}",num1);
      Console.WriteLine("Second complex number: {0}",num2);
      Console.WriteLine("The sum of the two numbers: {0}",sum);
 
   }
}

