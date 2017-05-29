using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VelocityTestCases.Entity;
using VelocityTestCases.References;

namespace VelocityTestCases.Utility
{
    class TestCasesCommon
    {
        public static void RedirectToHome()
        {

            IWebDriver driver = DriverAccess.Shared();
            if (driver.Url != Links.URL_VelocityBase+Links.Velocity_Eit_Home)
            {
                driver.Url = Links.URL_VelocityBase + Links.Velocity_Eit_Home;
            }
        }
        public static void RedirectToHome(IWebDriver driver)
        {
            driver.Url = Links.URL_VelocityBase + Links.Velocity_Eit_Home;
        }
        public static void Redirect(IWebDriver driver, string Url)
        {
            driver.Url = Url;
        }
        public static void Redirect(string Url)
        {
            IWebDriver driver = DriverAccess.Shared();
            driver.Url = Url;
        }

     
 
       
        public static void LicenceAgrement()
        {
            IWebDriver driver = DriverAccess.Shared();
            try
            {
                Thread.Sleep(3000);
                if (driver.PageSource.Contains("ESP (ALL) Terms & Conditions"))
                {
                    driver.FindElement(By.Id(TestElements.license_1stAcceptCheckbox)).Click();
                    driver.FindElement(By.Id(TestElements.license_2ndAcceptCheckbox)).Click();
                    driver.FindElement(By.XPath(("//input[@value=\'"
                                        + (TestElements.license_Signaturetxt + "\']")))).SendKeys("QA");
                    driver.FindElement(By.Id(TestElements.license_Acceptbtn)).Click();
                    Thread.Sleep(5000);
                }

                if (driver.FindElement(By.XPath(("//button[@class=\'"
                                    + (TestElements.license_GetStartedbtn + "\']")))).Displayed)
                {
                    driver.FindElement(By.XPath(("//button[@class=\'"
                                        + (TestElements.license_GetStartedbtn + "\']")))).Click();
                    Thread.Sleep(3000);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }


       
        internal static IWebElement CheckResultOfManageproductSearchForID(string ID)
        {
            IWebDriver driver = DriverAccess.Shared();
            IWebElement SearchParent = driver.FindElement(By.CssSelector("div[data-bind='foreach: products()']"));
            IReadOnlyCollection<IWebElement> SearchResultRows = SearchParent.FindElements(By.ClassName("product-row"));
            foreach (IWebElement row in SearchResultRows)
            {
                if (row.FindElement(By.CssSelector("span[data-bind='text: xid']")).Text == ID)
                {
                    return row;
                    break;
                }
            }
            return null;

        }



          internal static void AddBulkKeyWordsinDialog(IList<string> list)
          {
              IWebDriver driver = DriverAccess.Shared();
              string keywords="";
              SeleniumExtension.click(By.LinkText("Add Keywords"));
              foreach(string a in list){
              keywords+=a+",";
              }
              SeleniumExtension.AddTextToField(By.Id("token-input-productKeywords-EIT"), keywords);
              Wait.InSeconds(1);
              SeleniumExtension.click(By.XPath("//*[@id=\"bulkEditKeywordsModal-EIT\"]/div[2]/button"));
              Wait.InSeconds(1);
              SeleniumExtension.click(By.CssSelector("button[data-target='#sayModalSuccessDialog']"));
          }
    }
}
