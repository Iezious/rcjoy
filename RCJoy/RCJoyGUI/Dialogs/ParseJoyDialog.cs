using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tahorg.RCJoyGUI.Dialogs
{
    public partial class ParseJoyDialog : Form
    {
        private readonly PortCommunicator __Port = new PortCommunicator();

        public ParseJoyDialog()
        {
            InitializeComponent();
            cbComPort.Items.AddRange(SerialPort.GetPortNames().Cast<object>().ToArray());
            if (cbComPort.Items.Count > 0)
                cbComPort.SelectedIndex = 0;

            __Port.OnDataRecieved += PortOnOnDataRecieved;
        }

        private void ParseJoyDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (__Port.IsOpen) __Port.Close();
        }

        public string DescriptorText
        {
            get
            {
                return tbValues.Text.Replace("\n", "").Replace("\r", "");
            }
        }

        private void btnComRead_Click(object sender, EventArgs e)
        {
            if (cbComPort.Items.Count == 0) return;

            btnComRead.Enabled = false;

            try
            {
                ReadCom();
            }
            catch (Exception exx)
            {
                MessageBox.Show(this, exx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnComRead.Enabled = true;
        }

        private int tries;

        private void ReadCom()
        {
            if (fmMain.__Port.IsOpen)
                fmMain.__Port.Close();

            btnComRead.Enabled = false;

            __Port.Open(cbComPort.SelectedItem.ToString(), 57600);
            __Port.SendCommand(1, null);

            tries = 0;

            new Task(() =>
            {
                Thread.Sleep(3000);

                Invoke(new ThreadSafeInvokeDelegate(() =>
                {
                    btnComRead.Enabled = true;
                    if (__Port.IsOpen) __Port.Close();
                }));

            }).Start();
        }

        private void PortOnOnDataRecieved(byte command, byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                if (tries < 5)
                {
                    tries++;
                    __Port.SendCommand(1, null);
                }

                return;
            }

            Invoke(new ThreadSafeInvokeDelegate(() =>
            {
                tbValues.Text = string.Join(" ", data.Select(b => b.ToString("X2")));
            }));
        }
    }
}
