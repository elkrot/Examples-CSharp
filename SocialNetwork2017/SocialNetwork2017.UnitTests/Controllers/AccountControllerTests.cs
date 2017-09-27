using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetwork2017.BL;
using SocialNetwork2017.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace SocialNetwork2017.Controllers.Tests
{
    [TestClass()]
    public class AccountControllerTests
    {
        [TestMethod()]
        public void RegisterTest()
        {

        }
        //(Description = "Метод GetMembershipCreateStatusResultText() возвращает текст ошибки DublicateEmail")
        [TestMethod]
        public void GetMembershipCreateStatusResultTextMethodReturnsDublicateEmailErrorText()
        {
            //Arrange
            AccountController controller = new AccountController(new DataManager(null, null, null, null, null));

            //Act
            string result = controller.GetMembershipCreateStatusResultText(MembershipCreateStatus.DuplicateEmail);

            //Assert
            Assert.AreEqual("Пользователь с таким email адресом уже существует", result);
        }
        //"Метод GetMembershipCreateStatusResultText() возвращает текст ошибки DublicateUserName"
        [TestMethod]
        public void GetMembershipCreateStatusResultTextMethodReturnsDublicateUserNameErrorText()
        {
            //Arrange
            AccountController controller = new AccountController(new DataManager(null, null, null, null, null));

            //Act
            string result = controller.GetMembershipCreateStatusResultText(MembershipCreateStatus.DuplicateUserName);

            //Assert
            Assert.AreEqual("Пользователь с таким именем уже существует", result);
        }

        //"Метод GetMembershipCreateStatusResultText() возвращает текст ошибки Неизвестная ошибка"
        [TestMethod]
        public void GetMembershipCreateStatusResultTextMethodReturnsUnknownErrorText()
        {
            //Arrange
            AccountController controller = new AccountController(new DataManager(null, null, null, null, null));

            //Act
            string result = controller.GetMembershipCreateStatusResultText(MembershipCreateStatus.InvalidEmail);

            //Assert
            Assert.AreEqual("Неизвестная ошибка", result);
        }

    }
}