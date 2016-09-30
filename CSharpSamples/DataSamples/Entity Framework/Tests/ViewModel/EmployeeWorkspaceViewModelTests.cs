// (c) Корпорация Майкрософт (Microsoft Corp.). Все права защищены.

namespace Tests.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using EmployeeTracker.Common;
    using EmployeeTracker.Fakes;
    using EmployeeTracker.Model;
    using EmployeeTracker.ViewModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests;

    /// <summary>
    /// Модульные тесты для объекта EmployeeWorkspaceViewModel
    /// </summary>
    [TestClass]
    public class EmployeeWorkspaceViewModelTests
    {
        /// <summary>
        /// Проверьте создание рабочей области без данных
        /// </summary>
        [TestMethod]
        public void InitializeWithEmptyData()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);
                ObservableCollection<DepartmentViewModel> departments = new ObservableCollection<DepartmentViewModel>();
                ObservableCollection<EmployeeViewModel> employees = new ObservableCollection<EmployeeViewModel>();
                EmployeeWorkspaceViewModel vm = new EmployeeWorkspaceViewModel(employees, departments, unit);

                Assert.AreSame(employees, vm.AllEmployees, "ViewModel should expose the same instance of the collection so that changes outside the ViewModel are reflected.");
                Assert.IsNull(vm.CurrentEmployee, "Current employee should not be set if there are no department.");
                Assert.IsNotNull(vm.AddEmployeeCommand, "AddEmployeeCommand should be initialized");
                Assert.IsNotNull(vm.DeleteEmployeeCommand, "DeleteEmployeeCommand should be initialized");
            }
        }

        /// <summary>
        /// Проверьте создание рабочей области с данными
        /// </summary>
        [TestMethod]
        public void InitializeWithData()
        {
            Employee e1 = new Employee();
            Employee e2 = new Employee();

            Department d1 = new Department();
            Department d2 = new Department();

            using (FakeEmployeeContext ctx = new FakeEmployeeContext(new Employee[] { e1, e2 }, new Department[] { d1, d2 }))
            {
                UnitOfWork unit = new UnitOfWork(ctx);
                ObservableCollection<DepartmentViewModel> departments = new ObservableCollection<DepartmentViewModel>(ctx.Departments.Select(d => new DepartmentViewModel(d)));
                ObservableCollection<EmployeeViewModel> employees = new ObservableCollection<EmployeeViewModel>();
                foreach (var e in ctx.Employees)
                {
                    employees.Add(new EmployeeViewModel(e, employees, departments, unit));
                }

                EmployeeWorkspaceViewModel vm = new EmployeeWorkspaceViewModel(employees, departments, unit);

                Assert.IsNotNull(vm.CurrentEmployee, "Current employee should be set if there are departments.");
                Assert.AreSame(employees, vm.AllEmployees, "ViewModel should expose the same instance of the Employee collection so that changes outside the ViewModel are reflected.");
                Assert.AreSame(employees, vm.AllEmployees[0].ManagerLookup, "ViewModel should expose the same instance of the Employee collection to children so that changes outside the ViewModel are reflected.");
                Assert.AreSame(departments, vm.AllEmployees[0].DepartmentLookup, "ViewModel should expose the same instance of the Department collection to children so that changes outside the ViewModel are reflected.");
            }
        }

        /// <summary>
        /// Проверьте, что если значение null недопустимо, вызываются исключения NullArgumentException
        /// </summary>
        [TestMethod]
        public void NullArgumentChecks()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);

                Utilities.CheckNullArgumentException(() => { new EmployeeWorkspaceViewModel(null, new ObservableCollection<DepartmentViewModel>(), unit); }, "employees", "ctor");
                Utilities.CheckNullArgumentException(() => { new EmployeeWorkspaceViewModel(new ObservableCollection<EmployeeViewModel>(), null, unit); }, "departmentLookup", "ctor");
                Utilities.CheckNullArgumentException(() => { new EmployeeWorkspaceViewModel(new ObservableCollection<EmployeeViewModel>(), new ObservableCollection<DepartmentViewModel>(), null); }, "unitOfWork", "ctor");
            }
        }

        /// <summary>
        /// Проверьте методы getter и setter для текущего сотрудника
        /// </summary>
        [TestMethod]
        public void CurrentEmployee()
        {
            using (FakeEmployeeContext ctx = BuildContextWithData())
            {
                EmployeeWorkspaceViewModel vm = BuildViewModel(ctx);

                string lastProperty;
                vm.PropertyChanged += (sender, e) => { lastProperty = e.PropertyName; };

                lastProperty = null;
                vm.CurrentEmployee = null;
                Assert.IsNull(vm.CurrentEmployee, "CurrentEmployee should have been nulled.");
                Assert.AreEqual("CurrentEmployee", lastProperty, "CurrentEmployee should have raised a PropertyChanged when set to null.");

                lastProperty = null;
                var employee = vm.AllEmployees.First();
                vm.CurrentEmployee = employee;
                Assert.AreSame(employee, vm.CurrentEmployee, "CurrentEmployee has not been set to specified value.");
                Assert.AreEqual("CurrentEmployee", lastProperty, "CurrentEmployee should have raised a PropertyChanged when set to a value.");
            }
        }

        /// <summary>
        /// Проверьте, что отражены дополнения в коллекцию сотрудников
        /// </summary>
        [TestMethod]
        public void ExternalAddToEmployeeCollection()
        {
            using (FakeEmployeeContext ctx = BuildContextWithData())
            {
                UnitOfWork unit = new UnitOfWork(ctx);
                EmployeeWorkspaceViewModel vm = BuildViewModel(ctx, unit);

                EmployeeViewModel currentEmployee = vm.CurrentEmployee;
                EmployeeViewModel newEmployee = new EmployeeViewModel(new Employee(), vm.AllEmployees, new ObservableCollection<DepartmentViewModel>(), unit);

                vm.AllEmployees.Add(newEmployee);
                Assert.IsTrue(vm.AllEmployees.Contains(newEmployee), "New employee should have been added to AllEmployees.");
                Assert.AreSame(currentEmployee, vm.CurrentEmployee, "CurrentEmployee should not have changed.");
                Assert.IsFalse(ctx.IsObjectTracked(newEmployee.Model), "ViewModel is not responsible for adding employees created externally.");
            }
        }

        /// <summary>
        /// Проверьте, что отражены удаления из коллекции сотрудников
        /// Если текущий сотрудник остается в коллекции
        /// </summary>
        [TestMethod]
        public void ExternalRemoveFromEmployeeCollection()
        {
            using (FakeEmployeeContext ctx = BuildContextWithData())
            {
                UnitOfWork unit = new UnitOfWork(ctx);
                EmployeeWorkspaceViewModel vm = BuildViewModel(ctx, unit);

                EmployeeViewModel current = vm.AllEmployees.First();
                EmployeeViewModel toDelete = vm.AllEmployees.Skip(1).First();
                vm.CurrentEmployee = current;

                vm.AllEmployees.Remove(toDelete);
                Assert.IsFalse(vm.AllEmployees.Contains(toDelete), "Employee should have been removed from AllDepartments.");
                Assert.AreSame(current, vm.CurrentEmployee, "CurrentEmployee should not have changed.");
                Assert.IsTrue(ctx.IsObjectTracked(toDelete.Model), "ViewModel is not responsible for deleting employees removed externally.");
            }
        }

        /// <summary>
        /// Проверьте, что отражены удаления из коллекции сотрудников
        /// Если текущий сотрудник удален
        /// </summary>
        [TestMethod]
        public void ExternalRemoveSelectedEmployeeFromCollection()
        {
            using (FakeEmployeeContext ctx = BuildContextWithData())
            {
                UnitOfWork unit = new UnitOfWork(ctx);
                EmployeeWorkspaceViewModel vm = BuildViewModel(ctx, unit);
                EmployeeViewModel current = vm.CurrentEmployee;

                string lastProperty = null;
                vm.PropertyChanged += (sender, e) => { lastProperty = e.PropertyName; };

                vm.AllEmployees.Remove(current);
                Assert.IsFalse(vm.AllEmployees.Contains(current), "Employee should have been removed from AllEmployees.");
                Assert.IsNull(vm.CurrentEmployee, "CurrentEmployee should have been nulled as it was removed from the collection.");
                Assert.AreEqual("CurrentEmployee", lastProperty, "CurrentEmployee should have raised a PropertyChanged.");
                Assert.IsTrue(ctx.IsObjectTracked(current.Model), "ViewModel is not responsible for deleting employees removed externally.");
            }
        }

        /// <summary>
        /// Проверьте, что команда удаления отдела доступна, если только выбран сотрудник
        /// </summary>
        [TestMethod]
        public void DeleteCommandOnlyAvailableWhenEmployeeSelected()
        {
            using (FakeEmployeeContext ctx = BuildContextWithData())
            {
                EmployeeWorkspaceViewModel vm = BuildViewModel(ctx);

                vm.CurrentEmployee = null;
                Assert.IsFalse(vm.DeleteEmployeeCommand.CanExecute(null), "Delete command should be disabled when no employee is selected.");

                vm.CurrentEmployee = vm.AllEmployees.First();
                Assert.IsTrue(vm.DeleteEmployeeCommand.CanExecute(null), "Delete command should be enabled when employee is selected.");
            }
        }

        /// <summary>
        /// Проверьте, что сотрудник может быть удален
        /// </summary>
        [TestMethod]
        public void DeleteEmployee()
        {
            using (FakeEmployeeContext ctx = BuildContextWithData())
            {
                EmployeeWorkspaceViewModel vm = BuildViewModel(ctx);

                EmployeeViewModel toDelete = vm.CurrentEmployee;
                int originalCount = vm.AllEmployees.Count;

                string lastProperty = null;
                vm.PropertyChanged += (sender, e) => { lastProperty = e.PropertyName; };

                vm.DeleteEmployeeCommand.Execute(null);

                Assert.AreEqual(originalCount - 1, vm.AllEmployees.Count, "One employee should have been removed from the AllEmployees property.");
                Assert.IsFalse(vm.AllEmployees.Contains(toDelete), "The selected employee should have been removed.");
                Assert.IsFalse(ctx.IsObjectTracked(toDelete.Model), "The selected employee has not been removed from the context.");
                Assert.IsNull(vm.CurrentEmployee, "No employee should be selected after deletion.");
                Assert.AreEqual("CurrentEmployee", lastProperty, "CurrentEmployee should have raised a PropertyChanged.");
            }
        }

        /// <summary>
        /// Проверьте, что новый сотрудник может быть добавлен, если выбран другой сотрудник
        /// </summary>
        [TestMethod]
        public void AddWhenEmployeeSelected()
        {
            using (FakeEmployeeContext ctx = BuildContextWithData())
            {
                EmployeeWorkspaceViewModel vm = BuildViewModel(ctx);

                vm.CurrentEmployee = vm.AllEmployees.First();
                TestAddEmployee(ctx, vm);
            }
        }

        /// <summary>
        /// Проверьте, что новый сотрудник может быть добавлен, если другой сотрудник не выбран
        /// </summary>
        [TestMethod]
        public void AddWhenEmployeeNotSelected()
        {
            using (FakeEmployeeContext ctx = BuildContextWithData())
            {
                EmployeeWorkspaceViewModel vm = BuildViewModel(ctx);

                vm.CurrentEmployee = null;
                TestAddEmployee(ctx, vm);
            }
        }

        /// <summary>
        /// Проверяет добавление сотрудника в рабочую область и в единицу работы
        /// </summary>
        /// <param name="unitOfWork">Контекст, в который должен быть добавлен сотрудник</param>
        /// <param name="vm">Рабочая область, в которую должен быть добавлен сотрудник</param>
        private static void TestAddEmployee(FakeEmployeeContext ctx, EmployeeWorkspaceViewModel vm)
        {
            List<EmployeeViewModel> originalEmployees = vm.AllEmployees.ToList();

            string lastProperty = null;
            vm.PropertyChanged += (sender, e) => { lastProperty = e.PropertyName; };

            Assert.IsTrue(vm.AddEmployeeCommand.CanExecute(null), "Add command should always be enabled.");
            vm.AddEmployeeCommand.Execute(null);

            Assert.AreEqual(originalEmployees.Count + 1, vm.AllEmployees.Count, "One new employee should have been added to the AllEmployees property.");
            Assert.IsFalse(originalEmployees.Contains(vm.CurrentEmployee), "The new employee should be selected.");
            Assert.IsNotNull(vm.CurrentEmployee, "The new employee should be selected.");
            Assert.AreEqual("CurrentEmployee", lastProperty, "CurrentEmployee should have raised a PropertyChanged.");
            Assert.IsTrue(ctx.IsObjectTracked(vm.CurrentEmployee.Model), "The new employee has not been added to the context.");
        }

        /// <summary>
        /// Создает фиктивный контекст с заполненными данными
        /// </summary>
        /// <returns>Фиктивный контекст</returns>
        private static FakeEmployeeContext BuildContextWithData()
        {
            Employee e1 = new Employee();
            Employee e2 = new Employee();

            Department d1 = new Department();
            Department d2 = new Department();

            return new FakeEmployeeContext(new Employee[] { e1, e2 }, new Department[] { d1, d2 });
        }

        /// <summary>
        /// Создает ViewModel на основании фиктивного контекста
        /// </summary>
        /// <param name="ctx">Контекст, на котором основана модель просмотра</param>
        /// <returns>Новая модель ViewModel</returns>
        private static EmployeeWorkspaceViewModel BuildViewModel(FakeEmployeeContext ctx)
        {
            return BuildViewModel(ctx, new UnitOfWork(ctx));
        }

        /// <summary>
        /// Создает ViewModel на основании фиктивного контекста, с использованием существующей единицы работы
        /// </summary>
        /// <param name="ctx">Контекст, на котором основана модель просмотра</param>
        /// <param name="unit">Текущая единица работы</param>
        /// <returns>Новая модель ViewModel</returns>
        private static EmployeeWorkspaceViewModel BuildViewModel(FakeEmployeeContext ctx, UnitOfWork unit)
        {
            ObservableCollection<DepartmentViewModel> departments = new ObservableCollection<DepartmentViewModel>(ctx.Departments.Select(d => new DepartmentViewModel(d)));
            ObservableCollection<EmployeeViewModel> employees = new ObservableCollection<EmployeeViewModel>();
            foreach (var e in ctx.Employees)
            {
                employees.Add(new EmployeeViewModel(e, employees, departments, unit));
            }

            return new EmployeeWorkspaceViewModel(employees, departments, unit);
        }
    }
}
