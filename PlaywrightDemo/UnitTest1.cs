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

            var linkLogin = Page.Locator(selector: "#loginLink");
            await linkLogin.ClickAsync();
            await Page.FillAsync(selector: "#UserName", value: "admin");
            await Page.FillAsync(selector: "#Password", value: "password");

            var btnLogin = Page.Locator(selector: "#loginIn", new PageLocatorOptions { HasTextString = "Log in" });
            await btnLogin.ClickAsync();

            await Expect(Page.Locator(selector: "text=Employee Details")).ToBeVisibleAsync();

            await Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = "EaApp.jpg"
            });

        }
    }
}