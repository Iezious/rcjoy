using System;
using System.Collections.Generic;
using System.IO;
using Tahorg.RCJoyGUI.STM;

namespace Tahorg.RCJoyGUI
{
    public class PositionCountList<T> : List<T>
    {
        public int Append(T item)
        {
            var z = Count;
            Add(item);
            return z;
        }
    }

    [Flags]
    public enum STMProgramSettings : byte
    {
        NONE        = 0x00,
        LCD_ON      = 0x01,
        PPM_ON      = 0x02,
        UART_ON     = 0x04,
        XBEE_ON     = 0x08,
        EEPROM_ON   = 0x10,
        LCDBL_ON    = 0x20,
        NRF_ON      = 0x40
    }

    public enum EEPRomADDRStype : byte
    {
        TwoByteAddr = 0,
        OneByteAddr = 1
    }

    public class STMStringConstant
    {
        
    }

    public class STMProgram
    {
        public uint MEMORYBASE = 0x080E0000;

        public byte JoyReportLength { get; set; }
        public byte PPMLength { get; set; }
        public STMProgramSettings Settins { get; set; }

        private byte __LCD_I2C_ADDR = 0x27;
        private byte __EEP_I2C_ADDR = 0x50;
        private EEPRomADDRStype __EPP_ADDR_STYPE = EEPRomADDRStype.OneByteAddr;

        public byte LCD_I2C_Addr { get { return __LCD_I2C_ADDR; } set { __LCD_I2C_ADDR = value; }}
        public byte EEPROM_I2C_ADDR { get { return __EEP_I2C_ADDR; } set { __EEP_I2C_ADDR = value; }}
        public EEPRomADDRStype EEP_ADDR_STYLE { get { return __EPP_ADDR_STYPE; } set { __EPP_ADDR_STYPE = value; }}

        public ushort DataMapLength { get; set; }
        public readonly STMStringsArray StringConstants = new STMStringsArray();
        public readonly STMModelsArray Models = new STMModelsArray();
        public readonly STMVariablesArray Variables = new STMVariablesArray();

        public readonly STMCodeBlock CommonCode = new STMCodeBlock();
        public readonly STMCodeBlock StartupCode = new STMCodeBlock();
        public readonly STMCodeBlock MenuCode = new STMCodeBlock();

        public static void Align32Bits(ref uint pointer)
        {
            var mod = pointer%4;
            pointer += mod == 0 ? 0 : (4 - mod);
        }

        public void Map(ref uint pointer)
        {
            pointer += 4;
            pointer += 4;
            pointer += 4*2;
            pointer += 6*4;

            StringConstants.Remap(ref pointer);
            Models.Remap(ref pointer);
            Variables.Remap(ref pointer);
            CommonCode.Remap(ref pointer);
            StartupCode.Remap(ref pointer);
            MenuCode.Remap(ref pointer);
        }

        public byte[] Compile()
        {
            uint pointer = MEMORYBASE;
            Map(ref pointer);
            Align32Bits(ref pointer);

            var writer = new STMMemoryWriter(MEMORYBASE, pointer - MEMORYBASE + 4);
            writer.Write(MEMORYBASE, 0xCA);
            writer.Write(MEMORYBASE + 1, JoyReportLength);
            writer.Write(MEMORYBASE + 2, PPMLength);
            writer.Write(MEMORYBASE + 3, (byte)Settins);

            writer.Write(MEMORYBASE + 4, LCD_I2C_Addr);
            writer.Write(MEMORYBASE + 5, EEPROM_I2C_ADDR);
            writer.Write(MEMORYBASE + 6, (byte)EEP_ADDR_STYLE);
//            __out.WriteByte(0); // 7

            writer.Write(MEMORYBASE + 8, DataMapLength);
            writer.Write(MEMORYBASE + 10, (ushort)StringConstants.List.Count + 1);
            writer.Write(MEMORYBASE + 12, (ushort)Models.Models.Count);
            writer.Write(MEMORYBASE + 14, (ushort)Variables.Variables.Count);

            writer.Write(MEMORYBASE + 16, StringConstants.ArrayAddr);
            writer.Write(MEMORYBASE + 20, Models.ArrayAddr);
            writer.Write(MEMORYBASE + 24, Variables.ArrayAddr);

            writer.Write(MEMORYBASE + 28, CommonCode.BlockStart);
            writer.Write(MEMORYBASE + 32, StartupCode.BlockStart);
            writer.Write(MEMORYBASE + 36, MenuCode.BlockStart);

            StringConstants.WriteTo(writer);
            Models.WriteTo(writer);
            Variables.WriteTo(writer);

            CommonCode.WriteTo(writer);
            StartupCode.WriteTo(writer);
            MenuCode.WriteTo(writer);

            return writer.Buffer;
        }
    }
}
