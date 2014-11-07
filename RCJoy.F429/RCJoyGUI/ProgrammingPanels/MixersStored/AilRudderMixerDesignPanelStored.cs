using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class AilronToRudderMixerDesignPanelStored : DraggableElement, IFRAMUser
    {
        private int _DataIndex = -1;
        private uint _Addr;
        private bool _UseFRAM = false;

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
                return _UseFRAM ? new[] { Title + " eff" } : new string[0];
            }
        }

        public int[] DefaultValues
        {
            get
            {
                return _UseFRAM ? new[] { int.Parse(tbAilQuef.Text) } : new int[0];
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


        public AilronToRudderMixerDesignPanelStored()
        {
            InitializeComponent();
            RegiseterLink(lnkAileron, lnkRudder, lnkOut);
        }

        private void cbEEPROM_CheckedChanged(object sender, System.EventArgs e)
        {
            if (_UseFRAM != cbEEPROM.Checked)
            {
                _UseFRAM = cbEEPROM.Checked;
                FRAMMapper.ReAssign(this);
            }
        }

        private void tbAilQuef_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ValidateControl(tbAilQuef, () =>
                {
                    int i;

                    if (!int.TryParse(tbAilQuef.Text, out i))
                        return false;

                    return i > -95 && i < 95;
                });
        }

        public override XElement CreatXMLSave()
        {
            return new XElement("AilRudderMixerStored");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);
            data.Add(
                new XAttribute("DataIndex", _DataIndex),
                new XAttribute("AilAff", tbAilQuef.Text),
                new XAttribute("Addr", _Addr.ToString(CultureInfo.InvariantCulture)),
                new XAttribute("WriteFRAM", cbEEPROM.Checked)
                );
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);
            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            tbAilQuef.Text = data.AttributeValue("AilAff");
            cbEEPROM.Checked = _UseFRAM = bool.Parse(data.AttributeValue("WriteFRAM") ?? "false");
            _Addr = uint.Parse(data.AttributeValue("Addr") ?? "0");
        }

        public override void TakeData(int[] data)
        {
            if(_DataIndex < 0) return;

            lblValue1.Text = data[_DataIndex].ToString(CultureInfo.InvariantCulture);
        }

        public override void LinkIndex(ref int Counter)
        {
            _DataIndex = Counter;
            Counter+=2;
        }

        public override void Check(CodeGeneratorContext context)
        {
            if (!ValidateControl(tbAilQuef, () =>
            {
                int i;

                if (!int.TryParse(tbAilQuef.Text, out i))
                    return false;

                return i >= -95 && i <= 95;
            }))
                throw new CompilationCheckException("Wrong ailerons quotient value", CompilationCheckException.CompileIteration.PreCheck);

            if (lnkAileron.LinkedTo == null)
                throw new CompilationCheckException("Ailerons is not linked",
                    CompilationCheckException.CompileIteration.PreCheck);

            if (lnkRudder.LinkedTo == null)
                throw new CompilationCheckException("Rudder is not linked",
                    CompilationCheckException.CompileIteration.PreCheck);
        }
#if MEGA2560

        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            __H.WriteLine("#define {0} {1}", lnkOut.MappedValueName, _DataIndex);
        }

        public override void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            __CPP.WriteLine("mix_aileron_to_rudder({0}, {1}, {2}, {3});",
                lnkRudder.LinkedTo.MappedValueName,
                lnkAileron.LinkedTo.MappedValueName,
                tbAilQuef.Text,
                lnkOut.MappedValueName);
        }
#endif
#if STM32

        public override void MapVariables(CodeGeneratorContext context)
        {
            base.MapVariables(context);
            lnkOut.DataMapIdx = (short) _DataIndex;
        }

        public override void GenerateSTMCode(CodeGeneratorContext context, STMProgram prog)
        {
            var VarDataIndex = (ushort)(_DataIndex + 1);

            var variableIdx = prog.Variables.Add(
                VarDataIndex,
                (ushort)(cbEEPROM.Checked ? _Addr : 0),
                Title + " eff", 
                context.CurrentModel.Index,
                5, -95, 95, short.Parse(tbAilQuef.Text)
            );

            if (cbEEPROM.Checked)
            {
                prog.StartupCode.Add(0x0F, variableIdx, short.Parse(tbAilQuef.Text));
            }
            else
            {
                prog.StartupCode.Add(0x0D, VarDataIndex, short.Parse(tbAilQuef.Text));
            }

            context.ModelCode.Add(0x25, lnkRudder.LinkedTo.DataMapIdx, lnkAileron.LinkedTo.DataMapIdx,
                VarDataIndex, _DataIndex);

            prog.Blocks.Add(new STMBlockInfo(this, context.CurrentModel, GetLinks(), new[] { prog.Variables[variableIdx] }));
        }



#endif
    }
}
