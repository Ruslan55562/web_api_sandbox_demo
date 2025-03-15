using Reqnroll.BoDi;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using web_api_sandbox_demo_UI.Helpers;
using sandbox_demo_Shared.Helpers;
using sandbox_demo_Shared.Hooks;
using sandbox_demo_Shared.Configs;

namespace web_api_sandbox_demo_UI_Drivers
{
    [Binding]
    public class DriverManager
    {
        private readonly IObjectContainer _objectContainer;
        private readonly IConfiguration _configuration;
        private IWebDriver _driver;
        private readonly DriverFactory _driverFactory;
        private readonly string? _baseUrl;

        public static IWebDriver CurrentDriver { get; private set; }

        public DriverManager(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            _configuration = AppConfig.GetConfiguration();
            _driverFactory = new DriverFactory();
            _baseUrl = _configuration["BrowserSettings:BaseUrl"];
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ReportHelper.InitializeReport("UI");
        }

        [BeforeScenario]
        public void Initialize(ScenarioContext scenarioContext)
        {
            ReportHelper.CreateTest(scenarioContext);
            SelectBrowser();
            DBHook.OpenConnection();
        }

        public void SelectBrowser()
        {
            string browser = _configuration["BrowserSettings:Browser"]?.ToLower() ?? "chrome";
            _driver = _driverFactory.InitDriver(browser);
            CurrentDriver = _driver;
            _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
        }

        public void GoToBasePage()
        {
            _driver.Navigate().GoToUrl(_baseUrl);
            WaitHelper.WaitForBasePageToLoad(_driver);
        }

        public static IWebDriver GetDriverInstance()
        {
            return CurrentDriver;
        }

        public IWebDriver GetDriver()
        {
            return _objectContainer.Resolve<IWebDriver>();
        }

        [AfterScenario]
        public void CleanUp(ScenarioContext scenarioContext)
        {
            DBHook.CleanupUsers(scenarioContext);
            DBHook.CloseConnection();
            ReportHelper.ProduceReport(scenarioContext, _driver);

            if (_driver != null)
                _driver.Quit();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ReportHelper.Flush();
        }
    }
}