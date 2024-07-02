using NUnit.Framework;
using web_api_sandbox_demo_UI.Hooks;
using web_api_sandbox_demo_UI_Drivers;

namespace CalculatorStepDefinitions.Steps
{
    [Binding]
    public class DatabaseConnectionSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly DriverManager _driverManager;

        public DatabaseConnectionSteps(ScenarioContext scenarioContext, DriverManager driverManager)
        {
            _scenarioContext = scenarioContext;
            _driverManager = driverManager;

        }

        [Given(@"the database connection is open")]
        public void GivenTheDatabaseConnectionIsOpen()
        {
            DBHook.OpenConnection();
        }

        [Then(@"verify the connection state")]
        public void ThenVerifyTheConnectionState()
        {
            Assert.IsTrue(DBHook.IsConnectionOpen(), "Database connection should be open");
            DBHook.CloseConnection();
        }

        [Given(@"I open Main page")]
        public void GivenIOpenMainPage()
        {
            _driverManager.GoToBasePage();
        }

    }
}
