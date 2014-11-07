using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class TrimmerDesignPanelEx : DraggableElement, IFRAMUser
    {
        private int _DataIndex = -1;
        private uint _Addr;

        public TrimmerDesignPanelEx()
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
            get { return new[] {tbCName.Text}; }
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
        }

        public override XElement Serialize()
        {
            return new XElement("TrimmerEx",
                                   new XAttribute("Top", Top),
                                   new XAttribute("Left", Left),
                                   new XAttribute("ID", ID),
                                   new XAttribute("DataIndex", _DataIndex),
                                   new XAttribute("InitVal", tbInitVal.Text),
                                   new XAttribute("Addr", _Addr.ToString(CultureInfo.InvariantCulture)),
                                   new XAttribute("WriteFRAM", cbFramEnabled.Checked ? "1" : "0"),
                                   new XAttribute("Step", tbStep.Text),
                                   new XAttribute("CName", tbCName.Text),
                                   SerializeLinks()
                );
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);

            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            cbFramEnabled.Checked = data.AttributeValue("WriteFRAM") == "1";

            tbInitVal.Text = data.AttributeValue("InitVal");
            tbStep.Text = data.AttributeValue("Step");
            tbCName.Text = data.AttributeValue("CName");

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
            if (!ValidateControl(tbInitVal, () => ValidateInt(tbInitVal.Text)))
                throw new CompilationCheckException("Wrong init value", CompilationCheckException.CompileIteration.PreCheck);

            if (!ValidateControl(tbStep, () => ValidateInt(tbStep.Text)))
                throw new CompilationCheckException("Wrong step value", CompilationCheckException.CompileIteration.PreCheck);

            if (!ValidateControl(tbCName, () => string.IsNullOrWhiteSpace(tbCName.Text) || ValidateCName(tbCName.Text)))
                throw new CompilationCheckException("Wrong LCD text header", CompilationCheckException.CompileIteration.PreCheck);

            context.RegiterEEPVariable(_Addr, ID.ToString("N"));

            if (lnkAxis.LinkedTo == null)
                throw new CompilationCheckException("Input axis is not linked for the trimmer modifier",
                    CompilationCheckException.CompileIteration.PreCheck);
            
            if (lnkInc.LinkedTo == null)
                throw new CompilationCheckException("Increment click is not defined", CompilationCheckException.CompileIteration.PreCheck);

            if (lnkDec.LinkedTo == null)
                throw new CompilationCheckException("Decrement click is not defined", CompilationCheckException.CompileIteration.PreCheck);
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
            
            var variableIdx = prog.Variables.Add(
                VarDataIndex, 
                (ushort) (cbFramEnabled.Checked ? _Addr : 0), 
                tbCName.Text, context.CurrentModel.Index,
                short.Parse(tbStep.Text), -500, 500
                );

            if (cbFramEnabled.Checked)
            {
                prog.StartupCode.Add(0x0F, variableIdx, short.Parse(tbInitVal.Text));
            }
            else
            {
                prog.StartupCode.Add(0x0D, VarDataIndex, short.Parse(tbInitVal.Text));
            }

            context.ModelCode.Add(0x0E,
                    lnkInc.LinkedTo.DataMapIdx,
                    lnkDec.LinkedTo.DataMapIdx,
                    short.Parse(tbStep.Text),
                    lnkReset.LinkedTo != null ? lnkReset.LinkedTo.DataMapIdx : 0,
                    short.Parse(tbInitVal.Text),
                    variableIdx
                );

            context.ModelCode.Add(0x11, lnkAxis.LinkedTo.DataMapIdx, _DataIndex + 1, _DataIndex);
        }
#endif
    }
}
