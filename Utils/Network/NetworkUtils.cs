// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NetExtender.Utils.Numerics;
using NetExtender.Utils.Types;

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

        public static UInt16 PortCount(this PortType type)
        {
            return type switch
            {
                PortType.System => 1024,
                PortType.Register => 48128,
                PortType.Dynamic => 16384,
                _ => throw new NotSupportedException()
            };
        }
        
        public static Boolean ValidatePort(UInt16 port)
        {
            return port > 0;
        }

        public static Boolean ValidatePort(Int32 port)
        {
            return port.InRange(1, UInt16.MaxValue);
        }

        public static Boolean ValidatePort(String value)
        {
            return UInt16.TryParse(value, out UInt16 port) && ValidatePort(port);
        }
        
        public static Boolean IsPortAvailable(UInt16 port)
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            return properties.GetActiveTcpConnections().All(info => info.LocalEndPoint.Port != port);
        }

        public static UInt16 GeneratePort()
        {
            return GeneratePort(PortType.Dynamic);
        }

        public static UInt16 GeneratePort(this PortType type)
        {
            return (UInt16) (type switch
            {
                PortType.System => RandomUtils.Next(0, 1023),
                PortType.Register => RandomUtils.Next(1024, 49151),
                PortType.Dynamic => RandomUtils.Next(49152, 65535),
                _ => throw new NotSupportedException()
            });
        }

        public static UInt16 GenerateAvailablePort()
        {
            return GenerateAvailablePort(PortType.Dynamic);
        }

        public static UInt16 GenerateAvailablePort(this PortType type)
        {
            return GenerateAvailablePorts(type).First(IsPortAvailable);
        }
        
        public static IEnumerable<UInt16> GenerateAvailablePorts()
        {
            return GenerateAvailablePorts(PortType.Dynamic);
        }

        public static IEnumerable<UInt16> GenerateAvailablePorts(this PortType type)
        {
            return EnumerableUtils.GetEnumerableFrom(() => GeneratePort(type)).Distinct().Where(IsPortAvailable);
        }

        public static Boolean IsSuccessful([CanBeNull] this PingReply? reply)
        {
            return reply?.Status == IPStatus.Success;
        }

        public static async Task<Boolean> IsSuccessfulAsync([NotNull] this Task<PingReply?> reply)
        {
            if (reply is null)
            {
                throw new ArgumentNullException(nameof(reply));
            }

            try
            {
                PingReply ping = await reply.ConfigureAwait(false);
                return ping.IsSuccessful();
            }
            catch (PingException)
            {
                return false;
            }
        }

        public static Task<Boolean> CheckPingAsync(this IPAddress address)
        {
            return new Ping().SendPingAsync(address).IsSuccessfulAsync();
        }

        public static Task<Boolean> CheckPingAsync(this IPAddress address, Int32 timeout)
        {
            return new Ping().SendPingAsync(address, timeout).IsSuccessfulAsync();
        }

        public static Task<Boolean> CheckPingAsync(String address)
        {
            return new Ping().SendPingAsync(address).IsSuccessfulAsync();
        }

        public static Task<Boolean> CheckPingAsync(String address, Int32 timeout)
        {
            return new Ping().SendPingAsync(address, timeout).IsSuccessfulAsync();
        }
    }
}