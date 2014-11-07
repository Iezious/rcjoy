using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels.FlightMode
{
    public partial class FlightModeOutChannel : UserControl
    {
        private int _DataIndex = -1;
        private int _Number = -1;
        
        public FlightModeOutChannel()
        {
            InitializeComponent();
        }

        public LinkPoint GetLink()
        {
            return lnkOut;
        }

        public int Number
        {
            get { return _Number; }
            set
            {
                _Number = value;
                lnkOut.Name = "OUT_" + (Number + 1);
            }
        }

        public XElement Serialize()
        {
            return new XElement("Link",
                new XAttribute("DataIndex",_DataIndex),
                new XAttribute("Number", (_Number+1))
            );
        }

        public void Deserialize(XElement data)
        {
            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            Number = int.Parse(data.AttributeValue("Number") ?? "-1");
        }

        public void MapVariables(CodeGeneratorContext context)
        {
            lnkOut.DataMapIdx = (short) _DataIndex;
        }

        public void LinkIndex(ref int Counter)
        {
            _DataIndex = Counter;
            Counter++;
        }

        public void TakeData(int[] data)
        {
            if (_DataIndex != -1)
                lblValue.Text = data[_DataIndex].ToString(CultureInfo.InvariantCulture);
        }
    }
}
