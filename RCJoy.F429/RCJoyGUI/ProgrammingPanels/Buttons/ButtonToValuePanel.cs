using System.ComponentModel;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels.Buttons
{
    public partial class ButtonToValuePanel : ValidatePanel
    {
        private ushort _Index;


        public ButtonToValuePanel()
        {
            InitializeComponent();
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
                new XAttribute("Text", tbText.Text),
                new XAttribute("Value", tbCase.Text)
            );
        }

        public void Deserialize(XElement data)
        {
            Index = (ushort)(ushort.Parse(data.AttributeValue("Number")) - 1);
            tbCase.Text = data.AttributeValue("Value");
            tbText.Text = data.AttributeValue("Text");
        }

        public void GenerateSTMCode(CodeGeneratorContext context, STMProgram prog, int dataIndex)
        {
            if (string.IsNullOrWhiteSpace(tbText.Text))
            {
                context.ModelCode.Add(0x2C,
                    lnkButton.LinkedTo.DataMapIdx,
                    short.Parse(tbCase.Text),
                    dataIndex);
            }
            else
            {
                var stridx = prog.StringConstants.Add(tbText.Text); 
                
                context.ModelCode.Add(0x2F,
                    lnkButton.LinkedTo.DataMapIdx,
                    stridx,
                    short.Parse(tbCase.Text),
                    dataIndex);
            }
        }
    }
}
