// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// events2.cs
using System;
namespace MyCollections 
{
   using System.Collections;

   // Класс, работающий аналогично ArrayList, но посылающий уведомления о событии
   // при любых изменениях списка. 
   public class ListWithChangedEvent: ArrayList 
   {
      // Событие, с помощью которого клиенты могут получать уведомления
      // о любых изменениях элементов списка.
      public event EventHandler Changed;

      // Вызов события Changed; вызывается при каждом изменении списка
      protected virtual void OnChanged(EventArgs e) 
      {
         if (Changed != null)
            Changed(this,e);
      }

      // Переопределение некоторых методов, которые могут изменить список;
      // вызов события после каждого:
      public override int Add(object value) 
      {
         int i = base.Add(value);
         OnChanged(EventArgs.Empty);
         return i;
      }

      public override void Clear() 
      {
         base.Clear();
         OnChanged(EventArgs.Empty);
      }

      public override object this[int index] 
      {
         set 
         {
            base[index] = value;
            OnChanged(EventArgs.Empty);
         }
      }
   }
}

namespace TestEvents 
{
   using MyCollections;

   class EventListener 
   {
      private ListWithChangedEvent List;

      public EventListener(ListWithChangedEvent list) 
      {
         List = list;
         // добавления "ListChanged" к событию Changed для "List".
         List.Changed += new EventHandler(ListChanged);
      }

      // Вызов будет выполняться при любых изменениях списка.
      private void ListChanged(object sender, EventArgs e) 
      {
         Console.WriteLine("This is called when the event fires.");
      }

      public void Detach() 
      {
         // Отсоединение события и удаление списка
         List.Changed -= new EventHandler(ListChanged);
         List = null;
      }
   }

   class Test 
   {
      // Тестирование класса ListWithChangedEvent.
      public static void Main() 
      {
      // Создание нового списка.
      ListWithChangedEvent list = new ListWithChangedEvent();

      // Создание класса, ожидающего событий изменения списка.
      EventListener listener = new EventListener(list);

      // Добавление и удаление элементов списка.
      list.Add("item 1");
      list.Clear();
      listener.Detach();
      }
   }
}

