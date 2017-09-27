using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;

namespace SocialNetwork2017.IntegrationTests
{
    [TestClass]
    public class MyPageTests
    {
        public static string baseUrl = "http://localhost:3090/";
        //(Description = "Пользователь может войти на сайт, используя свои логин и пароль")]
        [TestMethod]
        public void UserCanEnter()
        {
            var driver = new FirefoxDriver();

            driver.Navigate().GoToUrl(baseUrl);

            driver.FindElement(By.Id("UserName")).SendKeys("ivan");
            driver.FindElement(By.Id("Password")).SendKeys("ivan");

            driver.FindElement(By.Id("LoginButton")).Click();

            Assert.AreEqual("Иванов Иван Иванович", driver.Title);
            Assert.AreEqual(baseUrl + "id2", driver.Url);
        }
    }
}
