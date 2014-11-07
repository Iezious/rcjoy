using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class ValueSwitcherDesignPanelValuePanel : ValidatePanel
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Value
        {
            get
            {
                int i;
                return int.TryParse(tbValue.Text, out i) ? i : 0;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string LCDText
        {
            get
            {
                return tbText.Text;
            }
        }


        public ValueSwitcherDesignPanelValuePanel()
        {
            InitializeComponent();
        }

        private void tbValue_Validating(object sender, CancelEventArgs e)
        {
            ValidateControl(tbValue, () => ValidateInt(tbValue.Text));
        }

        public XElement Serialize()
        {
            return new XElement("Value", 
                new XAttribute("Value", tbValue.Text),
                new XAttribute("Text", tbText.Text)
                );
        }

        public void Deserialize(XElement data)
        {
            tbValue.Text = data.AttributeValue("Value");
            tbText.Text = data.AttributeValue("Text");
        }

        public void Check()
        {
            if (!ValidateControl(tbValue, () => ValidateInt(tbValue.Text)))
                throw new CompilationCheckException("Wrong value in the value switcher: " + tbValue.Text);
        }

    }
}
