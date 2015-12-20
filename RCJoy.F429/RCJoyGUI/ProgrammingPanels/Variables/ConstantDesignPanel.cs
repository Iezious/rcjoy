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
        private bool _UseFRAM;

        public ConstantDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkOutAxis, lnkOutValue);
        }

        public uint SlotsUsed { get { return (uint)(_UseFRAM ? 1 : 0); } }
        public uint[] FRAMAddresses
        {
            get
            {
                return _UseFRAM ? new[] { _Addr } : new uint[0];
            }
            set
            {
                if (_UseFRAM) _Addr = value[0];
            }
        }

        public string[] Names
        {
            get
            {
                return _UseFRAM ? new[] { Title} : new string[0];
            }
        }

        public int[] DefaultValues
        {
            get
            {
                return _UseFRAM ? new[] { int.Parse(tbValue.Text) } : new int[0];
            }
        }

        public override void Removed()
        {
            FRAMMapper.UnRegisterUser(this);
        }

        public override void Initialized()
        {
            FRAMMapper.Register(this);
        }



        private void cbEEPRom_CheckedChanged(object sender, EventArgs e)
        {
            //            tbAddress.Enabled = lbAddress.Enabled =
            //            tbChangeStep.Enabled = lbStep.Enabled =
            //            tbName.Enabled = lbName.Enabled =
            //            cbEEPRom.Checked;

            if (_UseFRAM != cbEEPRom.Checked)
            {
                _UseFRAM = cbEEPRom.Checked;
                FRAMMapper.ReAssign(this);
            }
        }

        public override XElement CreatXMLSave()
        {
            return new XElement("Constant");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);
            data.Add(
                new XAttribute("DataIndex", _DataIndex),
                new XAttribute("EEPROM", cbEEPRom.Checked),
                new XAttribute("Value", tbValue.Text),
                new XAttribute("Addr", _Addr.ToString(CultureInfo.InvariantCulture)),
                new XAttribute("MinValue", tbMin.Text),
                new XAttribute("MaxValue", tbMax.Text),
                new XAttribute("WriteFRAM", cbEEPRom.Checked)

                );
        }

        public override void Deserialize(XElement data)
        {
            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            tbValue.Text = data.AttributeValue("Value");
            tbMin.Text = data.AttributeValue("MinValue");
            tbMax.Text = data.AttributeValue("MaxValue");
            cbEEPRom.Checked = _UseFRAM = bool.Parse(data.AttributeValue("WriteFRAM") ?? "false");

            _Addr = _UseFRAM ? uint.Parse(data.AttributeValue("Addr") ?? "0") : 0;

            base.Deserialize(data);
        }

        public override void Check(CodeGeneratorContext context)
        {
            if (!ValidateControl(tbValue, () => ValidateInt(tbValue.Text)))
                throw new CompilationCheckException("Wrong value", CompilationCheckException.CompileIteration.PreCheck);

            if (!ValidateControl(tbMin, () => ValidateInt(tbMin.Text)))
                throw new CompilationCheckException("Wrong min value", CompilationCheckException.CompileIteration.PreCheck);

            if (!ValidateControl(tbMax, () => ValidateInt(tbMax.Text)))
                throw new CompilationCheckException("Wrong max value", CompilationCheckException.CompileIteration.PreCheck);

            if(_UseFRAM)
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

            var variableIdx = prog.Variables.Add(
                (ushort) _DataIndex,
                (ushort)(cbEEPRom.Checked ? _Addr : 0),
                Title,
                context.CurrentModel.Index,
                10,
                -1000, 1000,
                short.Parse(tbValue.Text)
                );


            if (cbEEPRom.Checked)
            {
                prog.StartupCode.Add(0x0F, variableIdx, short.Parse(tbValue.Text));
            }
            else
            {
                prog.StartupCode.Add(0x0D, _DataIndex, short.Parse(tbValue.Text));
            }

            //prog.Blocks.Add(new STMBlockInfo(this, context.CurrentModel, null, new []{prog.Variables[variableIdx]}));
        }

    }
}
