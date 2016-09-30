// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// shapetest.cs
// компиляция с  /reference:abstractshape.dll;shapes.dll
public class TestClass
{
   public static void Main()
   {
      Shape[] shapes =
         {
            new Square(5, "Square #1"),
            new Circle(3, "Circle #1"),
            new Rectangle( 4, 5, "Rectangle #1")
         };
      
      System.Console.WriteLine("Shapes Collection");
      foreach(Shape s in shapes)
      {
         System.Console.WriteLine(s);
      }
         
   }
}

