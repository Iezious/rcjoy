using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels.Buttons
{
    public partial class MultiButtonHoldDesignPanel : DraggableElement
    {
        private int _DataIndex = -1;
        private readonly List<MultiButtonPanel> __Panels = new List<MultiButtonPanel>(12);

        public MultiButtonHoldDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkOut);
        }

        private void cbCasesCount_SelectedIndexChanged(object sender, EventArgs e)
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
                    CreatePanel(i);
                }

                if (__Panels.Count > 0)
                    Height = __Panels.Last().Bottom + 2;
            }
            else
            {
                for (int i = 0; i < panelsCount; i++)
                {
                    CreatePanel(i);
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
            var panel = new MultiButtonPanel();
            panel.Index = (ushort)chan_idx;
            panel.BackColor = Color.Transparent;

            int top = __Panels.Count == 0
                          ? cbCasesCount.Bottom + 2
                          : __Panels.Last().Bottom + 2;

            panel.Top = top;
            panel.Left = 3;
            panel.Width = Width - 4;
            panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            RegiseterLink(panel.GetLinks());
            __Panels.Add(panel);
            Controls.Add(panel);
        }

        public override XElement CreatXMLSave()
        {
            return new XElement("MultiButtonHoldPanel");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);
            data.Add(
                new XAttribute("Count", __Panels.Count),
                new XAttribute("DataIndex", _DataIndex),
                new XAttribute("Default", tbDefault.Text),
                SerializePanels());
        }

        private XElement SerializePanels()
        {
            return new XElement("Values", __Panels.Select(pnl => pnl.Serialize()).Cast<object>().ToArray());
        }

        public override void Check(CodeGeneratorContext context)
        {
            if (!ValidateControl(tbDefault,  () => ValidateInt(tbDefault.Text)))
                throw new CompilationCheckException("Wrong default value", CompilationCheckException.CompileIteration.PreCheck);

            foreach (var panel in __Panels)
                panel.Check();
        }

        public override void Deserialize(XElement data)
        {
            int cnt = int.Parse(data.AttributeValue("Count") ?? "0");
            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
            tbDefault.Text = data.AttributeValue("Default");

            cbCasesCount.SelectedIndex = cnt;

            SetPanelsCount(cnt);
            DeserializePanels(data.Element("Values"));
            base.Deserialize(data);
        }

        private void DeserializePanels(XElement data)
        {
            if (data == null) return;

            foreach (var xchan in data.Elements())
            {
                int num = int.Parse(xchan.AttributeValue("Number") ?? "0") - 1;
                if (num < 0 || num > __Panels.Count) continue;

                __Panels[num].Deserialize(xchan);
            }
        }
        
        public override void LinkIndex(ref int Counter)
        {
            _DataIndex = Counter;
            Counter += 2;
        }

        public override void MapVariables(CodeGeneratorContext context)
        {
            lnkOut.DataMapIdx = (short)_DataIndex;
        }

        public override void TakeData(int[] data)
        {
            if (_DataIndex < 0) return;

            lblValue.Text = data[_DataIndex].ToString(CultureInfo.InvariantCulture);
        }

        public override void GenerateSTMCode(CodeGeneratorContext context, STMProgram prog)
        {
            // init to default
            prog.StartupCode.Commands.Add(new STMCommand(0x0D, _DataIndex, tbDefault.Text));

            var args = new List<IConvertible>()
                {
                    __Panels.Count, 
                    tbDefault.Text,
                    _DataIndex
                };

            foreach (var panel in __Panels)
            {
                args.Add(panel.ButtonLink.LinkedTo.DataMapIdx);
                args.Add(panel.Value);
            }

            context.ModelCode.Commands.Add(new STMCommand(0x35, args.ToArray()));


            base.GenerateSTMCode(context, prog);
        }
    }
}

