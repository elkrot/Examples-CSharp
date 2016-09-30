// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqToTerraServerProvider
{
    class TerraServerQueryContext
    {
        private TerraServerQueryContext() { }

        // Выполняет дерево выражений, которое ему передается.
        internal static object Execute(Expression expression, bool IsEnumerable)
        {
            // Выражение должно представлять запрос к источнику данных.
            if (!IsQueryOverDataSource(expression))
                throw new InvalidProgramException("No query over the data source was specified.");

            // Найти вызов к Where() и получить предикат лямбда-выражения.
            InnermostWhereFinder whereFinder = new InnermostWhereFinder();
            MethodCallExpression whereExpression = whereFinder.GetInnermostWhere(expression);
            LambdaExpression lambdaExpression = (LambdaExpression)((UnaryExpression)(whereExpression.Arguments[1])).Operand;

            // Переслать лямбда-выражение через частичный вычислитель.
            lambdaExpression = (LambdaExpression)Evaluator.PartialEval(lambdaExpression);

            // Получить имя (имена) вхождений для использования в запросе к веб-службе.
            LocationFinder lf = new LocationFinder(lambdaExpression.Body);
            List<string> locations = lf.Locations;
            if (locations.Count == 0)
                throw new InvalidQueryException("You must specify at least one place name in your query.");

            // Вызвать веб-службу и получить результаты.
            Place[] places = WebServiceHelper.GetPlacesFromTerraServer(locations);

            // Скопировать вхождения IEnumerable в IQueryable.
            IQueryable<Place> queryablePlaces = places.AsQueryable<Place>();

            // Скопировать переданное дерево выражений, меняя только первый
            // аргумент внутреннего MethodCallExpression.
            ExpressionTreeModifier treeCopier = new ExpressionTreeModifier(queryablePlaces);
            Expression newExpressionTree = treeCopier.CopyAndModify(expression);

            // На этом этапе создается IQueryable, который заменяет методы Queryable на методы Enumerable.
            if (IsEnumerable)
                return queryablePlaces.Provider.CreateQuery(newExpressionTree);
            else
                return queryablePlaces.Provider.Execute(newExpressionTree);
        }

        private static bool IsQueryOverDataSource(Expression expression)
        {
            // Если выражение представляет незапрошенный экземпляр источника данных IQueryable,
            // то это выражение имеет тип ConstantExpression, а не MethodCallExpression.
            return (expression is MethodCallExpression);
        }
    }
}
