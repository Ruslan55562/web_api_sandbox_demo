using System;

namespace web_api_sandbox_demo_UI.POM.AccountServices
{
    public class AccountServicesPage
    {
        private readonly AccountServicesAssertions _accountServicesAssertions;
        private readonly AccountServicesMap _accountServicesMap;
        private string defaultAccountNumber = "12345";

        private string initialBalance = "//input[@id='initialBalance']";
        private string minimalBalance = "//input[@id='minimumBalance']";
        private string submitButton = "//input[@value='Submit']";

        private string newAccountTypeDropdown = "//select[@id='type']";
        private string openNewAccountButton = "//input[@value='Open New Account']";
        private string newAccountId = "//p/a[@id='newAccountId']";

        private string transferAmountField = "//input[@id='amount']";
        private string fromAccountDropdown = "//select[@id='fromAccountId']";
        private string toAccountDropdown = "//select[@id='toAccountId']";
        private string transferButton = "//input[@value='Transfer']";

        private string sendPaymentButton = "//input[@value='Send Payment']";
        private string successfullTransferMessage = "//div[@*='showResult']//p[position()=1]";
        private string fromAccountDropdownTransferPage = "//select[@name='fromAccountId']";

        private string loanAmount = "//input[@id='amount']";
        private string downPayment = "//input[@id='downPayment']";
        private string applyNowButton = "//input[@value='Apply Now']";

        private string loanProviderName = "//td[@id='loanProviderName']";
        private string loanResponseDate = "//td[@id='responseDate']";
        private string loanStatus = "//td[@id='loanStatus']";


        private string FromAccountOptionTransferPage(string accountId) => $"//select[@name='fromAccountId']/option[@value='{accountId}']";
        private string FromAccountOption(string accountId) => $"//select[@id='fromAccountId']/option[.='{accountId}']";
        private string ToAccountOption(string accountId) => $"//select[@id='toAccountId']/option[@label='{accountId}']";
        private string accountOpenedTitle(string title) => $"//h1[.='{title}']";
        private string AccountTypeOption(string optionName) => $"//select[@id='type']/option[.='{optionName}']";
        private string BalanceInTableValue(string columnName, string value) =>
            $"//th[.='{columnName}*']/ancestor::table//td[.='{value}'][not(preceding::td[.='{value}'])]";
        private string AvailableAmountInTableValue(string columnName, string value) =>
            $"(//th[.='{columnName}']/ancestor::table//td[.='{value}'])[position()=2]";
        private string TotalInTableValue(string columnName, string value) =>
            $"//b[.='{columnName}']/ancestor::tr//b[.='{value}']";
        private string AccountServicesNavigationOption(string optionName) => $"//div[@id='leftPanel']/ul//a[.='{optionName}']";
        public static string BillPaymentInputFields(string ngModel) => $"//input[@ng-model='{ngModel}']";


        public AccountServicesPage(AccountServicesAssertions accountServicesAssertions, AccountServicesMap accountServicesMap)
        {
            _accountServicesAssertions = accountServicesAssertions;
            _accountServicesMap = accountServicesMap;
        }

        public AccountServicesPage UpdateInitialAndMinimalBalance(string initial,string minimal)
        {
            _accountServicesMap.SendTextToInput(initialBalance, initial);
            _accountServicesMap.SendTextToInput(minimalBalance, minimal);
            PressSubmitButton();
            return this;
        }

        public AccountServicesPage GoToAccountServicesPage(string pageName)
        {
            _accountServicesMap.ClickButton(AccountServicesNavigationOption(pageName));
            return this;
        }

        public AccountServicesPage VerifyAccountTableData(Table balanceData)
        {
            var accountDetailsData = balanceData.Rows[0];
            var expectedHeaders = accountDetailsData.Keys.ToList();
            var expectedValues = accountDetailsData.Values.ToList();
            var actualValues = new List<string>
            {
            _accountServicesMap.GetElement(BalanceInTableValue(expectedHeaders[0], expectedValues[0])).Text,
            _accountServicesMap.GetElement(AvailableAmountInTableValue(expectedHeaders[1], expectedValues[1])).Text,
            _accountServicesMap.GetElement(TotalInTableValue(expectedHeaders[2], expectedValues[2])).Text
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
            _accountServicesMap.ClickButton(newAccountTypeDropdown);
            _accountServicesMap.ClickButtonWithWait(AccountTypeOption(type), TimeSpan.FromSeconds(1));
            return this;
        }

        public AccountServicesPage ClickOnOpenNewAccountButton()
        {
            _accountServicesMap.ClickButtonWithWait(openNewAccountButton,TimeSpan.FromSeconds(1.5));
            return this;
        }

        public AccountServicesPage VerifyPageTitle(string title)
        {
            _accountServicesAssertions.AreEqual
                (title, _accountServicesMap.GetElement(accountOpenedTitle(title)).Text, "The title text is not the same as expected");
            return this;
        }

        public AccountServicesPage VerifyNewAccountNumberIsCreated()
        {
            _accountServicesAssertions.IsElementDisplayed(newAccountId, "The new account id is not displayed");
            return this;
        }

        public AccountServicesPage EnterTheTransferAmmount(string amount)
        {
            _accountServicesMap.SendTextToInput(transferAmountField, amount);
            return this;
        }

        public AccountServicesPage ChooseFromAccountNumber(string accountNumber)
        {
            _accountServicesMap.ClickButtonWithWait(fromAccountDropdown, TimeSpan.FromSeconds(1));
            _accountServicesMap.ClickButton(FromAccountOption(accountNumber));
            return this;
        }

        public AccountServicesPage ChooseToAccountNumber(string accountNumber)
        {
            _accountServicesMap.ClickButtonWithWait(toAccountDropdown, TimeSpan.FromSeconds(1));
            _accountServicesMap.ClickButton(ToAccountOption(accountNumber));

            return this;
        }

        public AccountServicesPage FillInBillInputFields(Table billInfo)
        {
            _accountServicesMap.FillInInputRegistyFields(billInfo);
            _accountServicesMap.ClickButton(fromAccountDropdownTransferPage);
            _accountServicesMap.ClickButtonWithWait(FromAccountOptionTransferPage(defaultAccountNumber), TimeSpan.FromSeconds(1));
            PressSendPaymentButton();
            return this;
        }

        public AccountServicesPage ClickOnTransferButton()
        {
            _accountServicesMap.ClickButtonWithWait(transferButton,TimeSpan.FromSeconds(0.5));
            return this;
        }

        public AccountServicesPage VerifySuccessfulTransferMessage(string message)
        {
            _accountServicesAssertions.AreEqual(message, _accountServicesMap.GetElement(successfullTransferMessage).Text, 
                "The message is not the same as expected one");
            return this;
        }

        public AccountServicesPage FillInRequestLoanForm(string loanAmountValue, string downPaymentValue, string accountId)
        {
            _accountServicesMap.SendTextToInput(loanAmount, loanAmountValue);
            _accountServicesMap.SendTextToInput(downPayment, downPaymentValue);
            _accountServicesMap.ClickButtonWithWait(fromAccountDropdown, TimeSpan.FromSeconds(1));
            _accountServicesMap.ClickButton(FromAccountOption(accountId));
            _accountServicesMap.ClickButtonWithWait(applyNowButton,TimeSpan.FromSeconds(1));

            return this;
        }

        public AccountServicesPage VerifyLoanResponseData(Table loanResponse)
        {
            _accountServicesAssertions.VerifyLoanResponseData(loanResponse, new Dictionary<string, string>
                                                                    {{ "Loan Provider", loanProviderName },
                                                                     { "Status", loanStatus },
                                                                     { "Loan Response Date", loanResponseDate }});
            return this;
        }

        private AccountServicesPage PressSendPaymentButton()
        {
            _accountServicesMap.ClickButtonWithWait(sendPaymentButton,TimeSpan.FromSeconds(1));
            return this;
        }

        private AccountServicesPage PressSubmitButton()
        {
            _accountServicesMap.ClickButton(submitButton);
            return this;
        }

    }
}