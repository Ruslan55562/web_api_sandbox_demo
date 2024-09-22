using web_api_sandbox_demo_UI.CommonPageSpace;

namespace web_api_sandbox_demo_UI.POM.RegisterPage
{
    public class RegisterPage : CommonPage
    {
        private readonly RegisterPageAssertions _registerPageAssertions;
        private readonly RegisterPageMap _registerPageMap;

        private string registerFormButton = "//input[@value='Register']";

        private string NavPanelWelcomeMessage(string message) => $"//div[@id='leftPanel']/p[.='{message}']";
        private string HeaderWelcomeMessage(string welcomeMessage) => $"//h1[.='{welcomeMessage}']";
        private string ErrorMessageText(string message) => $"//span[contains(.,'{message}')]";
        private string ButtonsUnderAccountServicesPanel(string optionName) => $"//div[@id='leftPanel']//a[.='{optionName}']";
        public static string InputFields(string id) => $"//input[@id='{id}']";


        public RegisterPage(RegisterPageAssertions registerPageAssertions, RegisterPageMap registerPageMap)
        {
            _registerPageAssertions = registerPageAssertions;
            _registerPageMap = registerPageMap;
        }

        public RegisterPage ClickOnRegisterButton(string text)
        {
            _registerPageMap.ClickButton(ButtonByText(text));
            return this;
        }

        public RegisterPage ClickOnLogOutButton(string optionName)
        {
            _registerPageMap.ClickButtonWithWait(ButtonsUnderAccountServicesPanel(optionName), TimeSpan.FromSeconds(0.5));
            return this;
        }

        public RegisterPage FillInInputRegisterData(Table userData)
        {
            _registerPageMap.FillInInputRegistyFields(userData);
            return this;
        }

        public RegisterPage SendRegisterForm()
        {
            _registerPageMap.ClickButtonWithWait(registerFormButton,TimeSpan.FromSeconds(1));
            return this;
        }

        public RegisterPage VerifyWelcomeHeaderMessageIsDisplayed(string welcomeMessage)
        {
            _registerPageAssertions.IsElementDisplayed(HeaderWelcomeMessage(welcomeMessage), 
                "The element was not displayed on the page");
            return this;
        }

        public RegisterPage VerifyNavigationPanelMessageIsDisplayed(string message)
        {
            _registerPageAssertions.IsElementDisplayed(NavPanelWelcomeMessage(message), 
                "The element was not displayed on the page");
            return this;
        }

        public RegisterPage VerifyFormErrorMessage(string message)
        {
            var actualMessage = _registerPageMap.GetElement(ErrorMessageText(message)).Text.TrimEnd('.');

            _registerPageAssertions.AreEqual(message, actualMessage, "The error message is not equal to expected one");
            return this;
        }
    }
}