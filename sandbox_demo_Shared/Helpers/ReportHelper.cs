using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using Reqnroll;

namespace sandbox_demo_Shared.Helpers;

public static class ReportHelper
{
    private static ExtentReports _extent;
    private static ExtentTest _test;
    private static ExtentSparkReporter _sparkReporter;
    private static readonly string solutionDirectory = Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent
            (Environment.CurrentDirectory).FullName).FullName).FullName).FullName, "sandbox_demo_Shared");


    public static void InitializeReport(string reportType)
    {
        ClearFailedTestsDirectory();

        string reportDirectory = Path.Combine(solutionDirectory, "Reports");
        string failedTestsDirectory = Path.Combine(reportDirectory, "FailedTestsResults");

        if (!Directory.Exists(reportDirectory))
        {
            Directory.CreateDirectory(reportDirectory);
        }

        if (!Directory.Exists(failedTestsDirectory))
        {
            Directory.CreateDirectory(failedTestsDirectory);
        }

        string reportPath = Path.Combine(reportDirectory, $"ExtentReport_{reportType}.html");

        _extent = new ExtentReports();
        _sparkReporter = new ExtentSparkReporter(reportPath)
        {
            Config =
            {
                DocumentTitle = "Automation Test Report",
                ReportName = "Extent Report"
            }
        };

        _extent.AttachReporter(_sparkReporter);
    }

    public static void CreateTest(ScenarioContext scenarioContext)
    {
        _test = _extent.CreateTest(scenarioContext.ScenarioInfo.Title);
        scenarioContext["ExtentTest"] = _test;
    }

    public static void ProduceReport(ScenarioContext scenarioContext, IWebDriver driver = null)
    {
        var testError = scenarioContext.TestError;
        if (testError == null)
        {
            _test.Pass("Scenario passed");
        }
        else
        {
            _test.Fail(testError);
        }

        if (driver != null && testError != null)
        {
            string screenshotPath = CaptureScreenshot(driver, scenarioContext.ScenarioInfo.Title);
            _test.AddScreenCaptureFromPath(screenshotPath);
        }
    }

    public static void Flush()
    {
        _extent.Flush();
    }

    private static string CaptureScreenshot(IWebDriver driver, string scenarioName)
    {
        string failedTestsDirectory = Path.Combine(solutionDirectory, "Reports", "FailedTestsResults");

        if (!Directory.Exists(failedTestsDirectory))
        {
            Directory.CreateDirectory(failedTestsDirectory);
        }

        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string screenshotFile = Path.Combine(failedTestsDirectory, $"{scenarioName}_{timestamp}.png");

        Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        screenshot.SaveAsFile(screenshotFile);

        return screenshotFile;
    }

    private static void ClearFailedTestsDirectory()
    {

        string failedTestsDirectory = Path.Combine(solutionDirectory, "Reports", "FailedTestsResults");

        if (Directory.Exists(failedTestsDirectory))
        {
            var files = Directory.GetFiles(failedTestsDirectory);
            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"The error ocurred while deleting {file}: {ex.Message}");
                }
            }
        }
    }
}
