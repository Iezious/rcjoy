using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Tahorg.RCJoyGUI.ProgrammingPanels.ModelOut
{
    public partial class ModelChannelPanel : UserControl, ICalculationDataViewer
    {
        private int _Chan;

        public int Chan
        {
            get { return _Chan; }
        }

        public LinkPoint LinkIn { get { return lnkIn; } }

        public ModelChannelPanel()
        {
            InitializeComponent();
        }

        public void SetInfo(int chan_idx)
        {
            _Chan = chan_idx;

            lblName.Text = "Channel " + chan_idx;
            lblValue.Text = "XXXX";
            this.Name = lnkIn.Name = "PPM_" + chan_idx;
        }

        public void LinkIndex(ref int IndexCounter)
        {

        }

        public void TakeData(int[] data)
        {
            lblValue.Text = data[_Chan - 1].ToString(CultureInfo.InvariantCulture);
        }

        public XElement Serialize()
        {
            var data = lnkIn.Serialize();
            data.Add(new XAttribute("Chan", _Chan));
            
            return data;
        }

        public void Deserialize(XElement data)
        {
            lnkIn.Deserialize(data);
        }

        public LinkPoint GetLink()
        {
            return lnkIn;
        }

        public void TakeData(int data)
        {
            lblValue.Text = data > 0 ? data.ToString(CultureInfo.InvariantCulture) : "XXXX";
        }
    }
}
