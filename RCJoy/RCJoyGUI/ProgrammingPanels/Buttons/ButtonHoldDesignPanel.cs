using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class ButtonHoldDesignPanel : DraggableElement
    {
        public int _DataIndex = -1;

        public ButtonHoldDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkIn, lnkOut);
        }

        private void tbPressed_Validating(object sender, CancelEventArgs e)
        {
            ValidateControl(tbPressed, () => ValidateInt(tbPressed.Text));
        }

        private void tbReleased_Validating(object sender, CancelEventArgs e)
        {
            ValidateControl(tbReleased, () => ValidateInt(tbReleased.Text));
        }

        public override XElement Serialize()
        {
            return new XElement("ButtonHoldSwitcher",
               new XAttribute("Top", Top),
               new XAttribute("Left", Left),
               new XAttribute("ID", ID),
               new XAttribute("DataIndex", _DataIndex),
               new XAttribute("Pressed", tbPressed.Text),
               new XAttribute("Released", tbReleased.Text),
               SerializeLinks()
           );
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);

            tbPressed.Text = data.AttributeValue("Pressed");
            tbReleased.Text = data.AttributeValue("Released");
            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
        }

        public override void TakeData(int[] data)
        {
            if (_DataIndex < 0) return;

            lblValue.Text = data[_DataIndex].ToString(CultureInfo.InvariantCulture);
        }

        public override void LinkIndex(ref int Counter)
        {
            _DataIndex = Counter;

            Counter+=2;
        }

        public override void Check(CodeGeneratorContext context)
        {
            if (!ValidateControl(tbPressed, () => ValidateInt(tbPressed.Text)))
                throw new CompilationCheckException("Wrond value for pressed state", CompilationCheckException.CompileIteration.PreCheck);

            if (!ValidateControl(tbReleased, () => ValidateInt(tbReleased.Text)))
                throw new CompilationCheckException("Wrond value for pressed state", CompilationCheckException.CompileIteration.PreCheck);
            
        }


#if STM32
        public override void GenerateSTMCode(CodeGeneratorContext context, STMProgram prog)
        {
            var pIdx = short.Parse(tbPressed.Text);
            var rIdx = short.Parse(tbReleased.Text);

            var pBtnIdx = lnkIn.LinkedTo.DataMapIdx;
            
            context.ModelCode.Commands.Add(new STMCommand(
                    0x08, pBtnIdx, pIdx, rIdx, (short)_DataIndex 
                ));
        }

        public override void MapVariables(CodeGeneratorContext context)
        {
            base.MapVariables(context);
            lnkOut.DataMapIdx = (short)_DataIndex;
        }
#endif


#if MEGA2560

        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            __H.WriteLine("#define {0} {1}", lnkOut.MappedValueName, _DataIndex);
            __H.WriteLine("#define {0}_CHANGED {1}", lnkOut.MappedValueName, _DataIndex + 1);
        }

        public override void GenerateInit(CodeGeneratorContext context, TextWriter __CPP)
        {
            __CPP.WriteLine("    set({0}, {1});", lnkOut.MappedValueName, tbReleased.Text);
        }

        public override void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            __CPP.WriteLine("if({0}_DOWN)", lnkIn.LinkedTo.MappedValueName);
            __CPP.WriteLine("{");
            __CPP.WriteLine("  p0 = {0};", tbPressed.Text);
            __CPP.WriteLine("} else {");
            __CPP.WriteLine("  p0 = {0};", tbReleased.Text);
            __CPP.WriteLine("}");

            __CPP.WriteLine("set({0}_CHANGED, p0 != get({0}));", lnkOut.MappedValueName);
            __CPP.WriteLine("set({0}, p0);", lnkOut.MappedValueName);
        }

#endif
    }
}
