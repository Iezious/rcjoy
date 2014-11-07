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
    public partial class ValueMapperDesignPanel : DraggableElement
    {
        private readonly List<ValueMapperDesignPanelValuePanel> __Panels = new List<ValueMapperDesignPanelValuePanel>(16);
        private int _DataIndex = -1;

        public ValueMapperDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkIn, lnkOut);
            cbCasesCount.SelectedIndex = 0;
        }

        private void tbDefault_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbDefault, () => ValidateInt(tbDefault.Text));
        }


        private void cbCasesCount_SelectedValueChanged(object sender, EventArgs e)
        {
            SetPanelsCount(int.Parse((string)cbCasesCount.SelectedItem));
        }

        private void SetPanelsCount(int count)
        {
            if (__Panels.Count == count) return;

            while (__Panels.Count > count)
            {
                var pnl = __Panels.LastOrDefault();
                if (pnl == null) break;

                __Panels.RemoveAt(__Panels.Count - 1);
                Controls.Remove(pnl);
            }

            while (__Panels.Count < count)
            {
                var pnl = new ValueMapperDesignPanelValuePanel();
                Controls.Add(pnl);

                pnl.BackColor = Color.Transparent;
                pnl.Left = 5;
                pnl.Width = Width - 10;

                var pred = __Panels.LastOrDefault();
                pnl.Top = pred == null ? cbCasesCount.Bottom + 2 : pred.Bottom + 1;

                __Panels.Add(pnl);
            }

            if (__Panels.Count == 0)
                Height = cbCasesCount.Bottom + 4;
            else
                Height = __Panels.Last().Bottom + 2;

            Invalidate();
            Update();
        }

        public override XElement CreatXMLSave()
        {
            return new XElement("ValueMapper");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);
            data.Add(
                new XAttribute("DataIndex", _DataIndex),
                new XAttribute("Default", tbDefault.Text),
                SerializePanels()
            );
        }

        private XElement SerializePanels()
        {
            return new XElement("Panels", __Panels.Select(pnl => pnl.Serialize()));
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);

            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            tbDefault.Text = data.AttributeValue("Default");

            DeserealizePanels(data);
        }

        private void DeserealizePanels(XElement data)
        {
            var xpanels = data.Element("Panels");
            if (xpanels == null)
            {
                SetPanelsCount(0);
                return;
            }

            var xelems = xpanels.Elements("Case").ToArray();
            if (xelems.Length == 0)
            {
                SetPanelsCount(0);
                return;
            }

            SetPanelsCount(xelems.Length);

            for (var i = 0; i < xelems.Length; i++)
            {
                __Panels[i].Deserialize(xelems[i]);
            }


            cbCasesCount.SelectedItem = "" + xelems.Length;
        }

        public override void TakeData(int[] data)
        {
            if (_DataIndex < 0) return;

            lblValue.Text = data[_DataIndex].ToString(CultureInfo.InvariantCulture);
        }

        public override void LinkIndex(ref int Counter)
        {
            _DataIndex = Counter;
            Counter += 2;
        }

        public override void Check(CodeGeneratorContext context)
        {
            if (lnkIn.LinkedTo == null)
                throw new CompilationCheckException("Input link for value mapper is not defined", CompilationCheckException.CompileIteration.PreCheck);

            if(!ValidateControl(tbDefault, () => ValidateInt(tbDefault.Text)))
                throw new CompilationCheckException("Wrond default value in value mapper", CompilationCheckException.CompileIteration.PreCheck);

            foreach (var panel in __Panels) panel.Check();

            var cases = new HashSet<int>();

            foreach (var val in __Panels.Select(panel => panel.Case))
            {
                if(cases.Contains(val))
                    throw new CompilationCheckException("Duplate case value for value mapper", CompilationCheckException.CompileIteration.PreCheck);

                cases.Add(val);
            }
        }
#if MEGA2560

        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            __H.WriteLine("#define {0} {1}", lnkOut.MappedValueName, _DataIndex);
            __H.WriteLine("#define {0}_CHANGED {1}", lnkOut.MappedValueName, _DataIndex + 1);
        }

        public override void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            __CPP.WriteLine("p0 = get({0});", lnkIn.LinkedTo.MappedValueName);
            __CPP.WriteLine("switch(p0)");
            __CPP.WriteLine("{");

            foreach (var panel in __Panels)
                __CPP.WriteLine("  case {0}: p1={1}; break;", panel.Case, panel.Value);

            __CPP.WriteLine("  default: p1={0}; break;", tbDefault.Text);

            __CPP.WriteLine("}");
            __CPP.WriteLine("set({0}_CHANGED, p1 != get({0}));", lnkOut.MappedValueName);
            __CPP.WriteLine("set({0}, p1);", lnkOut.MappedValueName);

            base.GenerateCalculator(context, __CPP);
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
            var args = new List<IConvertible>();

            args.Add(lnkIn.LinkedTo.DataMapIdx);
            args.Add(short.Parse(tbDefault.Text));
            args.Add(__Panels.Count);
            args.Add(_DataIndex);

            foreach (var panel in __Panels)
            {
                args.Add(panel.Case);
                args.Add(panel.Value);
            }

            context.ModelCode.Add(0x0A, args.ToArray());

            base.GenerateSTMCode(context, prog);
        }
#endif
    }
}
