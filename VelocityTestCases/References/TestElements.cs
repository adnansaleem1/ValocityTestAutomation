using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelocityTestCases.References
{
    class TestElements
    {
    //-----------------Login Elements--------------
        public  const string Login_F_ID_AsiNumber = "txtAsiNum";
        public  const string Login_F_ID_UserName = "txtUserName";
        public  const string Login_F_ID_Password = "txtPassword";
        public  const string Login_CB_ID_RememberMe = "chkRememberMe";
        public  const string Login_Btn_XPath_Login = "//*[@id=\"memberLogin\"]/div[5]/input";



        //-----------------ESP Logo Check Test--------------
        public const string ESPLogo_Xpath = "//*[@id=\"main-header\"]/div/div[2]/div/a";

        //-----------------Add Product With SPG--------------
        public const string AddProduct_ManageProducts_tab_Xpath = "//*[@id=\"shellTop-view\"]/div[2]/div/div[1]/ul/li[3]/a";


        public static string AddProduct_AddNewProduct_button_Attribute = "button[data-bind='click: $root.showProductAddModal']";

        public static string AddProduct_AddName_Field_id = "prodName";

        public static string AddProduct_Description_Field_id = "prodDesc";

        public static string AddProduct_Type_Dropdown_Xpath = "//*[@id=\"addNewProduct\"]/div[2]/div/div[3]/div/select";

        public static string AddProduct_Apply_btn_Xpath = "//*[@id=\"addNewProduct\"]/div[3]/button";

        public static string AddProduct_DetailPage_Element_id = "product-info-view";

        public static string AddProduct_Summary_Field_id = "productSummary";

        public static string AddProduct_Catagory_btn_Xpath = "//*[@id=\"product-info-view\"]/div[2]/div[1]/div[1]/div[2]/div[2]/div[1]/button";

        public static string AddProduct_CatogoryPatent_Div_Attribute = "div[data-bind='foreach: uniqueCategories()']";

        public static string AddProduct_CatogoriesAll_Btn_Attribute = "button[data-bind='click: viewAllCategories']";

        public static string AddProduct_CatogriesSearch_Field_Xpath = "//*[@id=\"allCategoriesModal\"]/div[2]/div[1]/input";

        public static string AddProduct_CatogriesName_Span_Attribute = "span[data-bind='    text: name']";

        public static string AddProduct_CatogriesApply_Button_attribute = "button[data-bind='click: applyCategories']";

        public static string AddProduct_SaveProduct_btn_Attribute = "button[data-bind='command: saveProduct, activity: saveProduct.isExecuting']";

        public static string AddProduct_Images_tab_Attribute = "a[data-href='#/product/images']";

        public static string AddProduct_MediaLibrary_btn_Xapth = "//*[@id=\"product-images-view\"]/div[1]/div/div[2]/div/div[2]/div/input";

        public static string AddProduct_MediaLibraryPopupMain_div_class = "MediaLibrary_images";

        public static string AddProduct_SelectImageContainer_div_XPath = "//*[@id=\"divMediaLibrary\"]/div[1]/div[2]/div[2]";
        //*[@id="divMediaLibrary"]/div[1]/div[2]/div[3]

        public static string AddProduct_UseselctedImages_btn_Attribute = "button[data-bind='click: $root.addAndSaveProductMedia, enable: $root.canAttachMedia'";

        public static string AddProduct_Imprintingtab_a_attribute = "a[data-href='#/product/imprint']";

        public static string AddProduct_Shapes_input_id = "token-input-shapesInput";

        public static string AddProduct_BlueColor_btn_Xpath = "//*[@id=\"product-attributes-view\"]/div[1]/div[1]/div[2]/div[2]/div[1]/div[1]/button[2]";

        public static string AddProduct_Pricingtab_a_attribute = "a[data-href='#/product/pricing']";

        public static string AddProduct_BasePrice_field_Xpath = "//*[@id=\"pricingTabContent\"]/div[6]/div/div[1]/div/div/div[1]/div[1]/input";

        public static string AddProduct_SaveProductMedia_btn_Attribute = "button[data-bind='command: saveProductMedia, activity: saveProductMedia.isExecuting']";

        public static string AddProduct_Attributestab_a_Xapth = "//*[@id=\"shellTop-view\"]/div[2]/div/div[2]/ul/li[2]/a";

        public static string AddProduct_SaveProductColor_btn_Xapth = "//*[@id=\"btnSection\"]/div[3]/div[1]/button[1]";

        public static string AddProduct_OpenPriceGrid_a_Xapth = "//*[@id=\"chevron_825419739\"]";

        public static string AddProduct_PriceGrid_div_Xpath = "//*[@id=\"pgid_825419739\"]/div[1]";

        public static string AddProduct_priceType_Select_Xapth = "//*[@id=\"priceGridTab\"]/div/select";

        public static string AddProduct_confirmPriceTypeChange_a_attribute = "a[data-bind='click: onChangePriceType']";

        public static string AddProduct_SavePrice_Btn_attribute = "button[data-bind='command: savePricing, activity: savePricing.isExecuting']";

        public static string AddProduct_BlueColor_btn_Attribute = "button[data-bind='click: $root.addColor, enable: $root.colorPickerEnabled']";

        public static string AddProduct_PriceGrid_Parent_Class = "priceGrid";

        public static string AddProduct_PriceGrid_div_class = "data-grid";

        public static string AddProduct_MakeActive_btn_atribute = "button[data-bind='command: publishProduct, activity: publishProduct.isExecuting']";

        public static string AddProduct_MakeActive_btn_Xapth = "//*[@id=\"btnSection\"]/div[4]/div[1]/button[2]";

        public static string AddProduct_SucessOk_Btn_Xapth ="//*[@id=\"product-pricing-view\"]/div[8]/div[3]/button";

        public static string RestrictedWords_ManageProducts_tab_attribute = "a[data-href='#/dashboard']";

        public static string AddProduct_SubmitRestrictedWords_btn_Xpath = "//*[@id=\"validationModalPrice\"]/div[3]/button[4]";

        public static string AddProduct_SuccessRestrictedSubmit__btn_Xapth = "//*[@id=\"product-pricing-view\"]/div[10]/div[3]/button";

        public static string license_1stAcceptCheckbox = "Module_253599_C_ctl00_cbTerms";

        public static string license_2ndAcceptCheckbox = "Module_253599_C_ctl00_cbPrivacy";

        public static string license_Signaturetxt = "Signature";

        public static string license_Acceptbtn = "Module_253599_C_ctl00_btnOK";

        public static string license_GetStartedbtn = "btn btn-default btn-lg getStartedBtn";



        public static string BasicDetailTab_MakeActive_btn_Xapth = "//*[@id=\"btnSection\"]/div/div[1]/button[2]";

        public static string BulkActive_Btn_Xpath="//*[@id=\"bulkPublishModal\"]/div[3]/button";
        public static string BulkActive_OkBtn_Xpath = "//*[@id=\"sayModalSuccessForm\"]/div[3]/button";


        public static string DownloadReportBtnXpath = "//*[@id=\"ActiveZCodeBasePriceProducts\"]/div[3]/button";
    }
}
