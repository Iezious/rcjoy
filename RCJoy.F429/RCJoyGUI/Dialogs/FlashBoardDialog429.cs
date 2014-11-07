using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tahorg.RCJoyGUI.Data;
using Tahorg.RCJoyGUI.Properties;

namespace Tahorg.RCJoyGUI.Dialogs
{
    public partial class FlashBoardDialog429 : Form
    {
        public byte[] ProgramCode { get; set; }

        public FlashBoardDialog429()
        {
            InitializeComponent();
            btnUpload.Enabled = PortDataComm.IsOpen;

            if (PortDataComm.IsOpen)
                SetWorlingState("Port is ready");
            else
                SetErrrorState("Not connected");
        }

        private delegate void ThreadSync();


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

        private void SetStatus(int state, int done, int total)
        {
            Invoke(new ThreadSync(() =>
            {
                if (state < 0)
                {
                    SetErrrorState(string.Format("Upload error: {0}, restart board", state));
                    return;
                }

                switch (state)
                {
                    case 1:
                        SetWorlingState("Erasing flash block ...");
                        pbUploaded.Minimum = 0;
                        pbUploaded.Maximum = total + 10;
                        break;

                    case 2:
                        SetWorlingState("Erase finished");
                        pbUploaded.Value = 10;

                        break;

                    case 3:
                        SetWorlingState(string.Format("Writing {0}/{1} ...", done, total));
                        pbUploaded.Value = done + 10;

                        break;

                    case 4:
                        SetSuccessState("Complete, restart board");
                        btnCancel.DialogResult = DialogResult.OK;
                        btnCancel.Text = CommonResources.Close;
                        btnUpload.Visible = false;
                        pbUploaded.Value = pbUploaded.Maximum;
                        break;
                }
            }));
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            btnUpload.Enabled = false;
            btnCancel.Enabled = false;

            new Task(() =>
            {
                try
                {
                    DoUploadCode();
                }
                catch (Exception exx)
                {
                    Invoke(new Action(() =>
                    {
                        btnUpload.Enabled = true;
                        btnCancel.Enabled = true;
                        MessageBox.Show(this, exx.Message, CommonResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }

            }).Start();
        }

        private void FlashBoardDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !btnCancel.Enabled;
        }

        private void FlashBoardDialog429_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void DoUploadCode()
        {
            SetStatus(1, 0, ProgramCode.Length);

            if (!DoSendPrepareCommand()) return;
            if (!DoErasePrepareCommand()) return;
            //            if (!DoWaitReadyStatus()) return;

            SetStatus(2, 0, ProgramCode.Length);

            Thread.Sleep(250);

            int cursor = 0;
            var l = ProgramCode.Length;
            while (cursor < l)
            {
                if (!SendChunk(ref cursor)) return;
                SetStatus(3, cursor, l);
            }

            if (!DoSendFinishedCommand())
            {
                SetStatus(-28, 0, 1);
                return;
            }

            SetStatus(4, l, l);

        }

        private bool DoSendPrepareCommand()
        {
            byte[] ret;

            if (PortDataComm.SendCommand(0x60, out ret) != PortDataComm.CommandStatus.OK || ret == null || ret.Length != 1 || ret[0] != 1)
            {
                SetStatus(-1, 0, 1);
                return false;
            }


            return true;
        }

        private bool DoErasePrepareCommand()
        {
            Thread.Sleep(100);
            byte[] ret;
            if (PortDataComm.SendLongRunCommand(0x61, out ret, 5000) != PortDataComm.CommandStatus.OK || ret == null || ret.Length != 1 || ret[0] != 1)
            {
                SetStatus(-2, 0, 1);
                return false;
            }

            return true;
        }

        private bool DoSendFinishedCommand()
        {
            byte[] ret;
            if (PortDataComm.SendCommand(0x69, out ret) != PortDataComm.CommandStatus.OK || ret == null || ret.Length != 1 || ret[0] != 1)
            {
                SetStatus(-9, 0, 1);
                return false;
            }

            return true;
        }

        //        private bool DoWaitReadyStatus()
        //        {
        //            var st = DateTime.Now;
        //
        //            while (true)
        //            {
        //                Thread.Sleep(100);
        //
        //                byte[] ret;
        //
        //                if (PortDataComm.SendCommand(0x66, out ret) != PortDataComm.CommandStatus.OK || ret == null || ret.Length != 1)
        //                {
        //                    SetStatus(-1, 0, 1);
        //                    return false;
        //                }
        //
        //                if (ret[0] == 1) return true;
        //
        //                if ((DateTime.Now - st).TotalSeconds > 30)
        //                {
        //                    SetStatus(-2, 0, 1);
        //                    return false;
        //                }
        //            }
        //        }
        //
        private bool SendChunk(ref int startFrom)
        {
            var len = Math.Min(ProgramCode.Length - startFrom, 32);
            if (len == 0) return true;

            var pbuff = new byte[len + 5];

            Array.Copy(BitConverter.GetBytes(startFrom), 0, pbuff, 0, 4);
            Array.Copy(ProgramCode, startFrom, pbuff, 4, len);

            pbuff[len+4] = 0;
            pbuff[len+4] = pbuff.Aggregate<byte, byte>(0, (current, b1) => (byte)(current ^ b1));

            byte[] ret;

            if (PortDataComm.SendLongRunCommand(0x67, out ret, 500,pbuff) != PortDataComm.CommandStatus.OK || ret == null || ret.Length != 1)
            {
                SetStatus(-3, 0, 1);
                return false;
            }

            startFrom += len;

            return true;
        }
    }
}
