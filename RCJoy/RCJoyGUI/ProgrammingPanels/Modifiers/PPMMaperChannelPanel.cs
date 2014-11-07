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
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels.ModelOut
{
    public partial class PPMMaperChannelPanel : UserControl, ICalculationDataViewer
    {
        private int __Channel = 0;
        private int __DataIndex = -1;

        public PPMMaperChannelPanel()
        {
            InitializeComponent();
        }

        public void InitChan(int ch)
        {
            lnkChName.Text = "Chan " + ch;
            lnkIn.Name = "lnkIn_" + ch;
            lnkOut.Name = "lnkOut_" + ch;
            Name = "Ch_" + ch;

            __Channel = ch;
        }

        public void UnLink()
        {
            lnkIn.Unlink();
            lnkOut.Unlink();
        }

//        public void Link(ILinkResolver linkResolver)
//        {
//            lnkIn.Link(linkResolver);
//        }

        public LinkPoint LinkIn { get { return lnkIn; } }
        public LinkPoint LinkOut { get { return lnkOut; } }
        public int DataIndex { get { return __DataIndex; } }

        public XElement Serialize()
        {
            return new XElement("Chan", new XAttribute("Number", __Channel), new XAttribute("DataIndex",__DataIndex));
        }

        public void Deserialize(XElement data)
        {
            __DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
        }

        public void LinkIndex(ref int IndexCounter)
        {
            __DataIndex = IndexCounter;
            IndexCounter++;
        }

        public void TakeData(int[] data)
        {
            if(__DataIndex >=0)
                lblValue.Text = data[__DataIndex].ToString(CultureInfo.InvariantCulture);
        }

        public LinkPoint[] GetLinks()
        {
            return new[] {lnkIn, lnkOut};
        }

        public void Check()
        {
            if(lnkIn.LinkedTo == null)
                throw new CompilationCheckException("Input link is not connected", CompilationCheckException.CompileIteration.PreCheck);
        }
        
#if STM32
        public void MapVariables(CodeGeneratorContext context)
        {
            lnkOut.DataMapIdx = (short) __DataIndex;
        }
#endif
    }
}
