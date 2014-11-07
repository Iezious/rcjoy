using System;
using System.Collections.Generic;
using System.Text;

namespace UsbLibrary
{
    public class DataRecievedEventArgs : EventArgs
    {
        public readonly byte[] data;

        public DataRecievedEventArgs(byte[] data)
        {
            this.data = data;
        }
    }

    public class DataSendEventArgs : EventArgs
    {
        public readonly byte[] data;

        public DataSendEventArgs(byte[] data)
        {
            this.data = data;
        }
    }

    public delegate void DataRecievedEventHandler(object sender, DataRecievedEventArgs args);
    public delegate void DataSendEventHandler(object sender, DataSendEventArgs args);

    public class SpecifiedDevice : HIDDevice
    {
        public event DataRecievedEventHandler DataRecieved;
        public event DataSendEventHandler DataSend;

        public override InputReport CreateInputReport()
        {
            return new SpecifiedInputReport(this);
        }

        public static SpecifiedDevice FindSpecifiedDevice(int vendor_id, int product_id)
        {
            return (SpecifiedDevice)FindDevice(vendor_id, product_id, typeof(SpecifiedDevice));
        }

        protected override void HandleDataReceived(InputReport oInRep)
        {
            // Fire the event handler if assigned
            if (DataRecieved != null)
            {
                SpecifiedInputReport report = (SpecifiedInputReport)oInRep;
                DataRecieved(this, new DataRecievedEventArgs(report.Data));
            }
        }

        public void SendData(byte[] data)
        {
            SpecifiedOutputReport oRep = new SpecifiedOutputReport(this);	// create output report
            oRep.SendData(data);	// set the lights states
            try
            {
                Write(oRep); // write the output report
                if (DataSend != null)
                {
                    DataSend(this, new DataSendEventArgs(data));
                }
            }catch (HIDDeviceException ex)
            {
                // Device may have been removed!
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected override void Dispose(bool bDisposing)
        {
            if (bDisposing)
            {
                // to do's before exit
            }
            base.Dispose(bDisposing);
        }

    }
}
