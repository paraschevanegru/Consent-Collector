using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace TestConsentCollector.AutomatedTests.PageObjects
{
    public class BasePageObjectModel
    {
        //am mutat metoda WaitForPageToLoad din toate clasele aici pentru a evita duplicarea codului

        public IWebDriver driver;

        public void WaitForPageToLoad(string selector)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(selector)));
        }


    }
}
