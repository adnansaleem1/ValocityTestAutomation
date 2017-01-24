using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValocityTestCases.References;

namespace ValocityTestCases.Utility
{
    class ProductSearchUtility
    {

        internal static IWebElement findResultFromSearchResult(bool Active = true, string id = null)
        {
            IWebElement Result = null;
            IWebDriver driver = DriverAccess.Shared();
            IWebElement Parent;
            IList<IWebElement> childs;

            do
            {
                Parent = driver.FindElement(By.CssSelector("div[data-bind='foreach: products()']"));
                childs = Parent.FindElements(By.ClassName("product-row"));
                if (childs.Count > 0)
                {
                    if (id != null)
                    {
                        foreach (IWebElement ele in childs)
                        {
                            if (Common.Compare(ele.FindElement(By.CssSelector("span[data-bind='text: xid']")).Text, id))
                            {
                                Result = ele;
                                break;
                            }
                        }

                    }
                    else
                    {
                        foreach (IWebElement ele in childs)
                        {
                            IList<IWebElement> productStatus = ele.FindElement(By.ClassName("product-options")).FindElements(By.TagName("strong"));
                            if (Active == productStatus[2].Displayed && ele.FindElement(By.ClassName("product-options")).FindElement(By.TagName("button")).Enabled)
                            {
                                Result = ele;
                                break;
                            }

                        }
                    }

                }
                if (Result != null)
                {
                    break;
                }
            } while (ProductSearchUtility.NextPage());
            if (Result == null)
            {
                throw new Exception("No Element found from search Result on Given critaria.");
            }
            else
            {
                return Result;
            }


        }
        internal static void ActionOnSearchTile(IWebElement tile, string Action)
        {
            IWebDriver driver = DriverAccess.Shared();
            IReadOnlyList<IWebElement> ActionButtons = tile.FindElements(By.TagName("button"));
            foreach (IWebElement btn in ActionButtons)
            {
                if (Common.Compare(btn.Text, Action))
                {
                    SeleniumExtension.ScrolElementToDisplayElement(tile);
                    Wait.InSeconds(1);
                    btn.Click();
                    Wait.InSeconds(1);
                    //  driver.FindElement(By.LinkText("Yes, Delete this Product"));
                    //clicked = true;
                    Wait.WaitUntilLoadingInVisible(By.ClassName(CommonElements.loadingBackDrop_div_Class));
                    break;
                }
            }
            if (Action == "Edit")
            {
                ProductSearchUtility.AcceptApiEditOverRide();
            }

        }
        internal static void AcceptApiEditOverRide()
        {
            IWebDriver driver = DriverAccess.Shared();
            Wait.InSeconds(1);
            if (SeleniumExtension.ElementInDisplay(By.Id("editProductWarningDialog")))
            {
                try
                {
                    driver.FindElement(By.Id("editProductWarningDialog")).FindElement(By.TagName("button")).Click();
                }
                catch (Exception) { }
            }
            if (SeleniumExtension.ElementInDisplay(By.Id("bulkOperationWarningDialog")))
            {
                try
                {
                    driver.FindElement(By.Id("bulkOperationWarningDialog")).FindElement(By.TagName("button")).Click();
                }
                catch (Exception) { }
            }
        }
        internal static void SearchProductByName(string name)
        {
            IWebDriver driver = DriverAccess.Shared();
            SeleniumExtension.ScrolElementToDisplay(0, 0);
            Wait.InSeconds(1);
            SeleniumExtension.AddTextToField(By.Id("supplierSearchTextBox"), name);
            Wait.InSeconds(1);
            try
            {
                SeleniumExtension.click(By.Id("SearchButtonParent"));

            }
            catch (Exception)
            {

                SeleniumExtension.click(By.Id("SearchButton"));
            }
            Wait.InSeconds(3);
        }
        internal static void SearchProductByID(string id)
        {
            IWebDriver driver = DriverAccess.Shared();
            Wait.WaitUntilLoadingInVisible();
            SeleniumExtension.ScrolElementToDisplay(0, 0);
            Wait.InSeconds(1);
            SeleniumExtension.AddTextToField(By.Id("supplierSearchTextBox"), id);
            Wait.InSeconds(1);
            try
            {
                SeleniumExtension.click(By.Id("SearchButtonParent"));

            }
            catch (Exception)
            {

                SeleniumExtension.click(By.Id("SearchButton"));
            } Wait.InSeconds(3);
        }
        internal static IWebElement GetFirstFromSearchResult()
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                IWebElement Parent = driver.FindElement(By.CssSelector("div[data-bind='foreach: products()']"));
                IList<IWebElement> childs = Parent.FindElements(By.ClassName("product-row"));
                IWebElement row = null;
                foreach (IWebElement ele in childs)
                {
                    try
                    {
                        ele.FindElement(By.ClassName("informationMessage"));
                    }
                    catch (Exception)
                    {
                        row = ele;
                        break;
                    }
                }
                return row;
            }
            catch (Exception)
            {

                throw new Exception("No Result Found in Product Search");
            }
        }

        internal static IWebElement GetProductUnderReviewFromSearchResult()
        {
            IWebDriver driver = DriverAccess.Shared();
            IWebElement Parent = driver.FindElement(By.CssSelector("div[data-bind='foreach: products()']"));
            IList<IWebElement> childs = Parent.FindElements(By.ClassName("product-row"));
            foreach (IWebElement ele in childs)
            {

                try
                {
                    if (ele.FindElement(By.ClassName("informationMessage")).Displayed && ele.FindElement(By.LinkText("Make Changes Active")).Displayed)
                    {

                        return ele;
                    }

                }
                catch (Exception)
                {

                    //throw;
                }
            }
            throw new Exception("No Product Under Review in search Result");
            // return childs[0];
        }
        internal static void TryToSelectMultipleProducts(int p)
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                IWebElement Parent = driver.FindElement(By.CssSelector("div[data-bind='foreach: products()']"));
                IList<IWebElement> childs = Parent.FindElements(By.ClassName("product-row"));
                for (int count = 0; count < p; count++)
                {
                    childs[count].FindElement(By.ClassName("product-image")).FindElement(By.TagName("input")).Click();
                }
            }
            catch (Exception)
            {


            }
        }

        internal static void SelectDefaultFilterByName(string name)
        {
            try
            {

                IWebDriver driver = DriverAccess.Shared();
                IReadOnlyList<IWebElement> Links = driver.FindElement(By.Id("accordion2")).FindElements(By.TagName("a"));
                foreach (IWebElement ele in Links)
                {
                    if (Common.Compare(ele.Text, name))
                    {
                        SeleniumExtension.ScrolElementToDisplayByElement(ele);
                        Wait.InSeconds(1);
                        ele.Click();
                        break;
                    }
                }
                SeleniumExtension.ScrolElementToDisplay(0, 0);
                Wait.InSeconds(4);
            }
            catch (Exception)
            {

                throw;
            }
        }
        internal static void ClearAllFilters()
        {
            try
            {

                IWebDriver driver = DriverAccess.Shared();
                driver.FindElement(By.Id("clearAll")).Click();
                Wait.InSeconds(5);
            }
            catch (Exception)
            {

                throw;
            }
        }
        internal static void SelectFilterFromMoreFilters(string Heading, string Filter)
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                IReadOnlyList<IWebElement> headings = driver.FindElement(By.Id("accordion2")).FindElements(By.ClassName("accordion-heading"));
                foreach (IWebElement head in headings)
                {
                    if (Common.Compare(head.FindElement(By.TagName("span")).Text, Heading))
                    {
                        IWebElement aTag = head.FindElement(By.TagName("a"));
                        SeleniumExtension.ScrolElementToDisplayByElement(head.FindElement(By.XPath("..")));
                        // IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                        // long value = (long)executor.ExecuteScript("return window.scrollY;");
                        //SeleniumExtension.ScrolElementToDisplay(200, (int)value-200);
                        Wait.InSeconds(1);
                        if (aTag.GetAttribute("class").Contains("collapsed") || head.FindElement(By.XPath("..")).FindElement(By.ClassName("accordion-body")).GetAttribute("style") == "")
                        {

                            aTag.Click();
                        }
                        Wait.InSeconds(1);
                        ProductSearchUtility.SelectDefaultFilterByName(Filter);
                        break;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        internal static void SortByName(string p)
        {
            IWebDriver driver = DriverAccess.Shared();
            IWebElement SortElement = driver.FindElement(By.Id("btnSortBy"));
            SortElement.FindElement(By.ClassName("dropdown-toggle")).Click();
            Wait.InSeconds(1);
            IReadOnlyList<IWebElement> options = SortElement.FindElements(By.TagName("li"));
            foreach (IWebElement ele in options)
            {
                if (Common.Compare(ele.FindElement(By.TagName("a")).Text, p))
                {
                    ele.FindElement(By.TagName("a")).Click();
                    Wait.InSeconds(3);
                }
            }

        }

        internal static bool NextPage()
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                IWebElement nextbtn = driver.FindElement(By.Id("nextPageLinkId"));
                if (nextbtn.Enabled)
                {
                    nextbtn.Click();
                    Wait.InSeconds(5);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
        internal static bool PreviousPage()
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                IWebElement nextbtn = driver.FindElement(By.Id("prevPageLinkId"));
                if (nextbtn.Enabled)
                {
                    nextbtn.Click();
                    Wait.InSeconds(5);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
