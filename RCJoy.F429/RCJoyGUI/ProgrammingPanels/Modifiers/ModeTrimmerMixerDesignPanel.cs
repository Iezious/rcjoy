using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class ModeTrimmerMixerDesignPanel : DraggableElement, IFRAMUser
    {
        private int _DataIndex = -1;
        private uint _Addr;
        private int _ModeIdx = -1;

        public ModeTrimmerMixerDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkAxis, lnkInc, lnkDec, lnkReset, lnkOut);
        }

        public uint SlotsUsed { get { return 1; } }
        public uint[] FRAMAddresses
        {
            get { return new[] { _Addr }; }
            set
            {
                _Addr = value[0];
            }
        }

        public string[] Names
        {
            get { return new[] {Title}; }
        }

        public int[] DefaultValues
        {
            get { return new[] {int.Parse(tbInitVal.Text)}; }
        }

        public override void Removed()
        {
            FRAMMapper.UnRegisterUser(this);
        }

        public override void Initialized()
        {
            FRAMMapper.Register(this);

            cbMode.Items.Clear();

            foreach (var name in ModelPanel.Elements.OfType<FMPPMOutDesignPanel>().Select(mode => mode.ModeName))
                cbMode.Items.Add(name);

            if (_ModeIdx >= 0 && _ModeIdx < cbMode.Items.Count) cbMode.SelectedIndex = _ModeIdx;
        }

        public override void ModesUpdated()
        {
            cbMode.Items.Clear();

            foreach (var name in ModelPanel.Elements.OfType<FMPPMOutDesignPanel>().Select(mode => mode.ModeName))
                cbMode.Items.Add(name);

            if (_ModeIdx >= 0 && _ModeIdx < cbMode.Items.Count) cbMode.SelectedIndex = _ModeIdx;
        }

        public override XElement CreatXMLSave()
        {
            return new XElement("ModeTrimmer");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);
            data.Add(
                new XAttribute("DataIndex", _DataIndex),
                new XAttribute("InitVal", tbInitVal.Text),
                new XAttribute("Addr", _Addr.ToString(CultureInfo.InvariantCulture)),
                new XAttribute("WriteFRAM", cbFramEnabled.Checked ? "1" : "0"),
                new XAttribute("Step", tbStep.Text),
                new XAttribute("Mode", _ModeIdx = cbMode.SelectedIndex),
                new XAttribute("TrimType", cbTrimmerType.SelectedIndex)
            );
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);

            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            _ModeIdx = int.Parse(data.AttributeValue("Mode") ?? "-1");
            cbFramEnabled.Checked = data.AttributeValue("WriteFRAM") == "1";

            tbInitVal.Text = data.AttributeValue("InitVal");
            tbStep.Text = data.AttributeValue("Step");

            _Addr = uint.Parse(data.AttributeValue("Addr") ?? "0");
            cbTrimmerType.SelectedIndex = int.Parse(data.AttributeValue("TrimType") ?? "0");
        }

        private void cbMode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            _ModeIdx = cbMode.SelectedIndex;
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
            if (!ValidateControl(tbInitVal, () => ValidateInt(tbInitVal.Text)))
                throw new CompilationCheckException("Wrong init value", CompilationCheckException.CompileIteration.PreCheck);

            if (!ValidateControl(tbStep, () => ValidateInt(tbStep.Text)))
                throw new CompilationCheckException("Wrong step value", CompilationCheckException.CompileIteration.PreCheck);

            context.RegiterEEPVariable(_Addr, ID.ToString("N"));

            if (lnkAxis.LinkedTo == null)
                throw new CompilationCheckException("Input axis is not linked for the trimmer modifier",
                    CompilationCheckException.CompileIteration.PreCheck);
            
            if (lnkInc.LinkedTo == null)
                throw new CompilationCheckException("Increment click is not defined", CompilationCheckException.CompileIteration.PreCheck);

            if (lnkDec.LinkedTo == null)
                throw new CompilationCheckException("Decrement click is not defined", CompilationCheckException.CompileIteration.PreCheck);

            if(cbMode.SelectedIndex < 0)
                throw new CompilationCheckException("Mode is not selected", CompilationCheckException.CompileIteration.PreCheck);

        }

#if MEGA2560
        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            __H.WriteLine("#define {0} {1}", lnkOut.MappedValueName, _DataIndex);
        }

        public override void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            __CPP.WriteLine("trim({0}, {1}, {2});", 
                lnkAxis.LinkedTo.MappedValueName, lnkValue.LinkedTo.MappedValueName, lnkOut.MappedValueName);
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
            var VarDataIndex = (ushort) (_DataIndex + 1);

            _ModeIdx = cbMode.SelectedIndex;

            var variableIdx = prog.Variables.Add(
                VarDataIndex, 
                (ushort) (cbFramEnabled.Checked ? _Addr : 0), 
                Title, context.CurrentModel.Index,
                short.Parse(tbStep.Text), -500, 500,
                short.Parse(tbInitVal.Text)
                );

            if (cbFramEnabled.Checked)
            {
                prog.StartupCode.Add(0x0F, variableIdx, short.Parse(tbInitVal.Text));
            }
            else
            {
                prog.StartupCode.Add(0x0D, VarDataIndex, short.Parse(tbInitVal.Text));
            }


            var stmmodel = prog.Models[context.CurrentModel.Index];

            context.ModelCode.Add(0x22,
                    lnkInc.LinkedTo.DataMapIdx,
                    lnkDec.LinkedTo.DataMapIdx,
                    short.Parse(tbStep.Text),
                    lnkReset.LinkedTo != null ? lnkReset.LinkedTo.DataMapIdx : 0,
                    short.Parse(tbInitVal.Text),
                    variableIdx,
                    stmmodel.ModeVariable,
                    _ModeIdx
                );

            context.ModelCode.Add(0x11, lnkAxis.LinkedTo.DataMapIdx, _DataIndex + 1, _DataIndex);
            context.ModelCode.Add(0x2E, VarDataIndex, cbTrimmerType.SelectedIndex, _ModeIdx, stmmodel.ModeVariable);

            prog.Blocks.Add(new STMBlockInfo(this, context.CurrentModel, GetLinks(), new[] { prog.Variables[variableIdx] }));
        }
#endif
    }
}
