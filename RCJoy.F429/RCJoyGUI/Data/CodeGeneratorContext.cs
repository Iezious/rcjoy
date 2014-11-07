using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tahorg.RCJoyGUI.Data
{
    public class CodeGeneratorContext
    {
        private readonly Dictionary<uint, string> __EEPMap = new Dictionary<uint, string>(127);

        public ModelInfo CurrentModel { get; set; }
        public int FieldCounter { get; set; }
        public ProjectSettings Settings { get; set; }
//        public JoystickConfig Joystick { get; set; }
        public int PPMArrayLength { get; set; }
        public STMCodeBlock ModelCode { get; set; }

        public void RegiterEEPVariable(uint Address, string Name)
        {
            string currentvar;

            if (__EEPMap.TryGetValue(Address, out currentvar) && currentvar != Name)
            {
                throw new CompilationCheckException ("EEP address " + Address + " is already in use", CompilationCheckException.CompileIteration.EEPMaping);
            }

            __EEPMap[Address] = Name;
        }
    }

}
