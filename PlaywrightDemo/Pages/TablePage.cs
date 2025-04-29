using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightDemo.Pages
{
    public class TablePage
    {

        private IPage _page;
        public TablePage(IPage page)
        {
            _page = page;
        }

        private ILocator _dateOfBirth => _page.Locator(selector: "//table[@class='sortable']//th[text()='Date of Birth']");
        private ILocator _sortedReversed => _page.Locator(selector: "//table[@class='sortable']//th[@class=' sorttable_sorted_reverse']");
        private ILocator _sortedAscending => _page.Locator(selector: "//table[@class='sortable']//th[@class=' sorttable_sorted']");
        private ILocator _tableContent => _page.Locator(selector: "//table[@class='sortable']//tbody//tr");

        public async Task SortTableDescending() => await _dateOfBirth.DblClickAsync();
        public async Task SortTable() => await _dateOfBirth.ClickAsync();

        public async Task<IReadOnlyList<ILocator>> ReadTableContent() => await _tableContent.AllAsync();
        private async Task CheckSortedAscending() => await _sortedAscending.ClickAsync();
        private async Task CheckSortedReversed() => await _sortedReversed.ClickAsync();

        public async Task SortAndCheck()
        {
            await SortTable();
            await CheckSortedAscending();
        }
        public async Task SortAndCheckReverse()
        {
            await SortTable();
            await SortTable();
            await CheckSortedReversed();
        }

        public async Task SortDescendingAndCheckContent()
        {
            await SortTable();
            await SortTable();
            var sortedDates = await ReadThirdColumnFromTableAsync();
            var checkingSortingDates = sortedDates.OrderByDescending(x => x.Date);
            Assert.That(sortedDates, Is.EqualTo(checkingSortingDates), "Dates are not sorted descending");
            //await ReadTableContent();

        }


        public async Task ReadFirstTableContent()
        {
            // Pobierz pierwszy wiersz
            var firstRow = _tableContent.First;
            // Pobierz wszystkie komórki (td) z tego wiersza
            var values = await firstRow.Locator("td").AllInnerTextsAsync();
            //return values;
            //Console.WriteLine(string.Join(" | ", rows));


            //var rows = await _tableContent.AllAsync();

            //foreach (var row in rows)
            //{
            //    var cells = await row.Locator("td").AllInnerTextsAsync();
            //    Console.WriteLine(string.Join(" | ", cells));

            //}
        }


        public async Task<List<DateTime>> ReadThirdColumnFromTableAsync()
        {
            List<DateTime> thirdColumnData = new List<DateTime>();

            var rows = await _tableContent.AllAsync();

            foreach (var row in rows)
            {
                // Nth(2) == trzecia kolumna (indeksowane od 0)
                var thirdCell = row.Locator("td").Nth(2);
                var date_cell = DateTime.Parse(await thirdCell.InnerTextAsync());
                thirdColumnData.Add(date_cell);
            }
            return thirdColumnData;
        }

        //II - calosc
        //model danych do uzupelnien
        public class PersonRow
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string EmailAddress { get; set; }
            public string Residency { get; set; }
            public string Occupation { get; set; }

            public override bool Equals(object obj)
            {
                if (obj is not PersonRow other)
                    return false;

                return FirstName == other.FirstName &&
                       LastName == other.LastName &&
                       DateOfBirth == other.DateOfBirth &&
                       EmailAddress == other.EmailAddress &&
                       Residency == other.Residency &&
                       Occupation == other.Occupation;
            }

            public override int GetHashCode() => base.GetHashCode();
        }

        //zebranie danych z pierwszego wiersza
        public async Task<PersonRow> GetFirstRowAsPersonAsync()
        {
            var firstRow = _tableContent.First;
            var cells = await firstRow.Locator("td").AllInnerTextsAsync();

            return new PersonRow
            {
                FirstName = cells[0],
                LastName = cells[1],
                DateOfBirth = DateTime.Parse(cells[2]),
                EmailAddress = cells[3],
                Residency = cells[4],
                Occupation = cells[5]
            };
        }
        //porownanie zebranych danych z oczekiwanym modelem
        public async Task<bool> IsFirstRowEqualToExpectedAsync(PersonRow expected)
        {
            var actual = await GetFirstRowAsPersonAsync();
            //u mnie expected to beda te ktorymi uzupelnilam formularz
            return actual.Equals(expected);
        }

        //Przyklad zastosowania:
        //var expected = new PersonRow
        //{
        //    FirstName = "Luca",
        //    LastName = "Brassie",
        //    DateOfBirth = new DateTime(1966, 2, 3),
        //    EmailAddress = "Lucasb12@madeupemailacc.com",
        //    Residency = "Italy",
        //    Occupation = "IT Professional"
        //};

        //a to do testu:
        //bool result = await IsFirstRowEqualToExpectedAsync(expected);

        //Console.WriteLine(result? "✅ Dane się zgadzają!" : "❌ Dane NIE pasują.");




        //III
        public async Task IfActuallyCreated()
        {
            // Pobierz lokator pierwszego wiersza
            var firstRow = _tableContent.First;
            // Pobierz tekst z 5. kolumny (index 4, bo liczymy od zera)
            var cellText = await firstRow.Locator("td").Nth(4).InnerTextAsync();

            if (cellText.Trim().Equals("Aktor", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("✅ W kolumnie 5 znajduje się 'Aktor'");
            }
            else
            {
                Console.WriteLine("❌ W kolumnie 5 NIE znajduje się 'Aktor'");
            }
        }
    }
}
