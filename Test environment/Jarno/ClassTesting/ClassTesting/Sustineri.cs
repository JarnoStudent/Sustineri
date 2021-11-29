using ClassTesting.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace ClassTesting
{
    public partial class Sustineri : Form
    {
        List<Control> listControls = new List<Control>();
        Bitmap bitty = Resources.Sustineri___Logo_druppel_transparant;

        private const string tableUserData = "userdata";
        private string sessionUserID = "2";

        private enum PanelForm
        {
            panel1,
            panel2,
            panel3
        }
        public Sustineri()
        {
            InitializeComponent();
            Font h1 = new Font("Arial", 14, FontStyle.Bold);
            ControlCreator controlLabel1 = new ControlCreator(new Point(0, 0), new Size(100, 100));
            for (int i = 0; i < 3; i++)
            {
                listControls.Add(controlLabel1.CreateLabel($"label{ +i}", "test", ContentAlignment.MiddleCenter, Color.Red, h1, panel1));
            }

            listControls.Add(controlLabel1.CreateButton($"button", "test", ContentAlignment.MiddleCenter, Color.Red, h1, panel1, CreateHomePage));

            listControls.Add(controlLabel1.CreateNumBox($"numBox", 0, 10, h1, panel1));

            listControls.Add(controlLabel1.CreateTextBox($"textBox", h1, panel1, false));

            listControls.Add(controlLabel1.CreatePictureBox($"pictureBox", bitty, ImageLayout.Stretch, panel2));

            //Database database = new Database();
            //database.databaseCon();

            //for (int i = 0; i < 7; i++)
            //{
            //    MessageBox.Show(listControls[i].Name);
            //}

            //TYPE TESTING
            //TypeTesting controlName = new TypeTesting() { Name = "test1" };
            //MessageBox.Show(controlName.ToString());

            //int numberA = 15;
            //double stringA;
            //stringA = (double)numberA;

            //database.webRequesting();
            //List<string> userList = new List<string> { "userName: userOne, userPassword, userAdres" };

            //dynamic jsonNewUser = JsonConvert.SerializeObject(userList);
            //Console.WriteLine(jsonNewUser.userName);
            #region valid data for database
            //Database database = new Database();
            //dynamic userData = database.GetSingleData(tableUserData, sessionUserID);
            //Console.WriteLine(userData.userName);
            //Console.WriteLine(userData.userEmail);

            //database.UserName = "testUserOne";

            //JsonCreator userJson = new JsonCreator();
            //userJson.userID = "2";
            //userJson.userName = "testUserOne";
            //userJson.userPassword = "userPAssow";
            //userJson.userFirstName = "tewsar";
            //userJson.userInsertion = "";
            //userJson.userLastName = "userLastnaem";

            //string jsonNewUser = JsonConvert.SerializeObject(userJson);
            //Console.WriteLine(jsonNewUser);
            ////Console.WriteLine(jsonNewUser);

            //JObject jsonNewUser2 = JObject.Parse(jsonNewUser);
            //Console.WriteLine(jsonNewUser2);
            //string returnMessage = database.CreateUser(jsonNewUser2);
            //Console.WriteLine(returnMessage);
            //Console.WriteLine(jsonNewUser2);
            //Console.WriteLine((string)jsonNewUser2["userName"]);

            //database.CreateUser(jsonNewUser);


            #endregion
        }

        private void CreateHomePage(object sender, EventArgs e)
        {

        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            Database database = new Database();
            database.userName = "testName";
            database.userPassword = "testPassword";
        }

        private void UpdateUser_Click(object sender, EventArgs e)
        {
            Database database = new Database();
            database.UpdateUser("userName=testo");
        }

        private void DeleteUser_Click(object sender, EventArgs e)
        {
            Database database = new Database();

            //database.DeleteUser();
        }
    }
}
