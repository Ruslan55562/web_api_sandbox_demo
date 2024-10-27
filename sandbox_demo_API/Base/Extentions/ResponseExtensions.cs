using Newtonsoft.Json;
using NUnit.Framework.Legacy;
using RestSharp;
using sandbox_demo_API.Models.Instance_Models;
using sandbox_demo_API.Models.Response_Models;
using System.Globalization;

namespace web_api_sandbox_demo_API.Base.Extentions
{
    public static class ResponseExtensions
    {
        public static RestResponse SendTheRequest(RestClient client, RestRequest request)
        {
            if (client == null || request == null)
            {
                throw new InvalidOperationException("Client or request cannot be null.");
            }

            try
            {
                return client.Execute(request);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error sending the request: " + ex.Message, ex);
            }
        }

        public static void VerifyBillPaymentResult(ScenarioContext context, BillPayResponseModel expected)
        {
            var response = context["response"] as RestResponse;

            ClassicAssert.IsNotNull(response, "The response got from scenario context is null");

            var result = JsonConvert.DeserializeObject<BillPayResponseModel>(response.Content);

            ClassicAssert.IsNotNull(result, "The deserialized response is null");

            ClassicAssert.AreEqual(expected.AccountId, result.AccountId, "Account ID is not the same as expected");
            ClassicAssert.AreEqual(expected.Amount, result.Amount, "Amount is not the same as expected");
            ClassicAssert.AreEqual(expected.PayeeName, result.PayeeName, "PayeeName is not the same as expected");
        }

        public static void VerifyBalanceChange
            (string accountFromId, double initialBalanceFrom, string accountToId, double initialBalanceTo, double amount, ScenarioContext context)
        {
            var responseFrom = GetAccountBalance(accountFromId, context);
            double updatedBalanceFrom = responseFrom.Balance;

            var responseTo = GetAccountBalance(accountToId, context);
            double updatedBalanceTo = responseTo.Balance;

            ClassicAssert.AreEqual(initialBalanceFrom - amount, updatedBalanceFrom,
                $"Balance for account {accountFromId} did not decrease by {amount}. Expected: {initialBalanceFrom - amount} but got: {updatedBalanceFrom}"
            );

            ClassicAssert.AreEqual(initialBalanceTo + amount, updatedBalanceTo,
                $"Balance for account {accountToId} did not increase by {amount}. Expected: {initialBalanceTo + amount} but got: {updatedBalanceTo}"
            );
        }

        public static void VerifyBalanceChangeForSpecificAccount
          (string accountFromId, double initialBalanceFrom, double amount, ScenarioContext context)
        {
            var responseFrom = GetAccountBalance(accountFromId, context);
            double updatedBalanceFrom = responseFrom.Balance;

            ClassicAssert.AreEqual(initialBalanceFrom - amount, updatedBalanceFrom,
                $"Balance for account {accountFromId} did not decrease by {amount}. Expected: {initialBalanceFrom - amount} but got: {updatedBalanceFrom}"
            );
        }

        public static void VerifyResponseMessage(ScenarioContext context, string expectedMessage)
        {
            var response = context["response"] as RestResponse;
            var actualMessage = response?.Content ?? string.Empty;

            ClassicAssert.AreEqual(expectedMessage, actualMessage, "The response message does not match the expected one");
        }

        public static void ResponseStatusCode(RestResponse response, int expectedStatusCode)
        {
            var actualStatusCode = (int)response.StatusCode;
            ClassicAssert.AreEqual(expectedStatusCode, actualStatusCode, $"Unexpected Status Code {actualStatusCode}");
        }

        public static void GetAmountFromResponse(string jsonResponse, string amount)
        {
            var accounts = JsonConvert.DeserializeObject<List<CustomerAccountsModel>>(jsonResponse);
            ClassicAssert.AreEqual(amount, accounts.FirstOrDefault()?.balance.ToString("0.##", CultureInfo.InvariantCulture),
                "The amount is not equal to default value");
        }

        public static void ValidateResponseData(GetUserDataModel responseData, UserDataInstanceModel expectedData)
        {
            ClassicAssert.AreEqual(expectedData.FirstName, responseData.FirstName, "First name does not match.");
            ClassicAssert.AreEqual(expectedData.LastName, responseData.LastName, "Last name does not match.");
            ClassicAssert.AreEqual(expectedData.Street, responseData.Address.Street, "Street does not match.");
            ClassicAssert.AreEqual(expectedData.City, responseData.Address.City, "City does not match.");
            ClassicAssert.AreEqual(expectedData.State, responseData.Address.State, "State does not match.");
            ClassicAssert.AreEqual(expectedData.ZipCode, responseData.Address.ZipCode, "Zip code does not match.");
            ClassicAssert.AreEqual(expectedData.PhoneNumber, responseData.PhoneNumber, "Phone number does not match.");
            ClassicAssert.AreEqual(expectedData.SSN, responseData.SSN, "SSN does not match.");
        }

        private static GetAccountByIdModel GetAccountBalance(string accountId, ScenarioContext context)
        {
            var client = context["client"] as RestClient;
            var endpoint = $"/accounts/{accountId}";

            if (client == null)
            {
                throw new InvalidOperationException("RestClient not found in ScenarioContext");
            }

            var request = new RestRequest(endpoint, Method.Get);

            var response = client.Execute<GetAccountByIdModel>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to get balance for account {accountId}. Status Code: {response.StatusCode}");
            }

            return response.Data;
        }
    }
}
