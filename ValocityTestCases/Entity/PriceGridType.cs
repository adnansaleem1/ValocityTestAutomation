using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValocityTestCases.Entity
{
   public enum PricegridType {SingleGrid,MultipleGrid }
   public enum PriceTypesForItem { Both,List_Price,Net_Cost}
   public enum ExtrenalUserTabItems { Scoreboard,Media_Library,Manage_Product,catalog_Compliances,Supplier_info}
   public enum NewProductTabItems { Basic_Detail,Attributes,Impringting, Pricing, Images,SKU_Inventory,Availability,Summary }
   public enum EITUserTabItems { Manage_Products, EIT_Reports, EIT_Data_Quality,Message_Center}
   public enum ValocityUserType { External,Eit}
   public enum ApiVersion { JavaV2, JavaV3, NetV3 }
   public enum Application { EspWebSites,EspUpdates}
   public enum ApplicationState { Supplier_Home_Page_Tabs,EIT_User_Home_Page_Tabs,ProdcutInfo_Page_Tabs}



}
