using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace web_api_sandbox_demo_UI_Drivers
{
    public class OptionsFactory
    {
        public ChromeOptions GetChromeOptions(bool headless = true)
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--incognito");
            if (headless)
                options.AddArgument("--headless");

            return options;
        }

        public FirefoxOptions GetFirefoxOptions(bool headless = false)
        {
            var options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;
            options.AddArgument("--start-maximized");
            if (headless)
                options.AddArgument("--headless");

            return options;
        }

        public EdgeOptions GetEdgeOptions(bool headless = false)
        {
            var options = new EdgeOptions();
            options.AcceptInsecureCertificates = true;
            options.AddArgument("--start-maximized");
            if (headless)
                options.AddArgument("--headless");

            return options;
        }
    }
}