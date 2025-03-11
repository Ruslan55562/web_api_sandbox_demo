using Reqnroll.BoDi;
using web_api_sandbox_demo_UI.CommonPageSpace;


namespace web_api_sandbox_demo_UI.POM.RegisterPage
{
    public class RegisterPageAssertions :CommonPageAssertions
    {
        private readonly RegisterPageMap _registerPageMap;
        public RegisterPageAssertions(RegisterPageMap registerPageMap, IObjectContainer objectContainer) : base(objectContainer)
        {
            _registerPageMap = registerPageMap;
        }
    }
}
