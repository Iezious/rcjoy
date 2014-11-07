using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class ValueAxisModifierDesignPanel : DraggableElement
    {
#if STM32
        private int _DataIndex;
#endif
        public ValueAxisModifierDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkIn, lnkOut);
        }

        public override XElement Serialize()
        {
            return new XElement("ValueAxisModifier",
                                   new XAttribute("Top", Top),
                                   new XAttribute("Left", Left),
                                   new XAttribute("ID", ID),
#if STM32                                   
                                   new XAttribute("DataIndex", _DataIndex),
#endif
                                   SerializeLinks()
                );
        }

#if STM32
        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);
            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
        }
#endif

        public override void TakeData(int[] data)
        {
#if STM32
            
#endif
        }

        public override void LinkIndex(ref int Counter)
        {
#if STM32
            _DataIndex = Counter;
            Counter++;
#endif
        }

        public override void Check(CodeGeneratorContext context)
        {
            if (lnkIn.LinkedTo == null)
                throw new CompilationCheckException("Input link is not set",
                    CompilationCheckException.CompileIteration.PreCheck);
        }

        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            __H.WriteLine("#define {0} {1}",lnkOut.MappedValueName, lnkIn.LinkedTo.MappedValueName);
        }
#if STM32

        public override void MapVariables(CodeGeneratorContext context)
        {
            base.MapVariables(context);
            lnkOut.DataMapIdx = (short) _DataIndex;
        }

        public override void GenerateSTMCode(CodeGeneratorContext context, STMProgram prog)
        {
            context.ModelCode.Add(0x10, lnkIn.LinkedTo.DataMapIdx, _DataIndex);
        }
#endif
    }
}
