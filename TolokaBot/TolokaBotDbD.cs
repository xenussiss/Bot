using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Threading;


namespace TolokaBot
{
    //Данная программа написана автором в процессе изучения Selenium Web Driver и С#. Использовалась в исключительно образовательных целях.
    //Работоспособность бота не гарантируется в связи с часто меняющимися элементами веб страниц в заданиях "Яндекс.Толока"
    //Так же необходимо обратить внимание, что все эти задания имеют обучающую часть, в которой бот не будет работать корректно, т.к там присутствует заранее определенные правильные ответы.
    //Автор так-же предостерегает, что использование бота с помощью акканута с низким рейтингом в данных заданиях приведет к исключению данного аккаунта из пула подобных заданий,
    //и, возможно, к блокировке (при множественном провале капч). Обход капчи реализован посредством звуковых сигналов(оповещений), поэтому необходимо быть рядом и иметь включенное
    //звуковое устройство, для успешного обхода капчи.
    public class TolokaBotDbD
    {
        public IWebDriver driver;



        [SetUp]
        public void Setup()
        {
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("https://toloka.yandex.ru");
        }

        [Test]
        public void Test1()
        {

            var logo = new LogoStartPageObj(driver);
            logo
                .SignIn();
            var Login = new LoginPageObject(driver);
            Login
                .Login(Data.L1);
            var Password = new PasswordPageObject(driver);
            Password
                .Password(Data.StartPassword);
            Thread.Sleep(6000);
            var Main = new MainPageObject(driver);
            Main.PressingButtons();
            
            Cycle();
            

        }

        public void Cycle()
        {
            var Main = new MainPageObject(driver);

            while (true)
            {
                if(!Flags.Bok_o_bok_poisk_FOUND_and_READY | !Flags.Bok_o_bok_sravnenie_organizacii_FOUND_and_READY)
                {
                    Main.Search();

                }
                
                
                if (Flags.Bok_o_bok_poisk_FOUND_and_READY)
                {
                    Thread.Sleep(2000);
                    var BotPoisk = new Bok_o_bok_poiskPageObject(driver);
                    BotPoisk.MainAction();
                }
                if (Flags.Bok_o_bok_sravnenie_organizacii_FOUND_and_READY)
                {
                    Thread.Sleep(2000);
                    var BotSravnenie = new Bok_o_bok_sravnenie_organizaciiPageObject(driver);
                    BotSravnenie.MainAction();
                }
            }


        }

        [TearDown]
        public void TearDown()
        {
            Assert.Pass();
        }

    }
    
 }
