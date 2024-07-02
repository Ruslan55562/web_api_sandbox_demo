using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace web_api_sandbox_demo_UI_Drivers
{
    public class OptionsFactory
    {
        public ChromeOptions GetChromeOptions()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--incognito");
            return options;
        }

        public FirefoxOptions GetFirefoxOptions()
        {
            var options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;
            return options;
        }

        public EdgeOptions GetEdgeOptions()
        {
            var options = new EdgeOptions();
            options.AcceptInsecureCertificates = true;
            options.AddArgument("--start-maximized");
            return options;
        }
    }
}
