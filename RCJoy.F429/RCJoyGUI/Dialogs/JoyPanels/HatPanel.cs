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
    public partial class HatPanel : BaseControlPanel
    {
        public HatPanel()
        {
            InitializeComponent();
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            tbCName.Text = "HAT_" + wrex.Replace(tbName.Text, "").ToUpper();

        }

        private void tbName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbName, () => ValidateName(tbName.Text));
        }

        private void tbCName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbCName, () => ValidateCName(tbCName.Text));

        }

        private void tbMin_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbMin, () => ValidateInt(tbMin.Text));

        }

        private void tbMax_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbMax, () => ValidateInt(tbMax.Text));
        }


        private bool ValidateBits(string text, out int bits)
        {
            if (!int.TryParse(text, out bits))
            {
                return false;
            }

            return (bits > 0 || bits <= 8);
        }

        private void tbLength_Validating(object sender, CancelEventArgs e)
        {
            int bits = 0;

            e.Cancel = !ValidateControl(tbLength, () => ValidateBits(tbLength.Text, out bits));

            if (!e.Cancel)
            {
                tbMin.Text = "0";
                tbMax.Text = ((1 << bits) - 1).ToString(CultureInfo.InvariantCulture);
            }
        }

        public override void FillJoyInfo(JoystickConfig joy)
        {
            joy.Controls.Add(
                new HatSwitch
                    {
                        Name = tbName.Text,
                        ConstantName = tbCName.Text,
                        Length = int.Parse(tbLength.Text),
                        MinValue = int.Parse(tbMin.Text),
                        MaxValue = int.Parse(tbMax.Text)
                    }
                );
        }

        public override void FillFromJoyInfo(IJoystickControl control)
        {
            var axle = (HatSwitch)control;

            tbName.Text = axle.Name;
            tbCName.Text = axle.ConstantName;
            tbLength.Text = axle.Length.ToString(CultureInfo.InvariantCulture);
            tbMin.Text = axle.MinValue.ToString(CultureInfo.InvariantCulture);
            tbMax.Text = axle.MaxValue.ToString(CultureInfo.InvariantCulture);
        }

        public override bool Check()
        {
            int bits;

            if (
                ValidateControl(tbName, () => ValidateName(tbName.Text)) &
                ValidateControl(tbCName, () => ValidateCName(tbCName.Text)) &
                ValidateControl(tbMin, () => ValidateInt(tbMin.Text)) &
                ValidateControl(tbMax, () => ValidateInt(tbMax.Text)) &
                ValidateControl(tbLength, () => ValidateBits(tbLength.Text, out bits))
               )
            {
                int min;
                int max;

                if (
                    int.TryParse(tbMin.Text, out min) &&
                    int.TryParse(tbMax.Text, out max) &&
                        (
                            ValidateControl(tbMin, () => min < max) &
                            ValidateControl(tbMax, () => min < max)
                        )
                    )
                {
                    return true;
                }
            }

            return false;
        }
    }
}
