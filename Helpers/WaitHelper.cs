using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using web_api_sandbox_demo_UI.CommonPageSpace;

namespace web_api_sandbox_demo_UI.Helpers
{
    public static class WaitHelper
    {
        private static WebDriverWait _wait;
        private static CommonPage _commonPage = new CommonPage();
        public static void WaitForBasePageToLoad(IWebDriver driver)
        {
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement element = _wait.Until(drv => drv.FindElement(By.XPath(_commonPage.PageTitle)));
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(_commonPage.PageTitle)));
            AddJsWait(1000,driver);
        }

        private static void AddJsWait(int milliseconds, IWebDriver driver)
        {
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteAsyncScript($"window.setTimeout(arguments[arguments.length - 1], {milliseconds});");
        }
    }
}
