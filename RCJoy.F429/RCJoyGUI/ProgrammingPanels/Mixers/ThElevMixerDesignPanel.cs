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
    public partial class ThrottleToElevMixerDesignPanel : DraggableElement
    {
        private int _DataIndex = -1;

        public ThrottleToElevMixerDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkElevator, lnkThrottle, lnkOut);
        }

        private void tbAilQuef_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ValidateControl(tbThQuef, () =>
                {
                    int i;

                    if (!int.TryParse(tbThQuef.Text, out i))
                        return false;

                    return i > -95 && i < 95;
                });
        }

        public override XElement CreatXMLSave()
        {
            return new XElement("ThRudderMixer");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);
            data.Add(
                new XAttribute("DataIndex", _DataIndex),
                new XAttribute("ThAff", tbThQuef.Text)
            );
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);

            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            tbThQuef.Text = data.AttributeValue("ThAff");
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
            if (!ValidateControl(tbThQuef, () =>
            {
                int i;

                if (!int.TryParse(tbThQuef.Text, out i))
                    return false;

                return i >= -95 && i <= 95;
            }))
                throw new CompilationCheckException("Wrong throttle quotient value", CompilationCheckException.CompileIteration.PreCheck);

            if (lnkElevator.LinkedTo == null)
                throw new CompilationCheckException("Elevator is not linked",
                    CompilationCheckException.CompileIteration.PreCheck);

            if (lnkThrottle.LinkedTo == null)
                throw new CompilationCheckException("Throttle is not linked",
                    CompilationCheckException.CompileIteration.PreCheck);
        }

#if MEGA2560

        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            __H.WriteLine("#define {0} {1}", lnkOut.MappedValueName, _DataIndex);
        }

        public override void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            __CPP.WriteLine("mix_throttle_to_elev({0}, {1}, {2}, {3});",
                lnkElevator.LinkedTo.MappedValueName,
                lnkThrottle.LinkedTo.MappedValueName,
                tbThQuef.Text,
                lnkOut.MappedValueName);
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
            context.ModelCode.Add(0x17, lnkThrottle.LinkedTo.DataMapIdx, lnkElevator.LinkedTo.DataMapIdx,
                short.Parse(tbThQuef.Text), _DataIndex);

            base.GenerateSTMCode(context, prog);
        }

#endif
    }
}
