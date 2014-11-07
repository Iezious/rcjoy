using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI
{
    public partial class CodeGenerator
    {
#if STM32
        private STMProgram __prg;
        public byte[] ByteCode { get; private set; }

        private void PrepareProgram(CodeGeneratorContext context)
        {
            __prg = new STMProgram();
            Settings.Generate(__prg);
        }

        private void GenerateCode(CodeGeneratorContext context)
        {
//            JoyInfo.GenerateSTMCode(__prg, __prg.CommonCode);

            foreach (var panel in Panels)
            {
                panel.Generate(context, __prg);
            }
                
        }

        private void SaveProgram(CodeGeneratorContext context)
        {
            using (var binfile = new FileStream(Settings.OutputPath, FileMode.Create, FileAccess.Write))
            {
                __prg.DataMapLength = (ushort) context.FieldCounter;

                ByteCode = __prg.Compile();
                binfile.Write(ByteCode, 0, ByteCode.Length);

                binfile.Flush(true);
                binfile.Close();
            }
        }
#endif

    }
}
