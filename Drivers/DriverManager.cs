using BoDi;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

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

        [BeforeScenario]
        public void SelectBrowser()
        {
            Initialize();
        }

        public void Initialize()
        {
            string browser = _configuration["BrowserSettings:Browser"]?.ToLower() ?? "chrome";
            _driver = _driverFactory.InitDriver(browser);
            _objectContainer.RegisterInstanceAs<IWebDriver>(_driver, "driver");
        }

        public void GoToBasePage()
        {
            _driver.Navigate().GoToUrl(_baseUrl);
        }

        [AfterScenario]
        public void CleanUp()
        {
            if (_driver != null)
                _driver.Quit();
        }
    }
}
