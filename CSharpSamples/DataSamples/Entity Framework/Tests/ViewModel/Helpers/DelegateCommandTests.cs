// (c) Корпорация Майкрософт (Microsoft Corp.). Все права защищены.

namespace Tests.ViewModel.Helpers
{
    using System;
    using EmployeeTracker.ViewModel.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Модульные тесты для DelegateCommand
    /// </summary>
    [TestClass]
    public class DelegateCommandTests
    {
        /// <summary>
        /// Сконструируйте команду без предиката, заданного для CanExecute
        /// Проверьте, что CanExecute всегда имеет значение true и что эта команда выполняется, если задано значение null
        /// </summary>
        [TestMethod]
        public void ExecuteNoPredicateWithNull()
        {
            bool called = false;
            DelegateCommand cmd = new DelegateCommand((o) => called = true);
            Assert.IsTrue(cmd.CanExecute(null), "Command should always be able to execute when no predicate is supplied.");
            cmd.Execute(null);
            Assert.IsTrue(called, "Command did not run supplied Action.");
        }

        /// <summary>
        /// Сконструируйте команду с предикатом null
        /// </summary>
        [TestMethod]
        public void ConstructorAcceptsNullPredicate()
        {
            DelegateCommand cmd = new DelegateCommand((o) => { }, null);
            Assert.IsTrue(cmd.CanExecute(null), "Command with null specified for predicate should always be able to execute.");
        }

        /// <summary>
        /// Сконструируйте команду без предиката, заданного для CanExecute
        /// Проверьте, что CanExecute всегда имеет значение true, и что эта команда выполняется, если задан объект
        /// </summary>
        [TestMethod]
        public void ExecuteNoPredicateWithArgument()
        {
            bool called = false;
            DelegateCommand cmd = new DelegateCommand((o) => called = true);
            Assert.IsTrue(cmd.CanExecute("x"), "Command should always be able to execute when no predicate is supplied.");
            cmd.Execute("x");
            Assert.IsTrue(called, "Command did not run supplied Action.");
        }

        /// <summary>
        /// Сконструируйте команду с предикатом "true", заданным для CanExecute
        /// Проверьте CanExecute и что выполняется эта команда
        /// </summary>
        [TestMethod]
        public void ExecuteWithPredicate()
        {
            bool called = false;
            DelegateCommand cmd = new DelegateCommand((o) => called = true, (o) => true);
            Assert.IsTrue(cmd.CanExecute(null), "Command should be able to execute when predicate returns true.");
            cmd.Execute(null);
            Assert.IsTrue(called, "Command did not run supplied Action.");
        }

        /// <summary>
        /// Сконструируйте команду с предикатом "false", заданным для CanExecute
        /// Проверьте CanExecute, и что возникает попытка выполнения
        /// </summary>
        [TestMethod]
        public void AttemptExecuteWithFalsePredicate()
        {
            bool called = false;
            DelegateCommand cmd = new DelegateCommand((o) => called = true, (o) => false);
            Assert.IsFalse(cmd.CanExecute(null), "Command should not be able to execute when predicate returns false.");

            try
            {
                cmd.Execute(null);
            }
            catch (InvalidOperationException)
            {
            }

            Assert.IsFalse(called, "Command should not have run supplied Action.");
        }

        /// <summary>
        /// Проверьте, что если значение null недопустимо, вызываются исключения NullArgumentException
        /// </summary>
        [TestMethod]
        public void CheckNullArgumentExceptions()
        {
            Utilities.CheckNullArgumentException(() => { new DelegateCommand(null); }, "execute", "ctor");
        }
    }
}
