using Reqnroll.BoDi;
using sandbox_demo_UI.PageForms;
using web_api_sandbox_demo_UI.CommonPageSpace;
using web_api_sandbox_demo_UI.Helpers;
using web_api_sandbox_demo_UI_Drivers;

namespace web_api_sandbox_demo_UI.POM.AccountServices
{
    public class AccountServicesMap : CommonPageMap
    {
        public AccountServicesMap(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        public AccountServicesMap FillInInputRegistyFields(AccountServicesPage accountServicesPage, Table registryData)
        {
            var formData = registryData.Rows[0];

            foreach (var fieldMapping in BillPaymentInputFieldsPageForm.FieldIdMappings)
            {
                var fieldName = fieldMapping.Key;
                var fieldNgModel = fieldMapping.Value;

                if (formData.TryGetValue(fieldName, out var value))
                {
                    accountServicesPage.BillPaymentInputFields(fieldNgModel).SendTextToInput(DriverManager.GetDriverInstance(), value);
                }
            }

            return this;
        }
    }
}