using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class VariableDesignPanel : DraggableElement
    {
        private int _DataIndex = -1;

        public VariableDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkInc, lnkDec, lnkReset, lnkOut);
        }

        private void tbInitVal_Validating(object sender, CancelEventArgs e)
        {
            ValidateControl(tbInitVal, () => ValidateInt(tbInitVal.Text));
        }

        private void tbStep_Validating(object sender, CancelEventArgs e)
        {
            ValidateControl(tbStep, () =>
            {
                int dummy;
                return int.TryParse(tbStep.Text, out dummy);
            });
        }

        public override XElement CreatXMLSave()
        {
            return new XElement("Variable");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);

            data.Add(
                new XAttribute("DataIndex", _DataIndex),
                new XAttribute("InitVal", tbInitVal.Text),
                new XAttribute("Step", tbStep.Text)
                );
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);
            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            tbInitVal.Text = data.AttributeValue("InitVal");
            tbStep.Text = data.AttributeValue("Step");
        }

        public override void TakeData(int[] data)
        {
            if(_DataIndex < 0) return;

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

            if (lnkInc.LinkedTo == null)
                throw new CompilationCheckException("Increment click is not defined", CompilationCheckException.CompileIteration.PreCheck);

            if (lnkDec.LinkedTo == null)
                throw new CompilationCheckException("Decrement click is not defined", CompilationCheckException.CompileIteration.PreCheck);
        }

#if MEGA2560

        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            __H.WriteLine("#define {0} {1}", lnkOut.MappedValueName, _DataIndex);
            __H.WriteLine("#define {0}_CHANGED {1}", lnkOut.MappedValueName, _DataIndex + 1);
        }

        public override void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            __CPP.WriteLine("set_val({0}_CHANGED,0);", lnkOut.MappedValueName);

            __CPP.WriteLine("if({0}_PRESSED)", lnkInc.LinkedTo.MappedValueName);
            __CPP.WriteLine("{");
            __CPP.WriteLine("  set_val({0}, p0 = (get({0}) + ({1})) );", lnkOut.MappedValueName, tbStep.Text);
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
            __CPP.WriteLine("  set_val({0}, p0 = (get({0}) - ({1})) );", lnkOut.MappedValueName, tbStep.Text);
            __CPP.WriteLine("  set_val({0}_CHANGED,1);", lnkOut.MappedValueName);

            if (!string.IsNullOrWhiteSpace(tbCName.Text))
            {
                __CPP.WriteLine("#ifdef LCD");
                __CPP.WriteLine("  lcdPrintLine2(\"{0}\",p0);", tbCName.Text);
                __CPP.WriteLine("#endif  ");
            }
            __CPP.WriteLine("}");

            if (lnkReset.LinkedTo == null) return;

            __CPP.WriteLine("else if({0}_PRESSED)", lnkReset.LinkedTo.MappedValueName);
            __CPP.WriteLine("{");
            __CPP.WriteLine("  set_val({0}, p0 = {1} );", lnkOut.MappedValueName, tbInitVal.Text);
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
            __CPP.WriteLine("set_val({0}, {1});", lnkOut.MappedValueName, tbInitVal.Text);
            __CPP.WriteLine("set_val({0}_CHANGED,0);", lnkOut.MappedValueName);
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
            prog.StartupCode.Add(0x0D, _DataIndex, short.Parse(tbInitVal.Text));

            var variableIdx = prog.Variables.Add((ushort) _DataIndex, 0, Title,
                context.CurrentModel.Index,
                short.Parse(tbStep.Text), -1000, 1000,
                short.Parse(tbInitVal.Text));

            context.ModelCode.Add(0x0E, 
                    lnkInc.LinkedTo.DataMapIdx,
                    lnkDec.LinkedTo.DataMapIdx,
                    short.Parse(tbStep.Text),
                    lnkReset.LinkedTo != null ? lnkReset.LinkedTo.DataMapIdx : 0,
                    short.Parse(tbInitVal.Text),
                    variableIdx
                );

            //prog.Blocks.Add(new STMBlockInfo(this, context.CurrentModel, null, new[] { prog.Variables[variableIdx] }));
        }
#endif

    }
}
