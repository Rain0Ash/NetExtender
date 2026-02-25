using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetExtender.Monads
{
    public partial struct RayIdContext
    {
        [StructLayout(LayoutKind.Explicit, Size = Settings.Size)]
        internal struct Container
        {
            [FieldOffset(0)] public RayId Id;
            [FieldOffset(0)] public RayId.Container Metadata;
            [FieldOffset(RayId.Settings.Size)] public Guid ParentId;
            [FieldOffset(RayId.Settings.Size + sizeof(UInt64) + sizeof(UInt64))] public UInt64 ParentSpanId;
            [FieldOffset(RayId.Settings.Size + sizeof(UInt64) + sizeof(UInt64))] public DateTime ParentTimestamp;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal Container(Byte metadata)
            {
                Id = default;
                Metadata = new RayId.Container(metadata, default, default(UInt64));
                ParentId = default;
                ParentSpanId = default;
                ParentTimestamp = default;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal Container(RayId.Container container)
            {
                Id = default;
                Metadata = container;
                ParentId = default;
                ParentSpanId = default;
                ParentTimestamp = default;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Container(RayId id, Guid parent, UInt64 span)
            {
                Metadata = default;
                Id = id;
                ParentId = parent;
                ParentTimestamp = default;
                ParentSpanId = span;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal Container(RayId.Container container, Guid parent, UInt64 span)
            {
                Id = default;
                Metadata = container;
                ParentId = parent;
                ParentTimestamp = default;
                ParentSpanId = span;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Container(RayId id, Guid parent, DateTime timestamp)
            {
                Metadata = default;
                Id = id;
                ParentId = parent;
                ParentSpanId = default;
                ParentTimestamp = timestamp;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal Container(RayId.Container container, Guid parent, DateTime timestamp)
            {
                Id = default;
                Metadata = container;
                ParentId = parent;
                ParentSpanId = default;
                ParentTimestamp = timestamp;
            }
        }
    }
}