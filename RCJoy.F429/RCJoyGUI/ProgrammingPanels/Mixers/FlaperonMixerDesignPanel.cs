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
    public partial class FlaperonMixerDesignPanel : DraggableElement
    {
        private int _DataIndex = -1;

        public FlaperonMixerDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkAileron, lnkFlaps, lnkOut1, lnkOut2);
        }

        private void tbAilQuef_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ValidateControl(tbFlapQuef, () =>
                {
                    int i;

                    if (!int.TryParse(tbFlapQuef.Text, out i))
                        return false;

                    return i > -95 && i < 95;
                });
        }

        private void tbMaxUp_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ValidateControl(tbMaxUp, () =>
        {
            int i;

            if (!int.TryParse(tbMaxUp.Text, out i))
                return false;

            return i >= 0 && i <= 100;
        });
        }

        private void tbMaxDown_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = ValidateControl(tbMaxDown, () =>
        {
            int i;

            if (!int.TryParse(tbMaxDown.Text, out i))
                return false;

            return i >= 0 && i <= 100;
        });
        }

        public override XElement CreatXMLSave()
        {
            return new XElement("FlaperonsMixer");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);
            data.Add(
                new XAttribute("DataIndex", _DataIndex),
                new XAttribute("FlapAff", tbFlapQuef.Text),
                new XAttribute("MaxUp", tbMaxUp.Text),
                new XAttribute("MaxDown", tbMaxDown.Text)
                );
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);

            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            tbFlapQuef.Text = data.AttributeValue("FlapAff");
            tbMaxUp.Text = data.AttributeValue("MaxUp");
            tbMaxDown.Text = data.AttributeValue("MaxDown");
        }

        public override void TakeData(int[] data)
        {
            if (_DataIndex < 0) return;

            lblValue1.Text = data[_DataIndex].ToString(CultureInfo.InvariantCulture);
            lblValue2.Text = data[_DataIndex + 1].ToString(CultureInfo.InvariantCulture);
        }

        public override void LinkIndex(ref int Counter)
        {
            _DataIndex = Counter;
            Counter += 2;
        }

        public override void Check(CodeGeneratorContext context)
        {
            if (!ValidateControl(tbFlapQuef, () =>
            {
                int i;

                if (!int.TryParse(tbFlapQuef.Text, out i))
                    return false;

                return i >= -95 && i <= 95;
            }))
                throw new CompilationCheckException("Wrong flaps quotient value", CompilationCheckException.CompileIteration.PreCheck);

            if (lnkAileron.LinkedTo == null)
                throw new CompilationCheckException("Ailerons is not linked",
                    CompilationCheckException.CompileIteration.PreCheck);

            if (lnkFlaps.LinkedTo == null)
                throw new CompilationCheckException("Flaps is not linked",
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
            __CPP.WriteLine("mix_flapperons({0}, {1}, {2}, {3}, {4});",
                lnkAileron.LinkedTo.MappedValueName,
                lnkFlaps.LinkedTo.MappedValueName,
                tbFlapQuef.Text,
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
            context.ModelCode.Add(0x1A, lnkAileron.LinkedTo.DataMapIdx, lnkFlaps.LinkedTo.DataMapIdx,
                short.Parse(tbFlapQuef.Text), _DataIndex);

            base.GenerateSTMCode(context, prog);
        }

#endif

    }
}
