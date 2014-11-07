using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;
using Tahorg.RCJoyGUI.ProgrammingPanels;
using Tahorg.RCJoyGUI.ProgrammingPanels.Menu;

namespace Tahorg.RCJoyGUI
{
    public partial class DesignPanel : UserControl, ILinkResolver, IFRAMUser
    {
        private ModelInfo __Model;
        private Guid _ID;

        public Guid ID
        {
            get { return _ID; }
            private set { _ID = value; }
        }

        private int _DataIndex = -1;
        private uint _FRAMStateAddr = 0;

        public DesignPanel()
        {
            InitializeComponent();
            ID = Guid.NewGuid();
        }

        public void Initialize()
        {
            FRAMMapper.Register(this);
        }

        public void Remove()
        {
            FRAMMapper.UnRegisterUser(this);
        }

        
        public uint SlotsUsed { get { return 1; } }

        public uint[] FRAMAddresses
        {
            get { return new[] {_FRAMStateAddr}; }
            set { _FRAMStateAddr = value[0]; }
        }

        public string[] Names { get { return new[] { __Model.CName + "_STATE" }; } }

        public int[] DefaultValues { get { return new []{0}; } }

        #region drag

        private Point dragStart;

        private void mainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var loc = MousePosition;

            var dx = loc.X - dragStart.X;
            var dy = loc.Y - dragStart.Y;

            dragStart = loc;

            if (dx > 0 && mainPanel.Location.X + dx > 0)
                dx = mainPanel.Location.X;
            else if (dx < 0 && mainPanel.Location.X + mainPanel.Size.Width < ClientSize.Width)
                dx = 0;
            else if (dx < 0 && mainPanel.Size.Width + mainPanel.Location.X < ClientSize.Width + dx)
                dx = ClientSize.Width - (mainPanel.Location.X + mainPanel.Size.Width);

            if (dy > 0 && mainPanel.Location.Y + dy > 0)
                dy = mainPanel.Location.Y;
            else if (dy < 0 && mainPanel.Location.Y + mainPanel.Size.Height < ClientSize.Height)
                dy = 0;
            else if (dy < 0 && mainPanel.Size.Height + mainPanel.Location.Y < ClientSize.Height + dy)
                dy = ClientSize.Height - (mainPanel.Location.Y + mainPanel.Size.Height);

            mainPanel.Location = mainPanel.Location + new Size(dx, dy);
        }

        private void mainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                dragStart = MousePosition;
                foreach (var el in __Elements) el.IsSelected = false;
            }

            if (e.Button == MouseButtons.Left)
                Capture = true;
        }

        private void mainPanel_MouseUp(object sender, MouseEventArgs e)
        {
            Capture = false;
        }

        #endregion

        #region panels management

        private readonly List<DraggableElement> __Elements = new List<DraggableElement>(32);

        public IEnumerable<DraggableElement> Elements
        {
            get { return __Elements; }
        }

        private void LinkPanel(DraggableElement el, bool notMove = false)
        {
            if (!notMove)
                el.Location = mainPanel.PointToClient(dragStart);

            el.Parent = mainPanel;
            el.Selected += ElemSelectedChanged;
            el.Move += ElemMove;
            el.LinkSelected += OnPanelLinkSelected;
            el.ModelPanel = this;

            if (el is FMPPMOutDesignPanel)
                ((FMPPMOutDesignPanel) el).OnModeNameChanged += (sender, args) => ModesUpdated();

            __Elements.Add(el);

            el.Initialized();

            OnDataUpdated();
        }


        private void ElemSelectedChanged(object sender, EventArgs e)
        {
            ((Control)sender).BringToFront();

            foreach (var el in __Elements)
            {
                if (el != sender)
                {
                    el.IsSelected = false;
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = __Elements.FirstOrDefault(el => el.IsSelected);
            if (selected == null) return;

            __Elements.Remove(selected);
            selected.Unlink();

            if (selected is FMPPMOutDesignPanel)
                ReindexModes();

            if (SelectedInputPoint != null && SelectedInputPoint.HolderPanel == selected) SelectedInputPoint = null;
            if (SelectedOutputPoint != null && SelectedOutputPoint.HolderPanel == selected) SelectedOutputPoint = null;

            selected.Removed();
            selected.Dispose();

            OnDataUpdated();

            Invalidate();
            Update();
        }

        #endregion

        #region Link points

        private LinkPoint SelectedInputPoint = null;
        private LinkPoint SelectedOutputPoint = null;

        private void OnPanelLinkSelected(object sender, LinkSelectArgumetns e)
        {
            if (!e.ToState)
            {
                switch (e.Link.Direction)
                {
                    case enLinkDirection.Input:
                        SelectedInputPoint = null;

                        mainPanel.Invalidate();
                        mainPanel.Update();

                        break;

                    case enLinkDirection.Output:
                        SelectedOutputPoint = null;
                        break;
                }

                return;
            }

            switch (e.Link.Direction)
            {
                case enLinkDirection.Input:
                    if (SelectedInputPoint != null) SelectedInputPoint.IsSelected = false;
                    SelectedInputPoint = e.Link;

                    mainPanel.Invalidate();
                    mainPanel.Update();

                    break;

                case enLinkDirection.Output:
                    if (SelectedOutputPoint != null) SelectedOutputPoint.IsSelected = false;
                    SelectedOutputPoint = e.Link;
                    break;
            }



            CheckLinkMI();
        }

        private void CheckLinkMI()
        {
            linkToolStripMenuItem.Enabled = (SelectedInputPoint != null && SelectedOutputPoint != null &&
                                 SelectedInputPoint.LinkType == SelectedOutputPoint.LinkType &&
                                 SelectedInputPoint.LinkedTo == null);

        }

        private void linkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedInputPoint == null ||
                SelectedOutputPoint == null ||
                SelectedInputPoint.LinkedTo != null ||
                SelectedInputPoint.LinkType != SelectedOutputPoint.LinkType
            ) return;

            if (SelectedInputPoint.HolderPanel.IsInChain(SelectedOutputPoint.HolderPanel)) return;

            OnDataUpdated();

            SelectedInputPoint.LinkTo(SelectedOutputPoint);
            SelectedInputPoint.IsSelected = false;
            SelectedInputPoint = null;

            SelectedOutputPoint.IsSelected = false;
            SelectedOutputPoint = null;

            mainPanel.Invalidate();
            mainPanel.Update();
        }

        private void unlinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedInputPoint != null && SelectedOutputPoint != null && SelectedInputPoint.LinkedTo != SelectedOutputPoint) return;

            if (SelectedInputPoint != null)
                SelectedInputPoint.Unlink();
            else if (SelectedOutputPoint != null)
                SelectedOutputPoint.Unlink();

            OnDataUpdated();

            mainPanel.Invalidate();
            mainPanel.Update();
        }

        #endregion

        #region gui response

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            var ch = mainPanel.GetChildAtPoint(PointToClient(MousePosition), GetChildAtPointSkip.Invisible);
            var selected = __Elements.FirstOrDefault(el => el.IsSelected);

            removeToolStripMenuItem.Enabled = selected != null;

            modelSelectorToolStripMenuItem.Enabled = false;

            if (__Model.IsMenu)
            {
                variablesToolStripMenuItem.Enabled =
                mixersToolStripMenuItem.Enabled =
                axismodToolStripMenuItem.Enabled =
                modelToolStripMenuItem.Enabled =
                pPMOUTToolStripMenuItem.Enabled = false;

                menuActionsToolStripMenuItem.Enabled = true;

            }
            else
            {
                variablesToolStripMenuItem.Enabled =
                mixersToolStripMenuItem.Enabled =
                axismodToolStripMenuItem.Enabled =
                modelToolStripMenuItem.Enabled =
                pPMOUTToolStripMenuItem.Enabled = true;

                menuActionsToolStripMenuItem.Enabled = false;
            }
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            foreach (var el in __Elements) el.IsSelected = false;
            if (SelectedInputPoint != null)
            {
                SelectedInputPoint.IsSelected = false;
                SelectedInputPoint = null;
            }

            if (SelectedOutputPoint != null)
            {
                SelectedOutputPoint.IsSelected = false;
                SelectedOutputPoint = null;
            }

            CheckLinkMI();
        }

        private void ElemMove(object sender, EventArgs e)
        {
            OnDataUpdated();
            mainPanel.Invalidate();
            mainPanel.Update();
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            foreach (var element in __Elements) element.DrawLinks(e.Graphics, mainPanel);
        }

        #endregion


        #region Save/Read

        public XElement Serialize()
        {
            var xelem = new XElement("ModelSetup",
                new XAttribute("ID", ID),
                new XAttribute("StateAddr", _FRAMStateAddr),
                new XAttribute("DataIndex", _DataIndex),
                __Model.Seriliaze()
                );

            var xpanels = new XElement("Panels");
            xelem.Add(xpanels);

            foreach (var element in __Elements)
            {
                xpanels.Add(element.Serialize());
            }

            return xelem;
        }

        public void Deserialize(XElement data)
        {
            SetModel(new ModelInfo(data.Element("Model")));

            uint.TryParse(data.AttributeValue("StateAddr") ?? "0", out _FRAMStateAddr);
            int.TryParse(data.AttributeValue("DataIndex") ?? "-1", out _DataIndex);
            Guid.TryParse(data.AttributeValue("ID") ?? ID.ToString(), out _ID);

            var xpanels = data.Element("Panels");
            if (xpanels == null) return;

            foreach (var xpanel in xpanels.Elements())
            {
                CreatePanel(xpanel.Name, xpanel);
            }
        }

        public void Link()
        {
            foreach (var element in __Elements)
            {
                element.Link(this);
            }
        }

        #endregion

        #region External calls

        public event EventHandler DataUpdated;

        protected virtual void OnDataUpdated()
        {
            EventHandler handler = DataUpdated;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public ModelInfo GetModel()
        {
            return __Model;
        }


        public void SetModel(ModelInfo model)
        {
            __Model = model;
            Text = model.IsMenu ? "System menu" : model.Name;

            if (Parent != null)
                Parent.Text = model.Name;

            var panel = __Elements.OfType<PPMOutDesignPanel>().FirstOrDefault();
            if (panel != null) panel.InitializePanels(__Model);

            if (__Elements.OfType<FMPPMOutDesignPanel>().Any())
                ReindexModes();
        }

        public void JoystickUpdated()
        {
            var panel = __Elements.OfType<JoystickDesignPanel>().FirstOrDefault();
            if (panel == null) return;

            var pos = panel.Location;

            panel.Dispose();

            panel = new JoystickDesignPanel();
            panel.Location = pos;
            panel.InitializePanels(fmMain.JoyInfo);
            LinkPanel(panel, true);

            Link();
        }

        public void ModelsUpdated()
        {
            foreach (var element in __Elements)
            {
                element.ModelsUpdated();
            }
        }


        public void ModesUpdated()
        {
            foreach (var element in __Elements)
            {
                element.ModesUpdated();
            }
        }

        #endregion

        #region Resolver


        public LinkPoint GetLink(Guid objectID, string LinkName)
        {
            var elem = __Elements.FirstOrDefault(element => element.ID == objectID);
            if (elem == null) return null;
            return elem.GetLink(LinkName);
        }

        public ILinkPanel GetPanel(Guid objectID)
        {
            return __Elements.FirstOrDefault(element => element.ID == objectID);
        }

        #endregion

        #region Panels creation

        private void CreatePanel(XName panelName, XElement xElement)
        {
            switch (panelName.ToString())
            {
                case "Joystick":
                    {
                        var panel = new JoystickDesignPanel();
                        panel.InitializePanels(fmMain.JoyInfo);
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "PPMOut":
                    {
                        var panel = new PPMOutDesignPanel();
                        panel.InitializePanels(__Model);
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "FMPPMOut":
                    {
                        var panel = new FMPPMOutDesignPanel();
                        panel.InitializePanels(__Model, -1);
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "DeltaMixer":
                    {
                        var panel = new DeltaMixerDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "VTAilMixer":
                    {
                        var panel = new VTailMixerDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "AilRudderMixer":
                    {
                        var panel = new AilronToRudderMixerDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "FlaperonsMixer":
                    {
                        var panel = new FlaperonMixerDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "ThRudderMixer":
                    {
                        var panel = new ThrottleToElevMixerDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "Exponent":
                    {
                        var panel = new ExponentDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "Invertor":
                    {
                        var panel = new InvertorDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "LogicalButton":
                    {
                        var panel = new LogicalButtonDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "Variable":
                    {
                        var panel = new VariableDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "EEPVariable":
                    {
                        var panel = new EEPVariableDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "Trimmer":
                    {
                        var panel = new TrimmerDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "TrimmerEx":
                    {
                        var panel = new TrimmerDesignPanelEx();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "ValueMapper":
                    {
                        var panel = new ValueMapperDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }


                case "ValueAxisModifier":
                    {
                        var panel = new ValueAxisModifierDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "ValueSwitcher":
                    {
                        var panel = new ValueSwitcherDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;

                    }

                case "SelectModel":
                    {
                        var panel = new ModelSelectorDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "PPMMapper":
                    {
                        var panel = new PPMMaperDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "PPMMapperEx":
                    {
                        var panel = new PPMMapperExDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "ButtonHoldSwitcher":
                    {
                        var panel = new ButtonHoldDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;
                    }

                case "HatToButtonMapper":
                    {
                        var panel = new HatToButtonMapperDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;

                    }

                case "EnterMenu":
                    {
                        var panel = new MenuEnterDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;

                    }

                case "MenuActions":
                    {
                        var panel = new MenuActionsDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;

                    }

                case "Constant":
                    {
                        var panel = new ConstantDesignPanel();
                        panel.Deserialize(xElement);
                        LinkPanel(panel, true);
                        break;

                    }

                case "ModeTrimmer":
                {
                    var panel = new ModeTrimmerMixerDesignPanel();
                    panel.Deserialize(xElement);
                    LinkPanel(panel, true);
                    break;

                }

                case "TrimmerEmulator":
                {
                    var panel = new TrimmerEmulatorDesignPanel();
                    panel.Deserialize(xElement);
                    LinkPanel(panel, true);
                    break;

                }

            }
        }

        #region main

        private void joystickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (__Elements.OfType<JoystickDesignPanel>().Any()) return;

            var panel = new JoystickDesignPanel();
            panel.InitializePanels(fmMain.JoyInfo);
            LinkPanel(panel);
        }

        private void pPMOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (__Elements.OfType<PPMOutDesignPanel>().Any())
            {
                MessageBox.Show(Parent, "PPM out block is already present for this model", "Unable to create block",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (__Elements.OfType<FMPPMOutDesignPanel>().Any())
            {
                MessageBox.Show(Parent, "Model already has at least one flight mode PPM out block", "Unable to create block",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            var panel = new PPMOutDesignPanel();
            panel.InitializePanels(__Model);
            LinkPanel(panel);
        }


        private void flightModesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (__Elements.OfType<PPMOutDesignPanel>().Any())
            {
                MessageBox.Show(Parent, "PPM out block is already present for this model", "Unable to create block",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            var maxIdx = __Elements.OfType<FMPPMOutDesignPanel>().Aggregate(-1, (v, p) => Math.Max(p.ModeIndex, v));

            var panel = new FMPPMOutDesignPanel();
            panel.InitializePanels(__Model, maxIdx + 1);
            LinkPanel(panel);

            ModesUpdated();
        }

        private void modelSelectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new ModelSelectorDesignPanel());
        }

        private void ReindexModes()
        {
            var i = 0;

            foreach (var panel in __Elements.OfType<FMPPMOutDesignPanel>())
            {
                panel.InitializePanels(__Model, i);
                i++;
            }

            ModesUpdated();
        }

        #endregion

        #region Mixers

        private void deltaMixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new DeltaMixerDesignPanel());
        }

        private void vTailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new VTailMixerDesignPanel());
        }


        private void flaperonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new FlaperonMixerDesignPanel());
        }

        private void alieronsRudderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new AilronToRudderMixerDesignPanel());
        }

        private void throttleElevatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new ThrottleToElevMixerDesignPanel());
        }

        #endregion

        #region Mods

        private void centeredExpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new ExponentDesignPanel { Centered = true });
        }

        private void rangeExpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new ExponentDesignPanel { Centered = false });

        }

        private void invertorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new InvertorDesignPanel());

        }

        private void trimmerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new TrimmerDesignPanelEx());
            //            LinkPanel(new TrimmerDesignPanel());
        }

        private void pPMMaperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new PPMMapperExDesignPanel());
        }
        private void modeTrimmerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new ModeTrimmerMixerDesignPanel());
            ModesUpdated();
        }

        private void trimmerEmulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new TrimmerEmulatorDesignPanel());
        }

        private void valueTrimmerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new TrimmerDesignPanel());
        }


        #endregion

        #region Value types

        private void constantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new ConstantDesignPanel());
        }

        private void temporalVariableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new VariableDesignPanel());
        }

        private void eEPROMVariableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new EEPVariableDesignPanel());
        }

        private void hatValueSwitcherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new ValueMapperDesignPanel());
        }

        private void valueAxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new ValueAxisModifierDesignPanel());
        }

        private void switchingStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new ValueSwitcherDesignPanel());
        }

        #endregion

        #region Buttons

        private void logicalButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new LogicalButtonDesignPanel());
        }

        private void buttonHoldSwitchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new ButtonHoldDesignPanel());
        }

        private void hatButtonSwitcherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new HatToButtonMapperDesignPanel());
        }


        #endregion

        #region Menu

        private void openMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinkPanel(new MenuEnterDesignPanel());
        }


        private void menuActionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LinkPanel(new MenuActionsDesignPanel());
        }


        #endregion

        #endregion

        #region Generators

        public void PreGenerate()
        {
            foreach (var element in __Elements)
                element.PreGenerate();
        }

        public void TakeData(int[] intdata)
        {
            foreach (var element in __Elements)
                try { element.TakeData(intdata); }
                catch { }
        }

        public void TakeDataPPM(int[] intdata)
        {
            foreach (var element in __Elements)
                try { element.TakeDataPPM(intdata); }
                catch { }
        }


        public void TakeJoyData(int[] jdata)
        {
            foreach (var element in __Elements)
                try { element.TakeJoyData(jdata); }
                catch { }
        }

        public void LinkIndex(ref int Counter)
        {
            _DataIndex = Counter;
            Counter++;

            foreach (var element in __Elements)
            {
                element.LinkIndex(ref Counter);
            }
        }

        public void Check(CodeGeneratorContext context)
        {
            context.CurrentModel = __Model;

            DraggableElement cc = null;

            try
            {
                foreach (var element in __Elements)
                {
                    cc = element;
                    element.BackColor = this.BackColor;
                    element.Check(context);
                }
            }
            catch (Exception)
            {
                if (cc != null) cc.BackColor = Color.Red;
                fmMain.Instance.ActivateTab(this);
                throw;
            }

        }

        public void MapVariables(int modelIndex, CodeGeneratorContext context)
        {
            context.CurrentModel = __Model;
            __Model.Index = (ushort)modelIndex;

            try
            {
                foreach (var element in __Elements)
                {
                    element.MapVariables(context);
                }
            }
            catch (Exception)
            {
                fmMain.Instance.ActivateTab(this);
                throw;
            }
        }

        public void PrepareChains(CodeGeneratorContext context)
        {
            context.CurrentModel = __Model;
            __Model.Generators.Clear();

            var panels = new List<DraggableElement>(__Elements);

            while (panels.Count > 0)
            {
                DraggableElement panel = panels.Find(pnl => pnl.Determined) ?? panels.Find(pnl => pnl.CheckDetermined());

                if (panel == null)
                {
                    fmMain.Instance.ActivateTab(this);

                    foreach (var epnl in panels)
                        epnl.BackColor = Color.Red;

                    throw new CompilationCheckException("Unable to fully determine all the code dependencies, check the links", CompilationCheckException.CompileIteration.Chaining);
                }

                panels.Remove(panel);
                __Model.Generators.Add(panel);
            }
        }

#if MEGA2560

        public void GenerateJoyFile(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            context.CurrentModel = __Model;

            foreach (var generator in __Model.Generators)
                generator.GenerateJoyFile(context, __H, __CPP);
        }

        public void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            context.CurrentModel = __Model;

            __H.WriteLine("#define MODEL_INFO_{0} {1},{2}", __Model.CName, __Model.Index, 
                    __Model.GeneratorChannels
                );

            context.PPMArrayLength = Math.Max(context.PPMArrayLength, __Model.GeneratorChannels);

            foreach (var generator in __Model.Generators)
                generator.GenerateDataMap(context, __H, __CPP);
        }

        public void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            context.CurrentModel = __Model;

            __CPP.WriteLine("if(CurrentModel=={0})",context.CurrentModel.Index);
            __CPP.WriteLine("{");
            
            foreach (var generator in __Model.Generators)
                generator.GenerateCalculator(context, __CPP);

            __CPP.WriteLine("}");
        }

        public void GeneratePreCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            context.CurrentModel = __Model;

            foreach (var generator in __Model.Generators)
                generator.GeneratePreCalculator(context, __CPP);
        }

        public void GenerateInit(CodeGeneratorContext context, TextWriter __CPP)
        {
            if (__Model.Index == 0)
            {
                __CPP.WriteLine("  switchModel(MODEL_INFO_{0});", context.CurrentModel.CName);
                __CPP.WriteLine("  #ifdef LCD");
                __CPP.WriteLine("  lcdPrintLine1(\"{0}\");", context.CurrentModel.Name);
                __CPP.WriteLine("  #endif");
            }
            __CPP.WriteLine();

            context.CurrentModel = __Model;

            foreach (var generator in __Model.Generators)
                generator.GenerateInit(context, __CPP);


        }
#endif

#if STM32
        public void Generate(CodeGeneratorContext context, STMProgram prog)
        {
            context.CurrentModel = __Model;

            prog.Models.Add(__Model);
            prog.PPMLength = (byte)Math.Max(prog.PPMLength, __Model.Channels);

            var stmmodel = prog.Models[__Model.Index];

            context.ModelCode = __Model.IsMenu
                ? prog.MenuCode
                : stmmodel.ModelCode;

            var cnt = __Elements.OfType<FMPPMOutDesignPanel>().Count();

            if (cnt > 0)
            {
                stmmodel.Modes = new STMFlightMode[cnt];
            }

            stmmodel.ModeVariable = (uint)prog.Variables.Add((ushort)_DataIndex, (ushort)_FRAMStateAddr, 
                __Model.CName + "_STATE", __Model.Index, 1, 0, (short)cnt);

            prog.StartupCode.Add(0x0F, stmmodel.ModeVariable, 0);

            foreach (var generator in __Model.Generators)
                generator.GenerateSTMCode(context, prog);
        }

#endif
        #endregion

        private void switchingButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



    }
}
