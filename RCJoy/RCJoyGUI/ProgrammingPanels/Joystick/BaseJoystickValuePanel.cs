using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI.ProgrammingPanels.Joystick
{
    public partial class BaseJoystickValuePanel : UserControl, ICalculationDataViewer
    {
        public BaseJoystickValuePanel()
        {
            InitializeComponent();
        }

        public virtual LinkPoint GetLink()
        {
            throw new NotImplementedException();
        }

        public virtual void Link(ILinkResolver root)
        {
            throw new NotImplementedException();
        }

        public virtual void LinkIndex(ref int IndexCounter)
        {
        }

        public virtual void TakeData(int[] data)
        {
        }

        public virtual void FillSerialization(XElement xlinks)
        {
            
        }

        public virtual void ReadSerialization(XAttribute att)
        {
            
        }

#if MEGA2560
        public virtual void GenerateDataMap(CodeGeneratorContext context, TextWriter __H, TextWriter __CPP)
        {
            
        }

        public virtual void GenerateCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            
        }

        public virtual void  GeneratePreCalculator(CodeGeneratorContext context, TextWriter __CPP)
        {
            
        }
#endif

        public virtual void MapVariables(CodeGeneratorContext context)
        {
            
        }
    }
}
