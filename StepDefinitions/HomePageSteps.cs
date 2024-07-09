using NUnit.Framework;
using System.Globalization;
using web_api_sandbox_demo_UI.Hooks;
using web_api_sandbox_demo_UI.POM.HomePage;
using web_api_sandbox_demo_UI_Drivers;

namespace HomePageSteps.Steps
{
    [Binding]
    public class HomePageSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HomePage _homePage;

        public HomePageSteps(ScenarioContext scenarioContext, DriverManager driverManager, HomePage homePage)
        {
            _scenarioContext = scenarioContext;
            _homePage = homePage;
        }

        [StepArgumentTransformation]
        public List<string> TransformToListOfString(string commaSeparatedList)
        {
            return new List<string>(commaSeparatedList.Split(','));
        }

        [Then(@"I can see login form with '([^']*)' header")]
        public void ThenICanSeeLoginFormWithHeader(string header) => 
            _homePage.VerifyLoginFormWithHeaderIsDisplayed(header);

        [Then(@"I can see '([^']*)' sections")]
        public void ThenICanSeeSections(List<string> columns) => 
            _homePage.VerifyRightPanelListElementsAreDisplayed(columns);

        [Then(@"I can see '([^']*)' section items under '([^']*)' section")]
        public void ThenICanSeeSectionsUnderService(List<string> sectionItems, string serviceName) => 
            _homePage.VerifySectionItemsUnderService(sectionItems, serviceName);


        [Then(@"the news section contains '([^']*)' date")]
        public void ThenTheNewsSectionContainsDate(string currentDate) => 
            _homePage.VerifyNewsSectionDateTime(currentDate);


        [Then(@"the news section title '([^']*)' is above the background")]
        public void ThenTheNewsSectionTitleIsAboveTheBackground(string title) =>
            _homePage.VerifyNewsTitleIsAboveBackground(title);

    }
}
