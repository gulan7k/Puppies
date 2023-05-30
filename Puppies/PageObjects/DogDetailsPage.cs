using OpenQA.Selenium;

namespace Puppies.PageObjects
{
    public class DogDetailsPage
    {
        private readonly IWebDriver _driver = null;

        private IWebElement GetAdoptMeButton => _driver.FindElement(By.ClassName("rounded_button"));

        public DogDetailsPage(IWebDriver driver)
        {
            _driver = driver;
        }
        
        public OrderPage ClickAdoptButton()
        {
            GetAdoptMeButton.Click();
            return new OrderPage(_driver);
        }
    }
}
