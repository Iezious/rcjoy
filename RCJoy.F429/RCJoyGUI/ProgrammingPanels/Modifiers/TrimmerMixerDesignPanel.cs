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
    public partial class TrimmerDesignPanel : DraggableElement
    {
        private int _DataIndex = -1;

        public TrimmerDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkAxis, lnkValue, lnkOut);
        }

        public override XElement CreatXMLSave()
        {
            return new XElement("Trimmer");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);
            data.Add(
                new XAttribute("DataIndex", _DataIndex),
                new XAttribute("TrimType", cbTrimmerType.SelectedIndex)
                );
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);

            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            cbTrimmerType.SelectedIndex = int.Parse(data.AttributeValue("TrimType") ?? "0");

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
            if (lnkAxis.LinkedTo == null)
                throw new CompilationCheckException("Input axis is not linked for the trimmer modifier",
                    CompilationCheckException.CompileIteration.PreCheck);

            if (lnkValue.LinkedTo == null)
                throw new CompilationCheckException("Input trim value is not linked for the trimmer modifier",
                    CompilationCheckException.CompileIteration.PreCheck);
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
            context.ModelCode.Add(0x11, lnkAxis.LinkedTo.DataMapIdx, lnkValue.LinkedTo.DataMapIdx, _DataIndex);
            context.ModelCode.Add(0x2D, lnkValue.LinkedTo.DataMapIdx, cbTrimmerType.SelectedIndex);
            base.GenerateSTMCode(context, prog);
        }
#endif
    }
}
