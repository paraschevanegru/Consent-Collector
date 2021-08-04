using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace TestConsentCollector.AutomatedTests.PageObjects
{
    public class LoginPage : BasePageObjectModel
    {

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(this.driver, TimeSpan.FromSeconds(10)));
        }

        #region Login section

        [FindsBy(How = How.Id, Using = "username")]
        public IWebElement TxtUsername { get; set; }


        [FindsBy(How = How.Id, Using = "password")]
        public IWebElement TxtPassword { get; set; }

        [FindsBy(How = How.Id, Using = "submitButton")]
        public IWebElement BtnLogin { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".alert-danger")]
        public IWebElement ErrInvalidCred { get; set; }
        #endregion

        public void Login(string username, string password)
        {
            TxtUsername.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
        }
    }
}
