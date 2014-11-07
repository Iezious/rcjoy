using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tahorg.RCJoyGUI
{
    public partial class ValidatePanel : UserControl
    {
        public ValidatePanel()
        {
            InitializeComponent();
        }

        protected static readonly Regex wrex = new Regex(@"\W+");
        protected static readonly Regex checkRex = new Regex(@"^\w+$");

        protected bool ValidateControl(Control c, Func<bool> validator)
        {
            if (!validator())
            {
                c.BackColor = Color.Red;
                return false;
            }

            c.BackColor = SystemColors.Window;
            return true;
        }


        protected bool ValidateCName(string cname)
        {
            return checkRex.IsMatch(cname);
        }

        protected bool ValidateInt(string text)
        {
            int i;
            return int.TryParse(text, out i);
        }

        protected bool ValidateAxis(string text)
        {
            int i;
            return int.TryParse(text, out i) && (i >= 0) && (i<=1000);
        }

        protected bool ValidateName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }
    }
}
