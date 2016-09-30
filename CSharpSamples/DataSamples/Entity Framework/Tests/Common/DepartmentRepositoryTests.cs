// (c) Корпорация Майкрософт (Microsoft Corp.). Все права защищены.

namespace Tests.EntityFramework
{
    using System.Collections.Generic;
    using System.Linq;
    using EmployeeTracker.Common;
    using EmployeeTracker.Fakes;
    using EmployeeTracker.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Тестирует получение данных из объекта DepartmentObjectSetRepository
    /// </summary>
    [TestClass]
    public class DepartmentRepositoryTests
    {
        /// <summary>
        /// Проверьте, что GetAllDepartments возвращает все данные из базового набора ObjectSet
        /// </summary>
        [TestMethod]
        public void GetAllDepartments()
        {
            Department d1 = new Department();
            Department d2 = new Department();
            Department d3 = new Department();

            using (FakeEmployeeContext ctx = new FakeEmployeeContext(new Employee[] { }, new Department[] { d1, d2, d3 }))
            {
                DepartmentRepository rep = new DepartmentRepository(ctx);

                IEnumerable<Department> result = rep.GetAllDepartments();

                Assert.IsNotInstanceOfType(
                    result,
                    typeof(IQueryable),
                    "Repositories should not return IQueryable as this allows modification of the query that gets sent to the store. ");

                Assert.AreEqual(3, result.Count());
                Assert.IsTrue(result.Contains(d1));
                Assert.IsTrue(result.Contains(d2));
                Assert.IsTrue(result.Contains(d3));
            }
        }

        /// <summary>
        /// Проверьте, что GetAllDepartments возвращает пустой объект IEnumerable, если данные отсутствуют
        /// </summary>
        [TestMethod]
        public void GetAllDepartmentsEmpty()
        {
            using (FakeEmployeeContext ctx = new FakeEmployeeContext())
            {
                DepartmentRepository rep = new DepartmentRepository(ctx);

                IEnumerable<Department> result = rep.GetAllDepartments();
                Assert.AreEqual(0, result.Count());
            }
        }

        /// <summary>
        /// Проверьте, что при задании недопустимых параметров null вызывается ArgumentNullException
        /// </summary>
        [TestMethod]
        public void NullArgumentChecks()
        {
            Utilities.CheckNullArgumentException(() => { new DepartmentRepository(null); }, "context", "ctor");
        }
    }
}
