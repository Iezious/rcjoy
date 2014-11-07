using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Tahorg.RCJoyGUI.Data
{
//    public delegate void FRAMUserEvent(IFRAMUser user);
    
    public interface IFRAMUser
    {
        Guid ID { get; }
        uint SlotsUsed { get; }
        uint[] FRAMAddresses { get; set; }

        string[] Names { get; }

        int[]  DefaultValues { get; }
//        event FRAMUserEvent ItemDeleted;
//        event FRAMUserEvent AddressRequired;
    }


    public static class FRAMMapper
    {
        private const uint VARIABLES_START = 20;

        private static readonly Dictionary<uint, IFRAMUser> __Map = new Dictionary<uint, IFRAMUser>();

        private static void FillAddresses(IFRAMUser user)
        {
            var padds = user.FRAMAddresses;

            if(padds.All(a => a != 0)) return;

            var addrs = new uint[user.SlotsUsed];


            uint start = VARIABLES_START;
            for (var i = 0; i < user.SlotsUsed; i++)
            {
                addrs[i] = padds[i];
                if(addrs[i] != 0) continue;

                while (__Map.ContainsKey(start)) start += 2;
                addrs[i] = start;
                start += 2;
            }

            user.FRAMAddresses = addrs;
        }

        private static void CheckAddressUsage(IFRAMUser user)
        {
            var paddr = user.FRAMAddresses
                .Select(a => __Map.ContainsKey(a) ? 0 : a)
                .ToArray();

            if(paddr.Contains((uint)0)) FillAddresses(user);
        }

        public static void Register(IFRAMUser user)
        {
//            user.ItemDeleted += UnRegisterUser;
            CheckAddressUsage(user);

            foreach (var address in user.FRAMAddresses)
            {
                __Map.Add(address, user);
            }
        }

        public static void ReAssign(IFRAMUser user)
        {
            UnRegisterUser(user);
            Register(user);
        }

        public static void UnRegisterUser(IFRAMUser user)
        {
            var keys = __Map.Keys.ToArray();
            foreach (var addr in keys)
            {
                if (user.ID == __Map[addr].ID)
                    __Map.Remove(addr);
            }
        }

        public static void Clear()
        {
            __Map.Clear();
        }
    }
}
