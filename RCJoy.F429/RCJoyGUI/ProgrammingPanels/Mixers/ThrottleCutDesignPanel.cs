using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class ThrottleCutDesignPanel : DraggableElement
    {
        public int _DataIndex = -1;

        public ThrottleCutDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkThrottleIn, lnkEnableSwitch, lnkDisable, lnkOut);
        }


        public override XElement CreatXMLSave()
        {
            return new XElement("ThrottleCut");
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

        public override void Check(CodeGeneratorContext context)
        {
            if (lnkThrottleIn.LinkedTo == null)
                throw new CompilationCheckException("Throttle channel is not linked",
                    CompilationCheckException.CompileIteration.PreCheck);

            if (lnkEnableSwitch.LinkedTo == null)
                throw new CompilationCheckException("Enable/switch button is not linked",
                    CompilationCheckException.CompileIteration.PreCheck);
        }


        public override void LinkIndex(ref int Counter)
        {
            _DataIndex = Counter;
            Counter += 2;
        }

        public override void TakeData(int[] data)
        {
            if (_DataIndex < 0) return;

            lblOut.Text = data[_DataIndex].ToString(CultureInfo.InvariantCulture);
        }

        public override void MapVariables(CodeGeneratorContext context)
        {
            base.MapVariables(context);
            lnkOut.DataMapIdx = (short)_DataIndex;
        }

        public override void GenerateSTMCode(CodeGeneratorContext context, STMProgram prog)
        {
            prog.StartupCode.Add(0x30, _DataIndex);

            if (lnkDisable.LinkedTo != null)
            {
                context.ModelCode.Add(0x31,
                    lnkThrottleIn.LinkedTo.DataMapIdx,
                    lnkEnableSwitch.LinkedTo.DataMapIdx,
                    lnkDisable.LinkedTo.DataMapIdx,
                    _DataIndex
                );
            }
            else
            {
                context.ModelCode.Add(0x32,
                    lnkThrottleIn.LinkedTo.DataMapIdx,
                    lnkEnableSwitch.LinkedTo.DataMapIdx,
                    _DataIndex
                );
            }


            base.GenerateSTMCode(context, prog);
        }

    }
}
