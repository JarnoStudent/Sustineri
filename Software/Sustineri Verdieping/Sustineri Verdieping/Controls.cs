using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sustineri_Verdieping
{
	/// <summary>
	/// With this class you can create a Label, Button, NumericUpDown, TextBox or a PictureBox
	/// </summary>
	public class Controls
	{
		private Point ctrlPoint;
		private Size ctrlSize;
		public Control ctrlParent;
		private string name;		

		/// <summary>
		/// Objects available: Label, Button, NumericUpDown, TextBox, PictureBox
		/// </summary>
		/// <param name="point">Required, sets position x and y. 0,0 = TopLeft corner of the form</param>
		/// <param name="size">Required, sets size in pixels of the object, NumericUpDown does not use the y size you set to it</param>
		/// <param name="parent">Required, the object set to this will become the parent of the object</param>
		/// <param name="controlName">Optional, sets the name of the object</param>
		public Controls(Point point, Size size, Control parent, string controlName = "")
		{
			ctrlPoint = point;
			ctrlSize = size;
			ctrlParent = parent;
			name = controlName;
		}
		/// <summary>
		/// Creates a Label.
		/// </summary>
		/// <param name="text">Optional, sets text displayed in this object</param>
		/// <param name="font">Optional, sets the font of the text within this object</param>
		/// <param name="textAlignment">Optional, sets alignment of text within this object</param>
		/// <param name="color">Optional, sets background color of this object and if it is not white or empty text color will be white</param>
		/// <returns></returns>
		public Label CreateLabel(string text = "", Font font = null, ContentAlignment textAlignment = ContentAlignment.MiddleCenter, Color color = new Color())
		{
			Label label = new Label();
			label.Name = name;
			label.Location = ctrlPoint;
			label.Size = ctrlSize;
			label.Parent = ctrlParent;

			label.Text = text;
			label.TextAlign = textAlignment;
			if (font != null) label.Font = font;
			else label.Font = SustineriFont.TextFont;
			if (color.IsEmpty) color = Color.White;
			label.BackColor = color;
			if (color != Color.White) label.ForeColor = Color.White;

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
		/// <returns></returns>
		public Button CreateButton(EventHandler eHandler, string text = "", Font font = null, ContentAlignment textAlignment = ContentAlignment.MiddleCenter, Color color = new Color())
        {
			Button btn = new Button();
			btn.Name = name;
			btn.Location = ctrlPoint;
			btn.Size = ctrlSize;
			btn.Parent = ctrlParent;

			btn.Text = text;
			btn.TextAlign = textAlignment;
			if (font != null) btn.Font = font;
			else btn.Font = SustineriFont.TextFont;
			if (color.IsEmpty) color = Color.White;
			btn.BackColor = color;
			if (color != Color.White) btn.ForeColor = Color.White;

			btn.FlatStyle = FlatStyle.Flat;
			btn.FlatAppearance.BorderSize = 0;
			btn.Click += new EventHandler(eHandler);
			btn.BringToFront();

			return btn;
        }

		/// <summary>
		/// Creates a NumericUpDown
		/// </summary>
		/// <param name="min">Required, sets the minimum value of the NumericUpDown</param>
		/// <param name="max">Required, sets the maximum value of the NumericUpDown</param>
		/// <param name="font">Optional, sets the font of the text within this object</param>
		/// <returns></returns>
		public NumericUpDown CreateNumBox(int min, int max, Font font = null)
        {
			NumericUpDown numBox = new NumericUpDown();
			numBox.Name = name;
			numBox.Location = ctrlPoint;
			numBox.Size = ctrlSize;
			numBox.Parent = ctrlParent;

			numBox.Minimum = min;
			numBox.Maximum = max;
			numBox.Value = min;

			if (font != null) numBox.Font = font;
			else numBox.Font = SustineriFont.TextFont;
			numBox.BackColor = Color.White;

			numBox.BorderStyle = BorderStyle.FixedSingle;

			return numBox;
        }

		/// <summary>
		/// Creates a TextBox
		/// </summary>
		/// <param name="font">Optional, sets the font of the text within this object</param>
		/// <param name="isPassword">Optional, if set to true all characters will be replaced with '•'</param>
		/// <returns></returns>
		public TextBox CreateTextBox(Font font = null, bool isPassword = false)
        {
			TextBox txtBox = new TextBox();
			txtBox.Name = name;
			txtBox.Location = ctrlPoint;
			txtBox.Size = ctrlSize;
			txtBox.Parent = ctrlParent;

			if (font != null) txtBox.Font = font;
			else txtBox.Font = SustineriFont.TextFont;

			txtBox.BorderStyle = BorderStyle.None;
			if (isPassword) txtBox.PasswordChar = '•';

			return txtBox;
        }

		/// <summary>
		/// Creates a PictureBox
		/// </summary>
		/// <param name="image">Required, puts the image into the box</param>
		/// <param name="imgLayout">Optional, standard set to Stretch. Can be set to: None, Stretch, Center, Zoom or Tile mode </param>
		/// <param name="sendToBack">Optional, when set to true this object will be send to back</param>
		/// <returns></returns>
		public PictureBox CreatePicBox(Bitmap image, ImageLayout imgLayout = ImageLayout.Stretch, bool sendToBack = false)
        {
			PictureBox picBox = new PictureBox();
			picBox.Name = name;
			picBox.Location = ctrlPoint;
			picBox.Size = ctrlSize;
			picBox.Parent = ctrlParent;

			picBox.BackgroundImage = image;
			picBox.BackgroundImageLayout = imgLayout;

			if (sendToBack) picBox.SendToBack();

			return picBox;
        }
	}
}
