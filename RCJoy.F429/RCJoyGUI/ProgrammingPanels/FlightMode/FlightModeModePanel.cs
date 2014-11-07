using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels.FlightMode
{
    public partial class FlightModeModePanel : UserControl
    {
        private int _ModeNumber;

        private List<Tuple<LinkPoint, Label>> _Links = new List<Tuple<LinkPoint, Label>>(12);

        public FlightModeModePanel()
        {
            InitializeComponent();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            var p = new Pen(ForeColor);

            e.Graphics.DrawLines(p, 
                new[]
                {
                    new Point(3,1),     
                    new Point(Width-1,1),
                    new Point(Width-1,Height-1),
                    new Point(3,Height-1)
                });

            base.OnPaint(e);
        }

        public void SetData(int modeNo, int chanCount)
        {
            _ModeNumber = modeNo;

            Name = "ModelData_" + _ModeNumber;
            SetChannelsCount(chanCount);
        }

        public void SetChannelsCount(int chanCount)
        {
            while (_Links.Count > chanCount)
            {
                var pair = _Links[_Links.Count - 1];
                _Links.RemoveAt(_Links.Count - 1);

                var tp = pair.Item1;
                var lbl = pair.Item2;

                tp.Unlink();

                Controls.Remove(tp);
                Controls.Remove(lbl);
            }

            while (_Links.Count < chanCount)
            {
                var i = _Links.Count;

                var tp = new LinkPoint()
                {
                    Direction = enLinkDirection.Input,
                    LinkType = enLinkType.Axle,
                    Left = -3,
                    Top = 19*i + 32,
                    Name = "LNK_IN_" + _ModeNumber + "_" + i
                };

                var lbl = new Label()
                {
                    Text = "Channel " + (i + 1),
                    Left = tbName.Left,
                    Top = 31 + 19*i
                };

                Controls.Add(tp);
                Controls.Add(lbl);

                _Links.Add(new Tuple<LinkPoint, Label>(tp, lbl));
            }

            Height = 35 + _Links.Count*19;
        }

        public LinkPoint[] GetLinks()
        {
            return _Links.Select(pair => pair.Item1).ToArray();
        }

        public void UnLink()
        {
            foreach (var pair in _Links)
                pair.Item1.Unlink();
        }

        public void Check(CodeGeneratorContext context)
        {
            
        }

        public void MapVariables(CodeGeneratorContext context)
        {
            
        }
    }
    
}
