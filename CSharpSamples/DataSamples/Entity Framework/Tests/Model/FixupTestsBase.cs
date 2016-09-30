// (c) Корпорация Майкрософт (Microsoft Corp.). Все права защищены.

namespace Tests.Model
{
    using EmployeeTracker.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Базовый класс для тестов, которые проверяют поведение исправления между объектами
    /// Это используется для теста прокси-объектов Pure POCO и Entity Framework,
    /// чтобы убедиться в их одинаковом поведении.
    /// </summary>
    [TestClass]
    public abstract class FixupTestsBase
    {
        #region Отдел - сотрудники

        /// <summary>
        /// Добавьте сотрудника без отдела в новую коллекцию сотрудников отделов
        /// </summary>
        [TestMethod]
        public void AddUnassignedEmployeeToDepartment()
        {
            Department dep = CreateObject<Department>();
            Employee emp = CreateObject<Employee>();

            dep.Employees.Add(emp);
            Assert.IsTrue(dep.Employees.Contains(emp), "Employee was not added to collection on department.");
            Assert.AreSame(dep, emp.Department, "Department was not set on employee.");
        }

        /// <summary>
        /// Добавьте сотрудника с существующим отделом в новую коллекцию сотрудников отделов
        /// </summary>
        [TestMethod]
        public void AddAssignedEmployeeToDepartment()
        {
            Employee emp = CreateObject<Employee>();
            Department depOriginal = CreateObject<Department>(); 
            Department depNew = CreateObject<Department>();
            depOriginal.Employees.Add(emp);

            depNew.Employees.Add(emp);
            Assert.IsFalse(depOriginal.Employees.Contains(emp), "Employee was not removed from collection on old department.");
            Assert.IsTrue(depNew.Employees.Contains(emp), "Employee was not added to collection on CreateObject<department.");
            Assert.AreSame(depNew, emp.Department, "Department was not set on employee.");
        }

        /// <summary>
        /// Удалите сотрудника из коллекции сотрудников отделов
        /// </summary>
        [TestMethod]
        public void RemoveEmployeeFromDepartment()
        {
            Department dep = CreateObject<Department>(); 
            Employee emp = CreateObject<Employee>();
            dep.Employees.Add(emp);

            dep.Employees.Remove(emp);
            Assert.IsFalse(dep.Employees.Contains(emp), "Employee was not removed from collection on department.");
            Assert.IsNull(emp.Department, "Department was not nulled on employee.");
        }

        /// <summary>
        /// Добавьте сотрудника в отдел, к которому он уже относится
        /// </summary>
        [TestMethod]
        public void AddEmployeeToSameDepartment()
        {
            Department dep = CreateObject<Department>(); 
            Employee emp = CreateObject<Employee>();
            dep.Employees.Add(emp);

            dep.Employees.Add(emp);
            Assert.IsTrue(dep.Employees.Contains(emp), "Employee is not in collection on department.");
            Assert.AreSame(dep, emp.Department, "Department is not set on employee.");
        }

        /// <summary>
        /// Установите отдел для работника, которому не назначен отдел
        /// </summary>
        [TestMethod]
        public void SetDepartmentOnUnassignedEmployee()
        {
            Department dep = CreateObject<Department>(); 
            Employee emp = CreateObject<Employee>();

            emp.Department = dep;
            Assert.IsTrue(dep.Employees.Contains(emp), "Employee was not added to collection on department.");
            Assert.AreSame(dep, emp.Department, "Department was not set on employee.");
        }

        /// <summary>
        /// Установите отдел для работника, которому назначен другой отдел
        /// </summary>
        [TestMethod]
        public void SetDepartmentOnAssignedEmployee()
        {
            Employee emp = CreateObject<Employee>();
            Department depOriginal = CreateObject<Department>();
            Department depNew = CreateObject<Department>();
            emp.Department = depOriginal;

            emp.Department = depNew;
            Assert.IsFalse(depOriginal.Employees.Contains(emp), "Employee was not removed from collection on old department.");
            Assert.IsTrue(depNew.Employees.Contains(emp), "Employee was not added to collection on CreateObject<department.");
            Assert.AreSame(depNew, emp.Department, "Department was not set on employee.");
        }

        /// <summary>
        /// Удалите отдел для сотрудника
        /// </summary>
        [TestMethod]
        public void NullDepartmentOnAssignedEmployee()
        {
            Department dep = CreateObject<Department>();
            Employee emp = CreateObject<Employee>();
            emp.Department = dep;

            emp.Department = null;
            Assert.IsFalse(dep.Employees.Contains(emp), "Employee was not removed from collection on department.");
            Assert.IsNull(emp.Department, "Department was not nulled on employee.");
        }

        /// <summary>
        /// Установите свойство отдела для сотрудника в тот же отдел
        /// </summary>
        [TestMethod]
        public void SetSameDepartmentOnEmployee()
        {
            Department dep = CreateObject<Department>();
            Employee emp = CreateObject<Employee>();
            emp.Department = dep;

            emp.Department = dep;
            Assert.IsTrue(dep.Employees.Contains(emp), "Employee is not in collection on department.");
            Assert.AreEqual(1, dep.Employees.Count, "Employee has been added again to collection on department.");
            Assert.AreSame(dep, emp.Department, "Department is not set on employee.");
        }

        /// <summary>
        /// Установите отдел в null, если для него уже установлено значение null
        /// </summary>
        [TestMethod]
        public void NullDepartmentOnUnassignedEmployee()
        {
            Employee emp = CreateObject<Employee>();

            emp.Department = null;
            Assert.IsNull(emp.Department, "Department is not null on employee.");
        }

        #endregion

        #region Руководитель - отчеты

        /// <summary>
        /// Добавьте сотрудника без руководителя в новую коллекцию отчетов для руководителя
        /// </summary>
        [TestMethod]
        public void AddUnassignedEmployeeToManager()
        {
            Employee man = CreateObject<Employee>();
            Employee emp = CreateObject<Employee>();

            man.Reports.Add(emp);
            Assert.IsTrue(man.Reports.Contains(emp), "Employee was not added to collection on manager.");
            Assert.AreSame(man, emp.Manager, "Manager was not set on employee.");
        }

        /// <summary>
        /// Добавьте сотрудника с существующим руководителем в новую коллекцию отчетов для руководителя
        /// </summary>
        [TestMethod]
        public void AddAssignedEmployeeToManager()
        {
            Employee man = CreateObject<Employee>();
            Employee manOrig = CreateObject<Employee>();
            Employee emp = CreateObject<Employee>();
            manOrig.Reports.Add(emp);

            man.Reports.Add(emp);
            Assert.IsFalse(manOrig.Reports.Contains(emp), "Employee was not removed from collection on old manager.");
            Assert.IsTrue(man.Reports.Contains(emp), "Employee was not added to collection on manager.");
            Assert.AreSame(man, emp.Manager, "Manager was not set on employee.");
        }

        /// <summary>
        /// Удалите сотрудника из коллекции отчетов для руководителя
        /// </summary>
        [TestMethod]
        public void RemoveEmployeeFromManager()
        {
            Employee man = CreateObject<Employee>();
            Employee emp = CreateObject<Employee>();
            man.Reports.Add(emp);

            man.Reports.Remove(emp);
            Assert.IsFalse(man.Reports.Contains(emp), "Employee was not removed from collection on old manager.");
            Assert.IsNull(emp.Manager, "Manager was not nulled on employee.");
        }

        /// <summary>
        /// Добавьте сотрудника к руководителю, в подчинении которого он уже находится
        /// </summary>
        [TestMethod]
        public void AddEmployeeToSameManager()
        {
            Employee man = CreateObject<Employee>();
            Employee emp = CreateObject<Employee>();
            man.Reports.Add(emp);

            man.Reports.Add(emp);
            Assert.IsTrue(man.Reports.Contains(emp), "Employee is not in collection on manager.");
            Assert.AreSame(man, emp.Manager, "Manager is not set on employee.");
        }

        /// <summary>
        /// Установите руководителя для работника, которому не назначен руководитель
        /// </summary>
        [TestMethod]
        public void SetManagerOnUnassignedEmployee()
        {
            Employee man = CreateObject<Employee>();
            Employee emp = CreateObject<Employee>();

            emp.Manager = man;
            Assert.IsTrue(man.Reports.Contains(emp), "Employee was not added to collection on manager.");
            Assert.AreSame(man, emp.Manager, "Manager was not set on employee.");
        }

        /// <summary>
        /// Установите руководителя для работника, которому назначен другой руководитель
        /// </summary>
        [TestMethod]
        public void SetManagerOnAssignedEmployee()
        {
            Employee man = CreateObject<Employee>();
            Employee manOrig = CreateObject<Employee>();
            Employee emp = CreateObject<Employee>();
            emp.Manager = manOrig;

            emp.Manager = man;
            Assert.IsFalse(manOrig.Reports.Contains(emp), "Employee was not removed from collection on old manager.");
            Assert.IsTrue(man.Reports.Contains(emp), "Employee was not added to collection on manager.");
            Assert.AreSame(man, emp.Manager, "Manager was not set on employee.");
        }

        /// <summary>
        /// Удалите руководителя для сотрудника
        /// </summary>
        [TestMethod]
        public void NullManagerOnAssignedEmployee()
        {
            Employee man = CreateObject<Employee>();
            Employee emp = CreateObject<Employee>();
            emp.Manager = man;

            emp.Manager = null;
            Assert.IsFalse(man.Reports.Contains(emp), "Employee was not removed from collection on manager.");
            Assert.IsNull(emp.Manager, "Manager was not nulled on employee.");
        }

        /// <summary>
        /// Установите свойство руководителя для сотрудника в того же руководителя
        /// </summary>
        [TestMethod]
        public void SetSameManagerOnEmployee()
        {
            Employee man = CreateObject<Employee>();
            Employee emp = CreateObject<Employee>();
            emp.Manager = man;

            emp.Manager = man;
            Assert.IsTrue(man.Reports.Contains(emp), "Employee is not in collection on manager.");
            Assert.AreEqual(1, man.Reports.Count, "Employee has been added again to collection on manager.");
            Assert.AreSame(man, emp.Manager, "Manager is not set on employee.");
        }

        /// <summary>
        /// Установите руководителя в null, если для него уже установлено значение null
        /// </summary>
        [TestMethod]
        public void NullManagerOnUnassignedEmployee()
        {
            Employee emp = CreateObject<Employee>();

            emp.Manager = null;
            Assert.IsNull(emp.Manager, "Manager is not null on employee.");
        }

        #endregion

        /// <summary>
        /// Создайте объект типа T для тестов, которые будут выполняться на нем
        /// </summary>
        /// <typeparam name="T">Тип создаваемого объекта</typeparam>
        /// <returns>Экземпляр типа T или типа, производного от T</returns>
        protected abstract T CreateObject<T>() where T : class;
    }
}
