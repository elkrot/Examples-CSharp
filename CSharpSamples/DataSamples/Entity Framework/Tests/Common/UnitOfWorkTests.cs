// (c) Корпорация Майкрософт (Microsoft Corp.). Все права защищены.

namespace Tests.Common
{
    using System;
    using System.Linq;
    using EmployeeTracker.Common;
    using EmployeeTracker.Fakes;
    using EmployeeTracker.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Проверьте возможности UnitOfWork по отслеживанию изменений
    /// </summary>
    [TestClass]
    public class UnitOfWorkTests
    {
        /// <summary>
        /// Проверьте, что если значение null недопустимо, вызываются исключения NullArgumentException
        /// </summary>
        [TestMethod]
        public void NullArgumentChecks()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);

                Utilities.CheckNullArgumentException(() => { new UnitOfWork(null); }, "context", "ctor");

                Utilities.CheckNullArgumentException(() => { unit.AddEmployee(null); }, "employee", "AddEmployee");
                Utilities.CheckNullArgumentException(() => { unit.AddDepartment(null); }, "department", "AddDepartment");
                Utilities.CheckNullArgumentException(() => { unit.AddContactDetail(new Employee(), null); }, "detail", "AddContactDetail");
                Utilities.CheckNullArgumentException(() => { unit.AddContactDetail(null, new Phone()); }, "employee", "AddContactDetail");

                Utilities.CheckNullArgumentException(() => { unit.RemoveEmployee(null); }, "employee", "RemoveEmployee");
                Utilities.CheckNullArgumentException(() => { unit.RemoveDepartment(null); }, "department", "RemoveDepartment");
                Utilities.CheckNullArgumentException(() => { unit.RemoveContactDetail(null, new Phone()); }, "employee", "RemoveContactDetail");
                Utilities.CheckNullArgumentException(() => { unit.RemoveContactDetail(new Employee(), null); }, "detail", "RemoveContactDetail");
            }
        }

        /// <summary>
        /// Проверьте, что CreateObject возвращает допустимый объект для типов в модели
        /// </summary>
        [TestMethod]
        public void CreateObject()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);

                object entity = unit.CreateObject<Department>();
                Assert.IsInstanceOfType(entity, typeof(Department), "Department did not get created.");

                entity = unit.CreateObject<Employee>();
                Assert.IsInstanceOfType(entity, typeof(Employee), "Employee did not get created.");

                entity = unit.CreateObject<Email>();
                Assert.IsInstanceOfType(entity, typeof(Email), "Email did not get created.");

                entity = unit.CreateObject<Phone>();
                Assert.IsInstanceOfType(entity, typeof(Phone), "Phone did not get created.");

                entity = unit.CreateObject<Address>();
                Assert.IsInstanceOfType(entity, typeof(Address), "Address did not get created.");
            }
        }

        /// <summary>
        /// Проверьте, что отдел добавлен к базовому контексту
        /// </summary>
        [TestMethod]
        public void AddDepartment()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);

                Department dep = new Department();
                unit.AddDepartment(dep);
                Assert.IsTrue(ctx.Departments.Contains(dep), "Department was not added to underlying context.");
            }
        }

        /// <summary>
        /// Проверьте исключение при добавлении отдела, который уже находится в базовом контексте
        /// </summary>
        [TestMethod]
        public void AddDepartmentAlreadyInUnitOfWork()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);

                Department dep = new Department();
                unit.AddDepartment(dep);

                try
                {
                    unit.AddDepartment(dep);
                    Assert.Fail("Adding an Department that was already added did not throw.");
                }
                catch (InvalidOperationException ex)
                {
                    Assert.AreEqual("The supplied Department is already part of this Unit of Work.", ex.Message);
                }
            }
        }

        /// <summary>
        /// Проверьте, что сотрудник добавлен к базовому контексту
        /// </summary>
        [TestMethod]
        public void AddEmployee()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);

                Employee emp = new Employee();
                unit.AddEmployee(emp);
                Assert.IsTrue(ctx.Employees.Contains(emp), "Employee was not added to underlying context.");
            }
        }

        /// <summary>
        /// Проверьте исключение при добавлении сотрудника, который уже находится в базовом контексте
        /// </summary>
        [TestMethod]
        public void AddEmployeeAlreadyInUnitOfWork()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);

                Employee emp = new Employee();
                unit.AddEmployee(emp);

                try
                {
                    unit.AddEmployee(emp);
                    Assert.Fail("Adding an Employee that was already added did not throw.");
                }
                catch (InvalidOperationException ex)
                {
                    Assert.AreEqual("The supplied Employee is already part of this Unit of Work.", ex.Message);
                }
            }
        }

        /// <summary>
        /// Проверьте, что контактные данные добавлены к базовому контексту
        /// Контактные данные, созданные при вызове конструкторе на классе
        /// </summary>
        [TestMethod]
        public void AddContactDetailFromDefaultConstructor()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);
                Employee emp = new Employee();
                unit.AddEmployee(emp);

                ContactDetail cd = new Address();
                unit.AddContactDetail(emp, cd);
                Assert.IsTrue(ctx.ContactDetails.Contains(cd), "ContactDetail was not added to underlying context.");
            }
        }

        /// <summary>
        /// Проверьте исключение при добавлении контактных данных, которые уже находятся в базовом контексте
        /// </summary>
        [TestMethod]
        public void AddContactDetailAlreadyInUnitOfWork()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);

                Employee emp = new Employee();
                ContactDetail detail = new Phone();
                unit.AddEmployee(emp);
                unit.AddContactDetail(emp, detail);

                try
                {
                    unit.AddContactDetail(emp, detail);
                    Assert.Fail("Adding an ContactDetail that was already added did not throw.");
                }
                catch (InvalidOperationException ex)
                {
                    Assert.AreEqual("The supplied Phone is already part of this Unit of Work.", ex.Message);
                }
            }
        }

        /// <summary>
        /// Проверьте исключение при добавлении контактных данных сотруднику, который не находится в базовом контексте
        /// </summary>
        [TestMethod]
        public void AddContactDetailToEmployeeOutsideUnitOfWork()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);
                Employee emp = new Employee();
                ContactDetail detail = new Email();

                try
                {
                    unit.AddContactDetail(emp, detail);
                    Assert.Fail("Adding a contact detail to an employee outside the Unit of Work did not throw.");
                }
                catch (InvalidOperationException ex)
                {
                    Assert.AreEqual("The supplied Employee is not part of this Unit of Work.", ex.Message);
                }
            }
        }

        /// <summary>
        /// Проверьте, что отдел может быть удален из базового контекста
        /// </summary>
        [TestMethod]
        public void RemoveDepartment()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);
                Department dep = new Department();
                unit.AddDepartment(dep);

                unit.RemoveDepartment(dep);
                Assert.IsFalse(ctx.Departments.Contains(dep), "Department was not removed from underlying context.");
            }
        }

        /// <summary>
        /// Проверьте, что сотрудники уволены из отдела, если их отдел удален
        /// </summary>
        [TestMethod]
        public void RemoveDepartmentWithEmployees()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);
                Department dep = new Department();
                Employee emp = new Employee();
                unit.AddDepartment(dep);
                unit.AddEmployee(emp);
                emp.Department = dep;

                unit.RemoveDepartment(dep);
                Assert.IsFalse(ctx.Departments.Contains(dep), "Department was not removed from underlying context.");
                Assert.IsNull(emp.Department, "Employee.Department property has not been nulled when deleting department.");
                Assert.IsNull(emp.DepartmentId, "Employee.DepartmentId property has not been nulled when deleting department.");
                Assert.AreEqual(0, dep.Employees.Count, "Department.Employees collection was not cleared when deleting department.");
            }
        }

        /// <summary>
        /// Проверьте исключение при удалении отдела, не находящегося в текущем базовом контексте
        /// </summary>
        [TestMethod]
        public void RemoveDepartmentOutsideUnitOfWork()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);
                try
                {
                    unit.RemoveDepartment(new Department());
                    Assert.Fail("Removing a Department that was not added to Unit of Work did not throw.");
                }
                catch (InvalidOperationException ex)
                {
                    Assert.AreEqual("The supplied Department is not part of this Unit of Work.", ex.Message);
                }
            }
        }

        /// <summary>
        /// Проверьте, что сотрудник может быть удален из базового контекста
        /// </summary>
        [TestMethod]
        public void RemoveEmployee()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);
                Employee emp = new Employee();
                unit.AddEmployee(emp);

                unit.RemoveEmployee(emp);
                Assert.IsFalse(ctx.Employees.Contains(emp), "Employee was not removed from underlying context.");
            }
        }

        /// <summary>
        /// Проверьте, что сотрудник может быть удален из базового контекста
        /// И этот сотрудник становится не назначенным руководителю
        /// </summary>
        [TestMethod]
        public void RemoveEmployeeWithManager()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);
                Employee emp = new Employee();
                Employee man = new Employee();
                unit.AddEmployee(emp);
                unit.AddEmployee(man);
                emp.Manager = man;

                unit.RemoveEmployee(emp);
                Assert.IsFalse(ctx.Employees.Contains(emp), "Employee was not removed from underlying context.");
                Assert.AreEqual(0, man.Reports.Count, "Employee was not removed from managers reports.");
                Assert.IsNull(emp.Manager, "Manager property on Employee was not cleared.");
            }
        }

        /// <summary>
        /// Проверьте, что сотрудник может быть удален из базового контекста
        /// И что отчеты становятся не назначенными
        /// </summary>
        [TestMethod]
        public void RemoveEmployeeWithReports()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);
                Employee emp = new Employee();
                Employee man = new Employee();
                unit.AddEmployee(emp);
                unit.AddEmployee(man);
                emp.Manager = man;

                unit.RemoveEmployee(man);
                Assert.IsFalse(ctx.Employees.Contains(man), "Employee was not removed from underlying context.");
                Assert.AreEqual(0, man.Reports.Count, "Employee was not removed from managers reports.");
                Assert.IsNull(emp.Manager, "Manager property on Employee was not cleared.");
            }
        }

        /// <summary>
        /// Проверьте исключение при удалении работника, не находящегося в текущем базовом контексте
        /// </summary>
        [TestMethod]
        public void RemoveEmployeeOutsideUnitOfWork()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);

                try
                {
                    unit.RemoveEmployee(new Employee());
                    Assert.Fail("Removing an Employee that was not added to Unit of Work did not throw.");
                }
                catch (InvalidOperationException ex)
                {
                    Assert.AreEqual("The supplied Employee is not part of this Unit of Work.", ex.Message);
                }
            }
        }

        /// <summary>
        /// Проверьте, что контактные данные могут быть удалены из базового контекста
        /// </summary>
        [TestMethod]
        public void RemoveContactDetail()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);

                Employee emp = new Employee();
                ContactDetail detail = new Phone();
                unit.AddEmployee(emp);
                unit.AddContactDetail(emp, detail);

                unit.RemoveContactDetail(emp, detail);
                Assert.IsFalse(ctx.ContactDetails.Contains(detail), "ContactDetail was not removed from underlying context.");
                Assert.IsFalse(
                    emp.ContactDetails.Contains(detail),
                    "ContactDetail is still in collection on Employee after being removed via Unit of Work.");
            }
        }

        /// <summary>
        /// Проверьте исключение при удалении контактных данных, не находящихся в базовом контексте
        /// </summary>
        [TestMethod]
        public void RemoveContactDetailOutsideUnitOfWork()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                UnitOfWork unit = new UnitOfWork(ctx);
                try
                {
                    unit.RemoveContactDetail(new Employee(), new Address());
                    Assert.Fail("Removing a ContactDetail that was not added to Unit of Work did not throw.");
                }
                catch (InvalidOperationException ex)
                {
                    Assert.AreEqual("The supplied Address is not part of this Unit of Work.", ex.Message);
                }
            }
        }
    }
}
