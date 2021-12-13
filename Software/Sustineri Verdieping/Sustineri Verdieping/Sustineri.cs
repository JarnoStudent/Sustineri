using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sustineri_Verdieping
{
    public partial class Sustineri : Form
    {

        //temp fake data DELETE WHEN DATABASE EXISTS
        List<string> timePeriod = new List<string>();
        List<double> gasData = new List<double>() { 10, 2, 6, 40, 5, 7, 50, 20, 40, 28, 29, 48, 1, 48, 58, 27, 59, 28, 49, 6, 40, 5, 7, 50, 20, 40, 28, 29, 48, 1, 48, 58 };
        List<double> waterData = new List<double>() { 85, 23, 66, 470, 65, 97, 240, 120, 340, 228, 229, 148, 21, 48, 58, 27, 59, 28, 49, 26, 40, 85, 87, 50, 220, 410, 328, 129, 148, 91, 148, 58 };
        //fake data till here

        public int screenWidth = 0;
        public int screenHeight = 0;

        private int standardRoundingDiameter = 20;
        private int textboxRoundness = 8;

        int avgChartWidth;
        int avgChartHeight;
        int avgChartPosX;
        int firstChartPosY;
        int avgLabelHeight = 20;

        Bitmap logoSustineri = Properties.Resources.Cas_sustineri_logo;
        Bitmap logoDroplet = Properties.Resources.Cas_sustineri_logo_NoWords;
        Bitmap backgroundImage = Properties.Resources.backgroundImage;
        Bitmap backgroundImageToBlack = Properties.Resources.backgroundImageDipToBlack;
        Bitmap refreshImage = Properties.Resources.refresh;
        Bitmap logoutImage = Properties.Resources.signout;
        Bitmap accInfoImage = Properties.Resources.accountInfo;
        const int LOGO_SUSTINERI_X = 450 / 4 * 3, LOGO_SUSTINERI_Y = 126 / 4 * 3, LOGO_DROPLET_X = 235 / 4 * 3, LOGO_DROPLET_Y = 368 / 4 * 3; //DO NOT CHANGE VALUES
        List<Series> chartSeries = new List<Series>();
        List<Label> updatableLabels = new List<Label>();
        List<Button> menuMainButtons;
        Label themeBar;

        public Sustineri()
        {
            InitializeComponent();
            Screen scrActive = Screen.FromControl(this);
            screenWidth = scrActive.Bounds.Width;
            screenHeight = scrActive.Bounds.Height;
            avgChartWidth = screenWidth / 10 * 8;
            avgChartHeight = screenHeight / 2;
            avgChartPosX = (screenWidth - avgChartWidth) / 2;
            firstChartPosY = panel1.Height / 5;

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
        /// Names in Dutch so it can also be used as tekst
        /// </summary>
        enum BtnClickEvents
        {
            Login,
            Afmelden,
            Registreren,
            Terug,
            MaakGebruiker,
            Verversen,
            GebruikersInformatie
        }

        enum Months
        {
            jan,
            feb,
            mrt,
            apr,
            mei,
            jun,
            jul,
            aug,
            sept,
            okt,
            nov,
            dec
        }
        enum Days
        {
            maandag,
            dinsdag,
            woensdag,
            donderdag,
            vrijdag,
            zaterdag,
            zondag
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
            bool valid = true; // set valid to false if something went wrong like unable to create account or unable to login.

            switch (ctrl.Name)
            {
                case nameof(BtnClickEvents.Login)://Validation required
                    if (valid)
                    {
                        panel1.Controls.Clear(); // put in HomePage() later
                        MenuMain();
                    }
                    break;

                case nameof(BtnClickEvents.Terug):
                case nameof(BtnClickEvents.Afmelden):
                    panel2.Controls.Clear();
                    panel2.Visible = false;
                    LoginPage();
                    break;

                case nameof(BtnClickEvents.Registreren):
                    LoginPage(true);
                    break;

                case nameof(BtnClickEvents.MaakGebruiker)://Validation required
                    if (valid)
                    {
                        LoginPage();
                    }
                    break;

                case nameof(BtnClickEvents.Verversen):
                    for (int i = 0; i < chartSeries.Count; i++)
                    {
                        Refresh();
                    }
                    break;

                case nameof(BtnClickEvents.GebruikersInformatie):
                    valid = false;//remove when page can be created
                    //UserInfoPage();
                    break;

                case nameof(Pages.Home):
                    valid = false;//remove when page can be created
                    //HomePage();
                    break;

                case nameof(Pages.Water):
                    valid = false;//remove when page can be created
                    //WaterPage();
                    break;

                case nameof(Pages.Gas):
                    GasPage();
                    break;
                    //etc...
            }

            //Recoloring page switcher buttons depending on which one was clicked
            if (menuMainButtons != null && valid)
                for (int i = 0; i < menuMainButtons.Count; i++)
                {
                    if (menuMainButtons[i] == ctrl)
                    {
                        if (menuMainButtons[i].Name == nameof(Pages.Gas) || menuMainButtons[i].Name == nameof(BtnClickEvents.GebruikersInformatie))
                        { menuMainButtons[i].BackColor = ColorSustineri.Green; themeBar.BackColor = ColorSustineri.Green; }
                        else { menuMainButtons[i].BackColor = ColorSustineri.Blue; themeBar.BackColor = ColorSustineri.Blue; }
                        menuMainButtons[i].ForeColor = Color.White;
                    }
                    else
                    {
                        menuMainButtons[i].BackColor = Color.White;
                        menuMainButtons[i].ForeColor = Color.Black;
                    }
                }


        }

        /// <summary>
        /// Creates the main menu in panel2. this only gets hidden away when the login or register page activate.
        /// </summary>
        private void MenuMain()
        {
            panel2.Visible = true;
            panel2.Controls.Clear();
            int btnWidth = screenWidth / 15;
            int borderWidth = 4;
            int xLine = 1;
            int minBtnWidth = 80;
            if (btnWidth < minBtnWidth) btnWidth = minBtnWidth;

            CreateControls logo = new CreateControls(new Point(btnWidth, (panel2.Height - LOGO_SUSTINERI_Y) / 2), new Size(LOGO_SUSTINERI_X, LOGO_SUSTINERI_Y), panel2, "logo");
            logo.CreatePicBox(logoSustineri);

            CreateControls logOutBtn = new CreateControls(new Point(panel2.Width - btnWidth * xLine++, 0), new Size(btnWidth, panel2.Height - borderWidth), panel2, nameof(BtnClickEvents.Afmelden));
            logOutBtn.CreateButton(PageSwitcher, image: logoutImage);

            menuMainButtons = new List<Button>();

            CreateControls accInfoBtn = new CreateControls(new Point(panel2.Width - btnWidth * xLine++, 0), new Size(btnWidth, panel2.Height - borderWidth), panel2, nameof(BtnClickEvents.GebruikersInformatie));
            // accInfoBtn.CreateButton(PageSwitcher, image: accInfoImage);
            menuMainButtons.Add(accInfoBtn.CreateButton(PageSwitcher, image: accInfoImage));

            for (int i = 0; i < Enum.GetNames(typeof(Pages)).Count(); i++)
            {
                string name = Enum.GetName(typeof(Pages), i);
                CreateControls btn = new CreateControls(new Point(panel2.Width - (btnWidth * (xLine++)), 0), new Size(btnWidth, panel2.Height - borderWidth), panel2, name);
                Button button = btn.CreateButton(PageSwitcher, name, FontSustineri.H1);
                if (button.Name == nameof(Pages.Home)) { button.BackColor = ColorSustineri.Blue; button.ForeColor = Color.White; }
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
            panel1.Controls.Clear();
            chartSeries.Clear();

            int line = 0;
            int lineOffset = 10;
            int baseHeight = panel1.Height / 2;
            int fieldWidth = panel1.Width / 3;
            int labelWidth = fieldWidth / 3 * 2;
            int logoSizeX = LOGO_DROPLET_X * screenHeight / 1500;
            int logoSizeY = LOGO_DROPLET_Y * screenHeight / 1500;
            int labelCenterX = (panel1.Width - labelWidth) / 2;
            int maxCharLength = 15;

            string titleText = "Inloggen";
            string nameText = "Naam";
            string leftBtnText = nameof(BtnClickEvents.Login);
            string rightBtnText = nameof(BtnClickEvents.Registreren);
            if (isRegisterPage)
            {
                titleText = "Registreren";
                nameText += $" (max {maxCharLength} karakters)";
                leftBtnText = nameof(BtnClickEvents.Terug);
                rightBtnText = nameof(BtnClickEvents.MaakGebruiker);
            }

            CreateControls title = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * line)), new Size(labelWidth, avgLabelHeight + 5), panel1);
            title.CreateLabel(titleText, FontSustineri.H1);

            //name
            CreateControls namelbl = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), title.ObjSize, panel1);
            namelbl.CreateLabel(nameText, textAlignment: ContentAlignment.BottomLeft);
            //name textbox
            CreateControls nameBox = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), title.ObjSize, panel1, "Name");
            nameBox.CreateTextBox(maxLength: maxCharLength, roundCornerDiameter: textboxRoundness);

            //password
            CreateControls pwlbl = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), title.ObjSize, panel1);
            pwlbl.CreateLabel("Wachtwoord", textAlignment: ContentAlignment.BottomLeft);
            //passwordtextbox
            CreateControls pwBox = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), title.ObjSize, panel1, "Password");
            pwBox.CreateTextBox(isPassword: true, roundCornerDiameter: textboxRoundness);

            int extraPageLength = 0;
            if (isRegisterPage)
            {
                panel1.HorizontalScroll.Maximum = 0;
                panel1.AutoScroll = true;

                //passwordConfirmation
                CreateControls pwConfirmlbl = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), title.ObjSize, panel1);
                pwConfirmlbl.CreateLabel("Wachtwoord Bevestigen", textAlignment: ContentAlignment.BottomLeft);
                //textbox
                CreateControls pwConfirmBox = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), title.ObjSize, panel1, "Password Confirmation");
                pwConfirmBox.CreateTextBox(isPassword: true, roundCornerDiameter: textboxRoundness);

                //first name
                CreateControls firstNameLbl = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), title.ObjSize, panel1);
                firstNameLbl.CreateLabel($"Voornaam (max {maxCharLength} karakters)", textAlignment: ContentAlignment.BottomLeft);
                //textbox
                CreateControls firstNameBox = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), title.ObjSize, panel1, "FirstName");
                firstNameBox.CreateTextBox(maxLength: maxCharLength, roundCornerDiameter: textboxRoundness);

                //insertion & last name labels
                CreateControls insertionLbl = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), new Size(title.ObjSize.Width / 3, title.ObjSize.Height), panel1);
                insertionLbl.CreateLabel("Tussenvoegsel", textAlignment: ContentAlignment.BottomLeft);
                CreateControls lastNameLbl = new CreateControls(new Point(labelCenterX + title.ObjSize.Width / 5 * 2, baseHeight + ((avgLabelHeight + lineOffset) * line)), new Size(title.ObjSize.Width / 5 * 3, title.ObjSize.Height), panel1);
                lastNameLbl.CreateLabel($"Achternaam (max {maxCharLength} karakters)", textAlignment: ContentAlignment.BottomLeft);
                //textboxes
                CreateControls insertBox = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), insertionLbl.ObjSize, panel1, "Insertion");
                insertBox.CreateTextBox(maxLength: maxCharLength, roundCornerDiameter: textboxRoundness);
                CreateControls lastNameBox = new CreateControls(new Point(lastNameLbl.ObjPoint.X, baseHeight + ((avgLabelHeight + lineOffset) * line)), lastNameLbl.ObjSize, panel1, "Insertion");
                lastNameBox.CreateTextBox(maxLength: maxCharLength, roundCornerDiameter: textboxRoundness);

                //e-mail label
                CreateControls emailLbl = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), title.ObjSize, panel1);
                emailLbl.CreateLabel("e-mail", textAlignment: ContentAlignment.BottomLeft);
                //textbox
                CreateControls emailBox = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), title.ObjSize, panel1, "E-mail");
                emailBox.CreateTextBox(roundCornerDiameter: textboxRoundness);

                //street & house number
                CreateControls streetLbl = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), new Size(title.ObjSize.Width / 5 * 3, title.ObjSize.Height), panel1);
                streetLbl.CreateLabel("Straat", textAlignment: ContentAlignment.BottomLeft);
                CreateControls houseNumberLbl = new CreateControls(new Point(labelCenterX + title.ObjSize.Width / 3 * 2, baseHeight + ((avgLabelHeight + lineOffset) * line)), new Size(title.ObjSize.Width / 3, title.ObjSize.Height), panel1);
                houseNumberLbl.CreateLabel("Huisnummer", textAlignment: ContentAlignment.BottomLeft);
                //textbox
                CreateControls streetBox = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), streetLbl.ObjSize, panel1, "Street");
                streetBox.CreateTextBox(roundCornerDiameter: textboxRoundness);
                CreateControls houseNumberBox = new CreateControls(new Point(houseNumberLbl.ObjPoint.X, baseHeight + ((avgLabelHeight + lineOffset) * line)), houseNumberLbl.ObjSize, panel1, "HouseNumber");
                houseNumberBox.CreateTextBox(maxLength: maxCharLength, roundCornerDiameter: textboxRoundness);

                //postalcode & city
                CreateControls postalcodeLbl = new CreateControls(new Point(labelCenterX, panel1.Height / 2 + ((avgLabelHeight + lineOffset) * ++line)), new Size(title.ObjSize.Width / 3, title.ObjSize.Height), panel1);
                postalcodeLbl.CreateLabel("Postcode", textAlignment: ContentAlignment.BottomLeft);
                CreateControls cityLbl = new CreateControls(new Point(labelCenterX + title.ObjSize.Width / 5 * 2, baseHeight + ((avgLabelHeight + lineOffset) * line)), new Size(title.ObjSize.Width / 5 * 3, title.ObjSize.Height), panel1);
                cityLbl.CreateLabel("Stad", textAlignment: ContentAlignment.BottomLeft);
                //textbox
                CreateControls postalBox = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), postalcodeLbl.ObjSize, panel1, "PostalCode");
                postalBox.CreateTextBox(maxLength: 4, roundCornerDiameter: textboxRoundness);
                CreateControls cityBox = new CreateControls(new Point(cityLbl.ObjPoint.X, baseHeight + ((avgLabelHeight + lineOffset) * line)), cityLbl.ObjSize, panel1, "City");
                cityBox.CreateTextBox(roundCornerDiameter: textboxRoundness);

                //gender
                CreateControls genderLbl = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), new Size(title.ObjSize.Width / 3, title.ObjSize.Height), panel1);
                genderLbl.CreateLabel("Gender", textAlignment: ContentAlignment.BottomLeft);
                //Gender dropdown
                CreateControls genderDropDown = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), genderLbl.ObjSize, panel1, "Gender");
                genderDropDown.CreateDropDown(new string[] { "Man", "Vrouw", "Neutraal" }, roundCornerDiameter: textboxRoundness);

                //Birth date
                CreateControls birthDateLbl = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), title.ObjSize, panel1);
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
                CreateControls dayDropDown = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), new Size(objSizeX, title.ObjSize.Height), panel1);
                dayDropDown.CreateDropDown(days, "Dag", roundCornerDiameter: textboxRoundness);
                CreateControls monthDropDown = new CreateControls(new Point(labelCenterX + objSizeX + objSizeCalc, baseHeight + ((avgLabelHeight + lineOffset) * line)), new Size(objSizeX, title.ObjSize.Height), panel1);
                monthDropDown.CreateDropDown(months, "Maand", roundCornerDiameter: textboxRoundness);
                CreateControls yearDropDown = new CreateControls(new Point(labelCenterX + title.ObjSize.Width - objSizeX, baseHeight + ((avgLabelHeight + lineOffset) * line)), new Size(objSizeX, title.ObjSize.Height), panel1);
                yearDropDown.CreateDropDown(years, "Jaar", roundCornerDiameter: textboxRoundness);

                extraPageLength = yearDropDown.ObjPoint.Y - pwBox.ObjPoint.Y;
            }

            //buttons
            int btnWidth = labelWidth / 5 * 2;
            CreateControls leftBtn = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line) + avgLabelHeight), new Size(btnWidth, avgLabelHeight * 2), panel1, leftBtnText);
            leftBtn.CreateButton(PageSwitcher, leftBtnText, color: ColorSustineri.Blue, roundCornerDiameter: textboxRoundness);
            CreateControls rightButton = new CreateControls(new Point(labelCenterX + labelWidth - btnWidth, leftBtn.ObjPoint.Y), new Size(btnWidth, avgLabelHeight * 2), panel1, rightBtnText);
            rightButton.CreateButton(PageSwitcher, nameof(BtnClickEvents.Registreren), color: ColorSustineri.Blue, roundCornerDiameter: textboxRoundness);

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
        /// Creates Gas page
        /// </summary>
        private void GasPage()
        {
            panel1.Controls.Clear();
            chartSeries.Clear();

            List<double> weekGasData = new List<double>();
            timePeriod.Clear();
            timePeriod.AddRange(Enum.GetNames(typeof(Days)).Cast<string>().ToList());
            for (int i = 0; i < timePeriod.Count; i++)
            {
                weekGasData.Add(gasData[i]);
            }
            string gasChart = "Gas";
            int dropDownWidth = avgChartWidth / 8;
            CreateControls dropDown = new CreateControls(new Point(avgChartPosX + avgChartWidth - dropDownWidth, firstChartPosY + avgChartHeight), new Size(dropDownWidth, avgLabelHeight), panel1, gasChart);
            ComboBox comboBox = dropDown.CreateDropDown(new string[] { "Dag", "Week", "Maand", "Jaar" }, roundCornerDiameter: textboxRoundness);
            comboBox.SelectedIndex = 1;
            comboBox.SelectedIndexChanged += new EventHandler(DropDownEvents);

            CreateControls total = new CreateControls(new Point(avgChartPosX, firstChartPosY + avgChartHeight), new Size(dropDownWidth, avgLabelHeight), panel1, gasChart);
            updatableLabels.Add(total.CreateLabel("Total:", FontSustineri.H1));

            CreateCharts chart = new CreateCharts(new Point(avgChartPosX, firstChartPosY), new Size(avgChartWidth, avgChartHeight), panel1, gasChart);
            chart.Design(SeriesChartType.Column, timePeriod, weekGasData, gasChart, ColorSustineri.Green);
            chartSeries.Add(chart.ChartObj.Series[0]);

            Refresh(total, $"Total: {weekGasData.Sum()}");

        }

        /// <summary>
        /// Updates chart based on name of the chart and dropdown to selected data of: Dag, Week, Maand, Jaar
        /// </summary>
        private void DropDownEvents(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string total = "Total: ";
            List<double> y = new List<double>();
            if (comboBox.Name == "Gas") { y = gasData; }
            else if (comboBox.Name == "Water") { y = waterData; }

            switch (comboBox.SelectedItem.ToString())
            {
                case "Dag":
                    for (int i = 0; i < chartSeries.Count; i++) if (chartSeries[i].Name == comboBox.Name)
                        {
                            Refresh(chartSeries[i], y, 24, addToX: ".00");
                            total += chartSeries[i].Points.Sum(total => total.YValues.Sum()).ToString();
                        }
                    break;

                case "Week":
                    for (int i = 0; i < chartSeries.Count; i++) if (chartSeries[i].Name == comboBox.Name)
                        {
                            Refresh(chartSeries[i], y, Enum.GetNames(typeof(Days)).Cast<string>().ToList());
                            total += chartSeries[i].Points.Sum(total => total.YValues.Sum()).ToString();
                        }
                    break;

                case "Maand":
                    for (int i = 0; i < chartSeries.Count; i++) if (chartSeries[i].Name == comboBox.Name)
                        {
                            Refresh(chartSeries[i], y, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 1);
                            total += chartSeries[i].Points.Sum(total => total.YValues.Sum()).ToString();
                        }

                    break;

                case "Jaar":
                    for (int i = 0; i < chartSeries.Count; i++) if (chartSeries[i].Name == comboBox.Name)
                        {
                            Refresh(chartSeries[i], y, Enum.GetNames(typeof(Months)).Cast<string>().ToList());
                            total += chartSeries[i].Points.Sum(total => total.YValues.Sum()).ToString();
                        }
                    break;
            }
            for (int i = 0; i < updatableLabels.Count; i++) if (updatableLabels[i].Name == comboBox.Name) Refresh(updatableLabels[i], $"Total: {total}");
        }

        /// <summary>
        /// Refreshes a chart series directly using given lists
        /// </summary>
        private void Refresh(Series series, List<string> x, List<double> y)
        {
            series.Points.DataBindXY(x, y);
        }
        /// <summary>
        /// Refreshes a chart series using timePeriod list and given y axis list
        /// </summary>
        private void Refresh(Series series, List<double> y, int xTimePeriod, int increaseTimeperiodText = 0, string addToX = "")
        {
            timePeriod.Clear();
            List<double> dataForTimePeriod = new List<double>();
            for (int i = 0; i < xTimePeriod; i++)
            {
                timePeriod.Add((i + increaseTimeperiodText).ToString() + addToX);
                dataForTimePeriod.Add(y[i]);
            }
            series.Points.DataBindXY(timePeriod, dataForTimePeriod);
        }
        /// <summary>
        /// Refreshes a chart series using timePeriod list and given y axis list. X axis names are from your given list
        /// </summary>
        private void Refresh(Series series, List<double> y, List<string> xTimeNames)
        {
            timePeriod.Clear();
            List<double> dataForTimePeriod = new List<double>();
            for (int i = 0; i < xTimeNames.Count; i++)
            {
                timePeriod.Add(xTimeNames[i]);
                dataForTimePeriod.Add(y[i]);//failsafe in case Y has more variables
            }
            Console.WriteLine($"period: {timePeriod.Count}, Data: {dataForTimePeriod.Count}, y: {y.Count}");
            series.Points.DataBindXY(timePeriod, dataForTimePeriod);
        }

        /// <summary>
        /// Refreshes a chart series or label
        /// </summary>
        private void Refresh(Label label, string newText)
        {
            label.Text = newText;
        }
        /// <summary>
        /// Refreshes a chart series or label
        /// </summary>
        private void Refresh(CreateControls label, string newText)
        {
            if (label.Ctrl is Label)
            {
                Label lbl = label.Ctrl as Label;
                lbl.Text = newText;
            }
        }
        /// <summary>
        /// Refreshes a chart series or label
        /// </summary>
        private void Refresh(Control label, string newText)
        {
            if (label is Label)
            {
                Label lbl = label as Label;
                lbl.Text = newText;
            }
        }

        /// <summary>
        /// Returns the list in percentages
        /// </summary>
        /// <param name="list">List to be converted</param>
        /// <returns></returns>
        private List<double> ConvertToPercentageList(List<double> list)
        {
            List<double> percentages = new List<double>();
            for (int i = 0; i < list.Count; i++)
            {
                double total = list.Sum();
                double percentage = Math.Round(list[i] / (total / 100), 1);
                percentages.Add(percentage);
            }
            return percentages;
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
