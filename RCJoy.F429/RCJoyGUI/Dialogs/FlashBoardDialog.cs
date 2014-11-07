using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tahorg.RCJoyGUI.Dialogs
{
    public partial class FlashBoardDialog : Form
    {
        private ComProgrammer __Port;
        private int TotalLength;

        public FlashBoardDialog()
        {
            InitializeComponent();
            btnUpload.Enabled = false;

            cbComPort.Items.AddRange(SerialPort.GetPortNames().Cast<object>().ToArray());
            if (cbComPort.Items.Count > 0) cbComPort.SelectedIndex = 0;
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
                ShowError(exx.Message);
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
                        SetWorlingState(
                            TotalLength != 0 
                            ? String.Format("Writing {0:0}%", bytes * 100 / TotalLength)
                            : "Writing ... ");
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
//                cbFullFlashErase.Checked = true;
                tbFirmware.Text = ofdFirmware.FileName;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (__Port == null) return;


            const ComProgrammer.enBoardType bt = ComProgrammer.enBoardType.STM429IZ;

            if (!string.IsNullOrEmpty(tbFirmware.Text) && !File.Exists(tbFirmware.Text))
            {
                ShowError("Firmware file is not found");
                return;
            }

            byte[] firmware = null;

            if (!String.IsNullOrEmpty(tbFirmware.Text))
            {
                try
                {
                    firmware = File.ReadAllBytes(tbFirmware.Text);
                }
                catch (Exception exx)
                {
                    ShowError(exx.Message);
                    return;
                }
            }

            btnUpload.Enabled = false;
            btnCancel.Enabled = false;

            new Task(() =>
            {
                try
                {
                    if (firmware != null)
                    {
                        TotalLength = firmware.Length;
                        if (!__Port.WriteMainProgram(bt, firmware, cbFullFlashErase.Checked))
                        {
                            ShowError("Firmware upload failed");
                            return;
                        }
                    }

                    if (cbFullFlashErase.Checked) return;

                    if (cbEraseProgram.Checked && !__Port.EraseProgramArea(bt))
                    {
                        ShowError("Program erase failed");
                        return;
                    }

                    // ReSharper disable once RedundantJumpStatement
                    if (cbEraseVariables.Checked && !__Port.EraseVariablesArea(bt))
                    {
                        ShowError("Variables erase failed");
                        return;
                    }
                }
                catch (Exception exx)
                {

                    ShowError(exx.Message);
                }
                finally
                {
                    Invoke(new Action(() =>
                    {
                        btnUpload.Enabled = true;
                        btnCancel.Enabled = true;
                    }));
                }

            }).Start();
        }

        

        private void ShowError(string message)
        {
            if (!InvokeRequired)
            {
                MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }
        }

        private void FlashBoardDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !btnCancel.Enabled;
        }

    }
}
