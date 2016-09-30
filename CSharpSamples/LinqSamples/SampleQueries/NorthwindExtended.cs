// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//------------------------------------------------------------------------------
//Класс Partial, расширяющий существующий класс Northwind.
//------------------------------------------------------------------------------

namespace nwind {
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using System.Data;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;
    using System.Linq.Expressions;
    using System.ComponentModel;
    using System;

    public partial class Northwind {


        // Для примера переопределения CUD
        partial void InsertRegion(Region instance)
        {
            // Этот partial-метод вызывает ExecuteDynamicInsert для вставки экземпляра Region.
            // Вместо метода  ExecuteDynameicInsert здесь можно вызвать хранимую процедуру, 
            // передать ей параметры и вставить экземпляр в таблицу.
            Console.WriteLine("***** Executing InsertRegion Override ******");
            Console.WriteLine("Calling up ExecuteDynamicInsert on a Region instance");
            this.ExecuteDynamicInsert(instance);
        }

        // Для примера переопределения загрузки
        private IEnumerable<Product> LoadProducts(Category category)
        {
            // Этот partial-метод вызывает запрос LinqToSql, чтобы загрузить продукты для категории
            // Вместо запроса LinqToSQL для загрузки продуктов здесь также можно вызвать хранимую процедуру 
            Console.WriteLine("******** Using LinqToSQL query to load products belong to category that are not discontinued. ******");
            return this.Products.Where(p => p.CategoryID == category.CategoryID).Where(p=>!p.Discontinued);
        }



    }
    // Для расширяемого partial-метода
    public partial class Order {

        [System.Diagnostics.DebuggerNonUserCode]
        partial void OnValidate(System.Data.Linq.ChangeAction action)
        {
            switch (action)
            {
                case ChangeAction.Delete:
                    break;
                case ChangeAction.Insert:
                    break;
                case ChangeAction.Update:
                    if (this.ShipVia > 100)
                        throw new Exception("Exception: ShipVia cannot be bigger than 100");
                    break;
                case ChangeAction.None:
                    break;

                default:
                    break;
            }

        }
    }
}
