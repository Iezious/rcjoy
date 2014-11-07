using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class LogicalOrButtonDesignPanel : DraggableElement
    {
        private int _DataIndex = -1;

        public LogicalOrButtonDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkBtn1, lnkBtn2, lnkOut);
        }

        public override XElement CreatXMLSave()
        {
            return new XElement("LogicalOrButton");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);
            data.Add(new XAttribute("DataIndex", _DataIndex));
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);
            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
        }

        public override void TakeData(int[] data)
        {
            if(_DataIndex < 0) return;
#if STM32
            lblValue1.ForeColor = 
                (data[_DataIndex] & 0x1) == 0x1 ? Color.Red : SystemColors.WindowText;
#endif
        }

        public override void LinkIndex(ref int Counter)
        {
#if STM32
            _DataIndex = Counter;
            Counter++;
#endif
        }

        public override bool CheckDetermined()
        {
            return Determined = lnkBtn1.LinkedTo != null && lnkBtn1.LinkedTo.HolderPanel.Determined &&
                                lnkBtn2.LinkedTo != null && lnkBtn2.LinkedTo.HolderPanel.Determined;
        }

        public override void Check(CodeGeneratorContext context)
        {
            if (lnkBtn1.LinkedTo ==null)
                throw new CompilationCheckException("Button link for logical button can't be empty", CompilationCheckException.CompileIteration.PreCheck);

            if (lnkBtn2.LinkedTo == null)
                throw new CompilationCheckException("Button link for logical button can't be empty", CompilationCheckException.CompileIteration.PreCheck);
        }

#if MEGA2560
        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            __H.Write("#define {0}_DOWN ", lnkOut.MappedValueName);
            __H.Write("({0}_DOWN)", lnkClick.LinkedTo.MappedValueName);

            if (lnkMod1.LinkedTo != null)
                __H.Write(" && ({0}_DOWN)", lnkMod1.LinkedTo.MappedValueName);

            if (lnkMod2.LinkedTo != null)
                __H.Write(" && !({0}_DOWN)", lnkMod2.LinkedTo.MappedValueName);

            __H.WriteLine();


            __H.Write("#define {0}_PRESSED ", lnkOut.MappedValueName);
            __H.Write("({0}_PRESSED)", lnkClick.LinkedTo.MappedValueName);

            if (lnkMod1.LinkedTo != null)
                __H.Write(" && ({0}_DOWN)", lnkMod1.LinkedTo.MappedValueName);

            if (lnkMod2.LinkedTo != null)
                __H.Write(" && !({0}_DOWN)", lnkMod2.LinkedTo.MappedValueName);

            __H.WriteLine();
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

            prog.CommonCode.Add(
                0x33,
                    lnkBtn1.LinkedTo.DataMapIdx,
                    lnkBtn2.LinkedTo.DataMapIdx,
                    _DataIndex
                );


            base.GenerateSTMCode(context, prog);

//            context.ModelCode.Commands.Add(new STMCommand(
//                0x09, 
//                    lnkClick.LinkedTo.DataMapIdx,
//                    (lnkMod1.LinkedTo != null ? lnkMod1.LinkedTo.DataMapIdx : 0),
//                    (lnkMod2.LinkedTo != null ? lnkMod2.LinkedTo.DataMapIdx : 0),
//                    _DataIndex
//                ));
        }
#endif
    }
}
