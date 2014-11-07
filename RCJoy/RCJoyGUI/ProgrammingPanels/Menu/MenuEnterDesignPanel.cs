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
    public partial class MenuEnterDesignPanel : DraggableElement
    {
        public MenuEnterDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkClick);
        }

        public override XElement Serialize()
        {
            return new XElement("EnterMenu", new XAttribute("Top", Top), new XAttribute("Left", Left), new XAttribute("ID", ID), SerializeLinks());
        }

        public override void TakeData(int[] data)
        {
            
        }

        public override void LinkIndex(ref int Counter)
        {
            
        }

        public override void Check(CodeGeneratorContext context)
        {
            if(lnkClick.LinkedTo == null)
                throw new CompilationCheckException("Model switch click is not defined", CompilationCheckException.CompileIteration.PreCheck);
        }
#if MEGA2560

        public override void GeneratePreCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            __CPP.WriteLine("if({0}_PRESSED)", lnkClick.LinkedTo.MappedValueName);
            __CPP.WriteLine("{");
            __CPP.WriteLine("  switchModel(MODEL_INFO_{0});", context.CurrentModel.CName);
            __CPP.WriteLine("  #ifdef LCD");
            __CPP.WriteLine("  lcdPrintLine1(\"{0}\");",context.CurrentModel.Name);
            __CPP.WriteLine("  #endif");
            __CPP.WriteLine("}");
        }
#endif

#if STM32
        public override void GenerateSTMCode(CodeGeneratorContext context, STMProgram prog)
        {
            prog.CommonCode.Commands.Add(new STMCommand(
                0x1B, lnkClick.LinkedTo.DataMapIdx));
        }
#endif
    }
}
