using Newtonsoft.Json;
using NUnit.Framework.Legacy;
using RestSharp;
using sandbox_demo_API.Models.Response_Models;

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

            ClassicAssert.IsNotNull(response,"The response got from scenario context is null");

            var result = JsonConvert.DeserializeObject<BillPayResponseModel>(response.Content);

            ClassicAssert.IsNotNull(result, "The deserialized response is null");

            ClassicAssert.AreEqual(expected.AccountId, result.AccountId, "Account ID is not the same as expected");
            ClassicAssert.AreEqual(expected.Amount, result.Amount, "Amount is not the same as expected");
            ClassicAssert.AreEqual(expected.PayeeName, result.PayeeName, "PayeeName is not the same as expected");
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
    }

}
