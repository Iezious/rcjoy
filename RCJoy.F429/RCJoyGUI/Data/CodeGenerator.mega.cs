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
        private readonly List<JoystickConfig> JoyInfos;
        private readonly ProjectSettings Settings;
        public readonly DesignPanel[] Panels;
        private readonly Func<Action, bool> ExecuteStep;


        public CodeGenerator(ProjectSettings settings, IEnumerable<JoystickConfig> joysticks, DesignPanel[] panels, Func<Action, bool> executeStep)
        {
            Settings = settings;
            JoyInfos = joysticks.ToList();
            Panels = panels;
            ExecuteStep = executeStep;
        }

#if MEGA2560

        private void GenerateDataMapFiles(CodeGeneratorContext context)
        {
            using (TextWriter
                __H = File.CreateText(context.Settings.OutputPath + "\\DataMap.h"),
                __CPP = File.CreateText(context.Settings.OutputPath + "\\DataMap.cpp"))
            {
                __H.WriteLine("#ifndef _DATAMAP_H_");
                __H.WriteLine("#define _DATAMAP_H_");
                __H.WriteLine();
                __H.WriteLine("#include <arduino.h> ");
                __H.WriteLine("#include <EEPROM.h> ");
                __H.WriteLine("#include \"Config.h\" ");
                __H.WriteLine("#include \"LCD.h\" ");
                __H.WriteLine("#include \"Def.h\" ");
                __H.WriteLine("#include \"Joystick.h\" ");
                __H.WriteLine("#include \"XBeeWriter.h\"");
                __H.WriteLine("#include \"ppmGen.h\" ");
                __H.WriteLine("#include \"SerialCommander.h\" ");
                __H.WriteLine();
                __H.WriteLine("extern uint16_t ppm_data[];");
                __H.WriteLine("extern axis_t CalcData[];");


                __CPP.WriteLine("#include \"DataMap.h\" ");
                __CPP.WriteLine();

                JoyInfo.GenerateDataMap(__H, __CPP);
                Settings.GenerateDataMap(__H, __CPP);

                foreach (var panel in Panels)
                    panel.GenerateDataMap(context, __H, __CPP);

                __CPP.WriteLine("uint16_t ppm_data[PPM_CHANNELS];");
                __CPP.WriteLine("axis_t CalcData[DATA_LENGTH];");
                __CPP.WriteLine();
                __CPP.WriteLine("void InitData()");
                __CPP.WriteLine("{");

                foreach (var panel in Panels)
                    panel.GenerateInit(context, __CPP);

                __CPP.WriteLine("}");
                __CPP.WriteLine();



                __CPP.WriteLine("void Calculate()");
                __CPP.WriteLine("{");
                __CPP.WriteLine("axis_t p0, p1, p2;");

                Settings.GeneratePreCalculator(__CPP);
                JoyInfo.GenerateCalculator(__CPP);

                foreach (var panel in Panels)
                    panel.GeneratePreCalculator(context, __CPP);

                foreach (var panel in Panels)
                    panel.GenerateCalculator(context, __CPP);

                Settings.GeneratePostCalculator(__CPP);

                __CPP.WriteLine("}");

                __H.WriteLine("extern void InitData();");
                __H.WriteLine("extern void Calculate();");

                __H.WriteLine("#endif");
            }
        }

        private void GenerateConfig(CodeGeneratorContext context)
        {
            using (TextWriter __H = File.CreateText(context.Settings.OutputPath + "\\Config.h"))
            {
                __H.WriteLine("#ifndef _CONFIG_H_");
                __H.WriteLine("#define _CONFIG_H_");
                __H.WriteLine();

                Settings.GenerateDefines(context, __H);

                __H.WriteLine("#endif");
            }
        }

        private void GenerateJoystickFiles(CodeGeneratorContext context)
        {
            using (TextWriter
                __H = File.CreateText(context.Settings.OutputPath + "\\Joystick.h"),
                __CPP = File.CreateText(context.Settings.OutputPath + "\\Joystick.cpp"))
            {
                __H.WriteLine("#ifndef _JOYSTICK_H_");
                __H.WriteLine("#define _JOYSTICK_H_");
                __H.WriteLine();
                __H.WriteLine("#define RPT_JOY_LEN {0}", JoyInfo.Bytes());
                __H.WriteLine();

                __H.WriteLine("#include <inttypes.h>");
                __H.WriteLine("#include \"functions.h\"");
                __H.WriteLine("#include \"Config.h\" ");
                __H.WriteLine("#include \"Def.h\" ");

                __CPP.WriteLine("#include \"Joystick.h\" ");
                __CPP.WriteLine("#include \"Config.h\" ");
                __CPP.WriteLine("#include \"Def.h\" ");
                __CPP.WriteLine();
                __CPP.WriteLine("JoyData JoyInput;");
                __CPP.WriteLine();

                JoyInfo.GenerateJoyFile(__H, __CPP);

                foreach (var panel in Panels)
                    panel.GenerateJoyFile(context, __H, __CPP);

                __H.WriteLine("#endif");
            }
        }
#endif

        private void CheckPanels(CodeGeneratorContext context)
        {
            foreach (var panel in Panels)
                panel.Check(context);
        }

        private void PreparePanelsData(CodeGeneratorContext context)
        {
            JoyInfos.ForEach( j => j.PreGenerate());
            foreach (var panel in Panels)
            {
                panel.PreGenerate();
            }

            int Counter = 0;
            JoyInfos.ForEach( j=> j.LinkIndex(ref Counter));

            foreach (var panel in Panels)
            {
                panel.LinkIndex(ref Counter);
            }

            context.FieldCounter = Counter;

            var i = 0;

            foreach (var panel in Panels)
            {
                panel.MapVariables(i, context);
                i++;
            }

            foreach (var panel in Panels)
            {
                panel.PrepareChains(context);
            }
        }
        public bool GenerateProject()
        {
            var context = new CodeGeneratorContext {Settings = Settings};

            if (!ExecuteStep(FRAMMapper.Check)) return false;
            if (!ExecuteStep(() => CheckPanels(context))) return false;
            if (!ExecuteStep(() => PreparePanelsData(context))) return false;

#if MEGA2560

            if (!ExecuteStep(() => GenerateJoystickFiles(context))) return false;
            if (!ExecuteStep(() => GenerateDataMapFiles(context))) return false;

            if (!ExecuteStep(() => GenerateConfig(context))) return false;
#endif

#if STM32
            
            if(!ExecuteStep( () => PrepareProgram(context))) return false;
            if(!ExecuteStep( () => GenerateCode(context))) return false;
            if(!ExecuteStep( () => SaveProgram(context))) return false;
#endif
            return true;
        }

    }
}
