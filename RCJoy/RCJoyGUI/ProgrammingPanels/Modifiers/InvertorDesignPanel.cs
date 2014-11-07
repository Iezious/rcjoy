using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class InvertorDesignPanel : DraggableElement
    {
        private int _DataIndex = -1;

        public InvertorDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkIn, lnkOut);
        }

        public override XElement Serialize()
        {
            return new XElement("Invertor",
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
            Counter++;
        }

        public override void Check(CodeGeneratorContext context)
        {
            if (lnkIn.LinkedTo == null)
                throw new CompilationCheckException("Input value is not linked for the invert modifier",
                    CompilationCheckException.CompileIteration.PreCheck);
        }

#if MEGA2560
        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            __H.WriteLine("#define {0} {1}", lnkOut.MappedValueName, _DataIndex);
        }

        public override void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            __CPP.WriteLine("invert({0}, {1});", lnkIn.LinkedTo.MappedValueName, lnkOut.MappedValueName);
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
            context.ModelCode.Add(0x14, lnkIn.LinkedTo.DataMapIdx, _DataIndex);
        }
#endif
    }
}
