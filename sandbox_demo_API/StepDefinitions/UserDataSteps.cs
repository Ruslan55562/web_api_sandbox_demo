using Newtonsoft.Json;
using RestSharp;
using sandbox_demo_API.Models.Instance_Models;
using sandbox_demo_API.Models.Response_Models;
using System.Net;
using TechTalk.SpecFlow.Assist;
using web_api_sandbox_demo_API.Base.Extentions;

namespace sandbox_demo_API.StepDefinitions
{
    [Binding]
    public class UserDataSteps
    {
        private readonly ScenarioContext _context;

        public UserDataSteps(ScenarioContext context)
        {
            _context = context;
        }

        [Given(@"I prepare registration with the following data")]
        public void GivenIPrepareRegistrationWithTheFollowingData(Table userData)
        {
            var customerData = new Dictionary<string, string>();
            foreach (var row in userData.Rows)
            {
                foreach (var column in row)
                {
                    customerData[column.Key] = column.Value;
                }
            }
            _context["RegistrationData"] = customerData;
        }

        [Given(@"I create a registration request to endpoint ""([^""]*)"" with ""([^""]*)"" method")]
        public void GivenICreateARegistrationRequestToEndpointWithMethod(string endpoint, string method)
        {
            var customerData = _context["RegistrationData"] as Dictionary<string, string>;
            var request = RequestExtensions.CreateRegistrationRequest(_context,customerData, endpoint, method);
            _context["request"] = request;
        }

        [Then(@"the response should have the following data")]
        public void ThenTheResponseShouldHaveTheFollowingData(Table table)
        {
            var response = _context["response"] as RestResponse;

            if (response == null || response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("No valid response received for the GET request.");
            }

            var responseData = JsonConvert.DeserializeObject<GetUserDataModel>(response.Content);

            var expectedData = table.CreateInstance<UserDataInstanceModel>();

            ResponseExtensions.ValidateResponseData(responseData, expectedData);
        }

        [Then(@"The default amount of newly created account is ""([^""]*)""")]
        public void ThenTheDefaultAmountOfNewlyCreatedAccountIs(string amount)
        {
            var response = _context["response"] as RestResponse;
            ResponseExtensions.GetAmountFromResponse(response.Content,amount);
        }
    }
}
