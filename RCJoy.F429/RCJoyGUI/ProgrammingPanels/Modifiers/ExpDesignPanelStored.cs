using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class ExponentDesignPanelStored : DraggableElement, IFRAMUser
    {
        private int _DataIndex = -1;
        private bool _Centered = true;
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
                return _UseFRAM ? new[] { Title } : new string[0];
            }
        }

        public int[] DefaultValues
        {
            get
            {
                return _UseFRAM ? new[] { int.Parse(tbExp.Text) } : new int[0];
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
        public bool Centered
        {
            get { return _Centered; }
            set
            {
                _Centered = value;

                if(string.IsNullOrEmpty(Title))
                    Title = _Centered ? "Exponent" : "Throttle exponent";
            }
        }

        public ExponentDesignPanelStored()
        {
            InitializeComponent();
            RegiseterLink(lnkIn, lnkOut);
        }

        private void cbEEPROM_CheckedChanged(object sender, EventArgs e)
        {
            if (_UseFRAM != cbEEPROM.Checked)
            {
                _UseFRAM = cbEEPROM.Checked;
                FRAMMapper.ReAssign(this);
            }
        }

        private void tbAilQuef_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbExp, () =>
                {
                    int i;

                    if (!int.TryParse(tbExp.Text, out i))
                        return false;

                    return i >= -10 && i <= 10;
                });
        }

        public override XElement CreatXMLSave()
        {
            return new XElement("ExponentStored");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);
            data.Add(
                new XAttribute("DataIndex", _DataIndex),
                new XAttribute("Effect", tbExp.Text),
                new XAttribute("Centered", _Centered),
                new XAttribute("Addr", _Addr.ToString(CultureInfo.InvariantCulture)),
                new XAttribute("WriteFRAM", cbEEPROM.Checked)
                );
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);

            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            Centered = bool.Parse(data.AttributeValue("Centered") ?? "true");
            tbExp.Text = data.AttributeValue("Effect");
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
            if(!ValidateControl(tbExp, () =>
                {
                    int i;

                    if (!int.TryParse(tbExp.Text, out i))
                        return false;

                    return i >= -10 && i <= 10;
                }))
            throw new CompilationCheckException("Wrong exponent value",CompilationCheckException.CompileIteration.PreCheck);

            if (lnkIn.LinkedTo == null)
                throw new CompilationCheckException("Input value is not linked for the exponent modifier",
                    CompilationCheckException.CompileIteration.PreCheck);
        }
#if MEGA2560

        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            __H.WriteLine("#define {0} {1}",lnkOut.MappedValueName, _DataIndex);
        }

        public override void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            if(Centered)
                __CPP.WriteLine("map_exp({0}, {1}, {2});",lnkIn.LinkedTo.MappedValueName, tbExp.Text, lnkOut.MappedValueName);
            else
                __CPP.WriteLine("map_th_exp({0}, {1}, {2});", lnkIn.LinkedTo.MappedValueName, tbExp.Text, lnkOut.MappedValueName);

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
                Title,
                context.CurrentModel.Index,
                1, -10, 10, short.Parse(tbExp.Text)
            );

            if (cbEEPROM.Checked)
            {
                prog.StartupCode.Add(0x0F, variableIdx, short.Parse(tbExp.Text));
            }
            else
            {
                prog.StartupCode.Add(0x0D, VarDataIndex, short.Parse(tbExp.Text));
            }

            if(Centered)
                context.ModelCode.Add(0x2A, lnkIn.LinkedTo.DataMapIdx, VarDataIndex, _DataIndex);
            else
                context.ModelCode.Add(0x2B, lnkIn.LinkedTo.DataMapIdx, VarDataIndex, _DataIndex);

            prog.Blocks.Add(new STMBlockInfo(this, context.CurrentModel, GetLinks(), new[] { prog.Variables[variableIdx] }));

        }


#endif
    }
}
