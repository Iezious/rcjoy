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
    public partial class FlaperonMixerDesignPanelStored : DraggableElement, IFRAMUser
    {
        private int _DataIndex = -1;
        private uint _Addr;
        private bool _UseFRAM = false;

        public uint SlotsUsed { get { return (uint)(_UseFRAM ? 1 : 0); } }
        public uint[] FRAMAddresses
        {
            get
            {
                return _UseFRAM ? new[] { _Addr } : new uint[0];
            }
            set
            {
                if (_UseFRAM) _Addr = value[0];
            }
        }

        public string[] Names
        {
            get
            {
                return _UseFRAM ? new[] { Title + " flap" } : new string[0];
            }
        }

        public int[] DefaultValues
        {
            get
            {
                return _UseFRAM ? new[] { int.Parse(tbFlapQuef.Text) } : new int[0];
            }
        }

        public override void Removed()
        {
            FRAMMapper.UnRegisterUser(this);
        }

        public override void Initialized()
        {
            FRAMMapper.Register(this);
        }
      
        public FlaperonMixerDesignPanelStored()
        {
            InitializeComponent();
            RegiseterLink(lnkAileron, lnkFlaps, lnkOut1, lnkOut2);
        }

        private void cbEEPROM_CheckedChanged(object sender, EventArgs e)
        {
            if (_UseFRAM != cbEEPROM.Checked)
            {
                _UseFRAM = cbEEPROM.Checked;
                FRAMMapper.ReAssign(this);
            }
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

        public override XElement CreatXMLSave()
        {
            return new XElement("FlaperonsMixerStored");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);
            data.Add(
                new XAttribute("DataIndex", _DataIndex),
                new XAttribute("FlapAff", tbFlapQuef.Text),
                new XAttribute("Addr", _Addr.ToString(CultureInfo.InvariantCulture)),
                new XAttribute("WriteFRAM", cbEEPROM.Checked)
                );
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);

            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            tbFlapQuef.Text = data.AttributeValue("FlapAff");
            cbEEPROM.Checked = _UseFRAM = bool.Parse(data.AttributeValue("WriteFRAM") ?? "false");
            _Addr = uint.Parse(data.AttributeValue("Addr") ?? "0");
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
            Counter += 3;
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
            var VarDataIndex = (ushort)(_DataIndex + 2);

            var variableIdx = prog.Variables.Add(
                VarDataIndex,
                (ushort)(cbEEPROM.Checked ? _Addr : 0),
                Title + " flap",
                context.CurrentModel.Index,
                5, -95, 95, short.Parse(tbFlapQuef.Text)
            );

            if (cbEEPROM.Checked)
            {
                prog.StartupCode.Add(0x0F, variableIdx, short.Parse(tbFlapQuef.Text));
            }
            else
            {
                prog.StartupCode.Add(0x0D, VarDataIndex, short.Parse(tbFlapQuef.Text));
            }

            context.ModelCode.Add(0x27, lnkAileron.LinkedTo.DataMapIdx, lnkFlaps.LinkedTo.DataMapIdx,
                VarDataIndex, _DataIndex);

            base.GenerateSTMCode(context, prog);
            prog.Blocks.Add(new STMBlockInfo(this, context.CurrentModel, GetLinks(), new[] { prog.Variables[variableIdx] }));
        }



#endif

    }
}
