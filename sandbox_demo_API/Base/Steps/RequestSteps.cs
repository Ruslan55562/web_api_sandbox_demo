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

        [When(@"I send a request to '([^']*)' the database")]
        public void WhenISendARequestToTheDatabase(string endpoint)
        {
            var request = RequestExtensions.CreateRequestWithMethod(endpoint, Method.Post, _context);
            _context["response"] = ResponseExtensions.SendTheRequest(_context["client"] as RestClient, request);
        }
    }

}
