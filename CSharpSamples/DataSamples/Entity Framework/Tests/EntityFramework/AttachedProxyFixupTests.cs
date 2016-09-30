// (c) Корпорация Майкрософт (Microsoft Corp.). Все права защищены.

namespace Tests.EntityFramework
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using EmployeeTracker.EntityFramework;
    using EmployeeTracker.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.Model;

    /// <summary>
    /// Тестирует поведение исправления прокси-версий объектов в модели, присоединенных к контексту ObjectContext
    /// </summary>
    [TestClass]
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Context is disposed in test cleanup.")]
    public class AttachedProxyFixupTests : FixupTestsBase
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
        /// Возвращает прокси отслеживания изменений, производный от T и присоединенный к ObjectContext
        /// </summary>
        /// <typeparam name="T">Создаваемый тип</typeparam>
        /// <returns>Новый экземпляр типа T</returns>
        protected override T CreateObject<T>()
        {
            T obj = this.context.CreateObject<T>();

            Employee e = obj as Employee;
            if (e != null)
            {
                this.context.Employees.AddObject(e);
                return obj;
            }

            Department d = obj as Department;
            if (d != null)
            {
                this.context.Departments.AddObject(d);
                return obj;
            }

            ContactDetail c = obj as ContactDetail;
            if (c != null)
            {
                this.context.ContactDetails.AddObject(c);
                return obj;
            }
           
            Assert.Fail(string.Format(CultureInfo.InvariantCulture, "Need to update AttachedProxyFixupTests.CreateObject to be able to attach objects of type {0}.", obj.GetType().Name));
            return null;
        }
    }
}
