using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.JoyDialog
{
    public partial class DummyPanel : BaseControlPanel
    {
        public DummyPanel()
        {
            InitializeComponent();
        }

        private void tbSize_Validating(object sender, CancelEventArgs e)
        {
            int bits = 0;
            e.Cancel = !ValidateControl(tbSize, () => ValidateBits(tbSize.Text, out bits));
        }

        public override bool Check()
        {
            int bits = 0;
            return ValidateControl(tbSize, () => ValidateBits(tbSize.Text, out bits));
        }

        private bool ValidateBits(string text, out int bits)
        {
            if (!int.TryParse(text, out bits))
            {
                return false;
            }

            return (bits > 0 || bits <= 8);
        }

        public override void FillJoyInfo(JoystickConfig joy)
        {
            joy.Controls.Add(new LevelingBits
                {
                    Length = int.Parse(tbSize.Text),
                    Name = "DMMY"
                });
        }

        public override void FillFromJoyInfo(IJoystickControl control)
        {
            tbSize.Text = ((LevelingBits) control).Length.ToString(CultureInfo.InvariantCulture);
        }
    }
}
