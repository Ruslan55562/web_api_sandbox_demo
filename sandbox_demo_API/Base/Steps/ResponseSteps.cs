using RestSharp;
using web_api_sandbox_demo_API.Base.Extentions;

[Binding]
public class ResponseSteps
{
    private readonly ScenarioContext _context;

    public ResponseSteps(ScenarioContext context)
    {
        _context = context;
    }

    [When(@"I send the request")]
    public void WhenISendTheRequest()
    {
        var client = _context["client"] as RestClient;
        var request = _context["request"] as RestRequest;

        _context["response"] = ResponseExtensions.SendTheRequest(client, request);
    }

    [Then(@"I should see status code (.*)")]
    public void ThenIShouldSeeStatusCode(int expectedStatusCode)
    {
        var response = _context["response"] as RestResponse;
        ResponseExtensions.ResponseStatusCode(response, expectedStatusCode);
    }
}
