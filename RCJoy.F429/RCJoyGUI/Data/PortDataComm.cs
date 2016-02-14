using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace Tahorg.RCJoyGUI.Data
{
    public static class PortDataComm
    {
        public enum CommandStatus
        {
            OK,
            Error,
            NoCommand,
            Timeout
        }

        private static SerialPort __Port;
        public static bool IsOpen
        {
            get { return __Port != null && __Port.IsOpen; }
        }

        public static void Open(string portName, int baud)
        {
            if (IsOpen) return;

            __Port = new SerialPort(portName, baud, Parity.None);
            __Port.Open();
            __Port.WriteTimeout = 500;
            __Port.ReadTimeout = 500;
        }

        public static void Close()
        {
            if (!IsOpen) return;

            __Port.Close();
            __Port = null;
        }

        public static CommandStatus SendCommand(byte command, out byte[] result, params byte[] args)
        {
            result = null;
            if (!IsOpen) return CommandStatus.Error;
            return InternalSend(command, ref result, 500, args);
        }        
        
        public static CommandStatus SendLongRunCommand(byte command, out byte[] result, int Timeout, params byte[] args)
        {
            result = null;
            if (!IsOpen) return CommandStatus.Error;
            return InternalSend(command, ref result, Timeout, args);
        }

        private static CommandStatus InternalSend(byte command, ref byte[] result, int timeout, byte[] data)
        {
            if(!Monitor.TryEnter(__Port, timeout))
                return CommandStatus.Timeout;

            __Port.ReadTimeout = timeout;

            try
            {
                if (!IsOpen) return CommandStatus.Error;

                var header = new byte[] {0xFF, 0xFF, 
                    (byte) (data != null ? data.Length + 1 : 1), 
                    command};

                __Port.Write(header, 0, 4);

                if(data != null)
                    __Port.Write(data, 0, data.Length);

                int retcmd;
                var readres = ReadPort(out retcmd, ref result);
                if(readres != CommandStatus.OK) return readres;

                if (retcmd != command)
                    return CommandStatus.NoCommand;

                return CommandStatus.OK;
            }
            finally
            {
                Monitor.Exit(__Port);

                if (__Port.IsOpen == false)
                    __Port = null;
            }
        }

        private static CommandStatus ReadPort(out int wait_command, ref byte[] result)
        {
            wait_command = -1;
            if (!__Port.IsOpen) return CommandStatus.Error;

            try
            {
                while (__Port.ReadByte() != 0xFF) ;
                var check = __Port.ReadByte();

                int size = __Port.ReadByte() << 8;
                size |= __Port.ReadByte();

                if(check != 0xFF)
                    return CommandStatus.NoCommand;

                int command = __Port.ReadByte();
                size --;

                if (size == 0)
                {
                    result = new byte[0];
                    return CommandStatus.OK;
                }

                var buffer = new byte[size];
                for (var i = 0; i < size; i++)
                    buffer[i] = (byte) __Port.ReadByte();

                wait_command = command;
                result = buffer;
                return CommandStatus.OK;
            }
            catch
            {
                return CommandStatus.Error;
            }
        }
    }
}
