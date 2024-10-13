using sandbox_demo_UI.Map;
using web_api_sandbox_demo_UI.CommonPageSpace;


namespace sandbox_demo_UI.POM.FindTransaction
{
    public class FindTransactionPage : CommonPage
    {
        private readonly FindTransactionAssertions _findTransactionAssertions;
        private readonly FindTransactionMap _findTransactionMap;

        private string selectAccountDropdown = "//select[@id='accountId']";
        private string findTransactionByIdField = "//input[@id='criteria.transactionId']";
        private string findAccountByDateField = "//input[@id='criteria.onDate']";
        private string findAccountFromDateField = "//input[@id='criteria.fromDate']";
        private string findAccountToDateField = "//input[@id='criteria.toDate']";
        private string findAccountAmountField = "//input[@id='criteria.amount']";
        private string AccountIdOption(string id) => $"//option[.='{id}']";

        public FindTransactionPage(FindTransactionAssertions findTransactionAssertions, FindTransactionMap findTransactionMap)
        {
            _findTransactionAssertions = findTransactionAssertions;
            _findTransactionMap = findTransactionMap;
        }

        public FindTransactionPage ChooseAccountToFindTransaction(string accountId)
        {
            _findTransactionMap.ClickButtonWithWait(selectAccountDropdown, TimeSpan.FromSeconds(1));
            _findTransactionMap.ClickButton(AccountIdOption(accountId));
            return this;
        }

        public FindTransactionPage SearchTransactionById(string transactionId)
        {
            _findTransactionMap.SendTextToInput(findTransactionByIdField, transactionId);
            return this;
        }

        public FindTransactionPage SearchTransactionByDate(string date)
        {
            _findTransactionMap.SendTextToInput(findAccountByDateField, date);
            return this;
        }

        public FindTransactionPage SearchTransactionByDateRange(string fromDate, string toDate)
        {
            _findTransactionMap.SendTextToInput(findAccountFromDateField, fromDate);
            _findTransactionMap.SendTextToInput(findAccountToDateField, toDate);
            return this;
        }

        public FindTransactionPage SearchTransactionByAmount(string amount)
        {
            _findTransactionMap.SendTextToInput(findAccountAmountField, amount);
            return this;
        }

        public FindTransactionPage ClickFindButton(string byOption)
        {
            _findTransactionMap.ClickButtonWithWait(GenerateTransactionButtonXPath(byOption),TimeSpan.FromSeconds(0.5));
            return this;
        }

        private string GenerateTransactionButtonXPath(string searchTerm)
        {
            if (SearchTransactionType.searchTermMap.TryGetValue(searchTerm, out string xpath))
            {
                return xpath;
            }

            throw new ArgumentException($"Unknown search term: {searchTerm}");
        }
    }
}
