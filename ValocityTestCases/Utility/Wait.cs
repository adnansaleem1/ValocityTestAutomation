using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ValocityTestCases.References;

namespace ValocityTestCases.Utility
{
    class Wait
    {
        internal static void InSeconds(int sec)
        {
            int MilliSec = sec * 1000;
            MilliSec += Config.AddedDelay * 1000;
            Thread.Sleep(MilliSec);
        }

        internal static void WaitUntilElementPopup(By elementBy)
        {
            int MaxWaited = 0;
            Wait.InSeconds(1);
            IWebDriver driver = DriverAccess.Shared();
            while (!SeleniumExtension.ElementExists(elementBy))
            {
               // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
                Thread.Sleep(Config.LoopTimeOutToCheckElement * 1000);
                MaxWaited += Config.LoopTimeOutToCheckElement;
                if (MaxWaited >= Config.MaxTimeOutToCheckElement)
                {
                    break;
                    throw new TestCaseException("Max Wait Reached for Element Search Wait");
                }
            }
        }

        internal static void WaitUntilElementDisply(By elementBy)
        {
            int MaxWaited = 0;
            Wait.InSeconds(1);
            IWebDriver driver = DriverAccess.Shared();
            while (!SeleniumExtension.ElementDisplay(elementBy))
            {
                // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
                Thread.Sleep(Config.LoopTimeOutToCheckElement * 1000);
                MaxWaited += Config.LoopTimeOutToCheckElement;
                if (MaxWaited >= Config.MaxTimeOutToCheckElement)
                {
                    break;
                    throw new TestCaseException("Max Wait Reached for Element Search Wait");
                }
            }
        }

        internal static void WaitUntilLoadingInVisible(By by)
        {
            int MaxWaited = 0;
            Wait.InSeconds(1);
            IWebDriver driver = DriverAccess.Shared();
            while (SeleniumExtension.ElementDisplay(by))
            {
                // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
                Thread.Sleep(Config.LoopTimeOutToCheckElement * 1000);
                MaxWaited += Config.LoopTimeOutToCheckElement;
                if (MaxWaited >= Config.MaxTimeOutToCheckElement)
                {
                    break;
                    throw new TestCaseException("Max Wait Reached for Element Search Wait");
                }
            }
        }

        internal static void WaitUntilElementClickAble(By by)
        {
            IWebDriver driver = DriverAccess.Shared();
            int MaxWaited = 0;
            Wait.InSeconds(1);
            Wait.WaitUntilElementDisply(by);
            WebDriverWait wait = new WebDriverWait(driver, new  TimeSpan(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
            //while (SeleniumExtension.ElementDisplay(by))
            //{
            //    // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
            //    Thread.Sleep(Config.LoopTimeOutToCheckElement * 1000);
            //    MaxWaited += Config.LoopTimeOutToCheckElement;
            //    if (MaxWaited >= Config.MaxTimeOutToCheckElement)
            //    {
            //        break;
            //        throw new TestCaseException("Max Wait Reached for Element Search Wait");
            //    }
            //}
        }

        internal static void UntilSucessMessageShow()
        {
            try
            {
                int MaxWaited = 0;
                // Wait.InSeceonds(1);
                IWebDriver driver = DriverAccess.Shared();
                while (!SeleniumExtension.ElementExists(By.Id("toast-container")))
                {
                    // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
                    Thread.Sleep(100);
                    MaxWaited += Config.LoopTimeOutToCheckElement;
                    if (MaxWaited >= Config.MaxTimeOutToCheckElement)
                    {
                        break;
                        throw new TestCaseException("Max Wait Reached for Element Search Wait");
                    }
                }
                if (driver.FindElement(By.Id("toast-container")).FindElement(By.ClassName("toast-message")).Text != "Data saved successfully")
                {
                    throw new Exception("Data Save Field.....!");
                }
                Wait.InSeconds(3);
            }
            catch (Exception ex) {
                Wait.InSeconds(3);
            
            }
           
        }
        internal static void UntilFileDownloading() {

            try
            {
                int timeWaited = 0;
                int waitEachInterval=5000;
                while (!FileHandler.FileDownloaded() && timeWaited<(Config.MaxTimeOutToCheckFile*1000)) {
                    timeWaited += waitEachInterval;
                    Thread.Sleep(waitEachInterval);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        internal static void WaitUntilLoadingInVisible()
        {
           
          int MaxWaited = 0;
            Wait.InSeconds(1);
            IWebDriver driver = DriverAccess.Shared();
            while (SeleniumExtension.ElementDisplay(By.ClassName(CommonElements.loadingBackDrop_div_Class)))
            {
                // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
                Thread.Sleep(Config.LoopTimeOutToCheckElement * 1000);
                MaxWaited += Config.LoopTimeOutToCheckElement;
                if (MaxWaited >= Config.MaxTimeOutToCheckElement)
                {
                    break;
                    throw new TestCaseException("Max Wait Reached for Element Search Wait");
                }
            }
            while (SeleniumExtension.ElementDisplay(By.Id("body_busyBackdrop")))
            {
                // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
                Thread.Sleep(Config.LoopTimeOutToCheckElement * 1000);
                MaxWaited += Config.LoopTimeOutToCheckElement;
                if (MaxWaited >= Config.MaxTimeOutToCheckElement)
                {
                    break;
                    throw new TestCaseException("Max Wait Reached for Element Search Wait");
                }
            }
            
        }

        internal static void WaitUntilElementDisply(By by, int p)
        {
            int MaxWaited = 0;
            Wait.InSeconds(1);
            IWebDriver driver = DriverAccess.Shared();
            while (!SeleniumExtension.ElementDisplay(by))
            {
                // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
                Thread.Sleep(Config.LoopTimeOutToCheckElement * 1000);
                MaxWaited += Config.LoopTimeOutToCheckElement;
                if (MaxWaited/1000 >= p)
                {
                    break;
                    throw new TestCaseException("Max Wait Reached for Element Search Wait");
                }
            }
        }

        internal static void UntilModalisVisible(By by)
        {
            int MaxWaited = 0;
            Wait.InSeconds(1);
            IWebDriver driver = DriverAccess.Shared();
            while (SeleniumExtension.ElementDisplay(by))
            {
                // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Config.LoopTimeOutToCheckElement));
                Thread.Sleep(Config.LoopTimeOutToCheckElement * 1000);
                MaxWaited += Config.LoopTimeOutToCheckElement;
                if (MaxWaited >= Config.MaxTimeOutToCheckElement)
                {
                    break;
                    throw new TestCaseException("Max Wait Reached for Element Search Wait");
                }
            }
        }

        internal static void InMinute()
        {
            throw new NotImplementedException();
        }

        internal static void InMinute(int WaitTimeForVerification)
        {
            Thread.Sleep((WaitTimeForVerification * 1000 * 60));
        }
    }
}
