using System.Collections.Generic;
using System.IO;

namespace Tahorg.RCJoyGUI.STM
{
    public class STMStringConstant
    {
        public uint Addr;
        public readonly string Text;

        public STMStringConstant(string text)
        {
            Text = text;
        }

        public void Remap(ref uint Pointer)
        {
            if(string.IsNullOrEmpty(Text)) return;

            Addr = Pointer;

            Pointer += (uint)Text.Length;
            Pointer++;

            STMProgram.Align32Bits(ref Pointer);
        }

        public void WriteHeader(STMMemoryWriter __out)
        {
            __out.Write(Addr);
        }

        public void WriteTo(STMMemoryWriter __out)
        {
            if (Addr != 0)
                __out.Write(Addr, Text);
        }
    }

    public class STMStringsArray
    {
        public uint ArrayAddr;
        public readonly List<STMStringConstant> List = new List<STMStringConstant>();

        public void Remap(ref uint Pointer)
        {
            if (List.Count != 0)
            {
                ArrayAddr = Pointer;
                Pointer += (uint) (4*(List.Count+1));
                foreach (var v in List) v.Remap(ref Pointer);
            }
        }

        public void WriteHeader(Stream __out)
        {
            __out.WriteStreamLE(ArrayAddr);
        }

        public int Add(string Text)
        {
            if (string.IsNullOrWhiteSpace(Text)) return 0;
            
            List.Add(new STMStringConstant(Text));
            return List.Count; // yes! +1!
        }

        public void WriteTo(STMMemoryWriter __out)
        {
            if (List.Count > 0)
            {
                __out.Position = ArrayAddr;
                __out.Write((uint) 0); // this is NO string

                foreach (var v in List) v.WriteHeader(__out);

                foreach (var v in List) v.WriteTo(__out);
            }
        }
    }
}
