using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System;
using WikipediaUITesting.PageObjects;
using System.Threading;

namespace WikipediaUITesting.Tests
{
    public class WikipediaUITesting : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly WikipediaPage _wikipediaPage;

        public WikipediaUITesting()
        {
            _driver = new ChromeDriver();
            _wikipediaPage = new WikipediaPage(_driver);
        }

        [Fact]
        public void TestAutomationPage_HasTestDrivenDevelopmentSection_WithExpectedWordOccurrences()
        {
            // Arrange
            _driver.Navigate().GoToUrl("https://en.wikipedia.org/wiki/Test_automation");

            Thread.Sleep(5000);

            // Act
            var textContent = _wikipediaPage.GetTextContentFromTestDrivenDevelopmentSection();
            Console.WriteLine(textContent);

            // Preprocess text content
            var text = textContent.ToLower();
            text = Regex.Replace(text, @"\[.*?\]", string.Empty); // Remove brackets and their contents
            text = Regex.Replace(text, @"[^\w\s]", string.Empty); // Remove non-word characters (e.g., periods, hyphens, commas)

            // Split text into words
            var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Count word occurrences
            var wordOccurrences = words.GroupBy(w => w)
                .ToDictionary(g => g.Key, g => g.Count());

            // Assert
            Assert.NotEmpty(wordOccurrences);
            foreach (var wordOccurrence in wordOccurrences)
            {
                Console.WriteLine($"{wordOccurrence.Key}: {wordOccurrence.Value}");
            }
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}