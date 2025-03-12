using sandbox_demo_UI.PageForms;
using web_api_sandbox_demo_UI.CommonPageSpace;
using OpenQA.Selenium;
using web_api_sandbox_demo_UI.Helpers;

namespace sandbox_demo_UI.POM.FindTransaction
{
    public class FindTransactionPage : CommonPage
    {
        private readonly FindTransactionAssertions _findTransactionAssertions;
        private readonly FindTransactionMap _findTransactionMap;
        private static IWebDriver _driver;

        public FindTransactionPage(IWebDriver driver, FindTransactionAssertions findTransactionAssertions, FindTransactionMap findTransactionMap)
        {
            _driver = driver;
            _findTransactionAssertions = findTransactionAssertions;
            _findTransactionMap = findTransactionMap;
        }

        public static IWebElement SelectAccountDropdown => _driver.FindElement(By.XPath("//select[@id='accountId']"));
        public static IWebElement FindTransactionByIdField => _driver.FindElement(By.XPath("//input[@id='criteria.transactionId']"));
        public static IWebElement FindAccountByDateField => _driver.FindElement(By.XPath("//input[@id='criteria.onDate']"));
        public static IWebElement FindAccountFromDateField => _driver.FindElement(By.XPath("//input[@id='criteria.fromDate']"));
        public static IWebElement FindAccountToDateField => _driver.FindElement(By.XPath("//input[@id='criteria.toDate']"));
        public static IWebElement FindAccountAmountField => _driver.FindElement(By.XPath("//input[@id='criteria.amount']"));
        public static IWebElement AccountIdOption(string id) => _driver.FindElement(By.XPath($"//option[.='{id}']"));

        public FindTransactionPage ChooseAccountToFindTransaction(string accountId)
        {
            SelectAccountDropdown.ClickButtonWithWait(_driver, TimeSpan.FromSeconds(1));
            AccountIdOption(accountId).Click();
            return this;
        }

        public FindTransactionPage SearchTransactionById(string transactionId)
        {
            FindTransactionByIdField.SendTextToInput(_driver, transactionId);
            return this;
        }

        public FindTransactionPage SearchTransactionByDate(string date)
        {
            FindAccountByDateField.SendTextToInput(_driver, date);
            return this;
        }

        public FindTransactionPage SearchTransactionByDateRange(string fromDate, string toDate)
        {
            FindAccountFromDateField.SendTextToInput(_driver, fromDate);
            FindAccountToDateField.SendTextToInput(_driver, toDate);
            return this;
        }

        public FindTransactionPage SearchTransactionByAmount(string amount)
        {
            FindAccountAmountField.SendTextToInput(_driver, amount);
            return this;
        }

        public FindTransactionPage ClickFindButton(string byOption)
        {
            GenerateTransactionButtonElement(byOption).ClickButtonWithWait(_driver, TimeSpan.FromSeconds(0.5));
            return this;
        }

        private IWebElement GenerateTransactionButtonElement(string searchTerm)
        {
            if (SearchTransactionType.searchTermMap.TryGetValue(searchTerm, out string xpath))
            {
                return _driver.FindElement(By.XPath(xpath));
            }

            throw new ArgumentException($"Unknown search term: {searchTerm}");
        }
    }
}