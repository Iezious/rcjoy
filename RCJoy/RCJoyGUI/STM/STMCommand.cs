using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Tahorg.RCJoyGUI
{
    public class STMCommand
    {
        public readonly ushort CommandCode;
        public readonly short[] CommandArguments;

        public uint Length { get { return (uint) (2 + CommandArguments.Length*2); }}

        public STMCommand(ushort code, params IConvertible[] args)
        {
            CommandCode = code;
            CommandArguments = args.Select(a => a.ToInt16(NumberFormatInfo.InvariantInfo)).ToArray();
        }

        public void WriteTo(STMMemoryWriter __out)
        {
            __out.Write(CommandCode);

            foreach (var argument in CommandArguments)
                __out.Write(argument);    
        }
    }
}
