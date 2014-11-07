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
    public partial class ButtonPanel : BaseJoystickValuePanel
    {
        private int _ButtonMask = 0;
#if MEGA2560
        private int _DataIndex { get { return __buttons.DataIndex; } }
#endif
#if STM32
        private int _DataIndex { get { return __buttons.DataIndex + __ButtonIndex - 1; } }
#endif
        private ButtonsCollection __buttons;
        private int __ButtonIndex = 0;

        public ButtonPanel()
        {
            InitializeComponent();
        }

        public void SetInfo(ButtonsCollection data, int i, int idx)
        {
            lblName.Text = data.Name + " " + i;
            Name = lnkOut.Name = data.ConstantName + "_" + idx + "_" + i;
            __ButtonIndex = i;
#if MEGA2560
            _ButtonMask = 1 << (i - 1);
#endif
#if STM32
            _ButtonMask = 1;
#endif
            __buttons = data;
        }

        public override LinkPoint GetLink()
        {
            return lnkOut;
        }

        public override void TakeData(int[] data)
        {
            if (_DataIndex < 0) return;

            var states = data[_DataIndex];
            var enabled = (states & _ButtonMask) != 0;

            if (enabled)
            {
                lblName.ForeColor = Color.DarkRed;
                lblName.Font = new Font(lblName.Font, FontStyle.Bold);
            }
            else
            {
                lblName.ForeColor = this.ForeColor;
                lblName.Font = new Font(lblName.Font, FontStyle.Regular);
            }
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
            __H.WriteLine("#define {0}_DOWN {1}", lnkOut.MappedValueName, __buttons.GetDownConstant(__ButtonIndex));
            __H.WriteLine("#define {0}_PRESSED {1}", lnkOut.MappedValueName, __buttons.GetPressedConstant(__ButtonIndex));
        }
#endif
    }
}
