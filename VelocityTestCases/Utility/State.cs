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

    class State
    {
        public static void TabSwitchByName(ExtrenalUserTabItems tabname)
        {
            if (tabname == ExtrenalUserTabItems.Media_Library)
            {
                Wait.WaitUntilElementClickAble(By.LinkText("Media Library"));
                SeleniumExtension.click(By.LinkText("Media Library"));
                Wait.WaitUntilElementPopup(By.Id("txtSearchMediaLibrary"));
                Wait.InSeconds(1);
            }
            else if (tabname == ExtrenalUserTabItems.Supplier_info)
            {
                Wait.WaitUntilElementClickAble(By.LinkText("Supplier Info"));
                SeleniumExtension.click(By.LinkText("Supplier Info"));
                Wait.WaitUntilElementPopup(By.Id("preferences-view"));
                Wait.InSeconds(1);
            }
            else if (tabname == ExtrenalUserTabItems.catalog_Compliances)
            {
                Wait.WaitUntilElementClickAble(By.LinkText("Catalog & Compliance"));
                SeleniumExtension.click(By.LinkText("Catalog & Compliance"));
                Wait.WaitUntilElementPopup(By.Id("catalogName"));
                Wait.InSeconds(1);
            }
            else if (tabname == ExtrenalUserTabItems.Manage_Product)
            {
                Wait.WaitUntilElementClickAble(By.LinkText("Manage Products"));
                SeleniumExtension.click(By.LinkText("Manage Products"));
                Wait.WaitUntilElementPopup(By.Id("supplierSearchTextBox"));
                Wait.WaitUntilElementDisply(By.Id("txtPageNumber"));
                Wait.InSeconds(1);
            }

        }
        public static void TabSwitchByName(NewProductTabItems tabname)
        {
            if (NewProductTabItems.Basic_Detail == tabname)
            {
                Wait.WaitUntilElementDisply(By.LinkText("Basic Details"));
                SeleniumExtension.click(By.LinkText("Basic Details"));
                Wait.WaitUntilElementDisply(By.Id("productDescription"));
            }
            else if (NewProductTabItems.Attributes == tabname)
            {
                Wait.WaitUntilElementDisply(By.LinkText("Attributes"));
                SeleniumExtension.click(By.LinkText("Attributes"));
                Wait.WaitUntilElementDisply(By.Id("token-input-shapesInput"));
            }
            else if (NewProductTabItems.Impringting == tabname)
            {

                Wait.WaitUntilElementDisply(By.LinkText("Imprinting"));
                SeleniumExtension.click(By.LinkText("Imprinting"));
                Wait.WaitUntilElementDisply(By.Id("token-input-imprintColorNames"));
            }
            else if (NewProductTabItems.Pricing == tabname)
            {
                Wait.WaitUntilElementDisply(By.LinkText("Pricing"));
                SeleniumExtension.click(By.LinkText("Pricing"));
                Wait.WaitUntilElementDisply(By.Id("priceGridTab"));
            }
            else if (NewProductTabItems.Images == tabname)
            {

                Wait.WaitUntilElementDisply(By.LinkText("Images"));
                SeleniumExtension.click(By.LinkText("Images"));
                Wait.WaitUntilElementDisply(By.Id("btnUploadProductImage"));
            }
            else if (NewProductTabItems.SKU_Inventory == tabname)
            {
                Wait.WaitUntilElementDisply(By.LinkText("SKU & Inventory"));
                SeleniumExtension.click(By.LinkText("SKU & Inventory"));
                Wait.WaitUntilElementDisply(By.Id("product-sku-view"));
            }
            else if (NewProductTabItems.Availability == tabname)
            {

            }
            else if (NewProductTabItems.Summary == tabname)
            {

            }
        }

        internal static void TabSwitchByName(EITUserTabItems eITUserTabItem)
        {
            if (EITUserTabItems.EIT_Reports == eITUserTabItem)
            {
                Wait.WaitUntilElementClickAble(By.LinkText("EIT Reports"));
                SeleniumExtension.click(By.LinkText("EIT Reports"));
                Wait.WaitUntilElementPopup(By.Id("sectionSelection"));
            }
            else if (EITUserTabItems.Manage_Products == eITUserTabItem)
            {
                Wait.WaitUntilElementClickAble(By.LinkText("Manage Products"));
                SeleniumExtension.click(By.LinkText("Manage Products"));
                Wait.WaitUntilElementPopup(By.Id("supplierSearchTextBox"));

            }
        }
        internal static string GetUrlStateWithoutBase()
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                string url = driver.Url; // get the current URL (full)
                Uri currentUri = new Uri(url);
                return currentUri.LocalPath;
            }
            catch (Exception)
            {

                return "";
            }
        }

        internal static void GotoSupplierHome()
        {
            IWebDriver driver = DriverAccess.Shared();
            string localPath = State.GetUrlStateWithoutBase().ToString();
            string home = State.GetUrlBase();
            ApplicationState appstate = State.IdentifyApplicationState();
            if (UserUtility.LogedInUser.Type == VelocityUserType.Eit)
            {

                if (appstate == ApplicationState.EIT_User_Home_Page_Tabs)
                {
                    //  driver.Url = home + Links.Velocity_Eit_Home;
                    State.TabSwitchByName(EITUserTabItems.Manage_Products);
                    UserUtility.VelocityLoginAsSupplier(UserUtility.LogedInUser.ASINumber);
                }
                else if (appstate == ApplicationState.ProdcutInfo_Page_Tabs)
                {
                    SeleniumExtension.ScrolElementToDisplayByElement(By.LinkText("Manage My Products"));
                    Wait.WaitUntilElementClickAble(By.LinkText("Manage My Products"));
                    SeleniumExtension.click(By.LinkText("Manage My Products"));
                    Wait.WaitUntilElementDisply(By.Id("supplierSearchTextBox"));
                }
            }
            else if (UserUtility.LogedInUser.Type == VelocityUserType.External)
            {
                if (appstate == ApplicationState.ProdcutInfo_Page_Tabs)
                {
                    SeleniumExtension.ScrolElementToDisplayByElement(By.LinkText("Manage My Products"));
                    Wait.WaitUntilElementClickAble(By.LinkText("Manage My Products"));
                    SeleniumExtension.click(By.LinkText("Manage My Products"));
                    Wait.WaitUntilElementDisply(By.Id("supplierSearchTextBox"));
                }

            }
        }
        public static ApplicationState IdentifyApplicationState()
        {
            IWebDriver driver = DriverAccess.Shared();
            if (SeleniumExtension.ElementExists(By.LinkText("Media Library")) && SeleniumExtension.ElementExists(By.LinkText("Manage Products")))
            {
                return ApplicationState.Supplier_Home_Page_Tabs;
            }
            else if (SeleniumExtension.ElementExists(By.LinkText("SKU & Inventory")) && SeleniumExtension.ElementExists(By.LinkText("Basic Details")))
            {
                return ApplicationState.ProdcutInfo_Page_Tabs;
            }
            else if (SeleniumExtension.ElementExists(By.LinkText("EIT Reports")) && SeleniumExtension.ElementExists(By.LinkText("Manage Products")))
            {
                return ApplicationState.EIT_User_Home_Page_Tabs;
            }
            else
            {

                throw new TestCaseException("Unable to identify Syetem State.!");
            }

        }

        private static string GetUrlBase()
        {
            IWebDriver driver = DriverAccess.Shared();
            string url = driver.Url; // get the current URL (full)
            Uri currentUri = new Uri(url);
            return currentUri.Host.ToString();
        }
        internal static void GotoEITHome()
        {
            IWebDriver driver = DriverAccess.Shared();
            string localPath = State.GetUrlStateWithoutBase().ToString();
            string home = State.GetUrlBase();
            ApplicationState appstate = State.IdentifyApplicationState();
            if (UserUtility.LogedInUser.Type == VelocityUserType.Eit)
            {
                if (SeleniumExtension.ElementDisplay(By.LinkText("EIT Dashboard")))
                {
                    driver.FindElement(By.LinkText("EIT Dashboard")).Click();
                }
                Wait.WaitUntilElementDisply(By.Id("supplierSearchTextBox"));
            }
            else if (UserUtility.LogedInUser.Type == VelocityUserType.External)
            {
                UserUtility.LoginToVelocity(Credentials.VelocityEIT_User);

            }
        }

        internal static void RemoveDialogsFromPreviousTests()
        {
            IWebDriver driver = DriverAccess.Shared();
            if (SeleniumExtension.ElementDisplay(By.ClassName("modal-backdrop")))
            {
                IList<IWebElement> headeres = driver.FindElements(By.ClassName("modal-header"));
                foreach (IWebElement ele in headeres)
                {
                    if (ele.Displayed)
                    {
                        ele.FindElement(By.TagName("button")).Click();
                        Wait.InSeconds(2);
                    }
                }

            }
        }
        
        internal static void RemoveDialogsbuldPublish()
        {
            IWebDriver driver = DriverAccess.Shared();
            if (SeleniumExtension.ElementDisplay(By.Id("bulkPublishModal")))
            {
                driver.FindElement(By.Id("bulkPublishModal")).FindElement(By.LinkText("Cancel")).Click();
                Wait.InSeconds(2);
            }
        }
        

        internal static void GotoSupplierHomeByID(string Id)
        {
            IWebDriver driver = DriverAccess.Shared();
            string localPath = State.GetUrlStateWithoutBase().ToString();
            string home = State.GetUrlBase();
            ApplicationState appstate = State.IdentifyApplicationState();
            if (UserUtility.LogedInUser.Type == VelocityUserType.Eit)
            {



                if (appstate == ApplicationState.EIT_User_Home_Page_Tabs)
                {
                    //  driver.Url = home + Links.Velocity_Eit_Home;
                    State.TabSwitchByName(EITUserTabItems.Manage_Products);
                    UserUtility.VelocityLoginAsSupplier(Id);
                }
                else if (appstate == ApplicationState.ProdcutInfo_Page_Tabs)
                {
                    SeleniumExtension.ScrolElementToDisplayByElement(By.LinkText("Manage My Products"));
                    Wait.WaitUntilElementClickAble(By.LinkText("Manage My Products"));
                    SeleniumExtension.click(By.LinkText("Manage My Products"));
                    Wait.WaitUntilElementDisply(By.Id("supplierSearchTextBox"));
                }
                else if(appstate==ApplicationState.Supplier_Home_Page_Tabs){
                    if (State.SupplierId() != Id) {
                        State.GotoEITHome();
                        State.GotoSupplierHomeByID(Id);
                    }
                }
            }
            else if (UserUtility.LogedInUser.Type == VelocityUserType.External)
            {
                if (appstate == ApplicationState.ProdcutInfo_Page_Tabs)
                {
                    SeleniumExtension.ScrolElementToDisplayByElement(By.LinkText("Manage My Products"));
                    Wait.WaitUntilElementClickAble(By.LinkText("Manage My Products"));
                    SeleniumExtension.click(By.LinkText("Manage My Products"));
                    Wait.WaitUntilElementDisply(By.Id("supplierSearchTextBox"));
                    
                }

            }
        }

        private static string SupplierId()
        {
            IWebDriver driver = DriverAccess.Shared();
            return driver.FindElement(By.Id("asinum")).Text.Split('/')[1];
        }

        internal static void SwitchApplication(Application application)
        {

            IWebDriver driver = DriverAccess.Shared();

            if (application == Application.EspWebSites)
            {
                SeleniumExtension._click(By.LinkText("My Applications"));
                Wait.InSeconds(1);
                SeleniumExtension._click(By.LinkText("ESP Websites Admin"));
                Wait.InSeconds(3);
                if (State.IsLogout()) {
                    UserUtility.LoginToVelocity(UserUtility.LogedInUser);
                    State.GotoSupplierHome();
                    SeleniumExtension.click(By.LinkText("My Applications"));
                    Wait.InSeconds(1);
                    SeleniumExtension.click(By.LinkText("ESP Websites Admin"));
                }
                Wait.WaitUntilElementDisply(By.Id("siteList"), 15);
            }
            else if (application == Application.EspUpdates)
            {

                try
                {
                    SeleniumExtension._click(By.LinkText("My Applications"));
                    Wait.InSeconds(1);
                    SeleniumExtension._click(By.LinkText("ESP Updates"));
                    Wait.WaitUntilElementDisply(By.Id("supplierSearchTextBox"), 15);
                }
                catch (Exception)
                {
                    try
                    {

                        IList<IWebElement> ButtonsList = driver.FindElement(By.ClassName("appToolBarStrip")).FindElements(By.TagName("button"));
                        var App = ButtonsList.First(e => e.FindElement(By.TagName("span")).Text == "My Applications");
                        App.Click();
                        Wait.InSeconds(1);
                        SeleniumExtension._click(By.LinkText("ESP Updates"));
                        Wait.WaitUntilElementDisply(By.Id("supplierSearchTextBox"), 15);
                    }
                    catch (Exception)
                    {
                        driver.FindElement(By.Id("userInfo")).Click();
                        Wait.InSeconds(1);
                        SeleniumExtension.click(By.LinkText("ESP Updates"));
                        Wait.WaitUntilElementDisply(By.Id("supplierSearchTextBox"), 15);
                    }

                }

            }
        }

        private static bool IsLogout()
        {
            return SeleniumExtension.GetLocalPath().Contains("/login");
        }
    }
}
