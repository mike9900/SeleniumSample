using OpenQA.Selenium;

namespace SeleniumExample
{
    public class AddRemoveElementsPage
    {
        private IWebDriver _driver;
        public const string URL = "https://the-internet.herokuapp.com/add_remove_elements/";

        public AddRemoveElementsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement AddElementButton => _driver.WaitFindElement(By.XPath("//button[contains(text(),'Add Element')]"));
    }
}
