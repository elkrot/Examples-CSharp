// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// explicit2.cs
// Объявление интерфейса английской системы мер:
interface IEnglishDimensions 
{
   float Length();
   float Width();
}
// Объявление интерфейса метрической системы мер:
interface IMetricDimensions 
{
   float Length();
   float Width();
}
// Объявление класса "Box", реализующего два интерфейса:
// IEnglishDimensions и IMetricDimensions:
class Box : IEnglishDimensions, IMetricDimensions 
{
   float lengthInches;
   float widthInches;
   public Box(float length, float width) 
   {
      lengthInches = length;
      widthInches = width;
   }
// Явная реализация членов интерфейса IEnglishDimensions:
   float IEnglishDimensions.Length() 
   {
      return lengthInches;
   }
   float IEnglishDimensions.Width() 
   {
      return widthInches;      
   }
// Явная реализация членов интерфейса IMetricDimensions:
   float IMetricDimensions.Length() 
   {
      return lengthInches * 2.54f;
   }
   float IMetricDimensions.Width() 
   {
      return widthInches * 2.54f;
   }
   public static void Main() 
   {
      // Объявление экземпляра класса "myBox":
      Box myBox = new Box(30.0f, 20.0f);
      // Объявление экземпляра интерфейса английской системы мер:
      IEnglishDimensions eDimensions = (IEnglishDimensions) myBox;
      // Объявление экземпляра интерфейса метрической системы мер:
      IMetricDimensions mDimensions = (IMetricDimensions) myBox;
      // Печать размеров в единицах английской системы мер:
      System.Console.WriteLine("Length(in): {0}", eDimensions.Length());
      System.Console.WriteLine("Width (in): {0}", eDimensions.Width());
      // Печать размеров в единицах метрической системы мер:
      System.Console.WriteLine("Length(cm): {0}", mDimensions.Length());
      System.Console.WriteLine("Width (cm): {0}", mDimensions.Width());
   }
}

