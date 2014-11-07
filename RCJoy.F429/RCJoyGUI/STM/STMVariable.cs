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
        public short DEF;

        public string Name;

        public const uint LENGTH = 20;
        public uint Addr { get; set; }

        public void Remap(ref uint Pointer)
        {
            if (String.IsNullOrWhiteSpace(Name))
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
            __out.Write(DEF);
            __out.Write((ushort)0);
        }

        public void WriteTo(STMMemoryWriter __out)
        {
            if(NameAddr != 0)
                __out.Write(NameAddr, Name);
        }
    }

    public class STMVariablesArray
    {
        public uint ArrayAddr;
        public readonly List<STMVariable> Variables = new List<STMVariable>();

        public void Remap(ref uint Pointer)
        {
            ArrayAddr = Pointer;
            Pointer += (uint)(STMVariable.LENGTH * Variables.Count);
            for (int i = 0; i < Variables.Count; i++)
            {
                var v = Variables[i];

                v.Remap(ref Pointer);
                v.Addr = (uint) (ArrayAddr + i*STMVariable.LENGTH);
            }
        }

        public void WriteTo(STMMemoryWriter __out)
        {
            __out.Position = ArrayAddr;
            foreach (var v in Variables) v.WriteHeader(__out);
            foreach (var v in Variables) v.WriteTo(__out);
        }

        public int Add(ushort dataMap, ushort EEPAddr, string name, ushort ModelID, short inc, short min, short max, short def)
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

        public STMVariable this[int idx]
        {
            get { return Variables[idx]; }
        }
    }
}
