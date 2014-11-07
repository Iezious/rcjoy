using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.Dialogs
{
    public partial class EEPMapDialog : Form
    {
        private const int TIMEOUT = 2500;
        readonly List<EEPROMMapVariablePanel> __Panels = new List<EEPROMMapVariablePanel>();

        public EEPMapDialog()
        {
            InitializeComponent();

            statusLabel.Text = fmMain.__Port.IsOpen ? "Connected" : "Not connected";
            pnlButtons.Enabled = fmMain.__Port.IsOpen;
        }

        protected override void OnClosed(EventArgs e)
        {

            base.OnClosed(e);
        }

        public void SetData(IEnumerable<DesignPanel> models)
        {
            foreach (var model in models)
            {
                var mi = model.GetModel();
                if (mi.IsMenu) continue;

                FetchTree(mi, model);
                //                CreateVarPanels(mi, model);
                //
                //                foreach (var panel in model.Elements.OfType<IFRAMUser>())
                //                    CreateVarPanels(mi, panel);
            }

//            if (__Panels.Count > 0)
//                tblVariables.Height = __Panels.Count*26 + 24;
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
                        pnlButtons.Enabled = fmMain.__Port.IsOpen;
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
                        MessageBox.Show(this, exx.Message, "Check error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    pnlButtons.Enabled = fmMain.__Port.IsOpen;
                    pnlVariables.Enabled = true;
                }))).Start();
        }


        private void ReadVariables(IEnumerable<EEPROMMapVariablePanel> panels, Action onComplete)
        {
            EEPROMMapVariablePanel currentReadPanel = null;
            bool errorSet = false;
            string errorMessage = "";

            var waiter = new AutoResetEvent(false);

            var PortDataCB = new PortCommunicator.OnDataRecievedEvent((command, data) =>
            {
                if (command != 6) return;

                if (data.Length != 4)
                {
                    errorSet = true;
                    errorMessage = "Wrong packet structure";
                    waiter.Set();
                    return;
                }

                var w1 = (ushort)((data[0] << 8) | data[1]);
                if (w1 != 0)
                {
                    errorSet = true;
                    errorMessage = "EEPRom read failure";
                    waiter.Set();
                    return;
                }

                var w2 = (short)((data[2] << 8) | data[3]);

                currentReadPanel.Value = w2;
                waiter.Set();
            });

            fmMain.__Port.OnDataRecieved += PortDataCB;

            try
            {
                var readQueue = new Queue<Tuple<EEPROMMapVariablePanel, int>>(
                    panels.Select(pnl => new Tuple<EEPROMMapVariablePanel, int>(pnl, 0)));

                while (readQueue.Count > 0)
                {
                    var tpl = readQueue.Dequeue();
                    currentReadPanel = tpl.Item1;

                    var b1 = (byte)((currentReadPanel.Addr >> 8) & 0xFF);
                    var b2 = (byte)(currentReadPanel.Addr & 0xFF);

                    fmMain.__Port.SendCommand(0x6, b1, b2);

                    if (waiter.WaitOne(TIMEOUT) && !errorSet)
                    {
                        Invoke(new Action(() =>
                        {
                            sbProgress.Value++;
                        }));

                        continue;
                    }

                    if (errorSet) break;

                    if (tpl.Item2 < 5)
                    {
                        readQueue.Enqueue(new Tuple<EEPROMMapVariablePanel, int>(currentReadPanel, tpl.Item2+1));
                        continue;
                    }

                    errorSet = true;
                    errorMessage = "Request timeout";
                    break;
                }
            }
            finally
            {
                fmMain.__Port.OnDataRecieved -= PortDataCB;
//                waiter.Dispose();
                Invoke(onComplete);
            }

            if (errorSet)
            {
                Invoke(new Action(() => MessageBox.Show(this, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)));
            }
        }

        private void WriteVariables(IEnumerable<EEPROMMapVariablePanel> panels, Func<EEPROMMapVariablePanel, short> conv, Action onComplete)
        {
            bool errorSet = false;
            string errorMessage = "";

            var waiter = new AutoResetEvent(false);

            var PortDataCB = new PortCommunicator.OnDataRecievedEvent((command, data) =>
            {
                if (command != 7) return;

                if (data.Length != 2)
                {
                    errorSet = true;
                    errorMessage = "Wrong packet structure";
                    waiter.Set();
                    return;
                }

                //                var w1 = (ushort)((data[0] << 8) | data[1]);
                var w2 = data[1];

                if (w2 != 0)
                {
                    errorSet = true;
                    errorMessage = "EEPRom write failure";
                }

                waiter.Set();
            });

            fmMain.__Port.OnDataRecieved += PortDataCB;

            try
            {
                var readQueue = new Queue<Tuple<EEPROMMapVariablePanel, int>>(
                    panels.Select(pnl => new Tuple<EEPROMMapVariablePanel, int>(pnl, 0)));

                while (readQueue.Count > 0)
                {
                    var tpl = readQueue.Dequeue();
                    EEPROMMapVariablePanel currentReadPanel = tpl.Item1;

                    var b1 = (byte)((currentReadPanel.Addr >> 8) & 0xFF);
                    var b2 = (byte)(currentReadPanel.Addr & 0xFF);

                    var val = conv(currentReadPanel);

                    var b3 = (byte)((val >> 8) & 0xFF);
                    var b4 = (byte)(val & 0xFF);

                    fmMain.__Port.SendCommand(0x7, b1, b2, b3, b4);

                    if (waiter.WaitOne(TIMEOUT) && !errorSet)
                    {
                        Invoke(new Action(() =>
                        {
                            sbProgress.Value++;
                        }));

                        continue;
                    }

                    if (errorSet) break;

                    if (tpl.Item2 < 3)
                    {
                        readQueue.Enqueue(new Tuple<EEPROMMapVariablePanel, int>(currentReadPanel, tpl.Item2 + 1));
                        continue;
                    }

                    errorSet = true;
                    errorMessage = "Request timeout";
                    break;
                }
            }
            finally
            {
                fmMain.__Port.OnDataRecieved -= PortDataCB;
//                waiter.Dispose();
                Invoke(onComplete);

            }

            if (errorSet)
            {
                Invoke(new Action(() => MessageBox.Show(this, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)));
            }
        }

 


    }
}
