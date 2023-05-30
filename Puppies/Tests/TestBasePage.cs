using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Puppies.PageObjects;

namespace Puppies.Tests
{
    public class TestBasePage
    {
        public IWebDriver Driver = new ChromeDriver();

        [OneTimeSetUp]
        public void DriverInitialize()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Driver.Navigate().GoToUrl(configuration["Url"]);
        }

        public BasePageObject GetBasePageObject<T>()
        {
            return new BasePageObject(Driver);
        }

        [OneTimeTearDown]
        public void DriverCleanup()
        {
            Driver.Quit();
        }
    }
}
