using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tahorg.RCJoyGUI.Dialogs
{
    public partial class FlashBoardDialog : Form
    {
        private ComProgrammer __Port;
        public byte[] ProgramCode { get; set; }

        public FlashBoardDialog()
        {
            InitializeComponent();
            btnUpload.Enabled = false;

            cbComPort.Items.AddRange(SerialPort.GetPortNames().Cast<object>().ToArray());
            if (cbComPort.Items.Count > 0) cbComPort.SelectedIndex = 0;

            cbBoard.SelectedIndex = 2;
        }

        private delegate void ThreadSync();

        protected override void OnClosed(EventArgs e)
        {
            if (__Port != null) __Port.Dispose();
            base.OnClosed(e);
        }

        private void SetErrrorState(string text)
        {
            lblStatus.ForeColor = Color.Red;
            btnCancel.Enabled = true;
            lblStatus.Text = text;
        }

        private void SetSuccessState(string text)
        {
            lblStatus.ForeColor = Color.Green;
            btnCancel.Enabled = true;
            lblStatus.Text = text;
        }

        private void SetWorlingState(string text)
        {
            lblStatus.ForeColor = Color.Blue;
            lblStatus.Text = text;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (__Port != null)
                __Port.Dispose();



            try
            {
                __Port = new ComProgrammer((string)cbComPort.SelectedItem);
                __Port.OnStateChanged += PortOnOnStateChanged;

                __Port.Connect();
            }
            catch (Exception exx)
            {
                SetErrrorState("Connection error");
                MessageBox.Show(this, exx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PortOnOnStateChanged(ComProgrammer.enState state, uint bytes)
        {
            Invoke(new ThreadSync(() =>
            {

                switch (state)
                {
                    case ComProgrammer.enState.Connected:
                        SetSuccessState("Connected");
                        btnConnect.Enabled = false;
                        cbComPort.Enabled = false;
                        btnUpload.Enabled = true;
                        break;

                    case ComProgrammer.enState.Error:
                        SetErrrorState("Connection error");
                        btnUpload.Enabled = false;
                        break;

                    case ComProgrammer.enState.Erasing:
                        SetWorlingState("Erasing flash ...");
                        break;

                    case ComProgrammer.enState.EraseComplete:
                        SetWorlingState("Erase finished");
                        break;

                    case ComProgrammer.enState.Writing:
                        SetWorlingState("Writing ...");
                        break;

                    case ComProgrammer.enState.WriteComplete:
                        SetSuccessState("Complete");
                        btnCancel.DialogResult = DialogResult.OK;
                        btnCancel.Text = "Close";
                        btnUpload.Visible = false;
                        break;
                }
            }));
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (ofdFirmware.ShowDialog(this) == DialogResult.OK)
            {
                cbUploadFirmware.Checked = true;
                tbFirmware.Text = ofdFirmware.FileName;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (__Port == null) return;

            btnUpload.Enabled = false;
            btnCancel.Enabled = false;
            
            ComProgrammer.enBoardType bt = GetBoardType();
            
            new Task(() =>
            {
                byte[] firmware = null;

                if (cbUploadFirmware.Checked)
                {
                    if (!File.Exists(tbFirmware.Text))
                    {
                        MessageBox.Show(this, "Firmware file is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        firmware = File.ReadAllBytes(tbFirmware.Text);
                    }
                    catch (Exception exx)
                    {
                        MessageBox.Show(this, exx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                try
                {
                    if (firmware != null)
                    {
                        if (!__Port.WriteMainProgram(firmware))
                        {
                            MessageBox.Show(this, "Firmware upload failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (ProgramCode != null)
                    {
                        if (!__Port.WriteByteCode(bt, ProgramCode, firmware == null))
                        {
                            MessageBox.Show(this, "Configuration upload failed", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception exx)
                {

                    MessageBox.Show(this, exx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }).Start();
        }

        private ComProgrammer.enBoardType GetBoardType()
        {
            switch (cbBoard.SelectedIndex)
            {
                case 0:
                    return ComProgrammer.enBoardType.STM407VG;

                case 1:
                    return ComProgrammer.enBoardType.STM407VE;

                case 2:
                    return ComProgrammer.enBoardType.STM407VG;

                case 3:
                    return ComProgrammer.enBoardType.STM407ZE;

                default:
                    return ComProgrammer.enBoardType.STM407VG;
                /*
                Port 407VG (1024MB)
                Port 407VE (512MB)
                STMF4Discovery
                Port 407ZE (512MB)
                 */
            }
        }

        private void FlashBoardDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !btnCancel.Enabled;
        }
    }
}
