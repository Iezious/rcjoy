using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Tahorg.RCJoyGUI.Data;
using Tahorg.RCJoyGUI.Properties;

namespace Tahorg.RCJoyGUI.Dialogs
{
    public partial class SendDebugJoyDialog : Form
    {
        public SendDebugJoyDialog()
        {
            InitializeComponent();


            if (PortDataComm.IsOpen)
            {
                lblPortState.Text = Resources.ParseJoyDialog_ParseJoyDialog_Connected;
                lblPortState.ForeColor = Color.Green;
                pnlButtons.Enabled = true;
            }
            else
            {
                lblPortState.Text = Resources.ParseJoyDialog_ParseJoyDialog_Not_connected;
                lblPortState.ForeColor = Color.Red;
                pnlButtons.Enabled = false;
            }

            btnComSend.Enabled = false;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if(ofdLog.ShowDialog(this) != DialogResult.OK) return;

            lbLines.Items.Clear();

            using (var txtfile = File.OpenText(ofdLog.FileName))
            {
                string line;

                while ((line = txtfile.ReadLine()) != null)
                {
                    lbLines.Items.Add(line);
                }
            }
        }

        private void lbLines_SelectedValueChanged(object sender, EventArgs e)
        {
            btnComSend.Enabled = lbLines.SelectedItem != null;
        }

        private void btnComSend_Click(object sender, EventArgs e)
        {
            var line = (string)lbLines.SelectedItem;
            if(line == null) return;

            var values = line.Split(' ').Select(vvs => byte.Parse(vvs, NumberStyles.HexNumber)).ToArray();
            byte[] bytes;


            if (PortDataComm.SendCommand(0x0E, out bytes, values) != PortDataComm.CommandStatus.OK)
            {
                MessageBox.Show("Command execution fails", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
