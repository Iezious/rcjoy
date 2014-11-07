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
    public partial class FMPPMOutDesignPanel : DraggableElement
    {
        private ModelInfo __CurrentConfig;
        private readonly List<ModelChannelPanel> __Panels = new List<ModelChannelPanel>(32);
        private int _DataIndex = -1;
        private int _ModeIndex = -1;
        private bool _ModeActive = false;

        public FMPPMOutDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkButton);
        }

        public int ModeIndex { get { return _ModeIndex; } }

        public string ModeName { get { return Title; } }

        public event EventHandler OnModeNameChanged;

        protected virtual void ModeNameChanged()
        {
            EventHandler handler = OnModeNameChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void InitializePanels(ModelInfo data, int modeIndex)
        {
            _ModeIndex = modeIndex;

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
                          ? lnkButton.Bottom + 4
                          : __Panels.Last().Bottom + 2;

            panel.Top = top;
            panel.Left = 2;
            panel.Width = Width - 8;
            panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            RegiseterLink(panel.GetLink());
            __Panels.Add(panel);
            Controls.Add(panel);
        }

        public override void TakeData(int[] data)
        {
            _ModeActive = _DataIndex != -1 &&
                          _ModeIndex != -1 &&
                          data[_DataIndex] == _ModeIndex;
        }

        public override void TakeDataPPM(int[] intdata)
        {
            if (_ModeActive)
            {
                for (var i = 0; i < __Panels.Count; i++)
                    __Panels[i].TakeData(i < intdata.Length ? intdata[i] : -1);
            }
            else
            {
                foreach (var t in __Panels) t.TakeData(-1);
            }
        }

        public override XElement CreatXMLSave()
        {
            return new XElement("FMPPMOut");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);

            data.Add(
                new XAttribute("DataIndex", _DataIndex),
                new XAttribute("ModeIdx", _ModeIndex)
            );

            var xpanels = new XElement("Outs");

            foreach (var panel in __Panels)
                xpanels.Add(panel.Serialize());

            data.Add(xpanels);
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);

            int.TryParse(data.AttributeValue("DataIndex") ?? "-1", out _DataIndex);
            int.TryParse(data.AttributeValue("ModeIdx") ?? "-1", out _ModeIndex);

            var xlinks = data.Element("Outs");
            if (xlinks == null) return;


            foreach (var att in xlinks.Elements())
            {
                var num = int.Parse(att.AttributeValue("Chan") ?? "-1");

                var panel = __Panels.FirstOrDefault(pnl => pnl.Chan == num);
                if (panel != null) panel.Deserialize(att);
            }

            /*

            var lnk = data.Elements("Link").FirstOrDefault(el => el.AttributeValue("Name") == "lnkButton");
            lnkButton.Deserialize(lnk);
             * */
        }

        public override void Check(CodeGeneratorContext context)
        {
            if(lnkButton.LinkedTo == null)
                throw new CompilationCheckException("Selection button is not linked");

            if(__Panels.All(pnl => pnl.LinkIn.LinkedTo == null))
                throw new CompilationCheckException("No one ppm chanel is mapped");
        }

        public override void LinkIndex(ref int Counter)
        {
            
        }

        public override void GenerateSTMCode(CodeGeneratorContext context, STMProgram prog)
        {

            context.ModelCode.Add(0x21,
                    lnkButton.LinkedTo.DataMapIdx,
                    ModeIndex
                );


            var stmmodel = prog.Models[context.CurrentModel.Index];
            stmmodel.Modes[ModeIndex] = new STMFlightMode { Name = Title};

            _DataIndex = prog.Variables.Variables[(int) stmmodel.ModeVariable].DATAMAP_ADDR;

            var args = new List<IConvertible>
            {
                stmmodel.ModeVariable,
                ModeIndex,
                0
            };

            int count = 0;

            for (short i = 0; i < __Panels.Count; i++)
            {
                var panel = __Panels[i];
                if (panel.LinkIn.LinkedTo == null) continue;

                count++;

                args.Add(panel.LinkIn.LinkedTo.DataMapIdx);
                args.Add(i);
            }

            args[2] = count;

            context.ModelCode.Add(0x20, args.ToArray());

            base.GenerateSTMCode(context, prog);
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            ModeNameChanged();
        }
    }
}
