using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace web_api_sandbox_demo_UI_Drivers
{
    public class DriverFactory
    {
        private readonly OptionsFactory _optionsFactory;
        private readonly string _browser;
        private IWebDriver driver;

        public DriverFactory(IConfiguration configuration)
        {
            _optionsFactory = new OptionsFactory(configuration);
            _browser = configuration["BrowserSettings:Browser"];
        }

        public IWebDriver InitDriver()
        {
            string? webDriverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            driver = CreateDriver(_browser, webDriverPath);

            driver.Manage().Window.Maximize();
            return driver;
        }

        private IWebDriver CreateDriver(string browser, string? webDriverPath)
        {
            switch (browser.ToLower())
            {
                case "firefox":
                    driver = new FirefoxDriver(webDriverPath, _optionsFactory.GetFirefoxOptions());
                    break;
                case "edge":
                    driver = new EdgeDriver(webDriverPath, _optionsFactory.GetEdgeOptions());
                    break;
                case "chrome":
                default:
                    driver = new ChromeDriver(webDriverPath, _optionsFactory.GetChromeOptions());
                    break;
            }
            return driver;
        }
    }
}