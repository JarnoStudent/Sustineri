using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sustineri_Verdieping
{
    /// <summary>
    /// With this class you can create a Label, Button, NumericUpDown, TextBox or a PictureBox.
    /// For some Rounded corners are available
    /// </summary>
    public class Controls
    {
        public Point ObjPoint { get; protected set; }
        public Size ObjSize { get; protected set; }
        public Control ObjParent { get; protected set; }
        public string ObjName { get; protected set; }
        

        /// <summary>
        /// Rounds corners of control
        /// </summary>
        /// <param name="diameter">diameter of roundness</param>
        /// <param name="control">control that will be rounded</param>
        protected void RoundCorners(int diameter, Control control)
        {
            int quarter = 90;
            int half = 180;
            int threeQuarter = 270;
            Rectangle rectangle = new Rectangle(0, 0, control.Width, control.Height);
            System.Drawing.Drawing2D.GraphicsPath graphicsPath = new System.Drawing.Drawing2D.GraphicsPath();

            graphicsPath.AddArc(rectangle.X, rectangle.Y, diameter, diameter, half, quarter);
            graphicsPath.AddArc(rectangle.X + rectangle.Width - diameter, rectangle.Y, diameter, diameter, threeQuarter, quarter);
            graphicsPath.AddArc(rectangle.X + rectangle.Width - diameter, rectangle.Y + rectangle.Height - diameter, diameter, diameter, 0, quarter);
            graphicsPath.AddArc(rectangle.X, rectangle.Y + rectangle.Height - diameter, diameter, diameter, quarter, quarter);

            control.Region = new Region(graphicsPath);
        }
    }


    public class CreateControls : Controls
    {
        private Control ctrl = null;
        /// <summary>
        /// Objects available: Label, Button, NumericUpDown, TextBox, PictureBox
        /// </summary>
        /// <param name="point">Required, sets position x and y. 0,0 = TopLeft corner of the form</param>
        /// <param name="size">Required, sets size in pixels of the object, NumericUpDown does not use the y size you set to it</param>
        /// <param name="parent">Required, the object set to this will become the parent of the object</param>
        /// <param name="controlName">Optional, sets the name of the object</param>
        public CreateControls(Point point, Size size, Control parent, string controlName = "")
        {
            ObjPoint = point;
            ObjSize = size;
            ObjParent = parent;
            ObjName = controlName;
        }
        /// <summary>
        /// Creates a Label.
        /// </summary>
        /// <param name="text">Optional, sets text displayed in this object</param>
        /// <param name="font">Optional, sets the font of the text within this object</param>
        /// <param name="textAlignment">Optional, sets alignment of text within this object</param>
        /// <param name="color">Optional, sets background color of this object and if it is not white or empty text color will be white</param>
        /// <param name="roundedCornerDiameter">Optional, sets diameter of rounded corners</param>
        /// <returns></returns>
        public Label CreateLabel(string text = "", Font font = null, ContentAlignment textAlignment = ContentAlignment.MiddleCenter, Color color = new Color(), int roundedCornerDiameter = 0)
        {
            Label label = new Label();
            label.Name = ObjName;
            label.Location = ObjPoint;
            label.Size = ObjSize;
            label.Parent = ObjParent;

            label.Text = text;
            label.TextAlign = textAlignment;
            if (font != null) label.Font = font;
            else label.Font = SustineriFont.TextFont;
            if (color.IsEmpty) color = Color.White;
            label.BackColor = color;
            if (color != Color.White) label.ForeColor = Color.White;

            if (roundedCornerDiameter > 0) RoundCorners(roundedCornerDiameter, label);
            label.BringToFront();
            ctrl = label;

            return label;
        }


        /// <summary>
        /// Creates a Button.
        /// </summary>
        /// <param name="eHandler">Required: EventHandler</param>
        /// <param name="text">Optional, sets text displayed in this object</param>
        /// <param name="font">Optional, sets the font of the text within this object</param>
        /// <param name="textAlignment">Optional, sets alignment of text within this object</param>
        /// <param name="color">Optional, sets background color of this object and if it is white or empty, text color will be white</param>
        /// <param name="roundCornerDiameter">Optional, sets diameter of rounded corners</param>
        /// <returns></returns>
        public Button CreateButton(EventHandler eHandler, string text = "", Font font = null, ContentAlignment textAlignment = ContentAlignment.MiddleCenter, Color color = new Color(), int roundCornerDiameter = 0, Bitmap image = null)
        {
            Button btn = new Button();
            btn.Name = ObjName;
            btn.Location = ObjPoint;
            btn.Size = ObjSize;
            btn.Parent = ObjParent;

            btn.Text = text;
            btn.TextAlign = textAlignment;
            if (font != null) btn.Font = font;
            else btn.Font = SustineriFont.TextFont;
            if (color.IsEmpty) color = Color.White;
            btn.BackColor = color;
            if (color != Color.White || image != null) btn.ForeColor = Color.White;
            if (image != null) btn.BackgroundImage = image;
            btn.BackgroundImageLayout = ImageLayout.Zoom;
            btn.Cursor = Cursors.Hand;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += new EventHandler(eHandler);
            btn.BringToFront();

            if (roundCornerDiameter > 0) RoundCorners(roundCornerDiameter, btn);
            ctrl = btn;

            return btn;
        }

        /// <summary>
        /// Creates a NumericUpDown
        /// </summary>
        /// <param name="min">Required, sets the minimum value of the NumericUpDown</param>
        /// <param name="max">Required, sets the maximum value of the NumericUpDown</param>
        /// <param name="font">Optional, sets the font of the text within this object</param>
        /// <param name="roundedCornerDiameter">Optional, sets diameter of rounded corners</param>
        /// <returns></returns>
        public NumericUpDown CreateNumBox(int min, int max, Font font = null, Color color = new Color(), int roundedCornerDiameter = 0)
        {
            NumericUpDown numBox = new NumericUpDown();
            numBox.Name = ObjName;
            numBox.Location = ObjPoint;
            numBox.Size = ObjSize;
            numBox.Parent = ObjParent;

            numBox.Minimum = min;
            numBox.Maximum = max;
            numBox.Value = min;

            if (font != null) numBox.Font = font;
            else numBox.Font = SustineriFont.TextFont;
            if (color.IsEmpty) color = Color.White;
            numBox.BackColor = color;

            numBox.BorderStyle = BorderStyle.FixedSingle;
            if (roundedCornerDiameter > 0) RoundCorners(roundedCornerDiameter, numBox);
            numBox.BringToFront();
            ctrl = numBox;

            return numBox;
        }

        /// <summary>
        /// Creates a TextBox
        /// </summary>
        /// <param name="font">Optional, sets the font of the text within this object</param>
        /// <param name="isPassword">Optional, if set to true all characters will be replaced with '•'</param>
        /// <param name="roundedCornerDiameter">Optional, sets diameter of rounded corners</param>
        /// <returns></returns>
        public TextBox CreateTextBox(Font font = null, bool isPassword = false, Color color = new Color(), int roundedCornerDiameter = 0)
        {
            TextBox txtBox = new TextBox();
            txtBox.Name = ObjName;
            txtBox.Location = ObjPoint;
            txtBox.Size = ObjSize;
            txtBox.Parent = ObjParent;

            if (font != null) txtBox.Font = font;
            else txtBox.Font = SustineriFont.TextFont;
            txtBox.MinimumSize = ObjSize;
            if (color.IsEmpty) color = Color.White;
            txtBox.BackColor = color;

            txtBox.BorderStyle = BorderStyle.None;
            if (isPassword) txtBox.PasswordChar = '•';

            if (roundedCornerDiameter > 0) RoundCorners(roundedCornerDiameter, txtBox);
            txtBox.BringToFront();
            ctrl = txtBox;

            return txtBox;
        }

        /// <summary>
        /// Creates a PictureBox
        /// </summary>
        /// <param name="image">Required, puts the image into the box</param>
        /// <param name="imgLayout">Optional, standard set to Stretch. Can be set to: None, Stretch, Center, Zoom or Tile mode </param>
        /// <param name="sendToBack">Optional, when set to true this object will be send to back</param>
        /// <param name="roundedCornerDiameter">Optional, sets diameter of rounded corners</param>
        /// <returns></returns>
        public PictureBox CreatePicBox(Bitmap image = null, Color color = new Color(), ImageLayout imgLayout = ImageLayout.Stretch, bool sendToBack = false, int roundedCornerDiameter = 0, int bleed = 0)
        {
            PictureBox picBox = new PictureBox();
            picBox.Name = ObjName;
            picBox.Location = new Point(ObjPoint.X - bleed, ObjPoint.Y - bleed);
            picBox.Size = new Size(ObjSize.Width + bleed * 2, ObjSize.Height + bleed * 2);
            picBox.Parent = ObjParent;

            if (color.IsEmpty) color = Color.White;
            picBox.BackColor = color;
            if (image != null) picBox.BackgroundImage = image;
            picBox.BackgroundImageLayout = imgLayout;

            if (sendToBack) picBox.SendToBack();
            else picBox.BringToFront();
            if (roundedCornerDiameter > 0) RoundCorners(roundedCornerDiameter, picBox);
            ctrl = picBox;

            return picBox;
        }

    }
}
