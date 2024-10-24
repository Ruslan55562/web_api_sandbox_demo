using sandbox_demo_API.Models.Response_Models;
using TechTalk.SpecFlow.Assist;
using web_api_sandbox_demo_API.Base.Extentions;

namespace web_api_sandbox_demo_API.StepDefinitions
{
    [Binding]
    public class BillPaymentSteps
    {
        private readonly ScenarioContext _context;

        public BillPaymentSteps(ScenarioContext context)
        {
            _context = context;
        }


        [Then(@"The bill payment result should be:")]
        public void ThenTheBillPaymentResultShouldBe(Table billPayment)
        {
            var expected = billPayment.CreateInstance<BillPayResponseModel>();
            ResponseExtensions.VerifyBillPaymentResult(_context, expected);
        }
    }
}
