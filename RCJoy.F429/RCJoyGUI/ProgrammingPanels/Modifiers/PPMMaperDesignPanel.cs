using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;
using Tahorg.RCJoyGUI.ProgrammingPanels.ModelOut;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class PPMMaperDesignPanel : DraggableElement
    {
        private readonly List<PPMMaperChannelPanel> __Panels = new List<PPMMaperChannelPanel>(16);

        public PPMMaperDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkMin, lnkCenter, lnkMax);
        }


        private void tbMin_Validating(object sender, CancelEventArgs e)
        {
            ValidateControl(tbMin, () => ValidateInt(tbMin.Text));
        }

        private void tbCenter_Validating(object sender, CancelEventArgs e)
        {
            ValidateControl(tbCenter, () => ValidateInt(tbCenter.Text));
        }

        private void tbMax_Validating(object sender, CancelEventArgs e)
        {
            ValidateControl(tbMax, () => ValidateInt(tbMax.Text));
        }

        private void lnkMax_LinkedToChanged(object sender, EventArgs e)
        {
            tbMax.Visible = lnkMax.LinkedTo == null;
        }

        private void lnkCenter_LinkedToChanged(object sender, EventArgs e)
        {
            tbCenter.Visible = lnkCenter.LinkedTo == null;
        }

        private void lnkMin_LinkedToChanged(object sender, EventArgs e)
        {
            tbMin.Visible = lnkMin.LinkedTo == null;
        }

        private void lbChannels_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializePanels(int.Parse((string)cbChannels.SelectedItem));
        }

        public void InitializePanels(int panelsCount)
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
                Height = cbChannels.Bottom + 2;

            Invalidate();
            Update();
        }

        private void CreatePanel(int chan_idx)
        {
            var panel = new PPMMaperChannelPanel();
            panel.InitChan(chan_idx);
            panel.BackColor = Color.Transparent;

            int top = __Panels.Count == 0
                          ? cbChannels.Bottom + 2
                          : __Panels.Last().Bottom + 2;

            panel.Top = top;
            panel.Left = 0;
            panel.Width = Width;
            panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            RegiseterLink(panel.GetLinks());
            __Panels.Add(panel);
            Controls.Add(panel);
        }

        public override XElement CreatXMLSave()
        {
            return new XElement("PPMMapper");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);
            data.Add(
                new XAttribute("Count", __Panels.Count),
                new XAttribute("Max", tbMax.Text),
                new XAttribute("Min", tbMin.Text),
                new XAttribute("Center", tbCenter.Text),
                SerializePanels());
        }

        private XElement SerializePanels()
        {
            return new XElement("Channels", __Panels.Select(pnl => pnl.Serialize()));
        }

        public override void Deserialize(XElement data)
        {
            int cnt = int.Parse(data.AttributeValue("Count") ?? "0");

            cbChannels.SelectedIndex = cnt;

            tbMin.Text = data.AttributeValue("Min");
            tbMax.Text = data.AttributeValue("Max");
            tbCenter.Text = data.AttributeValue("Center");

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
                if(num < 0 || num > __Panels.Count) continue;

                __Panels[num].Deserialize(xchan);
            }
        }

        public override void TakeData(int[] data)
        {
            foreach (var panel in __Panels)
                panel.TakeData(data);
        }

        public override void LinkIndex(ref int Counter)
        {
            foreach (var panel in __Panels)
                panel.LinkIndex(ref Counter);
        }

//        public override bool CheckDetermined()
//        {
//            return Determined =
//                (lnkMin.LinkedTo == null || lnkMin.LinkedTo.HolderPanel.Determined) &&
//                (lnkCenter.LinkedTo == null || lnkCenter.LinkedTo.HolderPanel.Determined) &&
//                (lnkMax.LinkedTo == null || lnkMax.LinkedTo.HolderPanel.Determined) &&
//                __Panels.All(pnl => pnl.LinkIn.LinkedTo != null && pnl.LinkIn.LinkedTo.HolderPanel.Determined);
//        }

        public override void Check(CodeGeneratorContext context)
        {
            if (lnkMin.LinkedTo == null && !ValidateControl(tbMin, () => ValidateInt(tbMin.Text)))
                throw new CompilationCheckException("Axis min value is invalid",
                    CompilationCheckException.CompileIteration.PreCheck);

            if (lnkCenter.LinkedTo == null && !ValidateControl(tbCenter, () => ValidateInt(tbCenter.Text)))
                throw new CompilationCheckException("Axis center value is invalid",
                    CompilationCheckException.CompileIteration.PreCheck);

            if (lnkMax.LinkedTo == null && !ValidateControl(tbMax, () => ValidateInt(tbMax.Text)))
                throw new CompilationCheckException("Axis max value is invalid",
                    CompilationCheckException.CompileIteration.PreCheck);

            foreach (var panel in __Panels)
                panel.Check();
        }
#if MEGA2560

        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            foreach (var panel in __Panels)
            {
                __H.WriteLine("#define {0} {1}", panel.LinkOut.MappedValueName, panel.DataIndex);
            }
        }

        public override void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            string smin = lnkMin.LinkedTo != null
                ? string.Format("get({0})", lnkMin.LinkedTo.MappedValueName)
                : tbMin.Text;

            string smid = lnkCenter.LinkedTo != null
                ? string.Format("get({0})", lnkCenter.LinkedTo.MappedValueName)
                : tbCenter.Text;

            string smax = lnkMax.LinkedTo != null
                ? string.Format("get({0})", lnkMax.LinkedTo.MappedValueName)
                : tbMax.Text;

            foreach (var panel in __Panels)
            {
                __CPP.WriteLine("map_value({0}, {1}, {2}, {3}, {4});", panel.LinkIn.LinkedTo.MappedValueName, smin, smid, smax, panel.LinkOut.MappedValueName);
            }
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
                __Panels.Count,
                (lnkMin.LinkedTo != null ? 1 : 0) |
                (lnkCenter.LinkedTo != null ? 2 : 0) |
                (lnkMax.LinkedTo != null ? 4 : 0),
                lnkMin.LinkedTo != null ? lnkMin.LinkedTo.DataMapIdx : short.Parse(tbMin.Text),
                lnkCenter.LinkedTo != null ? lnkCenter.LinkedTo.DataMapIdx : short.Parse(tbCenter.Text),
                lnkMax.LinkedTo != null ? lnkMax.LinkedTo.DataMapIdx : short.Parse(tbMax.Text)
            };

            foreach (var panel in __Panels)
            {
                args.Add(panel.LinkIn.LinkedTo.DataMapIdx);
                args.Add(panel.LinkOut.DataMapIdx);
            }

            context.ModelCode.Add(0x15, args.ToArray());
            prog.Blocks.Add(new STMBlockInfo(this, context.CurrentModel, __Panels.SelectMany(pnl => pnl.GetLinks())));
        }
#endif
    }
}
