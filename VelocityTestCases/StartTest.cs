using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using VelocityTestCases.Utility;
using VelocityTestCases.References;
using System.Threading;
using System.Collections.Generic;
using VelocityTestCases.Entity;
using VelocityTestCases.BL;
using System.Data;
using RelevantCodes.ExtentReports;
using NUnit.Framework.Interfaces;

namespace VelocityTestCases
{
    [TestFixture]
    public class StartTest : ExtentBase
    {


        [Test, Order(0)]
        public void CheckEspLogo()
        {
            try
            {
                test = extent.StartTest("CheckEspLogo");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                SeleniumExtension.ElementInDisplayTest(By.XPath(TestElements.ESPLogo_Xpath));
                test.Log(LogStatus.Pass, "Pass");
                Logger.Log("ESP Web Logo Display - Pass", LogStatus.Pass, test);
            }
            catch (Exception ex)
            {
                Logger.Log("ESP Web Logo Display - Fail", ex, LogStatus.Fail, test);
                throw;
            }
        }
        [Test, Order(1)]
        public void SGRUserSearch()
        {
            try
            {
                test = extent.StartTest("SGRUserSearch");
                IWebDriver driver = DriverAccess.Shared();
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                UserUtility.SearchSupplierById(Credentials.SGR_User_ID);
                Logger.Log("Search SGR user - Pass", LogStatus.Pass, test);
            }
            catch (Exception ex)
            {
                Logger.Log("Search SGR user - Fail", ex, LogStatus.Fail, test);
                throw;
            }
        }
        [Test, Order(2)]
        public void AddProductWithSPG()
        {
            //IWebDriver driver =DriverAccess.Shared();
            try
            {
                test = extent.StartTest("AddProductWithSPG");
                string ProductID = "";
                IWebDriver driver = DriverAccess.Shared();
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                ProductUtility.AddNewProductInItDialog(Info.NewProduct_Name, Info.NewProduct_Description, Info.NewProduct_Type_Text);
                //SeleniumExtension.click(By.XPath(TestElements.AddProduct_Apply_btn_Xpath));
                ProductUtility.FillProductBasicInfo(Info.NewProduct_Summary, Info.NewProduct_Catogories, Info.NewProduct_Keywords);
                ProductID = driver.FindElement(By.Id("externalProductId")).GetAttribute("value");
                ProductUtility.SelectImageForNewProduct();
                ProductUtility.SetProductColors();
                PriceObjectSPG price = new PriceObjectSPG();
                ProductUtility.SetPriceByObject(price.ProductPrice);
                ProductUtility.MakeActive();
                if (ProductUtility.ValidateActiveProcessonPriceTab())
                {
                    Logger.Log("new product Add and mark it as Active - Pass", LogStatus.Pass, test);
                    Info.Added_Active_Product_Id_SPG = ProductID;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("new product Add and mark it as Active - Fail", ex, LogStatus.Fail, test);
                throw;
            }
        }
        [Test, Order(3)]
        public void RestrictedKeyWordsSubmitAndReviewAndDeleteChanges()
        {
            //IWebDriver driver =DriverAccess.Shared();
            try
            {
                test = extent.StartTest("RestrictedKeyWordsSubmitAndReviewAndDeleteChanges");
                IWebDriver driver = DriverAccess.Shared();
                UserUtility.LoginToVelocity(Credentials.VelocityExternal_User);
                State.GotoSupplierHome();
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                ProductUtility.AddNewProductInItDialog(Info.NewProduct_Name_Restricted, Info.NewProduct_Description_Restricted, Info.NewProduct_Type_Text);
                ProductUtility.FillProductBasicInfo(Info.NewProduct_Summary_Restricted, Info.NewProduct_Catogories, Info.NewProduct_Keywords);
                ProductUtility.SelectImageForNewProduct();
                ProductUtility.SetProductColors();
                PriceObjectSPG price = new PriceObjectSPG();
                ProductUtility.SetPriceByObject(price.ProductPrice);
                ProductUtility.MakeActiveRestricted();
                Wait.UntilModalisVisible(By.ClassName("validatingModalPrice"));
                Wait.InSeconds(2);
                try
                {
                    SeleniumExtension.click(By.XPath(TestElements.AddProduct_SubmitRestrictedWords_btn_Xpath));
                    Logger.Log("new product submit With Restricted Words - Pass", LogStatus.Pass, test);
                    Wait.WaitUntilLoadingInVisible(By.ClassName(CommonElements.loadingBackDrop_div_Class));
                    Wait.WaitUntilElementDisply(By.XPath(TestElements.AddProduct_SuccessRestrictedSubmit__btn_Xapth), 15);
                    Wait.InSeconds(2);
                    SeleniumExtension.click(By.XPath(TestElements.AddProduct_SuccessRestrictedSubmit__btn_Xapth));
                    Wait.InSeconds(1);

                }
                catch (Exception ex)
                {
                    SeleniumExtension.click(By.XPath(TestElements.AddProduct_SucessOk_Btn_Xapth));
                    throw new Exception("This Product Does not Contain restricted Key Words.");
                }
                //TestCasesCommon.LogoutUser();
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                //   Wait.WaitUntilElementClickAble(By.XPath(TestElements.AddProduct_ManageProducts_tab_Xpath));
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                ProductSearchUtility.SearchProductByName("\"" + Info.NewProduct_Name_Restricted + "\"");
                ProductSearchUtility.GetProductUnderReviewFromSearchResult().FindElement(By.LinkText("Delete Changes")).Click();
                Wait.WaitUntilElementDisply(By.LinkText("Yes, Delete these changes"));
                SeleniumExtension.click(By.LinkText("Yes, Delete these changes"));
                Wait.WaitUntilLoadingInVisible(By.ClassName(CommonElements.loadingBackDrop_div_Class));
                Logger.Log("new product Added with Reistricted key Words and Remove after Review -- Pass", LogStatus.Pass, test);

            }
            catch (Exception ex)
            {
                Logger.Log("new product Added with Reistricted key Words and Remove after Review -- Fail", ex);
                throw;
            }
        }
        // [Test, Order(4)]
        public void RestrictedKeyWordsSubmitAndReviewAndMakeActive()
        {
            //IWebDriver driver =DriverAccess.Shared();
            try
            {
                test = extent.StartTest("RestrictedKeyWordsSubmitAndReviewAndMakeActive");
                IWebDriver driver = DriverAccess.Shared();
                UserUtility.LoginToVelocity(Credentials.VelocityExternal_User);
                State.GotoSupplierHome();
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                ProductUtility.AddNewProductInItDialog(Info.NewProduct_Name_Restricted, Info.NewProduct_Description_Restricted, Info.NewProduct_Type_Text);
                ProductUtility.FillProductBasicInfo(Info.NewProduct_Summary_Restricted, Info.NewProduct_Catogories, Info.NewProduct_Keywords);
                ProductUtility.SelectImageForNewProduct();
                ProductUtility.SetProductColors();
                PriceObjectSPG price = new PriceObjectSPG();
                ProductUtility.SetPriceByObject(price.ProductPrice);
                ProductUtility.MakeActive();
                Wait.InSeconds(2);
                try
                {
                    SeleniumExtension.click(By.XPath(TestElements.AddProduct_SubmitRestrictedWords_btn_Xpath));
                    Logger.Log("new product submit With Restricted Words - Pass", LogStatus.Pass, test);
                    Wait.WaitUntilLoadingInVisible(By.ClassName(CommonElements.loadingBackDrop_div_Class));
                    Wait.InSeconds(2);
                    SeleniumExtension.click(By.XPath(TestElements.AddProduct_SuccessRestrictedSubmit__btn_Xapth));
                    Wait.InSeconds(1);

                }
                catch (Exception ex)
                {
                    //Logger.Log("new product submit With Restricted Words - Fail");
                    SeleniumExtension.click(By.XPath(TestElements.AddProduct_SucessOk_Btn_Xapth));
                    throw new Exception("This Product Does not contain restricted key words");
                }
                //TestCasesCommon.LogoutUser();
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                //   Wait.WaitUntilElementClickAble(By.XPath(TestElements.AddProduct_ManageProducts_tab_Xpath));
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                ProductSearchUtility.SearchProductByName("\"" + Info.NewProduct_Name_Restricted + "\"");
                ProductSearchUtility.GetProductUnderReviewFromSearchResult().FindElement(By.LinkText("Make Changes Active")).Click();
                Wait.WaitUntilElementDisply(By.Id("validationModalDash"));
                SeleniumExtension.click(By.XPath(TestElements.AddProduct_SubmitRestrictedWords_btn_Xpath));
                Wait.WaitUntilLoadingInVisible(By.ClassName("publishSuccessModalDash"));
                SeleniumExtension.click(By.XPath(TestElements.AddProduct_SuccessRestrictedSubmit__btn_Xapth));
                Logger.Log("new product Added with Reistricted key Words and Active after Review -- Pass", LogStatus.Pass, test);

            }
            catch (Exception ex)
            {
                Logger.Log("new product Added with Reistricted key Words and Active after Review -- Fail", ex);
                throw;
            }
        }
        [Test, Order(5)]
        public void ProductWithMutiplePriceGrids()
        {
            try
            {
                test = extent.StartTest("ProductWithMutiplePriceGrids");
                string ProductID = "";
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                IWebDriver driver = DriverAccess.Shared();
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                ProductUtility.AddNewProductInItDialog(Info.NewProduct_Name, Info.NewProduct_Description, Info.NewProduct_Type_Text);
                ProductUtility.FillProductBasicInfo(Info.NewProduct_Summary, Info.NewProduct_Catogories, Info.NewProduct_Keywords);
                ProductID = driver.FindElement(By.Id("externalProductId")).GetAttribute("value");
                ProductUtility.SelectImageForNewProduct();
                ProductUtility.SetProductColors();
                PriceObjectMPG price = new PriceObjectMPG();
                ProductUtility.SetPriceByObject(price.ProductPrice);
                ProductUtility.MakeActive();
                if (ProductUtility.ValidateActiveProcessonPriceTab())
                {
                    Logger.Log("new product Add and mark it as Active - Pass", LogStatus.Pass, test);
                }

            }
            catch (Exception ex)
            {
                Logger.Log("new product Add and mark it as Active - Fail", ex);
                throw;
            }

        }
        [Test, Order(6)]
        public void DeleteImageForProduct()
        {
            try
            {
                test = extent.StartTest("DeleteImageForProduct");
                IWebDriver driver = DriverAccess.Shared();
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                //Make Sure At least one record is available for this name
                ProductSearchUtility.SearchProductByName("\"" + Info.NewProduct_Name_deleteImage + "\"");
                ProductSearchUtility.ActionOnSearchTile(ProductSearchUtility.findResultFromSearchResult(true), "Edit");
                State.TabSwitchByName(NewProductTabItems.Images);
                SeleniumExtension.ScrolElementToDisplay(0, 0);
                ProductUtility.DeleteImageForProduct();
                ProductUtility.SaveTabState();
                State.TabSwitchByName(NewProductTabItems.Pricing);
                ProductUtility.MakeActive();
                if (ProductUtility.ValidateActiveProcessonPriceTab())
                {
                    Logger.Log("Delete Image for product- Pass", LogStatus.Pass, test);
                }

            }
            catch (Exception ex)
            {
                Logger.Log("Delete Image for product- Fail", ex);
                throw;
            }
        }
        [Test, Order(7)]
        public void InActiveProduct()
        {
            try
            {
                test = extent.StartTest("InActiveProduct");
                IWebDriver driver = DriverAccess.Shared();
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                //ProductSearchUtility.SearchProductByName("\"" + Info.Product_Name_ActiveProductToInactive + "\"");
                ProductSearchUtility.ActionOnSearchTile(ProductSearchUtility.findResultFromSearchResult(true), "Make Inactive");//TestCasesCommon.InActiveProductInSearchResult();
                Wait.WaitUntilLoadingInVisible();
                Wait.WaitUntilElementDisply(By.Id("unpublishProductModal"));
                driver.FindElement(By.Id("unpublishProductModal")).FindElement(By.Id("unpublishNow")).Click();
                Wait.InSeconds(2);
                driver.FindElement(By.Id("unpublishProductModal")).FindElement(By.ClassName("btn-primary")).Click();
                Wait.WaitUntilLoadingInVisible();
                Logger.Log("In Active product- Pass", LogStatus.Pass, test);

            }
            catch (Exception Ex)
            {
                Logger.Log("In Active product- Fail", Ex);

                throw;
            }
            //  TestCasesCommon.LogoutUser();

        }
        [Test, Order(8)]
        public void Update_Edit_AlreadyActiveProduct()
        {
            try
            {
                test = extent.StartTest("Update_Edit_AlreadyActiveProduct");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                IWebDriver driver = DriverAccess.Shared();
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                ProductSearchUtility.ClearAllFilters();
                ProductSearchUtility.SearchProductByName("\"" + Info.Active_Product_Name_ForUpdate + "\"");
                ProductSearchUtility.ActionOnSearchTile(ProductSearchUtility.findResultFromSearchResult(true), "Edit");
                Wait.WaitUntilLoadingInVisible(By.ClassName(CommonElements.loadingBackDrop_div_Class));
                ProductUtility.EditBasicInfo(Info.NewProduct_Update_Summary + Config.TestIterationName_number, Info.NewProduct_Update_Keywords);
                State.TabSwitchByName(NewProductTabItems.Pricing);
                ProductUtility.MakeActive();
                if (ProductUtility.ValidateActiveProcessonPriceTab())
                {
                    Logger.Log("Update Active product information - Pass", LogStatus.Pass, test);
                }

            }
            catch (Exception ex)
            {
                Logger.Log("Update Active product information - Fail", ex);

                throw;
            }

        }
        [Test, Order(9)]
        public void CopyProductAndActiveIt()
        {
            try
            {
                test = extent.StartTest("CopyProductAndActiveIt");
                IWebDriver driver = DriverAccess.Shared();
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);

                //ProductSearchUtility.SearchProductByName("\"" + Info.Active_Product_Name_ForCopy + "\"");
                ProductSearchUtility.ActionOnSearchTile(ProductSearchUtility.findResultFromSearchResult(true), "Copy");
                Wait.InSeconds(2);
                SeleniumExtension.AddTextToField(By.Id("copyProdName"), Info.Active_Product_NewName_ForCopy + Config.TestIterationName_number);
                SeleniumExtension.click(By.XPath("//*[@id=\"copyProductModal\"]/div[1]"));
                SeleniumExtension.click(By.XPath("//*[@id=\"copyProductModal\"]/div[4]/button"));
                Wait.WaitUntilElementDisply(By.Id("sayModalSuccessForm"));
                if (Common.Compare(driver.FindElement(By.Id("sayModalSuccessForm")).FindElement(By.TagName("h4")).Text, "Success"))
                {
                    SeleniumExtension.click(By.XPath("//*[@id=\"sayModalSuccessForm\"]/div[3]/button"));
                    Wait.InSeconds(1);
                }
                else
                {
                    throw new Exception("copy product process fail");
                }
                Wait.InSeconds(5);
                ProductSearchUtility.SearchProductByName("\"" + Info.Active_Product_NewName_ForCopy + Config.TestIterationName_number + "\"");
                ProductSearchUtility.ActionOnSearchTile(ProductSearchUtility.findResultFromSearchResult(false), "Make Active");

                // ProductUtility.ActiveProductModal("effectiveDateModalDash");
                Wait.InSeconds(2);
                Wait.WaitUntilLoadingInVisible();
                // Wait.(By.ClassName("publishSuccessModalDash"));
                if (SeleniumExtension.ElementDisplay(By.ClassName("publishSuccessModalDash")))
                {
                    SeleniumExtension.click(By.CssSelector("button[data-bind='click: prodValidationHelper.closeProduct']"));
                    Logger.Log("Copy product with new name.- Pass", LogStatus.Pass, test);
                }
                else
                {
                    throw new Exception("Copy product fail");
                }


            }
            catch (Exception ex)
            {
                Logger.Log("Copy product with new name.- Fail", ex);
                throw;
            }
        }
        [Test, Order(10)]
        public void BulkOperationsOnProduct()
        {
            try
            {
                test = extent.StartTest("BulkOperationsOnProduct");

                IWebDriver driver = DriverAccess.Shared();
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                ProductSearchUtility.SearchProductByName("\"" + Info.Search_Product_Name_BulkOperation + "\"");
                //ProductSearchUtility.ActionOnSearchTile(ProductSearchUtility.findResultFromSearchResult(false), "Delete");
                //Number of products that you want to select
                ProductSearchUtility.TryToSelectMultipleProducts(4);
                SeleniumExtension.ScrolElementToDisplay(0, 0);
                SeleniumExtension.click(By.CssSelector("a[data-bind='click: $root.openBulkEditOptionsModal']"));
                Wait.InSeconds(1);
                SeleniumExtension.getRadioAndCheckBoxByNameFromGivenList("Selected Products", driver.FindElement(By.CssSelector("div[data-bind='with: bulkMode']")).FindElements(By.TagName("label")), "strong").Click();
                Wait.InSeconds(1);
                SeleniumExtension.click(By.LinkText("Keywords"));
                driver.FindElement(By.Id("bulkEditOptionsModal")).FindElement(By.LinkText("Bulk Edit")).Click();
                ProductSearchUtility.AcceptApiEditOverRide();
                Wait.InSeconds(1);
                TestCasesCommon.AddBulkKeyWordsinDialog(Info.Bulk_Add_KeyWords);
                Wait.InSeconds(1);
                try
                {

                    // SeleniumExtension.click(By.CssSelector("//*[@id=\"dashboard-view\"]/div[2]/div[2]/div/div[1]/div[3]/ul/li[1]/div[2]/label[3]/input"));
                    SeleniumExtension.ScrolElementToDisplay(0, 0);
                    ProductSearchUtility.TryToSelectMultipleProducts(4);
                    SeleniumExtension.ScrolElementToDisplay(0, 0);
                    SeleniumExtension.click(By.CssSelector("a[data-bind='click: $root.openBulkEditOptionsModal']"));
                    Wait.InSeconds(1);
                    Wait.WaitUntilLoadingInVisible();

                    SeleniumExtension.getRadioAndCheckBoxByNameFromGivenList("Selected Products", driver.FindElement(By.CssSelector("div[data-bind='with: bulkMode']")).FindElements(By.TagName("label")), "strong").Click();
                    //SeleniumExtension.click(By.XPath("//*[@id=\"dashboard-view\"]/div[2]/div[2]/div/div[1]/div[3]/ul/li[1]/div[2]/label[3]/input"));
                    SeleniumExtension.click(By.LinkText("Make Active"));
                    driver.FindElement(By.Id("bulkEditOptionsModal")).FindElement(By.LinkText("Bulk Edit")).Click();
                    ProductSearchUtility.AcceptApiEditOverRide();

                   // SeleniumExtension.click(By.XPath(TestElements.BulkActive_Btn_Xpath));
                   // Wait.InSeconds(2);
                    if (SeleniumExtension.CheckIfAlertPresent())
                    {
                        driver.SwitchTo().Alert().Accept();
                        // SeleniumExtension.click(By.LinkText("Make Active"));
                        Wait.InSeconds(1);
                        SeleniumExtension.click(By.XPath("//*[@id=\"bulkPublishModal\"]/div[3]/a"));
                        Wait.InSeconds(1);
                        ProductSearchUtility.TryToSelectMultipleProducts(4);
                    }
                    else
                    {
                        Wait.WaitUntilElementDisply(By.Id("bulkPublishModal"));
                        driver.FindElement(By.Id("bulkPublishModal")).FindElement(By.Id("effectiveNow")).Click();
                        Wait.InSeconds(1);
                        driver.FindElement(By.Id("bulkPublishModal")).FindElement(By.ClassName("btn-primary")).Click();
                        Wait.InSeconds(2);
                        if (SeleniumExtension.CheckIfAlertPresent())
                        {
                            driver.SwitchTo().Alert().Accept();
                            // SeleniumExtension.click(By.LinkText("Make Active"));
                            Wait.InSeconds(1);
                            SeleniumExtension.click(By.XPath("//*[@id=\"bulkPublishModal\"]/div[3]/a"));
                            Wait.InSeconds(2);
                            // ProductSearchUtility.TryToSelectMultipleProducts(4);
                        }
                        else
                        {
                            Wait.WaitUntilElementDisply(By.Id("sayModalSuccessForm"));
                            driver.FindElement(By.Id("sayModalSuccessForm")).FindElement(By.ClassName("btn-primary")).Click();
                            Wait.InSeconds(1);
                        }

                    }
                    Wait.InSeconds(1);
                }
                catch (Exception)
                {
                    Logger.Log("Bulk Active Product Fail- Fail");
                    throw;
                }
                //TestCasesCommon.LogoutUser();
                Logger.Log("Bulk operation on  Product - Pass", LogStatus.Pass, test);

            }
            catch (Exception ex)
            {
                Logger.Log("Bulk operation on  Product - Fail", ex);

                throw;
            }
        }
        [Test, Order(11)]
        public void SearchUsingKeyWords()
        {
            try
            {
                test = extent.StartTest("SearchUsingKeyWords");
                IWebDriver driver = DriverAccess.Shared();
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                foreach (string kw in FilterInfo.SearchKeywordsList)
                {
                    ProductSearchUtility.SearchProductByName(kw);
                    Wait.InSeconds(5);
                }
                Logger.Log("Search using keywords - pass", LogStatus.Pass, test);

            }
            catch (Exception ex)
            {
                Logger.Log("Search using keywords - fail", ex);
                throw;
            }
        }
        [Test, Order(12)]
        public void SearchUsingDefaultFiltersSearch()
        {
            try
            {
                test = extent.StartTest("SearchUsingDefaultFiltersSearch");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                IWebDriver driver = DriverAccess.Shared();
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                foreach (string filter in FilterInfo.DefaultFilterList)
                {

                    ProductSearchUtility.SelectDefaultFilterByName(filter);
                }
                Wait.InSeconds(1);
                ProductSearchUtility.ClearAllFilters();
                Logger.Log("Search using Default Filter - pass", LogStatus.Pass, test);
                //TestCasesCommon.LogoutUser();
            }
            catch (Exception ex)
            {
                Logger.Log("Search using Default Filter - Fail", ex);

                throw;
            }

        }
        [Test, Order(13)]
        public void SearchUsingMoreFiltersSearch()
        {
            try
            {
                test = extent.StartTest("SearchUsingMoreFiltersSearch");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                IWebDriver driver = DriverAccess.Shared();
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                SeleniumExtension.ScrolElementToDisplayByElement(By.Id("MoreFiltersText"));
                Wait.InSeconds(1);
                SeleniumExtension.click(By.Id("MoreFiltersText"));
                Wait.InSeconds(2);
                foreach (MoreFilter filter in FilterInfo.MoreFilterList)
                {
                    ProductSearchUtility.SelectFilterFromMoreFilters(filter.heading, filter.filter);
                }
                Wait.InSeconds(1);
                ProductSearchUtility.ClearAllFilters();
                Logger.Log("Search using More Filter - pass", LogStatus.Pass, test);

            }
            catch (Exception ex)
            {
                Logger.Log("Search using More Filter - Fail", ex);
                throw;
            }
        }
        [Test, Order(14)]
        public void productResultSortBy()
        {
            try
            {
                test = extent.StartTest("productResultSortBy");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                IWebDriver driver = DriverAccess.Shared();
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                ProductSearchUtility.SortByName("Product Name");
                Wait.InSeconds(2);
                ProductSearchUtility.SortByName("Product Number");
                Wait.InSeconds(2);
                Logger.Log("Sort product search result - pass", LogStatus.Pass, test);

            }
            catch (Exception ex)
            {
                Logger.Log("Sort product search result - Fail", ex);

                throw;
            }

        }
        [Test, Order(15)]
        public void NavigateThroughSearchResult()
        {
            try
            {

                test = extent.StartTest("NavigateThroughSearchResult");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                IWebDriver driver = DriverAccess.Shared();
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                ProductSearchUtility.SearchProductByName(FilterInfo.ProductName_SearchForNavigation);
                ProductSearchUtility.NextPage();
                ProductSearchUtility.NextPage();
                ProductSearchUtility.PreviousPage();
                Logger.Log("Navigate through search result - pass", LogStatus.Pass, test);
            }
            catch (Exception ex)
            {
                Logger.Log("Navigate through search result - fail", ex);
                throw;
            }
        }
        [Test, Order(16)]
        public void DeleteActiveProduct()
        {
            try
            {
                test = extent.StartTest("DeleteActiveProduct");
                IWebDriver driver = DriverAccess.Shared();
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                //Make Sure At least one record is available for this name
                ProductSearchUtility.SearchProductByName("\"" + Info.Product_Name_ActiveProduct + "\"");
                ProductSearchUtility.ActionOnSearchTile(ProductSearchUtility.findResultFromSearchResult(true), "Delete");
                Wait.WaitUntilElementDisply(By.LinkText("Yes, Delete this Product"));
                driver.FindElement(By.LinkText("Yes, Delete this Product")).Click();
                Wait.WaitUntilLoadingInVisible();
                Logger.Log("Delete Active product- Pass", LogStatus.Pass, test);

            }
            catch (Exception ex)
            {
                Logger.Log("Delete Active product- Fail", ex);

                throw;
            }
            // TestCasesCommon.LogoutUser();
        }
        [Test, Order(17)]
        public void DeleteInActiveProduct()
        {
            try
            {
                test = extent.StartTest("DeleteInActiveProduct");
                IWebDriver driver = DriverAccess.Shared();
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                ProductSearchUtility.SearchProductByName("\"" + Info.Product_Name_ActiveProduct + "\"");
                //this.InActiveProduct();
                ProductSearchUtility.ActionOnSearchTile(ProductSearchUtility.findResultFromSearchResult(false), "Delete");
                Wait.InSeconds(1);
                Wait.WaitUntilElementDisply(By.LinkText("Yes, Delete this Product"));
                driver.FindElement(By.LinkText("Yes, Delete this Product")).Click();
                Wait.WaitUntilLoadingInVisible();
                Wait.InSeconds(2);
                Logger.Log("Delete In Active Product - Pass", LogStatus.Pass, test);
                //  TestCasesCommon.LogoutUser();
            }
            catch (Exception ex)
            {
                Logger.Log("Delete In Active Product - Fail", ex);
                throw;
            }

        }
        [Test, Order(20)]
        public void ReviewSuppliersInfo()
        {
            try
            {
                test = extent.StartTest("ReviewSuppliersInfo");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                IWebDriver driver = DriverAccess.Shared();
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Supplier_info);
                Wait.InSeconds(2);
                Logger.Log("Review Supplier info - pass", LogStatus.Pass, test);
            }
            catch (Exception ex)
            {
                Logger.Log("Review Supplier info - fail", ex);

                throw;
            }

        }
        [Test, Order(21)]
        public void AddUpdateCataLog()
        {
            try
            {
                test = extent.StartTest("AddUpdateCataLog");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                IWebDriver driver = DriverAccess.Shared();
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.catalog_Compliances);
                Catalog catalog = CatalogObject.Newcatalog;
                CatalogUtility.AddCatalog(catalog);
                CatalogUtility.UpdateCatalog(CatalogObject.Updatecatalog, catalog.Name);
                Logger.Log("Add Update Catalog - pass", LogStatus.Pass, test);
            }
            catch (Exception ex)
            {
                Logger.Log("Add Update Catalog - fail", ex);
                throw;
            }

        }
        [Test, Order(22)]
        public void ReviewLicenceAggreement()
        {
            try
            {
                test = extent.StartTest("ReviewLicenceAggreement");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                IWebDriver driver = DriverAccess.Shared();
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                SeleniumExtension.click(By.LinkText("Help"));
                SeleniumExtension.click(By.LinkText("License Agreement"));
                Wait.WaitUntilElementDisply(By.Id("LicenseAgreementAlert"));
                //driver.FindElement(By.Id("laContents"));
                SeleniumExtension.click(By.XPath("//*[@id=\"LicenseAgreementAlert\"]/div[1]/button"));
                Wait.InSeconds(1);
                Logger.Log("Review Licence Aggreement - Pass", LogStatus.Pass, test);
            }
            catch (Exception ex)
            {
                Logger.Log("Review Licence Aggreement - Fail", ex);

                throw;
            }

        }
        #region Reports Testing Section
        [Test, Order(23)]
        public void Report_ZCodeReview()
        {
            try
            {
                test = extent.StartTest("Report_ZCodeReview");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                IWebDriver driver = DriverAccess.Shared();
                State.GotoEITHome();
                State.TabSwitchByName(EITUserTabItems.EIT_Reports);
                //SeleniumExtension.click(By.XPath(CommonElements.ManageProduct_Tab_Xpath));
                SeleniumExtension.SelectByText(driver.FindElement(By.Id("sectionSelection")).FindElement(By.TagName("select")), "Z code Base Price");
                Wait.InSeconds(1);
                ReportsUtility.SelectOptioninFilterByName(By.Id("ActiveZCodeBasePriceProducts"), "ANZS");
                ReportsUtility.SelectOptioninFilterByName(By.Id("ActiveZCodeBasePriceProducts"), "SGRS");
                if (!driver.FindElement(By.XPath(TestElements.DownloadReportBtnXpath)).Enabled)
                {
                    throw new Exception("Some thing went worng while downloading Z Code Report");
                }
                FileHandler.ClearDefaultFolder();
                SeleniumExtension.click(By.XPath(TestElements.DownloadReportBtnXpath));
                Wait.UntilFileDownloading();
                FileHandler.CheckIfExcelFileContainRecords(FileHandler.FindExcelFilePathForReport());

            }
            catch (Exception)
            {

                throw;
            }
        }
        [Test, Order(24)]
        public void Report_ProductWithMissingImages()
        {
            try
            {
                test = extent.StartTest("Report_ProductWithMissingImages");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                IWebDriver driver = DriverAccess.Shared();
                State.TabSwitchByName(EITUserTabItems.EIT_Reports);
                //SeleniumExtension.click(By.XPath(CommonElements.ManageProduct_Tab_Xpath));
                SeleniumExtension.SelectByText(driver.FindElement(By.Id("sectionSelection")).FindElement(By.TagName("select")), "Suppliers with missing product Images");
                Wait.InSeconds(1);
                ReportsUtility.SelectOptioninFilterByName(By.Id("MissingImages"), "ANZS");
                ReportsUtility.SelectOptioninFilterByName(By.Id("MissingImages"), "SGRS");
                if (!driver.FindElement(By.Id("MissingImages")).FindElement(By.TagName("button")).Enabled)
                {
                    throw new Exception("Some thing went worng while downloading Z Code Report");
                }
                FileHandler.ClearDefaultFolder();
                driver.FindElement(By.Id("MissingImages")).FindElement(By.TagName("button")).Click();
                Wait.UntilFileDownloading();
                FileHandler.CheckIfExcelFileContainRecords(FileHandler.FindExcelFilePathForReport());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Test, Order(25)]
        public void Report_ProductsMakeActive()
        {
            try
            {
                test = extent.StartTest("Report_ProductsMakeActive");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                IWebDriver driver = DriverAccess.Shared();
                State.TabSwitchByName(EITUserTabItems.EIT_Reports);
                //SeleniumExtension.click(By.XPath(CommonElements.ManageProduct_Tab_Xpath));
                SeleniumExtension.SelectByText(driver.FindElement(By.Id("sectionSelection")).FindElement(By.TagName("select")), "Products Made Active");
                Wait.InSeconds(1);
                ReportsUtility.SelectOptioninFilterByName(By.Id("ProdsMadeActive"), "ANZS");
                ReportsUtility.SelectOptioninFilterByName(By.Id("ProdsMadeActive"), "SGRS");
                SeleniumExtension.AddTextToField(By.Id("pmaSDate"), "01/01/2016");
                SeleniumExtension.AddTextToField(By.Id("pmaEDate"), "01/10/2016");

                if (!driver.FindElement(By.Id("ProdsMadeActive")).FindElement(By.TagName("button")).Enabled)
                {
                    throw new Exception("Some thing went worng while downloading Z Code Report");
                }
                FileHandler.ClearDefaultFolder();
                driver.FindElement(By.Id("ProdsMadeActive")).FindElements(By.TagName("button"))[2].Click();
                Wait.UntilFileDownloading();
                FileHandler.CheckIfExcelFileContainRecords(FileHandler.FindExcelFilePathForReport());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Test, Order(26)]
        public void Report_ActiveCatalog()
        {
            try
            {
                test = extent.StartTest("Report_ActiveCatalog");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                IWebDriver driver = DriverAccess.Shared();
                State.TabSwitchByName(EITUserTabItems.EIT_Reports);
                //SeleniumExtension.click(By.XPath(CommonElements.ManageProduct_Tab_Xpath));
                SeleniumExtension.SelectByText(driver.FindElement(By.Id("sectionSelection")).FindElement(By.TagName("select")), "Active Catalog");
                Wait.InSeconds(1);
                ReportsUtility.SelectOptioninFilterByName(By.Id("ActiveCatalog"), "ANZS");
                ReportsUtility.SelectOptioninFilterByName(By.Id("ActiveCatalog"), "SGRS");

                if (!driver.FindElement(By.Id("ActiveCatalog")).FindElement(By.TagName("button")).Enabled)
                {
                    throw new Exception("Some thing went worng while downloading Z Code Report");
                }
                FileHandler.ClearDefaultFolder();
                driver.FindElement(By.Id("ActiveCatalog")).FindElements(By.TagName("button"))[0].Click();
                Wait.UntilFileDownloading();
                FileHandler.CheckIfExcelFileContainRecords(FileHandler.FindExcelFilePathForReport());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Test, Order(27)]
        public void Report_ActiveSupplier()
        {
            try
            {
                test = extent.StartTest("Report_ActiveSupplier");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                IWebDriver driver = DriverAccess.Shared();
                State.TabSwitchByName(EITUserTabItems.EIT_Reports);
                //SeleniumExtension.click(By.XPath(CommonElements.ManageProduct_Tab_Xpath));
                SeleniumExtension.SelectByText(driver.FindElement(By.Id("sectionSelection")).FindElement(By.TagName("select")), "Active Suppliers");
                Wait.InSeconds(1);
                ReportsUtility.SelectOptioninFilterByName(By.Id("ActiveASuppliers"), "ANZS");
                ReportsUtility.SelectOptioninFilterByName(By.Id("ActiveASuppliers"), "SGRS");

                if (!driver.FindElement(By.Id("ActiveASuppliers")).FindElement(By.TagName("button")).Enabled)
                {
                    throw new Exception("Some thing went worng while downloading Z Code Report");
                }
                FileHandler.ClearDefaultFolder();
                driver.FindElement(By.Id("ActiveASuppliers")).FindElements(By.TagName("button"))[0].Click();
                Wait.UntilFileDownloading();
                FileHandler.CheckIfExcelFileContainRecords(FileHandler.FindExcelFilePathForReport());
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Test, Order(28)]
        public void Report_ProductSubmitForReview()
        {
            try
            {
                test = extent.StartTest("Report_ProductSubmitForReview");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                IWebDriver driver = DriverAccess.Shared();
                State.TabSwitchByName(EITUserTabItems.EIT_Reports);
                //SeleniumExtension.click(By.XPath(CommonElements.ManageProduct_Tab_Xpath));
                SeleniumExtension.SelectByText(driver.FindElement(By.Id("sectionSelection")).FindElement(By.TagName("select")), "Products in review by supplier");
                Wait.InSeconds(1);
                ReportsUtility.SelectOptioninFilterByName(By.Id("InReviewProds"), "ANZS");
                ReportsUtility.SelectOptioninFilterByName(By.Id("InReviewProds"), "SGRS");
                ReportsUtility.SelectOptioninFilterByName(By.Id("InReviewProds"), "SPLR");
                ReportsUtility.SelectOptioninFilterByName(By.Id("InReviewProds"), "DIST");

                if (!driver.FindElement(By.Id("InReviewProds")).FindElement(By.TagName("button")).Enabled)
                {
                    throw new Exception("Some thing went worng while downloading Report");
                }
                FileHandler.ClearDefaultFolder();
                driver.FindElement(By.Id("InReviewProds")).FindElements(By.TagName("button"))[0].Click();
                Wait.UntilFileDownloading();
                FileHandler.CheckIfExcelFileContainRecords(FileHandler.FindExcelFilePathForReport());
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Test, Order(29)]
        public void DownloadPriceDefaultTemplate()
        {
            try
            {
                test = extent.StartTest("DownloadPriceDefaultTemplate");
                IWebDriver driver = DriverAccess.Shared();
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                SeleniumExtension.click(By.XPath("//*[@id=\"dashboard-view\"]/div[2]/div[2]/div/div[2]/button[3]"));
                SeleniumExtension.click(By.CssSelector("button[data-bind='click: $root.downloadTemplate']"));
                Wait.UntilFileDownloading();
                if (FileHandler.CompareTwoFiles(FileHandler.FindExcelFilePathForReport(), "\\Files\\TemplateImportSchema.csv"))
                {
                    Logger.Log("Download Price Default Template and review - Pass", LogStatus.Pass, test);
                }
                else
                {
                    throw new Exception("Tempate does not match with old one");
                }
                SeleniumExtension.click(By.LinkText("Cancel"));
            }
            catch (Exception ex)
            {
                Logger.Log("Download Price Default Template and review - fail", ex);

                throw;
            }

        }
        #endregion

        #region Api Testing Section
        //[Test, Order(30)]
        public void API2_GetProductById()
        {
            try
            {
                this.AddProductWithSPG();
                test = extent.StartTest("API2_GetProductById");

                string productId = Info.Added_Active_Product_Id_SPG;
                UserUtility.LoginToAPI(Credentials.APIV2_User);
                ProductObject po = ApiOperations.GetProductByID(productId, Credentials.APIV2_User);
                if (po.ExternalProductId == productId)
                {
                    Logger.Log("Api-2 Get product By ID - Pass", LogStatus.Pass, test);
                }
                else
                {
                    Logger.Log("Api-2 Get product By ID - fail");
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Api-2 Get product By ID - fail", ex);

                throw;
            }
        }
        //[Test, Order(31)]
        public void API2_PostProductAfterChange()
        {
            try
            {
                test = extent.StartTest("API2_PostProductAfterChange");
                string productId = Info.Added_Active_Product_Id_SPG;
                string NewProductName = "Test Case Itereation " + Config.TestIterationName_number + "Api 2";
                UserUtility.LoginToAPI(Credentials.APIV2_User);
                ApiOperations.ChangeProductName(productId, Credentials.APIV2_User, NewProductName);
                Logger.Log("Api-2 Get product By ID - Pass", LogStatus.Pass, test);
            }
            catch (Exception ex)
            {
                Logger.Log("Api-2 Get product By ID - fail", ex);

                throw;
            }
        }
        //[Test, Order(32)]
        public void API3_GetProductById()
        {
            try
            {
                test = extent.StartTest("API3_GetProductById");
                string productId = Info.Added_Active_Product_Id_SPG;
                UserUtility.LoginToAPI(Credentials.APIV3_User);
                ProductObject po = ApiOperations.GetProductByID(productId, Credentials.APIV3_User);
                if (po.ExternalProductId == productId)
                {
                    Logger.Log("Api-3 Get product By ID - Pass", LogStatus.Pass, test);
                }
                else
                {
                    Logger.Log("Api-3 Get product By ID - fail");
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Api-3 Get product By ID - fail", ex);

                throw;
            }
        }
        //  [Test, Order(33)]
        public void API3_PostProductAfterChange()
        {
            try
            {
                test = extent.StartTest("API3_PostProductAfterChange");
                string productId = Info.Added_Active_Product_Id_SPG;
                string NewProductName = "Test Case Itereation " + Config.TestIterationName_number + "Api 3";
                UserUtility.LoginToAPI(Credentials.APIV3_User);
                ApiOperations.ChangeProductName(productId, Credentials.APIV3_User, NewProductName);
                Logger.Log("Api-3 Post product after change - Pass", LogStatus.Pass, test);

            }
            catch (Exception ex)
            {
                Logger.Log("Api-3 Post product after change - fail", ex);

                throw;
            }
        }
        //[Test, Order(34)]
        public void API3_InActiveProduct()
        {
            try
            {
                test = extent.StartTest("API3_InActiveProduct");
                string productId = Info.Added_Active_Product_Id_SPG;
                //string NewProductName = "Test Case Itereation " + Config.TestIterationName_number;
                UserUtility.LoginToAPI(Credentials.APIV3_User);
                ApiOperations.InActiveProduct(productId, Credentials.APIV3_User);
                Logger.Log("Api-3 InActive Product - Pass", LogStatus.Pass, test);
            }
            catch (Exception ex)
            {
                Logger.Log("Api-3 InActive Product - fail", ex);

                throw;
            }
        }
        //[Test, Order(35)]
        public void API3_DeleteProduct()
        {
            try
            {
                test = extent.StartTest("API3_DeleteProduct");
                string productId = Info.Added_Active_Product_Id_SPG;
                // string NewProductName = "Test Case Itereation " + Config.TestIterationName_number;
                UserUtility.LoginToAPI(Credentials.APIV3_User);
                ApiOperations.DeleteProduct(productId, Credentials.APIV3_User);
                Logger.Log("Api-3 Delete Product - Pass", LogStatus.Pass, test);

            }
            catch (Exception ex)
            {
                Logger.Log("Api-3 Delete Product - fail", ex);

                throw;
            }
        }
        [Test, Order(36)]
        public void API3NET_GetProductById()
        {
            try
            {
                this.AddProductWithSPG();
                test = extent.StartTest("API3NET_GetProductById");

                string productId = Info.Added_Active_Product_Id_SPG;
                UserUtility.LoginToAPI(Credentials.APIV3Net_User);
                ProductObject po = ApiOperations.GetProductByID(productId, Credentials.APIV3Net_User);
                if (po.ExternalProductId == productId)
                {
                    Logger.Log("Api-3 Net Get product By ID - Pass", LogStatus.Pass, test);
                }
                else
                {
                    Logger.Log("Api-3 Net Get product By ID - fail");
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Api-3 Net Get product By ID - fail", ex);

                throw;
            }
        }
        [Test, Order(37)]
        public void API3NET_PostProductAfterChange()
        {
            try
            {
                test = extent.StartTest("API3NET_PostProductAfterChange");
                string productId = Info.Added_Active_Product_Id_SPG;
                string NewProductName = "Test Case Itereation " + Config.TestIterationName_number + "Api 4";
                UserUtility.LoginToAPI(Credentials.APIV3Net_User);
                ApiOperations.ChangeProductName(productId, Credentials.APIV3Net_User, NewProductName);
                Logger.Log("Api-3 NET Post product - Pass", LogStatus.Pass, test);

            }
            catch (Exception ex)
            {
                Logger.Log("Api-3 NET Post product - Fail", ex);

                throw;
            }
        }
        [Test, Order(38)]
        public void API3NET_VerifyHiddenKeyWordsWithoutChange()
        {
            try
            {
                test = extent.StartTest("API3NET_VerifyHiddenKeyWordsWithoutChange");
                IWebDriver driver = DriverAccess.Shared();
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);

                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                ProductSearchUtility.SearchProductByName(Info.Added_Active_Product_Id_SPG);
                ProductSearchUtility.ActionOnSearchTile(ProductSearchUtility.findResultFromSearchResult(false, Info.Added_Active_Product_Id_SPG), "Edit");
                Wait.InSeconds(1);
                //SeleniumExtension.click(By.XPath("//*[@id=\"editProductWarningDialog\"]/div[2]/button"));
                Wait.InSeconds(1);
                Wait.WaitUntilElementDisply(By.Id("productName"));
                Wait.WaitUntilLoadingInVisible();
                SeleniumExtension.ScrolElementToDisplayByElement(By.Id("token-input-seoKeywords"));

                IList<IWebElement> SEoli = driver.FindElement(By.Id("token-input-seoKeywords")).FindElement(By.XPath("..")).FindElement(By.XPath("..")).FindElements(By.TagName("li"));
                IList<IWebElement> Addli = driver.FindElement(By.Id("token-input-adKeywords")).FindElement(By.XPath("..")).FindElement(By.XPath("..")).FindElements(By.TagName("li"));
                if (SEoli.Count > 1 && Addli.Count > 1)
                {
                    Logger.Log("Verify Hidden Key Words Are There Without Change - Pass", LogStatus.Pass, test);

                }
                else
                {
                    Logger.Log("Verify Hidden Key Words Are There Without Change- fail");

                }
                State.GotoSupplierHome();
            }
            catch (Exception ex)
            {
                Logger.Log("Verify Hidden Key Words Are There Without Change- fail", ex);
                throw;
            }

        }
        [Test, Order(39)]
        public void API3NET_VerifyHiddenKeyWordsAfterdelete()
        {
            try
            {
                test = extent.StartTest("API3NET_VerifyHiddenKeyWordsAfterdelete");
                string productId = Info.Added_Active_Product_Id_SPG;
                string NewProductName = "Test Case Itereation " + Config.TestIterationName_number + "Api 4";
                UserUtility.LoginToAPI(Credentials.APIV3Net_User);
                ApiOperations.DeleteProductHiddenKeyWords(productId, Credentials.APIV3Net_User);

                IWebDriver driver = DriverAccess.Shared();
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.Supplier_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                ProductSearchUtility.SearchProductByID(Info.Added_Active_Product_Id_SPG);
                ProductSearchUtility.ActionOnSearchTile(ProductSearchUtility.findResultFromSearchResult(false, Info.Added_Active_Product_Id_SPG), "Edit");
                //Wait.InSeconds(1);
                //SeleniumExtension.click(By.XPath("//*[@id=\"editProductWarningDialog\"]/div[2]/button"));
                Wait.InSeconds(1);
                Wait.WaitUntilElementDisply(By.Id("productName"));
                Wait.WaitUntilLoadingInVisible();
                SeleniumExtension.ScrolElementToDisplayByElement(By.Id("token-input-seoKeywords"));

                IList<IWebElement> SEoli = driver.FindElement(By.Id("token-input-seoKeywords")).FindElement(By.XPath("..")).FindElement(By.XPath("..")).FindElements(By.TagName("li"));
                IList<IWebElement> Addli = driver.FindElement(By.Id("token-input-adKeywords")).FindElement(By.XPath("..")).FindElement(By.XPath("..")).FindElements(By.TagName("li"));
                if (SEoli.Count > 1 && Addli.Count > 1)
                {
                    Logger.Log("Verify Hidden Key Words Are There Without Change - Pass", LogStatus.Pass, test);

                }
                else
                {
                    Logger.Log("Verify Hidden Key Words Are There Without Change- fail");

                }
                State.GotoSupplierHome();
            }
            catch (Exception ex)
            {
                Logger.Log("Verify Hidden Key Words Are There Without Change- fail", ex);
                throw;
            }

        }
        [Test, Order(40)]
        public void API3NET_InActiveProduct()
        {
            try
            {
                test = extent.StartTest("API3NET_InActiveProduct");
                string productId = Info.Added_Active_Product_Id_SPG;
                // string NewProductName = "Test Case Itereation " + Config.TestIterationName_number;
                UserUtility.LoginToAPI(Credentials.APIV3Net_User);
                ApiOperations.InActiveProduct(productId, Credentials.APIV3Net_User);
                Logger.Log("Api-3 Net InActive Product - Pass", LogStatus.Pass, test);
            }
            catch (Exception ex)
            {
                Logger.Log("Api-3 Net InActive Product - fail", ex);

                throw;
            }
        }
        [Test, Order(41)]
        public void API3NET_DeleteProduct()
        {
            try
            {
                test = extent.StartTest("API3NET_DeleteProduct");
                string productId = Info.Added_Active_Product_Id_SPG;
                //string NewProductName = "Test Case Itereation " + Config.TestIterationName_number;
                UserUtility.LoginToAPI(Credentials.APIV3Net_User);
                ApiOperations.DeleteProduct(productId, Credentials.APIV3Net_User);
                Logger.Log("Api-3 net Delete Product - Pass", LogStatus.Pass, test);

            }
            catch (Exception ex)
            {
                Logger.Log("Api-3 net Delete Product - fail", ex);

                throw;
            }
        }
        #endregion
        [Test, Order(42)]
        public void KeyWordSearchVerificationInDownStream()
        {
            IWebDriver driver = DriverAccess.Shared();
            try
            {
                test = extent.StartTest("KeyWordSearchVerificationInDownStream");
                driver.Url = Links.URL_ESP_Web;
                UserUtility.LoginToESPweb(Credentials.ESP_web_AsiNumber, Credentials.ESP_Web_Username, Credentials.ESP_Web_Password);
                SeleniumExtension.Longclick(By.LinkText("ESP Web Home Page"));
                Wait.WaitUntilElementDisply(By.Id("searchKeyword"));
                SeleniumExtension.AddTextToField(By.Id("searchKeyword"), Info.NewProduct_Keywords[0]);
                SeleniumExtension.click(By.Id("btnQuickSearch"));
                Wait.WaitUntilLoadingInVisible(By.ClassName("block-ui-overlay"));
                for (var count = 1; count < Info.NewProduct_Keywords.Count; count++)
                {
                    SeleniumExtension.AddTextToField(By.CssSelector("input[ng-model='searchWithinTerms']"), Info.NewProduct_Keywords[count]);
                    SeleniumExtension.click(By.Id("btnSearchWithin"));
                    Wait.WaitUntilLoadingInVisible(By.ClassName("block-ui-overlay"));
                }
                if (driver.FindElement(By.ClassName("prod-list")).FindElements(By.ClassName("product-wrapper")).Count <= 0)
                {
                    throw new Exception("No Product Found With Given Key Words.");
                }
                Logger.Log("Key Words Search verification in down stram- Pass", LogStatus.Pass, test);
                driver.Url = Links.URL_VelocityBase;

            }
            catch (Exception ex)
            {
                Logger.Log("Key Words Search verification in down stram- Pass", ex);
                driver.Url = Links.URL_VelocityBase;
                throw;
            }

        }
        #region  ESP Updates for DIST/DECR
        [Test, Order(43)]
        public void Esp_CheckWebSitesLogo()
        {
            IWebDriver driver = DriverAccess.Shared();
            try
            {
                test = extent.StartTest("Esp_CheckWebSitesLogo");
                //  Wait.InSeconds(5);
                //UserUtility.LogoutVelocity();
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.distributer_AsiNumber);
                Wait.WaitUntilElementDisply(By.LinkText("Manage Products"));
                Wait.InSeconds(2);
                SeleniumExtension.ElementInDisplayTest(By.ClassName("espwebsitesLogo"));
                Logger.Log("Esp_CheckWebSitesLogo - Pass");
            }
            catch (Exception ex)
            {
                Logger.Log("Esp_CheckWebSitesLogo - Fail", ex);
                throw;
            }
        }
        [Test, Order(44)]
        public void Dist_AddPrivateProduct()
        {
            IWebDriver driver = DriverAccess.Shared();
            try
            {

                test = extent.StartTest("Dist_AddPrivateProduct");
                Info.Dist_PrivateProductNumber = "ATC" + Config.TestIterationName_number + "PR";
                Info.Dist_PrivateProductName = Info.NewProduct_Name;
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.distributer_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                ProductUtility.AddNewProductInItDialog(Info.Dist_PrivateProductName, Info.NewProduct_Description, Info.NewProduct_Type_Text);
                //SeleniumExtension.click(By.XPath(TestElements.AddProduct_Apply_btn_Xpath));
                ProductUtility.FillProductBasicInfo(Info.NewProduct_Summary, Info.NewProduct_Catogories, Info.NewProduct_Keywords, Info.Dist_PrivateProductNumber);
                SeleniumExtension.ScrolElementToDisplay(0, 0);
                SeleniumExtension.click(By.CssSelector("input[data-bind='checked: $root.distOnlyView']"));
                ProductUtility.SaveTabState();
                ProductUtility.SelectImageForNewProduct();
                ProductUtility.SetProductColors();
                PriceObjectSPG price = new PriceObjectSPG();
                ProductUtility.SetPriceByObject(price.ProductPrice);
                ProductUtility.MakeActive();
                //SeleniumExtension.ClickViaJavaScript(By.CssSelector(TestElements.AddProduct_MakeActive_btn_atribute));

                if (ProductUtility.ValidateActiveProcessonPriceTab())
                {
                    Logger.Log("new product Add and mark it as Active - Pass", LogStatus.Pass, test);
                }
            }
            catch (Exception ex)
            {


            }
        }
        [Test, Order(45)]
        public void Dist_VerifyPrivateProductInESPWebsites()
        {
            IWebDriver driver = DriverAccess.Shared();
            int WaitTimeForVerification = 0;
            Wait.InMinute(WaitTimeForVerification);
            try
            {
                test = extent.StartTest("Dist_VerifyPrivateProductInESPWebsites");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.distributer_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                State.SwitchApplication(Application.EspWebSites);
                IList<IWebElement> WebSiteList = driver.FindElement(By.Id("siteList")).FindElements(By.ClassName("list-item"));
                IWebElement webSite = null;
                foreach (var item in WebSiteList)
                {
                    IList<IWebElement> AncorList = item.FindElements(By.TagName("a"));
                    foreach (var a in AncorList)
                    {
                        if (a.Text.Contains("espwebsite.com"))
                        {
                            webSite = item;
                            break;
                        }

                    }
                    if (webSite != null) { break; }
                }
                if (webSite == null)
                {
                    throw new Exception("Unable to Find Website To verify New Active Product");
                }
                SeleniumExtension.ClickByText(webSite.FindElements(By.TagName("button")), "Edit Site");
                Wait.WaitUntilElementDisply(By.LinkText("Manage Products"));
                SeleniumExtension.click(By.LinkText("Manage Products"));
                Wait.InSeconds(1);
                SeleniumExtension.click(By.LinkText("Product Collections"));
                Wait.WaitUntilElementDisply(By.LinkText("Manage Product Collections"));
                SeleniumExtension.click(By.LinkText("Manage Product Collections"));
                IWebElement CollectionRow = driver.FindElement(By.CssSelector("table[role='grid']")).FindElements(By.TagName("tr"))[1];
                CollectionRow.FindElement(By.LinkText("Search")).Click();
                Wait.WaitUntilElementDisply(By.Id("topBreadcrumbs"));
                IWebElement NewAddedProduct = ProductUtility.SearchProductInWebSite("\"" + Info.Dist_PrivateProductName + "\"", Credentials.distributer_AsiNumber, Info.Dist_PrivateProductNumber, Info.Dist_PrivateProductName);
                if (NewAddedProduct != null)
                {
                    throw new Exception("Private Product is Visible in Esp Websites");
                }
                State.SwitchApplication(Application.EspUpdates);
                Wait.InSeconds(1);

            }
            catch (Exception ex)
            {
                throw;
                State.SwitchApplication(Application.EspUpdates);
                Wait.InSeconds(1);
            }
        }
        [Test, Order(46)]
        public void Dist_AddPrivateProductToCompanyStore()
        {
            IWebDriver driver = DriverAccess.Shared();
            try
            {
                test = extent.StartTest("Dist_AddPrivateProductToCompanyStore");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.distributer_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
            }
            catch (Exception ex)
            {


            }
        }
        [Test, Order(47)]
        public void Dist_AddsharedProduct()
        {
            IWebDriver driver = DriverAccess.Shared();
            try
            {
                Info.Dist_SharedProductNumber = "ATC" + Config.TestIterationName_number;
                Info.Dist_ShareProductName = Info.NewProduct_Name;
                test = extent.StartTest("Dist_AddsharedProduct");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.distributer_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                ProductUtility.AddNewProductInItDialog(Info.Dist_ShareProductName, Info.NewProduct_Description, Info.NewProduct_Type_Text);
                //SeleniumExtension.click(By.XPath(TestElements.AddProduct_Apply_btn_Xpath));
                ProductUtility.FillProductBasicInfo(Info.NewProduct_Summary, Info.NewProduct_Catogories, Info.NewProduct_Keywords, Info.Dist_SharedProductNumber);
                // ProductID = driver.FindElement(By.Id("externalProductId")).GetAttribute("value");
                ProductUtility.SelectImageForNewProduct();
                ProductUtility.SetProductColors();
                PriceObjectSPG price = new PriceObjectSPG();
                ProductUtility.SetPriceByObject(price.ProductPrice);
                ProductUtility.MakeActive();
                //SeleniumExtension.ClickViaJavaScript(By.CssSelector(TestElements.AddProduct_MakeActive_btn_atribute));
                if (ProductUtility.ValidateActiveProcessonPriceTab())
                {
                    Logger.Log("new product Add and mark it as Active - Pass", LogStatus.Pass, test);
                }

            }
            catch (Exception ex)
            {


            }
        }
        [Test, Order(48)]
        public void Dist_VerifySharedProductInESPWebsites()
        {
            IWebDriver driver = DriverAccess.Shared();
            int WaitTimeForVerification = 0;
            try
            {
                test = extent.StartTest("Dist_VerifySharedProductInESPWebsites");
                Wait.InMinute(WaitTimeForVerification);
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.distributer_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                State.SwitchApplication(Application.EspWebSites);
                IList<IWebElement> WebSiteList = driver.FindElement(By.Id("siteList")).FindElements(By.ClassName("list-item"));
                IWebElement webSite = null;
                foreach (var item in WebSiteList)
                {
                    IList<IWebElement> AncorList = item.FindElements(By.TagName("a"));
                    foreach (var a in AncorList)
                    {
                        if (a.Text.Contains("espwebsite.com"))
                        {
                            webSite = item;
                            break;
                        }
                    }
                    if (webSite != null)
                    {
                        break;
                    }
                }
                if (webSite == null)
                {
                    throw new Exception("Unable to Find Website To verify New Active Product");
                }
                SeleniumExtension.ClickByText(webSite.FindElements(By.TagName("button")), "Edit Site");
                Wait.WaitUntilElementDisply(By.LinkText("Manage Products"));
                SeleniumExtension.click(By.LinkText("Manage Products"));
                Wait.InSeconds(1);
                SeleniumExtension.click(By.LinkText("Product Collections"));
                Wait.WaitUntilElementDisply(By.LinkText("Manage Product Collections"));
                SeleniumExtension.click(By.LinkText("Manage Product Collections"));
                IWebElement CollectionRow = driver.FindElement(By.CssSelector("table[role='grid']")).FindElements(By.TagName("tr"))[1];
                CollectionRow.FindElement(By.LinkText("Search")).Click();
                Wait.WaitUntilElementDisply(By.Id("topBreadcrumbs"));
                IWebElement NewAddedProduct = ProductUtility.SearchProductInWebSite("\"" + Info.Dist_SharedProductNumber + "\"", Credentials.distributer_AsiNumber, Info.Dist_SharedProductNumber, Info.Dist_ShareProductName);
                if (NewAddedProduct == null)
                {
                    throw new Exception(string.Format("Unable to Find New Added Product(Id:{0}) in website After {1} Mins", Info.Dist_SharedProductNumber, WaitTimeForVerification));
                }

            }
            catch (Exception ex)
            {
                State.SwitchApplication(Application.EspUpdates);
                throw;
            }
        }
        [Test, Order(49)]
        public void Dist_CopySupplierProductUpdateAndActiveIt()
        {
            IWebDriver driver = DriverAccess.Shared();
            int ImagesCount, PriceGridCount, CopyImagesCount, CopyPricegridCount;
            // string Name, description;
            try
            {
                test = extent.StartTest("Dist_AddPrivateProductToCompanyStore");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.distributer_AsiNumber);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                // State.SwitchApplication(Application.EspWebSites);
                SeleniumExtension.click(By.XPath("//*[@id=\"dashboard-view\"]/div[2]/div[2]/div/div[2]/button[2]"));
                Wait.WaitUntilElementDisply(By.Id("Summary"));
                // Wait.WaitUntilElementDisply(By.LinkText("Manage Product Collections"));
                IWebElement SupplyProduct = ProductUtility.SearchProductInWebSite("VS1316", "93251");
                if (SupplyProduct != null)
                {
                    SupplyProduct.FindElement(By.Id("btnProductDetail")).Click();
                    Wait.WaitUntilElementDisply(By.ClassName("product-details-container"));
                    ImagesCount = driver.FindElement(By.ClassName("imageStrip")).FindElements(By.TagName("li")).Count;
                    PriceGridCount = driver.FindElements(By.ClassName("priceGridTop")).Count;
                    driver.FindElement(By.LinkText("Create Custom Product")).Click();
                    Wait.WaitUntilElementDisply(By.Id("summaryProductDescription"));
                    State.TabSwitchByName(NewProductTabItems.Images);
                    CopyImagesCount = driver.FindElements(By.ClassName("image-list-item")).Count;
                    if (ImagesCount != CopyImagesCount)
                    {

                        throw new Exception("Unable To Match Information With Copy Product..");
                    }
                    State.TabSwitchByName(NewProductTabItems.Basic_Detail);
                    //ProductUtility.FillProductBasicInfo(Info.NewProduct_Summary, Info.NewProduct_Catogories, Info.NewProduct_Keywords);

                    ProductUtility.EditBasicInfo(Info.NewProduct_Update_Summary + Config.TestIterationName_number, Info.NewProduct_Update_Keywords);
                    //ProductUtility.SaveTabState();
                    State.TabSwitchByName(NewProductTabItems.Pricing);
                    ProductUtility.MakeActive();
                    if (ProductUtility.ValidateActiveProcessonPriceTab())
                    {
                        Logger.Log("new product Add and mark it as Active - Pass", LogStatus.Pass, test);

                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Copy Supplier Product Update AndA ctive It - Fail "+ex.ToString());
            }
        }
        [Test, Order(50)]
        public void Dist_SupplierInfoPageShouldNotBeThere()
        {
            try
            {
                test = extent.StartTest("Dist_SupplierInfoPageShouldNotBeThere");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.distributer_AsiNumber);
                try
                {
                    Wait.WaitUntilElementClickAble(By.LinkText("Manage Products"));
                    SeleniumExtension.click(By.LinkText("Supplier Info"));
                    throw new Exception("Supplier Info Page was found");
                }
                catch (Exception ex)
                {
                    Logger.Log("Supplier Info Page Not Found- successfull");
                }
            }
            catch (Exception)
            {
                Logger.Log("Supplier Info Page Not Found- fail");
                throw;
            }

        }
        [Test, Order(51)]
        public void Dist_LicenseAgreementReview()
        {
            try
            {
                test = extent.StartTest("Dist_LicenseAgreementReview");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoSupplierHomeByID(Credentials.distributer_AsiNumber);
                Wait.WaitUntilElementClickAble(By.LinkText("Manage Products"));
                SeleniumExtension.click(By.LinkText("Help"));
                SeleniumExtension.click(By.LinkText("License Agreement"));
                Wait.WaitUntilElementDisply(By.Id("LicenseAgreementAlert"));
                //driver.FindElement(By.Id("laContents"));
                SeleniumExtension.click(By.XPath("//*[@id=\"LicenseAgreementAlert\"]/div[1]/button"));
                Wait.InSeconds(1);
                Logger.Log("Review Licence Aggreement - Pass", LogStatus.Pass, test);
            }
            catch (Exception ex)
            {
                Logger.Log("Review Licence Aggreement - Fail", ex);

                throw;
            }

        }
        [Test, Order(52)]
        public void Dist_Compliance_ImportExport_BulkOperations_ShouldNotBeThere()
        {
            IWebDriver driver = DriverAccess.Shared();

            test = extent.StartTest("Dist_Compliance_ImportExport_BulkOperations_ShouldNotBeThere");
            UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
            State.GotoSupplierHomeByID(Credentials.distributer_AsiNumber);
            State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);

            if (SeleniumExtension.ElementDisplay(By.LinkText("Catalog & Compliance")))
            {
                throw new Exception("Catalog And Compliance Tab is visible");
            }
            else if (SeleniumExtension.ElementDisplay(By.CssSelector("a[data-bind='click: openBulkEditOptionsModal']")))
            {
                throw new Exception("Bulk Operation Button is visible");

            }
            else if (SeleniumExtension.ElementDisplay(By.XPath("//*[@id=\"dashboard-view\"]/div[2]/div[2]/div/div[2]/button[1]")) && driver.FindElement(By.XPath("//*[@id=\"dashboard-view\"]/div[2]/div[2]/div/div[2]/button[1]")).Text == "Import Products")
            {
                throw new Exception("Import Button is visible");
            }
            else if (SeleniumExtension.ElementDisplay(By.XPath("//*[@id=\"dashboard-view\"]/div[2]/div[2]/div/div[2]/button[1]")) && driver.FindElement(By.XPath("//*[@id=\"dashboard-view\"]/div[2]/div[2]/div/div[2]/button[1]")).Text == "Export Products")
            {
                throw new Exception("Export Operation Button is visible");
            }
        }
        [Test, Order(53)]
        public void Decorator_Create_UpdateProduct()
        {
            try
            {
                Info.Dist_SharedProductNumber = "DECO" + Config.TestIterationName_number;
                Info.Dist_ShareProductName = Info.NewProduct_Name + " Decorator";
                IWebDriver driver = DriverAccess.Shared();
                test = extent.StartTest("Decorator_Create_UpdateProduct");
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);
                State.GotoEITHome();
                State.GotoSupplierHomeByID(Credentials.Decorator_AsiId);
                State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                ProductUtility.AddNewProductInItDialog(Info.Dist_ShareProductName, Info.NewProduct_Description, Info.NewProduct_Type_Text);
                //SeleniumExtension.click(By.XPath(TestElements.AddProduct_Apply_btn_Xpath));
                ProductUtility.FillProductBasicInfo(Info.NewProduct_Summary, Info.NewProduct_Catogories, Info.NewProduct_Keywords, Info.Dist_SharedProductNumber);
                // ProductID = driver.FindElement(By.Id("externalProductId")).GetAttribute("value");
                ProductUtility.SelectImageForNewProduct();
                ProductUtility.SetProductColors();
                PriceObjectSPG price = new PriceObjectSPG();
                ProductUtility.SetPriceByObject(price.ProductPrice);
                ProductUtility.MakeActive();
                if (ProductUtility.ValidateActiveProcessonPriceTab())
                {
                    Logger.Log("new product Add and mark it as Active - Pass", LogStatus.Pass, test);
                    State.GotoSupplierHome();
                    State.TabSwitchByName(ExtrenalUserTabItems.Manage_Product);
                    ProductSearchUtility.SearchProductByName("\"" + Info.Dist_ShareProductName + "\"");
                    ProductSearchUtility.ActionOnSearchTile(ProductSearchUtility.findResultFromSearchResult(true), "Edit");
                    Wait.WaitUntilLoadingInVisible(By.ClassName(CommonElements.loadingBackDrop_div_Class));
                    ProductUtility.EditBasicInfo(Info.NewProduct_Update_Summary + Config.TestIterationName_number, Info.NewProduct_Update_Keywords);
                    State.TabSwitchByName(NewProductTabItems.Pricing);
                    ProductUtility.MakeActive();
                    if (ProductUtility.ValidateActiveProcessonPriceTab())
                    {
                        Logger.Log("Update Active product information - Pass", LogStatus.Pass, test);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        //[Test, Order(27)]
        public void MyApplicationAccess()
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                UserUtility.LoginToVelocity(Credentials.VelocityExternal_User);
                // UserUtility.GotoApplicationByLinkText("ESP Updates");
                // Wait.WaitUntilElementDisply(By.Id(""));
                UserUtility.GotoApplicationByLinkText("Connect", By.Id("container"));
                driver.Url = Links.URL_VelocityBase + Links.Route_Velocity_External_User_Home;
                Wait.InSeconds(2);
                if (SeleniumExtension.GetLocalPath().StartsWith("/login"))
                {
                    Wait.WaitUntilElementDisply(By.Id("txtAsiNum"));
                    UserUtility.NullUserSession();
                    UserUtility.LoginToVelocity(Credentials.VelocityExternal_User);
                }

            }

            catch (Exception)
            {

                throw;
            }
        }

        #region One time Stuff
        [OneTimeSetUp]
        public void StartUp()
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                InIt init = new InIt();
                init.SetInformation();
                driver.Url = Links.URL_VelocityBase + Links.Route_Velocity_Login;
                driver.Manage().Window.Maximize();


                // LoginUser(driver);
            }
            catch (Exception ex)
            {
                Logger.Log("Login Failed..", ex);
            }

        }
        [SetUp]
        public void BeforeEveryTest()
        {
            try
            {
                Wait.WaitUntilLoadingInVisible();
                Wait.InSeconds(1);
                SeleniumExtension.AcceptIfAlertPresent();

                try
                {
                    State.RemoveDialogsFromPreviousTests();

                }
                catch (Exception)
                {
                }
                State.RemoveDialogsbuldPublish();
                Wait.WaitUntilLoadingInVisible();

            }
            catch (Exception)
            {

                throw;
            }
        }
        [TearDown]
        public void AddScreenShot()
        {
            test.Log(LogStatus.Info, "Screencast below: " + test.AddScreenCapture(this.TakeScreenShot()));
        }
        public string TakeScreenShot()
        {
            try
            {

                IWebDriver driver = DriverAccess.Shared();
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                string Path = Config.DefaultProjectPath + @"Report\" + test.GetTest().Name + ".png";
                ss.SaveAsFile(Path, System.Drawing.Imaging.ImageFormat.Png);
                return Path;
            }
            catch (Exception ex)
            {

                return "";
            }
        }
        [OneTimeTearDown]
        public void TearDownApplication()
        {

        }
        #endregion



    }
    #region Extent Report

    internal class ExtentManager
    {
        private static readonly ExtentReports _instance =
            new ExtentReports(Config.DefaultProjectPath + @"Report\Report.html", DisplayOrder.OldestFirst);

        static ExtentManager() { }

        private ExtentManager() { }

        public static ExtentReports Instance
        {
            get
            {
                return _instance;
            }
        }
    }

    public abstract class ExtentBase
    {
        protected ExtentReports extent;
        protected ExtentTest test;

        [OneTimeSetUp]
        public void FixtureInit()
        {
            extent = ExtentManager.Instance;
            extent.LoadConfig(@"\extent-config.xml");
        }

        [TearDown]
        public void TearDown()
        {
            var status = NUnit.Framework.TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(NUnit.Framework.TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", NUnit.Framework.TestContext.CurrentContext.Result.StackTrace);
            LogStatus logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = LogStatus.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = LogStatus.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = LogStatus.Skip;
                    break;
                default:
                    logstatus = LogStatus.Pass;
                    break;
            }

            test.Log(logstatus, "Test ended with " + logstatus + stacktrace);

            extent.EndTest(test);
            extent.Flush();
        }
    }
    #endregion
}
