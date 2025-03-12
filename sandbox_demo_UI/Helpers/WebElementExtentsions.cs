using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace web_api_sandbox_demo_UI.Helpers
{
    public static class WebElementExtentsions
    {
        public static void ClickButtonWithWait(this IWebElement element, IWebDriver driver, TimeSpan waitDuration)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
            var endTime = DateTime.Now.Add(waitDuration);
            element.Click();
            new WebDriverWait(driver, waitDuration).Until(drv => DateTime.Now >= endTime);
        }

        public static void SendTextToInput(this IWebElement element, IWebDriver driver, string text)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
            new WebDriverWait(driver, TimeSpan.FromMilliseconds(250)).Until(d => true);

            element.Clear();
            element.SendKeys(text);
        }
    }
}