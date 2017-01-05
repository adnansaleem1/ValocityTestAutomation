using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValocityTestCases.References;

namespace ValocityTestCases.Entity
{
    class APIUser:User
    {
        public ApiVersion version { get; set; }
        public string AuthToken { get; set; }
        public DateTime TokenExpirey { get; set; }

        internal string GetUrlForApiByUser()
        {
            string URL = "";
            if (this.version == ApiVersion.JavaV2)
            {
                URL = Links.ApiBasePath + Links.Route_ApiV2_Base;
            }
            else if (this.version == ApiVersion.JavaV3)
            {
                URL = Links.ApiBasePath + Links.Route_ApiV3_Base;

            }
            else if (this.version == ApiVersion.NetV3)
            {
                URL = Links.ApiBasePath + Links.Route_ApiV3Net_Base;

            }
            return URL;
        }
        internal string GetProductUrl() {
            return GetUrlForApiByUser() + Links.Route_Api_GetProduct;
        }

        internal string POSTProductUrl()
        {
            return GetUrlForApiByUser() + Links.Route_Api_PostProduct ;
        }
        internal string InActiveProductUrl()
        {
            return GetUrlForApiByUser() + Links.Route_Api_InActiveProduct;
        }
    }
}
