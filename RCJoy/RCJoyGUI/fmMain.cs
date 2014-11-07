using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;
using Tahorg.RCJoyGUI.Dialogs;

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
#if STM32
        public static readonly PortCommunicator __Port = new PortCommunicator();
#endif
        private readonly Timer __Reader;
        private bool ReaderEnabled = false;

        private static JoystickConfig __JoyInfo;
        public static JoystickConfig JoyInfo { get { return __JoyInfo; } }

        private static ProjectSettings __Settings; 
        public static ProjectSettings Settings { get { return __Settings; } }

        public static fmMain Instance { get; private set; }

        public fmMain()
        {
            Instance = this;

            InitializeComponent();

            connectToolStripMenuItem.DropDownItems.AddRange(SerialPort.GetPortNames()
                .Select(port => new ToolStripMenuItem(port, null, clickPort)
                    {
                        CheckState = CheckState.Unchecked,
                        CheckOnClick = false
                    } as ToolStripItem).ToArray());


            __Reader = new Timer { Interval = 250 };
            __Reader.Tick += ReaderOnTick;

            __Settings = new ProjectSettings();

#if STM32
            __Port.OnDataRecieved += PortOnOnDataRecieved;
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
            __Port.Open(((ToolStripMenuItem)sender).Text, 57600);
#endif

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
       
        private void PortOnOnDataRecieved(byte command, byte[] data)
        {
            if (command == 2)
            {
                var intdata = ConvertBytes(data);
                if (InvokeRequired) Invoke(new ThreadSafeInvokeDelegate( () =>
                {
                    foreach (var panel in Panels)
                        panel.TakeData(intdata);
                }));

                __Port.SendCommand(5, null);
            }

            if (command == 5)
            {
                var intdata = ConvertBytes(data);
                if (InvokeRequired) Invoke(new ThreadSafeInvokeDelegate(() =>
                {
                    foreach (var panel in Panels)
                        panel.TakeDataPPM(intdata);
                }));
            }
        }

        private int[] ConvertBytes(byte[] data)
        {
            var res = new int[data.Length/2];

            for (var i = 0; i < res.Length; i++)
            {
                res[i] = (short)((data[i * 2 + 1] << 8) | data[i * 2]);
            }

            return res;
        }


#endif
        private void ReaderOnTick(object sender, EventArgs eventArgs)
        {
            if (!__Port.IsOpen)
            {
                connectToolStripMenuItem.Enabled = true;
                disconnectToolStripMenuItem.Enabled = false;

                __Reader.Stop();
                return;
            }

            if(ReaderEnabled)
                __Port.SendCommand(2,null);
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
            if (__Port != null && __Port.IsOpen)
            {
                __Port.Close();
            }

            __Reader.Start();
        }

        #endregion

        #region Joystick menus

        private void ShowJoystickDialog(JoystickConfig config)
        {
            var dlg = new CreateJoystickDialog();
            if (config != null) dlg.JoystickInfo = config;

            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            __JoyInfo = dlg.JoystickInfo;

            foreach (var panel in Panels) panel.JoystickUpdated();
            SetChangedState();
        }

        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowJoystickDialog(null);
        }

        private void editConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (__JoyInfo == null)
            {
                createNewToolStripMenuItem_Click(sender, e);
            }

            ShowJoystickDialog(__JoyInfo);
        }

        private void createFromBoardCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new ParseJoyDialog();
            if(dlg.ShowDialog(this) != DialogResult.OK) return;

            var joy = new JoystickConfig();
            try
            {
                var parser = new DescriptionParser(joy);
                var data = dlg.DescriptorText.Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                parser.Parse(data.Select(s => uint.Parse(s, NumberStyles.HexNumber)).GetEnumerator());
            }
            catch (Exception exx)
            {
                ShowError(exx.Message, "Parsing error");
                return;
            }

            ShowJoystickDialog(joy);
        }


        #endregion

        #region Toolstrip

        void SetChangedState()
        {
            statusFileStateLabel.Text = "Changed *";
            statusFileStateLabel.Font = new Font(statusFileStateLabel.Font, FontStyle.Bold);
        }

        void SetSavedState()
        {
            statusFileStateLabel.Text = "Saved";
            statusFileStateLabel.Font = new Font(statusFileStateLabel.Font, FontStyle.Regular);
        }


        #endregion

        #region File procedures

        private readonly List<string> LastFiles = new List<string>(10);
        private string CurrentFileName = null;
        


        private void SetFileState()
        {
            toolStripFileLabel.Text = CurrentFileName ?? "New file";
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

            __JoyInfo = JoystickConfig.LoadFromXML(file);
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

        }

        private void SaveFile(string fname)
        {
            var file = new XDocument();
            var root = new XElement("Config");
            file.Add(root);

            if (__JoyInfo != null)
                root.Add(__JoyInfo.Serialize());

            if(__Settings != null)
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
            var tab = (TabPage) panel.Parent;
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
                toolStripCompileLabel.Text = "Generation failed";
                return false;
                
            }
        }



        private void generateProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (JoyInfo == null)
            {
                ShowError("Joystick infromation is absent!", "Check error");
                return;
            }

            toolStripCompileLabel.Text = "Generating ...";

            if (!ExecuteStep(() => JoyInfo.Check())) return;

            if (string.IsNullOrEmpty(Settings.OutputPath))
            {
                ShowError("Output path is not set");
                return;
            }

            var gen = new CodeGenerator(__Settings, __JoyInfo, Panels.ToArray(), ExecuteStep);

            if (gen.GenerateProject())
            {
                toolStripCompileLabel.Text = "Generation successed";
                SetChangedState();
            }
        }

        #endregion

        private void generateAndUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Panels.Count(pnl => !pnl.GetModel().IsMenu) > 0)
            {
                if (JoyInfo == null)
                {
                    ShowError("Joystick infromation is absent!", "Check error");
                    return;
                }

                toolStripCompileLabel.Text = "Generating ...";

                if (!ExecuteStep(() => JoyInfo.Check())) return;

                if (string.IsNullOrEmpty(Settings.OutputPath))
                {
                    ShowError("Output path is not set");
                    return;
                }

                var gen = new CodeGenerator(__Settings, __JoyInfo, Panels.ToArray(), ExecuteStep);

                if (!gen.GenerateProject()) return;


                toolStripCompileLabel.Text = "Generation successed";
                SetChangedState();

                var fUpload = new FlashBoardDialog();
                fUpload.ProgramCode = gen.ByteCode;

                fUpload.ShowDialog(this);
            }
            else
            {
                var fUpload = new FlashBoardDialog();
                fUpload.ProgramCode = null;
                fUpload.ShowDialog(this);
            }
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
            if(__Port != null && __Port.IsOpen)
                __Port.Close();
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

    }
}

