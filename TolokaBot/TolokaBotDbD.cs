using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Threading;


namespace TolokaBot
{
    //������ ��������� �������� ������� � �������� �������� Selenium Web Driver � �#. �������������� � ������������� ��������������� �����.
    //����������������� ���� �� ������������� � ����� � ����� ����������� ���������� ��� ������� � �������� "������.������"
    //��� �� ���������� �������� ��������, ��� ��� ��� ������� ����� ��������� �����, � ������� ��� �� ����� �������� ���������, �.� ��� ������������ ������� ������������ ���������� ������.
    //����� ���-�� ��������������, ��� ������������� ���� � ������� �������� � ������ ��������� � ������ �������� �������� � ���������� ������� �������� �� ���� �������� �������,
    //�, ��������, � ���������� (��� ������������� ������� ����). ����� ����� ���������� ����������� �������� ��������(����������), ������� ���������� ���� ����� � ����� ����������
    //�������� ����������, ��� ��������� ������ �����.
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
