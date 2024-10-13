using System;

namespace sandbox_demo_UI.Map
{
    public class SearchTransactionType
    {
        public static Dictionary<string, string> searchTermMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
             { "date", "//button[contains(@ng-click, 'DATE') and not(contains(@ng-click, 'DATE_RANGE'))]" },
             { "date range", "//button[contains(@ng-click, 'DATE_RANGE')]" },
             { "id", "//button[contains(@ng-click, 'ID')]" },
             { "amount", "//button[contains(@ng-click, 'AMOUNT')]" }
        };
    }
}
