using BoDi;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using web_api_sandbox_demo_UI.CommonPageSpace;

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
        private WebDriverWait _wait;
        private readonly CommonPage _commonPage;

        public DriverManager(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            _configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json")
                                    .Build();
            _driverFactory = new DriverFactory();
            _baseUrl = _configuration["BrowserSettings:BaseUrl"];
            _commonPage = new CommonPage();
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
            WaitForBasePageToLoad();
        }

        public void WaitForBasePageToLoad()
        {
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement element = _wait.Until(drv => drv.FindElement(By.XPath(_commonPage.PageTitle)));
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(_commonPage.PageTitle)));
            AddJsWait(1000);
        }

        private void AddJsWait(int milliseconds)
        {
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteAsyncScript($"window.setTimeout(arguments[arguments.length - 1], {milliseconds});");
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
        }
    }
}
