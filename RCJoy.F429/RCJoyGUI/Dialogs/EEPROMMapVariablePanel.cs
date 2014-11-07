using System;
using System.Drawing;
using System.Globalization;
using Tahorg.RCJoyGUI.Data;
using Tahorg.RCJoyGUI.Properties;

namespace Tahorg.RCJoyGUI.Dialogs
{
    public partial class EEPROMMapVariablePanel : ValidatePanel
    {
        private ushort __Addr;
        private short __Def;
        private short? __Value;

        private bool __Selected = false;

        public EEPROMMapVariablePanel()
        {
            InitializeComponent();
        }

        public void SetData(string ModelName, string varName, ushort addr, short def)
        {
            __Addr = addr;
            __Def = def;

            lblModel.Text = ModelName;
            lblVar.Text = varName;
            tbAddr.Text = addr.ToString(CultureInfo.InvariantCulture);
            tbDefValue.Text = def.ToString(CultureInfo.InvariantCulture);

            tbValue.BackColor = Color.Bisque;
        }

        public short? Value
        {
            get
            {

                return __Value;
            }
            set
            {
                __Value = value;
                if(!InvokeRequired)
                    SetValText(value);
                else
                {
                    Invoke(new Action(() => SetValText(value)));
                }
            }
        }

        private void SetValText(short? value)
        {
            tbValue.Text = value == null ? "" : value.ToString();
            tbValue.BackColor = value == null
                ? Color.Bisque
                : value == __Def
                    ? SystemColors.Control
                    : Color.HotPink;
        }

        public ushort Addr { get { return __Addr; } }

        public short DefValue { get { return __Def; } }

        public bool Selected
        {
            get { return __Selected; }
            set
            {
                __Selected = value;
                BackColor = __Selected ? SystemColors.Highlight : SystemColors.Control;
            }
        }

        private void pnl_Click(object sender, EventArgs e)
        {
            if (Enabled)
                Selected = !Selected;
        }

        public void ParseValues()
        {
            if (!ValidateControl(tbValue, () => string.IsNullOrWhiteSpace(tbValue.Text) || ValidateInt(tbValue.Text)))
                throw new CompilationCheckException(CommonResources.WrongValue);

            if (!string.IsNullOrWhiteSpace(tbValue.Text))
                __Value = short.Parse(tbValue.Text);
            else
                __Value = null;
        }
    }
}
