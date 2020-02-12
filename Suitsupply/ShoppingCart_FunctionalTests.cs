using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium.Interactions;
using log4net;
using log4net.Repository.Hierarchy;
using NUnit.Framework.Internal;


namespace ShopNet
{
    class ShoppingBag_FunctionalTests :TestBase
    {

        [Test]
        [Parallelizable]
        [TestCaseSource(typeof(TestBase), "BrowsersToRunWith")]
        public void AddOneJacketToTheCart(String BrowserName)
        {
            Initialize(BrowserName);
            driver.Navigate().GoToUrl("https://eu.suitsupply.com/on/demandware.store/Sites-INT-Site/en/Configurator-Show?type=jacket");

            waitf.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("js-filters-container")));
            By filterFabric = By.CssSelector("div[data-item-code='595.401/2']");
            waitf.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(filterFabric));
            driver.FindElement(filterFabric).Click();

            waitf.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-item-type='fit']")));
            By filterFit = By.CssSelector("div[data-item-code='havana-wide-lapel']");
            waitf.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(filterFit));
            driver.FindElement(filterFit).Click();

            waitf.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-item-type='lining']")));
            By filterLining = By.CssSelector("div[data-item-code='9680']");
            waitf.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(filterLining));
            driver.FindElement(filterLining).Click();

            waitf.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-item-type='button']")));
            By filterButton = By.CssSelector("div[data-item-code='12']");
            var size = waitf.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(filterButton));
            size.Click();

            By filterSize = By.ClassName("configurators--sizes-group-regular");
            waitf.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(filterSize));
            var groupRegular = driver.FindElement(filterSize);
            var regularSizes = groupRegular.FindElement(By.ClassName("checkbox"));
            regularSizes.Click();

            var submitButton = driver.FindElements(By.TagName("button")).First(b => b.Text.ToLower() == "add to bag");
            submitButton.Click();


            //cart contains 1 item
            var basketTotal = waitf.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("js-shopping-items-number")));
            Assert.AreEqual(basketTotal.Text.ToLower(), "(1 items)");
        }

    }

}
