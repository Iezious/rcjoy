using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsFormsToolkit.Threading;

namespace Tahorg.RCJoyGUI
{
    public class PortCommunicator
    {
        private enum enState
        {
            wHeader,
            wHeader2,
            wSize,
            wSize2,
            wCommand,
            rData
        }

        public delegate void OnDataRecievedEvent(byte Command, byte[] data);

        private ConcurrentQueue<byte[]> writeQueue = new ConcurrentQueue<byte[]>();
        private AutoResetEvent writeStoper = new AutoResetEvent(false);
        private Thread Writer;

        private readonly byte[] __Buffer = new byte[2048];
        private enState __State;
        private ushort __WaitForSize = 0;
        private byte __Command;
        private ushort __BufferPos;

        private SerialPort __Port;

        public event OnDataRecievedEvent OnDataRecieved;

        public PortCommunicator()
        {

        }

        public bool IsOpen
        {
            get { return __Port != null && __Port.IsOpen; }
        }

        public void Open(string portName, int baud)
        {
            if (IsOpen) return;

            __Port = new SerialPort(portName, baud, Parity.None);
            __Port.Open();

            WatchDog();
        }

        public void Close()
        {
            if (Writer != null)
                Writer.Abort();

            if (!IsOpen) return;


            __Port.Close();
            __Port = null;
        }

        public void SendCommand(byte command, params byte[] args)
        {
            if (!IsOpen) return;

            var l = args != null ? args.Length : 0;

            var sbuffer = new byte[l + 4];
            sbuffer[0] = sbuffer[1] = 0xFF;
            sbuffer[2] = (byte)(1 + l);
            sbuffer[3] = command;

            if (args != null)
                Array.Copy(args, 0, sbuffer, 4, l);


            writeQueue.Enqueue(sbuffer);
            WatchDog();
            writeStoper.Set();

        }

        private void WatchDog()
        {
            if (!IsOpen) return;
            if (Writer != null && Writer.IsAlive) return;

            Writer = new Thread(WriteThread) { IsBackground = true, Name = "ComPortWriter" };
            Writer.Start();
        }

        private void WriteThread()
        {
            while (true)
            {
                try
                {
                    byte[] sbuffer;

                    if (__Port != null && __Port.IsOpen && writeQueue.TryDequeue(out sbuffer))
                    {
                        __Port.Write(sbuffer, 0, sbuffer.Length);
                        TryReadPort();
                    }
                    else
                    {
                        writeStoper.WaitOne(200);
                    }
                }
                catch (ThreadAbortException)
                {
                    break;
                }
                catch (Exception)
                {
                }
            }

            Writer = null;
        }

        private void TryReadPort()
        {
            while (true)
            {
                if (!__Port.IsOpen)
                {
                    __Port = null;
                    return;
                }

                var b =__Port.ReadByte();
                if(ParseByte((byte)b)) return;
            }
        }

        private bool ParseByte(byte data)
        {
            switch (__State)
            {
                case enState.wHeader:

                    if (data == 0xFF)
                        __State = enState.wHeader2;

                    break;

                case enState.wHeader2:
                    __State = data == 0xFF ? enState.wSize : enState.wHeader;
                    break;

                case enState.wSize:
                    __State = enState.wSize2;
                    __WaitForSize = (ushort)(data << 8);
                    break;

                case enState.wSize2:
                    __WaitForSize |= data;
                    __State = __WaitForSize == 0 || __WaitForSize > __Buffer.Length ? enState.wHeader : enState.wCommand;

                    break;

                case enState.wCommand:
                    __Command = data;
                    __WaitForSize--;
                    __BufferPos = 0;

                    if (__WaitForSize > 0)
                        __State = enState.rData;
                    else
                    {
                        __State = enState.wHeader;
                        ExecCommand();
                    }

                    break;

                case enState.rData:
                    __Buffer[__BufferPos] = data;
                    __BufferPos++;

                    if (__WaitForSize == __BufferPos)
                    {
                        __State = enState.wHeader;
                        ExecCommand();
                    }

                    break;
            }

            return __State == enState.wHeader;
        }

        private void ExecCommand()
        {
            if (OnDataRecieved == null) return;

            if (__WaitForSize == 0)
            {
                OnDataRecieved(__Command, null);
                return;
            }

            var cb = new byte[__WaitForSize];
            Array.Copy(__Buffer, cb, __WaitForSize);
            OnDataRecieved(__Command, cb);
        }
    }
}
