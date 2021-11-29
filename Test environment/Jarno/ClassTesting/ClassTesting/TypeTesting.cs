using System.Linq;
using System.Windows.Forms;

namespace ClassTesting
{
    class TypeTesting
    {
        private int id;
        private string name;
        private string lastname;

        public string Name
        {
            get => name;
            set
            {
                if (value.All(char.IsLetter))
                {
                    name = value;
                }
                else
                {
                    MessageBox.Show("Name can only contain letters.");
                }
            }
        }

        public override string ToString()
        {
            return name;
        }
    }
}
