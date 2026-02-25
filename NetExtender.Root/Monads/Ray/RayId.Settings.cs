using System;
using System.Runtime.CompilerServices;
using NetExtender.Exceptions;
using NetExtender.Types.Hashes.Interfaces;
using NetExtender.Types.Times;
using NetExtender.Utilities.Types;

namespace NetExtender.Monads
{
    public partial struct RayId
    {
        public static class Settings
        {
            internal const Int32 Size = 32;

            private static volatile SyncRoot? SyncRoot = SyncRoot.Create();

            internal static IHasher hasher = IHasher.Default;
            public static IHasher Hasher
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return hasher;
                }
                set
                {
                    lock (SyncRoot ?? throw new ReadOnlyException())
                    {
                        hasher = value ?? throw new ArgumentNullException(nameof(value));
                    }
                }
            }

            internal static RayIdFormatType @default = RayIdFormatType.Default;
            public static RayIdFormatType RayId
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return @default;
                }
                set
                {
                    lock (SyncRoot ?? throw new ReadOnlyException())
                    {
                        @default = value switch
                        {
                            RayIdFormatType.GuidV4 or RayIdFormatType.GuidV7 or RayIdFormatType.W3C => value,
                            RayIdFormatType.GuidV4T or RayIdFormatType.GuidV7T or RayIdFormatType.W3CT => value,
                            RayIdFormatType.Custom => value,
                            RayIdFormatType.TCustom => value,
                            _ => throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(value, nameof(value), null)
                        };
                    }
                }
            }

            internal static DateTimeProvider time = DateTimeProvider.Utc.Provider;
            public static DateTimeProvider TimeProvider
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return time;
                }
                set
                {
                    if (value.IsEmpty)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    if (value.Kind is not DateTimeKind.Utc)
                    {
                        throw new EnumUndefinedOrNotSupportedException<DateTimeKind>(value.Kind, nameof(value.Kind), null);
                    }

                    lock (SyncRoot ?? throw new ReadOnlyException())
                    {
                        time = value;
                    }
                }
            }

            internal static IFormatProvider? format;
            public static IFormatProvider? FormatProvider
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return format;
                }
                set
                {
                    lock (SyncRoot ?? throw new ReadOnlyException())
                    {
                        format = value;
                    }
                }
            }

            internal static Byte metadata;

            internal static Byte version = 1;
            public static Byte Version
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return version;
                }
                set
                {
                    if (value <= 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(value), value, "Version must be greater than 0.");
                    }

                    lock (SyncRoot ?? throw new ReadOnlyException())
                    {
                        version = value;
                        Refresh();
                    }
                }
            }

            internal static Byte servermask;
            public static Byte ServerMask
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return servermask;
                }
                set
                {
                    if (value >= 1 << 3)
                    {
                        throw new ArgumentOutOfRangeException(nameof(value), value, $"Server mask must be less than {1 << 3}.");
                    }

                    lock (SyncRoot ?? throw new ReadOnlyException())
                    {
                        if (server > MaximumServerId(value))
                        {
                            throw new ArgumentOutOfRangeException(nameof(value), value, $"Can't set server mask to {value} with current server id {server}.");
                        }

                        if (service > MaximumServiceId(value))
                        {
                            throw new ArgumentOutOfRangeException(nameof(value), value, $"Can't set server mask to {value} with current service id {service}.");
                        }

                        servermask = value;
                        Refresh();
                    }
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static UInt16 MaximumServerId(Byte mask)
            {
                return mask switch
                {
                    0 => (1 << 6) - 1,
                    1 => (1 << 8) - 1,
                    2 => (1 << 4) - 1,
                    3 => (1 << 10) - 1,
                    4 => (1 << 2) - 1,
                    5 => (1 << 12) - 1,
                    6 => 0,
                    7 => (1 << 14) - 1,
                    _ => throw new NeverOperationException()
                };
            }

            internal static UInt16 server;
            public static UInt16 ServerId
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return server;
                }
                set
                {
                    lock (SyncRoot ?? throw new ReadOnlyException())
                    {
                        UInt16 maximum = MaximumServerId(servermask);

                        if (value > maximum)
                        {
                            throw new ArgumentOutOfRangeException(nameof(value), value, $"Server id must be less than {maximum} with the current server mask {servermask}.");
                        }

                        server = value;
                        Refresh();
                    }
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static UInt16 MaximumServiceId(Byte mask)
            {
                return mask switch
                {
                    0 => (1 << 10) - 1,
                    1 => (1 << 8) - 1,
                    2 => (1 << 12) - 1,
                    3 => (1 << 6) - 1,
                    4 => (1 << 14) - 1,
                    5 => (1 << 4) - 1,
                    6 => UInt16.MaxValue,
                    7 => (1 << 2) - 1,
                    _ => throw new NeverOperationException()
                };
            }

            internal static UInt16 service;
            public static UInt16 ServiceId
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return service;
                }
                set
                {
                    lock (SyncRoot ?? throw new ReadOnlyException())
                    {
                        UInt16 maximum = MaximumServiceId(servermask);

                        if (value > maximum)
                        {
                            throw new ArgumentOutOfRangeException(nameof(value), value, $"Service id must be less than {maximum} with the current server mask {servermask}.");
                        }

                        service = value;
                        Refresh();
                    }
                }
            }

            private static void Refresh()
            {
                metadata = (Byte) (servermask << 1);
                Container.Initialize(metadata, version, servermask, server, service);
            }

            internal static void Seal()
            {
                if (SyncRoot is not { } sync)
                {
                    return;
                }

                lock (sync)
                {
                    SyncRoot = null;
                }
            }
        }
    }
}