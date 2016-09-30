// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// shapes.cs
// компиляция с  /target:library /reference:abstractshape.dll
public class Square : Shape
{
   private int mySide;

   public Square(int side, string id) : base(id)
   {
      mySide = side;
   }

   public override double Area
   {
      get
      {
         // Возвращает площадь квадрата по заданной стороне:
         return mySide * mySide;
      }
   }
}

public class Circle : Shape
{
   private int myRadius;

   public Circle(int radius, string id) : base(id)
   {
      myRadius = radius;
   }

   public override double Area
   {
      get
      {
         // Возвращает площадь круга по заданному радиусу:
         return myRadius * myRadius * System.Math.PI;
      }
   }
}

public class Rectangle : Shape
{
   private int myWidth;
   private int myHeight;

   public Rectangle(int width, int height, string id) : base(id)
   {
      myWidth  = width;
      myHeight = height;
   }

   public override double Area
   {
      get
      {
         // Возвращает площадь прямоугольника по заданной ширине и высоте:
         return myWidth * myHeight;
      }
   }
}

