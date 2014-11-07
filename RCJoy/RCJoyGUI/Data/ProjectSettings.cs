using System;
#if MEGA2560
using System.IO;
#endif
using System.Xml.Linq;

namespace Tahorg.RCJoyGUI.Data
{
    public class ProjectSettings
    {

#if MEGA2560
        public enum PPMGenTimer
        {
            Timer1A = 0,
            Timer3A = 1,
            Timer4A = 2,
            Timer5A = 3
        }

        public enum Serial
        {
            Serial0 = 0,
            Serial1 = 1,
            Serial2 = 2,
            Serial3 = 3
        }

        private bool _ppmGenEnabled = true;
        private PPMGenTimer _ppmTimer = PPMGenTimer.Timer5A;
        private bool _lcdEnabled = false;
        private Serial _lcdSerial = Serial.Serial2;
        private bool _debugEnabled = true;
        private bool _reportDescrReadedEnabled = false;
        private Serial _debuggerSerial = Serial.Serial0;
        private Serial _xBeeSerial  = Serial.Serial3;
        private bool _xBeeEnabled = false;


#endif

#if STM32
        public enum enBoardType
        {
            STM32F407VG,
            STM32F407VE
        }

        private bool _ppmGenEnabled = true;

// ReSharper disable RedundantDefaultFieldInitializer
        private bool _lcdEnabled = false;
        private bool _cldbl = true;
        private byte _lcdAddr = 0x27;    // i2c lcd addr
        private bool _lcd8080 = false;

        private bool _romEnabled = true;
        private byte _romAddrStyle = 1;  // 8bit
        private byte _romAddr = 0x50;    // i2c rom addr

        private bool _debugEnabled = true;
        
        private bool _xBeeEnabled = true;
        private bool _nrfEnabled = false;

        private enBoardType __boardType = enBoardType.STM32F407VE;
// ReSharper restore RedundantDefaultFieldInitializer

#endif
        public ProjectSettings()
        {
            
        }

        private ProjectSettings(XElement data)
        {
            Deserialize(data);
        }

#if MEGA2560



        public bool PPMGenEnabled
        {
            get { return _ppmGenEnabled; }
            set { _ppmGenEnabled = value; }
        }

        public PPMGenTimer PPMTimer
        {
            get { return _ppmTimer; }
            set { _ppmTimer = value; }
        }

        public bool LCDEnabled
        {
            get { return _lcdEnabled; }
            set { _lcdEnabled = value; }
        }

        public Serial LCDSerial
        {
            get { return _lcdSerial; }
            set { _lcdSerial = value; }
        }

        public bool DebugEnabled
        {
            get { return _debugEnabled; }
            set { _debugEnabled = value; }
        }

        public bool ReportDescrReadedEnabled
        {
            get { return _reportDescrReadedEnabled; }
            set { _reportDescrReadedEnabled = value; }
        }

        public Serial DebuggerSerial
        {
            get { return _debuggerSerial; }
            set { _debuggerSerial = value; }
        }

        public bool XBeeEnabled
        {
            get { return _xBeeEnabled; }
            set { _xBeeEnabled = value; }
        }

        public Serial XBeeSerial
        {
            get { return _xBeeSerial; }
            set { _xBeeSerial = value; }
        }
#endif

#if STM32

        public bool PPMGenEnabled
        {
            get { return _ppmGenEnabled; }
            set { _ppmGenEnabled = value; }
        }

        public bool LCD_Enabled
        {
            get { return _lcdEnabled; }
            set { _lcdEnabled = value; }
        }

        public bool LCD_BlackLight
        {
            get { return _cldbl; }
            set { _cldbl = value; }
        }

        public byte LCD_I2C_Addr
        {
            get { return _lcdAddr; }
            set { _lcdAddr = value; }
        }

        public bool LCD_8080
        {
            get { return _lcd8080; }
            set { _lcd8080 = value; }
        }

        public bool ROM_Enabled
        {
            get { return _romEnabled; }
            set { _romEnabled = value; }
        }

        public byte ROM_AddrStyle
        {
            get { return _romAddrStyle; }
            set { _romAddrStyle = value; }
        }

        public byte ROM_Addr
        {
            get { return _romAddr; }
            set { _romAddr = value; }
        }

        public bool NRF_Enabled
        {
            get { return _nrfEnabled; }
            set { _nrfEnabled = value; }
        }


        public bool DebugEnabled
        {
            get { return _debugEnabled; }
            set { _debugEnabled = value; }
        }

        public bool XBeeEnabled
        {
            get { return _xBeeEnabled; }
            set { _xBeeEnabled = value; }
        }

        public enBoardType BoardType
        {
            get { return __boardType; }
            set { __boardType = value; }
        }
#endif

        public string OutputPath { get; set; }

#if MEGA2560
        public XElement Serialize()
        {
            return new XElement("Config",
                    new XElement("PPM",
                        new XAttribute("Enabled", _ppmGenEnabled),
                        new XAttribute("Timer", _ppmTimer)
                    ),
                    new XElement("LCD",
                        new XAttribute("Enabled", _lcdEnabled),
                        new XAttribute("Serial", _lcdSerial)
                    ),
                    new XElement("Debug",
                        new XAttribute("Enabled", _debugEnabled),
                        new XAttribute("ReportReaded", _reportDescrReadedEnabled),
                        new XAttribute("Serial", _debuggerSerial)
                    ),
                    new XElement("XBee",
                        new XAttribute("Enabled", _xBeeEnabled),
                        new XAttribute("Serial", _xBeeSerial)
                    ),

                    new XAttribute("Path", OutputPath)
                );
        }

        private void Deserialize(XElement data)
        {
            OutputPath = data.AttributeValue("Path") ?? "";

            var xppm = data.Element("PPM");

            if (xppm != null)
            {
                _ppmGenEnabled = bool.Parse(xppm.AttributeValue("Enabled"));
                _ppmTimer = (PPMGenTimer)Enum.Parse(typeof(PPMGenTimer), xppm.AttributeValue("Timer"), true);
            }

            var xlcd = data.Element("LCD");

            if (xlcd != null)
            {
                _lcdEnabled = bool.Parse(xlcd.AttributeValue("Enabled"));
                _lcdSerial = (Serial)Enum.Parse(typeof(Serial), xlcd.AttributeValue("Serial"), true);
            }

            var xdebug = data.Element("Debug");
            if (xdebug != null)
            {
                _debugEnabled = bool.Parse(xdebug.AttributeValue("Enabled"));
                _reportDescrReadedEnabled = bool.Parse(xdebug.AttributeValue("ReportReaded"));
                _debuggerSerial = (Serial)Enum.Parse(typeof(Serial), xdebug.AttributeValue("Serial"), true);
            }

            var xxbee = data.Element("XBee");
            if (xxbee != null)
            {
                _xBeeEnabled = bool.Parse(xxbee.AttributeValue("Enabled"));
                _xBeeSerial = (Serial)Enum.Parse(typeof(Serial), xxbee.AttributeValue("Serial"), true);
            }
        }
#endif

#if STM32
        public XElement Serialize()
        {
            return new XElement("Config",
                    new XElement("PPM",
                        new XAttribute("Enabled", _ppmGenEnabled)
                    ),

                    new XElement("LCD",
                        new XAttribute("Enabled", _lcdEnabled),
                        new XAttribute("BL", _cldbl),
                        new XAttribute("Addr", _lcdAddr),
                        new XAttribute("I8080", _lcd8080)
                    ),

                    new XElement("Debug",
                        new XAttribute("Enabled", _debugEnabled)
                    ),

                    new XElement("Retranslator",
                        new XAttribute("XBee", _xBeeEnabled),
                        new XAttribute("NRF", _nrfEnabled)
                    ),

                    new XElement("ROM",
                        new XAttribute("Enabled", _romEnabled),
                        new XAttribute("Addr", _romAddr),
                        new XAttribute("AStyle", _romAddrStyle)
                    ),

                    new XAttribute("Path", OutputPath),
                    new XAttribute("BoardType", BoardType.ToString())
                );
        }

        private void Deserialize(XElement data)
        {
            OutputPath = data.AttributeValue("Path") ?? "";
            Enum.TryParse(data.AttributeValue("BoardType"), true, out __boardType);

            var xppm = data.Element("PPM");

            if (xppm != null)
            {
                _ppmGenEnabled = bool.Parse(xppm.AttributeValue("Enabled"));
                
            }

            var xlcd = data.Element("LCD");
            if (xlcd != null)
            {
                bool.TryParse(xlcd.AttributeValue("Enabled"), out _lcdEnabled);
                bool.TryParse(xlcd.AttributeValue("BL"), out _cldbl);
                byte.TryParse(xlcd.AttributeValue("Addr"), out _lcdAddr);
                bool.TryParse(xlcd.AttributeValue("I8080"), out _lcd8080);
            }

            var xrom = data.Element("ROM");
            if (xrom != null)
            {
                bool.TryParse(xrom.AttributeValue("Enabled"), out _romEnabled);
                byte.TryParse(xrom.AttributeValue("Addr"), out _romAddr);
                byte.TryParse(xrom.AttributeValue("AStyle"), out _romAddrStyle);
            }

            var xdebug = data.Element("Debug");
            if (xdebug != null)
            {
                _debugEnabled = bool.Parse(xdebug.AttributeValue("Enabled"));
            }

            var xxbee = data.Element("Retranslator");
            if (xxbee != null)
            {
                bool.TryParse(xxbee.AttributeValue("XBee"), out _xBeeEnabled);
                bool.TryParse(xxbee.AttributeValue("NRF"), out _nrfEnabled);
            }
        }
#endif

        public static ProjectSettings LoadFromXML(XDocument file)
        {
            if (file == null || file.Root == null) return null;

            XElement data = file.Root.Element("Config");
            return data == null ? null : new ProjectSettings(data);
        }

#if MEGA2560
        public void GenerateDefines(CodeGeneratorContext context, TextWriter __H)
        {

            if (PPMGenEnabled)
            {
                __H.WriteLine("#define PPM_GENERATOR");

                switch (_ppmTimer)
                {
                    case PPMGenTimer.Timer1A:
                        __H.WriteLine("#define PPM_TIMER_1A");
                        break;

                    case PPMGenTimer.Timer3A:
                        __H.WriteLine("#define PPM_TIMER_3A");
                        break;
                    case PPMGenTimer.Timer4A:
                        __H.WriteLine("#define PPM_TIMER_4A");
                        break;

                    case PPMGenTimer.Timer5A:
                        __H.WriteLine("#define PPM_TIMER_5A");
                        break;
                }
            }

            if (LCDEnabled)
            {
                __H.WriteLine("#define LCD");

                switch (LCDSerial)
                {
                    case Serial.Serial0:
                        __H.WriteLine("#define LCD_SERIAL Serial");
                        break;
                    case Serial.Serial1:
                        __H.WriteLine("#define LCD_SERIAL Serial1");
                        break;
                    case Serial.Serial2:
                        __H.WriteLine("#define LCD_SERIAL Serial2");
                        break;
                    case Serial.Serial3:
                        __H.WriteLine("#define LCD_SERIAL Serial3");
                        break;
                }
            }

            if (DebugEnabled)
            {
                __H.WriteLine("#define DEBUG");

                switch (DebuggerSerial)
                {
                    case Serial.Serial0:
                        __H.WriteLine("#define DEBUG_SERIAL Serial");
                        __H.WriteLine("#define DEBUG_SERIAL_EVENT serialEvent");
                        break;
                    case Serial.Serial1:
                        __H.WriteLine("#define DEBUG_SERIAL Serial1");
                        __H.WriteLine("#define DEBUG_SERIAL_EVENT serialEvent1");

                        break;
                    case Serial.Serial2:
                        __H.WriteLine("#define DEBUG_SERIAL Serial2");
                        __H.WriteLine("#define DEBUG_SERIAL_EVENT serialEvent2");

                        break;
                    case Serial.Serial3:
                        __H.WriteLine("#define DEBUG_SERIAL Serial3");
                        __H.WriteLine("#define DEBUG_SERIAL_EVENT serialEvent3");

                        break;
                }
            }

            if (XBeeEnabled)
            {
                __H.WriteLine("#define XBEE");

                switch (XBeeSerial)
                {
                    case Serial.Serial0:
                        __H.WriteLine("#define XBEE_SERIAL Serial");
                        __H.WriteLine("#define XBEE_SERIAL_EVENT serialEvent");
                        break;

                    case Serial.Serial1:
                        __H.WriteLine("#define XBEE_SERIAL Serial1");
                        __H.WriteLine("#define XBEE_SERIAL_EVENT serialEvent1");
                        break;

                    case Serial.Serial2:
                        __H.WriteLine("#define XBEE_SERIAL Serial2");
                        __H.WriteLine("#define XBEE_SERIAL_EVENT serialEvent2");
                        break;

                    case Serial.Serial3:
                        __H.WriteLine("#define XBEE_SERIAL Serial3");
                        __H.WriteLine("#define XBEE_SERIAL_EVENT serialEvent3");
                        break;
                }
            }

            if (ReportDescrReadedEnabled)
            {
                __H.WriteLine("#define REPORT_READER");
            }

            __H.WriteLine("#define DATA_LENGTH {0}", context.FieldCounter);
            __H.WriteLine("#define PPM_CHANNELS {0}", context.PPMArrayLength);
        }


        public void GeneratePreCalculator(TextWriter __CPP)
        {

            if (LCDEnabled)
            {
                __CPP.WriteLine("byte lcd_writen=0;");
            }
        }

        public void GeneratePostCalculator(TextWriter __CPP)
        {
            if (DebugEnabled)
            {
                __CPP.WriteLine("#ifdef DEBUG");
                __CPP.WriteLine("WRITE_DEBUG;");
                __CPP.WriteLine("#endif");
            }

            if (XBeeEnabled)
            {
                __CPP.WriteLine("#ifdef XBEE");
                __CPP.WriteLine("XBEEWRITER.send();");
                __CPP.WriteLine("#endif");
            }
        }

        public void GenerateDataMap(TextWriter __H, TextWriter __CPP)
        {
            __H.WriteLine("extern uint8_t CurrentModel;");
            __CPP.WriteLine("uint8_t CurrentModel = 0;");
        }
#endif

#if STM32
        public void Generate(STMProgram prg)
        {
            prg.MEMORYBASE = __boardType == enBoardType.STM32F407VG ? 0x080E0000U : 0x08060000U;
            prg.Settins = STMProgramSettings.NONE;

            if (_lcdEnabled)
            {
                prg.Settins |= STMProgramSettings.LCD_ON;

                if (LCD_BlackLight)
                    prg.Settins |= STMProgramSettings.LCDBL_ON;
                
                prg.LCD_I2C_Addr = (byte)(LCD_8080 ? 0 : _lcdAddr);
            }
            else
            {
                prg.LCD_I2C_Addr = 0;
            }

            if (_romEnabled)
            {
                prg.Settins |=STMProgramSettings.EEPROM_ON;
                prg.EEPROM_I2C_ADDR = _romAddr;
                prg.EEP_ADDR_STYLE = (EEPRomADDRStype) _romAddrStyle;
            }

            if(_debugEnabled)
                prg.Settins |= STMProgramSettings.UART_ON;

            if(_ppmGenEnabled)
                prg.Settins |= STMProgramSettings.PPM_ON;

            if(_xBeeEnabled)
                prg.Settins |= STMProgramSettings.XBEE_ON;

            if(_nrfEnabled)
                prg.Settins |=STMProgramSettings.NRF_ON;
        }
#endif
    }
}
