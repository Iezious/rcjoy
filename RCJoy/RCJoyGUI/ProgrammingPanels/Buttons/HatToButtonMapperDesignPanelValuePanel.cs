using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class HatToButtonMapperDesignPanelValuePanel : ValidatePanel
    {
        private int __DataIndex = -1;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int DataIndex { get { return __DataIndex; } }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Case
        {
            get
            {
                int i;
                return int.TryParse(tbCase.Text, out i) ? i : 0;
            }
        }

        public HatToButtonMapperDesignPanelValuePanel()
        {
            InitializeComponent();
        }

        private void tbCase_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ValidateControl(tbCase, () =>  ValidateInt(tbCase.Text));
        }

        public XElement Serialize()
        {
            return new XElement("Case", new XAttribute("Key", tbCase.Text), new XAttribute("Number", Tag), new XAttribute("DataIndex", __DataIndex));
        }

        public void Deserialize(XElement data)
        {
            __DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            tbCase.Text = data.AttributeValue("Key");
        }


        public void Link(int num)
        {
            lnkOut.Name = "lnkOut_" + num;
            Name = "case_" + num;
            Tag = num;
            
            lblName.Text = "B" + (num + 1);
            BackColor = Color.Transparent;
        }

        public void UnLink()
        {
            lnkOut.Unlink();
        }

        public LinkPoint[] GetLinks()
        {
            return new[] { lnkOut };
        }


        public void LinkIndex(ref int IndexCounter)
        {
            __DataIndex = IndexCounter;
            IndexCounter++;
        }

        public void TakeData(int[] data)
        {
            if (__DataIndex < 0) return;

            var states = data[__DataIndex];
            var enabled = (states & 1) != 0;

            if (enabled)
            {
                lblName.ForeColor = Color.DarkRed;
                lblName.Font = new Font(lblName.Font, FontStyle.Bold);
            }
            else
            {
                lblName.ForeColor = this.ForeColor;
                lblName.Font = new Font(lblName.Font, FontStyle.Regular);
            }
        }

        public void Check()
        {
            if (!ValidateControl(tbCase, () => ValidateInt(tbCase.Text)))
                throw new CompilationCheckException("Wrong case value: " + tbCase.Text, CompilationCheckException.CompileIteration.PreCheck);
        }

        public void MapVariables(CodeGeneratorContext context)
        {
            lnkOut.DataMapIdx = (short)__DataIndex;
        }
    }
}
