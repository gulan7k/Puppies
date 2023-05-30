using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using Puppies.PageObjects;

namespace Puppies.Tests
{
    [TestClass]
    public class PuppiesPage : TestBasePage
    {
        private BasePageObject _basePageObject;

        private readonly ClientDetails _clientDetails = new ClientDetails()
        {
            Name = "Test",
            Address = "Test test test",
            Email = "Test@test.com"
        };

        [OneTimeSetUp]
        public void GetSite()
        {
            _basePageObject = GetBasePageObject<BasePageObject>();
        }

        [Test]
        public void ShouldAdoptBrook()
        {
            _clientDetails.PayType = "Check";
            var pageAfterAdoption = _basePageObject.AdoptDog("Brook", new List<string>{ "Chew Toy", "Travel Carrier" }, _clientDetails);

            ConfirmAdoptionProcessIsCompleted(pageAfterAdoption);
        }

        [Test]
        public void ShouldAdoptSparky()
        {
            _clientDetails.PayType = "Credit card";
            var pageAfterAdoption = _basePageObject.AdoptDog("Sparky", new List<string> { "Collar & Leash" }, _clientDetails);

            ConfirmAdoptionProcessIsCompleted(pageAfterAdoption);
        }

        [Test]
        public void ShouldAdopt2RandomDogsWith1Accessory()
        {
            _clientDetails.PayType = "Credit card";
            var homePage = _basePageObject.SelectDogToAdoptionAndGoToHomePage("Twinkie", new List<string> { "Collar & Leash" });
            var pageAfterAdoption = homePage.AdoptDog("Spud", new List<string> { "Collar & Leash" }, _clientDetails);

            ConfirmAdoptionProcessIsCompleted(pageAfterAdoption);
        }

        [Test]
        public void ShouldAdopt2RandomDogsWith3RandomAccessories()
        {
            _clientDetails.PayType = "Credit card";
            var homePage = _basePageObject.SelectDogToAdoptionAndGoToHomePage("Twinkie", new List<string> { "Collar & Leash", "Chew Toy", "First Vet Visit" });
            var pageAfterAdoption = homePage.AdoptDog("Spud", new List<string> {}, _clientDetails);

            ConfirmAdoptionProcessIsCompleted(pageAfterAdoption);
        }

        public void ConfirmAdoptionProcessIsCompleted(BasePageObject page)
        {
            page._driver.FindElement(By.TagName("body")).Text.Should()
                .Contain("Thank you for adopting a puppy!");
        }
    }
}
