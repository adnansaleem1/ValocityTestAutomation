using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityTestCases.Entity;
using VelocityTestCases.References;

namespace VelocityTestCases.Utility
{
    class CatalogUtility
    {
        public static void AddCatalog(Catalog cat) {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                SeleniumExtension.AddTextToField(By.Id("catalogName"), cat.Name+Config.TestIterationName_number);
                SeleniumExtension.SelectByText(By.XPath("//*[@id=\"catalogAndCompliance-view\"]/div[2]/div[1]/div[1]/div[2]/div[2]/div[1]/select"), cat.StartMonth);
                SeleniumExtension.SelectByText(By.XPath("//*[@id=\"catalogAndCompliance-view\"]/div[2]/div[1]/div[1]/div[2]/div[2]/div[2]/select"), cat.StartYear.ToString());
                SeleniumExtension.SelectByText(By.XPath("//*[@id=\"catalogAndCompliance-view\"]/div[2]/div[1]/div[1]/div[2]/div[3]/div[1]/select"), cat.EndMonth);
                SeleniumExtension.SelectByText(By.XPath("//*[@id=\"catalogAndCompliance-view\"]/div[2]/div[1]/div[1]/div[2]/div[3]/div[2]/select"), cat.EndYear.ToString());
                // SeleniumExtension.AddTextToField(By.Id("catalogName"),cat.Name);
                Wait.InSeconds(1);
                SeleniumExtension.click(By.Id("catalogByLink"));
                Wait.InSeconds(1);
                SeleniumExtension.AddTextToField(By.Id("catalogLink"), cat.Url);
                Wait.InSeconds(1);
                SeleniumExtension.click(By.Id("catalogByLink"));
                
                Wait.InSeconds(1);
                SeleniumExtension.click(By.XPath("//*[@id=\"catalogAndCompliance-view\"]/div[2]/div[1]/div[1]/div[2]/div[4]/div/div[3]/div/div/button"));
                Wait.InSeconds(4);
                if (SeleniumExtension.CheckIfAlertPresent()) {
                    driver.SwitchTo().Alert().Accept();
                    throw new Exception("Catalog Is Not Unique");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
           /// driver.FindElement().Clear().        
        }
        public static void AddDocument(Catalog cat)
        {

        }
        public static void UpdateCatalog(Catalog cat,string Name)
        {
            IWebDriver driver = DriverAccess.Shared();
            Wait.WaitUntilLoadingInVisible();
           IWebElement catalogElement  =CatalogUtility.FindCatalogByName(Name);
           if (catalogElement != null)
           {
               SeleniumExtension.ScrolElementToDisplayByElement(catalogElement);
               catalogElement.FindElement(By.CssSelector("a[data-bind='click: $root.openEditCatalogModal']")).Click();
               Wait.InSeconds(2);
               SeleniumExtension.AddTextToField(By.XPath("//*[@id=\"editCatalogModal\"]/div[2]/div/div[1]/div/input"), cat.Name+Config.TestIterationName_number+" Update Name");
               SeleniumExtension.SelectByText(By.XPath("//*[@id=\"editCatalogModal\"]/div[2]/div/div[3]/div/select[1]"), cat.StartMonth);
               SeleniumExtension.SelectByText(By.XPath("//*[@id=\"editCatalogModal\"]/div[2]/div/div[3]/div/select[2]"), cat.StartYear.ToString());
               SeleniumExtension.SelectByText(By.XPath("//*[@id=\"editCatalogModal\"]/div[2]/div/div[5]/div/select[1]"), cat.EndMonth);
               SeleniumExtension.SelectByText(By.XPath("//*[@id=\"editCatalogModal\"]/div[2]/div/div[5]/div/select[2]"), cat.EndYear.ToString());
               SeleniumExtension.AddTextToField(By.XPath("//*[@id=\"editCatalogModal\"]/div[2]/div/div[11]/div/input"),cat.Url);
               Wait.InSeconds(1);
               SeleniumExtension.click(By.XPath("//*[@id=\"editCatalogModal\"]/div[3]/button"));
               Wait.WaitUntilLoadingInVisible(By.ClassName(CommonElements.loadingBackDrop_div_Class));
               Wait.InSeconds(3);
               if (SeleniumExtension.CheckIfAlertPresent())
               {
                   driver.SwitchTo().Alert().Accept();
                   throw new Exception("Catalog Is Not Unique");
               }

           }
           else {
               throw new TestCaseException("No Catalog Found With this Name.");
           }
        }
        public static void UpdateDocument(Catalog cat,string updateName)
        {

        }
        public static IWebElement FindCatalogByName(string name)
        {
            IWebDriver driver = DriverAccess.Shared();
            IWebElement catalogElement = null;
            IWebElement Parent = driver.FindElement(By.XPath("//*[@id=\"catalogAndCompliance-view\"]/div[2]/div[1]/div[2]"));
            IList<IWebElement> catalogList = Parent.FindElements(By.XPath("./*"));
            foreach (IWebElement ele in catalogList) { 

            //IWebElement found= SeleniumExtension.FindElementByContainTextinOtherElement(By.XPath("//*[contains(text(), '"+name+"')]"),ele);
            if (ele.Text.Contains(name))
            {
                catalogElement= ele;
                break;
            }
            }
            return catalogElement;
        }
    }
}
