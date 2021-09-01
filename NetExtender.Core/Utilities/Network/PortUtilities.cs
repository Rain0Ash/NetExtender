// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Network
{
    public static class PortUtilities
    {
        public static String LocalhostPort(UInt16 port)
        {
            return $"{NetworkUtilities.Localhost}:{port}";
        }
        
        public static String LocalhostIPPort(UInt16 port)
        {
            return $"{NetworkUtilities.LocalhostIP}:{port}";
        }
        
        public static TcpConnectionInformation[] ActiveTcpConnections
        {
            get
            {
                IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
                return properties.GetActiveTcpConnections();
            }
        }
        
        public static IPEndPoint[] ActiveTcpListeners
        {
            get
            {
                IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
                return properties.GetActiveTcpListeners();
            }
        }
        
        public static IPEndPoint[] ActiveUdpListeners
        {
            get
            {
                IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
                return properties.GetActiveUdpListeners();
            }
        }

        public static ISet<UInt16> BusyPort
        {
            get
            {
                ISet<UInt16> busy = BusyTcpPort;
                busy.UnionWith(BusyUdpPort);
                return busy;
            }
        }

        public static ISet<UInt16> BusyTcpPort
        {
            get
            {
                IPEndPoint[] endpoints = ActiveTcpListeners;
                ISet<UInt16> set = new HashSet<UInt16>(endpoints.Length);
                set.UnionWith(endpoints.Select(ip => (UInt16) ip.Port));
                return set;
            }
        }
        
        public static ISet<UInt16> BusyUdpPort
        {
            get
            {
                IPEndPoint[] endpoints = ActiveUdpListeners;
                ISet<UInt16> set = new HashSet<UInt16>(endpoints.Length);
                set.UnionWith(endpoints.Select(ip => (UInt16) ip.Port));
                return set;
            }
        }

        public const UInt16 InvalidPort = 0;
        public const UInt16 SystemPortMinimum = PortMinimum;
        public const UInt16 SystemPortMaximum = RegisterPortMinimum - 1;
        public const UInt16 RegisterPortMinimum = 1 << 10;
        public const UInt16 RegisterPortMaximum = DynamicPortMinimum - 1;
        public const UInt16 DynamicPortMinimum = 1 << 15 | 1 << 14;
        public const UInt16 DynamicPortMaximum = PortMaximum;
        
        public const UInt16 PortMinimum = 1;
        public const UInt16 PortMaximum = UInt16.MaxValue;

        public static UInt16 Count(this PortType type)
        {
            return type switch
            {
                PortType.Invalid => 1,
                PortType.System => SystemPortMaximum - SystemPortMinimum + 1,
                PortType.Register => RegisterPortMaximum - RegisterPortMinimum + 1,
                PortType.Dynamic => DynamicPortMaximum - DynamicPortMinimum + 1,
                _ => throw new NotSupportedException()
            };
        }
        
        public static PortType GetPortType(UInt16 port)
        {
            return port switch
            {
                InvalidPort => PortType.Invalid,
                <= SystemPortMaximum => PortType.System,
                <= RegisterPortMaximum => PortType.Register,
                <= DynamicPortMaximum => PortType.Dynamic
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PortType GetPortType(Int32 port)
        {
            return ValidatePort(port) ? GetPortType((UInt16) port) : PortType.Invalid;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PortType GetPortType(String value)
        {
            return UInt16.TryParse(value, out UInt16 port) && ValidatePort(port) ? GetPortType(port) : PortType.Invalid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParsePort(Int32 value, out UInt16 port)
        {
            if (value is >= UInt16.MinValue and <= UInt16.MaxValue)
            {
                port = (UInt16) value;
                return true;
            }

            port = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParsePort(String value, out UInt16 port)
        {
            return UInt16.TryParse(value, out port);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ValidatePort(UInt16 port)
        {
            return port > InvalidPort;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ValidatePort(Int32 port)
        {
            return port is > InvalidPort and <= RegisterPortMaximum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ValidatePort(String value)
        {
            return TryParsePort(value, out UInt16 port) && ValidatePort(port);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPortAvailable(UInt16 port)
        {
            return IsTcpPortAvailable(port) && IsUdpPortAvailable(port);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsTcpPortAvailable(UInt16 port)
        {
            return ActiveTcpListeners.All(connection => connection.Port != port);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsUdpPortAvailable(UInt16 port)
        {
            return ActiveUdpListeners.All(connection => connection.Port != port);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> SelectAvailablePort(IEnumerable<UInt16> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Enumerable.Except(source, BusyPort);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> SelectAvailableTcpPort(IEnumerable<UInt16> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Enumerable.Except(source, BusyTcpPort);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> SelectAvailableUdpPort(IEnumerable<UInt16> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Enumerable.Except(source, BusyUdpPort);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 RandomPort()
        {
            return RandomPort(PortType.Dynamic);
        }

        public static UInt16 RandomPort(this PortType type)
        {
            return type switch
            {
                PortType.Invalid => InvalidPort,
                PortType.System => RandomUtilities.NextUInt16(SystemPortMinimum, SystemPortMaximum),
                PortType.Register => RandomUtilities.NextUInt16(RegisterPortMinimum, RegisterPortMaximum),
                PortType.Dynamic => RandomUtilities.NextUInt16(DynamicPortMinimum, DynamicPortMaximum),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16? RandomAvailablePort()
        {
            return RandomAvailablePort(PortType.Dynamic);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16? RandomAvailablePort(this PortType type)
        {
            return EnumerableUtilities.GetEnumerableFrom(() => RandomPort(type)).Except(BusyPort).FirstOrDefault();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16? RandomAvailableTcpPort()
        {
            return RandomAvailableTcpPort(PortType.Dynamic);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16? RandomAvailableTcpPort(this PortType type)
        {
            return EnumerableUtilities.GetEnumerableFrom(() => RandomPort(type)).Except(BusyTcpPort).FirstOrDefault();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16? RandomAvailableUdpPort()
        {
            return RandomAvailableUdpPort(PortType.Dynamic);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16? RandomAvailableUdpPort(this PortType type)
        {
            return EnumerableUtilities.GetEnumerableFrom(() => RandomPort(type)).Except(BusyUdpPort).FirstOrDefault();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<UInt16> RangeAvailablePort(this PortType type, ISet<UInt16> except)
        {
            return type switch
            {
                PortType.Invalid => EnumerableUtilities.GetEnumerableFrom(InvalidPort).Except(except),
                PortType.System => Enumerable.Except(MathUtilities.RangeInclude(SystemPortMinimum, SystemPortMaximum), except),
                PortType.Register => Enumerable.Except(MathUtilities.RangeInclude(RegisterPortMinimum, RegisterPortMaximum), except),
                PortType.Dynamic => Enumerable.Except(MathUtilities.RangeInclude(DynamicPortMinimum, DynamicPortMaximum), except),
                _ => throw new NotSupportedException()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> RangeAvailablePort()
        {
            return RangeAvailablePort(PortType.Dynamic);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> RangeAvailablePort(Int32 count)
        {
            return RangeAvailablePort(count, PortType.Dynamic);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> RangeAvailablePort(Int32 count, PortType type)
        {
            return RangeAvailablePort(type, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> RangeAvailablePort(this PortType type)
        {
            return RangeAvailablePort(type, BusyPort);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> RangeAvailablePort(this PortType type, Int32 count)
        {
            return count > 0 ? RangeAvailablePort(type).Take(count) : Array.Empty<UInt16>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> RangeAvailableTcpPort()
        {
            return RangeAvailableTcpPort(PortType.Dynamic);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> RangeAvailableTcpPort(Int32 count)
        {
            return RangeAvailableTcpPort(count, PortType.Dynamic);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> RangeAvailableTcpPort(Int32 count, PortType type)
        {
            return RangeAvailableTcpPort(type, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> RangeAvailableTcpPort(this PortType type)
        {
            return RangeAvailablePort(type, BusyTcpPort);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> RangeAvailableTcpPort(this PortType type, Int32 count)
        {
            return count > 0 ? RangeAvailableTcpPort(type).Take(count) : Array.Empty<UInt16>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> RangeAvailableUdpPort()
        {
            return RangeAvailableUdpPort(PortType.Dynamic);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> RangeAvailableUdpPort(Int32 count)
        {
            return RangeAvailableUdpPort(count, PortType.Dynamic);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> RangeAvailableUdpPort(Int32 count, PortType type)
        {
            return RangeAvailableUdpPort(type, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> RangeAvailableUdpPort(this PortType type)
        {
            return RangeAvailablePort(type, BusyUdpPort);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> RangeAvailableUdpPort(this PortType type, Int32 count)
        {
            return count > 0 ? RangeAvailableUdpPort(type).Take(count) : Array.Empty<UInt16>();
        }
    }
}