using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;
using Tahorg.RCJoyGUI.ProgrammingPanels.ModelOut;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class PPMOutDesignPanel : DraggableElement
    {
        private ModelInfo __CurrentConfig;
        private readonly List<ModelChannelPanel> __Panels = new List<ModelChannelPanel>(32);

        public PPMOutDesignPanel()
        {
            InitializeComponent();
        }

        public void InitializePanels(ModelInfo data)
        {
            if (__Panels.Count > 0)
            {
                while (__Panels.Count > data.Channels)
                {
                    var pnl = __Panels[__Panels.Count - 1];
                    __Panels.RemoveAt(__Panels.Count-1);

                    pnl.GetLink().Unlink();
                    UnRegiseterLink(pnl.GetLink());
                    pnl.Parent = null;
                    pnl.Dispose();
                }

                __CurrentConfig = data;

                for (int i = __Panels.Count; i < data.Channels; i++)
                {
                    CreatePanel(i + 1);
                }

                if (__Panels.Count > 0)
                    Height = __Panels.Last().Bottom + 2;
            }
            else
            {
                __CurrentConfig = data;

                for (int i = 0; i < data.Channels; i++)
                {
                    CreatePanel(i + 1);
                }
            }

            Invalidate();
            Update();
        }

        private void CreatePanel(int chan_idx)
        {
            var panel = new ModelChannelPanel();
            panel.SetInfo(chan_idx);

            int top = __Panels.Count == 0
                          ? labelHead.Bottom + 2
                          : __Panels.Last().Bottom + 2;

            panel.Top = top;
            panel.Left = 2;
            panel.Width = Width - 8;
            panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            RegiseterLink(panel.GetLink());
            __Panels.Add(panel);
            Controls.Add(panel);
        }

        public override void LinkIndex(ref int Counter)
        {

        }

        public override void TakeDataPPM(int[] intdata)
        {
            for (int i = 0; i < __Panels.Count; i++)
            {
                __Panels[i].TakeData(i < intdata.Length ? intdata[i] : -1);
            }
        }

        public override XElement Serialize()
        {
            var res = new XElement("PPMOut", new XAttribute("Top", Top), new XAttribute("Left", Left), new XAttribute("ID", ID));
            var xpanels = new XElement("Outs");

            foreach (var panel in __Panels)
            {
                xpanels.Add(panel.Serialize());
            }

            res.Add(xpanels);
            return res;
        }

        public override void Deserialize(XElement data)
        {
            Top = int.Parse(data.AttributeValue("Top") ?? "120");
            Left = int.Parse(data.AttributeValue("Left") ?? "120");
            ID = Guid.Parse(data.AttributeValue("ID") ?? Guid.NewGuid().ToString());

            var xlinks = data.Element("Outs");
            if (xlinks == null) return;

            foreach (var att in xlinks.Elements())
            {
                var num = int.Parse(att.AttributeValue("Chan") ?? "-1");

                var panel = __Panels.FirstOrDefault(pnl => pnl.Chan == num);
                if (panel != null) panel.Deserialize(att);
            }
        }

        public override void Check(CodeGeneratorContext context)
        {
            if(__Panels.All(pnl => pnl.LinkIn.LinkedTo == null))
                throw new CompilationCheckException("No one ppm chanel is mapped");
        }

        public override void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            for (int i = 0; i < __Panels.Count; i++)
            {
                var panel = __Panels[i];
                if (panel.LinkIn.LinkedTo == null) continue;

                __CPP.WriteLine("map_ppm({0},{1},{2},{3},{4});",
                    panel.LinkIn.LinkedTo.MappedValueName,
                    context.CurrentModel.PPMMin,
                    context.CurrentModel.PPMCenter,
                    context.CurrentModel.PPMMax,
                    i);
            }
        }

        public override void GenerateSTMCode(CodeGeneratorContext context, STMProgram prog)
        {
            for (short i = 0; i < __Panels.Count; i++)
            {
                var panel = __Panels[i];
                if (panel.LinkIn.LinkedTo == null) continue;

                context.ModelCode.Add(
                    0x07, panel.LinkIn.LinkedTo.DataMapIdx, i
                    );
            }
                
        }
    }
}
