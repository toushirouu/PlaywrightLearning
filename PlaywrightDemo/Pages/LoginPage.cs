using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightDemo.Pages
{
    public class LoginPage
    {

        private IPage _page;
        public LoginPage(IPage page)
        {
            _page = page;
        }

        private ILocator _linkLogin => _page.Locator(selector: "#loginLink", new PageLocatorOptions { HasTextString = "Login" });
        private ILocator _textUserName => _page.Locator(selector: "#UserName");
        private ILocator _textPassword => _page.Locator(selector: "#Password");
        private ILocator _btn_Login => _page.Locator(selector: "#loginIn", new PageLocatorOptions { HasTextString = "Log in" });
        private ILocator _linkEmployeeDetails => _page.Locator(selector: "text=Employee Details");


        public async Task ClickLogin() => await _linkLogin.ClickAsync();

        public async Task Login(string username, string password)
        {
            await _textUserName.FillAsync(username);
            await _textPassword.FillAsync(password);  
            await _btn_Login.ClickAsync();
        }

        public async Task<bool> IsEmployeeDetailsExists() => await _linkEmployeeDetails.IsVisibleAsync();
    }
}
