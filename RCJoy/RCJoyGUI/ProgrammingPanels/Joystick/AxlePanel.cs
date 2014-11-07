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

namespace Tahorg.RCJoyGUI.ProgrammingPanels.Joystick
{
    public partial class AxlePanel : BaseJoystickValuePanel
    {
        private int _DataIndex { get { return __axle.DataIndex; } }

        private JoystickAxle __axle;

        public AxlePanel()
        {
            InitializeComponent();
        }

        public void SetInfo(JoystickAxle data, int idx)
        {
            lblName.Text = data.Name;
            lblValue.Text = "XXXX";
            lnkOut.Name = data.CName;

            __axle = data;

            this.Name = data.CName;
        }

        public override LinkPoint GetLink()
        {
            return lnkOut;
        }

        public override void TakeData(int[] data)
        {
            if (_DataIndex >= 0 && data.Length > _DataIndex)
                lblValue.Text = data[_DataIndex].ToString(CultureInfo.InvariantCulture);
        }

#if STM32
        public override void MapVariables(CodeGeneratorContext context)
        {
            lnkOut.DataMapIdx = (short)(_DataIndex + 1);
        }
#endif

#if MEGA2560
        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            __H.WriteLine("#define {0} {1}", lnkOut.MappedValueName, __axle.GetIdxConstant());
        }
#endif
    }
}
