using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestConsentCollector.AutomatedTests.Helpers;
using TestConsentCollector.AutomatedTests.PageObjects;

namespace TestConsentCollector.AutomatedTests.Tests
{
    [TestClass]
    public class LoginTests : Browser, IDisposable
    {
        private LoginPage loginPage;
        private DashboardPage dashboardPage;


        public LoginTests()
        {
            Driver.Navigate()
                .GoToUrl("http://localhost:4200/authentication/login");
            loginPage = new LoginPage(Driver);
        }



        //[TestMethod]
        //public void Login_With_Valid_Credentials()
        //{
        //    loginPage.Login("nicoleta99", "Abcd1234*");
        //    dashboardPage = new DashboardPage(Driver);
        //    dashboardPage.WaitForPageToLoad("div");
        //    Assert.IsTrue(dashboardPage.ProfileMenu.Displayed);
        //}

        //[TestMethod]
        //public void Login_With_Invalid_Password()
        //{
        //    loginPage.Login("test", "test1");
        //    loginPage.WaitForPageToLoad(".alert-danger");
        //    Assert.AreEqual("Invalid Login Credentials", loginPage.ErrInvalidCred.Text);
        //}

        public void Dispose()
        {
            CloseBrowser();
        }
    }
}
