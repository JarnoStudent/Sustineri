using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

namespace Sustineri_Verdieping
{
    static class API
    {
        //Private string for standard database url that is used for every API request.
        private static readonly string databaseURL = "https://192.168.161.82/sustineri_api/api/";

        /// <summary>
        /// Method for making a API request.<br></br>
        /// Needs URL for making certain API request. Example: /user/create_user.php<br></br>
        /// Needs the method for making request. Example: POST<br></br>
        /// Needs a json string that works for API.<br></br>
        /// </summary>
        /// <param name="inputURL"></param>
        /// <param name="requestMethod"></param>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static dynamic APIRequest(string inputURL, string requestMethod, string jsonString)
        {
            //Set webrequest with URL to HttpWebRequest object and set contentype and method.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(databaseURL + inputURL);
            request.ContentType = "application/json";
            request.Method = requestMethod;

            //Set GetRequestStream to Stream object and write json on stream to be posted on body. Afterwards flush streamwriter.
            using (Stream stream = request.GetRequestStream())
            {
                StreamWriter streamWriter = new StreamWriter(stream);
                streamWriter.Write(jsonString);
                streamWriter.Flush();

                try
                {
                    //Set GetResponse to WebResponse object.
                    WebResponse response = request.GetResponse();

                    //Set GetResponseStream to variable Stream.
                    using (Stream datastream = response.GetResponseStream())
                    {
                        //Create object of streamreader.
                        StreamReader reader = new StreamReader(datastream);
                        //Set data from reading stream in string json.
                        string json = reader.ReadToEnd();
                        //Parse json string to dynamic responseJson.
                        dynamic responseJson = JObject.Parse(json);
                        //Return response where data is Message.
                        return responseJson;
                    }
                }
                catch (WebException ex)
                {
                    return ex;
                }
            }
        }
    }
}
