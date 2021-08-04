using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace TestConsentCollector.AutomatedTests.PageObjects
{
    public class DashboardPage : BasePageObjectModel
    {
        public DashboardPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(this.driver, TimeSpan.FromSeconds(20)));
        }

        [FindsBy(How = How.CssSelector, Using = "btn")]
        public IWebElement ProfileMenu { get; set; }

    }
}
