// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Configuration;
using UserInterface.AdventureWorksService;
using System.Data.Services.Client;

namespace UserInterface.Gateways
{

    public class ProductGateway : IProductGateway
    {

        /// <summary>
        /// Объект DataServiceContext, представляющий контекст времени выполнения для службы данных.
        /// </summary>
        AdventureWorksLTEntities context;

        /// <summary>
        /// URI, обозначающий служебную точку входа
        /// </summary>
        Uri serviceUri;

        /// <summary>
        /// Инициализация DataServiceContext
        /// </summary>
        public ProductGateway()
        {
            serviceUri = new Uri("http://localhost:50000/AdventureWorks.svc");
            context = new AdventureWorksLTEntities(serviceUri);
            context.MergeOption = MergeOption.OverwriteChanges;
        }

        /// <summary>
        /// Если имя продукта не указано, верните все продукты с указанным categoryId; в противном случае верните только продукты с указанным именем и categoryId.
        /// </summary>
 	/// <param name="productName">Имя продукта, используемое для выполнения запросов продуктов</param>
        /// <param name="category">Категория, используемая для выполнения запросов продуктов</param>
        public IList<Product> GetProducts(string productName, ProductCategory category)
        {
            IEnumerable<Product> query;
            
            int categoryId = category.ProductCategoryID;
            if (!String.IsNullOrEmpty(productName))
            {
                query = from p in context.Products.Expand("ProductCategory")
                        where p.ProductCategory.ProductCategoryID == categoryId && p.Name == productName
                        select p;
            }
            else
            {
                query = from p in context.Products.Expand("ProductCategory")
                        where p.ProductCategory.ProductCategoryID == categoryId
                        select p;
            }

            try
            {
                List<Product> productSet = query.ToList();
                return productSet;
            }catch(Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Вернуть все категории продуктов
        /// </summary>
        public IList<ProductCategory> GetCategories()
        {
            return context.ProductCategories.ToList();
        }

        /// <summary>
        /// Попробуйте удалить указанный продукт; если не удается выполнить удаление, верните сообщение об ошибке "Не удается выполнить удаление продукта"
        /// </summary>
	/// <param name="product">Удаляемый продукт</param>
        public string DeleteProduct(Product product)
        {
            context.DeleteObject(product);
            
            try
            {
                context.SaveChanges();
            }
            catch (DataServiceRequestException)
            {
                return "Product Cannot be Deleted";
            }
            return null;
        }

 	/// <summary>
        /// Использование этого метода предполагает, что все поля были изменены. Метод обновляет всю сущность, включая связь с ProductCategory.
        /// Изменения отправляются на сервер, используя SaveChangesOptions.Batch; таким образом, все операции отправляются в одном запросе HTTP.
	/// </summary>
	/// <param name="product">Обновляемый продукт</param>
        public void UpdateProduct(Product product)
        {
            ProductCategory newCategory = product.ProductCategory;
            context.SetLink(product, "ProductCategory", newCategory);
            context.UpdateObject(product);
            context.SaveChanges(SaveChangesOptions.Batch);
        }

 	/// <summary>
        /// Добавьте новый объект продукта к DataServiceContext, а затем свяжите объект с существующей ProductCategory.
        /// Изменения отправляются на сервер, используя SaveChangesOptions.Batch; таким образом, все операции отправляются в одном запросе HTTP.
	/// </summary>
	/// <param name="product">Добавляемый продукт</param>
        public void AddProduct(Product product)
        {
            product.rowguid = Guid.NewGuid();
            context.AddObject("Products", product);
            context.SetLink(product, "ProductCategory", product.ProductCategory);
            context.SaveChanges(SaveChangesOptions.Batch);
        }

    }
}
