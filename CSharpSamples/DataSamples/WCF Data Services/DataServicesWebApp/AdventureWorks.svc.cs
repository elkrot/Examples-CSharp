// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;

namespace DataServicesWebApp
{
    public class AdventureWorks : DataService<AdventureWorksLTEntities>
    {
        /// <summary>
        /// Этот метод вызывается только один раз для инициализации серверных политик.
        /// </summary>
        public static void InitializeService(IDataServiceConfiguration config)
        {
            // TODO: задайте правила, чтобы указать, какие наборы сущностей и служебные операции являются видимыми, обновляемыми и т.д.
            // Примеры:
            // config.SetEntitySetAccessRule("MyEntityset", EntitySetRights.AllRead);
            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);

            // Для целей тестирования используйте звездочку "*", чтобы отметить все служебные операции или наборы сущностей.
            // "*" НЕЛЬЗЯ использовать в рабочих системах.
            // В этом примере показаны только наборы сущностей, необходимые для строящегося приложения.
            // В этом примере используется EntitySetRight.All, разрешающий выполнение  чтения и записи в набор сущностей.
            config.SetEntitySetAccessRule("Products", EntitySetRights.All);
            config.SetEntitySetAccessRule("ProductCategories", EntitySetRights.All);
            config.SetEntitySetAccessRule("ProductDescriptions", EntitySetRights.All);
            config.SetEntitySetAccessRule("ProductModelProductDescriptions", EntitySetRights.All);
            config.SetEntitySetAccessRule("ProductModels", EntitySetRights.All);
        }
    }
}
