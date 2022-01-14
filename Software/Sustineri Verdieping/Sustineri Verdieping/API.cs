using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

namespace Sustineri_Verdieping
{
    static class API
    {
        private static readonly string databaseURL = "https://145.220.75.60/sustineri_api/api/"; // General url location of api.

        /// <summary>
        /// Method for making a API request.<br></br>
        /// Needs location of api file for making certain API request. Example: /user/create_user.php<br></br>
        /// Needs the method for making request. Example: POST<br></br>
        /// Needs a json string that works for API.<br></br>
        /// </summary>
        /// <param name="inputURL"></param>
        /// <param name="requestMethod"></param>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static dynamic APIRequest(string inputURL, string requestMethod, string jsonString)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(databaseURL + inputURL);
            request.ContentType = "application/json";
            request.Method = requestMethod;
            request.Timeout = 5000;
            request.ServerCertificateValidationCallback = delegate { return true; }; // Allows all ssl certificates.

            // Try to make a request using streamwriter and request options made before.
            try
            {
                using (Stream stream = request.GetRequestStream())
                {
                    StreamWriter streamWriter = new StreamWriter(stream);
                    streamWriter.Write(jsonString);
                    streamWriter.Flush();

                    // Try to get the request and read the data so it can be stored in a variable and returned.
                    try
                    {
                        WebResponse response = request.GetResponse();

                        using (Stream datastream = response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(datastream);
                            string json = reader.ReadToEnd();
                            dynamic responseJson = JObject.Parse(json);
                            return responseJson;
                        }
                    }
                    catch (WebException ex)
                    {
                        return ex;
                    }
                }
            }
            catch (WebException ex)
            {
                return ex;
            }
        }
    }
}
