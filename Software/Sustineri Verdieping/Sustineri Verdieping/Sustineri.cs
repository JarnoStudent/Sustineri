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
        public Sustineri()
        {
            InitializeComponent();
            this.Shown += new EventHandler(ShowLabelTest);
        }
        private void ShowLabelTest(object sender, EventArgs e)
        {
            Controls ctrl = new Controls(new Point(50, 0), new Size(100, panel2.Height), panel2, "lbl");
            Label lbl =  ctrl.CreateLabel(text: "TestingLabel",color:Color.Red);
        }
    }
}
