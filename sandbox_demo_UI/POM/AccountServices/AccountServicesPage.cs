using OpenQA.Selenium;
using web_api_sandbox_demo_UI.Helpers;

namespace web_api_sandbox_demo_UI.POM.AccountServices
{
    public class AccountServicesPage
    {
        private readonly AccountServicesAssertions _accountServicesAssertions;
        private readonly AccountServicesMap _accountServicesMap;
        private readonly IWebDriver _driver;
        private string defaultAccountNumber = "12345";

        public AccountServicesPage(IWebDriver driver, AccountServicesAssertions accountServicesAssertions, AccountServicesMap accountServicesMap)
        {
            _driver = driver;
            _accountServicesAssertions = accountServicesAssertions;
            _accountServicesMap = accountServicesMap;
        }

        public IWebElement InitialBalance => _driver.FindElement(By.XPath("//input[@id='initialBalance']"));
        public IWebElement MinimalBalance => _driver.FindElement(By.XPath("//input[@id='minimumBalance']"));
        public IWebElement SubmitButton => _driver.FindElement(By.XPath("//input[@value='Submit']"));
        public IWebElement NewAccountTypeDropdown => _driver.FindElement(By.XPath("//select[@id='type']"));
        public IWebElement OpenNewAccountButton => _driver.FindElement(By.XPath("//input[@value='Open New Account']"));
        public IWebElement NewAccountId => _driver.FindElement(By.XPath("//p/a[@id='newAccountId']"));
        public IWebElement TransferAmountField => _driver.FindElement(By.XPath("//input[@id='amount']"));
        public IWebElement FromAccountDropdown => _driver.FindElement(By.XPath("//select[@id='fromAccountId']"));
        public IWebElement ToAccountDropdown => _driver.FindElement(By.XPath("//select[@id='toAccountId']"));
        public IWebElement TransferButton => _driver.FindElement(By.XPath("//input[@value='Transfer']"));
        public IWebElement SendPaymentButton => _driver.FindElement(By.XPath("//input[@value='Send Payment']"));
        public IWebElement SuccessfullTransferMessage => _driver.FindElement(By.XPath("//div[@*='showResult']//p[position()=1]"));
        public IWebElement FromAccountDropdownTransferPage => _driver.FindElement(By.XPath("//select[@name='fromAccountId']"));
        public IWebElement LoanAmount => _driver.FindElement(By.XPath("//input[@id='amount']"));
        public IWebElement DownPayment => _driver.FindElement(By.XPath("//input[@id='downPayment']"));
        public IWebElement ApplyNowButton => _driver.FindElement(By.XPath("//input[@value='Apply Now']"));
        public IWebElement LoanProviderName => _driver.FindElement(By.XPath("//td[@id='loanProviderName']"));
        public IWebElement LoanResponseDate => _driver.FindElement(By.XPath("//td[@id='responseDate']"));
        public IWebElement LoanStatus => _driver.FindElement(By.XPath("//td[@id='loanStatus']"));

        public IWebElement FromAccountOptionTransferPage(string accountId) => _driver.FindElement(By.XPath($"//select[@name='fromAccountId']/option[@value='{accountId}']"));
        public IWebElement FromAccountOption(string accountId) => _driver.FindElement(By.XPath($"//select[@id='fromAccountId']/option[.='{accountId}']"));
        public IWebElement ToAccountOption(string accountId) => _driver.FindElement(By.XPath($"//select[@id='toAccountId']/option[@label='{accountId}']"));
        public IWebElement AccountOpenedTitle(string title) => _driver.FindElement(By.XPath($"//h1[.='{title}']"));
        public IWebElement AccountTypeOption(string optionName) => _driver.FindElement(By.XPath($"//select[@id='type']/option[.='{optionName}']"));
        public IWebElement BalanceInTableValue(string columnName, string value) => _driver.FindElement(By.XPath($"//th[.='{columnName}*']/ancestor::table//td[.='{value}'][not(preceding::td[.='{value}'])]"));
        public IWebElement AvailableAmountInTableValue(string columnName, string value) => _driver.FindElement(By.XPath($"(//th[.='{columnName}']/ancestor::table//td[.='{value}'])[position()=2]"));
        public IWebElement TotalInTableValue(string columnName, string value) => _driver.FindElement(By.XPath($"//b[.='{columnName}']/ancestor::tr//b[.='{value}']"));
        public IWebElement AccountServicesNavigationOption(string optionName) => _driver.FindElement(By.XPath($"//div[@id='leftPanel']/ul//a[.='{optionName}']"));
        public IWebElement BillPaymentInputFields(string ngModel) => _driver.FindElement(By.XPath($"//input[@ng-model='{ngModel}']"));

        public AccountServicesPage UpdateInitialAndMinimalBalance(string initial, string minimal)
        {
            InitialBalance.SendTextToInput(_driver, initial);
            MinimalBalance.SendTextToInput(_driver, minimal);
            PressSubmitButton();
            return this;
        }

        public AccountServicesPage GoToAccountServicesPage(string pageName)
        {
            AccountServicesNavigationOption(pageName).ClickButtonWithWait(_driver, TimeSpan.FromSeconds(0.5));
            return this;
        }

        public AccountServicesPage VerifyAccountTableData(Table balanceData)
        {
            var accountDetailsData = balanceData.Rows[0];
            var expectedHeaders = accountDetailsData.Keys.ToList();
            var expectedValues = accountDetailsData.Values.ToList();
            var actualValues = new List<string>
            {
                BalanceInTableValue(expectedHeaders[0], expectedValues[0]).Text,
                AvailableAmountInTableValue(expectedHeaders[1], expectedValues[1]).Text,
                TotalInTableValue(expectedHeaders[2], expectedValues[2]).Text
            };

            for (int i = 0; i < expectedHeaders.Count; i++)
            {
                _accountServicesAssertions.AreEqual(expectedValues[i], actualValues[i],
                    $"The actual value for '{expectedHeaders[i]}' is not the same as expected");
            }

            return this;
        }

        public AccountServicesPage SelectNewAccountType(string type)
        {
            NewAccountTypeDropdown.Click();
            AccountTypeOption(type).ClickButtonWithWait(_driver, TimeSpan.FromSeconds(1));
            return this;
        }

        public AccountServicesPage ClickOnOpenNewAccountButton()
        {
            OpenNewAccountButton.ClickButtonWithWait(_driver, TimeSpan.FromSeconds(1.5));
            return this;
        }

        public AccountServicesPage VerifyPageTitle(string title)
        {
            _accountServicesAssertions.AreEqual
                (title, AccountOpenedTitle(title).Text, "The title text is not the same as expected");
            return this;
        }

        public AccountServicesPage VerifyNewAccountNumberIsCreated()
        {
            _accountServicesAssertions.IsTrue(NewAccountId.Displayed, "The new account id is not displayed");
            return this;
        }

        public AccountServicesPage EnterTheTransferAmmount(string amount)
        {
            TransferAmountField.SendTextToInput(_driver, amount);
            return this;
        }

        public AccountServicesPage ChooseFromAccountNumber(string accountNumber)
        {
            FromAccountDropdown.ClickButtonWithWait(_driver, TimeSpan.FromSeconds(1));
            FromAccountOption(accountNumber).Click();
            return this;
        }

        public AccountServicesPage ChooseToAccountNumber(string accountNumber)
        {
            ToAccountDropdown.ClickButtonWithWait(_driver, TimeSpan.FromSeconds(1));
            ToAccountOption(accountNumber).Click();
            return this;
        }

        public AccountServicesPage FillInBillInputFields(Table billInfo)
        {
            _accountServicesMap.FillInInputRegistyFields(this, billInfo);
            FromAccountDropdownTransferPage.Click();
            FromAccountOptionTransferPage(defaultAccountNumber).ClickButtonWithWait(_driver, TimeSpan.FromSeconds(1));
            PressSendPaymentButton();
            return this;
        }

        public AccountServicesPage ClickOnTransferButton()
        {
            TransferButton.ClickButtonWithWait(_driver, TimeSpan.FromSeconds(0.5));
            return this;
        }

        public AccountServicesPage VerifySuccessfulTransferMessage(string message)
        {
            _accountServicesAssertions.AreEqual(message, SuccessfullTransferMessage.Text,
                "The message is not the same as expected one");
            return this;
        }

        public AccountServicesPage FillInRequestLoanForm(string loanAmountValue, string downPaymentValue, string accountId)
        {
            LoanAmount.SendTextToInput(_driver, loanAmountValue);
            DownPayment.SendTextToInput(_driver, downPaymentValue);
            FromAccountDropdown.ClickButtonWithWait(_driver, TimeSpan.FromSeconds(1));
            FromAccountOption(accountId).Click();
            ApplyNowButton.ClickButtonWithWait(_driver, TimeSpan.FromSeconds(1));

            return this;
        }

        public AccountServicesPage VerifyLoanResponseData(Table loanResponse)
        {
            _accountServicesAssertions.VerifyLoanResponseData(loanResponse, new Dictionary<string, IWebElement>
            {
                { "Loan Provider", LoanProviderName },
                { "Status", LoanStatus },
                { "Loan Response Date", LoanResponseDate }
            });
            return this;
        }

        private AccountServicesPage PressSendPaymentButton()
        {
            SendPaymentButton.ClickButtonWithWait(_driver, TimeSpan.FromSeconds(1));
            return this;
        }

        private AccountServicesPage PressSubmitButton()
        {
            SubmitButton.ClickButtonWithWait(_driver, TimeSpan.FromSeconds(0.5));
            return this;
        }
    }
}