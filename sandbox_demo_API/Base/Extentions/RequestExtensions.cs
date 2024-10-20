using RestSharp;
using sandbox_demo_API_Configs;

namespace web_api_sandbox_demo_API.Base.Extentions
{
    public static class RequestExtensions
    {

        public static RestRequest GetRequest(ScenarioContext context)
        {
            return context["request"] as RestRequest;
        }

        public static RestRequest CreateRequestWithMethod(string endpoint, Method method, ScenarioContext context)
        {
            var baseUrl = ConfigurationLoader.GetBaseUrl();
            var fullUrl = $"{baseUrl}{endpoint}";
            var request = new RestRequest(fullUrl, method);

            context["request"] = request;

            return request;
        }
    }

}
