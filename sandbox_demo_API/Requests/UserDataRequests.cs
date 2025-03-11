using RestSharp;
using sandbox_demo_API.Utils;
using sandbox_demo_Shared.Configs;
using System.Net;
using web_api_sandbox_demo_API.Base.Extentions;

namespace sandbox_demo_API.Requests
{
    public class UserDataRequests
    {
        public static RestRequest CreateRegistrationRequest(ScenarioContext context, Dictionary<string, string> customerData, string endpoint, string method)
        {
            if (!method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"The method '{method}' is not supported. Expected 'POST'");
            }

            InitializeSession(context);

            if (context["JSESSIONID"] is string sessionId)
            {
                var getRequest = RequestExtensions.CreateRequestWithMethod($"{PageUrls.RegisterPage};{sessionId}", Method.Get, context, AppConfig.GetWebAppUrl());
                ResponseExtensions.SendTheRequest(context["client"] as RestClient, getRequest);
            }

            var requestContent = CreateRegistrationRequestBody(customerData);
            var request = RequestExtensions.CreateRequestWithMethod(endpoint, Method.Post, context, AppConfig.GetWebAppUrl());
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/x-www-form-urlencoded", requestContent, ParameterType.RequestBody);

            return request;
        }

        private static void InitializeSession(ScenarioContext context)
        {
            var request = RequestExtensions.CreateRequestWithMethod(PageUrls.IndexPage, Method.Get, context, AppConfig.GetWebAppUrl());
            var response = ResponseExtensions.SendTheRequest(context["client"] as RestClient, request);

            if (response.Cookies.FirstOrDefault(c => c.Name == "JSESSIONID") is { } sessionCookie)
            {
                var cookieContainer = context["cookieContainer"] as CookieContainer;
                var existingCookie = cookieContainer?.GetCookies(new Uri(AppConfig.GetBaseUrl()))
                    .Cast<Cookie>()
                    .FirstOrDefault(c => c.Name == "JSESSIONID");

                if (existingCookie == null && cookieContainer != null)
                {
                    cookieContainer.Add(new Uri(AppConfig.GetBaseUrl()),
                                        new Cookie(sessionCookie.Name, sessionCookie.Value, sessionCookie.Path, sessionCookie.Domain));
                }
                context["JSESSIONID"] = $"{sessionCookie.Name}={sessionCookie.Value}";
            }
            else
            {
                throw new InvalidOperationException("JSESSIONID not found in response cookies.");
            }
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
    }
}
