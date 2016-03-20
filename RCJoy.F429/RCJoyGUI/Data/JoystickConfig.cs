using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Tahorg.RCJoyGUI.Data
{
    public interface IJoystickControl
    {
        string Name { get; set; }

        XElement Serialize();
        //        void Deserialize(XElement data);

        void Check();
        int Bits();

        void PreGenerate();

#if MEGA2560
        void GenerateJoysticReport(TextWriter __out);
        void GenerateJoysticType(TextWriter __out);
        void GenerateConvertor(TextWriter __out);
        void GenerateCodeConstants(TextWriter __out);
        void GenerateCalculator(TextWriter __out);
#endif
        void LinkIndex(ref int indexCounter);

#if STM32
        void GenerateSTMCode(STMProgram prg, STMCodeBlock codeBlock, ref ushort BlockStart);
#endif
    }

    public class JoystickAxle : IJoystickControl
    {
        private int _DataIndex = -1;

        public string Name { get; set; }
        public string CName { get; set; }
        public int Length { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public int DataIndex { get { return _DataIndex; } }

        public JoystickAxle()
        {
        }

        public JoystickAxle(XElement xElement)
        {
            Deserialize(xElement);
        }

        public int Bits()
        {
            return Length;
        }

        #region Save/Read

        public XElement Serialize()
        {
            return new XElement("Axle",
                    new XAttribute("Name", Name),
                    new XAttribute("CName", CName),
                    new XAttribute("Length", Length),
                    new XAttribute("MinValue", MinValue),
                    new XAttribute("DataIndex", _DataIndex),
                    new XAttribute("MaxValue", MaxValue)
            );
        }

        public void Check()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new CompilationCheckException("Joystick check fails, axis name is not set", CompilationCheckException.CompileIteration.PreCheck);

            if (string.IsNullOrWhiteSpace(CName))
                throw new CompilationCheckException("Joystick check fails, axis cname is not set", CompilationCheckException.CompileIteration.PreCheck);

            if (Length == 0)
                throw new CompilationCheckException("Joystick check fails, wrong axis length", CompilationCheckException.CompileIteration.PreCheck);

//            if (MinValue == 0)
//                throw new CompilationCheckException("Joystick check fails, wrong axis min value", CompilationCheckException.CompileIteration.PreCheck);

            if (MaxValue <= MinValue)
                throw new CompilationCheckException("Joystick check fails, wrong axis max walue", CompilationCheckException.CompileIteration.PreCheck);
        }

        private void Deserialize(XElement data)
        {
            Name = data.AttributeValue("Name");
            CName = data.AttributeValue("CName");
            Length = int.Parse(data.AttributeValue("Length"));
            MinValue = int.Parse(data.AttributeValue("MinValue"));
            MaxValue = int.Parse(data.AttributeValue("MaxValue"));
            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
        }

        public void LinkIndex(ref int indexCounter)
        {
            _DataIndex = indexCounter;
#if STM32
            indexCounter+=2;
#else
            indexCounter++;
#endif
        }

        #endregion

        #region Generators
        
        public void PreGenerate()
        {

        }

#if MEGA2560


        public void GenerateJoysticReport(TextWriter __out)
        {
            __out.WriteLine("\t\t\t uint16_t {0} : {1};", CName, Length);
        }

        public void GenerateJoysticType(TextWriter __out)
        {
            //__out.WriteLine("\t axis_t {0};", CName);
            __out.WriteLine("\t uint16_t {0};", CName);
        }

        public void GenerateConvertor(TextWriter __out)
        {
            __out.WriteLine("\t JoyInput.{0} = report->{0};", CName);
        }

        public void GenerateCodeConstants(TextWriter __out)
        {
            __out.WriteLine("#define INPUT_AXLE_{0} {1}", CName, DataIndex);
        }

        public void GenerateCalculator(TextWriter __out)
        {
            __out.WriteLine("map_axe(JoyInput.{0}, {1}, {2}, INPUT_AXLE_{0});", CName, MinValue, MaxValue);
        }

        public string GetIdxConstant()
        {
            return string.Format("INPUT_AXLE_{0}", CName);
        }
#endif

#if STM32
        public void GenerateSTMCode(STMProgram prg, STMCodeBlock codeBlock, ref ushort BlockStart)
        {
            // Extract joystick axis
            codeBlock.Add(
                    0x01, (short)BlockStart, (short)Length, (short)_DataIndex
                );

            BlockStart += (ushort)Length;

            // map joy axis value to internal
            codeBlock.Add(
                    0x04, (short)_DataIndex, (short)MinValue, (short)MaxValue, (short)(_DataIndex+1)
                );
        }
#endif

        #endregion
    }

    public class ButtonsCollection : IJoystickControl
    {
        private int _DataIndex = -1;
        public string Name { get; set; }
        public string ConstantName { get; set; }
        public int ButtonsCount { get; set; }
        public int ButtonsStateBits { get; set; }
        public int DataIndex { get { return _DataIndex; } }

        public ButtonsCollection()
        {
            ButtonsStateBits = 1;
        }

        public ButtonsCollection(XElement xElement)
        {
            Deserialize(xElement);
        }

        public int Bits()
        {
            return ButtonsStateBits * ButtonsCount;
        }

        #region Save/Read

        public XElement Serialize()
        {
            return new XElement("Buttons",
                                new XAttribute("Name", Name),
                                new XAttribute("ConstantName", ConstantName),
                                new XAttribute("DataIndex", _DataIndex),
                                new XAttribute("ButtonsCount", ButtonsCount),
                                new XAttribute("ButtonsStateBits", ButtonsStateBits)

                );
        }

        private void Deserialize(XElement data)
        {
            Name = data.AttributeValue("Name");
            ConstantName = data.AttributeValue("ConstantName");
            ButtonsCount = int.Parse(data.AttributeValue("ButtonsCount"));
            ButtonsStateBits = int.Parse(data.AttributeValue("ButtonsStateBits"));
            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");

        }

        public void Check()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new CompilationCheckException("Joystick check fails, buttons name is not set", CompilationCheckException.CompileIteration.PreCheck);

            if (string.IsNullOrWhiteSpace(ConstantName))
                throw new CompilationCheckException("Joystick check fails, buttons cname is not set", CompilationCheckException.CompileIteration.PreCheck);

            if (ButtonsCount == 0)
                throw new CompilationCheckException("Joystick check fails, buttons count is not set", CompilationCheckException.CompileIteration.PreCheck);

        }

        public void LinkIndex(ref int indexCounter)
        {
            _DataIndex = indexCounter;
#if STM32
            indexCounter += ButtonsCount;
#else
            indexCounter++;
#endif
        }

        #endregion

        #region Generators

        public void PreGenerate()
        {

        }

#if MEGA2560




        public void GenerateJoysticReport(TextWriter __out)
        {
            if (Bits() <= 8)
                __out.WriteLine("\t\t\t uint8_t {0} : {1};", ConstantName, Bits());
            else if (Bits() <= 16)
                __out.WriteLine("\t\t\t uint16_t {0} : {1};", ConstantName, Bits());
            else if (Bits() <= 32)
                __out.WriteLine("\t\t\t uint32_t {0} : {1};", ConstantName, Bits());
            else
            {
                throw new InvalidDataException("Too long button field");
            }
        }

        public void GenerateJoysticType(TextWriter __out)
        {
            if (Bits() <= 8)
            {
                __out.WriteLine("\t uint8_t {0};", ConstantName);
                __out.WriteLine("\t uint8_t {0}_Change;", ConstantName);
            }
            else if (Bits() <= 16)
            {
                __out.WriteLine("\t uint16_t {0};", ConstantName);
                __out.WriteLine("\t uint16_t {0}_Change;", ConstantName);

            }
            else if (Bits() <= 32)
            {
                __out.WriteLine("\t uint32_t {0};", ConstantName);
                __out.WriteLine("\t uint32_t {0}_Change;", ConstantName);

            }
            else
            {
                throw new InvalidDataException("Too long button field");
            }
        }

        public void GenerateCodeConstants(TextWriter __out)
        {
            uint stateMask = 0;
            int i;

            for (i = 0; i < ButtonsStateBits; i++)
            {
                stateMask |= (uint)1 << i;
            }

            for (i = 0; i < ButtonsCount; i++)
            {
                uint mask = stateMask << (i * ButtonsStateBits);

                __out.WriteLine("#define {0}_{1}_DOWN JoyInput.{0} & 0x{2:X}", ConstantName, i + 1, mask);
                __out.WriteLine("#define {0}_{1}_PRESSED (JoyInput.{0} & 0x{2:X}) && (JoyInput.{0}_Change & 0x{2:x})", ConstantName, i + 1, mask);
            }
        }

        public void GenerateConvertor(TextWriter __out)
        {
            __out.WriteLine("\t JoyInput.{0}_Change = JoyInput.{0} ^ report->{0};", ConstantName);
            __out.WriteLine("\t JoyInput.{0} = report->{0};", ConstantName);
        }

        public void GenerateCalculator(TextWriter __out)
        {

        }

        public string GetDownConstant(int idx)
        {
            return string.Format("{0}_{1}_DOWN", ConstantName, idx);
        }

        public string GetPressedConstant(int idx)
        {
            return string.Format("{0}_{1}_PRESSED", ConstantName, idx);
        }
#endif

#if STM32
        public void GenerateSTMCode(STMProgram prg, STMCodeBlock codeBlock, ref ushort BlockStart)
        {
            for (var i = 0; i < ButtonsCount; i++)
            {
                // extract each button with separate command
                codeBlock.Add(0x02, (short) (BlockStart), (short)(_DataIndex + i));
                BlockStart+=(ushort)ButtonsStateBits;
            }
        }
#endif
        #endregion
    }

    public class HatSwitch : IJoystickControl
    {
        private int _DataIndex = -1;

        public string Name { get; set; }
        public string ConstantName { get; set; }
        public int Length { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int DataIndex { get { return _DataIndex; } }

        public HatSwitch()
        {
            Length = 4;
            MinValue = 0;
            MaxValue = 8;
        }

        public HatSwitch(XElement data)
        {
            Deserialize(data);
        }

        #region Save/Read

        public XElement Serialize()
        {
            return new XElement("HatSwitch",
                    new XAttribute("Name", Name),
                    new XAttribute("ConstantName", ConstantName),
                    new XAttribute("DataIndex", _DataIndex),
                    new XAttribute("Length", Length),
                    new XAttribute("MinValue", MinValue),
                    new XAttribute("MaxValue", MaxValue)
            );
        }

        public void Check()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new CompilationCheckException("Joystick check fails, hat name is not set", CompilationCheckException.CompileIteration.PreCheck);

            if (string.IsNullOrWhiteSpace(ConstantName))
                throw new CompilationCheckException("Joystick check fails, hat cname is not set", CompilationCheckException.CompileIteration.PreCheck);

            if (Length == 0)
                throw new CompilationCheckException("Joystick check fails, hat length is not set", CompilationCheckException.CompileIteration.PreCheck);


//            if (MinValue == 0)
//                throw new CompilationCheckException("Joystick check fails, wrong hat min value", CompilationCheckException.CompileIteration.PreCheck);

            if (MaxValue <= MinValue)
                throw new CompilationCheckException("Joystick check fails, wrong hat max walue", CompilationCheckException.CompileIteration.PreCheck);
        }


        private void Deserialize(XElement data)
        {
            Name = data.Attribute("Name").Value;
            ConstantName = data.Attribute("ConstantName").Value;
            Length = int.Parse(data.Attribute("Length").Value);
            MinValue = int.Parse(data.Attribute("MinValue").Value);
            MaxValue = int.Parse(data.Attribute("MaxValue").Value);
            _DataIndex = int.Parse(data.AttributeValue("DataIndex") ?? "-1");
        }


        #endregion

        #region Generators

        public int Bits()
        {
            return Length;
        }

        public void LinkIndex(ref int indexCounter)
        {
            _DataIndex = indexCounter;
            indexCounter += 2;
        }

        public void PreGenerate()
        {

        }

#if MEGA2560



        public void GenerateJoysticReport(TextWriter __out)
        {
            if (Bits() <= 8)
                __out.WriteLine("\t\t\t uint8_t {0} : {1};", ConstantName, Bits());
            else if (Bits() <= 16)
                __out.WriteLine("\t\t\t uint16_t {0} : {1};", ConstantName, Bits());
            else if (Bits() <= 32)
                __out.WriteLine("\t\t\t uint32_t {0} : {1};", ConstantName, Bits());
            else
            {
                throw new InvalidDataException("Too long button field");
            }
        }

        public void GenerateJoysticType(TextWriter __out)
        {
            if (Bits() <= 8)
            {
                __out.WriteLine("\t uint8_t {0};", ConstantName);
                __out.WriteLine("\t uint8_t {0}_Change;", ConstantName);
            }
            else if (Bits() <= 16)
            {
                __out.WriteLine("\t uint16_t {0};", ConstantName);
                __out.WriteLine("\t uint16_t {0}_Change;", ConstantName);

            }
            else if (Bits() <= 32)
            {
                __out.WriteLine("\t uint32_t {0};", ConstantName);
                __out.WriteLine("\t uint32_t {0}_Change;", ConstantName);

            }
            else
            {
                throw new InvalidDataException("Too long button field");
            }
        }

        public void GenerateCodeConstants(TextWriter __out)
        {
            __out.WriteLine("#define {0}_VAL JoyInput.{0}", ConstantName);
            __out.WriteLine("#define {0}_CHANGED JoyInput.{0}_Change", ConstantName);

            __out.WriteLine("#define INPUT_HAT_{0} {1}", ConstantName, DataIndex);
            __out.WriteLine("#define INPUT_HAT_{0}_CHANGED {1}", ConstantName, DataIndex+1);
        }

        public void GenerateConvertor(TextWriter __out)
        {
            __out.WriteLine("\t JoyInput.{0}_Change = report->{0} != JoyInput.{0};", ConstantName);
            __out.WriteLine("\t JoyInput.{0} = report->{0};", ConstantName);
        }


        public void GenerateCalculator(TextWriter __out)
        {
            __out.WriteLine("set_val(INPUT_HAT_{0}, JoyInput.{0});", ConstantName);
            __out.WriteLine("set_val(INPUT_HAT_{0}_CHANGED, JoyInput.{0}_Change);", ConstantName);
        }

        public string GetValueConstant()
        {
            return string.Format("INPUT_HAT_{0}", ConstantName);
        }

        public string GetChangedConstant()
        {
            return string.Format(" INPUT_HAT_{0}_CHANGED", ConstantName);
        }
#endif
#if STM32
        public void GenerateSTMCode(STMProgram prg, STMCodeBlock codeBlock, ref ushort BlockStart)
        {
            codeBlock.Add(0x03, (short)BlockStart, (short)Length, (short)_DataIndex, (short)(_DataIndex+1));
            BlockStart += (ushort) Bits();
        }
#endif

        #endregion
    }

    public class LevelingBits : IJoystickControl
    {
        public string Name { get; set; }
        public int Length { get; set; }

        public LevelingBits()
        {
            Name = "Empty";
        }

        public LevelingBits(XElement data)
        {
            Deserialize(data);
        }

        public int Bits()
        {
            return Length;
        }

        #region Save/Read

        public XElement Serialize()
        {
            return new XElement("Leveler",
                    new XAttribute("Name", Name),
                    new XAttribute("Length", Length)
            );
        }

        public void Check()
        {

            if (Length == 0)
                throw new CompilationCheckException("Joystick check fails, leveling bits length is not set", CompilationCheckException.CompileIteration.PreCheck);
        }

        private void Deserialize(XElement data)
        {
            Name = data.Attribute("Name").Value;
            Length = int.Parse(data.Attribute("Length").Value);
        }

        public void LinkIndex(ref int indexCounter)
        {

        }

        #endregion

        #region Generators

//        private static int Counter;
        public void PreGenerate()
        {
//            Counter = 0;
        }

#if MEGA2560



        public void GenerateJoysticReport(TextWriter __out)
        {
            if (Bits() <= 8)
                __out.WriteLine("\t\t\t uint8_t {0}_{2} : {1};", Name, Bits(), Counter);
            else if (Bits() <= 16)
                __out.WriteLine("\t\t\t uint16_t {0}_{2} : {1};", Name, Bits(), Counter);
            else if (Bits() <= 32)
                __out.WriteLine("\t\t\t uint32_t {0}_{2} : {1};", Name, Bits(), Counter);
            else
            {
                throw new InvalidDataException("Too long button field");
            }

            Counter++;
        }

        public void GenerateJoysticType(TextWriter __out)
        {

        }

        public void GenerateCodeConstants(TextWriter __out)
        {

        }

        public void GenerateConvertor(TextWriter __out)
        {

        }

        public void GenerateCalculator(TextWriter __out)
        {
            
        }
#endif

#if STM32
        public void GenerateSTMCode(STMProgram prg, STMCodeBlock codeBlock, ref ushort BlockStart)
        {
            BlockStart += (ushort) Length;
        }
#endif
        #endregion
    }

    public class JoystickConfig
    {
        public Guid ID { get; set; }

        private readonly List<IJoystickControl> __Controls = new List<IJoystickControl>();

        public string Name { get; set; }

        public string Code { get; set; }

        public UInt16 VendorID { get; set; }

        public UInt16 ProductID { get; set; }

        public string ReportStructure { get; set; }

        public JoystickConfig()
        {
            Code = "JOYSTICK";
        }

        public JoystickConfig(XElement data)
        {
            Deserialize(data);
        }

        public static IEnumerable<JoystickConfig> LoadFromXML(XDocument file)
        {
            if (file == null || file.Root == null) return null;
            return file.Root.Elements("Joystick").Select(xj => new JoystickConfig(xj));
        }

        public List<IJoystickControl> Controls { get { return __Controls; } }

        public XElement Serialize()
        {
            var xel = new XElement("Joystick", 
                new XAttribute("Name", Name), 
                new XAttribute("Code", Code),
                new XAttribute("VendorID", VendorID.ToString("X4")),
                new XAttribute("ProductID", ProductID.ToString("X4")),
                new XAttribute("ReportStructure", ReportStructure??""),
                new XAttribute("ID", ID));

            foreach (var joystickControl in __Controls)
                xel.Add(joystickControl.Serialize());

            return xel;
        }

        public void Deserialize(XElement data)
        {
            Name = data.Attribute("Name").Value;
            Code = data.Attribute("Code").Value;
            VendorID = UInt16.Parse(data.AttributeValue("VendorID") ?? "0", NumberStyles.HexNumber);
            ProductID = UInt16.Parse(data.AttributeValue("ProductID") ?? "0", NumberStyles.HexNumber);
            ReportStructure = data.AttributeValue("ReportStructure");
            ID = Guid.Parse(data.AttributeValue("ID") ?? Guid.NewGuid().ToString());

            foreach (var xElement in data.Elements())
            {
                switch (xElement.Name.LocalName)
                {
                    case "Axle":
                        __Controls.Add(new JoystickAxle(xElement));
                        break;

                    case "Buttons":
                        __Controls.Add(new ButtonsCollection(xElement));
                        break;

                    case "HatSwitch":
                        __Controls.Add(new HatSwitch(xElement));
                        break;

                    case "Leveler":
                        __Controls.Add(new LevelingBits(xElement));
                        break;
                }
            }
        }

        public int Bits()
        {
            return Controls.Sum(control => control.Bits());
        }

        public int Bytes()
        {
            return Bits() / 8;
        }

        #region Generator

        public void PreGenerate()
        {
            foreach (var control in __Controls)
            {
                control.PreGenerate();
            }
        }

        public void LinkIndex(ref int IndexCounter)
        {
            foreach (var control in __Controls)
            {
                control.LinkIndex(ref IndexCounter);
            }
        }

#if MEGA2560

        private void GenerateReportClass(TextWriter __H)
        {
            __H.WriteLine("struct JoyUSBData");
            __H.WriteLine("{");
            __H.WriteLine("\t union {");

            __H.WriteLine("\t\t uint8_t raw_data[RPT_JOY_LEN];");
            __H.WriteLine("\t\t struct {");


            foreach (var control in __Controls)
            {
                control.GenerateJoysticReport(__H);
            }

            __H.WriteLine("\t\t };"); // struct
            __H.WriteLine("\t };"); //union
            __H.WriteLine("};");
        }

        private void GenerateDataClass(TextWriter __H)
        {
            __H.WriteLine("struct JoyData");
            __H.WriteLine("{");

            foreach (var control in __Controls)
            {
                control.GenerateJoysticType(__H);
            }

            __H.WriteLine("};");
        }

        private void GenerateParser(TextWriter __CPP)
        {
            __CPP.WriteLine("void ParseJoyData(const JoyUSBData *report)");
            __CPP.WriteLine("{");

            foreach (var control in __Controls)
            {
                control.GenerateConvertor(__CPP);
            }

            __CPP.WriteLine("}");
        }

        public void GenerateJoyFile(TextWriter __H, TextWriter __CPP)
        {



            __H.WriteLine();
            GenerateReportClass(__H);
            __H.WriteLine();
            GenerateDataClass(__H);
            __H.WriteLine();


            __H.WriteLine("void ParseJoyData(const JoyUSBData *report);");
            __H.WriteLine("extern JoyData JoyInput;");
            __H.WriteLine();


            GenerateParser(__CPP);
        }

        public void GenerateDataMap(TextWriter __H, TextWriter __CPP)
        {
            foreach (var control in __Controls)
                control.GenerateCodeConstants(__H);
        }

        public void GenerateCalculator(TextWriter __CPP)
        {
            foreach (var control in __Controls)
                control.GenerateCalculator(__CPP);
        }
#endif

#if STM32

        public void GenerateSTMCode(STMProgram prg, STMCodeBlock codeBlock)
        {
            ushort BitsCounter = 0;

            if (ProductID != 0 && VendorID != 0)
                codeBlock.Add(0x34,VendorID, ProductID);

            foreach (var control in __Controls)
                control.GenerateSTMCode(prg, codeBlock, ref BitsCounter);

            prg.JoyReportLength = (byte) Bytes();
        }

#endif

        #endregion

        public void Check()
        {
            foreach (var control in __Controls)
            {
                control.Check();
            }
        }

        public int[] Parse(byte[] intdata)
        {
            var bitsarray = new bool[intdata.Length*8];

            for (int i = 0; i < intdata.Length; i++)
            {
                var b = intdata[i];
                int mask = 1;

                for (var k = 0; k < 8; k++)
                {
                    bitsarray[i*8 + k] = (b & mask) != 0;
                    mask = mask << 1;
                }
            }

            var vals = new List<int>();

            foreach (var control in __Controls)
            {
                var cbits = bitsarray.Take(control.Bits()).ToArray();
                bitsarray = bitsarray.Skip(control.Bits()).ToArray();

                var val = 0;
                var mask = 1;

                foreach (var cbit in cbits)
                {
                    if(cbit) val |= mask;
                    mask = mask << 1;
                }

                vals.Add(val);
            }

            return vals.ToArray();
        }
    }
}
