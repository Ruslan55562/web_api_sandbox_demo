using Newtonsoft.Json;
using RestSharp;
using sandbox_demo_API.Models.Request_Models;
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

            var body = JsonConvert.SerializeObject(jsonObject.body);
            request.AddJsonBody(body);
        }

        public static RestRequest CreateRegistrationRequest(ScenarioContext context, Dictionary<string, string> customerData, string endpoint, string method)
        {
            if (!method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"The method '{method}' is not supported. Expected 'POST'");
            }

            var jSessionId = InitializeSession(context);

            var requestContent = CreateRegistrationRequestBody(customerData);

            var request = new RestRequest(ConfigurationLoader.GetWebAppUrl() + endpoint, Method.Post)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddHeader("Cookie", jSessionId);
            request.AddParameter("application/x-www-form-urlencoded", requestContent, ParameterType.RequestBody);

            return request;
        }

        private static string InitializeSession(ScenarioContext context)
        {
            var client = context["client"] as RestClient;
            var request = new RestRequest(ConfigurationLoader.GetWebAppUrl() + "/index.htm", Method.Get);

            var response = client.Execute(request);

            if (response.Cookies.FirstOrDefault(c => c.Name == "JSESSIONID") is { } sessionCookie)
            {
                context["JSESSIONID"] = $"JSESSIONID={sessionCookie.Value}";
            }
            else
            {
                throw new InvalidOperationException("JSESSIONID not found in response cookies.");
            }

            return context["JSESSIONID"] as string ?? throw new InvalidOperationException("JSESSIONID is missing.");
        }




        private static string CreateRegistrationRequestBody(Dictionary<string, string> customerData)
        {
            var parameters = new List<string>
            {
                $"customer.firstName={customerData["FirstName"].Replace(" ", "+")}",
                $"customer.lastName={customerData["LastName"].Replace(" ", "+")}",
                $"customer.address.street={customerData["Street"].Replace(" ", "+")}",
                $"customer.address.city={customerData["City"].Replace(" ", "+")}",
                $"customer.address.state={customerData["State"].Replace(" ", "+")}",
                $"customer.address.zipCode={customerData["ZipCode"]}",
                $"customer.phoneNumber={customerData["PhoneNumber"]}",
                $"customer.ssn={customerData["SSN"]}",
                $"customer.username={customerData["Username"]}",
                $"customer.password={customerData["Password"]}",
                $"repeatedPassword={customerData["Password"]}"
            };

            return string.Join("&", parameters);
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
