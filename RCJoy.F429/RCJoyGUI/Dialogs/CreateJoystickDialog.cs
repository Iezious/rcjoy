using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;
using Tahorg.RCJoyGUI.JoyDialog;

namespace Tahorg.RCJoyGUI.Dialogs
{
    public partial class CreateJoystickDialog : Form
    {
        private readonly List<BaseControlPanel> __Panels = new List<BaseControlPanel>(32);
        private JoystickConfig _joystickInfo;
        private Guid __ID = Guid.Empty;
        private string __RepoprtStructure;

        public JoystickConfig JoystickInfo
        {
            get
            {
                return _joystickInfo;
            }
            set
            {
                __ID = value.ID;
                __RepoprtStructure = value.ReportStructure;
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
            tbVendorProduct.Text = string.Format("{0:X4}:{1:X4}", joystickInfo.VendorID, joystickInfo.ProductID);
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
            //tbVendorProduct.Text = wrex.Replace(tbName.Text, "").ToUpper();
        }

        private void tbName_Validating(object sender, CancelEventArgs e)
        {
            ValidateControl(tbName, () => !string.IsNullOrWhiteSpace(tbName.Text));
        }

        private void tbCName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = !ValidateControl(tbVendorProduct, () =>
            {
                string[] vs = tbVendorProduct.Text.Split(':');
                if (vs.Length != 2) return false;

                ushort v;
                ushort p;

                return UInt16.TryParse(vs[0], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out v) &
                       UInt16.TryParse(vs[1], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out p);
            });

        }

        private bool CheckJoystick()
        {
            var valid = 
                __Panels.Count > 0 &&
                __Panels.Aggregate(true, (current, panel) => current & panel.Check());

            valid = valid & 
                ValidateControl(tbName, () => !string.IsNullOrWhiteSpace(tbName.Text));

            
            UInt16 v = 0;
            UInt16 p = 0;

            valid = valid & ValidateControl(tbVendorProduct, () =>
            {
                string[] vs = tbVendorProduct.Text.Split(':');
                if (vs.Length != 2) return false;

                return UInt16.TryParse(vs[0], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out v) &
                       UInt16.TryParse(vs[1], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out p);
            });

            if (!valid) return false;

            _joystickInfo = new JoystickConfig
                {
                    Code = wrex.Replace(tbName.Text, "").ToUpper(), 
                    Name = tbName.Text,
                    ProductID = p,
                    VendorID = v,
                    ReportStructure =  __RepoprtStructure,
                    ID = __ID != Guid.Empty ? __ID : Guid.NewGuid()
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            if(!CheckJoystick())
                return;

            if(sfdExport.ShowDialog(this.ParentForm) != DialogResult.OK)
                return;

            using (var sfw = new StreamWriter(sfdExport.FileName, false, Encoding.UTF8))
            {
                var xdoc = new XDocument(_joystickInfo.Serialize());
                xdoc.Save(sfw);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if(ofdImport.ShowDialog(this.ParentForm) != DialogResult.OK)
                return;

            using (var ofr = new StreamReader(ofdImport.FileName, Encoding.UTF8))
            {
                try
                {
                    var cstr = ofr.ReadToEnd();
                    var xjoy = XDocument.Parse(cstr);
                    JoystickInfo = new JoystickConfig(xjoy.Root);

                    CheckJoystick();
                }
                catch(Exception exx)
                {
                    MessageBox.Show(this, exx.Message, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
