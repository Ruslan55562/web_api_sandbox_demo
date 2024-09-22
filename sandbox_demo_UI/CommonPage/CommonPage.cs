using System;

namespace web_api_sandbox_demo_UI.CommonPageSpace
{
    public class CommonPage
    {
        public string PageTitle = "//img[@title='ParaBank']";
        public string ButtonByText(string text) => $"//a[.='{text}']";
        public string HeaderNavigationButtons(string buttonName) => $"//div[@id='headerPanel']//li[.='{buttonName}']";
        public string FooterNavigationButtons(string buttonName) => $"//div[@id='footerPanel']//a[.='{buttonName}']";
    }
}
