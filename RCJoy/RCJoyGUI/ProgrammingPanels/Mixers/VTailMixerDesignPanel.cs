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
    public partial class VTailMixerDesignPanel : DraggableElement
    {
        private int _DataIndex = -1;

        public VTailMixerDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkRudder, lnkElev, lnkOut1, lnkOut2);
        }

        private void tbAilQuef_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ValidateControl(tbRudQuef, () =>
                {
                    int i;

                    if (!int.TryParse(tbRudQuef.Text, out i))
                        return false;

                    return i > 5 && i < 95;
                });
        }

        public override XElement Serialize()
        {
            return new XElement("VTAilMixer",
                                   new XAttribute("Top", Top),
                                   new XAttribute("Left", Left),
                                   new XAttribute("ID", ID),
                                   new XAttribute("DataIndex", _DataIndex),
                                   new XAttribute("RudAff", tbRudQuef.Text),
                                   SerializeLinks()
                );
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);

            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            tbRudQuef.Text = data.AttributeValue("RudAff");
        }

        public override void TakeData(int[] data)
        {
            if(_DataIndex < 0) return;

            lblValue1.Text = data[_DataIndex].ToString(CultureInfo.InvariantCulture);
            lblValue2.Text = data[_DataIndex+1].ToString(CultureInfo.InvariantCulture);
        }

        public override void LinkIndex(ref int Counter)
        {
            _DataIndex = Counter;
            Counter+=2;
        }

        public override void Check(CodeGeneratorContext context)
        {
            if (!ValidateControl(tbRudQuef, () =>
            {
                int i;

                if (!int.TryParse(tbRudQuef.Text, out i))
                    return false;

                return i >= -95 && i <= 95;
            }))
                throw new CompilationCheckException("Wrong rudder quotient value", CompilationCheckException.CompileIteration.PreCheck);

            if (lnkElev.LinkedTo == null)
                throw new CompilationCheckException("Elevator is not linked",
                    CompilationCheckException.CompileIteration.PreCheck);

            if (lnkRudder.LinkedTo == null)
                throw new CompilationCheckException("Rudder is not linked",
                    CompilationCheckException.CompileIteration.PreCheck);
        }

#if MEGA2560

        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            __H.WriteLine("#define {0} {1}", lnkOut1.MappedValueName, _DataIndex);
            __H.WriteLine("#define {0} {1}", lnkOut2.MappedValueName, _DataIndex + 1);
        }

        public override void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            __CPP.WriteLine("mix_delta({0}, {1}, {2}, {3}, {4});",
                lnkElev.LinkedTo.MappedValueName,
                lnkRudder.LinkedTo.MappedValueName,
                tbRudQuef.Text,
                lnkOut1.MappedValueName,
                lnkOut2.MappedValueName);
        }
#endif

#if STM32

        public override void MapVariables(CodeGeneratorContext context)
        {
            base.MapVariables(context);
            lnkOut1.DataMapIdx = (short)_DataIndex;
            lnkOut2.DataMapIdx = (short)(_DataIndex + 1);
        }

        public override void GenerateSTMCode(CodeGeneratorContext context, STMProgram prog)
        {
            context.ModelCode.Add(0x19, lnkRudder.LinkedTo.DataMapIdx, lnkElev.LinkedTo.DataMapIdx,
                short.Parse(tbRudQuef.Text), _DataIndex);
        }

#endif
    }
}
