using System.Linq;
using OpenQA.Selenium;

namespace Puppies.PageObjects
{
    public class EnterDetailsPage
    {
        private readonly IWebDriver _driver = null;
        
        private void PlaceOrder() => _driver.FindElement(By.ClassName("submit")).Click();

        private void FillInName(string name) => _driver.FindElement(By.Id("order_name")).SendKeys(name);

        private void FillInAddress(string address) => _driver.FindElement(By.Id("order_address")).SendKeys(address);

        private void FillInEmail(string email) => _driver.FindElement(By.Id("order_email")).SendKeys(email);

        public EnterDetailsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void SelectPayType(string payType)
        {
            _driver.FindElement(By.Id("order_pay_type")).FindElements(By.TagName("option")).First(x => x.Text.Contains(payType)).Click();
        }

        public BasePageObject EnterDetails(ClientDetails clientDetails)
        {
            FillInName(clientDetails.Name);
            FillInAddress(clientDetails.Address);
            FillInEmail(clientDetails.Email);
            SelectPayType(clientDetails.PayType);
            PlaceOrder();
            return new BasePageObject(_driver);
        }
    }
}
