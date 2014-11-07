using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using LibUsbDotNet.WinUsb;
using UsbLibrary;

namespace ST_LINKv2_Probe
{
    public partial class Form1 : Form
    {
//        public static UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(0x0483, 0x3748);
        UsbDevice MyUsbDevice;
        SpecifiedDevice dev;

        public Form1()
        {
            InitializeComponent();
//            dev = UsbLibrary.SpecifiedDevice.FindSpecifiedDevice(0x0483, 0x3748);
            dev = UsbLibrary.SpecifiedDevice.FindSpecifiedDevice(0x0483, 0x3748);
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetVer();
        }

        byte[] ExecuteCommand(byte cmd1, byte cmd2, int rlen)
        {
            byte[] cbuffer = new byte[2];
            cbuffer[0] = cmd1;
            cbuffer[1] = cmd2;

            ErrorCode res;

            using (var wr = MyUsbDevice.OpenEndpointWriter((WriteEndpointID) 0x81))
            {
                int written;
                res = wr.Write(cbuffer, 1000, out written);
            }

            if (res != ErrorCode.Ok)
                return null;
            
            byte[] rbuffer = new byte[rlen];


            using (var rd = MyUsbDevice.OpenEndpointReader(ReadEndpointID.Ep01, rlen))
            {
                int tot = 0;

                while (tot < rlen)
                {
                    int read;
                    rd.Read(rbuffer, tot, rlen - tot, 1000, out read);

                    if(read == 0) break;

                    tot += read;
                }
            }

            return rbuffer;

        }

        void GetVer()
        {

            var b = ExecuteCommand(0xF1, 0, 6);

        }
    }
}
