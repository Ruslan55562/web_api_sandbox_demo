using web_api_sandbox_demo_UI_Drivers;

namespace web_api_sandbox_demo_UI.StepDefinitions
{
    [Binding]
    public class CommonPageSteps
    {
        private readonly DriverManager _driverManager;

        public CommonPageSteps(DriverManager driverManager)
        {
            _driverManager = driverManager;

        }


        [Given(@"I open Main page")]
        public void GivenIOpenMainPage() => 
            _driverManager.GoToBasePage();
    }
}
