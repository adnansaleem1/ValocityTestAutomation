using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using VelocityTestCases.References;


namespace VelocityTestCases.Utility
{
    static class DriverAccess
    {
        private static IWebDriver Mydriver = null;
        public static IWebDriver Shared()
        {
            if (Mydriver == null)
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddUserProfilePreference("download.default_directory", Config.DefaultFileDownloadPath);
                chromeOptions.AddUserProfilePreference("intl.accept_languages", "nl");
                chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                chromeOptions.AddUserProfilePreference("Proxy", "");

                //var driver = new ChromeDriver(@"D:\chromedriver_win32\", chromeOptions);

                Mydriver = new ChromeDriver(chromeOptions);
                Mydriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromMinutes(2));
                Mydriver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromMinutes(2));
            }
            return Mydriver;
        }
        public static IWebDriver NewDriver()
        {
            return new ChromeDriver(new ChromeOptions { Proxy = null });
        }
    }
}
