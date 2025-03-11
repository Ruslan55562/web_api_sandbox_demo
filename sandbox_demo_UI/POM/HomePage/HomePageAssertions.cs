using Reqnroll.BoDi;
using web_api_sandbox_demo_UI.CommonPageSpace;

namespace web_api_sandbox_demo_UI.POM.HomePage
{
    public class HomePageAssertions : CommonPageAssertions
    {
        private readonly HomePageMap _homePageMap;
        public HomePageAssertions(HomePageMap homePageMap, IObjectContainer objectContainer) :  base(objectContainer)
        {
            _homePageMap = homePageMap; 
        }


        public HomePageAssertions VerifyRightPanelListElementsAreDisplayed(List<string> elements,Func<string,string> RightPanelListElements)
        {
            foreach (var item in elements)
            {
                IsElementDisplayed(RightPanelListElements(item), $"The list element with name {item} is not displayed");
            }
            return this;
        }

        public HomePageAssertions VerifySectionItemsUnderService(List<string> sectionItems, string sectionName, Func<string, string,string> SectionItemsUnderService)
        {
            foreach (var item in sectionItems)
            {
               Contains(_homePageMap.GetElement(SectionItemsUnderService(sectionName, item)).Text,
                    sectionItems, $"The page doesn't contain {item} section item");
            }
            return this;
        }

        public HomePageAssertions VerifyNewsSectionDateTime(string expectedDate, string actualDate)
        {
            AreEqual(expectedDate, actualDate, "The actual date is not equal to expected");
            return this;
        }

        public HomePageAssertions VerifyNewsTitleIsAboveBackground(string title, Func<string, string> RightPanelNewsTitle)
        {
            string actualBackground = _homePageMap.GetElement(RightPanelNewsTitle(title)).GetCssValue("background");
            string actualZIndex = _homePageMap.GetElement(RightPanelNewsTitle(title)).GetCssValue("z-index");
            IsTrue(actualZIndex == "auto" || string.IsNullOrEmpty(actualZIndex),
                         "The news section title has a specified z-index which is unexpected");
            IsTrue(actualBackground.Contains("images/icon4.jpg"), "The background image was not found");
            return this;
        }
    }
}
