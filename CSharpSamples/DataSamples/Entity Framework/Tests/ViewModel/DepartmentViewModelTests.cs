// (c) Корпорация Майкрософт (Microsoft Corp.). Все права защищены.

namespace Tests.ViewModel
{
    using EmployeeTracker.Model;
    using EmployeeTracker.ViewModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests;

    /// <summary>
    /// Модульные тесты для DepartmentViewModel
    /// </summary>
    [TestClass]
    public class DepartmentViewModelTests
    {
        /// <summary>
        /// Проверьте методы getter (считывание) и setter (присвоение) на модели ViewModel, зависящей от изменений базовых данных и уведомлений
        /// </summary>
        [TestMethod]
        public void PropertyGetAndSet()
        {
            // Проверьте, что исходные свойства отображаются в модели ViewModel
            Department dep = new Department { DepartmentName = "DepartmentName", DepartmentCode = "DepartmentCode" };
            DepartmentViewModel vm = new DepartmentViewModel(dep);
            Assert.AreEqual(dep, vm.Model, "Bound object property did not return object from model.");
            Assert.AreEqual("DepartmentName", vm.DepartmentName, "DepartmentName property did not return value from model.");
            Assert.AreEqual("DepartmentCode", vm.DepartmentCode, "DepartmentCode property did not return value from model.");

            // Проверьте, что при изменении свойств обновляется модель и возникает событие PropertyChanged
            string lastProperty;
            vm.PropertyChanged += (sender, e) => { lastProperty = e.PropertyName; };

            lastProperty = null;
            vm.DepartmentName = "DepartmentName_NEW";
            Assert.AreEqual("DepartmentName", lastProperty, "Setting DepartmentName property did not raise correct PropertyChanged event.");
            Assert.AreEqual("DepartmentName_NEW", dep.DepartmentName, "Setting DepartmentName property did not update model.");

            lastProperty = null;
            vm.DepartmentCode = "DepartmentCode_NEW";
            Assert.AreEqual("DepartmentCode", lastProperty, "Setting DepartmentName property did not raise correct PropertyChanged event.");
            Assert.AreEqual("DepartmentCode_NEW", dep.DepartmentCode, "Setting DepartmentCode property did not update model.");
        }

        /// <summary>
        /// Проверьте, что метод getter (считывание) отражает изменения в модели
        /// </summary>
        [TestMethod]
        public void ModelChangesFlowToProperties()
        {
            // Проверьте, что ViewModel возвращает текущее значение для модели
            Department dep = new Department { DepartmentName = "DepartmentName", DepartmentCode = "DepartmentCode" };
            DepartmentViewModel vm = new DepartmentViewModel(dep);

            vm.DepartmentName = "DepartmentName_NEW";
            Assert.AreEqual("DepartmentName_NEW", dep.DepartmentName, "DepartmentName property is not fetching the value from the model.");
            vm.DepartmentCode = "DepartmentCode_NEW";
            Assert.AreEqual("DepartmentCode_NEW", dep.DepartmentCode, "DepartmentCode property is not fetching the value from the model.");
        }

        /// <summary>
        /// Проверьте, что если значение null недопустимо, вызываются исключения NullArgumentException
        /// </summary>
        [TestMethod]
        public void CheckNullArgumentExceptions()
        {
            Utilities.CheckNullArgumentException(() => { new DepartmentViewModel(null); }, "department", "ctor");
        }
    }
}
