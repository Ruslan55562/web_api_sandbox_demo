using BoDi;
using sandbox_demo_UI.Map;
using web_api_sandbox_demo_UI.CommonPageSpace;

namespace web_api_sandbox_demo_UI.POM.RegisterPage
{
    public class RegisterPageMap : CommonPageMap
    {
        private ScenarioContext _scenarioContext;
        public RegisterPageMap(IObjectContainer objectContainer, ScenarioContext scenarioContext) : base(objectContainer)
        {
            _scenarioContext = scenarioContext;
        }

        public RegisterPageMap FillInInputRegistyFields(Table registryData)
        {
            var formData = registryData.Rows[0];

            foreach (var fieldMapping in RegistrationInputFieldsConstants.FieldIdMappings)
            {
                var fieldName = fieldMapping.Key;
                var fieldId = fieldMapping.Value;

                if (formData.TryGetValue(fieldName, out var value))
                {
                    SendTextToInput(RegisterPage.InputFields(fieldId), value);

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
