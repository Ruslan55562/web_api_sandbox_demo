using Reqnroll.BoDi;
using NUnit.Framework.Legacy;
using System.Collections;

namespace web_api_sandbox_demo_UI.CommonPageSpace
{
    public class CommonPageAssertions
    {
        private readonly CommonPageMap _commonPageMap;
        private IObjectContainer _objectContainer;
        public CommonPageAssertions(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            _commonPageMap = new CommonPageMap(_objectContainer);
        }

        public void AreEqual(object expected, object actual, string message)
        {
            ClassicAssert.AreEqual(expected, actual, message);
        }

        public void AreNotEqual(object expected, object actual, string message)
        {
            ClassicAssert.AreNotEqual(expected, actual, message);
        }

        public void IsTrue(bool condition, string message)
        {
            ClassicAssert.IsTrue(condition, message);
        }

        public void IsFalse(bool condition, string message)
        {
            ClassicAssert.IsFalse(condition, message);
        }

        public void IsEmpty(string actual, string message)
        {
            ClassicAssert.IsEmpty(actual, message);
        }

        public void Contains(object expected, ICollection actual, string message)
        {
            ClassicAssert.Contains(expected, actual, message);
        }

        public void IsNull(object actual,string message)
        {
            ClassicAssert.IsNull(actual, message);
        }

        public void IsNotNull(object actual,string message)
        {
            ClassicAssert.IsNotNull(actual, message);
        }

        public void IsElementDisplayed(string locator,string message)
        {
            ClassicAssert.IsTrue(_commonPageMap.GetElement(locator).Displayed, message);
        }
    }
}
