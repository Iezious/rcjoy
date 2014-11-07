using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace Tahorg.RCJoyGUI
{
    public class ComProgrammer : IDisposable
    {
        private readonly SerialPort __Port;

        private const byte START_COMM = 0x7F;
        private const byte ACK = 0x79;
        private const byte NACK = 0x1F;

        private const byte GET = 0x00;
        private const byte GET_VERSION = 0x01;
        private const byte GET_ID = 0x02;

        private const byte READ = 0x11;
        private const byte GO = 0x21;
        private const byte WRITE = 0x31;

        private const byte ERASE = 0x43;
        private const byte ERASE_EXT = 0x44;

        private const byte WRITE_PROTECT = 0x63;
        private const byte WRITE_OPEN = 0x73;

        private const byte READ_PROTECT = 0x82;
        private const byte READ_OPEN = 0x92;

        public delegate void StateChangedDeletage(enState state, uint bytes);

        public event StateChangedDeletage OnStateChanged;

        private bool SupprotsErase = false;
        private bool SupprotsEraseEx = false;

        public byte VersionMajor { get; private set; }
        public byte VersionMinor { get; private set; }

        protected virtual void OnOnStateChanged(enState state, uint bytes = 0)
        {
            StateChangedDeletage handler = OnStateChanged;
            if (handler != null) handler(state, bytes);
        }

        public enum enState
        {
            None,
            NotConnected,
            Connected,
            Erasing,
            EraseComplete,
            Writing,
            WriteComplete,
            Error
        }

        public enum enBoardType
        {
            STM407VG,
            STM407VE,
            STM407ZG,
            STM407ZE
        }

        public ComProgrammer(string port)
        {
            __Port = new SerialPort(port, 57600, Parity.Even);
            __Port.ReadTimeout = 3000;
            __Port.WriteTimeout = 1000;
        }

        public void Dispose()
        {
            if (__Port == null) return;
            __Port.Dispose();
        }

        #region Private protocol calls

        private bool WaitACK()
        {
            var b = __Port.ReadByte();

            if (b == ACK) return true;
            if (b == NACK) return false;

            throw new DataException("Wrong respose, board is not ready, try again");
        }

        private bool SendCommand(byte Command)
        {
            __Port.Write(new[] { Command, (byte)~Command }, 0, 2);
            return WaitACK();
        }

        private bool InitConn()
        {
            __Port.ReadTimeout = 2000;

            __Port.Write(new[] { START_COMM }, 0, 1);
            return WaitACK();
        }

        private byte[] ReadBuffer()
        {
            int wait_size = __Port.ReadByte();
            if (wait_size == -1) return null;

            wait_size++;

            var buffer = new byte[wait_size];
            var read = 0;

            while (read < wait_size)
                read += __Port.Read(buffer, read, wait_size - read);

            return buffer;

        }

        private bool ExecGet()
        {
            __Port.ReadTimeout = 2000;

            if (!SendCommand(GET)) return false;

            var buffer = ReadBuffer();
            if (buffer == null) return false;

            SupprotsErase = buffer.Contains(ERASE);
            SupprotsEraseEx = buffer.Contains(ERASE_EXT);

            return WaitACK();
        }

        private bool ExecGetID()
        {
            __Port.ReadTimeout = 2000;

            if (!SendCommand(GET_ID)) return false;

            var buffer = ReadBuffer();
            if (buffer == null) return false;

            VersionMajor = buffer[0];
            VersionMinor = buffer[1];

            return WaitACK();
        }
        private void SendDataWithCRC(params byte[] data)
        {
            var xor = (byte)data.Aggregate(0, (current, b) => current ^ b);
            __Port.Write(data, 0, data.Length);
            __Port.Write(new[] { xor }, 0, 1);
        }

        private void SendBufferWithLengthAndCRC(byte[] data, int start, int count)
        {
            if(count == 0) return;
            
            byte xor = (byte)(count - 1);
            for (var i = 0; i < count; i++) xor = (byte)(xor ^ data[i + start]);

            __Port.Write(new[] { (byte)(count - 1) }, 0, 1);
            __Port.Write(data, start, count);
            __Port.Write(new[] { xor }, 0, 1);
        }

        private bool EraseAll()
        {
            OnOnStateChanged(enState.Erasing);

            __Port.ReadTimeout = 10000;

            if (!SendCommand(ERASE))
            {
                OnOnStateChanged(enState.Error);
                return false;
            }

            if (!SendCommand(0xFF))
            {
                OnOnStateChanged(enState.Error);
                return false;
            }

            OnOnStateChanged(enState.EraseComplete);
            return true;
        }


        private bool ErasePages(params byte[] pages)
        {
            OnOnStateChanged(enState.Erasing);
            if (!SendCommand(ERASE))
            {
                OnOnStateChanged(enState.Error);
                return false;
            }

            __Port.ReadTimeout = 10000;

            SendBufferWithLengthAndCRC(pages, 0, pages.Length);

            if (!WaitACK())
            {
                OnOnStateChanged(enState.Error);
                return false;
            }

            OnOnStateChanged(enState.EraseComplete);
            return true;
        }

        private bool EraseAllEx()
        {
            OnOnStateChanged(enState.Erasing);
            if (!SendCommand(ERASE_EXT))
            {
                OnOnStateChanged(enState.Error);
                return false;
            }

            __Port.ReadTimeout = 40000;

            SendDataWithCRC(0xFF, 0xFF);
            if (!WaitACK())
            {
                OnOnStateChanged(enState.Error);
                return false;
            }

            OnOnStateChanged(enState.EraseComplete);
            return true;
        }

        private bool ErasePagesExt(params byte[] pages)
        {
            OnOnStateChanged(enState.Erasing);
            if (!SendCommand(ERASE_EXT))
            {
                OnOnStateChanged(enState.Error);
                return false;
            }


            var b = new byte[pages.Length * 2 + 2];
            b[0] = 0;
            b[1] = (byte)(pages.Length - 1);
            for (var i = 0; i < pages.Length; i++)
            {
                b[i * 2 + 2] = 0;
                b[i * 2 + 3] = pages[i];
            }

            __Port.ReadTimeout = 10000;

            SendDataWithCRC(b);
            if (!WaitACK())
            {
                OnOnStateChanged(enState.Error);
                return false;
            }

            OnOnStateChanged(enState.EraseComplete);
            return true;
        }

        private bool Write(uint addr, byte[] data)
        {
            __Port.ReadTimeout = 10000;

            uint c_addr = addr;
            int c_sent = 0;

            var addr_buffer = new byte[4];

            int to_send = data.Length;

            while (true)
            {
                int chunk_size = Math.Min(to_send - c_sent, 256);
                if (chunk_size <= 0) break;

                addr_buffer[0] = (byte)((c_addr >> 24) & 0xFF);
                addr_buffer[1] = (byte)((c_addr >> 16) & 0xFF);
                addr_buffer[2] = (byte)((c_addr >> 8) & 0xFF);
                addr_buffer[3] = (byte)(c_addr & 0xFF);

                if (!SendCommand(WRITE))
                {
                    OnOnStateChanged(enState.Error);
                    return false;
                }

                SendDataWithCRC(addr_buffer);

                if (!WaitACK())
                {
                    OnOnStateChanged(enState.Error);
                    return false;
                }

                SendBufferWithLengthAndCRC(data, c_sent, chunk_size);
                if (!WaitACK())
                {
                    OnOnStateChanged(enState.Error);
                    return false;
                }

                c_sent += chunk_size;
                c_addr += (uint)chunk_size;

                OnOnStateChanged(enState.Writing, (uint)c_sent);
            }

            OnOnStateChanged(enState.WriteComplete, (uint)c_sent);
            return true;
        }

        #endregion

        public void Connect()
        {
            if (!__Port.IsOpen) __Port.Open();

            if (!InitConn() || !ExecGet() || !ExecGetID())
            {
                OnOnStateChanged(enState.Error);
                return;
            }

            OnOnStateChanged(enState.Connected);
        }

        public bool WriteMainProgram(byte[] data)
        {
            return 
                (
                    (SupprotsErase && EraseAll()) ||
                    (SupprotsEraseEx && EraseAllEx())
                ) && Write(0x08000000U, data);
        }

        public bool WriteByteCode(enBoardType board, byte[] code, bool doClear)
        {
            switch (board)
            {
                case enBoardType.STM407VE:
                case enBoardType.STM407ZE:
                    return (!doClear || (SupprotsErase && ErasePages(7)) || (SupprotsEraseEx && ErasePagesExt(7))) && Write(0x08060000U, code);


                case enBoardType.STM407VG:
                case enBoardType.STM407ZG:
                    return (!doClear || (SupprotsErase && ErasePages(11)) || (SupprotsEraseEx && ErasePagesExt(11))) && Write(0x080E0000U, code);

                default:
                    return false;
            }
        }
    }
}
