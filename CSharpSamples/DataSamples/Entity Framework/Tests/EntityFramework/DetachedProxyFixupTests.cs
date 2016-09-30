// (c) Корпорация Майкрософт (Microsoft Corp.). Все права защищены.

namespace Tests.EntityFramework
{
    using System.Diagnostics.CodeAnalysis;
    using EmployeeTracker.EntityFramework;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.Model;

    /// <summary>
    /// Тестирует поведение исправления прокси-версий объектов в модели, не присоединенных к контексту ObjectContext
    /// </summary>
    [TestClass]
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Context is disposed in test cleanup.")]
    public class DetachedProxyFixupTests : FixupTestsBase
    {
        /// <summary>
        /// Контекст, используемый для создания прокси
        /// </summary>
        private EmployeeEntities context;

        /// <summary>
        /// Создает ресурсы, необходимые для этого теста
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            this.context = new EmployeeEntities();

            // Модульные тесты запускаются без базы данных, поэтому необходимо отключить LazyLoading
            this.context.ContextOptions.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// Освобождает ресурсы, используемые для выполнения этого теста
        /// </summary>
        [TestCleanup]
        public void Teardown()
        {
            this.context.Dispose();
        }

        /// <summary>
        /// Возвращает прокси отслеживания изменений, производный от T
        /// </summary>
        /// <typeparam name="T">Создаваемый тип</typeparam>
        /// <returns>Новый экземпляр типа T</returns>
        protected override T CreateObject<T>()
        {
            return this.context.CreateObject<T>();
        }
    }
}
