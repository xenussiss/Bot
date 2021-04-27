using OpenQA.Selenium;
using System;
using System.Threading;

namespace TolokaBot
{
    class LoginPageObject 
    {
        private IWebDriver _driver;

        private readonly By _login = By.CssSelector("#passp-field-login");
        private readonly By _loginButton = By.XPath("/html/body/div/div/div[2]/div[2]/div/div/div[2]/div[3]/div/div/div/div[1]/form/div[3]/button");
        

        public LoginPageObject(IWebDriver driver)
        {
            _driver = driver;
        }
        public PasswordPageObject Login(string loginText)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
            _driver.FindElement(_login).SendKeys(loginText);
            _driver.FindElement(_loginButton).Click();
            Thread.Sleep(300);
            return new PasswordPageObject(_driver);
        }

    }
}
