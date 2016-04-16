using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;
using Tahorg.RCJoyGUI.Dialogs;
using Tahorg.RCJoyGUI.Properties;

namespace Tahorg.RCJoyGUI
{
    delegate void ThreadSafeInvokeDelegate();

    [StructLayout(LayoutKind.Explicit)]
    struct tbufferrc2
    {
        [FieldOffset(0x00)]
        public ushort Header;

        [FieldOffset(0x02)]
        public byte Command;

        [FieldOffset(0x03)]
        public byte SubCommand;

        [FieldOffset(0x04)]
        public ushort uArgument1;

        [FieldOffset(0x06)]
        public uint bArgument1;
    }

    public partial class fmMain : Form
    {

#if MEGA2560
        private SerialPort __Port;
#endif
        private Thread __Reader;
        private bool ReaderEnabled;

        private static readonly List<JoystickConfig> __JoyInfos = new List<JoystickConfig>();
        public static List<JoystickConfig> JoyInfos { get { return __JoyInfos; } }

        private static ProjectSettings __Settings;
        public static ProjectSettings Settings { get { return __Settings; } }

        public static fmMain Instance { get; private set; }

        public fmMain()
        {
            Instance = this;

            InitializeComponent();


            FillComPorts();

            __Settings = new ProjectSettings();

#if !DEBUG
            captureDebugDataToolStripMenuItem.Visible = false;
            debugToolStripMenuItem.Visible = false;
#endif

            ReadLastList();
            SetFileState();
        }


        #region port functions

        private void clickPort(object sender, EventArgs eventArgs)
        {
#if MEGA2560

            __Port = new SerialPort(((ToolStripMenuItem)sender).Text, 115200);
            __Port.DataReceived += PortOnDataReceived;
            __Port.Open();
            __ComBuffer = String.Empty;
#endif


#if STM32

            try
            {
                if (PortDataComm.IsOpen) return;
                PortDataComm.Open(((ToolStripMenuItem)sender).Text, 115200);

                byte[] tbuff;
                var answer = PortDataComm.SendCommand(4, out tbuff, 1, 2, 3, 4);
                if (answer != PortDataComm.CommandStatus.OK)
                {
                    ShowError("Unable to connect");
                    PortDataComm.Close();
                }

                toolStripConnection.Text = "Connected";
            }
            catch
            {
                ShowError("Unable to connect");
            }
#endif

            __Reader = new Thread(RunReaderOnTick) { IsBackground = true };
            __Reader.Start();

            connectToolStripMenuItem.Enabled = false;
            disconnectToolStripMenuItem.Enabled = true;
        }

#if MEGA2560
        private string __ComBuffer = String.Empty;

        private void PortOnDataReceived(object sender, SerialDataReceivedEventArgs args)
        {
            if (args.EventType == SerialData.Eof) return;
            if (__Port.BytesToRead == 0) return;

            int len = __Port.BytesToRead;
            var line = __Port.ReadExisting();
            __ComBuffer += line;

            while (true)
            {
                int nl = __ComBuffer.IndexOf(Environment.NewLine, StringComparison.InvariantCulture);

                if (nl == -1) break;

                var data = __ComBuffer.Substring(0, nl);
                __ComBuffer = nl+2 < __ComBuffer.Length ?  __ComBuffer.Substring(nl + 2) : String.Empty;

                if (InvokeRequired)
                    Invoke((MethodInvoker)(() => ParseComData(data)));

                ParseComData(data);
            }
        }
#endif

#if STM32

        private void PortOnOnDataRecieved(int command, PortDataComm.CommandStatus success, byte[] data)
        {
            if (success != PortDataComm.CommandStatus.OK) return;

            if (command == 2)
            {
                var intdata = ConvertBytes(data);
                if (InvokeRequired)
                    Invoke(new ThreadSafeInvokeDelegate(() =>
                    {
                        var p = CurrentPanel;
                        if (p != null) p.TakeData(intdata);
                    }));
                else
                {
                    var p = CurrentPanel;
                    if (p != null) p.TakeData(intdata);
                }
            }

            if (command == 5)
            {
                var intdata = ConvertBytes(data);
                if (InvokeRequired)
                    Invoke(new ThreadSafeInvokeDelegate(() =>
                    {
                        var p = CurrentPanel;
                        if (p != null) p.TakeDataPPM(intdata);
                    }));
                else
                {
                    var p = CurrentPanel;
                    if (p != null) p.TakeDataPPM(intdata);
                }
            }

            if (command == 8)
            {
                var vdata = new byte[128];
                Array.Copy(data, vdata, data.Length);

                if (InvokeRequired)
                    Invoke(new ThreadSafeInvokeDelegate(() =>
                    {
                        var p = CurrentPanel;
                        if (p != null) p.TakeJoyData(vdata);
                    }));
                else
                {
                    var p = CurrentPanel;
                    if (p != null) p.TakeJoyData(vdata);
                }
            }
        }

        private int[] ConvertBytes(byte[] data)
        {
            var res = new int[data.Length / 2];

            for (var i = 0; i < res.Length; i++)
            {
                res[i] = (short)((data[i * 2 + 1] << 8) | data[i * 2]);
            }

            return res;
        }


#endif
        private void RunReaderOnTick()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(250);

                    if (!PortDataComm.IsOpen)
                    {
                        Invoke(new Action(() =>
                        {
                            connectToolStripMenuItem.Enabled = true;
                            disconnectToolStripMenuItem.Enabled = false;
                            toolStripConnection.Text = "Not connected";
                        }));

                        break;
                    }

                    if (ReaderEnabled)
                    {
                        byte[] data;

                        if (PortDataComm.SendCommand(2, out data) == PortDataComm.CommandStatus.OK)
                            PortOnOnDataRecieved(2, PortDataComm.CommandStatus.OK, data);

                        if (PortDataComm.SendCommand(5, out data) == PortDataComm.CommandStatus.OK)
                            PortOnOnDataRecieved(5, PortDataComm.CommandStatus.OK, data);

                        if (PortDataComm.SendCommand(8, out data) == PortDataComm.CommandStatus.OK)
                            PortOnOnDataRecieved(8, PortDataComm.CommandStatus.OK, data);

                    }

                }
                catch (ThreadAbortException)
                {
                    break;
                }
            }

            __Reader = null;
        }
#if MEGA2560

        void ParseComData(string data)
        {
            if (data.StartsWith("MD"))
            {
                var intdata = data.Split(' ').Skip(1).Select(n => int.Parse(n, NumberStyles.HexNumber)).ToArray();
                
                foreach (var panel in Panels)
                    panel.TakeData(intdata);
            }

            if (data.StartsWith("JD"))
            {
                var intdata = data.Split(' ').Skip(1).Select(n => byte.Parse(n, NumberStyles.HexNumber)).ToArray();
                var jdata = __JoyInfo.Parse(intdata);

                foreach (var panel in Panels)
                    panel.TakeJoyData(jdata);
            }
        }
#endif


        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PortDataComm.IsOpen)
            {
                PortDataComm.Close();
            }
        }

        #endregion

        #region Joystick menus

        private void UpdateJoystics()
        {
            editConfigToolStripMenuItem.DropDownItems.Clear();
            editConfigToolStripMenuItem.DropDownItems.AddRange(
                __JoyInfos.Select(joy => new ToolStripMenuItem(joy.Name, null, editConfigToolStripMenuItem_Click) { Tag = joy.ID }).Cast<ToolStripItem>().ToArray()
            );


            deleteJoystickToolStripMenuItem.DropDownItems.Clear();
            deleteJoystickToolStripMenuItem.DropDownItems.AddRange(
                __JoyInfos.Select(joy => new ToolStripMenuItem(joy.Name, null, deleteConfigToolStripMenuItem_Click) { Tag = joy.ID }).Cast<ToolStripItem>().ToArray()
            );
        }

        private void ShowJoystickDialog(Guid ID)
        {
            var dlg = new CreateJoystickDialog();
            if (ID != Guid.Empty) dlg.JoystickInfo = __JoyInfos.FirstOrDefault(j => j.ID == ID);

            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            if (ID == Guid.Empty)
                __JoyInfos.Add(dlg.JoystickInfo);
            else
                __JoyInfos[__JoyInfos.FindIndex(j => j.ID == ID)] = dlg.JoystickInfo;

            UpdateJoystics();
            foreach (var panel in Panels) panel.JoystickUpdated();
            SetChangedState();
        }

        private void ShowJoystickDialog(JoystickConfig joy)
        {
            var dlg = new CreateJoystickDialog();
            dlg.JoystickInfo = joy;

            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            if (__JoyInfos.All(j => j.ID != joy.ID))
                __JoyInfos.Add(dlg.JoystickInfo);
            else
                __JoyInfos[__JoyInfos.FindIndex(j => j.ID == joy.ID)] = dlg.JoystickInfo;

            UpdateJoystics();
            foreach (var panel in Panels) panel.JoystickUpdated();
            SetChangedState();
        }

        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowJoystickDialog(Guid.Empty);
        }

        private void editConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowJoystickDialog((Guid)((ToolStripItem)sender).Tag);
        }

        private void deleteConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ID = (Guid)((ToolStripItem)sender).Tag;
            var idx = __JoyInfos.FindIndex(j => j.ID == ID);
            if (idx < 0) return;

            if (Panels.Any(dp => dp.CheckJoystickInUse(__JoyInfos[idx])))
            {
                MessageBox.Show(this,
                    String.Format("Unable to delete joystick {0}, it's in use by current configuration. Remove usage of joystick first", __JoyInfos[idx].Name),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (MessageBox.Show(this, string.Format("Delete joystick {0}", __JoyInfos[idx].Name), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            __JoyInfos.RemoveAt(idx);

            foreach (var panel in Panels) panel.JoystickUpdated();

            SetChangedState();
            UpdateJoystics();
        }

        private string[] SplitValues(string data)
        {
            var rx = new Regex("[^0-9A-Fa-f]");
            var ds = rx.Replace(data, "");

            var res = new List<string>();
            while (ds.Length > 0)
            {
                res.Add(ds.Substring(0,2));
                ds = ds.Substring(2);
            }

            return res.ToArray();
        }

        private void createFromBoardCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new ParseJoyDialog();
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            var joy = new JoystickConfig();
            try
            {
                var parser = new DescriptionParser(joy);
                var data = SplitValues(dlg.DescriptorText);
                parser.Parse(data.Select(s => uint.Parse(s, NumberStyles.HexNumber)).GetEnumerator());
                joy.VendorID = dlg.VendorID;
                joy.ProductID = dlg.ProductID;
                joy.ReportStructure = dlg.DescriptorText;
            }
            catch (Exception exx)
            {
                ShowError(exx.Message, CommonResources.ParseError);
                return;
            }

            ShowJoystickDialog(joy);
        }


        #endregion

        #region Toolstrip

        void SetChangedState()
        {
            statusFileStateLabel.Text = CommonResources.fmMain_SetChangedState_Changed;
            statusFileStateLabel.Font = new Font(statusFileStateLabel.Font, FontStyle.Bold);
        }

        void SetSavedState()
        {
            statusFileStateLabel.Text = CommonResources.fmMain_SetSavedState_Saved;
            statusFileStateLabel.Font = new Font(statusFileStateLabel.Font, FontStyle.Regular);
        }


        #endregion

        #region File procedures

        private readonly List<string> LastFiles = new List<string>(10);
        private string CurrentFileName;

        private void SetFileState()
        {
            toolStripFileLabel.Text = CurrentFileName ?? CommonResources.NewFile;
        }

        private void RecreateLastMenu()
        {
            recentToolStripMenuItem.DropDownItems.Clear();

            while (LastFiles.Count > 10)
                LastFiles.RemoveAt(LastFiles.Count - 1);

            foreach (var file in LastFiles)
            {
                string txt = Path.GetFileName(file);

                var item = new ToolStripMenuItem(txt) { Tag = file };
                item.Click += (sender, args) => LoadFile((string)((ToolStripMenuItem)sender).Tag);

                recentToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        private void ReadLastList()
        {
            var fname = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create) + "\\RCJoy\\LastFiles.txt";

            if (!File.Exists(fname)) return;

            using (var reader = File.OpenText(fname))
            {
                string line;
                while ((line = reader.ReadLine()) != null) LastFiles.Add(line);
            }

            RecreateLastMenu();
        }

        private void SaveLastList()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create) + "\\RCJoy"))
            {
                Directory.CreateDirectory(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create) + "\\RCJoy");
            }

            var fname = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create) + "\\RCJoy\\LastFiles.txt";

            using (var writer = File.CreateText(fname))
            {
                foreach (var file in LastFiles)
                    writer.WriteLine(file);

                writer.Flush();
            }
        }

        private void AddFileToLastFiles(string fname)
        {
            LastFiles.Remove(fname);
            LastFiles.Insert(0, fname);
            RecreateLastMenu();
        }

        private void LoadFile(string fname)
        {
            if (!File.Exists(fname)) return;

            XDocument file;

            using (var reader = File.OpenText(fname))
            {
                file = XDocument.Load(reader);
            }

            modelsTabSwitch.TabPages.Clear();
            FRAMMapper.Clear();
            __JoyInfos.Clear();

            __JoyInfos.AddRange(JoystickConfig.LoadFromXML(file));
            __Settings = ProjectSettings.LoadFromXML(file) ?? new ProjectSettings();

            if (file.Root != null)
            {
                var xmodels = file.Root.Element("Models");

                if (xmodels != null)
                {
                    foreach (var xElement in xmodels.Elements())
                    {
                        var panel = new DesignPanel();
                        panel.Deserialize(xElement);
                        AddModelPanel(panel);
                    }

                    foreach (var panel in Panels)
                    {
                        panel.Link();
                    }
                }
            }

            CurrentFileName = fname;

            AddFileToLastFiles(fname);
            SaveLastList();
            SetFileState();
            SetSavedState();
            UpdateJoystics();
        }

        private void SaveFile(string fname)
        {
            var file = new XDocument();
            var root = new XElement("Config");
            file.Add(root);

            root.Add(__JoyInfos.Select(j => j.Serialize()).Cast<object>().ToArray());

            if (__Settings != null)
                root.Add(__Settings.Serialize());

            if (modelsTabSwitch.TabPages.Count > 0)
            {
                var xpanels = new XElement("Models");
                root.Add(xpanels);

                foreach (var panel in Panels) xpanels.Add(panel.Serialize());
            }


            using (var writer = File.CreateText(fname))
            {
                file.Save(writer);
                writer.Flush();
            }

            CurrentFileName = fname;

            SetSavedState();
            SetFileState();
            AddFileToLastFiles(fname);
            SaveLastList();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdMain.ShowDialog(this) != DialogResult.OK) return;
            LoadFile(ofdMain.FileName);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fsdSave.ShowDialog(this) != DialogResult.OK) return;

            string fname = fsdSave.FileName;
            SaveFile(fname);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentFileName != null)
                SaveFile(CurrentFileName);
            else
                saveAsToolStripMenuItem_Click(sender, e);
        }

        #endregion

        #region Models management

        public IEnumerable<DesignPanel> Panels
        {
            get
            {
                return modelsTabSwitch.TabPages.OfType<TabPage>().Select(page => (DesignPanel)page.Controls[0]);
            }
        }

        public DesignPanel CurrentPanel
        {
            get
            {
                return modelsTabSwitch.SelectedTab == null
                           ? null
                           : (DesignPanel)modelsTabSwitch.SelectedTab.Controls[0];
            }
        }

        private void ModelsUpdated()
        {
            foreach (var panel in Panels) panel.ModelsUpdated();
        }

        private void AddModelPanel(DesignPanel designPanel)
        {
            var page = new TabPage(designPanel.Text);

            page.Controls.Add(designPanel);
            designPanel.Dock = DockStyle.Fill;
            designPanel.DataUpdated += (sender, args) => SetChangedState();

            modelsTabSwitch.TabPages.Add(page);

            designPanel.Initialize();
        }

        private void AddModelPanel(ModelInfo model)
        {
            var designPanel = new DesignPanel();
            designPanel.SetModel(model);

            AddModelPanel(designPanel);
        }

        private void createModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new CreateModelDialog { Model = null };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            AddModelPanel(dlg.Model);
            ModelsUpdated();
        }


        private void updateModelDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var panel = CurrentPanel;
            if (panel == null) return;

            var dlg = new CreateModelDialog { Model = panel.GetModel() };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            panel.SetModel(dlg.Model);

            ModelsUpdated();
            SetChangedState();
        }

        private void deleteModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var panel = CurrentPanel;
            if (panel == null) return;

            modelsTabSwitch.TabPages.Remove(modelsTabSwitch.SelectedTab);
            panel.Remove();
            panel.Dispose();

            ModelsUpdated();
            SetChangedState();
        }

        public void ActivateTab(DesignPanel panel)
        {
            var tab = (TabPage)panel.Parent;
            modelsTabSwitch.SelectTab(tab);
        }


        #endregion

        #region Sketch menus


        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if MEGA2560
            var dlg = new SetProjectSettingsDialog {Data = __Settings};
            if(dlg.ShowDialog(this) != DialogResult.OK) return;
#endif
#if STM32
            var dlg = new STM32ProjectSettings { Data = __Settings };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
#endif
            __Settings = dlg.Data;

            SetChangedState();
        }

        #endregion

        #region Generators

        private void ShowError(string message, string caption = null)
        {
            MessageBox.Show(this, message, caption ?? "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool ExecuteStep(Action step)
        {
            try
            {
                step();
                return true;
            }
            catch (Exception exx)
            {
                ShowError(exx.Message);
                toolStripCompileLabel.Text = CommonResources.fmMain_ExecuteStep_Generation_failed;
                return false;

            }
        }



        private void generateProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (JoyInfos.Count == 0)
            {
                ShowError("Joystick infromation is absent!", "Check error");
                return;
            }

            toolStripCompileLabel.Text = CommonResources.fmMain_generateProjectToolStripMenuItem_Click_Generating____;

            if (!ExecuteStep(() => JoyInfos.ForEach( j => j.Check()))) return;

            if (string.IsNullOrEmpty(Settings.OutputPath))
            {
                ShowError("Output path is not set");
                return;
            }

            var gen = new CodeGenerator(__Settings, __JoyInfos, Panels.ToArray(), ExecuteStep);

            if (gen.GenerateProject())
            {
                toolStripCompileLabel.Text = CommonResources.fmMain_generateProjectToolStripMenuItem_Click_Generation_successed;
                SetChangedState();
            }
        }

        #endregion

        private void generateAndUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Panels.Count(pnl => !pnl.GetModel().IsMenu) <= 0)
            {
                ShowError("There is no code to upload", "Check error");
                return;
            }

            if (JoyInfos.Count == 0)
            {
                ShowError("Joystick infromation is absent!", "Check error");
                return;
            }

            toolStripCompileLabel.Text =
                CommonResources.fmMain_generateProjectToolStripMenuItem_Click_Generating____;

            if (!ExecuteStep(() => JoyInfos.ForEach(j => j.Check()))) return;


            if (string.IsNullOrEmpty(Settings.OutputPath))
            {
                ShowError("Output path is not set");
                return;
            }

            var gen = new CodeGenerator(__Settings, __JoyInfos, Panels.ToArray(), ExecuteStep);

            if (!gen.GenerateProject()) return;


            toolStripCompileLabel.Text =
                CommonResources.fmMain_generateProjectToolStripMenuItem_Click_Generation_successed;
            SetChangedState();

            var fUpload = new FlashBoardDialog429();
            fUpload.ProgramCode = gen.ByteCode;

            fUpload.ShowDialog(this);
        }

        private void uploadFirmwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fUpload = new FlashBoardDialog();
            fUpload.ShowDialog(this);
        }

        private void statusLine_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void eEPManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var d = new EEPMapDialog())
            {
                d.SetData(Panels);
                d.ShowDialog(this);
            }
        }

        private void fmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (__Reader != null)
                __Reader.Abort();

            if (PortDataComm.IsOpen)
                PortDataComm.Close();
        }

        private void liveDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReaderEnabled = !ReaderEnabled;
            liveDataToolStripMenuItem.Checked = ReaderEnabled;
        }

        private void boardToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            liveDataToolStripMenuItem.Checked = ReaderEnabled;
        }

        private void FillComPorts()
        {
            connectToolStripMenuItem.DropDownItems.Clear();
            connectToolStripMenuItem.DropDownItems.AddRange(SerialPort.GetPortNames()
                .Select(port => new ToolStripMenuItem(port, null, clickPort)
                {
                    CheckState = CheckState.Unchecked,
                    CheckOnClick = false
                } as ToolStripItem).ToArray());

        }

        private void scanPortsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillComPorts();
        }

        private void captureDebugDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DebugJoyDialog().ShowDialog(this);
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SendDebugJoyDialog().ShowDialog(this);
        }
    }
}

