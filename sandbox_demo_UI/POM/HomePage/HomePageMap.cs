using Reqnroll.BoDi;
using web_api_sandbox_demo_UI.CommonPageSpace;
using web_api_sandbox_demo_UI.Helpers;
using web_api_sandbox_demo_UI_Drivers;

namespace web_api_sandbox_demo_UI.POM.HomePage
{
    public class HomePageMap : CommonPageMap
    {
        public HomePageMap(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        public HomePageMap LogIntoApplication(HomePage homePage, string username, string password)
        {
            homePage.GetUserNameField().SendTextToInput(DriverManager.GetDriverInstance(), username);
            homePage.GetPasswordField().SendTextToInput(DriverManager.GetDriverInstance(), password);
            homePage.GetLogInButton().Click();
            return this;
        }
    }
}