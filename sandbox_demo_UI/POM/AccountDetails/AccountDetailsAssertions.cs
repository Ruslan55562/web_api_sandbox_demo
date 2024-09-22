using BoDi;
using web_api_sandbox_demo_UI.CommonPageSpace;

namespace web_api_sandbox_demo_UI.POM.AccountDetails
{
    public class AccountDetailsAssertions : CommonPageAssertions
    {
        private readonly AccountDetailsMap _accountDetailsMap;
        public AccountDetailsAssertions(IObjectContainer objectContainer,AccountDetailsMap accountDetailsMap) : base(objectContainer)
        {
            _accountDetailsMap = accountDetailsMap;
        }

        public AccountDetailsAssertions VerifyAccountDetailsData(Table accountDetails, string accountDetailsTableHeaders, string accountDetailsTableValues)
        {
            var accountDetailsData = accountDetails.Rows[0];

            var expectedHeaders = accountDetailsData.Keys.ToList();
            var expectedValues = accountDetailsData.Values.ToList();

            var actualAccountDetailsHeaders = _accountDetailsMap.GetElements(accountDetailsTableHeaders);
            var actualAccountDetailsValues = _accountDetailsMap.GetElements(accountDetailsTableValues);

            for (int i = 0; i < expectedHeaders.Count; i++)
            {
                var actualHeader = actualAccountDetailsHeaders[i].Text.TrimEnd(':');
                var actualValue = actualAccountDetailsValues[i].Text;

                AreEqual(expectedHeaders[i], actualHeader, 
                    $"Expected header {expectedHeaders[i]} but found {actualHeader}");
                AreEqual(expectedValues[i], actualValue, 
                    $"Expected value '{expectedValues[i]}' but found '{actualValue}'");
            }

            return this;
        }

        public AccountDetailsAssertions VerifyAccountActivityData(Table accountActivity, string accountActivityTableHeaders, string accountActivityTableValues)
        {
            var accountActivityData = accountActivity.Rows[0];
            var actualAccountActivityHeaders = _accountDetailsMap.GetElements(accountActivityTableHeaders);
            var actualAccountActivityValues = _accountDetailsMap.GetElements(accountActivityTableValues);

            var expectedHeaders = accountActivityData.Keys.ToList();
            var expectedValues = accountActivityData.Values.ToList();
            for (int i = 0; i < expectedHeaders.Count; i++)
            {
                var actualHeader = actualAccountActivityHeaders[i].Text.TrimEnd(':');
                var actualValue = actualAccountActivityValues[i].Text;

                AreEqual(expectedHeaders[i], actualHeader, 
                    $"The actual header {actualHeader} is not the same as expected {expectedHeaders[i]}");
                AreEqual(expectedValues[i], actualValue, 
                    $"The actual value {actualValue} is not the same as expected {expectedValues[i]}");
            }

            return this;
        }

        public AccountDetailsAssertions VerifyAccountActivityTransactionData
            (Table accountActivityTransactionData,string accountActivityTransactionHeaders, string accountActivityTransactionValues)
        {
            var expectedTransactionData = accountActivityTransactionData.Rows[0];
            var expectedHeaders = expectedTransactionData.Keys.ToList();
            var expectedValues = expectedTransactionData.Values.ToList();

            var actualTransactionTableHeaders = _accountDetailsMap.GetElements(accountActivityTransactionHeaders);
            var actualTransactionTableValues = _accountDetailsMap.GetElements(accountActivityTransactionValues);

            for (int i = 0; i < expectedHeaders.Count; i++)
            {
                var actualHeader = actualTransactionTableHeaders[i].Text;
                var actualValue = actualTransactionTableValues[i].Text;

                AreEqual(expectedHeaders[i], actualHeader,
                    $"Expected header {expectedHeaders[i]} but found {actualHeader}");

                AreEqual(expectedValues[i], actualValue,
                    $"Expected value '{expectedValues[i]} but found {actualValue}");
            }
            return this;
        }

        public AccountDetailsAssertions VerifyTransactionData(Table transactionData, string transactionTableHeaders, string transactionTableValues)
        {
            var expectedTransactionData = transactionData.Rows[0];
            var expectedHeaders = expectedTransactionData.Keys.ToList();
            var expectedValues = expectedTransactionData.Values.ToList();

            var actualTransactionTableHeaders = _accountDetailsMap.GetElements(transactionTableHeaders);
            var actualTransactionTableValues = _accountDetailsMap.GetElements(transactionTableValues);

            for (int i = 0; i < expectedHeaders.Count; i++)
            {
                var actualHeader = actualTransactionTableHeaders[i].Text.TrimEnd(':');
                var actualValue = actualTransactionTableValues[i].Text;

                AreEqual(expectedHeaders[i], actualHeader,
                    $"Expected header {expectedHeaders[i]} but found {actualHeader}");

                AreEqual(expectedValues[i], actualValue,
                    $"Expected value '{expectedValues[i]} but found {actualValue}");
            }

            return this;
        }
    }
}