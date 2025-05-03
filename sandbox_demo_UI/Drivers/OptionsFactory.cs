using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using Microsoft.Extensions.Configuration;

namespace web_api_sandbox_demo_UI_Drivers
{
    public class OptionsFactory
    {
        private readonly IConfiguration _configuration;
        private readonly List<string> _arguments;

        public OptionsFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _arguments = _configuration.GetSection("BrowserSettings:Arguments").Get<List<string>>() ?? new List<string>();
        }

        public ChromeOptions GetChromeOptions()
        {
            var options = new ChromeOptions();
            AddArguments(options);
            return options;
        }

        public FirefoxOptions GetFirefoxOptions()
        {
            var options = new FirefoxOptions();
            AddArguments(options);
            options.AcceptInsecureCertificates = true;
            return options;
        }

        public EdgeOptions GetEdgeOptions()
        {
            var options = new EdgeOptions();
            AddArguments(options);
            options.AcceptInsecureCertificates = true;
            return options;
        }

        private void AddArguments(dynamic options)
        {
            foreach (var argument in _arguments)
            {
                options.AddArgument(argument);
            }
        }
    }
}