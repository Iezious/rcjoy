using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Tahorg.RCJoyGUI.Data;
using Tahorg.RCJoyGUI.STM;

namespace Tahorg.RCJoyGUI
{
    public class STMBlockInkInfo
    {
        public string Name;
        public short DataIndex;
        public ushort Bits;

        public const uint LENGTH = 8;
        public uint NameAddr;

        public STMBlockInkInfo()
        {
            
        }

        public STMBlockInkInfo(LinkPoint p)
        {
            if (p.Direction == enLinkDirection.Input)
            {
                var pp = p.LinkedTo;

                if (pp != null)
                {
                    Name = p.Name;
                    DataIndex = pp.DataMapIdx;
                }
                else
                {
                    DataIndex = 0;
                }
            }
            else
            {
                Name = p.Name;
                DataIndex = p.DataMapIdx;
            }
            
            LinkDirection = p.Direction;
            LinkType = p.LinkType;
        }

        public enLinkDirection LinkDirection
        {
            get { return (Bits & 1) == 1 ? enLinkDirection.Input : enLinkDirection.Output; }
            set
            {
                if (value == enLinkDirection.Input)
                    Bits = (ushort)(Bits & 0xFFFC | 0x1);
                else
                    Bits = (ushort)(Bits & 0xFFFC | 0x2);
            }
        }

        public enLinkType LinkType
        {
            get
            {

                if ((Bits & 0x10) == 0x10) return enLinkType.Button;
                if ((Bits & 0x20) == 0x20) return enLinkType.Value;
                if ((Bits & 0x40) == 0x40) return enLinkType.Axle;

                LinkType = enLinkType.Axle;

                return enLinkType.Axle;
            }

            set
            {
                switch (value)
                {
                    case enLinkType.Button:
                        Bits = (ushort)(Bits & 0xFF0F | 0x10);
                        break;
                    case enLinkType.Value:
                        Bits = (ushort)(Bits & 0xFF0F | 0x20);
                        break;
                    case enLinkType.Axle:
                        Bits = (ushort)(Bits & 0xFF0F | 0x40);
                        break;
                }
            }
        }

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
            __out.Write(DataIndex);
            __out.Write(Bits);
        }

        public void WriteTo(STMMemoryWriter __out)
        {
            if (NameAddr != 0)
                __out.Write(NameAddr, Name);
        }
    }

    public class STMBlockInfo
    {
        public string Name;
        public STMBlockInkInfo[] Links;
        public STMVariable[] Variables;
        public ushort ModelIndex;

        public uint NameAddr;
        public uint LinksAddr;
        public uint VarsAddr;

        public const uint LENGTH = 20;


        public STMBlockInfo()
        {
            
        }

        public STMBlockInfo(DraggableElement el, ModelInfo model, IEnumerable<LinkPoint> links = null, IEnumerable<STMVariable> variables = null)
        {
            Name = el.Title;
            ModelIndex = model.Index;
            Links = (links ?? el.GetLinks()).Where(l => l.Direction == enLinkDirection.Output || l.LinkedTo != null).Select(l => new STMBlockInkInfo(l)).ToArray();
            Variables = variables != null ? variables.ToArray() : null;
        }


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

            if (Links != null && Links.Length != 0)
            {
                LinksAddr = Pointer;
                Pointer += (uint)(STMBlockInkInfo.LENGTH * Links.Length);

                foreach (var mode in Links) mode.Remap(ref Pointer);
            }

            STMProgram.Align32Bits(ref Pointer);

            if (Variables != null && Variables.Length != 0)
            {
                VarsAddr = Pointer;
                Pointer += (uint)(4 * Variables.Length);
            }

            STMProgram.Align32Bits(ref Pointer);
        }

        public void WriteHeader(STMMemoryWriter __out)
        {
            __out.Write(NameAddr);
            __out.Write(LinksAddr);
            __out.Write(VarsAddr);

            __out.Write(ModelIndex);
            __out.Write((ushort)(Links != null ? Links.Length : 0));
            __out.Write((ushort)(Variables != null ? Variables.Length : 0));
            __out.Write((ushort)0);
        }

        public void WriteTo(STMMemoryWriter __out)
        {
            if (NameAddr != 0)
                __out.Write(NameAddr, Name);

            if (LinksAddr != 0)
            {
                __out.Position = LinksAddr;

                foreach (var link in Links)
                    link.WriteHeader(__out);

                foreach (var link in Links)
                    link.WriteTo(__out);
            }

            if (VarsAddr != 0)
            {
                __out.Position = VarsAddr;

                foreach (var variable in Variables)
                    __out.Write(variable.Addr);
            }
        }
    }

    public class STMBlocksArray
    {
        public uint ArrayAddr;
        public readonly List<STMBlockInfo> Blocks  = new List<STMBlockInfo>();

        public STMBlockInfo this[int i]
        {
            get { return Blocks[i]; }
        }

        public void Add(STMBlockInfo block)
        {
            Blocks.Add(block);                        
        }
        public void Remap(ref uint Pointer)
        {
            ArrayAddr = Pointer;
            Pointer += (uint)(Blocks.Count * STMBlockInfo.LENGTH);

            foreach (var block in Blocks)
                block.Remap(ref Pointer);
        }

        public void WriteTo(STMMemoryWriter __out)
        {
            __out.Position = ArrayAddr;

            foreach (var block in Blocks)
                block.WriteHeader(__out);

            foreach (var block in Blocks)
                block.WriteTo(__out);
        }
    }
}
