using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tahorg.RCJoyGUI.Data;
using Tahorg.RCJoyGUI.Properties;

namespace Tahorg.RCJoyGUI.Dialogs
{
    public partial class EEPMapDialog : Form
    {
        readonly List<EEPROMMapVariablePanel> __Panels = new List<EEPROMMapVariablePanel>();

        public EEPMapDialog()
        {
            InitializeComponent();

            statusLabel.Text = PortDataComm.IsOpen ? CommonResources.Connected: CommonResources.NotConnected;
            pnlButtons.Enabled = PortDataComm.IsOpen;
        }

        public void SetData(IEnumerable<DesignPanel> models)
        {
            foreach (var model in models)
            {
                var mi = model.GetModel();
                if (mi.IsMenu) continue;

                FetchTree(mi, model);
            }
        }

        private void FetchTree(ModelInfo mi, Control cnt)
        {
            if (cnt is IFRAMUser)
            {
                CreateVarPanels(mi, cnt as IFRAMUser);
            }

            foreach (Control c in cnt.Controls)
                FetchTree(mi, c);
        }


        private void CreateVarPanels(ModelInfo mi, IFRAMUser varHolder)
        {
            var cnt = varHolder.SlotsUsed;

            var names = varHolder.Names;
            var defs = varHolder.DefaultValues;
            var addrs = varHolder.FRAMAddresses;

            for (var i = 0; i < cnt; i++)
            {
                CreateVarPanel(mi, names[i], addrs[i], defs[i]);
            }
        }

        private void CreateVarPanel(ModelInfo mi, string varName, uint addr, int def)
        {
            var panel = new EEPROMMapVariablePanel();

            panel.SetData(mi.Name, varName, (ushort)addr, (short)def);
            panel.Value = null;

            __Panels.Add(panel);

            tblVariables.Controls.Add(panel);
            panel.Dock = DockStyle.Top;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            EnumPanelForRead(panel => true);

        }

        private void btnReadSelected_Click(object sender, EventArgs e)
        {
            EnumPanelForRead(panel => panel.Selected);            
        }


        private void btnResetSelected_Click(object sender, EventArgs e)
        {
            EnumPanelsForWrite(panel => panel.Selected, panel => panel.DefValue, false);
        }

        private void btnWriteSelected_Click(object sender, EventArgs e)
        {
            EnumPanelsForWrite(panel => panel.Selected, panel => panel.Value ?? 0, true);
        }

        private void btnWriteAll_Click(object sender, EventArgs e)
        {
            EnumPanelsForWrite(panel => true, panel => panel.Value ?? 0, true);
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            EnumPanelsForWrite(panel => true, panel => panel.DefValue, false);
        }

        private void EnumPanelForRead(Func<EEPROMMapVariablePanel, bool> selector)
        {
            pnlButtons.Enabled = false;
            pnlVariables.Enabled = false;

            sbProgress.Minimum = 0;
            sbProgress.Maximum = __Panels.Count(selector);
            sbProgress.Value = 0;


            (new Task(() => ReadVariables(__Panels.Where(selector),
                    () =>
                    {
                        pnlButtons.Enabled = PortDataComm.IsOpen;
                        pnlVariables.Enabled = true;
                    }))).Start();
        }

        private void EnumPanelsForWrite(Func<EEPROMMapVariablePanel, bool> selector, Func<EEPROMMapVariablePanel, short> conv, bool precheck)
        {
            if (precheck)
            {
                foreach (var panel in __Panels.Where(selector))
                {
                    try
                    {
                        panel.ParseValues();
                    }
                    catch (CompilationCheckException exx)
                    {
                        MessageBox.Show(this, exx.Message, CommonResources.Check_error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            pnlButtons.Enabled = false;
            pnlVariables.Enabled = false;

            sbProgress.Minimum = 0;
            sbProgress.Maximum = __Panels.Count(selector);
            sbProgress.Value = 0;

            (new Task(() => WriteVariables(__Panels.Where(selector),
                conv,
                () =>
                {
                    pnlButtons.Enabled = PortDataComm.IsOpen;
                    pnlVariables.Enabled = true;
                }))).Start();
        }


        private void ReadVariables(IEnumerable<EEPROMMapVariablePanel> panels, Action onComplete)
        {
            bool errorSet = false;
            string errorMessage = string.Empty;

            foreach (var currentReadPanel in panels)
            {
                try
                {
                    var b1 = (byte)((currentReadPanel.Addr >> 8) & 0xFF);
                    var b2 = (byte)(currentReadPanel.Addr & 0xFF);

                    byte[] data;
                    if (PortDataComm.SendCommand(0x6,out data, b1, b2) != PortDataComm.CommandStatus.OK || data == null)
                    {
                        errorMessage = CommonResources.ComunicationError;
                        errorSet = true;
                        break;
                    }

                    if (data.Length != 4)
                    {
                        errorMessage = CommonResources.WrongPacketStructure; 
                        errorSet = true;
                        break;
                    }

                    var w1 = (ushort)((data[0] << 8) | data[1]);
                    if (w1 != 0)
                    {
                        errorSet = true;
                        errorMessage = Resources.EEPMapDialog_ReadFault;
                        break;
                    }

                    var w2 = (short)((data[2] << 8) | data[3]);
                    currentReadPanel.Value = w2;

                    Invoke(new Action(() =>
                    {
                        sbProgress.Value++;
                    }));
                }
                catch (Exception exx)
                {
                    errorSet = true;
                    errorMessage = exx.Message;
                    break;
                }
                
            }

            Invoke(onComplete);

            if (errorSet)
            {
                Invoke(new Action(() => MessageBox.Show(this, errorMessage, CommonResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error)));
            }
        }

        private void WriteVariables(IEnumerable<EEPROMMapVariablePanel> panels, Func<EEPROMMapVariablePanel, short> conv,
            Action onComplete)
        {
            bool errorSet = false;
            string errorMessage = String.Empty;

            byte[] data;

            foreach (var currentReadPanel in panels)
            {

                try
                {
                    var b1 = (byte) ((currentReadPanel.Addr >> 8) & 0xFF);
                    var b2 = (byte) (currentReadPanel.Addr & 0xFF);

                    var val = conv(currentReadPanel);

                    var b3 = (byte) ((val >> 8) & 0xFF);
                    var b4 = (byte) (val & 0xFF);

                    
                    if (PortDataComm.SendCommand(0x7, out data, b1, b2, b3, b4) != PortDataComm.CommandStatus.OK || data == null)
                    {
                        errorMessage = CommonResources.ComunicationError;
                        errorSet = true;
                        break;
                    }

                    if (data.Length != 2)
                    {
                        errorSet = true;
                        errorMessage = CommonResources.WrongPacketStructure;
                        break;
                    }

                    var w2 = data[1];

                    if (w2 != 0)
                    {
                        errorSet = true;
                        errorMessage = Resources.EEPMapDialog_WriteFault;
                        break;
                    }

                    Invoke(new Action(() =>
                    {
                        sbProgress.Value++;
                    }));

                }
                catch (Exception exx)
                {
                    errorSet = true;
                    errorMessage = exx.Message;
                    break;
                }
            }

            if (!errorSet)
            {
                if (PortDataComm.SendCommand(0x9, out data) != PortDataComm.CommandStatus.OK)
                {
                    errorMessage = Resources.EEPMapDialog_WriteFault;
                    errorSet = true;
                }
            }

            Invoke(onComplete);

            if (errorSet)
            {
                Invoke(
                    new Action(
                        () => MessageBox.Show(this, errorMessage, CommonResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error)));
            }
        }
    }
}
