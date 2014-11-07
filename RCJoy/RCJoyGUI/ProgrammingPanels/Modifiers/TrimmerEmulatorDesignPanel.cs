using System.Globalization;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class TrimmerEmulatorDesignPanel : DraggableElement
    {
        private int _DataIndex = -1;

        public TrimmerEmulatorDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkAxis, lnkSet, lnkReset, lnkReset, lnkOut);
        }
        public override XElement Serialize()
        {
            return new XElement("TrimmerEmulator",
                                   new XAttribute("Top", Top),
                                   new XAttribute("Left", Left),
                                   new XAttribute("ID", ID),
                                   new XAttribute("DataIndex", _DataIndex),
                                   SerializeLinks()
                );
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);

            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
        }

        public override void TakeData(int[] data)
        {
            if(_DataIndex < 0) return;

            lblValue1.Text = data[_DataIndex].ToString(CultureInfo.InvariantCulture);
        }

        public override void LinkIndex(ref int Counter)
        {
            _DataIndex = Counter;
            Counter+=3;
        }

        public override void Check(CodeGeneratorContext context)
        {

            if (lnkAxis.LinkedTo == null)
                throw new CompilationCheckException("Input axis is not linked for the trimmer modifier",
                    CompilationCheckException.CompileIteration.PreCheck);
            
            if (lnkSet.LinkedTo == null)
                throw new CompilationCheckException("Set click is not defined", CompilationCheckException.CompileIteration.PreCheck);

            if (lnkReset.LinkedTo == null)
                throw new CompilationCheckException("Reset click is not defined", CompilationCheckException.CompileIteration.PreCheck);
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
            prog.StartupCode.Add(0x24, _DataIndex);

            context.ModelCode.Add(0x23,
                    lnkSet.LinkedTo.DataMapIdx,
                    lnkReset.LinkedTo.DataMapIdx,
                    lnkAxis.LinkedTo.DataMapIdx,
                    _DataIndex
                );
        }
#endif
    }
}
