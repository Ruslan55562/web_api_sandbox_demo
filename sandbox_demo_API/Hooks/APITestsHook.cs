using RestSharp;
using sandbox_demo_Shared.Helpers;
using sandbox_demo_Shared.Configs;
using sandbox_demo_Shared.Hooks;
using System.Net;

namespace sandbox_demo_API.Hooks
{
    [Binding]
    public class APITestsHook
    {
        private readonly ScenarioContext _context;

        public APITestsHook(ScenarioContext context)
        {
            _context = context;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ReportHelper.InitializeReport("API");
        }

        [BeforeScenario]
        public void Initialize()
        {
            DBHook.OpenConnection();
            ReportHelper.CreateTest(_context);
            InitializeClient();
        }

        private void InitializeClient()
        {
            var cookieContainer = new CookieContainer();
            var client = new RestClient(new RestClientOptions(AppConfig.GetBaseUrl())
            {
                CookieContainer = cookieContainer
            });
            _context["client"] = client;
            _context["cookieContainer"] = cookieContainer;
        }

        [AfterScenario]
        public void CleanUp()
        {
            DBHook.CloseConnection();
            ReportHelper.ProduceReport(_context);
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ReportHelper.Flush();
        }
    }
}
