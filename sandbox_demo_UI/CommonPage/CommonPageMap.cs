using Reqnroll.BoDi;
using OpenQA.Selenium;
using web_api_sandbox_demo_UI_Drivers;

namespace web_api_sandbox_demo_UI.CommonPageSpace
{
    public class CommonPageMap
    {
        private readonly IObjectContainer _objectContainer;
        private IWebDriver _driver;
        private readonly DriverManager _driverManager;
        public CommonPageMap(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            _driverManager = new DriverManager(_objectContainer);
            _driver = _driverManager.GetDriver();
        }
    }
}
