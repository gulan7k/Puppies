using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using OpenQA.Selenium;

namespace Puppies.PageObjects
{
    public class BasePageObject
    {
        private List<IWebElement> GetPuppiesList => _driver.FindElements(By.ClassName("puppy_list")).ToList(); 
        
        private IWebElement GetNextPageButton => _driver.FindElement(By.ClassName("pagination")).FindElement(By.ClassName("next_page"));

        private DogDetailsPage ViewDogDetails(string dogName)
        {
            IWebElement dogSection = null;
            IWebElement nextPageButton = null;
            bool disabledButton = false;

            while (!disabledButton)
            {
                disabledButton = GetNextPageButton.GetAttribute("class").Contains("disabled");
                try
                {
                    dogSection = GetPuppiesList.First(x => x.Text.Contains(dogName));
                    break;
                }
                catch (Exception e)
                {
                    GoToAnotherPageOfPuppies();
                }
            } 

            if (!(dogSection is null))
            {
                dogSection.FindElement(By.ClassName("rounded_button")).Click();
                return new DogDetailsPage(_driver);
            }
            else
            {
                throw new SyntaxErrorException("Can't find a dog with selected name");
            }
            
        }

        private void GoToAnotherPageOfPuppies()
        {
            GetNextPageButton.Click();
        }

        public readonly IWebDriver _driver = null;

        public BasePageObject(IWebDriver driver)
        {
            _driver = driver;
        }

        public BasePageObject AdoptDog(string dogName, List<string> accessories, ClientDetails clientDetails)
        {
            var detailPage = ViewDogDetails(dogName);

            var orderPage = detailPage.ClickAdoptButton();

            if (accessories.Count != 0)
            {
                orderPage.AddAccessories(accessories);
            }
            
            var homePage = orderPage.CompleteTheAdoption().EnterDetails(clientDetails);
            return homePage;
        }

        public BasePageObject SelectDogToAdoptionAndGoToHomePage(string dogName, List<string> accessories)
        {
            var detailPage = ViewDogDetails(dogName);
            var orderPage = detailPage.ClickAdoptButton();

            orderPage.AddAccessories(accessories);
            
            var homePage = orderPage.AdoptAnotherPuppy();
            return homePage;
        }
    }
}
