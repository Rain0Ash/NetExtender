using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Exceptions;

namespace NetExtender.Monads
{
    public partial struct RayId
    {
        [StructLayout(LayoutKind.Explicit, Size = Settings.Size)]
        internal struct Container
        {
            private static Container Instance = Initialize(Settings.metadata, Settings.version, Settings.servermask, Settings.server, Settings.service);

            [FieldOffset(0)] public Byte Metadata;
            [FieldOffset(1)] public Byte Version;
            [FieldOffset(2)] public UInt16 Service;
            [FieldOffset(4)] public Guid Id;
            [FieldOffset(20)] public UInt64 SpanId;
            [FieldOffset(20)] public DateTime Timestamp;
            [FieldOffset(28)] public UInt32 Info;
            [FieldOffset(28)] public Byte Low;
            [FieldOffset(29)] public Byte Middle;
            [FieldOffset(30)] public Byte High;
            [FieldOffset(31)] public RayIdFlags Flags;

            private Container(Byte metadata, Byte version, UInt16 service)
            {
                Metadata = metadata;
                Version = version;
                Service = service;
                Id = default;
                SpanId = default;
                Timestamp = default;
                Info = default;
                Low = default;
                Middle = default;
                High = default;
                Flags = default;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Container(Byte metadata, Guid id, UInt64 span)
            {
                this = Instance;
                Metadata |= metadata;
                Id = id;
                SpanId = span;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Container(Byte metadata, Guid id, DateTime timestamp)
            {
                this = Instance;
                Metadata |= metadata;
                Id = id;
                Timestamp = timestamp;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Container(Byte metadata, Guid id, UInt64 span, UInt32 info)
            {
                this = Instance;
                Metadata |= metadata;
                Id = id;
                SpanId = span;
                Info = info;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Container(Byte metadata, Guid id, DateTime timestamp, UInt32 info)
            {
                this = Instance;
                Metadata |= metadata;
                Id = id;
                Timestamp = timestamp;
                Info = info;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Container(Byte metadata, Guid id, UInt64 span, RayIdFlags flags)
                : this(metadata, id, span)
            {
                Flags = flags;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Container(Byte metadata, Guid id, DateTime timestamp, RayIdFlags flags)
                : this(metadata, id, timestamp)
            {
                Flags = flags;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Container(Byte metadata, Guid id, UInt64 span, RayIdFlags flags, Byte high, Byte middle, Byte low)
                : this(metadata, id, span, flags)
            {
                Low = low;
                Middle = middle;
                High = high;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Container(Byte metadata, Guid id, DateTime timestamp, RayIdFlags flags, Byte high, Byte middle, Byte low)
                : this(metadata, id, timestamp, flags)
            {
                Low = low;
                Middle = middle;
                High = high;
            }

            internal static Container Initialize(Byte metadata, Byte version, Byte servermask, UInt16 server, UInt16 service)
            {
                return Instance = new Container(metadata, version, GetService(servermask, server, service));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly (UInt16 Server, UInt16 Service) GetService(Byte mask)
            {
                return (GetServerId(mask), GetServiceId(mask));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static UInt16 GetService(Byte servermask, UInt16 server, UInt16 service)
            {
                return unchecked((UInt16) (servermask switch
                {
                    0 => server << 10 | service,
                    1 => server << 8 | service,
                    2 => server << 12 | service,
                    3 => server << 6 | service,
                    4 => server << 14 | service,
                    5 => server << 4 | service,
                    6 => service,
                    7 => server << 2 | service,
                    _ => throw new NeverOperationException()
                }));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly UInt16 GetServerId(Byte mask)
            {
                return (UInt16) (mask switch
                {
                    0 => (Service >> 10) & 0x003F,
                    1 => (Service >> 8) & 0x00FF,
                    2 => (Service >> 12) & 0x000F,
                    3 => (Service >> 6) & 0x03FF,
                    4 => (Service >> 14) & 0x0003,
                    5 => (Service >> 4) & 0x0FFF,
                    6 => 0,
                    7 => (Service >> 2) & 0x3FFF,
                    _ => throw new NeverOperationException()
                });
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly UInt16 GetServiceId(Byte mask)
            {
                return (UInt16) (mask switch
                {
                    0 => Service & 0x03FF,
                    1 => Service & 0x00FF,
                    2 => Service & 0x0FFF,
                    3 => Service & 0x003F,
                    4 => Service & 0x3FFF,
                    5 => Service & 0x000F,
                    6 => Service,
                    7 => Service & 0x0003,
                    _ => throw new NeverOperationException()
                });
            }
        }
    }
}