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
        Bitmap backgroundImageToBlack = Properties.Resources.backgroundImageDipToBlack;
        Bitmap refreshImage = Properties.Resources.refresh;
        Bitmap logoutImage = Properties.Resources.signout;
        const int LOGO_SUSTINERI_X = 450 / 4 * 3, LOGO_SUSTINERI_Y = 126 / 4 * 3, LOGO_DROPLET_X = 235 / 4 * 3, LOGO_DROPLET_Y = 368 / 4 * 3; //DO NOT CHANGE VALUES
        const string LOGIN = "Login", LOGOUT = "Afmelden", REGISTER = "Registreren", BACK = "Terug", CREATEUSER = "CreateUser";

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
            exitBtn.CreateButton(CloseApp, "⨉", FontSustineri.H1, color: Color.Red);
            CreateControls mimimizeBtn = new CreateControls(new Point(screenWidth - btnwidth * 3, 0), new Size(btnwidth, panel3.Height), panel3, "minimize");
            mimimizeBtn.CreateButton(MinimizeApp, "—", FontSustineri.H1, color: Color.LightGray);
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
                        if (menuMainButtons[i].Name == Pages.Gas.ToString()) { menuMainButtons[i].BackColor = ColorSustineri.Green; themeBar.BackColor = ColorSustineri.Green; }
                        else { menuMainButtons[i].BackColor = ColorSustineri.Blue; themeBar.BackColor = ColorSustineri.Blue; }
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
                case BACK:
                case LOGOUT:
                    panel2.Controls.Clear();
                    panel2.Visible = false;
                    LoginPage();
                    break;
                case REGISTER:
                    LoginPage(true);
                    break;
                case CREATEUSER:
                    //check if user is created and add error or jump to login page depending on result
                    LoginPage();
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
                Button button = btn.CreateButton(PageSwitcher, name, FontSustineri.H1);
                if (button.Name == Pages.Home.ToString()) { button.BackColor = ColorSustineri.Blue; button.ForeColor = Color.White; }
                menuMainButtons.Add(button);
            }

            CreateControls border = new CreateControls(new Point(0, panel2.Height - borderWidth), new Size(panel2.Width, borderWidth), panel2);
            themeBar = border.CreateLabel(color: ColorSustineri.Blue);
            //add HomePage(); later
        }

        /// <summary>
        /// Creates the login page
        /// </summary>
        private void LoginPage(bool isRegisterPage = false)
        {
            int line = 0;
            int lineOffset = 10;
            int fieldWidth = panel1.Width / 3;
            int labelWidth = fieldWidth / 3 * 2;
            int labelHeight = 20;
            int logoSizeX = LOGO_DROPLET_X * screenHeight / 1500;
            int logoSizeY = LOGO_DROPLET_Y * screenHeight / 1500;
            int labelCenterX = (panel1.Width - labelWidth) / 2;
            int maxCharLength = 15;

            string titleText = "Inloggen";
            string nameText = "Naam";
            string leftBtnText = LOGIN;
            string rightBtnText = REGISTER;
            if (isRegisterPage)
            {
                titleText = "Registreren";
                nameText += $" (max {maxCharLength} karakters)";
                leftBtnText = BACK;
                rightBtnText = CREATEUSER;
            }

            CreateControls title = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * line)), new Size(labelWidth, labelHeight + 5), panel1);
            title.CreateLabel(titleText, FontSustineri.H1);

            //name
            CreateControls namelbl = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), title.ObjSize, panel1);
            namelbl.CreateLabel(nameText, textAlignment: ContentAlignment.BottomLeft);
            //name textbox
            CreateControls nameBox = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), title.ObjSize, panel1, "Name");
            nameBox.CreateTextBox(maxLength: maxCharLength, roundCornerDiameter: textboxRoundness);

            //password
            CreateControls pwlbl = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), title.ObjSize, panel1);
            pwlbl.CreateLabel("Wachtwoord", textAlignment: ContentAlignment.BottomLeft);
            //passwordtextbox
            CreateControls pwBox = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), title.ObjSize, panel1, "Password");
            pwBox.CreateTextBox(isPassword: true, roundCornerDiameter: textboxRoundness);

            int extraPageLength = 0;
            if (isRegisterPage)
            {
                panel1.HorizontalScroll.Maximum = 0;
                panel1.AutoScroll = true;                

                //passwordConfirmation
                CreateControls pwConfirmlbl = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), title.ObjSize, panel1);
                pwConfirmlbl.CreateLabel("Wachtwoord Bevestigen", textAlignment: ContentAlignment.BottomLeft);
                //textbox
                CreateControls pwConfirmBox = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), title.ObjSize, panel1, "Password Confirmation");
                pwConfirmBox.CreateTextBox(isPassword: true, roundCornerDiameter: textboxRoundness);

                //first name
                CreateControls firstNameLbl = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), title.ObjSize, panel1);
                firstNameLbl.CreateLabel($"Voornaam (max {maxCharLength} karakters)", textAlignment: ContentAlignment.BottomLeft);
                //textbox
                CreateControls firstNameBox = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), title.ObjSize, panel1, "FirstName");
                firstNameBox.CreateTextBox(maxLength: maxCharLength, roundCornerDiameter: textboxRoundness);

                //insertion & last name labels
                CreateControls insertionLbl = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), new Size(title.ObjSize.Width / 3, title.ObjSize.Height), panel1);
                insertionLbl.CreateLabel("Tussenvoegsel", textAlignment: ContentAlignment.BottomLeft);
                CreateControls lastNameLbl = new CreateControls(new Point(labelCenterX + title.ObjSize.Width / 5 * 2, panel1.Height / 2 + ((labelHeight + lineOffset) * line)), new Size(title.ObjSize.Width / 5 * 3, title.ObjSize.Height), panel1);
                lastNameLbl.CreateLabel($"Achternaam (max {maxCharLength} karakters)", textAlignment: ContentAlignment.BottomLeft);
                //textboxes
                CreateControls insertBox = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), insertionLbl.ObjSize, panel1, "Insertion");
                insertBox.CreateTextBox(maxLength: maxCharLength, roundCornerDiameter: textboxRoundness);
                CreateControls lastNameBox = new CreateControls(new Point(lastNameLbl.ObjPoint.X, panel1.Height / 2 + ((labelHeight + lineOffset) * line)), lastNameLbl.ObjSize, panel1, "Insertion");
                lastNameBox.CreateTextBox(maxLength: maxCharLength, roundCornerDiameter: textboxRoundness);

                //e-mail label
                CreateControls emailLbl = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), title.ObjSize, panel1);
                emailLbl.CreateLabel("e-mail", textAlignment: ContentAlignment.BottomLeft);
                //textbox
                CreateControls emailBox = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), title.ObjSize, panel1, "E-mail");
                emailBox.CreateTextBox(roundCornerDiameter: textboxRoundness);

                //street & house number
                CreateControls streetLbl = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), new Size(title.ObjSize.Width / 5 * 3, title.ObjSize.Height), panel1);
                streetLbl.CreateLabel("Straat", textAlignment: ContentAlignment.BottomLeft);
                CreateControls houseNumberLbl = new CreateControls(new Point(labelCenterX + title.ObjSize.Width / 3 * 2, panel1.Height / 2 + ((labelHeight + lineOffset) * line)), new Size(title.ObjSize.Width / 3, title.ObjSize.Height), panel1);
                houseNumberLbl.CreateLabel("Huisnummer", textAlignment: ContentAlignment.BottomLeft);
                //textbox
                CreateControls streetBox = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), streetLbl.ObjSize, panel1, "Street");
                streetBox.CreateTextBox(roundCornerDiameter: textboxRoundness);
                CreateControls houseNumberBox = new CreateControls(new Point(houseNumberLbl.ObjPoint.X, panel1.Height / 2 + ((labelHeight + lineOffset) * line)), houseNumberLbl.ObjSize, panel1, "HouseNumber");
                houseNumberBox.CreateTextBox(maxLength: maxCharLength, roundCornerDiameter: textboxRoundness);

                //postalcode & city
                CreateControls postalcodeLbl = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), new Size(title.ObjSize.Width / 3, title.ObjSize.Height), panel1);
                postalcodeLbl.CreateLabel("Postcode", textAlignment: ContentAlignment.BottomLeft);
                CreateControls cityLbl = new CreateControls(new Point(labelCenterX + title.ObjSize.Width / 5 * 2, panel1.Height / 2 + ((labelHeight + lineOffset) * line)), new Size(title.ObjSize.Width / 5 * 3, title.ObjSize.Height), panel1);
                cityLbl.CreateLabel("Stad", textAlignment: ContentAlignment.BottomLeft);
                //textbox
                CreateControls postalBox = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), postalcodeLbl.ObjSize, panel1, "PostalCode");
                postalBox.CreateTextBox(maxLength: 4, roundCornerDiameter: textboxRoundness);
                CreateControls cityBox = new CreateControls(new Point(cityLbl.ObjPoint.X, panel1.Height / 2 + ((labelHeight + lineOffset) * line)), cityLbl.ObjSize, panel1, "City");
                cityBox.CreateTextBox(roundCornerDiameter: textboxRoundness);

                //gender
                CreateControls genderLbl = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), new Size(title.ObjSize.Width / 3, title.ObjSize.Height), panel1);
                genderLbl.CreateLabel("Gender", textAlignment: ContentAlignment.BottomLeft);
                //Gender dropdown
                CreateControls genderDropDown = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), genderLbl.ObjSize, panel1, "Gender");
                genderDropDown.CreateDropDown(new string[] { "Man", "Vrouw", "Neutraal" }, "...", roundCornerDiameter: textboxRoundness);

                //Birth date
                CreateControls birthDateLbl = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), title.ObjSize, panel1);
                birthDateLbl.CreateLabel("Geboortedatum", textAlignment: ContentAlignment.BottomLeft);
                string[] days = new string[31];
                string[] months = new string[12];
                string[] years = new string[90];
                for (int i = 0; i < days.Count(); i++)
                {
                    int day = i + 1;
                    if (day < 10) days[i] = 0 + day.ToString();
                    else days[i] = day.ToString();
                }
                for (int i = 0; i < months.Count(); i++)
                {
                    int month = i + 1;
                    if (month < 10) months[i] = 0 + month.ToString();
                    else months[i] = month.ToString();
                }
                for (int i = 0; i < years.Count(); i++)
                {
                    int year = DateTime.Now.Year - i - 1;
                    years[i] = year.ToString();
                }
                int objSizeCalc = birthDateLbl.ObjSize.Width / 11;
                int objSizeX = objSizeCalc * 3;
                //Birth date dropdown
                CreateControls dayDropDown = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line)), new Size(objSizeX, title.ObjSize.Height), panel1);
                dayDropDown.CreateDropDown(days, "Dag", roundCornerDiameter: textboxRoundness);
                CreateControls monthDropDown = new CreateControls(new Point(labelCenterX + objSizeX + objSizeCalc, panel1.Height / 2 + ((labelHeight + lineOffset) * line)), new Size(objSizeX, title.ObjSize.Height), panel1);
                monthDropDown.CreateDropDown(months, "Maand", roundCornerDiameter: textboxRoundness);
                CreateControls yearDropDown = new CreateControls(new Point(labelCenterX + title.ObjSize.Width - objSizeX, panel1.Height / 2 + ((labelHeight + lineOffset) * line)), new Size(objSizeX, title.ObjSize.Height), panel1);
                yearDropDown.CreateDropDown(years, "Jaar", roundCornerDiameter: textboxRoundness);

                extraPageLength = yearDropDown.ObjPoint.Y - pwBox.ObjPoint.Y;
            }

            //buttons
            int btnWidth = labelWidth / 5 * 2;
            CreateControls leftBtn = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((labelHeight + lineOffset) * ++line) + labelHeight), new Size(btnWidth, labelHeight * 2), panel1, leftBtnText);
            leftBtn.CreateButton(PageSwitcher, leftBtnText, color: ColorSustineri.Blue, roundCornerDiameter: textboxRoundness);
            CreateControls rightButton = new CreateControls(new Point(labelCenterX + labelWidth - btnWidth, leftBtn.ObjPoint.Y), new Size(btnWidth, labelHeight * 2), panel1, rightBtnText);
            rightButton.CreateButton(PageSwitcher, REGISTER, color: ColorSustineri.Blue, roundCornerDiameter: textboxRoundness);

            //visual appearance
            int fieldHeight = pwBox.ObjPoint.Y + rightButton.ObjSize.Height;

            CreateControls logo = new CreateControls(new Point((panel1.Width - logoSizeX) / 2, title.ObjPoint.Y - lineOffset * 4 - logoSizeY), new Size(logoSizeX, logoSizeY), panel1, "logo");
            logo.CreatePicBox(logoDroplet);

            CreateControls colorField = new CreateControls(new Point((panel1.Width - fieldWidth) / 2, (panel1.Height - fieldHeight) / 2), new Size(fieldWidth, fieldHeight + extraPageLength), panel1, "background");
            colorField.CreatePicBox(color: Color.White, sendToBack: true, roundCornerDiameter: standardRoundingDiameter);
                        
            if (panel1.VerticalScroll.Visible)
            {
                CreateControls background = new CreateControls(new Point(0, 0), new Size(panel1.Width, panel1.Height), panel1, "background");
                background.CreatePicBox(backgroundImageToBlack, sendToBack: true);
                CreateControls backgroundExtended = new CreateControls(new Point(0, panel1.Height), new Size(panel1.Width, colorField.ObjPoint.Y * 2 + colorField.ObjSize.Height - screenHeight), panel1, "background");
                backgroundExtended.CreatePicBox(color: Color.Black, sendToBack: true);
            }
            else
            {
                CreateControls background = new CreateControls(new Point(0, 0), new Size(panel1.Width, panel1.Height), panel1, "background");
                background.CreatePicBox(backgroundImage, sendToBack: true);
            }
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
