using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PromoSeeker
{
    internal static class Pepper
    {
        public static List<Tweet> GetData(List<string> wordsList)
        {
            //hide chromium window
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("headless");

            IWebDriver driver = new ChromeDriver(chromeOptions);
            driver.Navigate().GoToUrl("https://www.pepper.pl/nowe");

            //find cookie accept button and click it
            IWebElement cookieAcceptButton = driver.FindElement(By.XPath("//button[.=' Akceptuj wszystkie ']"));
            System.Threading.Thread.Sleep(1000);
            cookieAcceptButton.Click();

            //znajdź wszystkie pozycje i zrób z nich listę Iwebelementów
            IList<IWebElement> allArticles = driver.FindElements(By.TagName("article"));

            var articleList = new List<Tweet>();

            foreach (var article in allArticles)
            {
                foreach (var words in wordsList)
                {
                    if (article.Text.ToLower().Contains(words.ToLower()))
                    {
                        articleList.Add(new Tweet
                        {
                            Id = Regex.Replace(article.GetAttribute("id"), "[^0-9]", ""),
                            Name = article.FindElement(By.ClassName("thread-title")).Text,
                            Url = article.FindElement(By.TagName("a")).GetAttribute("href").ToString()
                        });
                    }
                }
            }

            driver.Close();
            driver.Quit();
            driver.Dispose();

            return articleList;
        }
    }
}
