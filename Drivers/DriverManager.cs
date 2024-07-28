using BoDi;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using web_api_sandbox_demo_UI.Helpers;
using web_api_sandbox_demo_UI.Hooks;

namespace web_api_sandbox_demo_UI_Drivers
{
    [Binding]
    public class DriverManager
    {
        private readonly IObjectContainer _objectContainer;
        private readonly IConfiguration _configuration;
        private IWebDriver _driver;
        private readonly DriverFactory _driverFactory;
        private readonly string ?_baseUrl;

        public DriverManager(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            _configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json")
                                    .Build();
            _driverFactory = new DriverFactory();
            _baseUrl = _configuration["BrowserSettings:BaseUrl"];
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ReportHelper.InitializeReport();
        }

        [BeforeScenario]
        public void Initialize()
        {
            SelectBrowser();
            DBHook.OpenConnection();
            ReportHelper.CreateTest();
        }

        public void SelectBrowser()
        {
            string browser = _configuration["BrowserSettings:Browser"]?.ToLower() ?? "chrome";
            _driver = _driverFactory.InitDriver(browser);
            _objectContainer.RegisterInstanceAs<IWebDriver>(_driver, "driver");
        }

        public void GoToBasePage()
        {
            _driver.Navigate().GoToUrl(_baseUrl);
            WaitHelper.WaitForBasePageToLoad(_driver);
        }
  
        public IWebDriver GetDriver()
        {
            _driver = _objectContainer.Resolve<IWebDriver>("driver");
            return _driver;
        }

        [AfterScenario]
        public void CleanUp()
        {
            if (_driver != null)
                _driver.Quit();

            DBHook.CleanupUsers();
            DBHook.CloseConnection();
            ReportHelper.ProduceReport();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ReportHelper.Flush();
        }
    }
}
