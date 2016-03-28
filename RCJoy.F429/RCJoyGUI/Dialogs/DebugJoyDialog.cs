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
using Timer = System.Threading.Timer;

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
                pnlButtons.Enabled = true;
            }
            else
            {
                lblPortState.Text = Resources.ParseJoyDialog_ParseJoyDialog_Not_connected;
                lblPortState.ForeColor = Color.Red;
                pnlButtons.Enabled = false;

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") return;

            if (dlgSave.ShowDialog(this) != DialogResult.OK) return;

            using (var sw = new StreamWriter(dlgSave.FileName, false, Encoding.UTF8))
                sw.Write(textBox1.Text);
        }

        private void btnReadStates_Click(object sender, EventArgs e)
        {
            byte[] bytes;

            if (PortDataComm.SendCommand(0x0D, out bytes) != PortDataComm.CommandStatus.OK)
            {
                MessageBox.Show(this, "Error", "Unable to read statuses", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            textBox1.Lines = new[]
            {
                $"Host->gState:{bytes[0]:X2}",
                $"Host->EnumState:{bytes[1]:X2}",
                $"Host->RequestState:{bytes[2]:X2}",
                $"Host->Control.state:{bytes[3]:X2}",
                $"Host->device.speed:{bytes[4]:X2}",
                $"HID->State:{bytes[5]:X2}",
                $"HID->ContrlolState:{bytes[6]:X2}",
                $"ReportLength:{bytes[7]:0}",
                $"MiltiRerport:{bytes[8]:0}",
                $"DataFlow:{bytes[9]:0}",
            };
        }

        private bool IsCollecting = false;
        private bool StopCollection = false;

        private void ModeNotCollecting()
        {
            btnComRead.Text = "Start collecting";
            btnSave.Enabled = true;
            lblPortState.ForeColor = Color.Green;
            IsCollecting = false;
        }

        private void ModeCollecting()
        {
            btnSave.Enabled = false;
            lblPortState.ForeColor = Color.Blue;
            btnComRead.Text = "Stop collecting";
        }

        private void StartCollectionTask()
        {
            IsCollecting = true;
            StopCollection = false;

            ModeCollecting();
            textBox1.Text = "";

            new Task(ExecuteCollection).Start();
        }

        private void ExecuteCollection()
        {
            byte[] bytes;

            while (!StopCollection)
            {
                if (PortDataComm.SendCommand(0x08, out bytes) != PortDataComm.CommandStatus.OK)
                {
                    Invoke(new Action(ModeNotCollecting));
                    return;
                }

                Invoke(new Action(() =>
                {
                    var line = string.Join(" ", bytes.Select(b => b.ToString("X2")));
                    textBox1.Lines = textBox1.Lines.Union(new[] {line}).ToArray();
                }));

                Thread.Sleep(200);
            }

            Invoke(new Action(ModeNotCollecting));
        }


        private void btnComRead_Click(object sender, EventArgs e)
        {
            if (IsCollecting)
            {
                StopCollection = true;
            }
            else
            {
                StartCollectionTask();
            }
        }
    }
}
