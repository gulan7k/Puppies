using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Puppies.PageObjects
{
    public class OrderPage
    {
        private readonly IWebDriver _driver = null;

        private List<IWebElement> GetButtons() => _driver.FindElement(By.ClassName("cart_buttons")).FindElements(By.TagName("input")).ToList();

        private IWebElement GetCompleteTheAdoptionButton() => GetButtons().First(x => x.GetAttribute("value").Contains("Complete the Adoption"));

        private IWebElement GetAdoptAnotherPuppyButton() => GetButtons().First(x => x.GetAttribute("value").Contains("Adopt Another Puppy"));

        private IWebElement GetAccessoryButton(string accessory)
        {
            var table = _driver.FindElement(By.TagName("table"));
            var rowWithAccessory = table.FindElements(By.TagName("tr")).First(x => x.Text.Contains(accessory));
            return rowWithAccessory.FindElement(By.TagName("input"));
        }

        public OrderPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void AddAccessories(List<string> accessories)
        {
            foreach (var accessory in accessories)
            {
                GetAccessoryButton(accessory).Click();
            }
        }

        public EnterDetailsPage CompleteTheAdoption()
        {
            GetCompleteTheAdoptionButton().Click();
            return new EnterDetailsPage(_driver);
        }

        public BasePageObject AdoptAnotherPuppy()
        {
            GetAdoptAnotherPuppyButton().Click();
            return new BasePageObject(_driver);
        }
    }
}
