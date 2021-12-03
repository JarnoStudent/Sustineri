using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sustineri_Verdieping

public class Controls
{
	private Point ctrlPoint;
	private Size ctrlSize;
	private string name;

	public Controls( Point point, Size size, string controlName = "")
	{
		ctrlPoint = point;
		ctrlSize = size;
		name = controlName;
	}
	public Label CreateLabel(Controls parent, Color color, Font font, ContentAlignment textAlignment = ContentAlignment.MiddleCenter, string text = "")
    {
		Label label = new Label();
		label.Name = name;
		label.Location = ctrlPoint;
		label.Size = ctrlSize;
		label.Text = text;
		label.TextAlign = textAlignment;
		label.BackColor = color;
		if (color != Color.White) label.ForeColor = Color.White;
		label.Font = font;
		label.Parent = parent;
		return label;
    }
}
