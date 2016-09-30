﻿// (c) Корпорация Майкрософт (Microsoft Corp.). Все права защищены.

namespace Tests.ViewModel
{
    using System;
    using EmployeeTracker.Model;
    using EmployeeTracker.ViewModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests;

    /// <summary>
    /// Модульные тесты для объектов BasicEmployeeViewModelTest
    /// </summary>
    [TestClass]
    public class BasicEmployeeViewModelTests
    {
        /// <summary>
        /// Проверьте методы getter (считывание) и setter (присвоение) на модели ViewModel, зависящей от изменений базовых данных и уведомлений
        /// </summary>
        [TestMethod]
        public void PropertyGetAndSet()
        {
            // Проверьте, что исходные свойства отображаются в модели ViewModel
            Employee em = new Employee
            {
                Title = "Title",
                FirstName = "FirstName",
                LastName = "LastName",
                Position = "Position",
                BirthDate = new DateTime(2000, 1, 1),
                HireDate = new DateTime(2000, 2, 2)
            };
            BasicEmployeeViewModel vm = new BasicEmployeeViewModel(em);
            Assert.AreEqual(em, vm.Model, "Bound object property did not return object from model.");
            Assert.AreEqual("Title", vm.Title, "Title property did not return value from model.");
            Assert.AreEqual("FirstName", vm.FirstName, "FirstName property did not return value from model.");
            Assert.AreEqual("LastName", vm.LastName, "LastName property did not return value from model.");
            Assert.AreEqual("Position", vm.Position, "Position property did not return value from model.");
            Assert.AreEqual(new DateTime(2000, 1, 1), vm.BirthDate, "BirthDate property did not return value from model.");
            Assert.AreEqual(new DateTime(2000, 2, 2), vm.HireDate, "HireDate property did not return value from model.");
            Assert.AreEqual(null, vm.TerminationDate, "TerminationDate property did not return value from model.");

            // Проверьте, что при изменении свойств обновляется модель и возникает событие PropertyChanged
            string lastProperty;
            vm.PropertyChanged += (sender, e) => { lastProperty = e.PropertyName; };

            lastProperty = null;
            vm.Title = "NEW_Title";
            Assert.AreEqual("Title", lastProperty, "Setting Title property did not raise correct PropertyChanged event.");
            Assert.AreEqual("NEW_Title", em.Title, "Setting Title property did not update model.");

            lastProperty = null;
            vm.FirstName = "NEW_FirstName";
            Assert.AreEqual("FirstName", lastProperty, "Setting FirstName property did not raise correct PropertyChanged event.");
            Assert.AreEqual("NEW_FirstName", em.FirstName, "Setting FirstName property did not update model.");

            lastProperty = null;
            vm.LastName = "NEW_LastName";
            Assert.AreEqual("LastName", lastProperty, "Setting LastName property did not raise correct PropertyChanged event.");
            Assert.AreEqual("NEW_LastName", em.LastName, "Setting LastName property did not update model.");

            lastProperty = null;
            vm.Position = "NEW_Position";
            Assert.AreEqual("Position", lastProperty, "Setting Position property did not raise correct PropertyChanged event.");
            Assert.AreEqual("NEW_Position", em.Position, "Setting Position property did not update model.");

            lastProperty = null;
            vm.BirthDate = new DateTime(2001, 1, 1);
            Assert.AreEqual("BirthDate", lastProperty, "Setting BirthDate property did not raise correct PropertyChanged event.");
            Assert.AreEqual(new DateTime(2001, 1, 1), em.BirthDate, "Setting BirthDate property did not update model.");

            lastProperty = null;
            vm.HireDate = new DateTime(2001, 2, 2);
            Assert.AreEqual("HireDate", lastProperty, "Setting HireDate property did not raise correct PropertyChanged event.");
            Assert.AreEqual(new DateTime(2001, 2, 2), em.HireDate, "Setting HireDate property did not update model.");

            lastProperty = null;
            vm.TerminationDate = new DateTime(2001, 3, 3);
            Assert.AreEqual("TerminationDate", lastProperty, "Setting TerminationDate property did not raise correct PropertyChanged event.");
            Assert.AreEqual(new DateTime(2001, 3, 3), em.TerminationDate, "Setting TerminationDate property did not update model.");
        }

        /// <summary>
        /// Проверьте, что метод getter (считывание) отражает изменения в модели
        /// </summary>
        [TestMethod]
        public void ModelChangesFlowToProperties()
        {
            // Проверьте, что ViewModel возвращает текущее значение для модели
            Employee em = new Employee
            {
                Title = "Title",
                FirstName = "FirstName",
                LastName = "LastName",
                Position = "Position",
                BirthDate = new DateTime(2000, 1, 1),
                HireDate = new DateTime(2000, 2, 2)
            };
            BasicEmployeeViewModel vm = new BasicEmployeeViewModel(em);

            em.Title = "NEW_Title";
            Assert.AreEqual("NEW_Title", vm.Title, "Title property is not fetching the value from the model.");
            em.FirstName = "NEW_FirstName";
            Assert.AreEqual("NEW_FirstName", vm.FirstName, "FirstName property is not fetching the value from the model.");
            em.LastName = "NEW_LastName";
            Assert.AreEqual("NEW_LastName", vm.LastName, "LastName property is not fetching the value from the model.");
            em.Position = "NEW_Position";
            Assert.AreEqual("NEW_Position", vm.Position, "Position property is not fetching the value from the model.");
            em.BirthDate = new DateTime(2001, 1, 1);
            Assert.AreEqual(new DateTime(2001, 1, 1), vm.BirthDate, "BirthDate property is not fetching the value from the model.");
            em.HireDate = new DateTime(2001, 2, 2);
            Assert.AreEqual(new DateTime(2001, 2, 2), vm.HireDate, "HireDate property is not fetching the value from the model.");
            em.TerminationDate = new DateTime(2001, 3, 3);
            Assert.AreEqual(new DateTime(2001, 3, 3), vm.TerminationDate, "TerminationDate property is not fetching the value from the model.");
        }

        /// <summary>
        /// Проверьте, что если значение null недопустимо, вызываются исключения NullArgumentException
        /// </summary>
        [TestMethod]
        public void CheckNullArgumentExceptions()
        {
            Utilities.CheckNullArgumentException(() => { new BasicEmployeeViewModel(null); }, "employee", "ctor");
        }
    }
}
