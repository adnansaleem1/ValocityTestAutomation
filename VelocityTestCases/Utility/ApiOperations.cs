using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VelocityTestCases.Entity;

namespace VelocityTestCases.Utility
{
    class ApiOperations
    {
        public static ProductObject GetProductByID(string id, APIUser user)
        {

                var content = GetProductJsonByID(id, user);
                ProductObject ProductResponse = JsonConvert.DeserializeObject<ProductObject>(content);
                return ProductResponse;

        }

        private static string GetProductJsonByID(string id, APIUser user)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri(user.GetProductUrl() + id);
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;
            //request.AddBody(new { });
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("AuthToken", user.AuthToken);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            if (content == "" || response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception("Unable to find any product with given id - Fail");
            }
            return content;
        }

        internal static bool ChangeProductName(string productId, APIUser user, string NewProductName)
        {
            try
            {
                var content = GetProductJsonByID(productId, user);
                JObject rss = JObject.Parse(content);
                rss["Name"] = NewProductName;
                var UpdatedProduct = rss.ToString();
                RestClient client = new RestClient();
                client.BaseUrl = new Uri(user.POSTProductUrl());
                var request = new RestRequest(Method.POST);
               request.RequestFormat = DataFormat.Json;
               // var json = request.JsonSerializer.Serialize(UpdatedProduct);
                 request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddHeader("AuthToken", user.AuthToken);
                request.AddHeader("content-length", UpdatedProduct.Length.ToString());
                request.AddParameter("application/json", UpdatedProduct, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                if (response.Content == "" && response.StatusCode != HttpStatusCode.OK)
                {

                    throw new Exception("Unable to Update product - Fail");
                }
                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    ProductSubmitError Error = JsonConvert.DeserializeObject<ProductSubmitError>(response.Content);
                    if (Error.StatusCode == "BAD_REQUEST")
                    {
                        Logger.Log(String.Format("Product ID: {0} Return With Error on submit", productId));
                        foreach (string error in Error.AdditionalInfo)
                        {
                            Logger.Log(error);
                        }
                    }
                    else if (Error.StatusCode == "OK")
                    {
                        Logger.Log(String.Format("Product ID: {0} Updated successfully", productId));
                    }
                }
                else {
                    Logger.Log(String.Format("Product ID: {0} Updated successfully", productId));                
                }
                return true;
            }
            catch (Exception)
            {
                
                throw;
            }


        }

        internal static bool InActiveProduct(string productId, APIUser user)
        {
            try
            {
                RestClient client = new RestClient();
                client.BaseUrl = new Uri(user.POSTProductUrl() + productId + "/inactivate");
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
               // request.AddBody(UpdatedProduct);
                //request.AddBody(new { });
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddHeader("AuthToken", user.AuthToken);
                IRestResponse response = client.Execute(request);

                if (response.Content != "" || response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Unable to InActive product - Fail");
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            } 
        }
        internal static bool DeleteProduct(string productId, APIUser user)
        {
            try
            {
                RestClient client = new RestClient();
                client.BaseUrl = new Uri(user.POSTProductUrl() + productId);
                var request = new RestRequest(Method.DELETE);
                request.RequestFormat = DataFormat.Json;
                // request.AddBody(UpdatedProduct);
                //request.AddBody(new { });
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddHeader("AuthToken", user.AuthToken);
                IRestResponse response = client.Execute(request);

                if (response.Content == "" && response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Unable to Delete product - Fail");
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal static bool DeleteProductHiddenKeyWords(string productId, APIUser user)
        {
            try
            {
                var content = GetProductJsonByID(productId, user);
                JObject rss = JObject.Parse(content);
                rss.Property("Categories").Remove();
                rss.Property("ProductKeywords").Remove();
                //rss["Name"] = NewProductName;
                var UpdatedProduct = rss.ToString();
                RestClient client = new RestClient();
                client.BaseUrl = new Uri(user.POSTProductUrl());
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                // var json = request.JsonSerializer.Serialize(UpdatedProduct);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddHeader("AuthToken", user.AuthToken);
                request.AddHeader("content-length", UpdatedProduct.Length.ToString());
                request.AddParameter("application/json", UpdatedProduct, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                if (response.Content == "" && response.StatusCode != HttpStatusCode.OK)
                {

                    throw new Exception("Unable to Update product - Fail");
                }
                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    ProductSubmitError Error = JsonConvert.DeserializeObject<ProductSubmitError>(response.Content);
                    if (Error.StatusCode == "BAD_REQUEST")
                    {
                        Logger.Log(String.Format("Product ID: {0} Return With Error on submit", productId));
                        foreach (string error in Error.AdditionalInfo)
                        {
                            Logger.Log(error);
                        }
                    }
                    else if (Error.StatusCode == "OK")
                    {
                        Logger.Log(String.Format("Product ID: {0} Updated successfully", productId));
                    }
                }
                else
                {
                    Logger.Log(String.Format("Product ID: {0} Updated successfully", productId));
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
