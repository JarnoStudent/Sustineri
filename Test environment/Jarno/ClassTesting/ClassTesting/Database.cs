using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Web;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ClassTesting
{
    class Database
    {
        #region explain why no sql
        //NOT NEEDED EXPLAIN WHY!!!!
        //private string myconnectionstring = "server=192.168.161.80;uid=root;pwd=869p1zrg;database=test;";

        //public void databasecon()
        //{
        //    mysqlconnection conn = new mysqlconnection(myconnectionstring);
        //    try
        //    {
        //        conn.open();
        //        messagebox.show("connection open!");
        //        conn.close();
        //        messagebox.show("connection closed!");
        //    }
        //    catch (mysqlexception ex)
        //    {
        //        messagebox.show(ex.message);
        //    }
        //}///////////////////////////////////////////////////////////////////////////////////////////
        #endregion

        #region test database (WORKS)
        //TEST DATABASE
        //public void webRequesting()
        //{
        //    WebRequest request = WebRequest.Create("http://127.0.0.1/php_rest_test/api/post/users/read_single_user.php?testID=2");
        //    WebResponse response = request.GetResponse();
        //    MessageBox.Show(((HttpWebResponse)response).StatusDescription);
        //    using (Stream dataStream = response.GetResponseStream())
        //    {
        //        // Open the stream using a StreamReader for easy access.
        //        StreamReader reader = new StreamReader(dataStream);

        //        // Read the content of PHP response.
        //        string responseFromServer = reader.ReadToEnd();

        //        //Parse json data to dynamic variable data so all data can be shown (ONLY WORKS WITH SINGLE DATA).
        //        dynamic data = JObject.Parse(responseFromServer);//["testUser"].ToString();

        //        string dataUser = data.testUser;
        //        string dataPassword = data.testPassword;
        //        string dataAdres = data.testAdres;

        //        Console.WriteLine(dataUser);
        //        Console.WriteLine(dataPassword);
        //        Console.WriteLine(dataAdres);

        //        //Display full content in json.
        //        Console.WriteLine(responseFromServer);


        //    }
        //    response.Close();
        //}

        //public void CreateUser()
        //{
        //    string testUser = "testNumberThree";
        //    string testPassword = "testPasswordThree";
        //    string testAdres = "testAdresThree";

        //    string CreateUserURL = "http://127.0.0.1/php_rest_test/api/post/users/create_user.php?testUser=" + testUser + "&testPassword=" + testPassword + "&testAdres=" + testAdres;

        //    using (WebClient client = new WebClient())
        //    {
        //        string src = client.DownloadString(CreateUserURL);
        //    }
        //}

        //public void UpdateUser()
        //{
        //    string testID = "8";
        //    string testUser = "testNumberFive";
        //    string testPassword = "testPasswordFive";
        //    string testAdres = "testAdresFive";

        //    string UpdateUserURL = "http://127.0.0.1/php_rest_test/api/post/users/update_user.php?testID=" + testID + "&testUser=" + testUser + "&testPassword=" + testPassword + "&testAdres=" + testAdres;

        //    using (WebClient client = new WebClient())
        //    {
        //        string src = client.DownloadString(UpdateUserURL);
        //    }
        //}

        //public void DeleteUser()
        //{
        //    string testID = "8";

        //    string DeleteUserURL = "http://127.0.0.1/php_rest_test/api/post/users/delete_user.php?testID=" + testID;

        //    using (WebClient client = new WebClient())
        //    {
        //        string src = client.DownloadString(DeleteUserURL);
        //    }
        //}//////////////////////////////////////////////////
        #endregion

        #region Code for sustineri database
        //Code for when api gets implemented in database
        private const string databaseURL = "http://192.168.161.80/";

        public string userID;
        public string userName;
        public string userPassword;
        public string userFirstName;
        public string userInsertion;
        public string userLastName;
        public string userEmail;
        public string userStreet;
        public string userHouseNumber;
        public string userPostalCode;
        public string userCity;
        public string userGender;
        public string userBirthDate;

        /// <summary>
        /// Gets a single record from the database where userID is id of user record you want to get.
        /// Returns dynamic variable json data.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="userID"></param>
        public dynamic GetSingleData(string tableName, string userID)
        {
            WebRequest request = WebRequest.Create(databaseURL + "php_sustineri_api/api/post/" + tableName + "/read_single_user.php?userID=" + userID);
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader responseReader = new StreamReader(responseStream))
                        {
                            string json = responseReader.ReadToEnd();
                            dynamic data = JObject.Parse(json);
                            return data;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                return ex.ToString();
            }
        }

        public string CreateUser()
        {
            string CreateUserURL = "http://192.168.161.80/php_sustineri_api/api/post/userdata/create_user.php?" + userName + userPassword +
                "&userFirstName=" + userFirstName + "&userInsertion=" + userInsertion + "&userLastName=" + userLastName + "&userEmail" + userEmail +
                "&userStreet=" + userStreet + "&userHouseNumber=" + userHouseNumber + "&userPostalCode=" + userPostalCode + "&userCity=" + userCity +
                "&userGender=" + userGender + "&userBirtDate=" + userBirthDate;

            using (WebClient client = new WebClient())
            {
                try
                {
                    string src = client.DownloadString(CreateUserURL);
                    return "User created!";
                }
                catch (WebException ex)
                {
                    return ex.ToString();
                }
            }
        }

        public void UpdateUser(string userData)
        {
            string UpdateUserURL = "http://192.168.161.80/php_sustineri_api/api/post/userdata/update_user.php?" + userData;

            using (WebClient client = new WebClient())
            {
                string src = client.DownloadString(UpdateUserURL);
            }
        }

        public void DeleteUser(string userID)
        {
            string DeleteUserURL = "http://192.168.161.80/php_sustineri_api/api/post/user_data/delete_user.php?userID=" + userID;

            using (WebClient client = new WebClient())
            {
                string src = client.DownloadString(DeleteUserURL);
            }
        }
        #endregion
    }
}
