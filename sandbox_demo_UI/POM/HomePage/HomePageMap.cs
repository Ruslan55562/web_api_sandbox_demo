using Reqnroll.BoDi;
using web_api_sandbox_demo_UI.CommonPageSpace;

namespace web_api_sandbox_demo_UI.POM.HomePage
{
    public class HomePageMap : CommonPageMap
    {
        public HomePageMap(IObjectContainer objectContainer) : base(objectContainer)
        {

        }

        public HomePageMap LogIntoApplication(string username, string password,string userNameField,string passwordField, string logInButton)
        {
            SendTextToInput(userNameField, username);
            SendTextToInput(passwordField, password);
            ClickButton(logInButton);
            return this;
        }
    }
}
