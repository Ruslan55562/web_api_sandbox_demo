using web_api_sandbox_demo_UI.POM.AccountServices;

namespace web_api_sandbox_demo_UI.StepDefinitions
{
    [Binding]
    public class AccountServicesSteps
    {
        private readonly AccountServicesPage _accountServicesPage;

        public AccountServicesSteps(AccountServicesPage accountServicesPage)
        {
            _accountServicesPage = accountServicesPage;
        }

        [When(@"I update initial balance to '([^']*)' and minumum balance to '([^']*)'")]
        public void WhenIUpdateInitialBalanceToAndMinumumBalanceTo(string initial, string minumum) =>
            _accountServicesPage.UpdateInitialAndMinimalBalance(initial, minumum);

        [When(@"I go to '([^']*)' page")]
        public void WhenIGoToPage(string pageName) =>
            _accountServicesPage.GoToAccountServicesPage(pageName);

        [When(@"I select '([^']*)' type")]
        public void WhenISelectType(string type) =>
            _accountServicesPage.SelectNewAccountType(type);

        [When(@"I open new account")]
        public void WhenIOpenNewAccount() =>
            _accountServicesPage.ClickOnOpenNewAccountButton();

        [When(@"I enter '([^']*)' transfer amount")]
        public void WhenIEnterTransferAmount(string amount) =>
            _accountServicesPage.EnterTheTransferAmmount(amount);

        [When(@"I send a tranfer from '([^']*)' to '([^']*)' account")]
        public void WhenISendATranferFromToAccount(string fromAccountId, string toAccountId) =>
            _accountServicesPage.ChooseFromAccountNumber(fromAccountId).ChooseToAccountNumber(toAccountId).
            ClickOnTransferButton();

        [When(@"I fill in bill input fields with the next data")]
        public void WhenIFillInBillInputFieldsWithTheNextData(Table billInfo) =>
            _accountServicesPage.FillInBillInputFields(billInfo);

        [When(@"I enter '([^']*)' as loan amount and '([^']*)' down payment from '([^']*)' account")]
        public void WhenIEnterAsLoanAmountAndDownPaymentFromAccount(string loanAmount, string downPayment, string accountId) =>
            _accountServicesPage.FillInRequestLoanForm(loanAmount, downPayment, accountId);

        [Then(@"I see successfull operation message with '([^']*)' text")]
        public void ThenISeeSuccessfullOperationMessageWithText(string message) =>
            _accountServicesPage.VerifySuccessfulTransferMessage(message);

        [Then(@"I see '([^']*)' title")]
        public void ThenISeeTitle(string title) =>
            _accountServicesPage.VerifyPageTitle(title);

        [Then(@"The new account number is created")]
        public void ThenTheNewAccountNumberIsCreated() =>
            _accountServicesPage.VerifyNewAccountNumberIsCreated();

        [Then(@"The Account has the following balance data:")]
        public void ThenTheAccountHasTheFollowingBalanceData(Table balanceData) =>
            _accountServicesPage.VerifyAccountTableData(balanceData);

        [Then(@"The loan response has the following data:")]
        public void ThenTheLoanRequestHasTheFollowingData(Table table) =>
            _accountServicesPage.VerifyLoanResponseData(table);
    }
}