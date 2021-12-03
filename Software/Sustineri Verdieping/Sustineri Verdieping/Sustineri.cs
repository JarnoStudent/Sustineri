using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sustineri_Verdieping
{
    public partial class Sustineri : Form
    {
        public int screenWidth = 0;
        public int screenHeight = 0;

        private int standardRoundingDiameter = 20;
        private int textboxRoundness = 8;

        Bitmap logoSustineri = Properties.Resources.Cas_sustineri_logo;
        Bitmap logoDroplet = Properties.Resources.Cas_sustineri_logo_NoWords;
        Bitmap backgroundImage = Properties.Resources.backgroundImage;
        Bitmap refreshImage = Properties.Resources.refresh;
        Bitmap logoutImage = Properties.Resources.signout;
        const int LOGO_SUSTINERI_X = 450 / 4 * 3, LOGO_SUSTINERI_Y = 126 / 4 * 3, LOGO_DROPLET_X = 235 / 4 * 3, LOGO_DROPLET_Y = 368 / 4 * 3; //DO NOT CHANGE VALUES
        const string LOGIN = "Login", LOGOUT = "Logout", REGISTER = "Register";

        List<Button> menuMainButtons;
        Label themeBar;


        public Sustineri()
        {
            InitializeComponent();
            Screen scrActive = Screen.FromControl(this);
            screenWidth = scrActive.Bounds.Width;
            screenHeight = scrActive.Bounds.Height;
            panel2.Height = LOGO_SUSTINERI_Y + LOGO_SUSTINERI_Y / 4 * 2;
            panel2.Parent = this;
            panel3.Parent = this;
            panel2.Visible = false;
            panel3.BackColor = Color.FromArgb(240, 240, 240);
            this.Shown += new EventHandler(Toolbar);//becomes the login page later
        }

        enum Pages
        {
            Gas,
            Water,
            Home
        }

        /// <summary>
        /// Adds the bar at the top of the screen to be able to close or minimize the application
        /// </summary>
        private void Toolbar(object sender, EventArgs e)
        {
            int btnwidth = 60;
            CreateControls exitBtn = new CreateControls(new Point(screenWidth - btnwidth, 0), new Size(btnwidth, panel3.Height), panel3, "exit");
            exitBtn.CreateButton(CloseApp, "⨉", SustineriFont.H1, color: Color.Red);
            CreateControls mimimizeBtn = new CreateControls(new Point(screenWidth - btnwidth * 3, 0), new Size(btnwidth, panel3.Height), panel3, "minimize");
            mimimizeBtn.CreateButton(MinimizeApp, "—", SustineriFont.H1, color: Color.LightGray);
            LoginPage();
        }

        /// <summary>
        /// All buttons that switch pages should use this as click event. Which page gets loaded depends on the name of the sender.
        /// </summary>
        private void PageSwitcher(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;

            //Recoloring page switcher buttons depending on which one was clicked
            if (menuMainButtons != null)
                for (int i = 0; i < menuMainButtons.Count; i++)
                {
                    if (menuMainButtons[i] == ctrl)
                    {
                        if (menuMainButtons[i].Name == Pages.Gas.ToString()) { menuMainButtons[i].BackColor = SustineriColor.Green; themeBar.BackColor = SustineriColor.Green; }
                        else { menuMainButtons[i].BackColor = SustineriColor.Blue; themeBar.BackColor = SustineriColor.Blue; }
                        menuMainButtons[i].ForeColor = Color.White;
                    }
                    else
                    {
                        menuMainButtons[i].BackColor = Color.White;
                        menuMainButtons[i].ForeColor = Color.Black;
                    }
                }

            panel1.Controls.Clear();
            switch (ctrl.Name)
            {
                case LOGIN:
                    MenuMain();//add login check later
                    break;
                case LOGOUT:
                    panel2.Controls.Clear();
                    panel2.Visible = false;
                    LoginPage();
                    break;
                case REGISTER:
                    //RegisterPage();
                    break;
                    //etc...
            }
        }

        /// <summary>
        /// Creates the main menu in panel2. this only gets hidden away when the login or register page activate.
        /// </summary>
        private void MenuMain()
        {
            panel2.Visible = true;
            int btnWidth = screenWidth / 15;
            int borderWidth = 4;

            int minBtnWidth = 80;
            if (btnWidth < minBtnWidth) btnWidth = minBtnWidth;

            CreateControls logo = new CreateControls(new Point(btnWidth, (panel2.Height - LOGO_SUSTINERI_Y) / 2), new Size(LOGO_SUSTINERI_X, LOGO_SUSTINERI_Y), panel2, "logo");
            logo.CreatePicBox(logoSustineri);

            CreateControls logOutBtn = new CreateControls(new Point(panel2.Width - btnWidth, 0), new Size(btnWidth, panel2.Height - borderWidth), panel2, LOGOUT);
            logOutBtn.CreateButton(PageSwitcher, image: logoutImage);
            menuMainButtons = new List<Button>();

            for (int i = 0; i < Enum.GetNames(typeof(Pages)).Count(); i++)
            {
                string name = Enum.GetName(typeof(Pages), i);
                CreateControls btn = new CreateControls(new Point(panel2.Width - (btnWidth * (i + 2)), 0), new Size(btnWidth, panel2.Height - borderWidth), panel2, name);
                Button button = btn.CreateButton(PageSwitcher, name, SustineriFont.H1);
                if (button.Name == Pages.Home.ToString()) { button.BackColor = SustineriColor.Blue; button.ForeColor = Color.White; }
                menuMainButtons.Add(button);
            }

            CreateControls border = new CreateControls(new Point(0, panel2.Height - borderWidth), new Size(panel2.Width, borderWidth), panel2);
            themeBar = border.CreateLabel(color: SustineriColor.Blue);
            //add HomePage(); later
        }

        /// <summary>
        /// Creates the login page
        /// </summary>
        private void LoginPage()
        {
            int line = 0;
            int lineOffset = 10;
            int fieldWidth = panel1.Width / 3;
            int labelWidth = fieldWidth / 3 * 2;
            int labelHeight = 20;
            int logoSizeX = LOGO_DROPLET_X * screenHeight / 1500;
            int logoSizeY = LOGO_DROPLET_Y * screenHeight / 1500;
            int labelCenterX = (panel1.Width - labelWidth) / 2;

            CreateControls loginLabel = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * line)), new Size(labelWidth, labelHeight + 5), panel1);
            loginLabel.CreateLabel("Inloggen", SustineriFont.H1);

            //name
            CreateControls namelbl = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), loginLabel.ObjSize, panel1);
            namelbl.CreateLabel("Naam", textAlignment: ContentAlignment.BottomLeft);
            //name textbox
            CreateControls nameBox = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), loginLabel.ObjSize, panel1, "Name");
            nameBox.CreateTextBox(roundedCornerDiameter: textboxRoundness);
            CreateControls nameBoxBorder = new CreateControls(nameBox.ObjPoint, nameBox.ObjSize, panel1);
            nameBoxBorder.CreatePicBox(color: Color.Black, sendToBack: true, roundedCornerDiameter: textboxRoundness, bleed: 1);

            //password
            CreateControls pwlbl = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), loginLabel.ObjSize, panel1);
            pwlbl.CreateLabel("Wachtwoord", textAlignment: ContentAlignment.BottomLeft);
            //passwordtextbox
            CreateControls pwBox = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), loginLabel.ObjSize, panel1, "Password");
            pwBox.CreateTextBox(isPassword: true, roundedCornerDiameter: textboxRoundness);
            CreateControls pwBoxBorder = new CreateControls(pwBox.ObjPoint, pwBox.ObjSize, panel1);
            pwBoxBorder.CreatePicBox(color: Color.Black, sendToBack: true, roundedCornerDiameter: textboxRoundness, bleed: 1);

            //buttons
            int btnWidth = labelWidth / 5 * 2;
            CreateControls loginBtn = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line) + labelHeight), new Size(btnWidth, labelHeight * 2), panel1, LOGIN);
            loginBtn.CreateButton(PageSwitcher, LOGIN, color: SustineriColor.Blue, roundCornerDiameter: textboxRoundness);
            CreateControls registerBtn = new CreateControls(new Point(labelCenterX + labelWidth - btnWidth, loginBtn.ObjPoint.Y), new Size(btnWidth, labelHeight * 2), panel1, REGISTER);
            registerBtn.CreateButton(PageSwitcher, "Registreren", color: SustineriColor.Blue, roundCornerDiameter: textboxRoundness);

            //visual appearance
            int fieldHeight = registerBtn.ObjPoint.Y;

            CreateControls logo = new CreateControls(new Point((panel1.Width - logoSizeX) / 2, panel1.Height / 2 - logoSizeY / 6 * 7), new Size(logoSizeX, logoSizeY), panel1, "logo");
            logo.CreatePicBox(logoDroplet);

            CreateControls colorField = new CreateControls(new Point((panel1.Width - fieldWidth) / 2, (panel1.Height - fieldHeight) / 2), new Size(fieldWidth, fieldHeight), panel1, "background");
            colorField.CreatePicBox(color: Color.White, sendToBack: true, roundedCornerDiameter: standardRoundingDiameter);

            CreateControls background = new CreateControls(new Point(0, 0), new Size(panel1.Width, panel1.Height), panel1, "background");
            background.CreatePicBox(backgroundImage, sendToBack: true);
        }

        /// <summary>
        /// closes the application
        /// </summary>
        private void CloseApp(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// minimizes the application
        /// </summary>
        private void MinimizeApp(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

    }
}
