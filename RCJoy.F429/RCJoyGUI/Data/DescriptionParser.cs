using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tahorg.RCJoyGUI.Data
{
    public class DescriptionParser
    {
        private readonly JoystickConfig _Joystick;

        #region USB CONSTANTS

        private const uint DATA_SIZE_MASK = 0x03;
        private const uint TYPE_MASK = 0x0C;
        private const uint TAG_MASK = 0xF0;

        private const uint DATA_SIZE_0 = 0x00;
        private const uint DATA_SIZE_1 = 0x01;
        private const uint DATA_SIZE_2 = 0x02;
        private const uint DATA_SIZE_4 = 0x03;

        private const uint TYPE_MAIN = 0x00;
        private const uint TYPE_GLOBAL = 0x04;
        private const uint TYPE_LOCAL = 0x08;

        private const uint TAG_MAIN_INPUT = 0x80;
        private const uint TAG_MAIN_OUTPUT = 0x90;
        private const uint TAG_MAIN_COLLECTION = 0xA0;
        private const uint TAG_MAIN_FEATURE = 0xB0;
        private const uint TAG_MAIN_ENDCOLLECTION = 0xC0;

        private const uint TAG_GLOBAL_USAGEPAGE = 0x00;
        private const uint TAG_GLOBAL_LOGICALMIN = 0x10;
        private const uint TAG_GLOBAL_LOGICALMAX = 0x20;
        private const uint TAG_GLOBAL_PHYSMIN = 0x30;
        private const uint TAG_GLOBAL_PHYSMAX = 0x40;
        private const uint TAG_GLOBAL_UNITEXP = 0x50;
        private const uint TAG_GLOBAL_UNIT = 0x60;
        private const uint TAG_GLOBAL_REPORTSIZE = 0x70;
        private const uint TAG_GLOBAL_REPORTID = 0x80;
        private const uint TAG_GLOBAL_REPORTCOUNT = 0x90;
        private const uint TAG_GLOBAL_PUSH = 0xA0;
        private const uint TAG_GLOBAL_POP = 0xB0;

        private const uint TAG_LOCAL_USAGE = 0x00;
        private const uint TAG_LOCAL_USAGEMIN = 0x10;
        private const uint TAG_LOCAL_USAGEMAX = 0x20;

        private const uint HID_MAIN_ITEM_COLLECTION_PHYSICAL = 0x00;
        private const uint HID_MAIN_ITEM_COLLECTION_APPLICATION = 0x01;
        private const uint HID_MAIN_ITEM_COLLECTION_LOGICAL = 0x02;
        private const uint HID_MAIN_ITEM_COLLECTION_REPORT = 0x03;
        private const uint HID_MAIN_ITEM_COLLECTION_NAMED_ARRAY = 0x04;
        private const uint HID_MAIN_ITEM_COLLECTION_USAGE_SWITCH = 0x05;
        private const uint HID_MAIN_ITEM_COLLECTION_USAGE_MODIFIER = 0x06;

        private const uint HID_ITEM_TYPE_MAIN = 0x00;
        private const uint HID_ITEM_TYPE_GLOBAL = 0x01;
        private const uint HID_ITEM_TYPE_LOCAL = 0x02;
        private const uint HID_ITEM_TYPE_RESERVED = 0x03;

        private const uint HID_LONG_ITEM_PREFIX = 0xfe;

        private const uint bmHID_MAIN_ITEM_TAG = 0xfc;

        private const uint bmHID_MAIN_ITEM_INPUT = 0x80;
        private const uint bmHID_MAIN_ITEM_OUTPUT = 0x90;
        private const uint bmHID_MAIN_ITEM_FEATURE = 0xb0;
        private const uint bmHID_MAIN_ITEM_COLLECTION = 0xa0;
        private const uint bmHID_MAIN_ITEM_END_COLLECTION = 0xce;

        private const uint GLOBAL_USAGE_GENERIC_CONTROLS = 0x01;
        private const uint GLOBAL_USAGE_SIM_CONTROLS = 0x02;
        private const uint GLOBAL_USAGE_GAME_CONTROLS = 0x05;
        private const uint GLOBAL_USAGE_BUTTON = 0x09;


        private const uint HID_USAGE_POINTER = 0x01;
        private const uint HID_USAGE_MOUSE = 0x02;
        private const uint HID_USAGE_JOYSTICK = 0x04;
        private const uint HID_USAGE_GAMEPAD = 0x05;
        private const uint HID_USAGE_KEYBOARD = 0x06;
        private const uint HID_USAGE_KEYPAD = 0x07;
        private const uint HID_USAGE_MULTIAXIS = 0x08;
        private const uint HID_USAGE_TABLET = 0x09;


        private const uint HID_USAGE_GP_TURN_LR = 0x21;     // Turn Right/Left DV 8.1
        private const uint HID_USAGE_GP_PITCH_FB = 0x22;    // Pitch Forward/Backward DV 8.1
        private const uint HID_USAGE_GP_ROLL_LR = 0x23;     // Roll Right/Left DV 8.1
        private const uint HID_USAGE_GP_MOVE_LR = 0x24;     // Move Right/Left DV 8.1
        private const uint HID_USAGE_GP_MOVE_FB = 0x25;     // Move Forward/Backward DV 8.1
        private const uint HID_USAGE_GP_MOVE_UD = 0x26;     // Move Up/Down DV 8.1
        private const uint HID_USAGE_GP_LEAN_LR = 0x27;     // Lean Right/Left DV 8.1
        private const uint HID_USAGE_GP_LEAN_FB = 0x28;     // Lean Forward/Backward DV 8.1
        private const uint HID_USAGE_GP_HEIGHT = 0x29;      // Height of POV DV


        private const uint HID_USAGE_X = 0x30;
        private const uint HID_USAGE_Y = 0x31;
        private const uint HID_USAGE_Z = 0x32;
        private const uint HID_USAGE_RX = 0x33;
        private const uint HID_USAGE_RY = 0x34;
        private const uint HID_USAGE_RZ = 0x35;
        private const uint HID_USAGE_SLIDER = 0x36;
        private const uint HID_USAGE_DIAL = 0x37;
        private const uint HID_USAGE_WHEEL = 0x38;
        private const uint HID_USAGE_HATSWITCH = 0x39;
        private const uint HID_USAGE_COUNTERBUFFER = 0x3A;
        private const uint HID_USAGE_BYTECOUNT = 0x3B;
        private const uint HID_USAGE_MOTIONWAKEUP = 0x3C;
        private const uint HID_USAGE_START = 0x3D;
        private const uint HID_USAGE_SELECT = 0x3E;

        private const uint HID_USAGE_XV = 0x40;
        private const uint HID_USAGE_VY = 0x41;
        private const uint HID_USAGE_YZ = 0x42;
        private const uint HID_USAGE_VBRX = 0x43;
        private const uint HID_USAGE_VBRY = 0x44;
        private const uint HID_USAGE_VBRZ = 0x45;
        private const uint HID_USAGE_VNO = 0x46;

        private const uint HID_USAGE_AILERON = 0xB0;
        private const uint HID_USAGE_AILERON_TRIM = 0xB1;
        private const uint HID_USAGE_COLLECTIVE= 0xB5;
	    private const uint HID_USAGE_ELEVATOR = 0xB8;
        private const uint HID_USAGE_ELEVATORTRIM = 0xB9;
        private const uint HID_USAGE_RUDDER = 0xBA;
	    private const uint HID_USAGE_THROTTLE = 0xBB;
        private const uint HID_USAGE_GEAR = 0xBE;
	    private const uint HID_USAGE_TOEEBRAKE = 0xBF;
	    private const uint HID_USAGE_ACCELERATOR = 0xC4;
        private const uint HID_USAGE_BRAKE = 0xC5;

        #endregion

        public DescriptionParser(JoystickConfig joy)
        {
            _Joystick = joy;
        }

        private readonly List<uint> __Usages = new List<uint>();
        private uint __Bits;
        private uint __Count;
        private uint __CurrentUsageBlock;
        private bool __EOF = false;
        private uint __ReportID = 0;

        public void Parse(IEnumerator<uint> descr)
        {
            while (descr.MoveNext())
            {
                uint tag = descr.Current;

                uint __ItemType = tag & TYPE_MASK;
                uint __ItemTag = tag & TAG_MASK;
                uint __ItemSize = tag & DATA_SIZE_MASK;

                uint __ItemValue = 0;

                for (int i = 0; i < __ItemSize; i++)
                {
                    if (!descr.MoveNext())
                        throw new ArgumentException("Wrong descriptor structure");

                    __ItemValue = (__ItemValue << 8) | descr.Current;

                }

                ParseCommand(__ItemType, __ItemTag, __ItemValue);
                if(__EOF) return;
            } 

        }

        private void ParseCommand(uint _type, uint _tag, uint _val)
        {
            switch (_type)
            {
                case TYPE_MAIN:
                    ParseMain(_tag, _val);
                    break;

                case TYPE_GLOBAL:
                    ParseGlobal(_tag, _val);
                    break;


                case TYPE_LOCAL:
                    ParseLocal(_tag, _val);
                    break;

                default:
                    throw new ArgumentException("Wrong type in descriptor");

            }
        }

        private uint __CollectionCounter = 0;

        private void ParseMain(uint tag, uint val)
        {
            switch (tag)
            {
                case TAG_MAIN_COLLECTION:
                    __CollectionCounter ++;
                    __Usages.Clear();
                    break;

                case TAG_MAIN_ENDCOLLECTION:
                    __CollectionCounter --;
                    __EOF = __CollectionCounter <= 0;
                    __Usages.Clear();
                    break;

                case TAG_MAIN_INPUT:
                    if (__ReportID == 0 || __ReportID == 1)
                        CreateCollecedInput();

                    break;

                case TAG_MAIN_OUTPUT:
                case TAG_MAIN_FEATURE:
                    break;
            }
        }

        private void ParseGlobal(uint tag, uint val)
        {
            switch (tag)
            {
                case TAG_GLOBAL_USAGEPAGE:

//                    if (val == GLOBAL_USAGE_BUTTON || val == GLOBAL_USAGE_GENERIC_CONTROLS || val == GLOBAL_USAGE_GAME_CONTROLS)
//                    {
                        __CurrentUsageBlock = val;
                        __Usages.Clear();
                        break;
//                    }
//
//                    throw new ArgumentException("Sorry, but usage is not supported yet!");


                case TAG_GLOBAL_LOGICALMIN:
                case TAG_GLOBAL_LOGICALMAX:
                case TAG_GLOBAL_PHYSMIN:
                case TAG_GLOBAL_PHYSMAX:
                case TAG_GLOBAL_UNITEXP:
                case TAG_GLOBAL_UNIT:
                    break;

                case TAG_GLOBAL_REPORTSIZE:
                    __Bits = val;
                    break;

                case TAG_GLOBAL_REPORTID:
                    __ReportID = val;
                    break;
                    

                case TAG_GLOBAL_REPORTCOUNT:
                    __Count = val;
                    break;
            }
        }

        private void ParseLocal(uint tag, uint val)
        {
            switch (tag)
            {
                case TAG_LOCAL_USAGE:
                    __Usages.Add(val);
                    break;

                case TAG_LOCAL_USAGEMIN:
                case TAG_LOCAL_USAGEMAX:
                    break;
            }
        }

        private int buttons_counter = 1;
        private int hats_counter = 1;
        private int lev_counter = 1;
        private int axis_counter = 1;

        private void CreateButtons()
        {
            _Joystick.Controls.Add(new ButtonsCollection
            {
                ButtonsCount = (int)__Count,
                ButtonsStateBits = (int)__Bits,
                Name = "Buttons " + buttons_counter,
                ConstantName = "BUTTONS_" + buttons_counter
            });

            buttons_counter++;
            __Usages.Clear();
            __CurrentUsageBlock = 0;
        }

        private uint GetMaxBits( uint bits)
        {
            var res = 0U;
            for (int i = 0; i < bits; i++) res = (res << 1) | 1;
            return res;
        }

        private void CreateAxle(string name, string cname)
        {
            _Joystick.Controls.Add(new JoystickAxle
            {
                Length = (int) __Bits,
                MinValue = 0,
                MaxValue = (int) GetMaxBits(__Bits),
                Name = name,
                CName = cname
            });
        }

        private void CreateValueGroup(string name, string cname)
        {
            _Joystick.Controls.Add(new HatSwitch
            {
                Length = (int)__Bits,
                MinValue = 0,
                MaxValue = (int)GetMaxBits(__Bits),
                ConstantName = cname,
                Name = name
            });
        }

        private void CreateCollecedInput()
        {
            if (__CurrentUsageBlock == GLOBAL_USAGE_BUTTON)
            {
                CreateButtons();
                return;
            }

            if (__Usages.Count == 0)
            {
                _Joystick.Controls.Add(new LevelingBits
                {
                    Length = (int)(__Bits * __Count),
                    Name = "Pad " + lev_counter
                });

                lev_counter++;
                __CurrentUsageBlock = 0;
                return;
            }


            if (__Bits == 1)
            {
                CreateButtons();
                return;
            }

            foreach (var usage in __Usages)
            {
                switch (usage)
                {
                    case HID_USAGE_X:
                    case HID_USAGE_AILERON:
                        CreateAxle("Ailerons", "AILERONS");
                        break;

                    case HID_USAGE_Y:
                    case HID_USAGE_ELEVATOR:
                        CreateAxle("Elevator", "ELEVATOR");
                        break;

                    case HID_USAGE_Z:
                    case HID_USAGE_THROTTLE:
                        CreateAxle("Throttle", "THROTTLE");
                        break;

                    case HID_USAGE_AILERON_TRIM:
                        CreateValueGroup("Aileron trim", "AILTRIM");
                        break;

                    case HID_USAGE_ELEVATORTRIM:
                        CreateValueGroup("Elevator trim", "ELEVTRIM");
                        break;

                    case HID_USAGE_COLLECTIVE:
                        CreateAxle("Collective", "COLLECTIVE");
                        break;

                    case HID_USAGE_BRAKE:
                        CreateAxle("Brake", "BRAKE");
                        break;

                    case HID_USAGE_TOEEBRAKE:
                        CreateAxle("Toe brake", "BRAKE");
                        break;

                    case HID_USAGE_ACCELERATOR:
                        CreateAxle("Accelerate", "ACCEL");
                        break;

                    case HID_USAGE_RX:
                        CreateAxle("Rx", "RX");
                        break;

                    case HID_USAGE_RY:
                        CreateAxle("Ry", "RY");
                        break;

                    case HID_USAGE_RZ:
                    case HID_USAGE_RUDDER:
                        CreateAxle("Rudder", "TWIST");
                        break;

                    case HID_USAGE_SLIDER:
                        CreateAxle("Slider", "SLIDER");
                        break;

                    case HID_USAGE_DIAL:
                        CreateAxle("Dial", "DIAL");
                        break;

                    case HID_USAGE_WHEEL:
                        CreateAxle("Dial", "DIAL");
                        break;
                
                    case HID_USAGE_GP_TURN_LR:
                        CreateAxle("Turn L/R", "TURN_LR");
                        break;

                    case HID_USAGE_GP_PITCH_FB:
                        CreateAxle("Pitch F/B", "PITCH_FB");
                        break;

                    case HID_USAGE_GP_ROLL_LR:
                        CreateAxle("Roll L/R", "ROLL_LR");
                        break;

                    case HID_USAGE_GP_MOVE_LR:
                        CreateAxle("Move L/R", "MOVE_LR");
                        break;

                    case HID_USAGE_GP_MOVE_FB:
                        CreateAxle("Move F/B", "MOVE_FB");
                        break;

                    case HID_USAGE_GP_MOVE_UD:
                        CreateAxle("Move U/D", "MOVE_UD");
                        break;

                    case HID_USAGE_GP_LEAN_LR:
                        CreateAxle("Lean L/R", "LEAN_LR");
                        break;

                    case HID_USAGE_GP_LEAN_FB:
                        CreateAxle("Lean F/B", "LEAN_FB");
                        break;

                    case HID_USAGE_GP_HEIGHT:
                        CreateAxle("Height", "HEIGHT");
                        break;

                    case HID_USAGE_HATSWITCH:
                        CreateValueGroup("Hat " + hats_counter, "HAT_" + hats_counter);
                        hats_counter++;
                        break;

                    default:
                        CreateAxle("Axis " + axis_counter, "AXIS" + axis_counter);
                        axis_counter++;
                        break;
                        
                }
            }

            __Usages.Clear();

        }


    }
}
