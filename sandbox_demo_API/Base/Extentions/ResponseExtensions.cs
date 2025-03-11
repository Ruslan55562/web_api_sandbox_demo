using NUnit.Framework.Legacy;
using RestSharp;

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
