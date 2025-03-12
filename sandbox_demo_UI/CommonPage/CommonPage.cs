using OpenQA.Selenium;
using web_api_sandbox_demo_UI_Drivers;

namespace web_api_sandbox_demo_UI.CommonPageSpace
{
    public class CommonPage
    {
        protected IWebDriver Driver => DriverManager.GetDriverInstance();

        public IWebElement PageTitle => Driver.FindElement(By.XPath("//img[@title='ParaBank']"));
        public IWebElement ButtonByText(string text) => Driver.FindElement(By.XPath($"//a[.='{text}']"));
        public IWebElement HeaderNavigationButtons(string buttonName) => 
            Driver.FindElement(By.XPath($"//div[@id='headerPanel']//li[.='{buttonName}']"));
        public IWebElement FooterNavigationButtons(string buttonName) => 
            Driver.FindElement(By.XPath($"//div[@id='footerPanel']//a[.='{buttonName}']"));
    }
}