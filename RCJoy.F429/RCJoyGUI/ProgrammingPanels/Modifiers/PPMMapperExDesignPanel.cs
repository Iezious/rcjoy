using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;
using Tahorg.RCJoyGUI.STM;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class PPMMapperExDesignPanel : DraggableElement
    {
        private readonly List<PPMMapperExChannerPanel> _Panels = new List<PPMMapperExChannerPanel>(12);

        public PPMMapperExDesignPanel()
        {
            InitializeComponent();
        }

        #region FRAM


        public override void Initialized()
        {
            foreach (var panel in _Panels)
                panel.Initialized();
            
        }

        public override void Removed()
        {
            foreach (var panel in _Panels)
                panel.Removed();
        }

        #endregion

        private void cbChannels_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializePanels(int.Parse((string)cbChannels.SelectedItem));
        }

        public void InitializePanels(int panelsCount)
        {
            if (_Panels.Count > 0)
            {
                while (_Panels.Count > panelsCount)
                {
                    var pnl = _Panels[_Panels.Count - 1];
                    _Panels.RemoveAt(_Panels.Count - 1);

                    pnl.UnLink();
                    UnRegiseterLink(pnl.GetLinks());
                    pnl.Parent = null;
                    pnl.Removed();
                    pnl.Dispose();
                }

                for (int i = _Panels.Count; i < panelsCount; i++)
                {
                    CreatePanel(i);
                }

                if (_Panels.Count > 0)
                    Height = _Panels.Last().Bottom + 2;
            }
            else
            {
                for (int i = 0; i < panelsCount; i++)
                {
                    CreatePanel(i);
                }
            }

            if (_Panels.Count > 0)
                Height = _Panels.Last().Bottom + 2;
            else
                Height = cbChannels.Bottom + 2;

            Invalidate();
            Update();
        }

        private void CreatePanel(int chan_idx)
        {
            var panel = new PPMMapperExChannerPanel();
            panel.Channel = (ushort) chan_idx;
            panel.BackColor = Color.Transparent;

            int top = _Panels.Count == 0
                          ? pnlHeaders.Bottom + 2
                          : _Panels.Last().Bottom + 2;

            panel.Top = top;
            panel.Left = 3;
            panel.Width = Width-4;
            panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            RegiseterLink(panel.GetLinks());
            _Panels.Add(panel);
            Controls.Add(panel);
        }


        public override XElement CreatXMLSave()
        {
            return new XElement("PPMMapperEx");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);
            data.Add(
                new XAttribute("Count", _Panels.Count),
                SerializePanels());
        }

        private XElement SerializePanels()
        {
            return new XElement("Channels", _Panels.Select(pnl => pnl.Serialize()).Cast<object>().ToArray());
        }

        public override void Deserialize(XElement data)
        {
            int cnt = int.Parse(data.AttributeValue("Count") ?? "0");

            cbChannels.SelectedIndex = cnt;

            InitializePanels(cnt);
            DeserializePanels(data.Element("Channels"));
            base.Deserialize(data);
        }

        private void DeserializePanels(XElement data)
        {
            if (data == null) return;

            foreach (var xchan in data.Elements())
            {
                int num = int.Parse(xchan.AttributeValue("Number") ?? "0") - 1;
                if (num < 0 || num > _Panels.Count) continue;

                _Panels[num].Deserialize(xchan);
            }
        }

        public override void Check(CodeGeneratorContext context)
        {
            foreach (var panel in _Panels)
                panel.Check(context, ID);
        }

        public override void LinkIndex(ref int Counter)
        {
            foreach (var panel in _Panels)
                panel.LinkIndex(ref Counter);
        }

        public override void TakeData(int[] data)
        {
            foreach (var panel in _Panels)
                panel.TakeData(data);
        }

        public override void MapVariables(CodeGeneratorContext context)
        {
            foreach (var panel in _Panels)
                panel.MapVariables(context);
        }

        public override void GenerateSTMCode(CodeGeneratorContext context, STMProgram prog)
        {
            var vcollector = new List<STMVariable>();
            foreach (var panel in _Panels)
                panel.GenerageSMTCode(context, prog, vcollector);

            prog.Blocks.Add(new STMBlockInfo(this, context.CurrentModel, _Panels.SelectMany(pnl => pnl.GetLinks()), vcollector));
        }
    }
}
