using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class HatToButtonMapperDesignPanel : DraggableElement
    {
        private readonly List<HatToButtonMapperDesignPanelValuePanel> __Panels = new List<HatToButtonMapperDesignPanelValuePanel>(16);
        private int _DataIndex = -1;

        public HatToButtonMapperDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkIn);
            cbCasesCount.SelectedIndex = 0;
        }

        private void cbCasesCount_SelectedValueChanged(object sender, EventArgs e)
        {
            SetPanelsCount(int.Parse((string)cbCasesCount.SelectedItem));
        }

        private void SetPanelsCount(int panelsCount)
        {
            if (__Panels.Count > 0)
            {
                while (__Panels.Count > panelsCount)
                {
                    var pnl = __Panels[__Panels.Count - 1];
                    __Panels.RemoveAt(__Panels.Count - 1);

                    pnl.UnLink();
                    UnRegiseterLink(pnl.GetLinks());
                    pnl.Parent = null;
                    pnl.Dispose();
                }

                for (int i = __Panels.Count; i < panelsCount; i++)
                {
                    CreatePanel(i + 1);
                }

                if (__Panels.Count > 0)
                    Height = __Panels.Last().Bottom + 2;
            }
            else
            {
                for (int i = 0; i < panelsCount; i++)
                {
                    CreatePanel(i + 1);
                }
            }

            if (__Panels.Count > 0)
                Height = __Panels.Last().Bottom + 2;
            else
                Height = cbCasesCount.Bottom + 2;

            Invalidate();
            Update();
        }

        private void CreatePanel(int chan_idx)
        {
            var panel = new HatToButtonMapperDesignPanelValuePanel();
            panel.Link(chan_idx);
            panel.BackColor = Color.Transparent;

            int top = __Panels.Count == 0
                          ? cbCasesCount.Bottom + 2
                          : __Panels.Last().Bottom + 2;

            panel.Top = top;
            panel.Left = 5;
            panel.Width = Width-6;
            panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            RegiseterLink(panel.GetLinks());
            __Panels.Add(panel);
            Controls.Add(panel);
        }


        public override XElement Serialize()
        {
            return new XElement("HatToButtonMapper",
                                   new XAttribute("Top", Top),
                                   new XAttribute("Left", Left),
                                   new XAttribute("ID", ID),
                                   new XAttribute("DataIndex", _DataIndex),
                                   SerializeLinks(),
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
            foreach (var panel in __Panels)
                panel.TakeData(data);
        }

        public override void LinkIndex(ref int Counter)
        {
            foreach (var panel in __Panels)
                panel.LinkIndex(ref Counter);
        }

        public override void Check(CodeGeneratorContext context)
        {
            if (lnkIn.LinkedTo == null)
                throw new CompilationCheckException("Input link for value mapper is not defined", CompilationCheckException.CompileIteration.PreCheck);

            foreach (var panel in __Panels) panel.Check();

            var cases = new HashSet<int>();

            foreach (var val in __Panels.Select(panel => panel.Case))
            {
                if(cases.Contains(val))
                    throw new CompilationCheckException("Duplicate case value for value mapper", CompilationCheckException.CompileIteration.PreCheck);

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

            foreach (var panel in __Panels)
            {
                panel.MapVariables(context);
            }

        }

        public override void GenerateSTMCode(CodeGeneratorContext context, STMProgram prog)
        {
            var args = new List<IConvertible>
            {
                lnkIn.LinkedTo.DataMapIdx, 
                __Panels.Count
            };

            foreach (var panel in __Panels)
            {
                args.Add(panel.Case);
                args.Add(panel.DataIndex);
            }

            context.ModelCode.Add(0x1D, args.ToArray());
        }
#endif
    }
}
