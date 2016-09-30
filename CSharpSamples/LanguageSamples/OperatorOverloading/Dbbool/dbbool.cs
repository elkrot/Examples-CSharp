// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// dbbool.cs
using System;

public struct DBBool
{
   // Три возможных значения DBBool:
   public static readonly DBBool dbNull = new DBBool(0);
   public static readonly DBBool dbFalse = new DBBool(-1);
   public static readonly DBBool dbTrue = new DBBool(1);
   // Частное поле, в котором хранятся значения -1, 0, 1 для dbFalse, dbNull, dbTrue:
   int value; 

   // Закрытый конструктор. Значение параметра должно быть -1, 0 или 1:
   DBBool(int value) 
   {
      this.value = value;
   }

   // Неявное преобразование типа bool в DBBool. Сопоставление true с 
   // DBBool.dbTrue, а false -- с DBBool.dbFalse:
   public static implicit operator DBBool(bool x) 
   {
      return x? dbTrue: dbFalse;
   }

   // Явное преобразование типа DBBool в bool. Выдача 
   // исключения, если данный DBBool -- это dbNull, иначе -- возврат
   // true или false:
   public static explicit operator bool(DBBool x) 
   {
      if (x.value == 0) throw new InvalidOperationException();
      return x.value > 0;
   }

   // Оператор равенства. Возвращает dbNull, если оба операнда -- dbNull, 
   // иначе возвращает dbTrue или dbFalse:
   public static DBBool operator ==(DBBool x, DBBool y) 
   {
      if (x.value == 0 || y.value == 0) return dbNull;
      return x.value == y.value? dbTrue: dbFalse;
   }

   // Оператор неравенства. Возвращает dbNull, если оба операнда -
   // dbNull, иначе возвращает dbTrue или dbFalse:
   public static DBBool operator !=(DBBool x, DBBool y) 
   {
      if (x.value == 0 || y.value == 0) return dbNull;
      return x.value != y.value? dbTrue: dbFalse;
   }

   // Оператор логического отрицания. Возвращает dbTrue, если операнд -
   // dbFalse; dbNull, если операнд -- dbNull, и dbFalse, если
   // операнд -- dbTrue:
   public static DBBool operator !(DBBool x) 
   {
      return new DBBool(-x.value);
   }

   // Логический оператор И. Возвращает dbFalse, если оба операнда -
   // dbFalse; dbNull, если оба операнда -- dbNull, иначе -- dbTrue:
   public static DBBool operator &(DBBool x, DBBool y) 
   {
      return new DBBool(x.value < y.value? x.value: y.value);
   }

   // Логический оператор ИЛИ. Возвращает dbTrue, если оба операнда -
   // dbTrue; dbNull, если оба операнда -- dbNull, иначе -- dbFalse:
   public static DBBool operator |(DBBool x, DBBool y) 
   {
      return new DBBool(x.value > y.value? x.value: y.value);
   }

   // Оператор подтверждения истинности. Возвращает true, если операнд -
   // dbTrue, иначе -- false:
   public static bool operator true(DBBool x) 
   {
      return x.value > 0;
   }

   // Оператор подтверждения ложности. Возвращает true, если операнд -
   // dbFalse, иначе -- false:
   public static bool operator false(DBBool x) 
   {
      return x.value < 0;
   }

   // Перегрузка преобразования типа DBBool в строку:
   public static implicit operator string(DBBool x) 
   {
      return x.value > 0 ? "dbTrue"
           : x.value < 0 ? "dbFalse"
           : "dbNull";
   }

   // Переопределение метода Object.Equals(object o):
   public override bool Equals(object o) 
   {
      try 
      {
         return (bool) (this == (DBBool) o);
      }
      catch 
      {
         return false;
      }
   }

   // Переопределение метода Object.GetHashCode():
   public override int GetHashCode() 
   {
      return value;
   }

   // Переопределение метода ToString для преобразования типа DBBool в строку:
   public override string ToString() 
   {
      switch (value) 
      {
         case -1:
            return "DBBool.False";
         case 0:
            return "DBBool.Null";
         case 1:
            return "DBBool.True";
         default:
            throw new InvalidOperationException();
      }
   }
}

class Test 
{
   static void Main() 
   {
      DBBool a, b;
      a = DBBool.dbTrue;
      b = DBBool.dbNull;

      Console.WriteLine( "!{0} = {1}", a, !a);
      Console.WriteLine( "!{0} = {1}", b, !b);
      Console.WriteLine( "{0} & {1} = {2}", a, b, a & b);
      Console.WriteLine( "{0} | {1} = {2}", a, b, a | b);
      // Вызов оператора true, чтобы определить логическое 
      // значение переменной с типом DBBool:
      if (b)
         Console.WriteLine("b is definitely true");
      else
         Console.WriteLine("b is not definitely true");   
   }
}

