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

        private int standardRoundingDiameter = 10;

        Bitmap logoSustineri = Properties.Resources.Cas_sustineri_logo;
        Bitmap logoDroplet = Properties.Resources.Cas_sustineri_logo_NoWords;
        const int LOGO_SUSTINERI_X = 450 / 4 * 3, LOGO_SUSTINERI_Y = 126 / 4 * 3, LOGO_DROPLET_X = 235 / 4 * 3, LOGO_DROPLET_Y = 368 / 4 * 3; //DO NOT CHANGE VALUES

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
            panel3.BackColor = Color.FromArgb(240, 240, 240);
            this.Shown += new EventHandler(ExitBar);//becomes the login page later
        }

        enum Pages
        {
            Gas,
            Water,
            Home
        }

        private void ExitBar(object sender, EventArgs e)
        {
            int btnwidth = 60;
            Controls exitBtn = new Controls(new Point(screenWidth - btnwidth, 0), new Size(btnwidth, panel3.Height), panel3, "exit");
            exitBtn.CreateButton(CloseApp, "X", SustineriFont.H1, color: Color.Red);
            MenuMain();//will become login screen
        }


        private void MenuMain()
        {
            int btnWidth = screenWidth / 15;
            int borderWidth = 4;

            Controls logo = new Controls(new Point(btnWidth, (panel2.Height - LOGO_SUSTINERI_Y) / 2), new Size(LOGO_SUSTINERI_X, LOGO_SUSTINERI_Y), panel2, "logo");
            logo.CreatePicBox(logoSustineri);

            menuMainButtons = new List<Button>();

            for (int i = 0; i < Enum.GetNames(typeof(Pages)).Count(); i++)
            {
                string name = Enum.GetName(typeof(Pages), i);
                Controls btn = new Controls(new Point(panel2.Width - (btnWidth * (i + 1)), 0), new Size(btnWidth, panel2.Height - borderWidth), panel2, name);
                Button button = btn.CreateButton(PageSwitcher, name, SustineriFont.H1);
                if (button.Name == Pages.Home.ToString()) { button.BackColor = SustineriColor.Blue; button.ForeColor = Color.White; }
                menuMainButtons.Add(button);
            }

            Controls border = new Controls(new Point(0, panel2.Height - borderWidth), new Size(panel2.Width, borderWidth), panel2);
            themeBar = border.CreateLabel(color: SustineriColor.Blue);
        }

        private void PageSwitcher(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            //Recoloring button
            for (int i = 0; i < menuMainButtons.Count(); i++)
            {
                if (menuMainButtons[i].Name == ctrl.Name)
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

        }

        private void CloseApp(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
