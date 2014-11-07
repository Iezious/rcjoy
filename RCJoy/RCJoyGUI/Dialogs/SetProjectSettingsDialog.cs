using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.Dialogs
{
    public partial class SetProjectSettingsDialog : Form
    {
        public SetProjectSettingsDialog()
        {
            InitializeComponent();
        }
#if MEGA2560

        public ProjectSettings Data
        {
            get
            {
                return new ProjectSettings
                {
                    PPMGenEnabled = cbPPMEnabled.Checked,
                    PPMTimer = (ProjectSettings.PPMGenTimer) cbTimer.SelectedIndex,
                    LCDEnabled = cbLCDEnabled.Checked,
                    LCDSerial = (ProjectSettings.Serial) cbLCDSerial.SelectedIndex,
                    XBeeEnabled = cbXbeeEnabled.Checked,
                    XBeeSerial = (ProjectSettings.Serial) cbXBeeSerial.SelectedIndex,
                    DebugEnabled = cbGenerateDebug.Checked,
                    ReportDescrReadedEnabled = cbGenerateJoqQuery.Checked,
                    DebuggerSerial = (ProjectSettings.Serial) cbDebugSerial.SelectedIndex,
                    OutputPath = tbOutPath.Text
                };
            }
            set
            {
                cbPPMEnabled.Checked = value.PPMGenEnabled;
                cbTimer.SelectedIndex = (int) value.PPMTimer;

                cbLCDEnabled.Checked = value.LCDEnabled;
                cbLCDSerial.SelectedIndex = (int) value.LCDSerial;

                cbXbeeEnabled.Checked = value.XBeeEnabled;
                cbXBeeSerial.SelectedIndex = (int) value.XBeeSerial;

                cbGenerateDebug.Checked = value.DebugEnabled;
                cbGenerateJoqQuery.Checked = value.ReportDescrReadedEnabled;
                cbDebugSerial.SelectedIndex = (int) value.DebuggerSerial;

                tbOutPath.Text = value.OutputPath;
            }
        }
#endif

        private void btnSelectDir_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbOutPath.Text))
            {
                fsdOutPath.SelectedPath = tbOutPath.Text;
            }

            if (fsdOutPath.ShowDialog() == DialogResult.OK)
            {
                tbOutPath.Text = fsdOutPath.SelectedPath;
            }
        }
    }
}
