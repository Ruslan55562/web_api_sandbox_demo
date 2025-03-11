using Newtonsoft.Json;
using RestSharp;
using sandbox_demo_API.Models.Request_Models;
using sandbox_demo_Shared.Configs;

namespace web_api_sandbox_demo_API.Base.Extentions
{
    public static class RequestExtensions
    {
        public static RestRequest GetRequest(ScenarioContext context)
        {
            return context["request"] as RestRequest;
        }

        public static RestRequest CreateRequestWithMethod(string endpoint, Method method, ScenarioContext context, string baseUrl = null)
        {
            baseUrl ??= AppConfig.GetBaseUrl();
            var fullUrl = $"{baseUrl}{endpoint}";
            var request = new RestRequest(fullUrl, method);

            context["request"] = request;

            return request;
        }

        public static void AddQueryParametersFromFile(RestRequest request, string fileName)
        {
            var parameters = LoadJsonData<Dictionary<string, string>>(fileName);
            foreach (var param in parameters)
            {
                request.AddQueryParameter(param.Key, param.Value);
            }
        }

        public static void SetParametersAndBodyFromFile(RestRequest request, string filePath)
        {
            var json = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", filePath));
            var jsonObject = JsonConvert.DeserializeObject<BillPayRequestModel>(json);

            request.AddQueryParameter("accountId", jsonObject.accountId.ToString());
            request.AddQueryParameter("amount", jsonObject.amount.ToString());
            request.AddJsonBody(JsonConvert.SerializeObject(jsonObject.body));
        }

        private static T? LoadJsonData<T>(string fileName)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", fileName);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The json file isn't found: {filePath}");
            }
            var jsonContent = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(jsonContent);
        }
    }
}
