using Microsoft.Playwright;
using PlaywrightDemo.Pages;

namespace PlaywrightDemo
{
    public class UnitTestPlaywright
    {
        [SetUp]
        public void Setup()
        {

        }

        //[Test]
        //public async Task Test1()
        //{
        //    //Playwright
        //    using var playwright = await Playwright.CreateAsync();

        //    //Open the browser
        //    await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        //    {
        //        Headless = false
        //    });


        //    //Page
        //    IPage page = await browser.NewPageAsync();
        //    await page.GotoAsync(url: "http://www.eaapp.somee.com");
        //    await page.ClickAsync(selector: "id=loginLink");
        //    await page.ScreenshotAsync(new PageScreenshotOptions
        //    {
        //        Path = "EaApp.jpg"
        //    });

        //    await page.FillAsync(selector: "#UserName", value: "admin");
        //    await page.FillAsync(selector: "#Password", value: "password");
        //    await page.ClickAsync(selector: "#loginIn");
        //    bool isExist = await page.Locator(selector: "text=Employee Details").IsVisibleAsync();
        //    Assert.IsTrue(isExist);

        //}
        //[Test]
        //public async Task TestWithPOM()
        //{
        //    //Playwright
        //    using var playwright = await Playwright.CreateAsync();

        //    //Open the browser
        //    await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        //    {
        //        Headless = false
        //    });


        //    //Page
        //    IPage page = await browser.NewPageAsync();
        //    await page.GotoAsync(url: "http://www.eaapp.somee.com");

        //    LoginPage loginPage = new LoginPage(page);
        //    await loginPage.ClickLogin();
        //    await loginPage.Login(username: "admin", password: "password");

        //    bool isExist = await loginPage.IsEmployeeDetailsExists();
        //    Assert.IsTrue(isExist);

        //}

        [Test]
        public async Task TestTableSortedAscending()
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
            await page.GotoAsync(url: "https://automationtesting.co.uk/tables.html");

            TablePage tablePage = new TablePage(page);
            await tablePage.SortAndCheck();
        }

        [Test]
        public async Task TestTableSortedReversed()
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
            await page.GotoAsync(url: "https://automationtesting.co.uk/tables.html");

            TablePage tablePage = new TablePage(page);

            await tablePage.SortAndCheckReverse();
        }
        [Test]
        public async Task TestTableSortedTableContent()
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
            await page.GotoAsync(url: "https://automationtesting.co.uk/tables.html");

            TablePage tablePage = new TablePage(page);

            //dblclick date header locator
            await tablePage.SortTableDescending();

            //I - w calosci tutaj
            //to read data from date column - methode 1
            List<DateTime> thirdColumnData = new List<DateTime>();

            var rows = await tablePage.ReadTableContent();

            foreach (var row in rows)
            {
                // Nth(2) == trzecia kolumna (indeksowane od 0)
                var thirdCell = row.Locator("td").Nth(2);
                var date_cell = DateTime.Parse(await thirdCell.InnerTextAsync());
                thirdColumnData.Add(date_cell);
            }
            //here if methode: return thirdColumnData - methode 1


            //checking if sorting works
            var sortedDates = await tablePage.ReadThirdColumnFromTableAsync();
            var checkingSortingDates = sortedDates.OrderByDescending(x => x.Date);
            Assert.That(sortedDates, Is.EqualTo(checkingSortingDates), "Dates are not sorted descending");

            //II - w PO

            //III - w PO
        }
    }
}