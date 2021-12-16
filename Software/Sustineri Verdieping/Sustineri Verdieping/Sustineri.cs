using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sustineri_Verdieping
{
    public partial class Sustineri : Form
    {
        #region
        //temp fake data DELETE WHEN DATABASE EXISTS
        List<string> timePeriod = new List<string>();
        List<double> gasData = new List<double>() { 10, 2, 6, 40, 5, 7, 50, 20, 40, 28, 29, 48, 1, 48, 58, 27, 59, 28, 49, 6, 40, 5, 7, 50, 20, 40, 28, 29, 48, 1, 48, 58 };
        List<double> waterData = new List<double>() { 85, 23, 66, 470, 65, 97, 240, 120, 340, 228, 229, 148, 21, 48, 58, 27, 59, 28, 49, 26, 40, 85, 87, 50, 220, 410, 328, 129, 148, 91, 148, 58 };
        //fake data till here
        List<string> requestDates;
        private uint waterLimit = uint.MaxValue;
        int dateOffset = 0;

        public int screenWidth = 0;
        public int screenHeight = 0;

        private readonly int standardRoundingDiameter = 20;
        private readonly int textboxRoundness = 8;

        private int avgChartWidth;
        private int avgChartHeight;
        private int avgChartPosX;
        private int firstChartPosY;
        private int avgLabelHeight = 20;

        private readonly Bitmap logoSustineri = Properties.Resources.Cas_sustineri_logo;
        private readonly Bitmap logoDroplet = Properties.Resources.Cas_sustineri_logo_NoWords;
        private readonly Bitmap backgroundImage = Properties.Resources.backgroundImage;
        private readonly Bitmap backgroundImageToBlack = Properties.Resources.backgroundImageDipToBlack;
        private readonly Bitmap refreshImage = Properties.Resources.refresh;
        private readonly Bitmap logoutImage = Properties.Resources.signout;
        private readonly Bitmap accInfoImage = Properties.Resources.accountInfo;
        private const int LOGO_SUSTINERI_X = 450 / 4 * 3, LOGO_SUSTINERI_Y = 126 / 4 * 3, LOGO_DROPLET_X = 235 / 4 * 3, LOGO_DROPLET_Y = 368 / 4 * 3; //DO NOT CHANGE VALUES
        private const string TYPE_GAS = "  Gasverbruik", TYPE_WATER = "  Waterverbruik";
        private const string TYPE_WEEK = "Week", TYPE_MONTH = "Maand";
        List<Series> chartSeries = new List<Series>();
        List<Label> updatableLabels = new List<Label>();
        List<Button> menuMainButtons;
        List<Control> registerControls;
        List<Button> dateChanger;
        PictureBox themeBar;
        #endregion
        public Sustineri()
        {
            InitializeComponent();
            Screen scrActive = Screen.FromControl(this);
            screenWidth = scrActive.Bounds.Width;
            screenHeight = scrActive.Bounds.Height;
            avgChartWidth = screenWidth / 10 * 8;
            avgChartHeight = screenHeight / 3 * 2;
            avgChartPosX = (screenWidth - avgChartWidth) / 2;
            firstChartPosY = panel1.Height / 15;

            panel2.Height = LOGO_SUSTINERI_Y + LOGO_SUSTINERI_Y / 4 * 2;
            panel2.Parent = this;
            panel3.Parent = this;
            panel2.Visible = false;
            panel3.BackColor = Color.FromArgb(240, 240, 240);
            this.Shown += new EventHandler(Toolbar);
        }

        /// <summary>
        /// Adds the bar at the top of the screen to be able to close or minimize the application
        /// </summary>
        private void Toolbar(object sender, EventArgs e)
        {
            int btnwidth = 60;
            CreateControls exitBtn = new CreateControls(new Point(screenWidth - btnwidth, 0), new Size(btnwidth, panel3.Height), panel3, "exit");
            exitBtn.CreateButton(CloseApp, "⨉", FontSustineri.H2, color: Color.Red);
            CreateControls mimimizeBtn = new CreateControls(new Point(screenWidth - btnwidth * 3, 0), new Size(btnwidth, panel3.Height), panel3, "minimize");
            mimimizeBtn.CreateButton(MinimizeApp, "—", FontSustineri.H2, color: Color.LightGray);
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
                    if (registerControls != null)
                        for (int i = 0; i < registerControls.Count; i++)
                            if (!registerControls[i].Name.Contains('*') )
                                registerControls[i].Text = "Liever niet invullen";
                    //VALIDATION HERE!!! 
                    if (valid)
                    {
                        LoginPage();
                    }
                    break;

                case nameof(BtnClickEvents.Verversen):
                    for (int i = 0; i < chartSeries.Count; i++)
                    {
                        if (chartSeries[i].Name == nameof(Pages.Gas))
                        {
                            //pull data from database into the list here
                            Refresh(chartSeries[i], timePeriod, gasData);
                        }
                        if (chartSeries[i].Name == nameof(Pages.Water))
                        {
                            //pull data from database into the list here
                            Refresh(chartSeries[i], timePeriod, waterData);
                        }
                    }
                    break;

                case nameof(BtnClickEvents.GebruikersInformatie):
                    //valid = false;//remove when page can be created
                    UserInfoPage();
                    break;

                case nameof(Pages.Home):
                    valid = false;//remove when page can be created
                    //HomePage();
                    break;

                case nameof(Pages.Water):
                    DataPage(TYPE_WATER, waterData, true);
                    break;

                case nameof(Pages.Gas):
                    DataPage(TYPE_GAS, gasData);
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
                Button button = btn.CreateButton(PageSwitcher, name, FontSustineri.H2);
                if (button.Name == nameof(Pages.Home)) { button.BackColor = ColorSustineri.Blue; button.ForeColor = Color.White; }
                menuMainButtons.Add(button);
            }

            CreateControls border = new CreateControls(new Point(0, panel2.Height - borderWidth), new Size(panel2.Width, borderWidth), panel2);
            themeBar = border.CreatePicBox(color: ColorSustineri.Blue);
            //add HomePage(); later
        }


        private CreateControls CreateSingleLineInput(string text, int width, int line, int maxCharLength = 100, bool isPassword = false, int addToXPos = 0, bool fromCenter = true)
        {
            int labelCenterX = (panel1.Width - width) / 2;
            int posYOrigin = panel1.Height / 2; if (!fromCenter) { posYOrigin = 0; labelCenterX = (screenWidth - avgChartWidth) / 2; }
            labelCenterX += addToXPos;
            CreateControls label = CreateField(text, width, line, addToXPos: addToXPos, fromCenter: fromCenter);

            CreateControls textBox = new CreateControls(new Point(labelCenterX, CalculatePosition(++line, posYOrigin)), label.ObjSize, panel1, text);
            textBox.CreateTextBox(isPassword: isPassword, maxLength: maxCharLength, roundCornerDiameter: textboxRoundness);
            return textBox;
        }

        private CreateControls CreateField(string text, int labelWidth, int line, Font font = null, ContentAlignment contentAlign = ContentAlignment.BottomLeft, int addToXPos = 0, bool fromCenter = true)
        {
            int labelCenterX = (panel1.Width - labelWidth) / 2;
            int posYOrigin = panel1.Height / 2; if (!fromCenter) { posYOrigin = 0; labelCenterX = (screenWidth - avgChartWidth) / 2; }
            int addToY = 5; if (font != null && font.Height > addToY) addToY = font.Height;
            labelCenterX += addToXPos;
            CreateControls label = new CreateControls(new Point(labelCenterX, CalculatePosition(line, posYOrigin)), new Size(labelWidth, avgLabelHeight + addToY), panel1);
            label.CreateLabel(text, font, contentAlign);
            return label;
        }

        /// <summary>
        /// First line starts in the center of the screen
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private int CalculatePosition(int line, int baseHeight = 0)
        {
            if (baseHeight == 0) { baseHeight = panel1.Height / 10; }
            int lineOffset = 10;
            return baseHeight + ((avgLabelHeight + lineOffset) * line);
        }


        /// <summary>
        /// Creates the login page
        /// </summary>
        private void LoginPage(bool isRegisterPage = false)
        {
            panel1.Controls.Clear();
            chartSeries.Clear();
            #region
            int line = 0;
            int lineOffset = 10;
            int baseHeight = panel1.Height / 2;
            int fieldWidth = panel1.Width / 3;
            int labelWidth = fieldWidth / 3 * 2;
            int logoSizeX = LOGO_DROPLET_X * screenHeight / 1500;
            int logoSizeY = LOGO_DROPLET_Y * screenHeight / 1500;
            int labelCenterX = (panel1.Width - labelWidth) / 2;
            int maxCharLength = 16;

            string titleText = "Inloggen";
            string nameText = "Gebruikersnaam";//wordt vervangen met email
            string passText = "Wachtwoord";
            string leftBtnText = nameof(BtnClickEvents.Login);
            string rightBtnText = nameof(BtnClickEvents.Registreren);
            #endregion
            if (isRegisterPage)
            {
                registerControls = new List<Control>();
                titleText = "Registreren";
                nameText += $" * (max {maxCharLength} karakters)";
                passText += " *";
                leftBtnText = nameof(BtnClickEvents.Terug);
                rightBtnText = nameof(BtnClickEvents.MaakGebruiker);
            }

            CreateControls title = CreateField(titleText, labelWidth, line, FontSustineri.H2, ContentAlignment.TopCenter);

            CreateSingleLineInput(nameText, labelWidth, ++line, maxCharLength); line++;
            CreateControls password = CreateSingleLineInput(passText, labelWidth, ++line, isPassword: true); line++;
            int lastLoginObjectPos = password.ObjPoint.Y;

            int extraPageLength = 0;
            if (isRegisterPage)
            {
                panel1.HorizontalScroll.Maximum = 0;
                panel1.AutoScroll = true;
                // Password confirmation
                registerControls.Add(CreateSingleLineInput("Wachtwoord Bevestigen *", labelWidth, ++line, isPassword: true).Ctrl); line++;
                // First name
                registerControls.Add(CreateSingleLineInput($"Voornaam (max {maxCharLength} karakters)", labelWidth, ++line).Ctrl); line++;
                // Insertion and last name
                registerControls.Add(CreateSingleLineInput("Tussenvoegsel", labelWidth / 3, ++line, addToXPos: -labelWidth / 3).Ctrl);
                registerControls.Add(CreateSingleLineInput($"Achternaam (max {maxCharLength} karakters)", labelWidth / 5 * 3, line++, addToXPos: labelWidth / 5).Ctrl);
                // E-mail
                registerControls.Add(CreateSingleLineInput("E-mail *", labelWidth, ++line).Ctrl); line++;
                // Street and housenumber
                registerControls.Add(CreateSingleLineInput("Straat", labelWidth / 5 * 3, ++line, addToXPos: -labelWidth / 5).Ctrl);
                registerControls.Add(CreateSingleLineInput("Huisnummer", labelWidth / 3, line++, addToXPos: labelWidth / 3).Ctrl);
                // Postal code and city
                registerControls.Add(CreateSingleLineInput("Postcode", labelWidth / 3, ++line, addToXPos: -labelWidth / 3).Ctrl);
                registerControls.Add(CreateSingleLineInput("Stad", labelWidth / 5 * 3, line++, addToXPos: labelWidth / 5).Ctrl);
                // Gender
                CreateField("Gender", labelWidth / 3, ++line, addToXPos: -labelWidth / 3);
                //Gender dropdown
                CreateControls genderDropDown = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line)), new Size(labelWidth / 3, avgLabelHeight), panel1, "Gender");
                genderDropDown.CreateDropDown(new string[] { "Man", "Vrouw", "Anders", "Liever niet invullen" }, roundCornerDiameter: textboxRoundness);
                registerControls.Add(genderDropDown.Ctrl);

                CreateField("Geboortedatum", labelWidth, ++line);
                //Birth date picker
                CreateControls birthDate = new CreateControls(new Point(labelCenterX, CalculatePosition(++line, panel1.Height / 2)), new Size(labelWidth / 2, avgLabelHeight), panel1);
                birthDate.CreateDatePicker(textboxRoundness);

                extraPageLength = birthDate.ObjPoint.Y - lastLoginObjectPos;
            }

            //buttons
            int btnWidth = labelWidth / 5 * 2;
            CreateControls leftBtn = new CreateControls(new Point(labelCenterX, baseHeight + ((avgLabelHeight + lineOffset) * ++line) + avgLabelHeight), new Size(btnWidth, avgLabelHeight * 2), panel1, leftBtnText);
            leftBtn.CreateButton(PageSwitcher, leftBtnText, color: ColorSustineri.Blue, roundCornerDiameter: textboxRoundness);
            CreateControls rightButton = new CreateControls(new Point(labelCenterX + labelWidth - btnWidth, leftBtn.ObjPoint.Y), new Size(btnWidth, avgLabelHeight * 2), panel1, rightBtnText);
            rightButton.CreateButton(PageSwitcher, nameof(BtnClickEvents.Registreren), color: ColorSustineri.Blue, roundCornerDiameter: textboxRoundness);

            //visual appearance
            int fieldHeight = lastLoginObjectPos + rightButton.ObjSize.Height;

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
        /// Creates a data page using given data
        /// </summary>
        /// <param name="dataType">Make sure to give each dataType string a unique name</param>
        /// <param name="data">Required, data displayed on the y axis</param>
        /// <param name="hasWaterLimitSetter">Use this on water page</param>
        private void DataPage(string dataType, List<double> data, bool hasWaterLimitSetter = false)
        {
            panel1.Controls.Clear();
            panel1.HorizontalScroll.Maximum = 0;
            panel1.AutoScroll = true;
            chartSeries.Clear();
            Color color = ColorSustineri.Blue;
            if (dataType == TYPE_GAS) color = ColorSustineri.Green;

            timePeriod.Clear();
            timePeriod.AddRange(Enum.GetNames(typeof(Days)).Cast<string>().ToList());
            int dropDownWidth = avgChartWidth / 8;
            CreateControls dropDown = new CreateControls(new Point(avgChartPosX + avgChartWidth - dropDownWidth, firstChartPosY + avgLabelHeight), new Size(dropDownWidth, avgLabelHeight * 2), panel1, dataType);
            ComboBox comboBox = dropDown.CreateDropDown(new string[] { TYPE_WEEK, TYPE_MONTH }, font: FontSustineri.H2, roundCornerDiameter: textboxRoundness);
            comboBox.SelectedIndex = 0;
            comboBox.SelectedIndexChanged += new EventHandler(DropDownEvents);

            CreateControls viewingDate = new CreateControls(new Point(screenWidth / 2 - 200, firstChartPosY + avgLabelHeight), new Size(250, avgLabelHeight * 2), panel1);
            updatableLabels.Add(viewingDate.CreateLabel($"{WeekPicker()}", FontSustineri.H2));

            CreateControls previous = new CreateControls(new Point(viewingDate.ObjPoint.X - avgLabelHeight * 2, viewingDate.ObjPoint.Y), new Size(avgLabelHeight * 2, avgLabelHeight * 2), panel1, TYPE_WEEK);
            dateChanger = new List<Button>();
            dateChanger.Add(previous.CreateButton(DateChange, "<", font: FontSustineri.H2, roundCornerDiameter: textboxRoundness));
            CreateControls next = new CreateControls(new Point(viewingDate.ObjPoint.X + viewingDate.ObjSize.Width, viewingDate.ObjPoint.Y), new Size(avgLabelHeight * 2, avgLabelHeight * 2), panel1, TYPE_WEEK);
            dateChanger.Add(next.CreateButton(DateChange, ">", font: FontSustineri.H2, roundCornerDiameter: textboxRoundness));

            CreateCharts chart = new CreateCharts(new Point(avgChartPosX, firstChartPosY), new Size(avgChartWidth, avgChartHeight), panel1, dataType);
            chart.Design(SeriesChartType.Column, timePeriod, data, dataType, color);
            chartSeries.Add(chart.ChartObj.Series[0]);
            double total = chart.ChartObj.Series[0].Points.Sum(sum => sum.YValues.Sum());

            CreateControls totalLabel = new CreateControls(new Point(avgChartPosX, firstChartPosY + avgChartHeight), new Size(180, avgLabelHeight), panel1, dataType);
            updatableLabels.Add(totalLabel.CreateLabel($"Totaal: {total}", FontSustineri.H2, textAlignment: ContentAlignment.MiddleLeft));

            if (hasWaterLimitSetter)
            {
                CreateControls limLbl = new CreateControls(new Point(avgChartPosX, totalLabel.ObjPoint.Y + totalLabel.ObjSize.Height * 2), new Size(180, avgLabelHeight), panel1);
                limLbl.CreateLabel("Water limiet per beurt:", FontSustineri.H3, ContentAlignment.BottomLeft);
                CreateControls numUpDown = new CreateControls(new Point(limLbl.ObjPoint.X + limLbl.ObjSize.Width, limLbl.ObjPoint.Y), new Size(150, avgLabelHeight), panel1, "waterLimit");
                numUpDown.CreateNumBox(0, 1000000, roundCornerDiameter: textboxRoundness);
                CreateControls Llbl = new CreateControls(new Point(numUpDown.ObjPoint.X + numUpDown.ObjSize.Width, limLbl.ObjPoint.Y), new Size(20, avgLabelHeight), panel1);
                Llbl.CreateLabel("L", FontSustineri.H3, ContentAlignment.BottomLeft);
                CreateControls confirm = new CreateControls(new Point(Llbl.ObjPoint.X + Llbl.ObjSize.Width, Llbl.ObjPoint.Y), new Size(150, numUpDown.ObjSize.Height), panel1);
                confirm.CreateButton(SetLimit, "Instellen", FontSustineri.H3, color: ColorSustineri.Blue, roundCornerDiameter: textboxRoundness);
            }
        }

        List<TextBox> accInfoPageData;
        private void UserInfoPage()
        {
            panel1.Controls.Clear();
            panel1.HorizontalScroll.Maximum = 0;
            panel1.AutoScroll = true;
            accInfoPageData = new List<TextBox>();
            int labelWidth = 200;

            int line = 0;
            CreateField("Accountgegevens", labelWidth * 2, line++, FontSustineri.H1, ContentAlignment.MiddleLeft, fromCenter: false); line++;
            accInfoPageData.Add(CreateSingleLineInput("Gebruikersnaam", labelWidth, ++line, fromCenter: false).Ctrl as TextBox);
            accInfoPageData.Add(CreateSingleLineInput("Wachtwoord", labelWidth, ++line, isPassword: true, fromCenter: false).Ctrl as TextBox);

        }

        private string WeekPicker(int weekOffset = 0)
        {
            weekOffset = weekOffset * 7;
            var diff = DateTime.Now.DayOfWeek - DayOfWeek.Monday;
            var date = DateTime.Now.AddDays((-1 * diff) + weekOffset);

            requestDates = new List<string>();
            for (int i = 0; i < 7; i++) requestDates.Add($"{date.AddDays(i).Day}-{date.AddDays(i).Month}-{date.AddDays(i).Year}");

            string weekStart = $"{date.Day}-{date.Month}-{date.Year}";
            date = date.AddDays(6);
            string weekEnd = $"{date.Day}-{date.Month}-{date.Year}";

            return $"{weekStart} / {weekEnd}";
        }

        /* private string MonthPicker(int monthOffset = 0)
         {
             var date = DateTime.Now.AddMonths(monthOffset);
             requestDates = new List<string>();
             for (int i = 0; i < DateTime.DaysInMonth(date.Year, date.Month); i++)
             {
                 string day = "";
                 if (i < 10) day += "0";
                 day += i.ToString();
                 requestDates.Add($"{day}-{date.Month}-{date.Year}");
             }

             return $"{date.Year}";
         }*/

        private string YearPicker(int yearOffset = 0)
        {
            var date = new DateTime(day: 1, month: 1, year: DateTime.Now.AddYears(yearOffset).Year);

            requestDates = new List<string>();
            requestDates.Add($"{date.Year}");

            return $"{date.Year}";
        }

        private void SetLimit(object sender, EventArgs e)
        {
            if (panel1.Controls.Find("waterLimit", true) != null)
            {
                Control[] numUD = panel1.Controls.Find("waterLimit", true);
                waterLimit = (uint)Convert.ToInt32(numUD[0].Text);
                Console.WriteLine(waterLimit);
            }
        }

        /// <summary>
        /// Updates chart based on name of the chart and dropdown to selected data of: Dag, Week, Maand, Jaar
        /// </summary>
        private void DropDownEvents(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            dateOffset = 0;

            switch (comboBox.SelectedItem.ToString())
            {
                case TYPE_WEEK:
                    timePeriod = new List<string>();
                    timePeriod = Enum.GetNames(typeof(Days)).Cast<string>().ToList();
                    for (int i = 0; i < dateChanger.Count; i++) dateChanger[i].Name = TYPE_WEEK;
                    UpdateCharts(TYPE_WEEK);
                    break;
                #region
                /* case TYPE_MONTH:
                     dateActive = MonthPicker(); //starts at current month and updates request list
                     for (int i = 0; i < chartSeries.Count; i++) if (chartSeries[i].Name == comboBox.Name)
                         {
                             Refresh(chartSeries[i], y, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 1);
                             total += chartSeries[i].Points.Sum(total => total.YValues.Sum()).ToString();
                         }

                     break;*/
                #endregion

                case TYPE_MONTH:
                    timePeriod = new List<string>();
                    timePeriod = Enum.GetNames(typeof(Months)).Cast<string>().ToList();
                    for (int i = 0; i < dateChanger.Count; i++) dateChanger[i].Name = TYPE_MONTH;
                    UpdateCharts(TYPE_MONTH);
                    break;
            }
        }

        private void UpdateCharts(string message)
        {
            string dateText = "";
            string total = "";
            if (message == TYPE_MONTH) dateText = YearPicker(dateOffset);
            if (message == TYPE_WEEK) dateText = WeekPicker(dateOffset);
            for (int i = 0; i < chartSeries.Count; i++)
            {
                List<double> data = new List<double>();
                if (chartSeries[i].Name == TYPE_GAS) { data = gasData; }
                else if (chartSeries[i].Name == TYPE_WATER) { data = waterData; }

                //anti error code, for some reason when item is removed from data it also removes from waterData or gasData
                List<double> finalData = new List<double>();
                for (int j = 0; j < timePeriod.Count; j++) finalData.Add(data[j]);
                //*************************

                Refresh(chartSeries[i], timePeriod, finalData);
                total += chartSeries[i].Points.Sum(total => total.YValues.Sum()).ToString();
            }
            for (int i = 0; i < updatableLabels.Count; i++)
            {
                if (updatableLabels[i].Name == chartSeries[0].Name) Refresh(updatableLabels[i], $"Totaal: {total}");
                else Refresh(updatableLabels[i], dateText);
            }
        }

        private void DateChange(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == "<") dateOffset--;
            else if (dateOffset < 0) dateOffset++;
            //Need to update water & gas data here
            UpdateCharts(btn.Name);
        }

        /// <summary>
        /// Refreshes a chart series directly using given lists
        /// </summary>
        private void Refresh(Series series, List<string> x, List<double> y)
        {
            series.Points.DataBindXY(x, y);
        }

        /// <summary>
        /// Refreshes a chart series or label
        /// </summary>
        private void Refresh(Label label, string newText)
        {
            label.Text = newText;
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

    public enum Pages
    {
        Gas,
        Water,
        Home
    }

    /// <summary>
    /// Names in Dutch so it can also be used as tekst
    /// </summary>
    public enum BtnClickEvents
    {
        Login,
        Afmelden,
        Registreren,
        Terug,
        MaakGebruiker,
        Verversen,
        GebruikersInformatie
    }

    public enum Months
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

    public enum Days
    {
        maandag,
        dinsdag,
        woensdag,
        donderdag,
        vrijdag,
        zaterdag,
        zondag
    }
}
