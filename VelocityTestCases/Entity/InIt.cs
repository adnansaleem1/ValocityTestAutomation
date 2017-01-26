using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityTestCases.References;
using VelocityTestCases.UI;
using VelocityTestCases.Utility;

namespace VelocityTestCases.Entity
{
    class InIt
    {
        public  string URL_VelocityBase{ get; set; }
        public  string ApiBasePath{ get; set; }
        public  string URL_ESP_Web{ get; set; }
        public string URL_MMS { get; set; }
        public IList<VelocityUser> VelocityUser { get; set; }
        public IList<APIUser> APIUser { get; set; }
        public void GetConfigFromJson() {
            JObject JsonInit = FileHandler.GetJsonFileContents("References\\Init.json");
            InIt FillObject=null;
            if ((string)JsonInit["run"] == "staging")
            {
                var content = JsonInit["staging"].ToString() ;
                FillObject = JsonConvert.DeserializeObject<InIt>(content);
            }
            else if ((string)JsonInit["run"] == "uat")
            {
                FillObject = JsonConvert.DeserializeObject<InIt>(JsonInit["uat"].ToString());

            }
            else if ((string)JsonInit["run"] == "production")
            {
                FillObject = JsonConvert.DeserializeObject<InIt>(JsonInit["production"].ToString());
            }
            this.URL_VelocityBase = FillObject.URL_VelocityBase;
            this.ApiBasePath = FillObject.ApiBasePath;
            this.URL_ESP_Web = FillObject.URL_ESP_Web;
            this.URL_MMS = FillObject.URL_MMS;
            this.VelocityUser = FillObject.VelocityUser;
            this.APIUser = FillObject.APIUser;
        }
        public void SetInformation(){
          //  Form1 Env_Selection_From = new Form1();
           // Env_Selection_From.Show();
            this.GetConfigFromJson();
             Links.URL_VelocityBase=this.URL_VelocityBase ;
             Links.ApiBasePath=this.ApiBasePath ;
             Links.URL_ESP_Web = this.URL_ESP_Web;
            Links.URL_MMS = this.URL_MMS;
            if (this.VelocityUser[0].Type == VelocityUserType.Eit)
            {
                Credentials.VelocityEIT_User = this.VelocityUser[0];
                Credentials.VelocityExternal_User = this.VelocityUser[1];

            }
            else {
                Credentials.VelocityEIT_User = this.VelocityUser[1];
                Credentials.VelocityExternal_User = this.VelocityUser[0];
            }
            foreach (APIUser user in APIUser) {
                if (user.version == ApiVersion.JavaV2) {
                    Credentials.APIV2_User = user;
                }
                else if (user.version == ApiVersion.JavaV3) {
                    Credentials.APIV3_User = user;

                }
                else if (user.version == ApiVersion.NetV3) {
                    Credentials.APIV3Net_User = user;
                }
            
            }
          
        }

    }
}
