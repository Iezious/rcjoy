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
using Tahorg.RCJoyGUI.ProgrammingPanels.FlightMode;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class FlightModeDesignPanel : DraggableElement
    {
        readonly List<FlightModeOutChannel> _outChannels = new List<FlightModeOutChannel>(12);
        readonly List<FlightModeModePanel>  _modes = new List<FlightModeModePanel>(12);

        public FlightModeDesignPanel()
        {
            InitializeComponent();
        }


        public override XElement CreatXMLSave()
        {
            return new XElement("");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);
        }


        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);
        }



        public override void MapVariables(CodeGeneratorContext context)
        {
            foreach (var channel in _outChannels)
                channel.MapVariables(context);

            foreach (var mode in _modes)
                mode.MapVariables(context);
        }

        public override void Check(CodeGeneratorContext context)
        {
            foreach (var mode in _modes)
                mode.Check(context);
        }

        public override void LinkIndex(ref int Counter)
        {
            foreach (var channel in _outChannels)
                channel.LinkIndex(ref Counter);
        }

        public override void TakeData(int[] data)
        {
            foreach (var channel in _outChannels)
                channel.TakeData(data);
        }


        public override IEnumerable<LinkPoint> GetLinks()
        {
            foreach (var channel in _outChannels)
            {
                yield return channel.GetLink();
            }

            foreach (var lnk in _modes.SelectMany(mode => mode.GetLinks()))
            {
                yield return lnk;
            }
        }
    }
}
