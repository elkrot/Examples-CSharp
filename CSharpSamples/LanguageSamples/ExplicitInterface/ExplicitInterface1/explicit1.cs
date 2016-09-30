// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// explicit1.cs
interface IDimensions 
{
   float Length();
   float Width();
}

class Box : IDimensions 
{
   float lengthInches;
   float widthInches;

   public Box(float length, float width) 
   {
      lengthInches = length;
      widthInches = width;
   }
   // Реализация явного члена интерфейса: 
   float IDimensions.Length() 
   {
      return lengthInches;
   }
   // Реализация явного члена интерфейса:
   float IDimensions.Width() 
   {
      return widthInches;      
   }

   public static void Main() 
   {
      // Объявление экземпляра класса "myBox":
      Box myBox = new Box(30.0f, 20.0f);
      // Объявление экземпляра интерфейса "myDimensions":
      IDimensions myDimensions = (IDimensions) myBox;
      // Печать размеров окна:
      /* Следующие строки комментариев  вызовут ошибки          компиляции, поскольку они пытаются получить доступ к реализованному явным образом         члену интерфейса из экземпляра класса:                   */
      //System.Console.WriteLine("Length: {0}", myBox.Length());
      //System.Console.WriteLine("Width: {0}", myBox.Width());
      /* Печать размеров при помощи вызова методов          из экземпляра интерфейса:                         */
      System.Console.WriteLine("Length: {0}", myDimensions.Length());
      System.Console.WriteLine("Width: {0}", myDimensions.Width());
   }
}

