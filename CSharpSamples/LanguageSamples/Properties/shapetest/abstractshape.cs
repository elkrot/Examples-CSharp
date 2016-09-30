// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// abstractshape.cs
// компилировать с /target:library
// csc /target:library abstractshape.cs
using System;

public abstract class Shape
{
   private string myId;

   public Shape(string s)
   {
      Id = s;   // вызов метода доступа SET свойства Id
   }

   public string Id
   {
      get 
      {
         return myId;
      }

      set
      {
         myId = value;
      }
   }

   // Area -- это свойство, доступное только для чтения с единственно необходимым методом доступа GET:
   public abstract double Area
   {
      get;
   }

   public override string ToString()
   {
      return Id + " Area = " + string.Format("{0:F2}",Area);
   }
}

