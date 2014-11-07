using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tahorg.RCJoyGUI.Data;
using Tahorg.RCJoyGUI.Properties;

namespace Tahorg.RCJoyGUI.Dialogs
{
    public partial class DebugJoyDialog : Form
    {
        public DebugJoyDialog()
        {
            InitializeComponent();

            if (PortDataComm.IsOpen)
            {
                lblPortState.Text = Resources.ParseJoyDialog_ParseJoyDialog_Connected;
                lblPortState.ForeColor = Color.Green;
                btnComRead.Enabled = true;
            }
            else
            {
                lblPortState.Text = Resources.ParseJoyDialog_ParseJoyDialog_Not_connected;
                lblPortState.ForeColor = Color.Red;
                btnComRead.Enabled = false;

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") return;

            if (dlgSave.ShowDialog(this) != DialogResult.OK) return;

            using (var sw = new StreamWriter(dlgSave.FileName, false, Encoding.UTF8))
                sw.Write(textBox1.Text);
        }

        private void btnComRead_Click(object sender, EventArgs e)
        {
            byte[] bytes;

            if (PortDataComm.SendCommand(0x0B, out bytes) != PortDataComm.CommandStatus.OK)
            {
                MessageBox.Show(this, "Error", "Unable to start data collection", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            btnComRead.Enabled = false;
            btnSave.Enabled = false;
            lblPortState.ForeColor = Color.Blue;
            textBox1.Text = "";

            new Task(() =>
            {
                for (var t = 0; t < 20; t++)
                {
                    Invoke(new Action(() =>
                    {
                        lblPortState.Text = "Collecting: " + t;
                    }));
                    Thread.Sleep(1000);

                }

                if (PortDataComm.SendCommand(0x0C, out bytes) != PortDataComm.CommandStatus.OK)
                {

                    Invoke(new Action(() =>
                    {
                        MessageBox.Show(this, "Error", "Unable to retreve collected data", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        lblPortState.Text = "Collecting";
                        lblPortState.ForeColor = Color.Red;

                        EnableAll();
                    }));

                    return;
                }

                Invoke(new Action(() =>
                {
                    ParseData(bytes);
                    lblPortState.Text = "Ready";
                    lblPortState.ForeColor = Color.Green;
                    EnableAll();
                }));

            }).Start();
        }

        private void ParseData(byte[] bytes)
        {
            if (bytes.Length == 0) return;
            var lines = new List<string>(1000);

            int len = bytes[0];

            lines.Add(len.ToString("X2"));

            if (len > 0)
            {
                for (int i = 1; i < bytes.Length; i += len)
                {
                    lines.Add(string.Join(" ", bytes.Skip(i).Take(len).Select(b => b.ToString("X2"))));
                }
            }

            textBox1.Lines = lines.ToArray();
        }

        private void EnableAll()
        {
            btnComRead.Enabled = true;
            btnSave.Enabled = true;
        }
    }
}
