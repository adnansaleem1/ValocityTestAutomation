using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocityTestCases.Utility
{
    class ReportsUtility
    {

        internal static void SelectOptioninFilterByName(OpenQA.Selenium.By by, string p)
        {
            try
            {

                IWebDriver driver = DriverAccess.Shared();
                driver.FindElement(by).FindElement(By.CssSelector("input[value='" + p + "']")).Click();
            }
            catch (Exception)
            {
                
               
            }
        }
    }
}
