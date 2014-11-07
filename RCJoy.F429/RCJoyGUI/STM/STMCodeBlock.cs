using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tahorg.RCJoyGUI
{
    public class STMCodeBlock
    {
        public uint BlockStart;
        public readonly List<STMCommand> Commands = new List<STMCommand>();

        public void Remap(ref uint Pointer)
        {
            BlockStart = Pointer;
            Pointer += (uint) Commands.Sum(cmd => cmd.Length) + 2;
                // +2 - last return command
            STMProgram.Align32Bits(ref Pointer);
        }

        public void WriteTo(STMMemoryWriter __out)
        {
            __out.Position = BlockStart;

            foreach (var cmd in Commands)
                cmd.WriteTo(__out);

            __out.Write((ushort)0);
        }

        public void Add(ushort cmd, params IConvertible[] prms)
        {
            Commands.Add(new STMCommand(cmd, prms));
        }
    }
}
