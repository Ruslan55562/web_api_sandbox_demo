using web_api_sandbox_demo_UI.CommonPageSpace;

namespace web_api_sandbox_demo_UI.POM.AccountDetails
{
    public class AccountDetailsPage : CommonPage
    {
        private readonly AccountDetailsMap _accountDetailsMap;
        private readonly AccountDetailsAssertions _accountDetailsAssertion;

        private string accountDetailsTitle = "//h1[.='Account Details']";
        private string accountDetailsTableHeaders = "//div[@ng-if='showDetails']//td[following-sibling::*]";
        private string accountDetailsTableValues = "//div[@ng-if='showDetails']//td[@class='ng-binding']";
        private string accountActivityTableHeaders= "//table[@class='form_activity']//b";
        private string accountActivityTableValues = "//table[@class='form_activity']//option[not(preceding-sibling::option)]";
        private string accountActivityTransactionHeaders = "//table[@id='transactionTable']//th";
        private string accountActivityTransactionValues = "//table[@id='transactionTable']//td";
        private string transactionDetailsTableHeaders = "//table//b";
        private string transactionDetailsTableValues = "//table//td[preceding-sibling::*]";

        private string AccountNumberLink(string number) => $"(//table[@id='accountTable']//a)[{number}]";
        private string TransactionNumberLink(string number) => $"(//table[@id='transactionTable']//a)[{number}]";



        public AccountDetailsPage(AccountDetailsAssertions accountDetailsAssertions, AccountDetailsMap accountDetailsMap)
        {
            _accountDetailsAssertion = accountDetailsAssertions;
            _accountDetailsMap = accountDetailsMap;
        }

        public AccountDetailsPage ClickOnAccountLink(string number)
        {
            _accountDetailsMap.ClickButton(AccountNumberLink(number));
            return this;
        }

        public AccountDetailsPage ClickOnTransactionLink(string number)
        {
            _accountDetailsMap.ClickButtonWithWait(TransactionNumberLink(number),TimeSpan.FromSeconds(0.5));
            return this;
        }

        public AccountDetailsPage VerifyAccountDetailsData(Table accountDetails)
        {
            _accountDetailsAssertion.IsElementDisplayed(accountDetailsTitle, "The page title is not displayed");
            _accountDetailsAssertion.VerifyAccountDetailsData(accountDetails, accountDetailsTableHeaders, accountDetailsTableValues);
            return this;
        }

        public AccountDetailsPage VerifyAccountActivityData(Table accountActivity)
        {
            _accountDetailsAssertion.VerifyAccountActivityData(accountActivity, accountActivityTableHeaders, accountActivityTableValues);
            return this;
        }

        public AccountDetailsPage VerifyAccountActivityTransactionData(Table accountActivityTransactionData)
        {
            _accountDetailsAssertion.VerifyAccountActivityTransactionData
                (accountActivityTransactionData, accountActivityTransactionHeaders, accountActivityTransactionValues); 
            return this;
        }

        public AccountDetailsPage VerifyTransactioDetailsnData(Table transactionData)
        {
            _accountDetailsAssertion.VerifyTransactionData(transactionData,transactionDetailsTableHeaders, transactionDetailsTableValues);
            return this;
        }

    }
}
