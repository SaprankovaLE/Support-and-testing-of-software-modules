using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PythonWebAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Запуск веб-драйвера Chrome
            IWebDriver driver = new ChromeDriver();

            try
            {
                driver.Navigate().GoToUrl("https://www.python.org/");
                Console.WriteLine("Открыта страница Python.");
                System.Threading.Thread.Sleep(5000);

                var downloadsButton = driver.FindElement(By.LinkText("Downloads"));
                downloadsButton.Click();
                Console.WriteLine("Перейдено на страницу Downloads.");
                System.Threading.Thread.Sleep(5000);

                var searchBox = driver.FindElement(By.Name("q"));
                var searchButton = driver.FindElement(By.Id("submit"));
                searchBox.SendKeys("python 3.11");
                Console.WriteLine("Текст 'python 3.11' был введен в поле поиска.");
                searchButton.Click();
                Console.WriteLine("Нажата кнопка 'GO'.");
                System.Threading.Thread.Sleep(5000);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
            finally
            {
                driver.Quit();
                Console.WriteLine("Браузер закрыт.");
            }
        }
    }
}
