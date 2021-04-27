using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace TolokaBot
{
    class LogoStartPageObj
    {
        private IWebDriver _driver;

        private readonly By _startButton = By.XPath("/html/body/div[1]/div[2]/div/div[2]/div/div[1]/div[1]/div/div/button[1]/span");

        public LogoStartPageObj(IWebDriver driver)
        {
            _driver = driver;
        }

        public LoginPageObject SignIn()
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            _driver.FindElement(_startButton).Click();
            return new LoginPageObject(_driver);
        }
    }
}
