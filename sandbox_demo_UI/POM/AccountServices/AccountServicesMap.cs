using Reqnroll.BoDi;
using sandbox_demo_UI.Map;
using web_api_sandbox_demo_UI.CommonPageSpace;

namespace web_api_sandbox_demo_UI.POM.AccountServices
{
    public class AccountServicesMap : CommonPageMap
    {
        public AccountServicesMap(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        public AccountServicesMap FillInInputRegistyFields(Table registryData)
        {
            var formData = registryData.Rows[0];

            foreach (var fieldMapping in BillPaymentInputFieldsModel.FieldIdMappings)
            {
                var fieldName = fieldMapping.Key;
                var fieldNgModel = fieldMapping.Value;

                if (formData.TryGetValue(fieldName, out var value))
                {
                    SendTextToInput(AccountServicesPage.BillPaymentInputFields(fieldNgModel), value);
                }
            }

            return this;
        }
    }
}
