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
            Task.Run(async () => await Task.Delay(200)).GetAwaiter().GetResult();
            IWebElement element = _commonPage.PageTitle;
            _wait.Until(driver => element.Displayed);
        }
    }
}