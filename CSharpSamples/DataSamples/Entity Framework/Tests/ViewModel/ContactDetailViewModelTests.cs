// (c) Корпорация Майкрософт (Microsoft Corp.). Все права защищены.

namespace Tests.ViewModel
{
    using System.Collections.Generic;
    using EmployeeTracker.Model;
    using EmployeeTracker.ViewModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Модульные тесты для объекта ContactDetailViewModel
    /// </summary>
    [TestClass]
    public class ContactDetailViewModelTests
    {
        /// <summary>
        /// Проверьте, что BuildViewModel может создавать все типы контактных данных
        /// </summary>
        [TestMethod]
        public void BuildViewModel()
        {
            Phone p = new Phone();
            Email e = new Email();
            Address a = new Address();

            var pvm = ContactDetailViewModel.BuildViewModel(p);
            Assert.IsInstanceOfType(pvm, typeof(PhoneViewModel), "Factory method created wrong ViewModel type.");
            Assert.AreEqual(p, pvm.Model, "Underlying model object on ViewModel is not correct.");

            var evm = ContactDetailViewModel.BuildViewModel(e);
            Assert.IsInstanceOfType(evm, typeof(EmailViewModel), "Factory method created wrong ViewModel type.");
            Assert.AreEqual(e, evm.Model, "Underlying model object on ViewModel is not correct.");

            var avm = ContactDetailViewModel.BuildViewModel(a);
            Assert.IsInstanceOfType(avm, typeof(AddressViewModel), "Factory method created wrong ViewModel type.");
            Assert.AreEqual(a, avm.Model, "Underlying model object on ViewModel is not correct.");
        }

        /// <summary>
        /// Проверьте, что BuildViewModel не вызывается, если производится обработка нераспознанного типа
        /// </summary>
        [TestMethod]
        public void BuildViewModelUnknownType()
        {
            var f = new FakeContactDetail();
            var fvm = ContactDetailViewModel.BuildViewModel(f);
            Assert.IsNull(fvm, "BuildViewModel should return null when it doesn't know how to handle a type.");
        }

        /// <summary>
        /// Проверьте, что если значение null недопустимо, вызываются NullArgumentException
        /// </summary>
        [TestMethod]
        public void CheckNullArgumentExceptions()
        {
            Utilities.CheckNullArgumentException(() => { ContactDetailViewModel.BuildViewModel(null); }, "detail", "BuildViewModel");
        }

        /// <summary>
        /// Фиктивный тип контакта для теста BuildViewModelUnknownType
        /// </summary>
        private class FakeContactDetail : ContactDetail
        {
            /// <summary>
            /// Возвращает допустимые значения для поля использования
            /// Реализация заглушки, возвращает только null
            /// </summary>
            public override IEnumerable<string> ValidUsageValues
            {
                get { return null; }
            }
        }
    }
}
