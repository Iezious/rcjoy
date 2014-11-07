using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class ValueSwitcherDesignPanel : DraggableElement
    {
        public int _DataIndex = -1;

        private readonly List<ValueSwitcherDesignPanelValuePanel> __Panels = new List<ValueSwitcherDesignPanelValuePanel>(16);


        public ValueSwitcherDesignPanel()
        {
            InitializeComponent();
            RegiseterLink(lnkIn, lnkOut);
            cbValuesCunt.SelectedIndex = 0;
        }

        private void tbCName_Validating(object sender, CancelEventArgs e)
        {
            ValidateControl(tbCName, () => ValidateCName(tbCName.Text));
        }

        private void cbValuesCunt_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPanelsCount(int.Parse((string)cbValuesCunt.SelectedItem));

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
                var pnl = new ValueSwitcherDesignPanelValuePanel();
                Controls.Add(pnl);

                pnl.BackColor = Color.Transparent;
                pnl.Left = 8;
                pnl.Width = Width - 16;

                var pred = __Panels.LastOrDefault();
                pnl.Top = pred == null ? cbValuesCunt.Bottom + 2 : pred.Bottom + 1;

                __Panels.Add(pnl);
            }

            if (__Panels.Count == 0)
                Height = cbValuesCunt.Bottom + 4;
            else
                Height = __Panels.Last().Bottom + 2;

            Invalidate();
            Update();
        }


        public override XElement Serialize()
        {
            return new XElement("ValueSwitcher",
                       new XAttribute("Top", Top),
                       new XAttribute("Left", Left),
                       new XAttribute("ID", ID),
                       new XAttribute("DataIndex", _DataIndex),
                       new XAttribute("CName", tbCName.Text),
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

            tbCName.Text = data.AttributeValue("CName");
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

            var xelems = xpanels.Elements("Value").ToArray();
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


            cbValuesCunt.SelectedItem = "" + xelems.Length;
        }

        public override void TakeData(int[] data)
        {
            if (_DataIndex < 0) return;

            lblValue.Text = data[_DataIndex].ToString(CultureInfo.InvariantCulture);
        }

        public override void LinkIndex(ref int Counter)
        {
            _DataIndex = Counter;
            Counter+=3;
        }

        public override void Check(CodeGeneratorContext context)
        {
            if (lnkIn.LinkedTo == null)
                throw new CompilationCheckException("No click defined for value switcher", CompilationCheckException.CompileIteration.PreCheck);

            if(__Panels.Count == 0)
                throw new CompilationCheckException("Value switcher has no values", CompilationCheckException.CompileIteration.PreCheck);

            if(__Panels.Count == 1)
                throw new CompilationCheckException("Value switcher has only one value", CompilationCheckException.CompileIteration.PreCheck);

            foreach (var panel in __Panels) panel.Check();
        }

#if MEGA2560

        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            __H.WriteLine("#define {0} {1}", lnkOut.MappedValueName, _DataIndex);
            __H.WriteLine("#define {0}_CHANGED {1}", lnkOut.MappedValueName, _DataIndex + 1);
            __H.WriteLine("#define {0}_STATE {1}", MappedID, _DataIndex+2);
        }

        public override void GenerateInit(CodeGeneratorContext context, TextWriter __CPP)
        {
            var panel = __Panels[0];
            __CPP.WriteLine();
            __CPP.WriteLine("    set({0}_CHANGED, 1);", lnkOut.MappedValueName);
            __CPP.WriteLine("    set({0}_STATE, 0);", MappedID);
            __CPP.WriteLine("    set({0}, {1});", lnkOut.MappedValueName, panel.Value);
        }

        public override void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            __CPP.WriteLine("if({0}_PRESSED)", lnkIn.LinkedTo.MappedValueName);
            __CPP.WriteLine("{");
            __CPP.WriteLine("  p0 = get({0}_STATE) + 1;",MappedID);
            __CPP.WriteLine("  if(p0 >= {0}) p0=0;", __Panels.Count);

            for (var i = 0; i < __Panels.Count; i++)
            {
                var panel = __Panels[i];

                __CPP.WriteLine("  if(p0 == {0})", i);
                __CPP.WriteLine("  {");
                __CPP.WriteLine("    set({0}_CHANGED, 1);", lnkOut.MappedValueName);
                __CPP.WriteLine("    set({0}_STATE, {1});", MappedID, i);
                __CPP.WriteLine("    set({0}, {1});", lnkOut.MappedValueName, panel.Value);
                if (!string.IsNullOrWhiteSpace(tbCName.Text) && !string.IsNullOrWhiteSpace(panel.LCDText))
                {
                    __CPP.WriteLine("#ifdef LCD");
                    __CPP.WriteLine("    lcdPrintLine2(\"{0}\",\"{1}\");", tbCName.Text, panel.LCDText);
                    __CPP.WriteLine("#endif  ");

                }
                __CPP.WriteLine("  }");
            }

            __CPP.WriteLine("}");
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
            prog.StartupCode.Commands.Add(new STMCommand( 
                    0x0B, __Panels[0].Value, _DataIndex
                ));

            var args = new List<IConvertible>()
                {
                    lnkIn.LinkedTo.DataMapIdx, 
                    __Panels.Count, 
                    _DataIndex,
                    prog.StringConstants.Add(tbCName.Text.Trim())
                };

            foreach (var panel in __Panels)
            {
                args.Add(panel.Value);
                args.Add(prog.StringConstants.Add(panel.LCDText.Trim()));
            }

            context.ModelCode.Commands.Add(new STMCommand(0x0C, args.ToArray()));
        }
#endif
    }
}
