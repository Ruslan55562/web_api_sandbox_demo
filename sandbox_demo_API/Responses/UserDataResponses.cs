using Newtonsoft.Json;
using NUnit.Framework.Legacy;
using sandbox_demo_API.Models.Instance_Models;
using sandbox_demo_API.Models.Response_Models;
using System.Globalization;

namespace sandbox_demo_API.Responses
{
    public class UserDataResponses
    {
        public static void GetAmountFromResponse(string jsonResponse, string amount)
        {
            var accounts = JsonConvert.DeserializeObject<List<CustomerAccountsModel>>(jsonResponse);

            ClassicAssert.AreEqual(amount, accounts.FirstOrDefault()?.balance.ToString("0.##", CultureInfo.InvariantCulture),
                "The amount is not equal to default value");
        }

        public static void ValidateResponseData(GetUserDataModel responseData, UserDataInstanceModel expectedData)
        {
            ClassicAssert.Multiple(() =>
            {
                ClassicAssert.AreEqual(expectedData.FirstName, responseData.FirstName, "First name does not match.");
                ClassicAssert.AreEqual(expectedData.LastName, responseData.LastName, "Last name does not match.");
                ClassicAssert.AreEqual(expectedData.Street, responseData.Address.Street, "Street does not match.");
                ClassicAssert.AreEqual(expectedData.City, responseData.Address.City, "City does not match.");
                ClassicAssert.AreEqual(expectedData.State, responseData.Address.State, "State does not match.");
                ClassicAssert.AreEqual(expectedData.ZipCode, responseData.Address.ZipCode, "Zip code does not match.");
                ClassicAssert.AreEqual(expectedData.PhoneNumber, responseData.PhoneNumber, "Phone number does not match.");
                ClassicAssert.AreEqual(expectedData.SSN, responseData.SSN, "SSN does not match.");
            });
        }
    }
}
