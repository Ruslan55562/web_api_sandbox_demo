using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using web_api_sandbox_demo_UI_Drivers;

namespace web_api_sandbox_demo_UI.CommonPageSpace
{
    public class CommonPageMap
    {
        private readonly IObjectContainer _objectContainer;
        private IWebDriver _driver;
        private readonly DriverManager _driverManager;
        public CommonPageMap(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            _driverManager = new DriverManager(_objectContainer);
            _driver = _driverManager.GetDriver();
        }

        public IWebElement GetElement(string locator)
        {
            return _driver.FindElement(By.XPath(locator));
        }

        public ReadOnlyCollection<IWebElement> GetElements(string locator)
        {
            return _driver.FindElements(By.XPath(locator));
        }

        public void ClickButton(string locator)
        {
            _driver.FindElement(By.XPath(locator)).Click();
        }

        public void ClickButtonWithWait(string locator, TimeSpan waitDuration)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var button = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
            var endTime = DateTime.Now.Add(waitDuration);
            button.Click();
            new WebDriverWait(_driver, waitDuration).Until(drv => DateTime.Now >= endTime);
        }

        public void SendTextToInput(string locator,string text)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
            new WebDriverWait(_driver, TimeSpan.FromMilliseconds(250)).Until(d => true);

            element.Clear();
            element.SendKeys(text); ;

        }
    }
}
