using System.Globalization;
using web_api_sandbox_demo_UI.CommonPageSpace;

namespace web_api_sandbox_demo_UI.POM.HomePage
{
    public class HomePage : CommonPage
    {

        private HomePageAssertions _homePageAssertions;
        private readonly HomePageMap _homePageMap;

        private string newsDateTime = "//ul[@class='events']/li[@class='captionthree']";
        private string userNameField = "//input[@name='username']";
        private string passwordField = "//input[@name='password']";
        private string logInButton = "//input[@value='Log In']";

        private string LoginFormTitle(string header) => $"//div[@id='leftPanel']/h2[.='{header}']";
        private string RightPanelListElements(string element) => $"//div[@id='rightPanel']//li[.='{element}']";
        private string SectionItemsUnderService(string sectionName, string item) =>
            $"//div[@id='rightPanel']//li[.='{sectionName}']/following-sibling::li[.='{item}']";
        private string RightPanelNewsTitle(string titleText) => $"//h4[.='{titleText}']";


        public HomePage(HomePageAssertions homePageAssertions, HomePageMap homePageMap)
        {
            _homePageAssertions = homePageAssertions;
            _homePageMap = homePageMap;
        }

        public HomePage VerifyLoginFormWithHeaderIsDisplayed(string header)
        {
            _homePageAssertions.IsElementDisplayed(LoginFormTitle(header), $"The login form with {header} title wasn't found");
            return this;
        }

        public HomePage VerifyRightPanelListElementsAreDisplayed(List<string> elements)
        {
            _homePageAssertions.VerifyRightPanelListElementsAreDisplayed(elements, RightPanelListElements);
            return this;
        }

        public HomePage VerifySectionItemsUnderService(List<string> sectionItems, string sectionName)
        {
            _homePageAssertions.VerifySectionItemsUnderService(sectionItems, sectionName, SectionItemsUnderService);
            return this;
        }

        public HomePage VerifyNewsSectionDateTime(string expectedDate)
        {
            _homePageAssertions.VerifyNewsSectionDateTime(expectedDate, GetDateFromField());
            return this;
        }

        public HomePage VerifyNewsTitleIsAboveBackground(string title)
        {
            _homePageAssertions.VerifyNewsTitleIsAboveBackground(title, RightPanelNewsTitle);
            return this;
        }

        public HomePage LogIntoApplication(string username, string password)
        {
            _homePageMap.LogIntoApplication(username, password, userNameField,passwordField,logInButton);
            return this;
        }

        public HomePage ClickOnHeaderNavigationButton(string buttonName)
        {
            _homePageMap.ClickButton(HeaderNavigationButtons(buttonName));
            return this;
        }

        private string GetDateFromField()
        {
            return DateTime.Today.ToString(_homePageMap.GetElement(newsDateTime).Text, CultureInfo.InvariantCulture);
        }
    }
}
