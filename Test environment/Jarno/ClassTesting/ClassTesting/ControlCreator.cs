using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClassTesting
{
    class ControlCreator
    {
        private Point controlPoint;
        private Size controlSize;

        public ControlCreator(Point aControlPoint, Size aControlSize)
        {
            controlPoint = aControlPoint;
            controlSize = aControlSize;
        }

        public Label CreateLabel(string controlName, string controlText, ContentAlignment controlAlignment, Color controlColor, Font controlFont, Panel panelParent)
        {
            Label label = new Label();
            label.Name = controlName;
            label.Location = controlPoint;
            label.Size = controlSize;
            label.Text = controlText;
            label.TextAlign = controlAlignment;
            label.BackColor = controlColor;
            if (controlColor != Color.White) label.ForeColor = Color.White;
            label.Font = controlFont;
            label.Parent = panelParent;
            return label;
        }

        public Button CreateButton(string controlName, string controlText, ContentAlignment controlAlignment, Color controlColor, Font controlFont, Panel panelParent, EventHandler eHandler)
        {
            Button button = new Button();
            button.Name = controlName;
            button.Location = controlPoint;
            button.Size = controlSize;
            button.Text = controlText;
            button.TextAlign = controlAlignment;
            button.BackColor = controlColor;
            if (controlColor != Color.White) button.ForeColor = Color.White;
            button.Font = controlFont;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Parent = panelParent;
            button.Click += new EventHandler(eHandler);
            button.BringToFront();
            return button;
        }

        public NumericUpDown CreateNumBox(string controlName, int controlMin, int controlMax, Font controlFont, Panel panelParent)
        {
            NumericUpDown numericUpDown = new NumericUpDown();
            numericUpDown.Name = controlName;
            numericUpDown.Location = controlPoint;
            numericUpDown.Size = controlSize;
            numericUpDown.BackColor = Color.White;
            numericUpDown.Value = controlMin;
            numericUpDown.Minimum = controlMin;
            numericUpDown.Maximum = controlMax;
            numericUpDown.Font = controlFont;
            numericUpDown.BorderStyle = BorderStyle.FixedSingle;
            numericUpDown.ThousandsSeparator = false;
            numericUpDown.Parent = panelParent;
            return numericUpDown;
        }

        public TextBox CreateTextBox(string controlName, Font font, Panel panel, bool isPassword)
        {
            TextBox textBox = new TextBox();
            textBox.Name = controlName;
            textBox.Location = controlPoint;
            textBox.Size = controlSize;
            textBox.Font = font;
            textBox.BorderStyle = BorderStyle.None;
            textBox.Parent = panel;
            if (isPassword) { textBox.PasswordChar = '•'; panel.Controls.Add(textBox); }
            return textBox;
        }

        public PictureBox CreatePictureBox(string controlName, Bitmap imageBackGround, ImageLayout controlImageLayout, Panel panel)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Name = controlName;
            pictureBox.Location = controlPoint;
            pictureBox.Size = controlSize;
            pictureBox.BackgroundImage = imageBackGround;
            pictureBox.BackgroundImageLayout = controlImageLayout;
            pictureBox.Parent = panel;
            if (imageBackGround == default) { pictureBox.SendToBack(); }
            return pictureBox;
        }
    }
}
