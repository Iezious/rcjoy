using System;
using System.Collections.Generic;
using System.IO;

namespace Tahorg.RCJoyGUI.STM
{
    public class STMVariable
    {
        public uint NameAddr;
        public ushort EEPROM_ADDR;
        public ushort DATAMAP_ADDR;
        public ushort MODEL_IDX;
        public short INC;
        public short MIN;
        public short MAX;

        public string Name;

        public void Remap(ref uint Pointer)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                NameAddr = 0;
                return;
            }

            NameAddr = Pointer;
            Pointer += (uint)Name.Length;
            Pointer ++;

            STMProgram.Align32Bits(ref Pointer);
        }

        public void WriteHeader(STMMemoryWriter __out)
        {
            __out.Write(NameAddr);
            __out.Write(EEPROM_ADDR);
            __out.Write(DATAMAP_ADDR);
            __out.Write(MODEL_IDX);
            __out.Write(INC);
            __out.Write(MIN);
            __out.Write(MAX);
        }

        public void WriteTo(STMMemoryWriter __out)
        {
            if(NameAddr != 0)
                __out.Write(NameAddr, Name);
        }
    }

    public class STMVariablesArray
    {
//        public const int VARIABLES_SIZE = 64;

        public uint ArrayAddr;
        public readonly List<STMVariable> Variables = new List<STMVariable>();

        public void Remap(ref uint Pointer)
        {
            ArrayAddr = Pointer;
            Pointer += (uint)(16*Variables.Count);
            foreach (var v in Variables) v.Remap(ref Pointer);
        }

        public void WriteTo(STMMemoryWriter __out)
        {
            __out.Position = ArrayAddr;
            foreach (var v in Variables) v.WriteHeader(__out);
            foreach (var v in Variables) v.WriteTo(__out);
        }

        public int Add(ushort dataMap, ushort EEPAddr, string name, ushort ModelID, short inc, short min, short max)
        {
            var idx = Variables.Count;
            Variables.Add(new STMVariable
            {
                DATAMAP_ADDR = dataMap, 
                EEPROM_ADDR = EEPAddr, 
                Name = name, 
                MODEL_IDX = ModelID,
                MIN = min,
                MAX = max,
                INC = inc
            });

            return idx;
        }
    }
}
