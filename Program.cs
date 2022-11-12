using Microsoft.Playwright;

var token = "Token";
using var playwright = await Playwright.CreateAsync();
await using var browser = await playwright.Chromium.LaunchAsync();
var page = await browser.NewPageAsync();
await page.GotoAsync("https://playwright.dev/dotnet");

var a = await page.EvaluateAsync("localStorage.setItem('accept-contact-terms','true');");

await page.Context.AddCookiesAsync(new List<Microsoft.Playwright.Cookie> {
    new Microsoft.Playwright.Cookie
    {
        Name= "token",
        Value= token,
        Domain = ".divar.ir",
        Path="/",
    }
});

var getContactElement = page.Locator(".post-actions__get-contact");

await getContactElement.ClickAsync();

await page.ScreenshotAsync(new()
{
    Path = "c:\\screenshot.png"
});