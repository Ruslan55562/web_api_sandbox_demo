﻿using OpenQA.Selenium;
using Reqnroll.BoDi;
using web_api_sandbox_demo_UI.CommonPageSpace;

namespace web_api_sandbox_demo_UI.POM.AccountServices
{
    public class AccountServicesAssertions : CommonPageAssertions
    {
        private readonly AccountServicesMap _accountServicesMap;

        public AccountServicesAssertions(IObjectContainer objectContainer, AccountServicesMap accountServicesMap) : base(objectContainer)
        {
            _accountServicesMap = accountServicesMap;
        }

        public AccountServicesAssertions VerifyLoanResponseData(Table loanResponse, Dictionary<string, IWebElement> elements)
        {
            var expectedValues = new Dictionary<string, string>
            {
                { "Loan Provider", loanResponse.Rows[0]["Loan Provider"] },
                { "Status", loanResponse.Rows[0]["Status"] },
                { "Loan Response Date", DateTime.Now.ToString("MM-d-yyyy") }
            };

            foreach (var key in expectedValues.Keys)
            {
                var actualValue = elements[key].Text;
                AreEqual(expectedValues[key], actualValue, "The actual value is not the same as expected");
            }

            return this;
        }
    }
}