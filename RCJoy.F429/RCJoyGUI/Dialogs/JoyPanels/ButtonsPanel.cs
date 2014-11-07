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
    public partial class ButtonsPanel : BaseControlPanel
    {
        public ButtonsPanel()
        {
            InitializeComponent();
            lblHead.Text = "Buttons";

//            formToolTip.SetToolTip(lblName, "Field name");
//            formToolTip.SetToolTip(lblCName, "Name of constants for define");
//            formToolTip.SetToolTip(lblCount, "Number of buttons");
//            formToolTip.SetToolTip(lblSize, "Bits per button");
//
//            tbSize.Text = "1";
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            tbCName.Text = "BTN_" + wrex.Replace(tbName.Text, "").ToUpper();
        }

        private void tbCName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbCName, () => ValidateCName(tbCName.Text));
        }

        private void tbName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbName, () => ValidateName(tbName.Text));

        }

        private void tbCount_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbCount, () => ValidateInt(tbCount.Text));

        }

        private void tbSize_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbSize, () => ValidateInt(tbSize.Text));
        }

        public override void FillJoyInfo(JoystickConfig joy)
        {
            joy.Controls.Add(
                new ButtonsCollection()
                    {
                        Name = tbName.Text,
                        ConstantName = tbCName.Text,
                        ButtonsCount = int.Parse(tbCount.Text),
                        ButtonsStateBits = int.Parse(tbSize.Text)
                    }
                );
        }

        public override void FillFromJoyInfo(IJoystickControl control)
        {
            var buttons = (ButtonsCollection) control;

            tbName.Text = buttons.Name;
            tbCName.Text = buttons.ConstantName;
            tbCount.Text = buttons.ButtonsCount.ToString(CultureInfo.InvariantCulture);
            tbSize.Text = buttons.ButtonsStateBits.ToString(CultureInfo.InvariantCulture);
        }

        public override bool Check()
        {
            if (
                    ValidateControl(tbName, () => ValidateName(tbName.Text)) &
                    ValidateControl(tbCName, () => ValidateCName(tbCName.Text)) &
                    ValidateControl(tbSize, () => ValidateInt(tbSize.Text)) &
                    ValidateControl(tbCount, () => ValidateInt(tbCount.Text))
                )
            {
                    return true;
            }

            return false;
        }
    }
}
