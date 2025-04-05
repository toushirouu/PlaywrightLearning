## CLI usage

Playwright.ps1 file path
- Go to project_name\bin\Debug\net8.0\

### codegen
To generate code using CLI execute in powershell in project_name\bin\Debug\net8.0\
- .\playwright.ps1 codegen PAGE_URL

example:

	.\playwright.ps1 codegen http://eaapp.somee.com

### screenshot
To capture a screenshot of specific page
- .\playwright.ps1 screenshot --full-page PAGE_URL OUTPUT_FILE.png

example:

	.\playwright.ps1 screenshot --full-page http://eaapp.somee.com test.png


### install 
Ensure browsers necessary for current version of Playwright are installed
- .\playwright.ps1 install BROWSER_NAME

example:

	.\playwright.ps1 install chrome
