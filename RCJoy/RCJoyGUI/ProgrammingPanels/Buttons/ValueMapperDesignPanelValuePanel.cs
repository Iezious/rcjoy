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
    public partial class ValueMapperDesignPanelValuePanel : ValidatePanel
    {
        public ValueMapperDesignPanelValuePanel()
        {
            InitializeComponent();
        }

        private void tbCase_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ValidateControl(tbCase, () =>  ValidateInt(tbCase.Text));
        }

        private void tbValue_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ValidateControl(tbValue, () => ValidateInt(tbValue.Text));
        }

        public XElement Serialize()
        {
            return new XElement("Case", new XAttribute("Key", tbCase.Text), new XAttribute("Value", tbValue.Text));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Case
        {
            get
            {
                int i;
                return int.TryParse(tbCase.Text, out i) ? i : 0;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Value
        {
            get
            {
                int i;
                return int.TryParse(tbValue.Text, out i) ? i : 0;
            }
        }

        public void Deserialize(XElement thecase)
        {
            tbValue.Text = thecase.AttributeValue("Value");
            tbCase.Text = thecase.AttributeValue("Key");
        }

        public void Check()
        {
            if (!ValidateControl(tbCase, () => ValidateInt(tbCase.Text)))
                throw new CompilationCheckException("Wrong case value: " + tbCase.Text, CompilationCheckException.CompileIteration.PreCheck);

            if (!ValidateControl(tbValue, () => ValidateInt(tbValue.Text)))
                throw new CompilationCheckException("Wrong value: " + tbValue.Text, CompilationCheckException.CompileIteration.PreCheck);
        }
    }
}
