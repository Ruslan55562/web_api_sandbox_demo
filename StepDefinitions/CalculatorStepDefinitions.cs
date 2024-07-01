using NUnit.Framework;
using SpecFlowProject;
using web_api_sandbox_demo_UI.Hooks;

namespace CalculatorStepDefinitions.Steps
{
    [Binding]
    public class DatabaseConnectionSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public DatabaseConnectionSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
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
            DBHook.CloseConnection();
            Assert.IsTrue(DBHook.IsConnectionOpen(), "Database connection should be open");
        }
    }
}
