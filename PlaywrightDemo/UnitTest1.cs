using System.Runtime.InteropServices;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightDemo
{
    public class NunitPlaywright : PageTest
    {
        [SetUp]
        public async Task Setup()
        {
            await Page.GotoAsync(url: "http://www.eaapp.somee.com");
        }

        [Test]
        public async Task Test1()
        {

           
            await Page.ClickAsync(selector:"id=loginLink");
            await Page.FillAsync(selector: "#UserName", value: "admin");
            await Page.FillAsync(selector: "#Password", value: "password");
            await Page.ClickAsync(selector: "#loginIn");
            await Expect(Page.Locator(selector: "text=Employee Details")).ToBeVisibleAsync();

            await Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = "EaApp.jpg"
            });

        }
    }
}