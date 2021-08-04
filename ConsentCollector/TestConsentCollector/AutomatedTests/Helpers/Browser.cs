using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestConsentCollector.AutomatedTests.Helpers
{
    public abstract class Browser
    {
        protected IWebDriver Driver;

        protected Browser()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
        }

        protected void CloseBrowser()
        {
            Driver.Close();
            Driver.Quit();

        }
    }
}
