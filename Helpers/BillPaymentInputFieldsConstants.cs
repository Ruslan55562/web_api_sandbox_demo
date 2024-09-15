using System;

namespace web_api_sandbox_demo_UI.Helpers
{
    public static class BillPaymentInputFieldsConstants
    {
        public static string payeeNameInputField = "payee.name";
        public static string adressInputField = "payee.address.street";
        public static string cityInputField = "payee.address.city";
        public static string stateInputField = "payee.address.state";
        public static string zipCodeInputFie = "payee.address.zipCode";
        public static string phoneInputField = "payee.phoneNumber";
        public static string accountInputField = "payee.accountNumber";
        public static string verifyAccountInputField = "verifyAccount";
        public static string amountInputField = "amount";


        public static readonly Dictionary<string, string> FieldIdMappings = new Dictionary<string, string>
        {
            { "Payee Name", payeeNameInputField },
            { "Address", adressInputField },
            { "City", cityInputField },
            { "State", stateInputField },
            { "Zip Code", zipCodeInputFie },
            { "Phone", phoneInputField },
            { "Account", accountInputField },
            { "Verify Account", verifyAccountInputField },
            { "Amount", amountInputField }
        };
    }
}
