using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityTestCases.Entity;

namespace VelocityTestCases.References
{
    class Info
    {
        //--------------New Product Info-------------------
        public const string NewProduct_Name = "chin up bar";
        public const string NewProduct_Name_Restricted = "Leatherman CamelBak Bottle";
        public const string NewProduct_Description = "wall mount chin up bar";
        public const string NewProduct_Description_Restricted = "EZ up or E-Z up CamelBak CamelBak";
        public const string NewProduct_Type_Text = "Sports, Fitness, & Outdoors";
        public static IList<string> NewProduct_Keywords = new List<string>() { "chin up", "Fitness", "Home Fitness" };


        public const string NewProduct_Type_Value = "$048";

        public static string NewProduct_Summary = "Product use for Chinup Exercise";
        public static string NewProduct_Summary_Restricted = "EZ up or E-Z up CamelBak CamelBak";

        public static IEnumerable<string> NewProduct_Catogories = new List<string> { "Sports Equipment & Access.-Jogging", "Key Holders-With Miniature Sports Replica" };
        public static string NewProduct_Color = "Blue";
        public static IList<Itemsprice> PriceListForProduct = new List<Itemsprice>{new Itemsprice{Order=1,Quantity=2,ListPrice=10,PriceCode="N 60%"},
                                                        new Itemsprice{Order=1,Quantity=5,ListPrice=9,PriceCode="N 60%"},
                                                        new Itemsprice{Order=1,Quantity=10,ListPrice=8,PriceCode="N 60%"},
                                                        new Itemsprice{Order=1,Quantity=20,ListPrice=7.5,PriceCode="N 60%"}};
        public static double BasePriceForProduct = 12.5;
        public static string PriceInclude { get; set; }

        public static string BasePriceGridNameForProduct = "Blue color Prices";
        public const string PriceType = "List Price";

        public static IEnumerable<string> ImageFileNameList = new List<string> { "1.jpg", "2.jpg", "3.jpg" };

        public static string priceCode = "J/Y 5%";
        public static string LastAddedProductID = "";

        public static string NewProduct_Name_deleteImage = "chin up bar";

        public static string Product_Name_ActiveProduct = "chin up bar";

        public static string Product_Name_ActiveProductToInactive = "chin up bar";

        public static string Active_Product_Name_ForUpdate = "chin up bar";

        public static IEnumerable<string> NewProduct_Update_Catogories = new List<string> { "ARM BANDS", "FASTENERS" };

        public static string NewProduct_Update_Summary = "This Is Updated Summry";

        public static IEnumerable<string> NewProduct_Update_Keywords = new List<string>() { "chin up Bar", "Fitness From Home", "Core Strength" };

        public static string Active_Product_Name_ForCopy = "chin up bar";

        public static string Active_Product_NewName_ForCopy = "chin up bar Copy ";

        //this filter should have Multiple records in Result
        public static string Search_Product_Name_BulkOperation = "chin up bar";

        public static IList<string> Bulk_Add_KeyWords = new List<string>() { "Bulk Operation", "Updated Keys" };

        public static string Added_Active_Product_Id_SPG = "9207-550753165";
        public static string Added_Active_Product_Id_MPG = "9207-550753165";
        public static string Addvertising_Keywords = "product,health,";
        public static string hidden_Keywords = "product,health,";



        public static string Dist_ShareProductName = "";

        public static string Dist_SharedProductNumber = "";

        public static string Dist_PrivateProductNumber = "";

        public static string Dist_PrivateProductName = "";
    }
}
