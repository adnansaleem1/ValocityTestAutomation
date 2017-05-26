using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityTestCases.BL;
using VelocityTestCases.Entity;
using VelocityTestCases.References;

namespace VelocityTestCases.Utility
{
    class ProductUtility
    {

        public static void SetProductColors()
        {
            IWebDriver driver = DriverAccess.Shared();
            SeleniumExtension.click(By.XPath(TestElements.AddProduct_Attributestab_a_Xapth));
            Wait.WaitUntilLoadingInVisible(By.ClassName(CommonElements.loadingBackDrop_div_Class));
            Wait.WaitUntilElementDisply(By.Id(TestElements.AddProduct_Shapes_input_id));
            //   SeleniumExtension.click(By.CssSelector(TestElements.AddProduct_BlueColor_btn_Attribute));
            for (int Count = 0; Count <= 5; Count++)
            {
                try
                {
                    driver.FindElements(By.CssSelector(TestElements.AddProduct_BlueColor_btn_Attribute))[5].Click();
                    break;
                }
                catch (Exception ex)
                {
                }
            }
            SizeObject objSize = new SizeObject();
            ProductUtility.SetSizeForProduct(objSize.size);
            ProductUtility.SaveTabState();

        }

        public static void SelectImageForNewProduct()
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                //Wait.WaitUntilElementDisply(By.CssSelector(TestElements.AddProduct_Images_tab_Attribute));
                State.TabSwitchByName(NewProductTabItems.Images);
                SeleniumExtension.ScrolElementToDisplay(0, 0);


                if (!Config.UploadImageForProduct)
                {
                    SeleniumExtension.click(By.XPath(TestElements.AddProduct_MediaLibrary_btn_Xapth));
                    Wait.WaitUntilElementDisply(By.ClassName(TestElements.AddProduct_MediaLibraryPopupMain_div_class));
                    Wait.InSeconds(1);
                    IWebElement modal = driver.FindElement(By.ClassName("mediaLibraryModal"));
                    IWebElement ParentCheckboxDiv = modal.FindElement(By.ClassName("large-scroll-y"));
                    IReadOnlyList<IWebElement> childs = ParentCheckboxDiv.FindElements(By.ClassName("mediaContents"));
                    for (int Count = 0; Count <= 3; Count++)
                    {
                        try
                        {
                            childs[Count].FindElement(By.TagName("input")).Click();

                        }
                        catch (Exception ex) { }
                    }
                }
                else
                {
                    ProductUtility.UploadFile();

                }

                SeleniumExtension.click(By.CssSelector(TestElements.AddProduct_UseselctedImages_btn_Attribute));
                // Wait.WaitUntilLoadingInVisible(By.ClassName(CommonElements.loadingBackDrop_div_Class));
                Wait.InSeconds(3);
                ProductUtility.SaveTabState();
            }
            catch (Exception ex)
            {
                Logger.Log("Set image for new Product - Fail", ex);
            }
        }
        public static void UploadFile()
        {
            IWebDriver driver = DriverAccess.Shared();
            foreach (string image in Info.ImageFileNameList)
            {
                String script = "document.getElementById('image-to-upload').value='" + Config.ImagesFolderPath + image + "';document.getElementById('btnUploadProductImage').click();";
                ((IJavaScriptExecutor)driver).ExecuteScript(script);
                //Wait.WaitUntilLoadingInVisible(By.ClassName(CommonElements.loadingImageUpload));

            }


        }
        internal static void AddNewProductInItDialog(string name, string description, string type)
        {
            Wait.WaitUntilElementPopup(By.CssSelector(TestElements.AddProduct_AddNewProduct_button_Attribute));
            Wait.InSeconds(1);
            SeleniumExtension.click(By.CssSelector(TestElements.AddProduct_AddNewProduct_button_Attribute));
            SeleniumExtension.AddTextToField(By.Id(TestElements.AddProduct_AddName_Field_id), name);
            SeleniumExtension.AddTextToField(By.Id(TestElements.AddProduct_Description_Field_id), description);
            SeleniumExtension.SelectByText(By.XPath(TestElements.AddProduct_Type_Dropdown_Xpath), type);
            //  SeleniumExtension.SelectByValue(By.XPath(TestElements.AddProduct_Type_Dropdown_Xpath), Info.NewProduct_Type_Value);

            Wait.InSeconds(1);
            SeleniumExtension.click(By.XPath(TestElements.AddProduct_Apply_btn_Xpath));
        }
        internal static void FillProductBasicInfo(string Summary, IEnumerable<string> cat, IEnumerable<string> Keywords)
        {
            IWebDriver driver = DriverAccess.Shared();
            Wait.WaitUntilElementDisply(By.Id(TestElements.AddProduct_DetailPage_Element_id));
            Wait.WaitUntilElementDisply(By.Id(TestElements.AddProduct_Summary_Field_id));
            Wait.WaitUntilLoadingInVisible(By.ClassName(CommonElements.loadingBackDrop_div_Class));
            Wait.InSeconds(1);
            SeleniumExtension.AddTextToField(By.Id(TestElements.AddProduct_Summary_Field_id), Summary);
            // SeleniumExtension.clickIfClickable(By.XPath("//*[@id=\"product-info-view\"]/div[2]/div[1]/div[1]/div[2]/div[4]/div/label/button"));
            //  Wait.InSeceonds(1);
            //  SeleniumExtension.clickIfClickable(By.XPath("//*[@id=\"modalDeleteAllKeywords\"]/div[3]/a[2]"));
            SeleniumExtension.click(By.XPath(TestElements.AddProduct_Catagory_btn_Xpath));
            Wait.InSeconds(1);
            SeleniumExtension.TryToClick(By.CssSelector(TestElements.AddProduct_CatogoriesAll_Btn_Attribute));
            Wait.InSeconds(1);
            foreach (string Item in cat)
            {
                SeleniumExtension.AddTextToField(By.XPath(TestElements.AddProduct_CatogriesSearch_Field_Xpath), Item);
                Wait.InSeconds(1);
                IWebElement ParentCheckboxDiv = driver.FindElement(By.CssSelector(TestElements.AddProduct_CatogoryPatent_Div_Attribute));
                IReadOnlyList<IWebElement> childs = ParentCheckboxDiv.FindElements(By.XPath(".//*"));
                foreach (IWebElement ele in childs)
                {
                    if (ele.FindElement(By.CssSelector(TestElements.AddProduct_CatogriesName_Span_Attribute)).Text == Item)
                    {
                        ele.FindElement(By.TagName("input")).Click();

                        break;
                    }
                }

            }

            SeleniumExtension.click(By.CssSelector(TestElements.AddProduct_CatogriesApply_Button_attribute));
            string keywordsCommaspareted = "";
            foreach (string word in Keywords)
            {
                keywordsCommaspareted += word + ",";
            }
            if (keywordsCommaspareted.Length != 0)
            {
                //  keywordsCommaspareted = keywordsCommaspareted.Remove(keywordsCommaspareted.Length - 1);
                SeleniumExtension.AddTextToField(By.Id("token-input-productKeywords"), keywordsCommaspareted);
            }
            if (UserUtility.LogedInUser.Type == VelocityUserType.Eit)
            {
                SeleniumExtension.ScrolElementToDisplayByElement(By.Id("token-input-adKeywords"));
                SeleniumExtension.AddTextToField(By.Id("token-input-adKeywords"), Info.Addvertising_Keywords);
                SeleniumExtension.ScrolElementToDisplayByElement(By.Id("token-input-seoKeywords"));

                SeleniumExtension.AddTextToField(By.Id("token-input-seoKeywords"), Info.hidden_Keywords);
                Wait.InSeconds(1);
            }
            ProductUtility.SaveTabState();
        }
        internal static void SaveTabState()
        {

            IWebDriver driver = DriverAccess.Shared();
            IReadOnlyList<IWebElement> childs = driver.FindElement(By.Id("btnSection")).FindElements(By.ClassName("viewControls"));
            foreach (IWebElement ele in childs)
            {
                if (ele.Displayed)
                {
                    IList<IWebElement> buttons = ele.FindElements(By.TagName("button"));
                    IWebElement savebtn = null;
                    foreach (IWebElement btn in buttons)
                    {
                        if (Common.Compare(btn.Text, "Save"))
                        {
                            savebtn = btn;
                        }
                    }
                    // IWebElement saveBtn= ele.FindElement(By.CssSelector("div[data-bind='visible: !isBulkOperation()']")).FindElements(By.TagName("button"))[0];
                    if (savebtn.Enabled)
                    {
                        savebtn.Click();
                        Wait.UntilSucessMessageShow();
                        Wait.WaitUntilLoadingInVisible();
                        break;
                    }
                    else
                    {
                        throw new TestCaseException("Unable to save tab State there was no changes made");
                    }
                }
            }

        }
        internal static void MakeActive()
        {
            IWebDriver driver = DriverAccess.Shared();
            IReadOnlyList<IWebElement> childs = driver.FindElement(By.Id("btnSection")).FindElements(By.ClassName("viewControls"));
            foreach (IWebElement ele in childs)
            {
                if (ele.Displayed)
                {
                    IList<IWebElement> buttons = ele.FindElements(By.TagName("button"));
                    IWebElement Activebtn = null;
                    foreach (IWebElement btn in buttons)
                    {
                        if (Common.Compare(btn.Text, "Make Active"))
                        {
                            Activebtn = btn;
                        }
                    }
                    if (Activebtn.Enabled)
                    {
                        Activebtn.Click();
                        //Wait.UntilSucessMessageShow();
                        //Wait.WaitUntilLoadingInVisible();
                        try
                        {
                            //Wait.WaitUntilElementDisply(By.ClassName("publishSuccessModalPrice"));
                            //driver.FindElement(By.ClassName("publishSuccessModalPrice")).FindElement(By.Id("effectiveNow")).Click();
                            //Wait.InSeconds(1);
                            //SeleniumExtension.findButtonByText(driver.FindElement(By.ClassName("effectiveDateModalPrice")).FindElements(By.TagName("button")), "OK").Click();
                        }
                        catch (Exception) { }

                        Wait.WaitUntilLoadingInVisible();
                        break;
                    }
                    else
                    {
                        throw new TestCaseException("Unable to Active Product there was no changes made");
                    }
                }
            }

        }
        internal static void MakeActiveImages()
        {
            IWebDriver driver = DriverAccess.Shared();
            IReadOnlyList<IWebElement> childs = driver.FindElement(By.Id("btnSection")).FindElements(By.ClassName("viewControls"));
            foreach (IWebElement ele in childs)
            {
                if (ele.Displayed)
                {
                    IList<IWebElement> buttons = ele.FindElements(By.TagName("button"));
                    IWebElement Activebtn = null;
                    foreach (IWebElement btn in buttons)
                    {
                        if (Common.Compare(btn.Text, "Make Active"))
                        {
                            Activebtn = btn;
                        }
                    }
                    if (Activebtn.Enabled)
                    {
                        Activebtn.Click();
                        //Wait.UntilSucessMessageShow();
                        //Wait.WaitUntilLoadingInVisible();
                        try
                        {
                            Wait.WaitUntilElementDisply(By.ClassName("effectiveDateModalPrice"));
                            driver.FindElement(By.ClassName("effectiveDateModalPrice")).FindElement(By.Id("effectiveNow")).Click();
                            Wait.InSeconds(1);
                            SeleniumExtension.findButtonByText(driver.FindElement(By.ClassName("effectiveDateModalPrice")).FindElements(By.TagName("button")), "OK").Click();
                        }
                        catch (Exception) { }

                        Wait.WaitUntilLoadingInVisible();
                        break;
                    }
                    else
                    {
                        throw new TestCaseException("Unable to Active Product there was no changes made");
                    }
                }
            }

        }
        #region Set Price By Object Logic
        internal static void SetPriceByObject(PriceFill PriceObj)
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                SeleniumExtension.click(By.CssSelector(TestElements.AddProduct_Pricingtab_a_attribute));
                Wait.WaitUntilElementDisply(By.ClassName("priceGrid"));
                Wait.WaitUntilElementDisply(By.Id("rbMpConfigGrid"));
                Wait.InSeconds(1);
                if (Info.PriceType.ToLower() != "List Price".ToLower())
                {
                    ProductUtility.SelectPriceType(PriceObj.PriceType);
                }
                if (PriceObj.GridType == PricegridType.MultipleGrid)
                {
                    SeleniumExtension.ScrolElementToDisplay(0, 0);
                    SeleniumExtension.click(By.Id("rbMpConfigGrid"));
                    Wait.WaitUntilElementDisply(By.Id("multipriceGridSetupModal"));
                    Wait.InSeconds(1);
                    ProductUtility.SetConfigrationForMultipleGrid(PriceObj.CritariaForMutiple);
                    ProductUtility.SetPriceGridCritaria(PriceObj.GetPriceGridList());
                    ProductUtility.SetPriceForEachGrid(PriceObj.GetPriceGridList());
                }
                else
                {
                    //General.SetPriceGridCritaria(PriceObj.GetPriceGridList());  
                    SeleniumExtension.ScrolElementToDisplay(0, 0);
                    ProductUtility.SetPriceForEachGrid(PriceObj.GetPriceGridList());
                }
                Wait.WaitUntilLoadingInVisible();
                SaveTabState();
            }
            catch (Exception ex)
            {
                Logger.Log("Select Price for Product - Fail", ex);
                throw;
            }
        }

        internal static void SetPriceForEachGrid(IList<PriceGridFill> list)
        {
            IWebDriver driver = DriverAccess.Shared();
            IReadOnlyList<IWebElement> parent;
            IReadOnlyList<IWebElement> child;
            Itemsprice itemsprice;
            parent = driver.FindElements(By.ClassName("priceGrid"));
            for (var gcount = 0; gcount < list.Count; gcount++)
            {

                ProductUtility.SetPriceInGrid(list[gcount], parent[gcount]);


            }
        }
        internal static void SetPriceInGrid(PriceGridFill GridInfo, IWebElement GridParentElement)
        {
            IWebDriver driver = DriverAccess.Shared();
            SeleniumExtension.ScrolElementToDisplayByElement(GridParentElement);
            if (GridInfo.PriceGridName != null || GridInfo.PriceGridName != "")
            {
                IWebElement gridNameElement = GridParentElement.FindElement(By.ClassName("accordion-heading")).FindElement(By.ClassName("desc")).FindElement(By.TagName("input"));
                gridNameElement.SendKeys(GridInfo.PriceGridName);
            }
            //SeleniumExtension.AddTextToField(, GridInfo.PriceGridName);
            try
            {
                GridParentElement.FindElement(By.ClassName("collapsed")).Click();
                Wait.InSeconds(1);
            }
            catch (Exception) { }


            IWebElement ParentCheckboxDiv = GridParentElement.FindElement(By.ClassName(TestElements.AddProduct_PriceGrid_div_class));
            IReadOnlyList<IWebElement> childs = ParentCheckboxDiv.FindElements(By.ClassName("dataCol"));
            for (var count = 0; count < GridInfo.PricesList.Count; count++)
            {
                IWebElement Col = childs[count];
                Itemsprice pri = GridInfo.PricesList[count];
                IReadOnlyList<IWebElement> Rows = Col.FindElements(By.ClassName("dataRow"));
                Rows[1].FindElement(By.TagName("input")).SendKeys(pri.Quantity.ToString());
                if (Info.PriceType == "Both")
                {
                    Rows[2].FindElement(By.TagName("input")).SendKeys(pri.ListPrice.ToString());
                    Rows[3].FindElement(By.TagName("input")).SendKeys(pri.NetCode.ToString());
                }
                else if (Info.PriceType == "Net Cost")
                {
                    //  Rows[2].FindElement(By.TagName("input")).SendKeys(pri.ListPrice.ToString());
                    Rows[3].FindElement(By.TagName("input")).SendKeys(pri.NetCode.ToString());
                    SeleniumExtension.SelectByText(Rows[4].FindElement(By.TagName("select")), Info.priceCode);
                }
                else if (Info.PriceType == "List Price")
                {
                    Rows[2].FindElement(By.TagName("input")).SendKeys(pri.ListPrice.ToString());
                    // Rows[3].FindElement(By.TagName("input")).SendKeys(pri.NetCode.ToString());
                    SeleniumExtension.SelectByText(Rows[4].FindElement(By.TagName("select")), Info.priceCode);
                }
                Wait.InSeconds(2);

            }

        }
        private static void SetPriceGridCritaria(IList<PriceGridFill> list)
        {
            IWebDriver driver = DriverAccess.Shared();
            IWebElement parent;
            IReadOnlyList<IWebElement> child;
            for (var gcount = 0; gcount < list.Count; gcount++)
            {
                //Each Price Grid
                foreach (PriceGridCritaria critaria in list[gcount].CritariaList)
                {
                    //Each Critaria For one Grid
                    parent = driver.FindElement(By.XPath(critaria.ParentElementXpath));
                    child = parent.FindElements(By.TagName("div"));
                    foreach (IWebElement ele in child)
                    {
                        //Find Critaria in Element List
                        if (ele.FindElement(By.TagName("span")).Text.ToLower() == critaria.CriatriaName.ToLower())
                        {
                            ele.FindElement(By.TagName("input")).Click();
                            Wait.InSeconds(1);
                        }
                    }
                }

                if (gcount == list.Count - 1)
                {
                    SeleniumExtension.click(By.XPath("//*[@id=\"multipriceGridSetupModal\"]/div[3]/button[1]"));
                    Wait.UntilSucessMessageShow();
                    Wait.UntilModalisVisible(By.CssSelector("button[data-bind='click: continuePriceConfiguration ']"));
                }
                else
                {

                    SeleniumExtension.click(By.XPath("//*[@id=\"multipriceGridSetupModal\"]/div[3]/button[2]"));
                    Wait.UntilSucessMessageShow();
                }
            }

        }
        private static void SetConfigrationForMultipleGrid(MultiplePriceConfigrationCritaria multiplePriceConfigrationCritaria)
        {
            IWebDriver driver = DriverAccess.Shared();
            IWebElement Parent = driver.FindElement(By.CssSelector("div[data-bind='foreach: availCriteriaTypes']"));
            IReadOnlyList<IWebElement> childs = Parent.FindElements(By.ClassName("span4"));
            bool critariaFind = false;
            String CritariaName;
            foreach (IWebElement ele in childs)
            {
                CritariaName = ele.FindElement(By.TagName("span")).Text;
                if (ele.FindElement(By.TagName("input")).Enabled)
                {
                    if (CritariaName.Contains(multiplePriceConfigrationCritaria.CritariaA) || CritariaName.Contains(multiplePriceConfigrationCritaria.CritariaB))
                    {
                        ele.FindElement(By.TagName("input")).Click();
                        critariaFind = true;
                    }
                }
            }
            if (critariaFind == false)
            {
                throw new Exception("No Given Critaria found");
            }
            SeleniumExtension.click(By.XPath("//*[@id=\"multipriceGridSetupModal\"]/div[3]/button"));
            Wait.WaitUntilElementDisply(By.Id("mpConfigStep2"));

        }
        public static void SelectPriceType(PriceTypesForItem a)
        {
            IWebDriver driver = DriverAccess.Shared();
            if (a == PriceTypesForItem.Both)
                SeleniumExtension.SelectByText(By.XPath(TestElements.AddProduct_priceType_Select_Xapth), "Both");
            else if (a == PriceTypesForItem.Net_Cost)
            {
                SeleniumExtension.SelectByText(By.XPath(TestElements.AddProduct_priceType_Select_Xapth), "Net Cost");
            }
            Wait.InSeconds(1);
            SeleniumExtension.click(By.CssSelector(TestElements.AddProduct_confirmPriceTypeChange_a_attribute));
        }
        #endregion

        internal static void SetSizeForProduct(Size size)
        {
            IWebDriver driver = DriverAccess.Shared();
            SeleniumExtension.ScrolElementToDisplay(200, 200);

            SeleniumExtension.click(By.XPath("//*[@id=\"product-attributes-view\"]/div[1]/div[1]/div[3]/div[2]/div[1]/button"));
            Wait.WaitUntilElementDisply(By.Id("sizesTypesModal"));
            SeleniumExtension.click(By.Id("btnViewAllSizeTypes"));
            Wait.InSeconds(1);
            IReadOnlyList<IWebElement> sizeTypes = driver.FindElement(By.Id("sizesTypesModal")).FindElements(By.TagName("a"));
            foreach (IWebElement ele in sizeTypes)
            {
                if (Common.Compare(ele.Text, size.SizeType))
                {

                    ele.Click();
                    Wait.InSeconds(2);
                    break;
                }

            }
            IReadOnlyList<IWebElement> sizeList = driver.FindElement(By.XPath(size.Parent_Container_Xapth)).FindElements(By.ClassName("control-group"));

            foreach (string name in size.SelectionList)
                foreach (IWebElement ele in sizeList)
                {
                    if (Common.Compare(ele.FindElement(By.TagName("span")).Text, name))
                    {
                        ele.FindElement(By.TagName("input")).Click();
                        break;
                    }
                }


        }
        internal static void DeleteImageForProduct()
        {
            IWebDriver driver = DriverAccess.Shared();
            Wait.WaitUntilElementDisply(By.CssSelector("a[data-bind='click:$root.removeMedia']"), 10);
            SeleniumExtension.click(By.CssSelector("a[data-bind='click:$root.removeMedia']"));
            Wait.InSeconds(1);
        }
        internal static void EditBasicInfo(string Summary, IEnumerable<string> Keywords)
        {
            IWebDriver driver = DriverAccess.Shared();
            Wait.WaitUntilElementDisply(By.Id(TestElements.AddProduct_DetailPage_Element_id));
            Wait.WaitUntilElementDisply(By.Id(TestElements.AddProduct_Summary_Field_id));
            Wait.WaitUntilLoadingInVisible(By.ClassName(CommonElements.loadingBackDrop_div_Class));
            Wait.InSeconds(1);
            SeleniumExtension.AddTextToField(By.Id(TestElements.AddProduct_Summary_Field_id), Summary);
            ProductUtility.SaveTabState();
        }


        internal static IWebElement SearchProductInWebSite(string SearchTerm, string SupplierID = "", string ProductNumber = "", string ProductName = "")
        {
            IWebDriver driver = DriverAccess.Shared();
            SeleniumExtension.AddTextToField(By.Id("ctl03_ctl00_txtSearchTerms"), SearchTerm);
            SeleniumExtension.click(By.LinkText("Go"));
            Wait.WaitUntilElementDisply(By.LinkText("Go"));
            IWebElement SupplyProduct = null;
            do
            {
                IReadOnlyList<IWebElement> pList = driver.FindElements(By.ClassName("prod-tile"));

                foreach (IWebElement ele in pList)
                {
                    try
                    {
                        string eleAsiNumber = ele.FindElement(By.ClassName("asiNum")).Text;
                        string eleProductNumber = ele.FindElement(By.Id("btnProductDetail")).Text.Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries)[0].ToString();
                        string eleProductName = ele.FindElement(By.Id("btnProductDetail")).Text.Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries)[1].ToString();

                        if ((SupplierID == "" || eleAsiNumber.Split('/')[1] == SupplierID) && (ProductNumber == "" || eleProductNumber == ProductNumber) && (ProductName == "" || eleProductName == ProductName))
                        {
                            SupplyProduct = ele;
                            break;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            } while (ProductUtility.SharedProdutNextPage());



            return SupplyProduct;
        }

        private static bool SharedProdutNextPage()
        {
            IWebDriver driver = DriverAccess.Shared();
            try
            {

                driver.FindElement(By.ClassName("endecaPageNext")).Click();
                Wait.WaitUntilElementDisply(By.Id("Summary"));
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }



        internal static void FillProductBasicInfo(string Summary, IEnumerable<string> cat, IEnumerable<string> Keywords, string ProductNumber)
        {
            IWebDriver driver = DriverAccess.Shared();
            Wait.WaitUntilElementDisply(By.Id(TestElements.AddProduct_DetailPage_Element_id));
            Wait.WaitUntilElementDisply(By.Id(TestElements.AddProduct_Summary_Field_id));
            Wait.WaitUntilLoadingInVisible(By.ClassName(CommonElements.loadingBackDrop_div_Class));
            Wait.InSeconds(1);
            SeleniumExtension.AddTextToField(By.Id(TestElements.AddProduct_Summary_Field_id), Summary);
            SeleniumExtension.AddTextToField(By.Id("productNum"), ProductNumber);

            // SeleniumExtension.clickIfClickable(By.XPath("//*[@id=\"product-info-view\"]/div[2]/div[1]/div[1]/div[2]/div[4]/div/label/button"));
            //  Wait.InSeceonds(1);
            //  SeleniumExtension.clickIfClickable(By.XPath("//*[@id=\"modalDeleteAllKeywords\"]/div[3]/a[2]"));
            SeleniumExtension.click(By.XPath(TestElements.AddProduct_Catagory_btn_Xpath));
            Wait.InSeconds(1);
            SeleniumExtension.TryToClick(By.CssSelector(TestElements.AddProduct_CatogoriesAll_Btn_Attribute));
            Wait.InSeconds(1);

            foreach (string Item in cat)
            {
                SeleniumExtension.AddTextToField(By.XPath(TestElements.AddProduct_CatogriesSearch_Field_Xpath), Item);
                Wait.InSeconds(1);
                IWebElement ParentCheckboxDiv = driver.FindElement(By.CssSelector(TestElements.AddProduct_CatogoryPatent_Div_Attribute));
                IReadOnlyList<IWebElement> childs = ParentCheckboxDiv.FindElements(By.XPath(".//*"));
                foreach (IWebElement ele in childs)
                {
                    if (ele.FindElement(By.CssSelector(TestElements.AddProduct_CatogriesName_Span_Attribute)).Text == Item)
                    {
                        ele.FindElement(By.TagName("input")).Click();

                        break;
                    }
                }

            }

            SeleniumExtension.click(By.CssSelector(TestElements.AddProduct_CatogriesApply_Button_attribute));
            string keywordsCommaspareted = "";
            foreach (string word in Keywords)
            {
                keywordsCommaspareted += word + ",";
            }
            if (keywordsCommaspareted.Length != 0)
            {
                //  keywordsCommaspareted = keywordsCommaspareted.Remove(keywordsCommaspareted.Length - 1);
                SeleniumExtension.AddTextToField(By.Id("token-input-productKeywords"), keywordsCommaspareted);
            }
            if (UserUtility.LogedInUser.Type == VelocityUserType.Eit)
            {
                SeleniumExtension.ScrolElementToDisplayByElement(By.Id("token-input-adKeywords"));
                SeleniumExtension.AddTextToField(By.Id("token-input-adKeywords"), Info.Addvertising_Keywords);
                SeleniumExtension.ScrolElementToDisplayByElement(By.Id("token-input-seoKeywords"));

                SeleniumExtension.AddTextToField(By.Id("token-input-seoKeywords"), Info.hidden_Keywords);
                Wait.InSeconds(1);
            }
            ProductUtility.SaveTabState();
        }

        internal static bool ValidateActiveProcessonPriceTab()
        {
            IWebDriver driver = DriverAccess.Shared();
            Wait.WaitUntilElementDisply(By.ClassName("publishSuccessModalPrice"), 10);
            if (driver.FindElement(By.ClassName("publishSuccessModalPrice")).FindElement(By.TagName("h3")).Text == "Success!")
            {
                // Logger.Log("new product Add and mark it as Active - Pass");
                SeleniumExtension.click(By.XPath(TestElements.AddProduct_SucessOk_Btn_Xapth));
                return true;
                //  TestCasesCommon.LogoutUser();
            }
            throw new NotImplementedException();
        }
          internal static bool ValidateActiveProcess(string model)
        {
            IWebDriver driver = DriverAccess.Shared();
            Wait.WaitUntilElementDisply(By.ClassName(model), 10);
            if (driver.FindElement(By.ClassName(model)).FindElement(By.TagName("h3")).Text == "Success!")
            {
                // Logger.Log("new product Add and mark it as Active - Pass");

                driver.FindElement(By.ClassName(model)).FindElement(By.ClassName("btn-default")).Click();
              //  SeleniumExtension.click(By.XPath(TestElements.AddProduct_SucessOk_Btn_Xapth));
                return true;
                //  TestCasesCommon.LogoutUser();
            }
            throw new NotImplementedException();
        }



          internal static void ActiveProductModal(string p)
          {
              IWebDriver driver = DriverAccess.Shared();
              Wait.WaitUntilElementDisply(By.ClassName(p));
              driver.FindElement(By.ClassName(p)).FindElement(By.Id("effectiveNow")).Click();
              Wait.InSeconds(1);
              SeleniumExtension.findButtonByText(driver.FindElement(By.ClassName(p)).FindElements(By.TagName("button")), "OK").Click();
          }

          internal static void MakeActiveRestricted()
          {
              IWebDriver driver = DriverAccess.Shared();
              IReadOnlyList<IWebElement> childs = driver.FindElement(By.Id("btnSection")).FindElements(By.ClassName("viewControls"));
              foreach (IWebElement ele in childs)
              {
                  if (ele.Displayed)
                  {
                      IList<IWebElement> buttons = ele.FindElements(By.TagName("button"));
                      IWebElement Activebtn = null;
                      foreach (IWebElement btn in buttons)
                      {
                          if (Common.Compare(btn.Text, "Make Active"))
                          {
                              Activebtn = btn;
                          }
                      }
                      if (Activebtn.Enabled)
                      {
                          Activebtn.Click();
                          //Wait.UntilSucessMessageShow();
                         Wait.WaitUntilLoadingInVisible();
                          break;
                      }
                      else
                      {
                          throw new TestCaseException("Unable to Active Product there was no changes made");
                      }
                  }
              }
          }
    }
}
