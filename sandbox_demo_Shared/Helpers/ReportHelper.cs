using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using TechTalk.SpecFlow;

namespace sandbox_demo_Shared.Helpers
{
    public static class ReportHelper
    {
        private static ExtentReports _extent;
        private static ExtentTest _test;
        private static ExtentSparkReporter _sparkReporter;

        public static void InitializeReport(string reportType)
        {
            string solutionDirectory =
                Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent
                (Environment.CurrentDirectory).FullName).FullName).FullName).FullName,"sandbox_demo_Shared");

            string reportDirectory = Path.Combine(solutionDirectory, "Reports");

            if (!Directory.Exists(reportDirectory))
            {
                Directory.CreateDirectory(reportDirectory);
            }

            string reportPath = Path.Combine(reportDirectory, $"ExtentReport_{reportType}.html");

            _extent = new ExtentReports();

            _sparkReporter = new ExtentSparkReporter(reportPath);
            _sparkReporter.Config.DocumentTitle = "Automation Test Report";
            _sparkReporter.Config.ReportName = "Extent Report";

            _extent.AttachReporter(_sparkReporter);
        }

        public static void CreateTest(ScenarioContext scenarioContext)
        {
            _test = _extent.CreateTest(scenarioContext.ScenarioInfo.Title);
            scenarioContext["ExtentTest"] = _test;
        }

        public static void ProduceReport(ScenarioContext scenarioContext)
        {
            var testError = scenarioContext.TestError;
            if (testError == null)
            {
                _test.Pass("Scenario passed");
            }
            else
            {
                _test.Fail($"Scenario failed: {testError.Message}");
                _test.Fail(testError);
            }
        }

        public static void Flush()
        {
            _extent.Flush();
        }
    }
}
