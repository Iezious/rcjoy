using System.Globalization;
using System.IO;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels.Joystick
{
    public partial class HatPanel : BaseJoystickValuePanel
    {
        private int _DataIndex {get { return __hat.DataIndex; }}
        private HatSwitch __hat;

        public HatPanel()
        {
            InitializeComponent();
        }

        public void SetInfo(HatSwitch data, int idx)
        {
            lblName.Text = data.Name;
            this.Name = lnkOut.Name = data.ConstantName;
            __hat = data;
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

        public override void TakeJoyData(byte[] jdata)
        {
            lblValue.Text = ExtractBits(jdata, BitsStart, __hat.Length).ToString(CultureInfo.InvariantCulture);
        }

#if STM32
        public override void MapVariables(CodeGeneratorContext context)
        {
            lnkOut.DataMapIdx = (short)_DataIndex;
        }
#endif

#if MEGA2560
        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            __H.WriteLine("#define {0} {1}",lnkOut.MappedValueName,__hat.GetValueConstant());
            __H.WriteLine("#define {0}_CHANGED {1}", lnkOut.MappedValueName, __hat.GetChangedConstant());
        }
#endif
    }
}
