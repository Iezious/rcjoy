using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class ExponentDesignPanel : DraggableElement
    {
        private int _DataIndex = -1;
        private bool _Centered = true;

        public bool Centered
        {
            get { return _Centered; }
            set
            {
                _Centered = value;
                Title = _Centered ? "Exponent" : "Throttle exponent";
            }
        }

        public ExponentDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkIn, lnkOut);
        }

        private void tbAilQuef_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbExp, () =>
                {
                    int i;

                    if (!int.TryParse(tbExp.Text, out i))
                        return false;

                    return i >= -10 && i <= 10;
                });
        }

        public override XElement Serialize()
        {
            return new XElement("Exponent",
                                   new XAttribute("Top", Top),
                                   new XAttribute("Left", Left),
                                   new XAttribute("ID", ID),
                                   new XAttribute("DataIndex", _DataIndex),
                                   new XAttribute("Effect", tbExp.Text),
                                   new XAttribute("Centered", _Centered),
                                   SerializeLinks()
                );
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);

            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            Centered = bool.Parse(data.AttributeValue("Centered") ?? "true");
            tbExp.Text = data.AttributeValue("Effect");
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
            if(!ValidateControl(tbExp, () =>
                {
                    int i;

                    if (!int.TryParse(tbExp.Text, out i))
                        return false;

                    return i >= -10 && i <= 10;
                }))
            throw new CompilationCheckException("Wrong exponent value",CompilationCheckException.CompileIteration.PreCheck);

            if (lnkIn.LinkedTo == null)
                throw new CompilationCheckException("Input value is not linked for the exponent modifier",
                    CompilationCheckException.CompileIteration.PreCheck);
        }
#if MEGA2560

        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            __H.WriteLine("#define {0} {1}",lnkOut.MappedValueName, _DataIndex);
        }

        public override void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            if(Centered)
                __CPP.WriteLine("map_exp({0}, {1}, {2});",lnkIn.LinkedTo.MappedValueName, tbExp.Text, lnkOut.MappedValueName);
            else
                __CPP.WriteLine("map_th_exp({0}, {1}, {2});", lnkIn.LinkedTo.MappedValueName, tbExp.Text, lnkOut.MappedValueName);

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
            if(Centered)
                context.ModelCode.Add(0x12, lnkIn.LinkedTo.DataMapIdx, short.Parse(tbExp.Text), _DataIndex);
            else
                context.ModelCode.Add(0x13, lnkIn.LinkedTo.DataMapIdx, short.Parse(tbExp.Text), _DataIndex);
        }
#endif
    }
}
