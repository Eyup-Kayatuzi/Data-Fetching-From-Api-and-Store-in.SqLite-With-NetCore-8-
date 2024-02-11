using DataFetchingAndSqLiteManupilation.Context;
using DataFetchingAndSqLiteManupilation.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace DataFetchingAndSqLiteManupilation.HelperMethods
{
    public class ApiRequest
    {
        public void CallGetProducts(string token, string endPoint)
        {
            var request = ApiRequestMethod(endPoint, "GET", "application/json");
            request.Headers.Add("Authorization", "JWT " + token);
            string responseText = ApiResponseMethod(request);
            if (responseText != null)
            {
                var products = JsonConvert.DeserializeObject<GetProductType>(responseText);
                using (var context = new ApplicationDbContext())
                {
                    //context.Products.Add(products.productList[0]);
                    var bb = context.Products.ToList();
                    var aa = context.SaveChanges();
                }
                Console.WriteLine(products.totalProduct);
            }
            else
            {

            }
        }

        public void  GetToken(AppSettings dataFromJsonFile)
        {
            string email = dataFromJsonFile.email;
            string password = dataFromJsonFile.password;
            string postData = $"email={email}&password={password}";
            byte[] dataBytes = Encoding.UTF8.GetBytes(postData);
            var request = ApiRequestMethod(dataFromJsonFile.getTokenEndPoint, "POST", "application/x-www-form-urlencoded", dataBytes.Length);
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(dataBytes, 0, dataBytes.Length);
            }
            string responseText = ApiResponseMethod(request);
            if (responseText != null)
            {
                var tokenObject = JsonConvert.DeserializeObject<TokenData>(responseText);
                Console.WriteLine(responseText);
                new ApiRequest().CallGetProducts(tokenObject.access, dataFromJsonFile.getProductEndPoint);
            }
            else
            {

            }
            
        }

        private HttpWebRequest ApiRequestMethod(string endPoint, string requestType, string contextType, int length = 0)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = requestType;
            request.ContentType = contextType;
            if(length != 0)
                request.ContentLength = length;
            return request;
        }
        private string ApiResponseMethod(HttpWebRequest request)
        {
            string returnValue = null;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    returnValue = reader.ReadToEnd();
                    return returnValue;
                }

            }
            catch (WebException ex)
            {
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine($"Error Code: {httpResponse.StatusCode}");
                    using (Stream data = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(data))
                        {
                            string text = reader.ReadToEnd();
                            Console.WriteLine(text);
                        }
                    }
                }
            }
            return returnValue;
        }
    }
}
