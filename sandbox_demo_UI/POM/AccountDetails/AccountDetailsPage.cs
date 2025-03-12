using web_api_sandbox_demo_UI.CommonPageSpace;
using OpenQA.Selenium;
using web_api_sandbox_demo_UI.Helpers;

namespace web_api_sandbox_demo_UI.POM.AccountDetails
{
    public class AccountDetailsPage : CommonPage
    {
        private readonly AccountDetailsMap _accountDetailsMap;
        private readonly AccountDetailsAssertions _accountDetailsAssertion;
        private readonly IWebDriver _driver;

        public AccountDetailsPage(IWebDriver driver, AccountDetailsAssertions accountDetailsAssertions, AccountDetailsMap accountDetailsMap)
        {
            _driver = driver;
            _accountDetailsAssertion = accountDetailsAssertions;
            _accountDetailsMap = accountDetailsMap;
        }

        public IWebElement AccountDetailsTitle => _driver.FindElement(By.XPath("//h1[.='Account Details']"));
        public IList<IWebElement> AccountDetailsTableHeaders => _driver.FindElements(By.XPath("//div[@ng-if='showDetails']//td[following-sibling::*]"));
        public IList<IWebElement> AccountDetailsTableValues => _driver.FindElements(By.XPath("//div[@ng-if='showDetails']//td[@class='ng-binding']"));
        public IList<IWebElement> AccountActivityTableHeaders => _driver.FindElements(By.XPath("//table[@class='form_activity']//b"));
        public IList<IWebElement> AccountActivityTableValues => _driver.FindElements(By.XPath("//table[@class='form_activity']//option[not(preceding-sibling::option)]"));
        public IList<IWebElement> AccountActivityTransactionHeaders => _driver.FindElements(By.XPath("//table[@id='transactionTable']//th"));
        public IList<IWebElement> AccountActivityTransactionValues => _driver.FindElements(By.XPath("//table[@id='transactionTable']//td"));
        public IList<IWebElement> TransactionDetailsTableHeaders => _driver.FindElements(By.XPath("//table//b"));
        public IList<IWebElement> TransactionDetailsTableValues => _driver.FindElements(By.XPath("//table//td[preceding-sibling::*]"));

        private IWebElement AccountNumberLink(string number) => _driver.FindElement(By.XPath($"(//table[@id='accountTable']//a)[{number}]"));
        private IWebElement TransactionNumberLink(string number) => _driver.FindElement(By.XPath($"(//table[@id='transactionTable']//a)[{number}]"));

        public AccountDetailsPage ClickOnAccountLink(string number)
        {
            AccountNumberLink(number).Click();
            return this;
        }

        public AccountDetailsPage ClickOnTransactionLink(string number)
        {
            TransactionNumberLink(number).ClickButtonWithWait(_driver, TimeSpan.FromSeconds(0.5));
            return this;
        }

        public AccountDetailsPage VerifyAccountDetailsData(Table accountDetails)
        {
            _accountDetailsAssertion.VerifyAccountDetailsData(accountDetails, this);
            return this;
        }

        public AccountDetailsPage VerifyAccountActivityData(Table accountActivity)
        {
            _accountDetailsAssertion.VerifyAccountActivityData(accountActivity, this);
            return this;
        }

        public AccountDetailsPage VerifyAccountActivityTransactionData(Table accountActivityTransactionData)
        {
            _accountDetailsAssertion.VerifyAccountActivityTransactionData(accountActivityTransactionData, this);
            return this;
        }

        public AccountDetailsPage VerifyTransactionDetailsData(Table transactionData)
        {
            _accountDetailsAssertion.VerifyTransactionData(transactionData, this);
            return this;
        }
    }
}