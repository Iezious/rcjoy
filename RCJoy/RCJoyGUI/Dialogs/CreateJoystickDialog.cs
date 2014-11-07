using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Tahorg.RCJoyGUI.Data;
using Tahorg.RCJoyGUI.JoyDialog;

namespace Tahorg.RCJoyGUI.Dialogs
{
    public partial class CreateJoystickDialog : Form
    {
        private readonly List<BaseControlPanel> __Panels = new List<BaseControlPanel>(32);
        private JoystickConfig _joystickInfo;

        public JoystickConfig JoystickInfo
        {
            get
            {
                return _joystickInfo;
            }
            set
            {
                FillPanels(value);
            }
        }

        public CreateJoystickDialog()
        {
            InitializeComponent();
            _joystickInfo = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addContextMenu.Show(btnAdd, 0, btnAdd.Height);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(!CheckJoystick()) return;
            DialogResult = DialogResult.OK;
        }

        void AddPanel(BaseControlPanel panel)
        {
            panel.Dock = DockStyle.Top;

            __Panels.Add(panel);

            pnlScroll.Controls.Add(panel);
            pnlScroll.Controls.SetChildIndex(panel, 0);

            panel.UpClicked += MovePanelUp;
            panel.DownClicked += MovePanelDown;
            panel.RemoveClicked += DeletePanel;

            //panel.Top = prev_panel == null ? 0 : prev_panel.Top + prev_panel.Height+10;
        }

        private void DeletePanel(object sender, EventArgs e)
        {
            __Panels.Remove((BaseControlPanel)sender);
            pnlScroll.Controls.Remove((Control)sender);
        }

        private void MovePanelDown(object sender, EventArgs e)
        {
            var panel = (BaseControlPanel)sender;

            var idx = __Panels.IndexOf(panel);
            if (idx == __Panels.Count - 1) return;

            var pnl2 = __Panels[idx + 1];
            __Panels[idx + 1] = panel;
            __Panels[idx] = pnl2;

            for (var i = 0; i < __Panels.Count; i++)
            {
                pnlScroll.Controls.SetChildIndex(__Panels[i], __Panels.Count - i - 1);
            }
        }

        private void MovePanelUp(object sender, EventArgs e)
        {
            var panel = (BaseControlPanel)sender;

            var idx = __Panels.IndexOf(panel);
            if (idx == 0) return;

            var pnl2 = __Panels[idx - 1];
            __Panels[idx - 1] = panel;
            __Panels[idx] = pnl2;

            for (var i = 0; i < __Panels.Count; i++)
            {
                pnlScroll.Controls.SetChildIndex(__Panels[i], __Panels.Count - i - 1);
            }
        }

        private void axisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPanel(new AxlePanel());
        }

        private void buttonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPanel(new ButtonsPanel());
        }

        private void hatswitchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPanel(new HatPanel());
        }

        private void dummyBitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPanel(new DummyPanel());
        }

        private void FillPanels(JoystickConfig joystickInfo)
        {
            foreach (var control in joystickInfo.Controls)
            {
                BaseControlPanel panel;

                if (control is JoystickAxle)
                {
                    panel = new AxlePanel();
                    panel.FillFromJoyInfo(control);
                }
                else if (control is ButtonsCollection)
                {
                    panel = new ButtonsPanel();
                    panel.FillFromJoyInfo(control);
                }
                else if (control is HatSwitch)
                {
                    panel = new HatPanel();
                    panel.FillFromJoyInfo(control);
                }
                else if (control is LevelingBits)
                {
                    panel = new DummyPanel();
                    panel.FillFromJoyInfo(control);
                }
                else
                {
                    continue;
                }

                AddPanel(panel);
            }

            tbName.Text = joystickInfo.Name;
            tbCName.Text = joystickInfo.Code;
        }

        protected static readonly Regex wrex = new Regex(@"\W+");
        protected static readonly Regex checkRex = new Regex(@"^\w+$");

        protected bool ValidateControl(Control c, Func<bool> validator)
        {
            if (!validator())
            {
                c.BackColor = Color.Red;
                return false;
            }

            c.BackColor = SystemColors.Window;
            return true;
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            tbCName.Text = wrex.Replace(tbName.Text, "").ToUpper();
        }

        private void tbName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbName, () => !string.IsNullOrWhiteSpace(tbName.Text));
        }

        private void tbCName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbCName, () => checkRex.IsMatch(tbCName.Text));

        }

        private bool CheckJoystick()
        {
            var valid = 
                __Panels.Count > 0 &&
                __Panels.Aggregate(true, (current, panel) => current & panel.Check());

            valid = valid & 
                ValidateControl(tbName, () => !string.IsNullOrWhiteSpace(tbName.Text)) &
                ValidateControl(tbCName, () => checkRex.IsMatch(tbCName.Text));

            if (!valid) return false;

            _joystickInfo = new JoystickConfig
                {
                    Code = tbCName.Text, 
                    Name = tbName.Text
                };

            foreach (var panel in __Panels) panel.FillJoyInfo(_joystickInfo);

            if (_joystickInfo.Bits()%8 != 0)
            {
                MessageBox.Show(this, "Wrong total bits number, must be multiple of 8", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                return false;
            }

            return true;
        }
    }
}
