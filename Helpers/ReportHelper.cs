using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;

namespace web_api_sandbox_demo_UI.Helpers
{
    public static class ReportHelper
    {
        private static ExtentReports _extent;
        private static ExtentTest _test;
        private static ExtentSparkReporter _sparkReporter;
        public static void InitializeReport()
        {
                string projectDirectory = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
                string reportPath = Path.Combine(projectDirectory, "Reports", "ExtentReport.html");

                if (File.Exists(reportPath))
                {
                    File.Delete(reportPath);
                }

                Directory.CreateDirectory(Path.GetDirectoryName(reportPath));

                _sparkReporter = new ExtentSparkReporter(reportPath);
                _sparkReporter.Config.DocumentTitle = "Automation Test Report";
                _sparkReporter.Config.ReportName = "Extent Report";

                _extent = new ExtentReports();
                _extent.AttachReporter(_sparkReporter);
        }

        public static void CreateTest()
        {
            _test = _extent.CreateTest(ScenarioContext.Current.ScenarioInfo.Title);
            ScenarioContext.Current["ExtentTest"] = _test;
        }

        public static void ProduceReport()
        {
            var testError = ScenarioContext.Current.TestError;
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
