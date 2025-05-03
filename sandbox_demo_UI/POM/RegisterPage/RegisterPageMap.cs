using OpenQA.Selenium.Support.UI;
using Reqnroll.BoDi;
using sandbox_demo_UI.PageForms;
using SeleniumExtras.WaitHelpers;
using web_api_sandbox_demo_UI.CommonPageSpace;
using web_api_sandbox_demo_UI.Helpers;
using web_api_sandbox_demo_UI_Drivers;

namespace web_api_sandbox_demo_UI.POM.RegisterPage
{
    public class RegisterPageMap : CommonPageMap
    {
        private readonly ScenarioContext _scenarioContext;

        public RegisterPageMap(IObjectContainer objectContainer, ScenarioContext scenarioContext) : base(objectContainer)
        {
            _scenarioContext = scenarioContext;
        }

        public RegisterPageMap FillInInputRegistyFields(RegisterPage registerPage, Table registryData)
        {
            var formData = registryData.Rows[0];

            foreach (var fieldMapping in RegistrationPageForm.FieldIdMappings)
            {
                var fieldName = fieldMapping.Key;
                var fieldId = fieldMapping.Value;

                if (formData.TryGetValue(fieldName, out var value))
                {
                    var inputField = registerPage.InputFields(fieldId);
                    var driver = DriverManager.GetDriverInstance();
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(0.5));
                    wait.Until(ExpectedConditions.ElementToBeClickable(inputField));

                    inputField.SendTextToInput(driver, value);

                    if (fieldName == "Username")
                    {
                        if (!_scenarioContext.ContainsKey("usernames"))
                        {
                            _scenarioContext["usernames"] = new List<string>();
                        }
                        ((List<string>)_scenarioContext["usernames"]).Add(value);
                    }
                }
            }

            return this;
        }
    }
}