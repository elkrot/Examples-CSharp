// (c) Корпорация Майкрософт (Microsoft Corp.). Все права защищены.

namespace Tests.ViewModel
{
    using EmployeeTracker.Model;
    using EmployeeTracker.ViewModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Модульные тесты для PhoneViewModel
    /// </summary>
    [TestClass]
    public class PhoneViewModelTests
    {
        /// <summary>
        /// Проверьте методы getter (считывание) и setter (присвоение) на модели ViewModel, зависящей от изменений базовых данных и уведомлений
        /// </summary>
        [TestMethod]
        public void PropertyGetAndSet()
        {
            // Проверьте, что исходные свойства отображаются в модели ViewModel
            Phone ph = new Phone { Number = "NUMBER", Extension = "EXTENSION" };
            PhoneViewModel vm = new PhoneViewModel(ph);
            Assert.AreEqual(ph, vm.Model, "Bound object property did not return object from model.");
            Assert.AreEqual(ph.ValidUsageValues, vm.ValidUsageValues, "ValidUsageValues property did not return value from model.");
            Assert.AreEqual("NUMBER", vm.Number, "Number property did not return value from model.");
            Assert.AreEqual("EXTENSION", vm.Extension, "Extension property did not return value from model.");

            // Проверьте, что при изменении свойств обновляется модель и возникает событие PropertyChanged
            string lastProperty;
            vm.PropertyChanged += (sender, e) => { lastProperty = e.PropertyName; };

            lastProperty = null;
            vm.Number = "NEW_NUMBER";
            Assert.AreEqual("Number", lastProperty, "Setting Number property did not raise correct PropertyChanged event.");
            Assert.AreEqual("NEW_NUMBER", ph.Number, "Setting Number property did not update model.");

            lastProperty = null;
            vm.Extension = "NEW_EXTENSION";
            Assert.AreEqual("Extension", lastProperty, "Setting Extension property did not raise correct PropertyChanged event.");
            Assert.AreEqual("NEW_EXTENSION", ph.Extension, "Setting Extension property did not update model.");
        }

        /// <summary>
        /// Проверьте, что метод getter (считывание) отражает изменения в модели
        /// </summary>
        [TestMethod]
        public void ModelChangesFlowToProperties()
        {
            // Проверьте, что ViewModel возвращает текущее значение для модели
            Phone ph = new Phone { Number = "NUMBER", Extension = "EXTENSION" };
            PhoneViewModel vm = new PhoneViewModel(ph);

            ph.Number = "NEW_NUMBER";
            ph.Extension = "NEW_EXTENSION";
            Assert.AreEqual("NEW_NUMBER", vm.Number, "Number property is not fetching the value from the model.");
            Assert.AreEqual("NEW_EXTENSION", vm.Extension, "Extension property is not fetching the value from the model.");
        }

        /// <summary>
        /// Проверьте, что если значение null недопустимо, вызываются исключения NullArgumentException
        /// </summary>
        [TestMethod]
        public void CheckNullArgumentExceptions()
        {
            Utilities.CheckNullArgumentException(() => { new PhoneViewModel(null); }, "detail", "ctor");
        }
    }
}
