using sandbox_demo_UI.POM.FindTransaction;
using System;

namespace sandbox_demo_UI.StepDefinitions
{
    [Binding]
    public class FindTransactionSteps
    {
        private readonly FindTransactionPage _findTransactionPage;

        public FindTransactionSteps(FindTransactionPage findTransactionPage)
        {
            _findTransactionPage = findTransactionPage; 
        }

        [When(@"I select '([^']*)' account")]
        public void WhenISelectAccount(string accountId) => 
            _findTransactionPage.ChooseAccountToFindTransaction(accountId);

        [When(@"I enter '([^']*)' to transaction id field")]
        public void WhenIEnterToTransactionIdField(string transactionId) => 
            _findTransactionPage.SearchTransactionById(transactionId);

        [When(@"I enter '([^']*)' to transaction date field")]
        public void WhenIEnterToTransactionDateField(string date) =>
            _findTransactionPage.SearchTransactionByDate(date);

        [When(@"I enter '([^']*)' from to '([^']*)' date")]
        public void WhenIEnterFromToDate(string fromDate, string toDate) =>
            _findTransactionPage.SearchTransactionByDateRange(fromDate, toDate);

        [When(@"I enter '([^']*)' amount")]
        public void WhenIEnterAmount(string amount) => 
            _findTransactionPage.SearchTransactionByAmount(amount);

        [When(@"I search transaction by '([^']*)'")]
        public void WhenISearchTransactionBy(string searchOption) => 
            _findTransactionPage.ClickFindButton(searchOption);
    }
}
