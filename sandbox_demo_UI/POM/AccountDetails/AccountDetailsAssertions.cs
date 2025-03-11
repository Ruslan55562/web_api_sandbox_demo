using web_api_sandbox_demo_UI.CommonPageSpace;
using Reqnroll.BoDi;

namespace web_api_sandbox_demo_UI.POM.AccountDetails
{
    public class AccountDetailsAssertions : CommonPageAssertions
    {
        private readonly AccountDetailsMap _accountDetailsMap;

        public AccountDetailsAssertions(IObjectContainer objectContainer, AccountDetailsMap accountDetailsMap) : base(objectContainer)
        {
            _accountDetailsMap = accountDetailsMap;
        }

        public AccountDetailsAssertions VerifyAccountDetailsData(Table accountDetails, AccountDetailsPage accountDetailsPage)
        {
            var accountDetailsData = accountDetails.Rows[0];
            var expectedHeaders = accountDetailsData.Keys.ToList();
            var expectedValues = accountDetailsData.Values.ToList();

            var actualHeaders = accountDetailsPage.AccountDetailsTableHeaders;
            var actualValues = accountDetailsPage.AccountDetailsTableValues;

            for (int i = 0; i < expectedHeaders.Count; i++)
            {
                var actualHeader = actualHeaders[i].Text.TrimEnd(':');
                var actualValue = actualValues[i].Text;

                AreEqual(expectedHeaders[i], actualHeader,
                    $"Expected header {expectedHeaders[i]} but found {actualHeader}");
                AreEqual(expectedValues[i], actualValue,
                    $"Expected value '{expectedValues[i]}' but found '{actualValue}'");
            }

            return this;
        }

        public AccountDetailsAssertions VerifyAccountActivityData(Table accountActivity, AccountDetailsPage accountDetailsPage)
        {
            var accountActivityData = accountActivity.Rows[0];
            var expectedHeaders = accountActivityData.Keys.ToList();
            var expectedValues = accountActivityData.Values.ToList();

            var actualHeaders = accountDetailsPage.AccountActivityTableHeaders;
            var actualValues = accountDetailsPage.AccountActivityTableValues;

            for (int i = 0; i < expectedHeaders.Count; i++)
            {
                var actualHeader = actualHeaders[i].Text.TrimEnd(':');
                var actualValue = actualValues[i].Text;

                AreEqual(expectedHeaders[i], actualHeader,
                    $"The actual header {actualHeader} is not the same as expected {expectedHeaders[i]}");
                AreEqual(expectedValues[i], actualValue,
                    $"The actual value {actualValue} is not the same as expected {expectedValues[i]}");
            }

            return this;
        }

        public AccountDetailsAssertions VerifyAccountActivityTransactionData(Table accountActivityTransactionData, AccountDetailsPage accountDetailsPage)
        {
            var expectedTransactionData = accountActivityTransactionData.Rows[0];
            var expectedHeaders = expectedTransactionData.Keys.ToList();
            var expectedValues = expectedTransactionData.Values.ToList();

            var actualHeaders = accountDetailsPage.AccountActivityTransactionHeaders;
            var actualValues = accountDetailsPage.AccountActivityTransactionValues;

            for (int i = 0; i < expectedHeaders.Count; i++)
            {
                var actualHeader = actualHeaders[i].Text;
                var actualValue = actualValues[i].Text;

                AreEqual(expectedHeaders[i], actualHeader,
                    $"Expected header {expectedHeaders[i]} but found {actualHeader}");

                AreEqual(expectedValues[i], actualValue,
                    $"Expected value '{expectedValues[i]}' but found {actualValue}");
            }
            return this;
        }

        public AccountDetailsAssertions VerifyTransactionData(Table transactionData, AccountDetailsPage accountDetailsPage)
        {
            var expectedTransactionData = transactionData.Rows[0];
            var expectedHeaders = expectedTransactionData.Keys.ToList();
            var expectedValues = expectedTransactionData.Values.ToList();

            var actualHeaders = accountDetailsPage.TransactionDetailsTableHeaders;
            var actualValues = accountDetailsPage.TransactionDetailsTableValues;

            for (int i = 0; i < expectedHeaders.Count; i++)
            {
                var actualHeader = actualHeaders[i].Text.TrimEnd(':');
                var actualValue = actualValues[i].Text;

                AreEqual(expectedHeaders[i], actualHeader,
                    $"Expected header {expectedHeaders[i]} but found {actualHeader}");

                AreEqual(expectedValues[i], actualValue,
                    $"Expected value '{expectedValues[i]}' but found {actualValue}");
            }

            return this;
        }
    }
}