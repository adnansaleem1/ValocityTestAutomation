using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocityTestCases.Utility
{
    class SeleniumExtension
    {
        public static void ElementInDisplayTest(By element)
        {
            IWebDriver driver = DriverAccess.Shared();
            if (!driver.FindElement(element).Displayed)
            {
                throw new TestCaseException("Element Not in display");
            }
        }
        public static bool ElementInDisplay(By element)
        {
            IWebDriver driver = DriverAccess.Shared();
            if (driver.FindElement(element).Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static void click(By element)
        {
            IWebDriver driver = DriverAccess.Shared();
            Wait.WaitUntilElementClickAble(element);
            driver.FindElement(element).Click();
            Wait.InSeconds(1);
        }
        public static void AddTextToField(By by, string text)
        {
            IWebDriver driver = DriverAccess.Shared();
            IWebElement element = driver.FindElement(by);
            element.Clear();
            element.SendKeys(text);
        }
        public static bool ElementExists(By byelement)
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                driver.FindElement(byelement);
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public static bool ElementDisplay(By byelement)
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                IWebElement ellemnt= driver.FindElement(byelement);
                if (ellemnt.Displayed)
                {
                    return true;
                }else{
                return false;
                }
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        internal static void SelectByText(By by, string p)
        {
            IWebDriver driver = DriverAccess.Shared();
            SelectElement Select= new SelectElement(driver.FindElement(by));
            Select.SelectByText(p);
        }
 
        internal static void SelectByValue(By by, string p)
        {
            IWebDriver driver = DriverAccess.Shared();
            SelectElement Select = new SelectElement(driver.FindElement(by));
            Select.SelectByValue(p);
        }
        internal static void ScrolElementToDisplay(int x,int y) {
            IWebDriver driver = DriverAccess.Shared();
            //IWebElement element = driver.FindElement(by);
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo("+x+","+y+");");
            Wait.InSeconds(1);
           // A/ctions actions = new Actions(driver);
          //  actions.MoveToElement(element);
            // actions.click();
           // actions.Perform();
        }

        internal static void ScrolElementToDisplayByElement(By by)
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                IWebElement element = driver.FindElement(by);
                //  ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(" + x + "," + y + ");");

                Actions actions = new Actions(driver);
                actions.MoveToElement(element);
                //actions.click();
                actions.Perform();
                Wait.InSeconds(1);
            }
            catch (Exception)
            {
                
                //throw;
            }
        }

        internal static void SelectByText(IWebElement webElement,string t)
        {
            IWebDriver driver = DriverAccess.Shared();
            SelectElement Select = new SelectElement(webElement);
            Select.SelectByText(t);
        }
        internal static void UploadFile(string filepath_name,string JSSelect) {
            IWebDriver driver = DriverAccess.Shared();
            String script = "document.getElementById('fileName').value='" + filepath_name + "';";
            ((IJavaScriptExecutor)driver).ExecuteScript(script);
        
        }

        internal static void ExecuteJavaScript(string p)
        {
            IWebDriver driver = DriverAccess.Shared();
            ((IJavaScriptExecutor)driver).ExecuteScript(p);
        }

        internal static void ClickViaJavaScript(string p)
        {
            throw new NotImplementedException();
        }

        internal static void ClickViaJavaScript(By by)
        {
            IWebDriver driver = DriverAccess.Shared();
            IWebElement compare = driver.FindElement(by);
            IJavaScriptExecutor js = ((IJavaScriptExecutor)(driver));
            js.ExecuteScript("arguments[0].click();", compare);
            
        }

        internal static void ScrolElementToDisplayByElement(IWebElement Element)
        {
            IWebDriver driver = DriverAccess.Shared();
           // IWebElement element = driver.FindElement(by);
            //  ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(" + x + "," + y + ");");

            Actions actions = new Actions(driver);
            actions.MoveToElement(Element);
            //actions.click();
            actions.Perform();
            Wait.InSeconds(1);
        }
        internal static void ScrolElementToDisplayByElementWithMargin(IWebElement Element,int x,int y)
        {
            IWebDriver driver = DriverAccess.Shared();
            // IWebElement element = driver.FindElement(by);
            //  ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(" + x + "," + y + ");");

            Actions actions = new Actions(driver);
            actions.MoveToElement(Element,x,y);
            //actions.click();
            actions.Perform();
            Wait.InSeconds(1);
        }
        internal static void clickIfTextMatch(By by, string p)
        {
            IWebDriver driver = DriverAccess.Shared();

            try
            {
                IReadOnlyList<IWebElement> parent = driver.FindElements(by);
                foreach(IWebElement ele in parent){
                    if (Common.Compare(ele.Text, p)) {
                        ele.Click();
                        Wait.InSeconds(1);
                    }
                }

            }
            catch (Exception ex) { 
            
            
            }
        }

        internal static void ScrolElementToDisplayElement(IWebElement ele)
        {
            IWebDriver driver = DriverAccess.Shared();
            //IWebElement element = driver.FindElement(by);
            //  ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(" + x + "," + y + ");");

            Actions actions = new Actions(driver);
            actions.MoveToElement(ele);
            //actions.click();
            actions.Perform();
            Wait.InSeconds(1);
        }

        internal static void clickIfClickable(By by)
        {

            IWebDriver driver = DriverAccess.Shared();
            //if (driver.FindElement(by).Enabled == true) {

            try
            {
                driver.FindElement(by).Click();

            }
            catch (Exception)
            {
                
                //throw;
            }          //  }
        }

        internal static IWebElement FindElementByContainTextinOtherElement(By by, IWebElement ele)
        {
            try
            {
                 return ele.FindElement(by);
            }
            catch (Exception)
            {

                return null;
            }
        }

        internal static bool CheckIfAlertPresent()
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                driver.SwitchTo().Alert();
                return true;
            }   // try 
            catch (NoAlertPresentException Ex)
            {
                return false;
            } 
        }
        internal static void AcceptIfAlertPresent()
        {
            IWebDriver driver = DriverAccess.Shared();
            if (SeleniumExtension.CheckIfAlertPresent())
            {
                driver.SwitchTo().Alert().Accept();
            }
        }
        internal static string GetLocalPath()
        {
            IWebDriver driver = DriverAccess.Shared();
            var uri = new Uri(driver.Url);
            return uri.LocalPath;
        }




        internal static void Longclick(By by)
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                driver.FindElement(by).Click();
                Wait.InSeconds(1);
            }
            catch (Exception)
            {
                
                //throw;
            }
        }


        internal static IWebElement getRadioAndCheckBoxByNameFromGivenList(string text,System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> eleList, string Tagname)
        {
            IWebElement input = null;
            foreach (IWebElement ele in eleList) {

                if (Common.Compare(ele.FindElement(By.TagName(Tagname)).Text, text)) {
                    input= ele.FindElement(By.TagName("input"));
                    break;
                }
            }
            if (input == null) {

                throw new Exception(String.Format("Unable To Find checkBox/Radio By name :{0} in given List",text));
            }
            else if (!input.Displayed)
            {
                throw new Exception(String.Format("element checkBox/Radio By name :{0} is found in given List but not visible", text));
            }
            else {

                return input;
            }

        }

        internal static void ClickByText(ReadOnlyCollection<IWebElement> Elements, string Text)
        {
            IWebElement MyButton=Elements.FirstOrDefault(e => e.Text == Text);
            if (MyButton == null) {
                throw new Exception("Unable To Find Button.");
            }else{
            MyButton.Click();
                Wait.InSeconds(1);
            }
        }

        internal static void TryToClick(By by)
        {
            IWebDriver driver = DriverAccess.Shared();
            try
            {
                driver.FindElement(by).Click();
            }
            catch (Exception)
            {
            }
        }

        internal static IWebElement findButtonByText(ReadOnlyCollection<IWebElement> readOnlyCollection, string p)
        {
            return readOnlyCollection.First(e => e.Text == p);
        }

        internal static void _click(By element)
        {
            IWebDriver driver = DriverAccess.Shared();
            //Wait.WaitUntilElementClickAble(element);
            driver.FindElement(element).Click();
            Wait.InSeconds(1);
        }
    }
}
