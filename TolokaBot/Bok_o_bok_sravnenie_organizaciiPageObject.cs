using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TolokaBot
{
    class Bok_o_bok_sravnenie_organizaciiPageObject
    {
        private IWebDriver _driver;

        private readonly By _nextStep = By.CssSelector("div.control:nth-child(2)");

        private readonly By _firstButton = By.CssSelector("div.task:nth-child(1) > div:nth-child(2) > section:nth-child(1) > label:nth-child(2)");
        private readonly By _secondButton = By.CssSelector("div.task:nth-child(1) > div:nth-child(2) > section:nth-child(1) > label:nth-child(4)");
        private readonly By _thirdButton = By.CssSelector("div.task:nth-child(1) > div:nth-child(2) > section:nth-child(1) > label:nth-child(6)");
        private readonly By _fourthButton = By.CssSelector("div.task:nth-child(1) > div:nth-child(2) > section:nth-child(1) > label:nth-child(8)");

        private readonly By _completeTaskButton = By.XPath("//*[text()='Отправить']");

        private readonly By _popUpCompleteButton = By.ClassName("base-modal-popup-footer__primary-button");
        private readonly By _captchaActive = By.CssSelector(".task-captcha__footer");

        private readonly By _taskCounter = By.CssSelector(".task-suite__counter");

        int TaskCounter = 0;
        Random random = new Random();

        private bool TaskCompletedBySystem = false;



        public Bok_o_bok_sravnenie_organizaciiPageObject(IWebDriver driver)
        {
            _driver = driver;
        }

        public void MainAction()
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            while (!TaskCompletedBySystem)
            {
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                FindCounterValue();

                int counter = 0;
                while (counter <= TaskCounter--)
                {
                    PressingButtonsPattern();
                    counter++;

                }
                if (counter == TaskCounter)
                {
                    PressingButtonsPattern();
                    if (_driver.FindElement(_popUpCompleteButton).Displayed)
                    {
                        Flags.Bok_o_bok_sravnenie_organizacii_FOUND_and_READY = false;


                        _driver.FindElement(_popUpCompleteButton).Click();
                        TaskCompletedBySystem = true;

                    }
                    else
                    {
                        PressingCompleteTaskButton();

                    }
                }
            }
        }



        private void FirstButtonPressing()
        {
            _driver.FindElement(_firstButton).Click();
            _driver.FindElement(_nextStep).Click();
            Thread.Sleep(300);
        }

        private void SecondButtonPressing()
        {
            _driver.FindElement(_secondButton).Click();
            _driver.FindElement(_nextStep).Click();
            Thread.Sleep(300);
        }

        private void ThirdButtonPressing()
        {
            _driver.FindElement(_thirdButton).Click();
            _driver.FindElement(_nextStep).Click();
            Thread.Sleep(300);
        }

        private void FourthButtonPressing()
        {
            _driver.FindElement(_fourthButton).Click();
            _driver.FindElement(_nextStep).Click();
            Thread.Sleep(300);
        }

        private void PressingCompleteTaskButton()
        {
            Thread.Sleep(100);
            _driver.FindElement(_completeTaskButton).Click();
        }
        private void FindCounterValue()
        {
            IList<IWebElement> Elements = _driver.FindElements(By.ClassName("task-suite"));
            foreach (IWebElement e in Elements)
            {

                IList<IWebElement> elem = e.FindElements(_taskCounter);
                foreach(IWebElement eChild in elem)
                {
                    Console.WriteLine(eChild.Text);
                    var realValue = _driver.FindElement(_taskCounter).Text;

                    if (realValue == "1 / 7")
                    {
                        TaskCounter = 7;
                    }
                    if (realValue == "1 / 8")
                    {
                        TaskCounter = 8;

                    }
                    if (realValue == "1 / 9")
                    {
                        TaskCounter = 9;
                    }
                    if (realValue == "1 / 10")
                    {
                        TaskCounter = 10;
                    }

                }
            }
            
        }


        private void PressingButtonsPattern()
        {

            int r = random.Next(1, 4);
            bool captchaPopUp = _driver.FindElement(_captchaActive).Displayed;
            if (!captchaPopUp)
            {
                if (r == 1)
                {
                    FirstButtonPressing();

                }
                if (r == 2)
                {
                    SecondButtonPressing();

                }
                if (r == 3)
                {
                    ThirdButtonPressing();

                }
                if (r == 4)
                {
                    FourthButtonPressing();

                }


            }
            else
            {
                Parallel.Invoke(

                    () => MethodsLib.CapthcaAlert(),
                    () => Thread.Sleep(120000)

                );
            }

        }



    }
}
