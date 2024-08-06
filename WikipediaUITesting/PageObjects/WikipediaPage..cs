using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace WikipediaUITesting.PageObjects
{
    public class WikipediaPage
    {
        private readonly IWebDriver _driver;

        public WikipediaPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string Title => _driver.Title;

        public string GetTextContentFromTestDrivenDevelopmentSection()
        {
            var section = _driver.FindElement(By.XPath("/html/body/div[2]/div/div[3]/main/div[3]/div[3]/div[1]/div[13]/h3"));
            Console.WriteLine(section.Text);
            return section.Text;

        }
    }
}