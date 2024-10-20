using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

class Program
{
    static void Main(string[] args)
    {
        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl("https://vk.com/video");

            // Ждем, пока страницы загрузятся
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Получаем ссылки на видео в секциях "Для вас" и "Тренды"
            List<string> videoUrls = new List<string>();
            var sections = new List<string> { "Для вас", "Тренды" };

            foreach (var section in sections)
            {
                // Ищем каждый раздел по отсутствию надписи
                var sectionElement = driver.FindElement(By.XPath($"//a[text()='{section}']"));
                var videoLinks = sectionElement.FindElements(By.XPath("./following-sibling::div//a[contains(@href, '/video')]"));
                foreach (var link in videoLinks)
                {  
                    videoUrls.Add(link.GetAttribute("href"));
                }
            }
            // Сохраняем количество ссылок в файл
            File.WriteAllText("video_links_count.txt", videoUrls.Count.ToString());           
            // Проводим обработку видео
            using (StreamWriter writer = new StreamWriter("video_info.txt"))
            {
                foreach (var url in videoUrls)
                {
                    try
                    {
                        driver.Navigate().GoToUrl(url);
                        driver.Title.Contains("Видеозапись");
                        string title = driver.FindElement(By.ClassName("video_page_title")).Text;
                        string views = driver.FindElement(By.ClassName("video_views")).Text;
                        string likes = driver.FindElement(By.ClassName("video_likes")).Text;
                        string date = driver.FindElement(By.ClassName("video_date")).Text;
                        string channelName = driver.FindElement(By.ClassName("video_owner_name")).Text;
                        string subscriberCount = driver.FindElement(By.ClassName("video_owner_subs")).Text;
                        writer.WriteLine($"{title}, {views}, {likes}, {date}, {channelName}, {subscriberCount}");
                    }
                    catch (NoSuchElementException e)
                    {
                        Console.WriteLine($"Ошибка при обработке видео: {url}. Ошибка: {e.Message}"); 
                    }
                   catch (Exception e)
                    {
                        Console.WriteLine($"Ошибка: {e.Message}");
                    }
                }
            }
        }
    }
}