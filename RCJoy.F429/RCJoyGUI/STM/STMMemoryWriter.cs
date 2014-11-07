using System;
using System.Text;

namespace Tahorg.RCJoyGUI
{
    public class STMMemoryWriter
    {
        public readonly uint Base;
        public readonly byte[] Buffer;
        public uint Position { get; set; }

        public STMMemoryWriter(uint _base, uint size)
        {
            Base = _base;
            Buffer = new byte[size];
        }

        public void Write(uint Addr, byte[] data)
        {
            Array.Copy(data, 0, Buffer, Addr-Base, data.Length);
        }

        public void Write(uint Addr, string data)
        {
            byte[] bdata = Encoding.ASCII.GetBytes(data);
            Write(Addr, bdata);
            Write((uint) (Addr+bdata.Length), (byte)0);

        }

        public void Write(uint Addr, byte data)
        {
            Buffer[Addr - Base] = data;
        }

        public void Write(uint Addr, short data)
        {
            Buffer[Addr - Base] = (byte)(data & 0xFF);
            Buffer[Addr - Base + 1] = (byte)((data >> 8) & 0xFF);
        }

        public void Write(uint Addr, ushort data)
        {
            Buffer[Addr - Base] = (byte)(data & 0xFF);
            Buffer[Addr - Base + 1] = (byte)((data >> 8) & 0xFF);
        }

        public void Write(uint Addr, int data)
        {
            Buffer[Addr - Base] = (byte)(data & 0xFF);
            Buffer[Addr - Base + 1] = (byte)((data >> 8) & 0xFF);
            Buffer[Addr - Base + 2] = (byte)((data >> 16) & 0xFF);
            Buffer[Addr - Base + 3] = (byte)((data >> 24) & 0xFF);
        }

        public void Write(uint Addr, uint data)
        {
            Buffer[Addr - Base] = (byte)(data & 0xFF);
            Buffer[Addr - Base + 1] = (byte)((data >> 8) & 0xFF);
            Buffer[Addr - Base + 2] = (byte)((data >> 16) & 0xFF);
            Buffer[Addr - Base + 3] = (byte)((data >> 24) & 0xFF);
        }

        public void Write(byte[] data)
        {
            Array.Copy(data, 0, Buffer, Position-Base, data.Length);
            Position += (uint)data.Length;
        }

        public void Write(string data)
        {
            byte[] bdata = Encoding.ASCII.GetBytes(data);
            Write(Position, bdata);
            Position += (uint)bdata.Length;
            Write(Position, (byte)0);
            Position++;
        }

        public void Write(byte data)
        {
            Buffer[Position - Base] = data;
            Position++;
        }

        public void Write(short data)
        {
            Buffer[Position - Base] = (byte)(data & 0xFF);
            Position++;
            Buffer[Position - Base] = (byte)((data >> 8) & 0xFF);
            Position++;
        }

        public void Write(ushort data)
        {
            Buffer[Position - Base] = (byte)(data & 0xFF);
            Position++;
            Buffer[Position - Base] = (byte)((data >> 8) & 0xFF);
            Position++;
        }

        public void Write(int data)
        {
            Buffer[Position - Base] = (byte)(data & 0xFF);
            Position++;
            
            Buffer[Position - Base] = (byte)((data >> 8) & 0xFF);
            Position++;
            
            Buffer[Position - Base] = (byte)((data >> 16) & 0xFF);
            Position++;
            
            Buffer[Position - Base] = (byte)((data >> 24) & 0xFF);
            Position++;
        }

        public void Write(uint data)
        {
            Buffer[Position - Base] = (byte)(data & 0xFF);
            Position++;

            Buffer[Position - Base] = (byte)((data >> 8) & 0xFF);
            Position++;

            Buffer[Position - Base] = (byte)((data >> 16) & 0xFF);
            Position++;

            Buffer[Position - Base] = (byte)((data >> 24) & 0xFF);
            Position++;
        }
    }
}
