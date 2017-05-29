using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocityTestCases.References
{
    class CommonElements
    {
        public const string ManageProduct_Tab_Xpath = "//*[@id=\"shellTop-view\"]/div/div/ul/li[1]/a";
        public const string SupplierSearch_Field_Id = "supplierSearchTextBox";
        public const string SupplierSearch_btn_Xpath = "//*[@id=\"eit-dashboard-view\"]/div[2]/div/a";
        public const string SupplierResultContainer_Div_Xpath = "//*[@id=\"eit-dashboard-view\"]/div[5]/div";
        public const string SupplierResultContainer_AsiNumber_Display ="span[data-bind='text: asiNumber']";
        public const string SupplierResultContainer_LoginAsSuplier_Link = "a[data-bind='click: $root.loginAsSupplier']";
        public const string loadingBackDrop_div_Class="loading-backdrop";


        public static string loadingImageUpload = "";


        public static string MyAccount_Dropdown_Xpath = "//*[@id=\"shellTop-view\"]/div[1]/div[1]/div/div/div/ul/li[5]/a";
        public static string Logout_btn_Xpath = "//*[@id=\"logoutForm\"]/a";

    }
}
