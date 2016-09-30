// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LinqToTerraServerProvider
{
    public class TerraServerQueryProvider : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
        {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            try
            {
                return (IQueryable)Activator.CreateInstance(typeof(QueryableTerraServerData<>).MakeGenericType(elementType), new object[] { this, expression });
            }
            catch (System.Reflection.TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }

        // Этот метод вызывают стандартные операторы запроса класса Queryable, возвращающие коллекции.
        public IQueryable<TResult> CreateQuery<TResult>(Expression expression)
        {
            return new QueryableTerraServerData<TResult>(this, expression);
        }

        public object Execute(Expression expression)
        {
            return TerraServerQueryContext.Execute(expression, false);
        }

        // Этот метод вызывают стандартные операторы запроса класса Queryable с одиночным значением.
        // Он также может быть вызван из QueryableTerraServerData.GetEnumerator().
        public TResult Execute<TResult>(Expression expression)
        {
            bool IsEnumerable = (typeof(TResult).Name == "IEnumerable`1");

            return (TResult)TerraServerQueryContext.Execute(expression, IsEnumerable);
        }
    }
}
