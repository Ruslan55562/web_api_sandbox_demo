using web_api_sandbox_demo_UI.POM.HomePage;

namespace HomePageSteps.Steps
{
    [Binding]
    public class HomePageSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HomePage _homePage;
        private readonly string todayDate = DateTime.Today.ToString("MM/dd/yyyy");

        public HomePageSteps(ScenarioContext scenarioContext, HomePage homePage)
        {
            _scenarioContext = scenarioContext;
            _homePage = homePage;
        }

        [StepArgumentTransformation]
        public List<string> TransformToListOfString(string commaSeparatedList)
        {
            return new List<string>(commaSeparatedList.Split(','));
        }

        [Given(@"I am logged in with username '([^']*)' and password '([^']*)'")]
        public void GivenIAmLoggedInWithUsernameAndPassword(string username, string password) =>
            _homePage.LogIntoApplication(username, password);

        [When(@"I click on '([^']*)' navigation button")]
        public void WhenIClickOnNavigationButton(string buttonName) =>
             _homePage.ClickOnHeaderNavigationButton(buttonName);

        [Then(@"I can see login form with '([^']*)' header")]
        public void ThenICanSeeLoginFormWithHeader(string header) =>
            _homePage.VerifyLoginFormWithHeaderIsDisplayed(header);

        [Then(@"I can see '([^']*)' sections")]
        public void ThenICanSeeSections(List<string> columns) =>
            _homePage.VerifyRightPanelListElementsAreDisplayed(columns);

        [Then(@"I can see latest news sections")]
        public void ThenICanSeeLatestNewsSections() =>
            _homePage.VerifyRightPanelListElementsAreDisplayed(new List<string> { todayDate });

        [Then(@"I can see '([^']*)' section items under '([^']*)' section")]
        public void ThenICanSeeSectionsUnderService(List<string> sectionItems, string serviceName) =>
            _homePage.VerifySectionItemsUnderService(sectionItems, serviceName);

        [Then(@"I can see '([^']*)' section items under latest news section")]
        public void ThenICanSeeSectionItemsUnderLatestNewsSection(List<string> sectionItems) =>
            _homePage.VerifySectionItemsUnderService(sectionItems, todayDate);

        [Then(@"the news section contains today date")]
        public void ThenTheNewsSectionContainsTodayDate() =>
            _homePage.VerifyNewsSectionDateTime(todayDate);

        [Then(@"the news section title '([^']*)' is above the background")]
        public void ThenTheNewsSectionTitleIsAboveTheBackground(string title) =>
            _homePage.VerifyNewsTitleIsAboveBackground(title);
    }
}
