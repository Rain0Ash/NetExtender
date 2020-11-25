// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using NetExtender.Utils.Numerics;

namespace NetExtender.Utils.Network
{
    public enum PortType
    {
        Dynamic,
        Register,
        System
    }
    
    public static class NetworkUtils
    {
        public const String LocalhostIP = "127.0.0.1";

        public static Boolean ValidateIPv4(String ip)
        {
            return !String.IsNullOrEmpty(ip) && ip.Count(c => c == '.') == 3 && IPAddress.TryParse(ip, out _);
        }

        public static IList<Byte> ConvertIP(String ip)
        {
            return IPAddress.TryParse(ip, out IPAddress address) ? ConvertIP(address) : null;
        }
        
        public static IList<Byte> ConvertIP(this IPAddress address)
        {
            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            return address.GetAddressBytes().Reverse().ToImmutableArray();
        }

        public static Boolean ValidatePort(Int32 port)
        {
            return port.InRange(1, UInt16.MaxValue);
        }

        public static Boolean ValidatePort(String port)
        {
            Int32.TryParse(port, out Int32 prt);
            return ValidatePort(prt);
        }

        public static UInt16 GeneratePort(PortType type = PortType.Dynamic)
        {
            return (UInt16)(type switch
            {
                PortType.System => RandomUtils.Next(0, 1023),
                PortType.Register => RandomUtils.Next(1024, 49151),
                PortType.Dynamic => RandomUtils.Next(49152, 65535),
                _ => throw new NotSupportedException()
            });
        }

        public static async Task<Boolean> CheckPingAsync(String address, Int32 timeout = 2000)
        {
            try
            {
                PingReply reply = await new Ping().SendPingAsync(address, timeout).ConfigureAwait(false);
                if (reply is null)
                {
                    return false;
                }

                return reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                return false;
            }
        }
    }
}