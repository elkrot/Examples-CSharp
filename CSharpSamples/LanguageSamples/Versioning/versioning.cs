// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// versioning.cs
// Ожидается CS0114
public class MyBase 
{
   public virtual string Meth1() 
   {
      return "MyBase-Meth1";
   }
   public virtual string Meth2() 
   {
      return "MyBase-Meth2";
   }
   public virtual string Meth3() 
   {
      return "MyBase-Meth3";
   }
}

class MyDerived : MyBase 
{
   // Переопределение виртуального метода Meth1 при помощи ключевого слова "override":
   public override string Meth1() 
   {
      return "MyDerived-Meth1";
   }
   // Явное сокрытие виртуального метода Meth2 при помощи нового
   // ключевого слова:
   public new string Meth2() 
   {
      return "MyDerived-Meth2";
   }
   // Поскольку в следующем объявлении нет ни одного ключевого слова,
   // программисту будет выдано предупреждение о том, что 
   // метод скрывает унаследованный член MyBase.Meth3():
   public string Meth3() 
   {
      return "MyDerived-Meth3";
   }

   public static void Main() 
   {
      MyDerived mD = new MyDerived();
      MyBase mB = (MyBase) mD;

      System.Console.WriteLine(mB.Meth1());
      System.Console.WriteLine(mB.Meth2());
      System.Console.WriteLine(mB.Meth3());
   }
}

