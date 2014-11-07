using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;
using Tahorg.RCJoyGUI.ProgrammingPanels.Joystick;

namespace Tahorg.RCJoyGUI.ProgrammingPanels
{
    public partial class JoystickDesignPanel : DraggableElement
    {
        public JoystickDesignPanel()
        {
            InitializeComponent();
        }

        private JoystickConfig __CurrentConfig;
        private Guid __PreMapConfigID = Guid.Empty;
        private readonly List<BaseJoystickValuePanel> __Panels = new List<BaseJoystickValuePanel>(32);

        public Guid JoyID
        {
            get { return __CurrentConfig != null ? __CurrentConfig.ID : __PreMapConfigID; }
            set { __PreMapConfigID = value; }
        }

        public void InitializePanels(JoystickConfig config)
        {
            Title = config.Name;

            __CurrentConfig = config;

            var bitscounter = 0;

            for (int i = 0; i < __CurrentConfig.Controls.Count; i++)
            {
                var control = __CurrentConfig.Controls[i];

                if (control is JoystickAxle)
                {
                    AddAxle((JoystickAxle) control, i, bitscounter);
                }
                else if (control is ButtonsCollection)
                {
                    AddButtons((ButtonsCollection)control, i, bitscounter);
                }
                else if (control is HatSwitch)
                {
                    AddHat((HatSwitch)control, i, bitscounter);
                }

                bitscounter += control.Bits();
            }
        }

        private void AddPanel(BaseJoystickValuePanel panel, int BitsCounter)
        {
            int top = __Panels.Count == 0
                          ? labelHead.Bottom + 2
                          : __Panels.Last().Bottom + 2;

            panel.Top = top;
            panel.Left = 3;
            panel.Width = Width - 3;
            panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel.BitsStart = BitsCounter;

            __Panels.Add(panel);
            Controls.Add(panel);
        }

        private void AddAxle(JoystickAxle control, int idx, int BitsCounter)
        {
            var panel = new AxlePanel();
            panel.SetInfo(control, idx);
            AddPanel(panel, BitsCounter);

            RegiseterLink(panel.GetLink());
        }


        private void AddButtons(ButtonsCollection control, int idx, int BitsCounter)
        {
            for (int i = 0; i < control.ButtonsCount; i++)
            {
                var panel = new ButtonPanel();
                panel.SetInfo(control, i + 1, idx);
                AddPanel(panel, BitsCounter + i);

                RegiseterLink(panel.GetLink());
            }
        }

        private void AddHat(HatSwitch control, int idx, int BitsCounter)
        {
            var panel = new HatPanel();
            panel.SetInfo(control, idx);
            AddPanel(panel, BitsCounter);

            RegiseterLink(panel.GetLink());
        }

        public override bool CheckJoystickInUse(JoystickConfig joystickConfig)
        {
            return __CurrentConfig != null && joystickConfig != null && joystickConfig.ID == __CurrentConfig.ID;
        }

        #region Save

        public override XElement CreatXMLSave()
        {
            return new XElement("Joystick");
        }

        public override void Serialize(XElement data)
        {
            base.Serialize(data);

            data.Add(new XAttribute("JoyID", __CurrentConfig.ID));
        }

        public void JoystickUpdated()
        {
            foreach (var panel in __Panels)
            {
                UnRegiseterLink(panel.GetLink());
                Controls.Remove(panel);
                panel.Dispose();
            }

            __Panels.Clear();
        }

        public override void Deserialize(XElement data)
        {
            base.Deserialize(data);

            var idval = data.AttributeValue("JoyID");
            if(idval != null) __PreMapConfigID = new Guid(idval);
        }

        public override void Link(ILinkResolver root)
        {
            base.Link(root);

            JoystickConfig joy;

            if (__CurrentConfig == null)
            {
                joy = fmMain.JoyInfos.FirstOrDefault(j => j.ID == __PreMapConfigID) ??
                      fmMain.JoyInfos.FirstOrDefault();
            }
            else
            {
                joy = fmMain.JoyInfos.FirstOrDefault(j => j.ID == __CurrentConfig.ID) ??
                      fmMain.JoyInfos.FirstOrDefault();
            }

            if (joy == null)
                throw new ArgumentException("Wrong joystick ID or no joystick in configuration file");

            InitializePanels(joy);

        }

        #endregion

        #region Data binding
        

        public override void TakeData(int[] data)
        {
#if STM32 && !STM32F429
            foreach (var panel in __Panels)
            {
                panel.TakeData(data);
            }
#endif
        }

#if STM32
        public override void MapVariables(CodeGeneratorContext context)
        {
            base.MapVariables(context);

            foreach (var panel in __Panels)
            {
                panel.MapVariables(context);
            }
        }
#endif

        public override void TakeJoyData(byte[] jdata)
        {
#if STM32F429
            foreach (var panel in __Panels)
            {
                panel.TakeJoyData(jdata);
            }
#endif
        }

        public override void LinkIndex(ref int Counter)
        {
        }

        #endregion

        #region Generators

        public override void Check(CodeGeneratorContext context)
        {
            if(__Panels.Count == 0)
                throw new CompilationCheckException("Joystick info is invalid, no data is present",CompilationCheckException.CompileIteration.PreCheck);

            __CurrentConfig.Check();
        }

        public override void PreGenerate()
        {
            Determined = true;
        }

        public override void GenerateJoystick(CodeGeneratorContext context, STMProgram prog)
        {
            __CurrentConfig.GenerateSTMCode(prog, context.ModelCode);
        }


#if MEGA2560

        public override void GenerateJoyFile(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
                        
        }

        public override void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            foreach (var panel in __Panels)
                panel.GenerateDataMap(context, __H, __CPP);
        }

        public override void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            foreach (var panel in __Panels)
                panel.GenerateCalculator(context, __CPP);
        }

        public override void GeneratePreCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            foreach (var panel in __Panels)
                panel.GeneratePreCalculator(context, __CPP);
        }
#endif

        #endregion


    }
}
