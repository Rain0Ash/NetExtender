// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Network
{
    public enum PortType
    {
        Invalid,
        Dynamic,
        Register,
        System
    }

    public static class NetworkUtilities
    {
        public const String Localhost = "localhost";
        public const String LocalhostIP = "127.0.0.1";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ValidateIPv4(String ip)
        {
            return !String.IsNullOrEmpty(ip) && ip.Count(c => c == '.') == 3 && IPAddress.TryParse(ip, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[]? ConvertIP(String ip)
        {
            return IPAddress.TryParse(ip, out IPAddress? address) ? ConvertIP(address) : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ConvertIP(String ip, [MaybeNullWhen(false)] out Byte[] bytes)
        {
            if (IPAddress.TryParse(ip, out IPAddress? address))
            {
                bytes = ConvertIP(address);
                return true;
            }

            bytes = default;
            return false;
        }

        public static Byte[] ConvertIP(this IPAddress address)
        {
            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            return address.GetAddressBytes().InnerReverse();
        }

        public static IPEndPoint ToIPEndPoint(this IPAddress address, UInt16 port)
        {
            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            return new IPEndPoint(address, port);
        }

        public static IPEndPoint ToIPEndPoint(this IPAddress address, Int32 port)
        {
            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            return new IPEndPoint(address, port);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSuccessful(this PingReply? reply)
        {
            return reply?.Status == IPStatus.Success;
        }

        public static async Task<Boolean> IsSuccessfulAsync(this Task<PingReply> reply)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> CheckPingAsync(this IPAddress address)
        {
            return new Ping().SendPingAsync(address).IsSuccessfulAsync();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> CheckPingAsync(this IPAddress address, Int32 timeout)
        {
            return new Ping().SendPingAsync(address, timeout).IsSuccessfulAsync();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> CheckPingAsync(String address)
        {
            return new Ping().SendPingAsync(address).IsSuccessfulAsync();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> CheckPingAsync(String address, Int32 timeout)
        {
            return new Ping().SendPingAsync(address, timeout).IsSuccessfulAsync();
        }
    }
}