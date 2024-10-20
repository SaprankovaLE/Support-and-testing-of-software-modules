using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

class Program
{
    static void Main(string[] args)
    {
        using (IWebDriver driver = new ChromeDriver()) 
        {
            driver.Navigate().GoToUrl("https://ci.nsu.ru/news");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Установка начальной и конечной даты
            driver.FindElement(By.CssSelector("input[id='arrFilter_DATE_ACTIVE_FROM_1']")).SendKeys("01.10.2020"); 
            driver.FindElement(By.CssSelector("input[id='arrFilter_DATE_ACTIVE_FROM_2']")).SendKeys("01.10.2024"); 
            driver.FindElement(By.CssSelector("button[class='btn btn-success filter-btn']")).Click(); 

            // Загрузка всех новостей с помощью кнопки "Загрузить ещё"
            var loadMoreButton = driver.FindElement(By.CssSelector("a[class='moreNewsList loadMoreButton']"));
            while (loadMoreButton.Displayed)
            {
                loadMoreButton.Click();
                System.Threading.Thread.Sleep(2000);
                try
                {
                    loadMoreButton = driver.FindElement(By.CssSelector("a[class='moreNewsList loadMoreButton']"));
                } catch (Exception ex)
                {
                    break;
                }
            }

            // Открываем файл для записи
            using (StreamWriter writer = new StreamWriter("result.txt"))
            {
                // Находим все карточки новостей
                var newsCards = driver.FindElements(By.CssSelector(".news-card"));
                foreach (var card in newsCards)
                {
                    // Извлекаем данные из каждой карточки
                    string date = card.FindElement(By.CssSelector("div.date")).Text;
                    string title = card.FindElement(By.CssSelector("a.name")).Text;
                    string link = card.FindElement(By.CssSelector("a.name")).GetAttribute("href");
                    string imageUrl = card.FindElement(By.CssSelector("img")).GetAttribute("src");

                    // Записываем данные в файл
                    writer.WriteLine($"{date}, {title}, {link}, {imageUrl}");
                }
            }
        }
    }
}