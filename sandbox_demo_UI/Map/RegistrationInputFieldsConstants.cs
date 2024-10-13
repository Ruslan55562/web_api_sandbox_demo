using System;

namespace sandbox_demo_UI.Map
{
    public static class RegistrationInputFieldsConstants
    {
        public static string firstNameInputFieldId = "customer.firstName";
        public static string lastNameInputFieldId = "customer.lastName";
        public static string addressInputFieldId = "customer.address.street";
        public static string cityInputFieldId = "customer.address.city";
        public static string stateInputFieldId = "customer.address.state";
        public static string zipCodeInputFieldId = "customer.address.zipCode";
        public static string phoneInputFieldId = "customer.phoneNumber";
        public static string ssnInputFieldId = "customer.ssn";
        public static string userNameInputFieldId = "customer.username";
        public static string passwordInputFieldId = "customer.password";
        public static string confirmPasswordInputFieldId = "repeatedPassword";


        public static readonly Dictionary<string, string> FieldIdMappings = new Dictionary<string, string>
        {
            { "First Name", firstNameInputFieldId },
            { "Last Name", lastNameInputFieldId },
            { "Address", addressInputFieldId },
            { "City", cityInputFieldId },
            { "State", stateInputFieldId },
            { "Zip Code", zipCodeInputFieldId },
            { "Phone", phoneInputFieldId },
            { "SSN", ssnInputFieldId },
            { "Username", userNameInputFieldId },
            { "Password", passwordInputFieldId },
            { "Confirm", confirmPasswordInputFieldId }
        };
    }
}
