using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tahorg.RCJoyGUI.Data;
using Tahorg.RCJoyGUI.Properties;

namespace Tahorg.RCJoyGUI.Dialogs
{
    public partial class ParseJoyDialog : Form
    {
        public ParseJoyDialog()
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

        public string DescriptorText
        {
            get
            {
                return tbValues.Text.Replace("\n", "").Replace("\r", "");
            }
        }

        public ushort VendorID
        {
            get
            {
                return ushort.Parse(tbVendor.Text, NumberStyles.HexNumber);
            }
        }

        public ushort ProductID
        {
            get { return ushort.Parse(tbProduct.Text, NumberStyles.HexNumber); }
        }

        private void btnComRead_Click(object sender, EventArgs e)
        {


            try
            {
                ReadCom();
            }
            catch (Exception exx)
            {
                MessageBox.Show(this, exx.Message, Resources.Common_Headers_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void ReadCom()
        {
            btnComRead.Enabled = false;

            new Task(() =>
            {
                byte[] backdata = null;
                byte[] vendordata = null;


                for (var tries = 0; tries < 5; tries++)
                {
                    if (PortDataComm.SendCommand(1, out backdata, null) == PortDataComm.CommandStatus.OK && backdata != null && backdata.Length > 0) break;
                }

                for (var tries = 0; tries < 5; tries++)
                {
                    if (PortDataComm.SendCommand(0xA, out vendordata, null) == PortDataComm.CommandStatus.OK && vendordata != null && vendordata.Length == 4) break;
                }

                Invoke(new ThreadSafeInvokeDelegate(() =>
                {
                    if (backdata == null || backdata.Length == 0 || vendordata == null || vendordata.Length != 4)
                    {
                        lblPortState.Text = Resources.ParseJoyDialog_ReadCom_Communication_error;
                        lblPortState.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblPortState.Text = Resources.ParseJoyDialog_ReadCom_Complete;
                        lblPortState.ForeColor = Color.Red;
                        tbValues.Text = string.Join(" ", backdata.Select(b => b.ToString("X2")));

                        tbVendor.Text = string.Join("", vendordata.Take(2).Select(b => b.ToString("X2")));
                        tbProduct.Text = string.Join("", vendordata.Skip(2).Select(b => b.ToString("X2")));
                    }
                    btnComRead.Enabled = true;
                }));

            }).Start();
        }
    }
}
