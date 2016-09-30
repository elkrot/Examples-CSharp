// (c) Корпорация Майкрософт (Microsoft Corp.). Все права защищены.

namespace Tests.ViewModel
{
    using System.Diagnostics.CodeAnalysis;
    using EmployeeTracker.Model;
    using EmployeeTracker.ViewModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests;

    /// <summary>
    /// Модульные тесты для EmailViewModel
    /// </summary>
    [TestClass]
    public class EmailViewModelTests
    {
        /// <summary>
        /// Проверьте методы getter (считывание) и setter (присвоение) на модели ViewModel, зависящей от изменений базовых данных и уведомлений
        /// </summary>
        [TestMethod]
        public void PropertyGetAndSet()
        {
            // Проверьте, что исходные свойства отображаются в модели ViewModel
            Email em = new Email { Address = "EMAIL" };
            EmailViewModel vm = new EmailViewModel(em);
            Assert.AreEqual(em, vm.Model, "Bound object property did not return object from model.");
            Assert.AreEqual(em.ValidUsageValues, vm.ValidUsageValues, "ValidUsageValues property did not return value from model.");
            Assert.AreEqual("EMAIL", vm.Address, "Address property did not return value from model.");

            // Проверьте, что при изменении свойств обновляется модель и возникает событие PropertyChanged
            string lastProperty;
            vm.PropertyChanged += (sender, e) => { lastProperty = e.PropertyName; };

            lastProperty = null;
            vm.Address = "NEW_EMAIL";
            Assert.AreEqual("Address", lastProperty, "Setting Address property did not raise correct PropertyChanged event.");
            Assert.AreEqual("NEW_EMAIL", em.Address, "Setting Address property did not update model.");
        }

        /// <summary>
        /// Проверьте, что метод getter (считывание) отражает изменения в модели
        /// </summary>
        [TestMethod]
        public void ModelChangesFlowToProperties()
        {
            // Проверьте, что ViewModel возвращает текущее значение для модели
            Email em = new Email { Address = "EMAIL" };
            EmailViewModel vm = new EmailViewModel(em);

            em.Address = "NEW_EMAIL";
            Assert.AreEqual("NEW_EMAIL", vm.Address, "Address property is not fetching the value from the model.");
        }

        /// <summary>
        /// Проверьте, что если значение null недопустимо, вызываются исключения NullArgumentException
        /// </summary>
        [TestMethod]
        public void CheckNullArgumentExceptions()
        {
            Utilities.CheckNullArgumentException(() => { new EmailViewModel(null); }, "detail", "ctor");
        }
    }
}
