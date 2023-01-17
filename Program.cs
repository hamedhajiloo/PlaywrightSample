using Microsoft.Playwright;
using System.Reflection;

var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyIjoiMDkxOTk3MTcyNzciLCJpc3MiOiJhdXRoIiwidmVyaWZpZWRfdGltZSI6MTY3Mzc5NjEwNCwiaWF0IjoxNjczOTc3MTg2LCJleHAiOjE2NzUyNzMxODYsInVzZXItdHlwZSI6InBlcnNvbmFsIiwidXNlci10eXBlLWZhIjoiXHUwNjdlXHUwNjQ2XHUwNjQ0IFx1MDYzNFx1MDYyZVx1MDYzNVx1MDZjYyIsInNpZCI6Ijc3OTE4ZmEyLTUwMDItNGRmZi1iZDNkLWZiNjlmMmU5M2UwMyJ9.Y71tWBnFRTzYd2alp0-4ychfBqlqfXP_eAQfgtshxRA";
using var playwright = await Playwright.CreateAsync();
await using var browser = await playwright.Chromium.LaunchAsync();
var page = await browser.NewPageAsync();
await page.GotoAsync("https://divar.ir/v/%D9%BE%D8%B1%D9%88%DA%98%D9%87-%D8%B3%D9%BE%DA%A9%D9%88-%D8%AA%D8%B9%D8%A7%D9%88%D9%86%DB%8C-%D9%85%D8%B1%D9%88%D8%A7%D8%B1%DB%8C%D8%AF-%D8%B4%D8%B1%D9%82-%D9%81%D8%A7%D8%AE%D8%B1_%D9%BE%DB%8C%D8%B4-%D9%81%D8%B1%D9%88%D8%B4_%D8%AA%D9%87%D8%B1%D8%A7%D9%86_%D9%88%D8%B1%D8%AF%D8%A2%D9%88%D8%B1%D8%AF_%D8%AF%DB%8C%D9%88%D8%A7%D8%B1/wYhuXDiX");

var a = await page.EvaluateAsync("localStorage.setItem('accept-contact-terms','true');");

await page.Context.AddCookiesAsync(new List<Cookie> {
    new Cookie
    {
        Name= "token",
        Value= token,
        Domain = ".divar.ir",
        Path="/",
    }
});

var retryCount = 0;

do
{
    var getContactElement = page.Locator(".post-actions__get-contact").First;
    if (getContactElement is null)
    {
        var notFoundElement = page.Locator(".not-found-message").First;
        if (notFoundElement != null)
        {
            Log($"not found", ConsoleColor.DarkRed);
            return;
        }

        if (retryCount > 5)
        {
            return;
        }

        retryCount++;
        Log($"Wait for redirection", ConsoleColor.Yellow);

        await Task.Delay(TimeSpan.FromSeconds(5));
        continue;
    }

    await getContactElement.ClickAsync();
    break;
} while (true);

var allLinks = page.Locator("xpath=//*[@id=\"app\"]/div[1]/div/div/div[1]/div[3]/div[1]/div/div[2]/a").First;
//var b = page.Locator("a");
var c = await allLinks.InnerTextAsync();
//var c = await b.GetAttributeAsync("href");

;
//foreach (var element in allLinks)
//{
//    if (element.GetAttribute("href").StartsWith("tel"))
//    {
//        phoneNumberHref += element.GetAttribute("href");
//    }
//}

//if (string.IsNullOrWhiteSpace(phoneNumberHref))
//{
//    var blockElement = driver.FindElements(By.XPath("//*[text()='محدودیت نمایش اطلاعات تماس']")).FirstOrDefault();
//    if (blockElement is not null)
//    {
//        var blockBtn = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div/footer/button"));
//        blockBtn.Click();

//        Log("Token is blocked", ConsoleColor.Red);

//        mission.Status = Status.Fail;
//        mission.FailReason = MissionFailReason.Blocked;
//        mission.Summary = "Blocked";
//        Log($"Blocked | VendorLoginInfoId: {vendorLoginInfoId}", ConsoleColor.Yellow);
//    }
//}
//else
//{
//    var phoneNumber = phoneNumberHref.Replace("tel:", ",").Trim(',');
//    Log($"Count:{me.RequestCount} | {phoneNumber}", ConsoleColor.Green);

//    mission.Answer = phoneNumber;
//    mission.Status = Status.Success;
//    mission.FailReason = 0;
//    Log($"Done | VendorLoginInfoId: {vendorLoginInfoId}", ConsoleColor.Yellow);
//}

//if (string.IsNullOrWhiteSpace(phoneNumberHref))
//{
//    await GetAdverPhoneByHttpApiAsync(mission);
//    if (mission.FailReason != MissionFailReason.Blocked)
//    {

//    }
//}

await page.ScreenshotAsync(new()
{
    Path = "c:\\screenshot.png"
});

void Log(string log, ConsoleColor consoleColor = ConsoleColor.White)
{
    Console.ForegroundColor = consoleColor;
    Console.WriteLine($"{DateTimeOffset.Now:HH:mm:ss} | {log}");
    Console.ResetColor();
}