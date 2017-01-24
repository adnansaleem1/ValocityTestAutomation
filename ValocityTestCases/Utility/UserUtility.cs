using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValocityTestCases.Entity;
using ValocityTestCases.References;
using ValocityTestCases.Entity;
using System.Threading;
using RestSharp;
using Newtonsoft.Json;

namespace ValocityTestCases.Utility
{
    class UserUtility
    {
        IWebDriver driver = DriverAccess.Shared();
        public static VelocityUser LogedInUser;
        public static void LoginToValocity(VelocityUser user)
        {
            if (LogedInUser == null || LogedInUser.ASINumber != user.ASINumber || LogedInUser.Password != user.Password || LogedInUser.UserName != user.UserName || LogedInUser.Type != user.Type)
            {
                UserUtility.LogoutVelocity();
                LogedInUser = user;
                UserUtility.LoginUser();
            }

        }

        public static void LogoutVelocity()
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                UserUtility.LogedInUser = null;
                if (State.GetUrlStateWithoutBase().ToString() != Links.Route_Velocity_Login.ToString())
                {

                    Wait.WaitUntilElementClickAble(By.XPath(CommonElements.MyAccount_Dropdown_Xpath));
                    SeleniumExtension.click(By.XPath(CommonElements.MyAccount_Dropdown_Xpath));
                    Wait.InSeconds(1);
                    SeleniumExtension.click(By.XPath(CommonElements.Logout_btn_Xpath));
                    Wait.InSeconds(3);

                }


            }
            catch (Exception)
            {
                throw new Exception("Unable To Logout Previous User");
            }
        }
        public static void LoginUser()
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                driver.FindElement(By.Id(TestElements.Login_F_ID_AsiNumber)).Clear();
                driver.FindElement(By.Id(TestElements.Login_F_ID_UserName)).Clear();
                driver.FindElement(By.Id(TestElements.Login_F_ID_Password)).Clear();
                driver.FindElement(By.Id(TestElements.Login_F_ID_AsiNumber)).SendKeys(LogedInUser.ASINumber);
                driver.FindElement(By.Id(TestElements.Login_F_ID_UserName)).SendKeys(LogedInUser.UserName);
                driver.FindElement(By.Id(TestElements.Login_F_ID_Password)).SendKeys(LogedInUser.Password);
                if (driver.FindElement(By.Id(TestElements.Login_CB_ID_RememberMe)).Selected)
                {
                    driver.FindElement(By.Id(TestElements.Login_CB_ID_RememberMe)).Click();
                }
                driver.FindElement(By.XPath(TestElements.Login_Btn_XPath_Login)).Click();
                if (UserUtility.LogedInUser.Type == ValocityUserType.Eit)
                {
                    try
                    {
                        Wait.WaitUntilElementDisply(By.Id("supplierSearchTextBox"));
                        // Wait.InSeconds(1);
                    }
                    catch (Exception ex)
                    {
                        //if(Exception.ReferenceEquals)
                        throw;
                    }
                }
                else if (UserUtility.LogedInUser.Type == ValocityUserType.External)
                {
                    try
                    {
                        Wait.WaitUntilElementDisply(By.Id("supplierSearchTextBox"));
                        // Wait.InSeconds(1);
                    }
                    catch (Exception ex)
                    {
                        //if(Exception.ReferenceEquals)
                        throw;
                    }


                }
                //Wait.InSeconds(5);
                //Credentials.Valocity_User_Type = LogedInUser.Type;
                Logger.Log("Login Successfull..");
            }
            catch (Exception)
            {

                throw new Exception("Unable To Login By The Given User");
            }
        }
        public void LoginToMMS(string userName, string Password)
        {
            driver.Url = Links.URL_MMS;
            WebDriverWait ewait = new WebDriverWait(driver, new TimeSpan(5));
            IAlert alert = ewait.Until(ExpectedConditions.AlertIsPresent());
            alert.SetAuthenticationCredentials(userName, Password);
            Wait.InSeconds(1);
        }

        internal static void VelocityLoginAsSupplier(string SId)
        {
            IWebElement Suppliertab = UserUtility.SearchSupplierById(SId);
            if (Suppliertab != null)
            {
                try
                {
                    Suppliertab.FindElement(By.CssSelector(CommonElements.SupplierResultContainer_LoginAsSuplier_Link)).Click();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                throw new TestCaseException("Unable to Login No Supplier Found With This ID");
            }
        }
        public static void LoginToESPweb(string ASINum, string UserName, string Password)
        {
            IWebDriver driver = DriverAccess.Shared();
            driver.FindElement(By.Id("asilogin_AsiNumber")).Clear();
            driver.FindElement(By.Id("asilogin_UserName")).Clear();
            driver.FindElement(By.Id("asilogin_Password")).Clear();
            driver.FindElement(By.Id("asilogin_AsiNumber")).SendKeys(ASINum);
            driver.FindElement(By.Id("asilogin_UserName")).SendKeys(UserName);
            driver.FindElement(By.Id("asilogin_Password")).SendKeys(Password);
            if (driver.FindElement(By.Id("rememberMe")).Selected)
            {
                driver.FindElement(By.Id("rememberMe")).Click();
            }
            driver.FindElement(By.Id("btnLogin")).Click();
            //Wait.WaitUntilLoadingInVisible(By.)
            Wait.InSeconds(5);
            try
            {
                //Robot robot = new Robot();
                //Thread.Sleep(3000);
                //robot.keyPress(KeyEvent.VK_ENTER);
                //Thread.Sleep(2000);
                //robot.keyRelease(KeyEvent.VK_ENTER);
                Thread.Sleep(2000);
                driver.SwitchTo().Alert().Accept();
                Wait.InSeconds(3);

                if (driver.FindElement(By.LinkText("ESP Web Home Page")).Displayed)
                {
                    TestCasesCommon.LicenceAgrement();
                    //driver.Url = TestData.sNavigateToWESP;
                    Thread.Sleep(5000);
                }

            }
            catch (Exception e)
            {

            }
            //Credentials.Valocity_User_Type = UserType;
            Logger.Log("Login Successfull..");

        }

        internal static IWebElement SearchSupplierById(string SId)
        {
            IWebDriver driver = DriverAccess.Shared();
            Wait.WaitUntilElementPopup(By.Id(CommonElements.SupplierSearch_Field_Id));
            SeleniumExtension.AddTextToField(By.Id(CommonElements.SupplierSearch_Field_Id), SId);
            SeleniumExtension.click(By.XPath(CommonElements.SupplierSearch_btn_Xpath));
            Wait.InSeconds(1);
            Wait.WaitUntilLoadingInVisible();
            Wait.InSeconds(1);
            IWebElement ParentDiv = driver.FindElement(By.XPath(CommonElements.SupplierResultContainer_Div_Xpath));
            IReadOnlyList<IWebElement> childs = ParentDiv.FindElements(By.ClassName("well-small"));
            IWebElement Suppliertab = null;
            foreach (IWebElement ele in childs)
            {
                string txt = ele.FindElement(By.CssSelector(CommonElements.SupplierResultContainer_AsiNumber_Display)).Text;
                if (txt == SId)
                {
                    Suppliertab = ele;
                    break;
                }
            }
            if (Suppliertab != null)
            {
                return Suppliertab;
            }
            else
            {
                throw new TestCaseException("No Supplier Found With This ID");
            }
        }


        internal static void GotoApplicationByLinkText(string p)
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                SeleniumExtension.click(By.LinkText("My Applications"));
                IWebElement parent = driver.FindElement(By.XPath("//*[@id=\"shellTop-view\"]/div[1]/div[1]/div/div/div/ul/li[3]/ul"));
                IReadOnlyList<IWebElement> childs = parent.FindElements(By.TagName("a"));
                // String selectLinkOpeninNewTab = Keys.chord(Keys.Control, "t");
                parent.FindElement(By.LinkText(p)).Click();
                //  Wait.WaitUntilElementDisply(wait);
                //driver.executeScript("window.history.go(-1)");
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal static void GotoApplicationByLinkText(string p, By by)
        {
            try
            {
                IWebDriver driver = DriverAccess.Shared();
                SeleniumExtension.click(By.LinkText("My Applications"));
                IWebElement parent = driver.FindElement(By.XPath("//*[@id=\"shellTop-view\"]/div[1]/div[1]/div/div/div/ul/li[3]/ul"));
                IReadOnlyList<IWebElement> childs = parent.FindElements(By.TagName("a"));
                // String selectLinkOpeninNewTab = Keys.chord(Keys.Control, "t");
                try
                {
                    parent.FindElement(By.LinkText(p)).Click();
                }
                catch (Exception)
                {

                    //  throw;
                }
                Wait.WaitUntilElementDisply(by);
                //driver.executeScript("window.history.go(-1)");
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal static void NullUserSession()
        {
            UserUtility.LogedInUser = null;
        }

        internal static void LoginToAPI(Entity.APIUser aPIUser)
        {
            try
            {
                if (aPIUser.AuthToken == "" || aPIUser.AuthToken == null)
                {
                    string Api_Url = aPIUser.GetUrlForApiByUser();
                    Api_Url += Links.Route_Api_Login;
                    RestClient client = new RestClient();
                    client.BaseUrl = new Uri(Api_Url);
                    var request = new RestRequest(Method.POST);
                    request.RequestFormat = DataFormat.Json;
                    request.AddBody(new { Asi = aPIUser.ASINumber, Username = aPIUser.UserName,Password=aPIUser.Password});
                    request.AddHeader("Content-Type","application/json");
                    request.AddHeader("Accept","application/json");
                   
                    IRestResponse response = client.Execute(request);
                    var content = response.Content;
                    if (content == "") {
                        throw new Exception("Given user was unable to login Api - Fail");
                    }
                    ApiLoginResponse LoginResponse = JsonConvert.DeserializeObject<ApiLoginResponse>(content);
                    aPIUser.AuthToken = LoginResponse.AccessToken;
                    aPIUser.TokenExpirey = LoginResponse.TokenExpirationTime;
                    // client.Post()
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }


    }
}

