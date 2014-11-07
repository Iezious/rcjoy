using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly List<BaseJoystickValuePanel> __Panels = new List<BaseJoystickValuePanel>(32);

        public void InitializePanels(JoystickConfig config)
        {
            Title = config.Name;

            __CurrentConfig = config;

            for (int i = 0; i < __CurrentConfig.Controls.Count; i++)
            {
                var control = __CurrentConfig.Controls[i];

                if (control is JoystickAxle)
                {
                    AddAxle((JoystickAxle) control, i);
                }
                else if (control is ButtonsCollection)
                {
                    AddButtons((ButtonsCollection)control, i);
                }
                else if (control is HatSwitch)
                {
                    AddHat((HatSwitch)control, i);
                }
            }
        }

        private void AddPanel(BaseJoystickValuePanel panel)
        {
            int top = __Panels.Count == 0
                          ? labelHead.Bottom + 2
                          : __Panels.Last().Bottom + 2;

            panel.Top = top;
            panel.Left = 3;
            panel.Width = Width - 3;
            panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            __Panels.Add(panel);
            Controls.Add(panel);
        }

        private void AddAxle(JoystickAxle control, int idx)
        {
            var panel = new AxlePanel();
            panel.SetInfo(control, idx);
            AddPanel(panel);

            RegiseterLink(panel.GetLink());
        }

        
        private void AddButtons(ButtonsCollection control, int idx)
        {
            for (int i = 0; i < control.ButtonsCount; i++)
            {
                var panel = new ButtonPanel();
                panel.SetInfo(control, i + 1, idx);
                AddPanel(panel);

                RegiseterLink(panel.GetLink());
            }
        }

        private void AddHat(HatSwitch control, int idx)
        {
            var panel = new HatPanel();
            panel.SetInfo(control, idx);
            AddPanel(panel);

            RegiseterLink(panel.GetLink());
        }

        #region Save

        public override XElement Serialize()
        {
            return new XElement("Joystick", 
                new XAttribute("Top", Top), 
                new XAttribute("Left", Left),
                new XAttribute("ID", ID)
            );
        }

        public override void Deserialize(XElement data)
        {
            Top = int.Parse(data.AttributeValue("Top") ?? "20");
            Left = int.Parse(data.AttributeValue("Left") ?? "20");
            ID = Guid.Parse(data.AttributeValue("ID") ?? Guid.NewGuid().ToString());
        }

        #endregion

        #region Data binding
        

        public override void TakeData(int[] data)
        {
#if STM32
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

        public override void TakeJoyData(int[] jdata)
        {
#if MEGA2560
            foreach (var panel in __Panels)
            {
                panel.TakeData(jdata);
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
        }

        public override void PreGenerate()
        {
            Determined = true;
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
