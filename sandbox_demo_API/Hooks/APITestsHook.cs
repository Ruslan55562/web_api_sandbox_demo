using RestSharp;
using sandbox_demo_API.Helpers;
using sandbox_demo_API_Configs;

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
            ReportHelper.InitializeReport();
        }

        [BeforeScenario]
        public void Initialize()
        {
            DBHook.OpenConnection();
            ReportHelper.CreateTest();
            var client = new RestClient(ConfigurationLoader.GetBaseUrl());
            _context["client"] = client;
        }

        [AfterScenario]
        public void CleanUp()
        {
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
