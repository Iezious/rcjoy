using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tahorg.RCJoyGUI.Data;

namespace Tahorg.RCJoyGUI
{
    public class STMFlightMode
    {
        public string Name;

        public uint NameAddr;

        public void Remap(ref uint Pointer)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                NameAddr = 0;
            }
            else
            {
                NameAddr = Pointer;
                Pointer += (uint)Name.Length;
                Pointer++;
            }

            STMProgram.Align32Bits(ref Pointer);
        }

        public void WriteHeader(STMMemoryWriter __out)
        {
            __out.Write(NameAddr);
        }

        public void WriteTo(STMMemoryWriter __out)
        {
            if (NameAddr != 0)
                __out.Write(NameAddr, Name);
        }
    }

    public class STMModel
    {
        public uint NameAddr;
        public uint CodeAddr;
        public uint ModesAddr;

        public ushort PPMChannels;
        public ushort PPMMin;
        public ushort PPMCenter;
        public ushort PPMMax;

        public bool isMenu;
        public ushort Index;

        public uint ModeVariable;
        public STMFlightMode[] Modes;

        public string Name;
        public readonly STMCodeBlock ModelCode = new STMCodeBlock();

        public void Remap(ref uint Pointer)
        {
            ModelCode.Remap(ref Pointer);
            CodeAddr = ModelCode.BlockStart;

            if (string.IsNullOrWhiteSpace(Name))
            {
                NameAddr = 0;
            }
            else
            {
                NameAddr = Pointer;
                Pointer += (uint)Name.Length;
                Pointer++;
            }

            STMProgram.Align32Bits(ref Pointer);

            if (Modes != null && Modes.Length != 0)
            {
                ModesAddr = Pointer;
                Pointer += (uint)(4 * Modes.Length);

                foreach (var mode in Modes) mode.Remap(ref Pointer);
            }
        }

        public const uint LENGTH = 28;

        public void WriteHeader(STMMemoryWriter __out)
        {
            /* 0 */
            __out.Write(NameAddr);
            /* 4 */
            __out.Write(CodeAddr);
            /* 8 */
            __out.Write(isMenu ? (ushort)0 : PPMChannels);
            /* 10 */
            __out.Write(PPMMin);
            /* 12 */
            __out.Write(PPMCenter);
            /* 14 */
            __out.Write(PPMMax);
            /* 16 */
            __out.Write((ushort)Modes.Length);
            /* 18 */
            __out.Write((ushort)ModeVariable);
            /* 20 */
            __out.Write(ModesAddr);
            /* 24 */
            __out.Write(Index);
            /* 26 */
            __out.Write((ushort)0);
            /* 28 */
        }

        public void WriteTo(STMMemoryWriter __out)
        {
            ModelCode.WriteTo(__out);

            if (NameAddr != 0)
                __out.Write(NameAddr, Name);

            if (ModesAddr != 0)
            {
                __out.Position = ModesAddr;

                foreach (var mode in Modes)
                    mode.WriteHeader(__out);

                foreach (var mode in Modes)
                    mode.WriteTo(__out);
            }
        }
    }

    public class STMModelsArray
    {
        public uint ArrayAddr;
        public readonly List<STMModel> Models = new List<STMModel>();

        public STMModel this[int idx]
        {
            get { return Models[idx]; }
        }

        public void Remap(ref uint Pointer)
        {
            ArrayAddr = Pointer;
            Pointer += (uint)(Models.Count * STMModel.LENGTH);

            foreach (var model in Models)
                model.Remap(ref Pointer);
        }

        public void WriteTo(STMMemoryWriter __out)
        {
            __out.Position = ArrayAddr;

            foreach (var model in Models)
            {
                model.WriteHeader(__out);
            }

            foreach (var model in Models)
                model.WriteTo(__out);
        }

        public void Add(ModelInfo model)
        {
            Models.Add(new STMModel
            {
                isMenu = model.IsMenu,
                Index = model.Index,
                Name = model.Name,
                Modes = new STMFlightMode[0],
                PPMChannels = (ushort)model.Channels,
                PPMMin = (ushort)model.PPMMin,
                PPMCenter = (ushort)model.PPMCenter,
                PPMMax = (ushort)model.PPMMax
            });
        }
    }
}
