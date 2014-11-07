using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class EEPVariableDesignPanel : DraggableElement, IFRAMUser
    {
        private int _DataIndex = -1;
        private uint _Addr;

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
                tbAddr.Text = _Addr.ToString(CultureInfo.InvariantCulture);
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

        public EEPVariableDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkInc, lnkDec, lnkReset, lnkOut);
        }

        public override void Removed()
        {
            FRAMMapper.UnRegisterUser(this);
        }

        public override void Initialized()
        {
            FRAMMapper.Register(this);
        }

        private void tbInitVal_Validating(object sender, CancelEventArgs e)
        {
            ValidateControl(tbInitVal, () => ValidateInt(tbInitVal.Text));
        }

        private void tbStep_Validating(object sender, CancelEventArgs e)
        {
            ValidateControl(tbStep, () => ValidateInt(tbStep.Text));
        }

        private void tbAddr_Validating(object sender, CancelEventArgs e)
        {
            ValidateControl(tbAddr, () =>
            {
                int dummy;
                return
                    int.TryParse(tbAddr.Text, out dummy) && dummy > 0 && (dummy % 2 == 0);
            });
        }

        public override XElement CreatXMLSave()
        {
            return new XElement("EEPVariable");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);
            data.Add(
                new XAttribute("DataIndex", _DataIndex),
                new XAttribute("InitVal", tbInitVal.Text),
                new XAttribute("Addr", tbAddr.Text),
                new XAttribute("Step", tbStep.Text)
            );
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);

            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            tbInitVal.Text = data.AttributeValue("InitVal");
            tbAddr.Text = data.AttributeValue("Addr");
            tbStep.Text = data.AttributeValue("Step");

            _Addr = uint.Parse(tbAddr.Text);
        }

        public override void TakeData(int[] data)
        {
            if (_DataIndex < 0) return;

            lblValue1.Text = data[_DataIndex].ToString(CultureInfo.InvariantCulture);
        }

        public override void LinkIndex(ref int Counter)
        {
            _DataIndex = Counter;
            Counter += 2;
        }

        public override void Check(CodeGeneratorContext context)
        {
            if (!ValidateControl(tbInitVal, () => ValidateInt(tbInitVal.Text)))
                throw new CompilationCheckException("Wrong init value", CompilationCheckException.CompileIteration.PreCheck);

            if (!ValidateControl(tbStep, () => ValidateInt(tbStep.Text)))
                throw new CompilationCheckException("Wrong step value", CompilationCheckException.CompileIteration.PreCheck);

            if (!ValidateControl(tbInitVal, () =>
            {
                int dummy;
                return
                    int.TryParse(tbAddr.Text, out dummy) && dummy > 0 && (dummy % 2 == 0);
            }))
                throw new CompilationCheckException("Wrong eeprom address value", CompilationCheckException.CompileIteration.PreCheck);

            context.RegiterEEPVariable(uint.Parse(tbAddr.Text), ID.ToString("N"));

            if (lnkInc.LinkedTo == null)
                throw new CompilationCheckException("Increment click is not defined", CompilationCheckException.CompileIteration.PreCheck);

            if (lnkDec.LinkedTo == null)
                throw new CompilationCheckException("Decrement click is not defined", CompilationCheckException.CompileIteration.PreCheck);
        }

#if MEGA2560
        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            __H.WriteLine("#define {0} {1}",lnkOut.MappedValueName,_DataIndex);
            __H.WriteLine("#define {0}_CHANGED {1}", lnkOut.MappedValueName, _DataIndex+1);
        }

        public override void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            __CPP.WriteLine("set_val({0}_CHANGED,0);", lnkOut.MappedValueName);

            __CPP.WriteLine("if({0}_PRESSED)", lnkInc.LinkedTo.MappedValueName);
            __CPP.WriteLine("{");
            __CPP.WriteLine("  setEep({0}, p0 = (get({0}) + ({1})), {2});", lnkOut.MappedValueName, tbStep.Text, tbAddr.Text);
            __CPP.WriteLine("  set_val({0}_CHANGED,1);", lnkOut.MappedValueName);

            if (!string.IsNullOrWhiteSpace(tbCName.Text))
            {
                __CPP.WriteLine("#ifdef LCD");
                __CPP.WriteLine("  lcdPrintLine2(\"{0}\",p0);", tbCName.Text);
                __CPP.WriteLine("#endif  ");
            }
            __CPP.WriteLine("}");


            __CPP.WriteLine("else if({0}_PRESSED)", lnkDec.LinkedTo.MappedValueName);
            __CPP.WriteLine("{");
            __CPP.WriteLine("  setEep({0}, p0 = (get({0}) - ({1})), {2});", lnkOut.MappedValueName, tbStep.Text, tbAddr.Text);
            __CPP.WriteLine("  set_val({0}_CHANGED,1);", lnkOut.MappedValueName);

            if (!string.IsNullOrWhiteSpace(tbCName.Text))
            {
                __CPP.WriteLine("#ifdef LCD");
                __CPP.WriteLine("  lcdPrintLine2(\"{0}\",p0);", tbCName.Text);
                __CPP.WriteLine("#endif  ");
            }
            __CPP.WriteLine("}");

            if(lnkReset.LinkedTo == null) return;

            __CPP.WriteLine("else if({0}_PRESSED)", lnkReset.LinkedTo.MappedValueName);
            __CPP.WriteLine("{");
            __CPP.WriteLine("  setEep({0}, p0 = {1}, {2});", lnkOut.MappedValueName, tbInitVal.Text, tbAddr.Text);
            __CPP.WriteLine("  set_val({0}_CHANGED,1);", lnkOut.MappedValueName);

            if (!string.IsNullOrWhiteSpace(tbCName.Text))
            {
                __CPP.WriteLine("#ifdef LCD");
                __CPP.WriteLine("  lcdPrintLine2(\"{0}\",p0);", tbCName.Text);
                __CPP.WriteLine("#endif  ");
            }
            __CPP.WriteLine("}");
        }

        public override void GenerateInit(CodeGeneratorContext context, TextWriter __CPP)
        {
            __CPP.WriteLine("set_val({0}, getEepVal({1}));", lnkOut.MappedValueName, tbAddr.Text);
            __CPP.WriteLine("set_val({0}_CHANGED,0);", lnkOut.MappedValueName);
        }
#endif
#if STM32
        public override void MapVariables(CodeGeneratorContext context)
        {
            base.MapVariables(context);
            lnkOut.DataMapIdx = (short)_DataIndex;
        }

        public override void GenerateSTMCode(CodeGeneratorContext context, STMProgram prog)
        {
            var variableIdx = prog.Variables.Add(
                (ushort)_DataIndex,
                ushort.Parse(tbAddr.Text),
                Title,
                context.CurrentModel.Index,
                short.Parse(tbStep.Text),
                -1000, 1000,
                short.Parse(tbInitVal.Text)
                );

            prog.StartupCode.Add(0x0F, variableIdx, short.Parse(tbInitVal.Text));

            context.ModelCode.Add(0x0E,
                    lnkInc.LinkedTo.DataMapIdx,
                    lnkDec.LinkedTo.DataMapIdx,
                    short.Parse(tbStep.Text),
                    lnkReset.LinkedTo != null ? lnkReset.LinkedTo.DataMapIdx : 0,
                    short.Parse(tbInitVal.Text),
                    variableIdx
                );

            prog.Blocks.Add(new STMBlockInfo(this, context.CurrentModel, null, new[] { prog.Variables[variableIdx] }));
        }
#endif
    }
}
