using OpenQA.Selenium;
using System;
using System.Threading;

namespace TolokaBot
{
    class PasswordPageObject
    {
        private IWebDriver _driver;

        private readonly By _password = By.CssSelector(".Textinput-Control");
        private readonly By _passwordButton = By.XPath("/html/body/div/div/div[2]/div[2]/div/div/div[2]/div[3]/div/div/div/form/div[3]/button");


        public PasswordPageObject(IWebDriver driver)
        {
            _driver = driver;
        }
        public MainPageObject Password(string passwordText)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
            _driver.FindElement(_password).SendKeys(passwordText);
            _driver.FindElement(_passwordButton).Click();
            Thread.Sleep(300);
            return new MainPageObject(_driver);
        }
    }
}
