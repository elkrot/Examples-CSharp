// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserInterface.AdventureWorksService;


namespace UserInterface.Gateways
{
    public interface IProductGateway
    {
        IList<Product> GetProducts(string productName, ProductCategory category);
        IList<ProductCategory> GetCategories();
        string DeleteProduct(Product product);
        void UpdateProduct(Product product);
        void AddProduct(Product product);
    }
}
