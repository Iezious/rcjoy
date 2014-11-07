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
    public partial class AilronToRudderMixerDesignPanel : DraggableElement
    {
        private int _DataIndex = -1;

        public AilronToRudderMixerDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkAileron, lnkRudder, lnkOut);
        }

        private void tbAilQuef_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ValidateControl(tbAilQuef, () =>
                {
                    int i;

                    if (!int.TryParse(tbAilQuef.Text, out i))
                        return false;

                    return i > -95 && i < 95;
                });
        }

        public override XElement Serialize()
        {
            return new XElement("AilRudderMixer",
                                   new XAttribute("Top", Top),
                                   new XAttribute("Left", Left),
                                   new XAttribute("ID", ID),
                                   new XAttribute("DataIndex", _DataIndex),
                                   new XAttribute("AilAff", tbAilQuef.Text),
                                   SerializeLinks()
                );
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);
            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            tbAilQuef.Text = data.AttributeValue("AilAff");
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
            if (!ValidateControl(tbAilQuef, () =>
            {
                int i;

                if (!int.TryParse(tbAilQuef.Text, out i))
                    return false;

                return i >= -95 && i <= 95;
            }))
                throw new CompilationCheckException("Wrong ailerons quotient value", CompilationCheckException.CompileIteration.PreCheck);

            if (lnkAileron.LinkedTo == null)
                throw new CompilationCheckException("Ailerons is not linked",
                    CompilationCheckException.CompileIteration.PreCheck);

            if (lnkRudder.LinkedTo == null)
                throw new CompilationCheckException("Rudder is not linked",
                    CompilationCheckException.CompileIteration.PreCheck);
        }
#if MEGA2560

        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            __H.WriteLine("#define {0} {1}", lnkOut.MappedValueName, _DataIndex);
        }

        public override void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            __CPP.WriteLine("mix_aileron_to_rudder({0}, {1}, {2}, {3});",
                lnkRudder.LinkedTo.MappedValueName,
                lnkAileron.LinkedTo.MappedValueName,
                tbAilQuef.Text,
                lnkOut.MappedValueName);
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
            context.ModelCode.Add(0x16, lnkRudder.LinkedTo.DataMapIdx, lnkAileron.LinkedTo.DataMapIdx,
                short.Parse(tbAilQuef.Text), _DataIndex);
        }

#endif
    }
}
