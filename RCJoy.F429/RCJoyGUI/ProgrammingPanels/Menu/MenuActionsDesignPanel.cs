using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels.Menu
{
    public partial class MenuActionsDesignPanel : DraggableElement
    {
        public MenuActionsDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkNextSection, lnkPrevSection, lnkNextSelection, lnkPrevSelection,
                lnkIncValue, lnkDecValue, lnkExecute);
        }

        public override XElement CreatXMLSave()
        {
            return new XElement("MenuActions");
        }

        public override void Check(CodeGeneratorContext context)
        {
            if(GetLinks().Any(lnk => lnk.LinkedTo == null))
                throw new CompilationCheckException("Not all actions are linked to buttons",
                    CompilationCheckException.CompileIteration.PreCheck);
        }

        public override void LinkIndex(ref int Counter)
        {
            
        }

        public override void GenerateSTMCode(CodeGeneratorContext context, STMProgram prog)
        {
            context.ModelCode.Add(0x1C, 
                lnkNextSection.LinkedTo.DataMapIdx,
                lnkPrevSection.LinkedTo.DataMapIdx,
                lnkNextSelection.LinkedTo.DataMapIdx,
                lnkPrevSelection.LinkedTo.DataMapIdx,
                lnkIncValue.LinkedTo.DataMapIdx,
                lnkDecValue.LinkedTo.DataMapIdx,
                lnkExecute.LinkedTo.DataMapIdx);
        }
    }
}
