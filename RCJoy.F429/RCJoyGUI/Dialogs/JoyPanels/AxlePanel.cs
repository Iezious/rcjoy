
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.JoyDialog
{
    public partial class AxlePanel : BaseControlPanel
    {
        public AxlePanel()
        {
            InitializeComponent();
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            tbCName.Text = "AX_" + wrex.Replace(tbName.Text, "").ToUpper();
        }

        public override void FillJoyInfo(JoystickConfig joy)
        {
            joy.Controls.Add(
                new JoystickAxle
                {
                    Name = tbName.Text,
                    CName = tbCName.Text,
                    Length = int.Parse(cbLength.Text),
                    MinValue = int.Parse(tbMin.Text),
                    MaxValue = int.Parse(tbMax.Text)
                });
        }

        public override void FillFromJoyInfo(IJoystickControl control)
        {
            var axle = (JoystickAxle)control;

            tbName.Text = axle.Name;
            tbCName.Text = axle.CName;
            cbLength.Text = axle.Length.ToString(CultureInfo.InvariantCulture);
            tbMin.Text = axle.MinValue.ToString(CultureInfo.InvariantCulture);
            tbMax.Text = axle.MaxValue.ToString(CultureInfo.InvariantCulture);
        }

        private void tbName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbName, () => ValidateName(tbName.Text));
        }

        private bool ValidateBits(string text, out int bits)
        {
            if (!int.TryParse(text, out bits))
            {
                return false;
            }

            return (bits > 0 || bits <= 16);
        }

        private void cbLength_Validating(object sender, CancelEventArgs e)
        {
            int bits = 0;

            e.Cancel = !ValidateControl(cbLength, () => ValidateBits(cbLength.Text, out bits));

            if (!e.Cancel)
            {
                tbMin.Text = "0";
                tbMax.Text = ((1 << bits) - 1).ToString(CultureInfo.InvariantCulture);
            }
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

        public override bool Check()
        {
            int bits;

            if (
                ValidateControl(tbName, () => ValidateName(tbName.Text)) &
                ValidateControl(tbCName, () => ValidateCName(tbCName.Text)) &
                ValidateControl(tbMin, () => ValidateInt(tbMin.Text)) &
                ValidateControl(tbMax, () => ValidateInt(tbMax.Text)) &
                ValidateControl(cbLength, () => ValidateBits(cbLength.Text, out bits))
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
