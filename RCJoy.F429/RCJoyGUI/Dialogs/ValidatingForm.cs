using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Tahorg.RCJoyGUI.Dialogs
{
    public class ValidatingForm : Form
    {

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

        protected bool ValidateInt(string text)
        {
            int i;
            return int.TryParse(text, out i);
        }
        protected bool ValidateInt(string text, NumberStyles style)
        {
            int i;
            return int.TryParse(text, style, NumberFormatInfo.InvariantInfo, out i);
        }
    }
}
