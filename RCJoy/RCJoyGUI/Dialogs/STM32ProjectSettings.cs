using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.Dialogs
{
    public partial class STM32ProjectSettings : ValidatingForm
    {
        public STM32ProjectSettings()
        {
            InitializeComponent();
        }

        public ProjectSettings Data
        {
            get
            {
                var res = new ProjectSettings
                {
                    OutputPath = tbOutPath.Text,
                    BoardType = (ProjectSettings.enBoardType) cbBoard.SelectedIndex,

                    LCD_Enabled = cbLCDEnable.Checked,
                    LCD_BlackLight = cbLCDBL.Checked,
                    LCD_8080 = cbLCDParallel.Checked,
                    LCD_I2C_Addr = (byte) int.Parse(tbLCDI2CAddr.Text, NumberStyles.HexNumber),
                    
                    ROM_Enabled = cbROMEnable.Checked,
                    ROM_AddrStyle = (byte) cbROMAddrStyle.SelectedIndex,
                    ROM_Addr = (byte) int.Parse(tbROMAddr.Text, NumberStyles.HexNumber),
                    
                    PPMGenEnabled = cbPPMGenerator.Checked,
                    DebugEnabled = cbPCUSART.Checked,
                    XBeeEnabled = cbRetrUSART.Checked,
                    NRF_Enabled = cbRetrSPINRF.Checked
                };

                return res;
            }

            set
            {
                tbOutPath.Text = value.OutputPath;
                cbBoard.SelectedIndex = (int) value.BoardType;

                cbLCDEnable.Checked = value.LCD_Enabled;
                cbLCDBL.Checked = value.LCD_BlackLight;
                cbLCDParallel.Checked = value.LCD_8080;
                tbLCDI2CAddr.Text = value.LCD_I2C_Addr.ToString("X2",NumberFormatInfo.InvariantInfo);

                cbROMEnable.Checked = value.ROM_Enabled;
                cbROMAddrStyle.SelectedIndex = value.ROM_AddrStyle;
                tbROMAddr.Text = value.ROM_Addr.ToString("X2", NumberFormatInfo.InvariantInfo);

                cbPPMGenerator.Checked = value.PPMGenEnabled;
                cbPCUSART.Checked = value.DebugEnabled;
                cbRetrUSART.Checked = value.XBeeEnabled;
                cbRetrSPINRF.Checked = value.NRF_Enabled;
            }
        }

        private void btnSelectDir_Click(object sender, EventArgs e)
        {
            if (fsDialog.ShowDialog() == DialogResult.OK)
                tbOutPath.Text = fsDialog.FileName;
        }

        bool Check()
        {
            var ok = true;
            if (cbROMEnable.Checked)
                ok &= ValidateControl(tbROMAddr, () => ValidateInt(tbROMAddr.Text, NumberStyles.HexNumber));

            if (cbLCDEnable.Checked && !cbLCDParallel.Checked)
                ok &= ValidateControl(tbLCDI2CAddr, () => ValidateInt(tbLCDI2CAddr.Text, NumberStyles.HexNumber));

            return ok;
        }

        private void tbROMAddr_Leave(object sender, EventArgs e)
        {
            if (cbROMEnable.Checked)
                ValidateControl(tbROMAddr, () => ValidateInt(tbROMAddr.Text, NumberStyles.HexNumber));
        }

        private void tbLCDI2CAddr_Leave(object sender, EventArgs e)
        {
            if(cbLCDEnable.Checked && !cbLCDParallel.Checked)
                ValidateControl(tbLCDI2CAddr, () => ValidateInt(tbLCDI2CAddr.Text, NumberStyles.HexNumber));

        }

        private void cbRetrSPINRF_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRetrSPINRF.Checked)
                cbRetrUSART.Checked = false;
        }

        private void cbRetrUSART_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRetrUSART.Checked)
                cbRetrSPINRF.Checked = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Check())
                DialogResult = DialogResult.OK;
        }
    }
}
