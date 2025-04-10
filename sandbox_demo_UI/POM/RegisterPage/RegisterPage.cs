using web_api_sandbox_demo_UI.CommonPageSpace;
using OpenQA.Selenium;
using web_api_sandbox_demo_UI.Helpers;

namespace web_api_sandbox_demo_UI.POM.RegisterPage
{
    public class RegisterPage : CommonPage
    {
        private readonly RegisterPageAssertions _registerPageAssertions;
        private readonly RegisterPageMap _registerPageMap;
        private readonly IWebDriver _driver;

        public RegisterPage(IWebDriver driver, RegisterPageAssertions registerPageAssertions, RegisterPageMap registerPageMap)
        {
            _driver = driver;
            _registerPageAssertions = registerPageAssertions;
            _registerPageMap = registerPageMap;
        }

        public IWebElement RegisterFormButton => _driver.FindElement(By.XPath("//input[@value='Register']"));
        public IWebElement NavPanelWelcomeMessage(string message) => _driver.FindElement(By.XPath($"//div[@id='leftPanel']/p[.='{message}']"));
        public IWebElement HeaderWelcomeMessage(string welcomeMessage) => _driver.FindElement(By.XPath($"//h1[.='{welcomeMessage}']"));
        public IWebElement ErrorMessageText(string message) => _driver.FindElement(By.XPath($"//span[contains(.,'{message}')]"));
        public IWebElement ButtonsUnderAccountServicesPanel(string optionName) => _driver.FindElement(By.XPath($"//div[@id='leftPanel']//a[.='{optionName}']"));
        public IWebElement InputFields(string id) => _driver.FindElement(By.XPath($"//input[@id='{id}']"));

        public RegisterPage ClickOnRegisterButton(string text)
        {
            ButtonByText(text).ClickButtonWithWait(_driver, TimeSpan.FromSeconds(0.5));
            return this;
        }

        public RegisterPage ClickOnLogOutButton(string optionName)
        {
           ButtonsUnderAccountServicesPanel(optionName).ClickButtonWithWait(_driver, TimeSpan.FromSeconds(0.5));
            return this;
        }

        public RegisterPage FillInInputRegisterData(Table userData)
        {
            _registerPageMap.FillInInputRegistyFields(this,userData);
            return this;
        }

        public RegisterPage SendRegisterForm()
        {
            RegisterFormButton.ClickButtonWithWait(_driver, TimeSpan.FromSeconds(1));
            return this;
        }

        public RegisterPage VerifyWelcomeHeaderMessageIsDisplayed(string welcomeMessage)
        {
            _registerPageAssertions.IsTrue(HeaderWelcomeMessage(welcomeMessage).Displayed, "The element was not displayed on the page");
            return this;
        }

        public RegisterPage VerifyNavigationPanelMessageIsDisplayed(string message)
        {
            _registerPageAssertions.IsTrue(NavPanelWelcomeMessage(message).Displayed, "The element was not displayed on the page");
            return this;
        }

        public RegisterPage VerifyFormErrorMessage(string message)
        {
            var actualMessage = ErrorMessageText(message).Text.TrimEnd('.');

            _registerPageAssertions.AreEqual(message, actualMessage, "The error message is not equal to expected one");
            return this;
        }
    }
}