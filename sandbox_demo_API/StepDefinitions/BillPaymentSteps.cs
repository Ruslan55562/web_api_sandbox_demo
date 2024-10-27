﻿using sandbox_demo_API.Models.Response_Models;
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

        [Then(@"I verify balance change of account ""([^""]*)"" from initial balance ""([^""]*)"" and account ""([^""]*)"" from initial balance ""([^""]*)"" with amount ""([^""]*)""")]
        public void ThenIVerifyBalanceChangeOfAccountFromInitialBalanceAndAccountFromInitialBalanceWithAmount
            (string accountOne, double balanceOne, string accountTwo, double balanceTwo, double amount)
        {
            ResponseExtensions.VerifyBalanceChange(accountOne, balanceOne, accountTwo, balanceTwo, amount, _context);
        }

        [Then(@"I verify balance change of account ""([^""]*)"" from initial balance ""([^""]*)"" with amount ""([^""]*)""")]
        public void ThenIVerifyBalanceChangeOfAccountFromInitialBalanceWithAmount(string accountId, double initialBalance, double amount)
        {
            ResponseExtensions.VerifyBalanceChangeForSpecificAccount(accountId, initialBalance, amount, _context);
        }

    }
}
