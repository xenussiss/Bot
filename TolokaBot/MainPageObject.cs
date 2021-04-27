using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Threading;
using System.Linq;

namespace TolokaBot
{
    class MainPageObject
    {
       
        private IWebDriver _driver;  

        private readonly By _UnableButton = By.XPath("//*[text()='Недоступные']");
        private readonly By _AllCategoriesButton = By.XPath("//*[text()='Все категории']");
        private readonly By _PairwiseСomparisonButton = By.XPath("//*[text()='Попарное сравнение']");

        private readonly By MainClass = By.ClassName("snippet__task-info");
        private readonly By ElementClass = By.ClassName("snippet__title");
        private readonly By ButtonClass = By.ClassName("snippet__take-btn");

        
                
        private readonly string _bok_o_bok_poisk_text = "Бок-о-бок, поиск";
        private readonly string _bok_o_bok_sravnenie_organizacii = "🔰 Бок-о-Бок: сравнение организаций";


        public MainPageObject(IWebDriver driver)
        {
            _driver = driver;
        }

        public void PressingButtons() //находит и тыкает флажки для уменьшения поля поиска
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(25);
            var unableButton = _driver.FindElement(_UnableButton);
            unableButton.Click();
            Thread.Sleep(500);
            var allCategories = _driver.FindElement(_AllCategoriesButton);
            allCategories.Click();
            Thread.Sleep(500);
            var pairwiseComparation = _driver.FindElement(_PairwiseСomparisonButton);
            pairwiseComparation.Click();
            Thread.Sleep(800);
        }

        public void Search()
        {                     
            Search_Bok_o_bok_poisk();
            
            if (!Flags.Bok_o_bok_poisk_FOUNDED)
            {
                Search_Bok_o_bok2();
                
            }
            
        }

        public void Search_Bok_o_bok_poisk()
        {
            
            //список всех "дедушек" - динамич.эл-тов.
            IList<IWebElement> AncientElements = _driver.FindElements(MainClass);
            

            foreach (IWebElement e in AncientElements)
            {
                
                IList<IWebElement> Elements =  e.FindElements(ElementClass);
                //поиск нужного эл-та
                
                foreach (IWebElement eChild in Elements)
                {
                    
                    Console.WriteLine(eChild.Text);
                    bool found = eChild.Text == _bok_o_bok_poisk_text;
                    

                    if (found)
                    {
                        //нажатие кнопки "приступить" 
                        var button = e.FindElements(ButtonClass).FirstOrDefault();
                        
                        if (button != null)
                        {
                            Flags.Bok_o_bok_poisk_FOUNDED = true;
                            button.Click();
                            Flags.Bok_o_bok_poisk_FOUND_and_READY = true;
                                                        
                            break;
                        }
                        break;
                        
                    }
                    
                }

                if(Flags.Bok_o_bok_poisk_FOUND_and_READY == true)
                {
                    break;
                }
               
                
            }
                       
        }
        public void Search_Bok_o_bok2()
        {
            IList<IWebElement> AncientElements = _driver.FindElements(MainClass);

            foreach (IWebElement e in AncientElements)
            {
                IList<IWebElement> Elements = e.FindElements(ElementClass);
               
                foreach (IWebElement eChild in Elements)
                {
                    Console.WriteLine(eChild.Text);
                    bool found = eChild.Text == _bok_o_bok_sravnenie_organizacii;

                    if (found)
                    {
                        
                        var button = e.FindElements(ButtonClass).FirstOrDefault();
                        if (button != null)
                        {
                            Flags.Bok_o_bok_sravnenie_organizacii_FOUNDED = true;
                            button.Click();
                            Flags.Bok_o_bok_sravnenie_organizacii_FOUND_and_READY = true;
                            break;
                        }
                        break;
                    }
                }
                if (Flags.Bok_o_bok_sravnenie_organizacii_FOUND_and_READY == true)
                {
                    break;
                }
            }

        }
               
    }

}
