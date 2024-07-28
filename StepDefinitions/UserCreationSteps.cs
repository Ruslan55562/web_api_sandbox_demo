using web_api_sandbox_demo_UI.POM.RegisterPage;

namespace web_api_sandbox_demo_UI.StepDefinitions
{
    [Binding]
    public class UserCreationSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly RegisterPage _registerPage;

        public UserCreationSteps(ScenarioContext scenarioContext,RegisterPage registerPage)
        {
            _scenarioContext = scenarioContext;
            _registerPage = registerPage;   
        }

        [Given(@"I click on '([^']*)' button")]
        [When(@"I click on '([^']*)' button")]
        public void GivenIClickOnButton(string buttonName) => 
            _registerPage.ClickOnRegisterButton(buttonName);

        [When(@"I fill in input fields with the next data")]
        public void GivenIFillInInputFieldsWithTheNextData(Table userData) =>
            _registerPage.FillInInputRegisterData(userData);

        [When(@"I send the registration form")]
        public void WhenISendTheRegistrationForm() =>
            _registerPage.SendRegisterForm();

        [When(@"I click on '([^']*)' button under Account Services panel")]
        public void WhenIClickOnButtonUnderAccountServicesPanel(string optionName) =>
            _registerPage.ClickOnLogOutButton(optionName);


        [Then(@"I can see welcome '([^']*)' message")]
        public void ThenICanSeeWelcomeMessage(string welcomeMessage) =>
            _registerPage.VerifyWelcomeHeaderMessageIsDisplayed(welcomeMessage);

        [Then(@"I can see '([^']*)' message above the nav panel")]
        public void ThenICanSeeMessageAboveTheNavPanel(string message) => 
            _registerPage.VerifyNavigationPanelMessageIsDisplayed(message);

        [Then(@"The error message '([^']*)' is displayed")]
        public void ThenTheErrorMessageIsDisplayed(string message) =>
            _registerPage.VerifyFormErrorMessage(message);

    }
}