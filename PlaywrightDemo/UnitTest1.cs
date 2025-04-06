using Microsoft.Playwright;

namespace PlaywrightDemo
{
    public class UnitTestPlaywright
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task Test1()
        {
            //Playwright
            using var playwright = await Playwright.CreateAsync();

            //Open the browser
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });


            //Page
            IPage page = await browser.NewPageAsync();
            await page.GotoAsync(url: "http://www.eaapp.somee.com");
            await page.ClickAsync(selector: "id=loginLink");
            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = "EaApp.jpg"
            });

            await page.FillAsync(selector: "#UserName", value: "admin");
            await page.FillAsync(selector: "#Password", value: "password");
            await page.ClickAsync(selector: "#loginIn");
            bool isExist = await page.Locator(selector: "text=Employee Details").IsVisibleAsync();
            Assert.IsTrue(isExist);

        }
    }
}