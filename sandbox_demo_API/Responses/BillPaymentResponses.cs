using Newtonsoft.Json;
using NUnit.Framework.Legacy;
using RestSharp;
using sandbox_demo_API.Helpers;
using sandbox_demo_API.Models.Response_Models;
using sandbox_demo_API.Utils;
using web_api_sandbox_demo_API.Base.Extentions;

namespace sandbox_demo_API.Responses
{
    public class BillPaymentResponses
    {
        public static void VerifyBillPaymentResult(ScenarioContext context, BillPayResponseModel expected)
        {
            var response = context["response"] as RestResponse;

            ClassicAssert.IsNotNull(response, "The response got from scenario context is null");

            var result = JsonConvert.DeserializeObject<BillPayResponseModel>(response.Content);

            ClassicAssert.IsNotNull(result, "The deserialized response is null");

            ClassicAssert.Multiple(() =>
            {
                ClassicAssert.AreEqual(expected.AccountId, result.AccountId, "Account ID is not the same as expected");
                ClassicAssert.AreEqual(expected.Amount, result.Amount, "Amount is not the same as expected");
                ClassicAssert.AreEqual(expected.PayeeName, result.PayeeName, "PayeeName is not the same as expected");
            });
        }

        public static void VerifyBalanceChange
            (string accountFromId, double initialBalanceFrom, string accountToId, double initialBalanceTo, double amount, ScenarioContext context)
        {
            var responseFrom = GetAccountBalance(accountFromId, context);
            double updatedBalanceFrom = responseFrom.Data.Balance;

            var responseTo = GetAccountBalance(accountToId, context);
            double updatedBalanceTo = responseTo.Data.Balance;

            ClassicAssert.Multiple(() =>
            {
                ClassicAssert.AreEqual(initialBalanceFrom - amount, updatedBalanceFrom,
                    $"Balance for account {accountFromId} did not decrease by {amount}. Expected: {initialBalanceFrom - amount} but got: {updatedBalanceFrom}"
                );

                ClassicAssert.AreEqual(initialBalanceTo + amount, updatedBalanceTo,
                    $"Balance for account {accountToId} did not increase by {amount}. Expected: {initialBalanceTo + amount} but got: {updatedBalanceTo}"
                );
            });
        }

        public static void VerifyBalanceChangeForSpecificAccount
          (string accountFromId, double initialBalanceFrom, double amount, ScenarioContext context)
        {
            var responseFrom = GetAccountBalance(accountFromId, context);
            double updatedBalanceFrom = responseFrom.Data.Balance;

            ClassicAssert.AreEqual(initialBalanceFrom - amount, updatedBalanceFrom,
                $"Balance for account {accountFromId} did not decrease by {amount}. Expected: {initialBalanceFrom - amount} but got: {updatedBalanceFrom}"
            );
        }

        private static ApiResponseWrapper<GetAccountByIdModel> GetAccountBalance(string accountId, ScenarioContext context)
        {
            var client = context["client"] as RestClient;
            var endpoint = $"{PageUrls.AccountPage}/{accountId}";

            if (client == null)
            {
                throw new InvalidOperationException("RestClient not found in ScenarioContext");
            }

            var request = RequestExtensions.CreateRequestWithMethod(endpoint, Method.Get, context);
            var response = ResponseExtensions.SendTheRequest(client, request);

            var apiResponseWrapper = new ApiResponseWrapper<GetAccountByIdModel>
            {
                StatusCode = (int)response.StatusCode,
                StatusDescription = response.StatusDescription,
                IsSuccessful = response.IsSuccessful,
                Content = response.Content,
                Headers = response.Headers.ToDictionary(header => header.Name, header => header.Value)
            };

            if (apiResponseWrapper.IsSuccessful)
            {
                apiResponseWrapper.Data = JsonConvert.DeserializeObject<GetAccountByIdModel>(response.Content);
            }

            return apiResponseWrapper;
        }
    }
}
