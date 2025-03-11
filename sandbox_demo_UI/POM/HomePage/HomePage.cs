using System.Globalization;
using web_api_sandbox_demo_UI.CommonPageSpace;
using OpenQA.Selenium;

namespace web_api_sandbox_demo_UI.POM.HomePage
{
    public class HomePage : CommonPage
    {
        private readonly HomePageAssertions _homePageAssertions;
        private readonly HomePageMap _homePageMap;
        private readonly IWebDriver _driver;

        public HomePage(IWebDriver driver, HomePageAssertions homePageAssertions, HomePageMap homePageMap)
        {
            _driver = driver;
            _homePageAssertions = homePageAssertions;
            _homePageMap = homePageMap;
        }

        public IWebElement GetNewsDateTime() => _driver.FindElement(By.XPath("//ul[@class='events']/li[@class='captionthree']"));
        public IWebElement GetUserNameField() => _driver.FindElement(By.XPath("//input[@name='username']"));
        public IWebElement GetPasswordField() => _driver.FindElement(By.XPath("//input[@name='password']"));
        public IWebElement GetLogInButton() => _driver.FindElement(By.XPath("//input[@value='Log In']"));

        public IWebElement GetLoginFormTitle(string header) => _driver.FindElement(By.XPath($"//div[@id='leftPanel']/h2[.='{header}']"));
        public IWebElement GetRightPanelListElement(string element) => _driver.FindElement(By.XPath($"//div[@id='rightPanel']//li[.='{element}']"));
        public IWebElement GetSectionItemUnderService(string sectionName, string item) => _driver.FindElement(By.XPath($"//div[@id='rightPanel']//li[.='{sectionName}']/following-sibling::li[.='{item}']"));
        public IWebElement GetRightPanelNewsTitle(string titleText) => _driver.FindElement(By.XPath($"//h4[.='{titleText}']"));

        public HomePage VerifyLoginFormWithHeaderIsDisplayed(string header)
        {
            _homePageAssertions.IsTrue(GetLoginFormTitle(header).Displayed, $"The login form with {header} title wasn't found");
            return this;
        }

        public HomePage VerifyRightPanelListElementsAreDisplayed(List<string> elements)
        {
            _homePageAssertions.VerifyRightPanelListElementsAreDisplayed(elements, this);
            return this;
        }

        public HomePage VerifySectionItemsUnderService(List<string> sectionItems, string sectionName)
        {
            _homePageAssertions.VerifySectionItemsUnderService(sectionItems, sectionName, this);
            return this;
        }

        public HomePage VerifyNewsSectionDateTime(string expectedDate)
        {
            _homePageAssertions.VerifyNewsSectionDateTime(expectedDate, GetDateFromField());
            return this;
        }

        public HomePage VerifyNewsTitleIsAboveBackground(string title)
        {
            _homePageAssertions.VerifyNewsTitleIsAboveBackground(title, this);
            return this;
        }

        public HomePage LogIntoApplication(string username, string password)
        {
            _homePageMap.LogIntoApplication(this, username, password);
            return this;
        }

        public HomePage ClickOnHeaderNavigationButton(string buttonName)
        {
            _homePageMap.ClickButton(HeaderNavigationButtons(buttonName));
            return this;
        }

        private string GetDateFromField()
        {
            return DateTime.Today.ToString(GetNewsDateTime().Text, CultureInfo.InvariantCulture);
        }
    }
}