using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;


namespace TolokaBot
{
    class Bok_o_bok_poiskPageObject
    {
        private IWebDriver _driver;

        private readonly By _nextStep = By.XPath("/html/body/div/div[1]/div/div[3]/div[2]/div[5]/svg/path");
        private readonly By _nextTask = By.XPath("//div[contains(@class, 'next-task')]");
                                                      
        //Desktop стандарт кнопки выбора
        private readonly By _leftButtonDesktop = By.XPath("//*[text()='Левый лучше']");
        private readonly By _middleButtonDesktop = By.XPath("//*[text()='Не могу сделать выбор']");
        private readonly By _rightButtonDesktop = By.XPath("//*[text()='Правый лучше']");

        private readonly By _taskCounter = By.ClassName("area-counter");
                                                     

        private readonly By _activeAnnotation = By.CssSelector(".paranja_shown");
        private readonly By _popUpCompleteButton = By.ClassName("base-modal-popup-footer__primary-button");
        private readonly By _captchaActive = By.CssSelector(".task-captcha__footer");
        private readonly By _mobileMode = By.CssSelector("device_touch");

        private bool mobileModeActive = false;
        private bool desktopModeActive = false;

        private int TaskCounter = 0;

        private bool TaskCompletedBySystem = false;

        Random random = new Random();
        public Bok_o_bok_poiskPageObject(IWebDriver driver)
        {
            _driver = driver;
        }



        public void MainAction()
        {
            //В задании неск. режимов (мобильнй и десктопный). 
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            while (!TaskCompletedBySystem)
            {
                
                CheckingMode();//Определение режима
                FindCounterValue();
                int counter = 0;
                if (mobileModeActive)
                {
                    while (counter <= TaskCounter--)//Определение количества пунктов в задании и выполнение действий по каждому пункту(кроме последнего)
                    {

                        MobilePatternPressingButtons();
                        counter++;
                    }
                    if (counter == TaskCounter) //В последнем пункте идет действие и проверка, посчитала ли система что все задания выполнены. Если да, то возврат к поиску
                    {
                        MobilePatternPressingButtons();
                        if (_driver.FindElement(_popUpCompleteButton).Displayed)
                        {
                            Flags.Bok_o_bok_poisk_FOUND_and_READY = false;
                            _driver.FindElement(_popUpCompleteButton).Click();
                            TaskCompletedBySystem = true;
                        }

                    }
                }
                if (desktopModeActive)
                {
                    while (counter <= TaskCounter--)
                    {

                        DesktopPatternPressingButtons();
                        counter++;
                    }
                    if (counter == TaskCounter)
                    {
                        DesktopPatternPressingButtons();
                        if (_driver.FindElement(_popUpCompleteButton).Displayed)
                        {
                            Flags.Bok_o_bok_poisk_FOUND_and_READY = false;
                            _driver.FindElement(_popUpCompleteButton).Click();
                            TaskCompletedBySystem = true;
                        }
                    }
                }
            }
        }

        private void DesktopPatternPressingButtons()
        {
            int r = random.Next(1, 3);
            
            if (!IsCaptchaPartPresent(_captchaActive))
            {
                if (r == 1)
                {
                    LeftPressingDesktop();
                }
                if (r == 2)
                {
                    MiddlePressingDesktop();
                }
                if (r == 3)
                {
                    RightPressingDesktop();
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
        private void MobilePatternPressingButtons()
        {
            int r = random.Next(1, 3);
                        
            if (!IsCaptchaPartPresent(_captchaActive))
            {
                if (r == 1)
                {
                    LeftPressingMobile();
                }
                if (r == 2)
                {
                    MiddlePressingMobile();
                }
                if (r == 3)
                {
                    RightPressingMobile();
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
        private bool IsMobilePartPresent(By by)
        {
            try
            {
                _driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        private bool IsCaptchaPartPresent(By by)
        {
            try
            {
                _driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        private void CheckingMode()
        {
            if (IsMobilePartPresent(_mobileMode))
            {

                mobileModeActive = true;
            }
            else
            {
                desktopModeActive = true;
            }
        }

        private void FindCounterValue()
        {
            IList<IWebElement> AncientElements = _driver.FindElements(_taskCounter);
            foreach(IWebElement e in AncientElements)
            {
                Console.WriteLine(e.Text);
                var realValue = e.Text;
                if (realValue == "6")
                {
                    TaskCounter = 6;
                }
                if (realValue == "7")
                {
                    TaskCounter = 7;
                }
                if (realValue == "8")
                {
                    TaskCounter = 8;
                }
                if (realValue == "9")
                {
                    TaskCounter = 9;
                }
                if (realValue == "10")
                {
                    TaskCounter = 10;
                }
                break;
            }
            
            

        }
        private void LeftPressingDesktop() //выплняет задание с нажатием на лев клав(desktop версия)
        {
            
            _driver.FindElement(_nextStep).Click();
            Thread.Sleep(50);
            _driver.FindElement(_leftButtonDesktop).Click();
            Thread.Sleep(50);
            _driver.FindElement(_nextStep).Click();
            Thread.Sleep(50);
            _driver.FindElement(_nextTask).Click();
        }

        private void MiddlePressingDesktop()
        {            
            _driver.FindElement(_nextStep).Click();
            Thread.Sleep(50);
            _driver.FindElement(_middleButtonDesktop).Click();
            Thread.Sleep(50);
            _driver.FindElement(_nextStep).Click();
            Thread.Sleep(50);
            _driver.FindElement(_nextTask).Click();

        }

        private void RightPressingDesktop()
        {
            
            _driver.FindElement(_nextStep).Click();
            Thread.Sleep(50);
            _driver.FindElement(_rightButtonDesktop).Click();
            Thread.Sleep(50);
            _driver.FindElement(_nextStep).Click();
            Thread.Sleep(50);
            _driver.FindElement(_nextTask).Click();

        }

        private void LeftPressingMobile()
        {
            _driver.FindElement(_middleButtonDesktop).Click();
            _driver.FindElement(_nextTask).Click();
        }

        private void MiddlePressingMobile()
        {
            _driver.FindElement(_rightButtonDesktop).Click();
            _driver.FindElement(_nextTask).Click();
        }

        private void RightPressingMobile()
        {
            _driver.FindElement(_leftButtonDesktop).Click();
            _driver.FindElement(_nextTask).Click();
        }
        private void CompareLeftPressingDesktop()
        {
            _driver.FindElement(_leftButtonDesktop).Click();
            _driver.FindElement(_nextTask).Click();
        }

        private void CompareMiddleDesktop()
        {
            _driver.FindElement(_middleButtonDesktop).Click();
            _driver.FindElement(_nextTask).Click();
        }
        private void CompareRightPressingDesktop()
        {
            _driver.FindElement(_rightButtonDesktop).Click();
            _driver.FindElement(_nextTask).Click();
        }
    }
}
