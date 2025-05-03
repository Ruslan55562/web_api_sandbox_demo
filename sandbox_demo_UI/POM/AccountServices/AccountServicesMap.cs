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

            foreach (var fieldMapping in BillPaymentPageForm.FieldIdMappings)
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

        public List<string> GetAccountTableData(Table balanceData, AccountServicesPage page)
        {
            var accountDetailsData = balanceData.Rows[0];
            var expectedHeaders = accountDetailsData.Keys.ToList();
            var expectedValues = accountDetailsData.Values.ToList();

            return new List<string>
            {
                page.BalanceInTableValue(expectedHeaders[0], expectedValues[0]).Text,
                page.AvailableAmountInTableValue(expectedHeaders[1], expectedValues[1]).Text,
                page.TotalInTableValue(expectedHeaders[2], expectedValues[2]).Text
            };
        }
    }
}