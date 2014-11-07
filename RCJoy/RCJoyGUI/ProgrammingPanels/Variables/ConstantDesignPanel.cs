using System;
using System.Globalization;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class ConstantDesignPanel : DraggableElement, IFRAMUser
    {
        private int _DataIndex;
        private uint _Addr;

        public ConstantDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkOutAxis, lnkOutValue);
        }

        public override void Removed()
        {
            FRAMMapper.UnRegisterUser(this);
        }

        public override void Initialized()
        {
            FRAMMapper.Register(this);
        }

        public uint SlotsUsed { get { return 1; } }
        public uint[] FRAMAddresses
        {
            get
            {
                return new[] { _Addr };
            }
            set
            {
                _Addr = value[0];
            }
        }


        public string[] Names
        {
            get { return new[] { tbName.Text }; }
        }

        public int[] DefaultValues
        {
            get { return new[] { int.Parse(tbValue.Text) }; }
        }


        private void cbEEPRom_CheckedChanged(object sender, EventArgs e)
        {
//            tbAddress.Enabled = lbAddress.Enabled =
//            tbChangeStep.Enabled = lbStep.Enabled =
//            tbName.Enabled = lbName.Enabled =
//            cbEEPRom.Checked;
        }

        public override XElement Serialize()
        {
            return new XElement("Constant",
                                   new XAttribute("Top", Top),
                                   new XAttribute("Left", Left),
                                   new XAttribute("ID", ID),
                                   new XAttribute("DataIndex", _DataIndex),
                                   new XAttribute("EEPROM", cbEEPRom.Checked),
                                   new XAttribute("Value", tbValue.Text),
                                   new XAttribute("Addr", _Addr.ToString(CultureInfo.InvariantCulture)),
                                   new XAttribute("Step", tbChangeStep.Text),
                                   new XAttribute("MinValue", tbMin.Text),
                                   new XAttribute("MaxValue", tbMax.Text),
                                   new XAttribute("CName", tbName.Text),
                                   SerializeLinks());
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);

            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            tbValue.Text = data.AttributeValue("Value");
            tbMin.Text = data.AttributeValue("MinValue");
            tbMax.Text = data.AttributeValue("MaxValue");
            tbChangeStep.Text = data.AttributeValue("Step");
            tbName.Text = data.AttributeValue("CName");
            cbEEPRom.Checked = bool.Parse(data.AttributeValue("EEPROM") ?? "false");

            _Addr = uint.Parse(data.AttributeValue("Addr") ?? "0");
        }

        public override void Check(CodeGeneratorContext context)
        {
            if (!ValidateControl(tbValue, () => ValidateInt(tbValue.Text)))
                throw new CompilationCheckException("Wrong value", CompilationCheckException.CompileIteration.PreCheck);

            if (!ValidateControl(tbMin, () => ValidateInt(tbMin.Text)))
                throw new CompilationCheckException("Wrong min value", CompilationCheckException.CompileIteration.PreCheck);

            if (!ValidateControl(tbMax, () => ValidateInt(tbMax.Text)))
                throw new CompilationCheckException("Wrong max value", CompilationCheckException.CompileIteration.PreCheck);

            if (!ValidateControl(tbChangeStep, () => ValidateInt(tbChangeStep.Text) && (int.Parse(tbChangeStep.Text) != 0)))
                throw new CompilationCheckException("Wrong inc/dec step", CompilationCheckException.CompileIteration.PreCheck);

            if (!ValidateControl(tbName, () => ValidateCName(tbName.Text)))
                throw new CompilationCheckException("Wrong or empy constant name", CompilationCheckException.CompileIteration.PreCheck);

            context.RegiterEEPVariable(_Addr, ID.ToString("N"));
        }


        public override void LinkIndex(ref int Counter)
        {
            _DataIndex = Counter;
            Counter += 2;
        }

        public override void MapVariables(CodeGeneratorContext context)
        {
            base.MapVariables(context);
            lnkOutAxis.DataMapIdx = (short)_DataIndex;
            lnkOutValue.DataMapIdx = (short)_DataIndex;
        }

        public override void GenerateSTMCode(CodeGeneratorContext context, STMProgram prog)
        {
            base.GenerateSTMCode(context, prog);

            if (cbEEPRom.Checked)
            {
                var variableIdx = prog.Variables.Add(
                    (ushort) _DataIndex,
                    (ushort) _Addr,
                    tbName.Text,
                    context.CurrentModel.Index,
                    short.Parse(tbChangeStep.Text),
                    -1000, 1000
                    );


                prog.StartupCode.Add(0x0F, variableIdx, short.Parse(tbValue.Text));
            }
            else
            {
                prog.Variables.Add((ushort)_DataIndex, 0, tbName.Text, context.CurrentModel.Index,
                    short.Parse(tbChangeStep.Text), -1000, 1000);

                prog.StartupCode.Add(0x0D, _DataIndex, short.Parse(tbValue.Text));
            }
        }

    }
}
