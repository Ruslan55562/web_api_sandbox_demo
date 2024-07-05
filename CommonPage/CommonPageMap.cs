using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
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

        public void ClickButton(string locator)
        {
            _driver.FindElement(By.XPath(locator)).Click();
        }

        public void SendTextToInput(string locator,string text)
        {
            IWebElement driver = _driver.FindElement(By.XPath(locator));
            driver.SendKeys(Keys.Control + "a");
            driver.SendKeys(text);

        }
    }
}
