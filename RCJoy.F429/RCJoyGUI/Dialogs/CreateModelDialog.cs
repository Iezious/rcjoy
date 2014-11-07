using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.Dialogs
{
    public partial class CreateModelDialog : ValidatingForm
    {
        public CreateModelDialog()
        {
            InitializeComponent();
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            tbCName.Text = wrex.Replace(tbName.Text, "").ToUpper();
        }

        private void tbName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbName, () => !string.IsNullOrWhiteSpace(tbName.Text));
        }

        private void tbCName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbCName, () => checkRex.IsMatch(tbCName.Text));
        }

        private void tbMin_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbMin, () => ValidateInt(tbMin.Text));
        }

        private void tbCenter_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbCenter, () => ValidateInt(tbCenter.Text));
        }

        private void tbMax_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbMax, () => ValidateInt(tbMax.Text));
        }

        private bool Check()
        {
            if (rbSysMenu.Checked)
            {
                return false;
            }

            if (
                ValidateControl(tbName, () => !string.IsNullOrWhiteSpace(tbName.Text)) &
                ValidateControl(tbCName, () => checkRex.IsMatch(tbCName.Text)) &
                ValidateControl(tbMin, () => ValidateInt(tbMin.Text)) &
                ValidateControl(tbMax, () => ValidateInt(tbMax.Text)) &
                ValidateControl(tbCenter, () => ValidateInt(tbCenter.Text))
               )
            {
                int min;
                int max;
                int center;

                if (
                    int.TryParse(tbMin.Text, out min) &&
                    int.TryParse(tbCenter.Text, out center) &&
                    int.TryParse(tbMax.Text, out max) &&
                        (
                            ValidateControl(tbMin, () => min < center && center < max ) &
                            ValidateControl(tbCenter, () => min < center && center < max) &
                            ValidateControl(tbMax, () => min < center && center < max)
                        )
                    )
                {
                    return true;
                }
            }

            return false;
        }

        public ModelInfo Model
        {
            get
            {
                return Check()
                           ? new ModelInfo
                               {
                                   Name = tbName.Text,
                                   CName = tbCName.Text,
                                   Channels = int.Parse(cbChannels.SelectedItem.ToString()),
                                   PPMMin = int.Parse(tbMin.Text),
                                   PPMCenter = int.Parse(tbCenter.Text),
                                   PPMMax = int.Parse(tbMax.Text),
                                   IsMenu = rbSysMenu.Checked,
                               }
                           : null;
            }
            set
            {
                if (value == null)
                {
                    tbName.Text = tbCName.Text = "";
                    cbChannels.SelectedItem = "8";

                    tbMin.Text = 1000.ToString(CultureInfo.InvariantCulture);
                    tbMax.Text = 2000.ToString(CultureInfo.InvariantCulture);
                    tbCenter.Text = 1500.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    tbName.Text = value.Name;
                    tbCName.Text = value.CName;
                    cbChannels.SelectedItem = value.Channels.ToString(CultureInfo.InvariantCulture);
                    tbMin.Text = value.PPMMin.ToString(CultureInfo.InvariantCulture);
                    tbCenter.Text = value.PPMCenter.ToString(CultureInfo.InvariantCulture);
                    tbMax.Text = value.PPMMax.ToString(CultureInfo.InvariantCulture);

                    if (Model.IsMenu)
                    {
                        rbSysMenu.Checked = true;
                        gModelData.Enabled = gPPMData.Enabled = false;
                    }
                    else
                    {
                        rbRCModel.Checked = true;
                        gbModelType.Enabled = false;
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (rbSysMenu.Checked)
                MessageBox.Show(this, "System tab is not supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (!Check()) return;

            DialogResult = DialogResult.OK;
        }

        private void rbRCModel_CheckedChanged(object sender, EventArgs e)
        {
            gModelData.Enabled = gPPMData.Enabled = rbRCModel.Checked;
        }
    }
}
