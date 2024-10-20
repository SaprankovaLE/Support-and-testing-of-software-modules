using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

class Program
{
    static void Main(string[] args)
    {
        using (IWebDriver driver = new ChromeDriver())
        {
            // Открываем веб-страницу
            driver.Navigate().GoToUrl("https://ci.nsu.ru/");

            IWebElement element1 = driver.FindElement(By.XPath("//footer"));
            Console.WriteLine("Найденный элемент: " + element1.Text);
            System.Threading.Thread.Sleep(10000);

            IWebElement element2 = driver.FindElement(By.Id("main-content"));
            Console.WriteLine("Найденный элемент: " + element2.Text);
            System.Threading.Thread.Sleep(10000);

            IWebElement element3 = driver.FindElement(By.ClassName("h1"));
            Console.WriteLine("Найденный элемент: " + element3.Text);
            System.Threading.Thread.Sleep(10000);

            driver.Quit();
        }
    }
}
