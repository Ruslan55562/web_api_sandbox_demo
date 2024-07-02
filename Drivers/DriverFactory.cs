using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System.Reflection;

namespace web_api_sandbox_demo_UI_Drivers
{
    public class DriverFactory
    {
        private static OptionsFactory _optionsFactory;
        private IWebDriver driver;

        public DriverFactory()
        {
            _optionsFactory = new OptionsFactory();
        }

        public IWebDriver InitDriver(string browser)
        {
            string? webDriverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            driver = CreateDriver(browser, webDriverPath, _optionsFactory);

            driver.Manage().Window.Maximize();
            return driver;
        }

        private IWebDriver CreateDriver(string browser, string? webDriverPath, OptionsFactory options)
        {
            switch (browser)
            {
                case "firefox":
                    driver = new FirefoxDriver(webDriverPath, options.GetFirefoxOptions());
                    break;
                case "edge":
                    driver = new EdgeDriver(webDriverPath, options.GetEdgeOptions());
                    break;
                case "chrome":
                    driver = new ChromeDriver(webDriverPath, options.GetChromeOptions());
                    break;
            }
              return driver;
        }
    }
}
