// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// XMLsample.cs
// компиляция с  /doc:XMLsample.xml
using System;

/// <summary>
/// Здесь сводная документация об уровне класса.</summary>
/// <remarks>
/// Расширенные комментарии можно связать с типом или членом 
/// при помощи тега remarks</remarks>
public class SomeClass
{
   /// <summary>
   /// Хранилище для свойства имени</summary>
   private string myName = null;

   /// <summary>
   /// Конструктор класса. </summary>
   public SomeClass()
   {
       // TODO: добавьте логику конструктора
   }
   
   /// <summary>
   /// Свойство имени </summary>
   /// <value>
   /// Тег value используется для описания значения свойства</value>
   public string Name
   {
      get 
      {
         if ( myName == null )
         {
            throw new Exception("Name is null");
         }
             
         return myName;
      }
   }

   /// <summary>
   /// Описание метода SomeMethod.</summary>
   /// <param name="s"> Здесь описание параметра s</param>
   /// <seealso cref="String">
   /// Атрибут cref можно использовать в любом теге  для ссылки на тип или член, 
   /// и тогда компилятор проверит существование ссылки. </seealso>
   public void SomeMethod(string s)
   {
   }

   /// <summary>
   /// Какой-либо другой метод. </summary>
   /// <returns>
   /// Возврат результатов, описанных при помощи тега returns.</returns>
   /// <seealso cref="SomeMethod(string)">
   /// Обратите внимание, что атрибут cref используется для ссылки на указанный метод </seealso>
   public int SomeOtherMethod()
   {
      return 0;
   }

   /// <summary>
   /// Точка входа для приложения.
   /// </summary>
   /// <param name="args"> Список аргументов командной строки</param>
   public static int Main(String[] args)
   {
      // TODO: добавьте сюда код для запуска приложения

       return 0;
   }
}

