using Reqnroll.BoDi;
using web_api_sandbox_demo_UI.CommonPageSpace;

namespace web_api_sandbox_demo_UI.POM.HomePage
{
    public class HomePageMap : CommonPageMap
    {
        public HomePageMap(IObjectContainer objectContainer) : base(objectContainer)
        {
        }

        public HomePageMap LogIntoApplication(HomePage homePage, string username, string password)
        {
            SendTextToInput(homePage.GetUserNameField(), username);
            homePage.GetPasswordField().SendTextToInput(_driver, password);
            homePage.GetLogInButton().Click();
            return this;
        }
    }
}