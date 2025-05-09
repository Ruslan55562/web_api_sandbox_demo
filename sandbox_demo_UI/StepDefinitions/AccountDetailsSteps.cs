﻿using web_api_sandbox_demo_UI.POM.AccountDetails;

namespace web_api_sandbox_demo_UI.StepDefinitions
{
    [Binding]
    public class AccountDetailsSteps
    {
        private readonly AccountDetailsPage _accountDetailsPage;

        public AccountDetailsSteps(AccountDetailsPage accountDetailsPage)
        {
            _accountDetailsPage = accountDetailsPage;
        }

        [When(@"I click on '([^']*)' account number link")]
        public void WhenIClickOnAccountNumberLink(string accountNumber) =>
            _accountDetailsPage.ClickOnAccountLink(accountNumber);

        [Then(@"I can see account details with the next data:")]
        public void ThenICanSeeAccountDetailsWithTheNextData(Table accountDetailsData) =>
            _accountDetailsPage.VerifyAccountDetailsData(accountDetailsData);

        [Then(@"I can see account activity with the next data:")]
        public void ThenICanSeeAccountActivityWithTheNextData(Table accountActivityData) =>
            _accountDetailsPage.VerifyAccountActivityData(accountActivityData);

        [When(@"I click on '([^']*)' transaction")]
        public void WhenIClickOnTransaction(string transactionName) =>
            _accountDetailsPage.ClickOnTransactionLink(transactionName);

        [Then(@"I can see trasaction details with the next data:")]
        public void ThenICanSeeTrasactionDetailsWithTheNextData(Table transactionDetails) =>
            _accountDetailsPage.VerifyTransactionDetailsData(transactionDetails);

        [Then(@"I can see account activity transaction table with the next data:")]
        public void ThenICanSeeAccountActivityTransactionTableWithTheNextData(Table accountActivityTransactionData) =>
            _accountDetailsPage.VerifyAccountActivityTransactionData(accountActivityTransactionData);
    }
}