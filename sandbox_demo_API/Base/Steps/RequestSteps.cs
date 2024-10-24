using RestSharp;
using web_api_sandbox_demo_API.Base.Extentions;

namespace web_api_sandbox_demo_API.Base.Steps
{
    [Binding]
    public class RequestSteps
    {
        private readonly ScenarioContext _context;

        public RequestSteps(ScenarioContext context)
        {
            _context = context;
        }

        [Given(@"I create a request to endpoint ""(.*)"" with ""(.*)"" method")]
        public void GivenICreateARequestToEndpointWithMethod(string endpoint, string method)
        {
            var httpMethod = (Method)Enum.Parse(typeof(Method), method, true);
            var request = RequestExtensions.CreateRequestWithMethod(endpoint, httpMethod, _context);
            _context["request"] = request;
        }

        [Given(@"I send a request to '([^']*)' the database")]
        [When(@"I send a request to '([^']*)' the database")]
        public void WhenISendARequestToTheDatabase(string endpoint)
        {
            var request = RequestExtensions.CreateRequestWithMethod(endpoint, Method.Post, _context);
            _context["response"] = ResponseExtensions.SendTheRequest(_context["client"] as RestClient, request);
        }

        [When(@"I set the request parameters from file ""([^""]*)""")]
        public void WhenISetTheRequestParametersFromFile(string filePath)
        {
            var request = (RestRequest)_context["request"];
            RequestExtensions.AddQueryParametersFromFile(request, filePath);
            _context["request"] = request;
        }

        [When(@"I set the request parameters and body from file ""([^""]*)""")]
        public void GivenISetTheRequestParametersAndBodyFromFile(string filePath)
        {
            var request = (RestRequest)_context["request"];
            RequestExtensions.SetParametersAndBodyFromFile(request, filePath);
            _context["request"] = request;
        }
    }

}
