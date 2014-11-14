using System.ComponentModel;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels.Buttons
{
    public partial class MultiButtonPanel : ValidatePanel
    {
        private ushort _Index;


        public MultiButtonPanel()
        {
            InitializeComponent();
        }

        public short Value
        {
            get { return short.Parse(tbCase.Text); }
        }

        public LinkPoint ButtonLink
        {
            get {  return lnkButton;}
        }

        public ushort Index
        {
            get { return _Index; }
            set
            {
                _Index = value;
                lnkButton.Name = "BL_" + (value + 1);
            }
        }

        public LinkPoint[] GetLinks()
        {
            return new[] {lnkButton};
        }

        public void UnLink()
        {
            lnkButton.Unlink();
        }

        public void Check()
        {
            if (!ValidateControl(tbCase, () => ValidateInt(tbCase.Text)))
                throw new CompilationCheckException("Wrong value: " + tbCase.Text, CompilationCheckException.CompileIteration.PreCheck);

            if (lnkButton.LinkedTo == null)
                throw new CompilationCheckException("Input button is not linked", CompilationCheckException.CompileIteration.PreCheck);
        }

        public XElement Serialize()
        {
            return new XElement("Case",
                new XAttribute("Number", _Index + 1),
                new XAttribute("Value", tbCase.Text)
            );
        }

        public void Deserialize(XElement data)
        {
            Index = (ushort)(ushort.Parse(data.AttributeValue("Number")) - 1);
            tbCase.Text = data.AttributeValue("Value");
        }
    }
}
