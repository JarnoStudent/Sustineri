using System.Drawing;

namespace Sustineri_Verdieping
{
    public struct ColorSustineri
    {
        public static Color Blue { get { return Color.FromArgb(6, 142, 200); } }
        public static Color Green { get { return Color.FromArgb(5, 150, 114); } }
    }

    public struct FontSustineri
    {
        public static Font H1 { get { return new Font("Arial", 14, FontStyle.Bold); } }
        public static Font H2 { get { return new Font("Arial", 12); } }
        public static Font TextFont { get { return new Font("Arial", 10); } }
    }

}
