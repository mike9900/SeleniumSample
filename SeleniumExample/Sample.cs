using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumExample
{
	public class Sample
	{
		IWebDriver driver;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			ChromeOptions options = new();
			options.AddArguments("--headless");
			driver = new ChromeDriver(options);
		}

		[SetUp]
		public void SetUp()
		{
			driver.Navigate().GoToUrl(AddRemoveElementsPage.URL);
			driver.WaitUntil(d => d.PageLoaded());
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			driver.Quit();
		}

		[Test, Author("Mike Anderson")]
		[TestCase(5)]
		[TestCase(20)]
		public void SampleTest(int elementsToCreate)
		{
			AddRemoveElementsPage addRemoveElementsPage = new(driver);

			Assert.NotNull(addRemoveElementsPage.AddElementButton, "Unable to find Add Element Button");

			IWebElement parentElement = driver.WaitFindElement(By.Id("elements"));

			Assert.Zero(
				parentElement.FindElements(By.ClassName("added-manually")).Count,
				"Elements existed before clicking the Add Element button");

			for (int addedElements = 0; addedElements < elementsToCreate; addedElements++)
			{
				addRemoveElementsPage.AddElementButton.Click();

				Assert.True(
					driver.WaitUntil(d => 
						parentElement.FindElements(By.ClassName("added-manually")).Count == addedElements + 1),
					$"Failed to find a new element after clicking the Add Element button {addedElements} times");
			}

			Assert.AreEqual(
				parentElement.FindElements(By.ClassName("added-manually")).Count,
				elementsToCreate,
				"Elements added");
		}
	}
}
