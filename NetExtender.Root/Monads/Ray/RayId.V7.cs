using System;
using System.Runtime.CompilerServices;

namespace NetExtender.Monads
{
    public unsafe partial struct RayId
    {
        internal static class V7
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static RayId Create(UInt64 span, RayIdFlags flags)
            {
                const Byte metadata = (Byte) RayIdFormat.GuidV7 << 6 | (Byte) Mask.ServerGenerated;
                Container container = new Container(metadata, NewId(Settings.time.Now), span, flags);
                return new RayId(&container);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static RayId Create(DateTime timestamp, RayIdFlags flags)
            {
                const Byte metadata = (Byte) RayIdFormat.GuidV7 << 6 | (Byte) Mask.Timestamp | (Byte) Mask.ServerGenerated;
                Container container = new Container(metadata, NewId(timestamp), timestamp, flags);
                return new RayId(&container);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static RayId Create(UInt64 span, RayIdFlags flags, UInt32 hash)
            {
                const Byte metadata = (Byte) RayIdFormat.GuidV7 << 6 | (Byte) Mask.ServerGenerated | (Byte) Mask.UsingHash;
                Container container = new Container(metadata, NewId(Settings.time.Now), span, hash & 0x00FFFFFF | (UInt32) flags << 24);
                return new RayId(&container);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static RayId Create(DateTime timestamp, RayIdFlags flags, UInt32 hash)
            {
                const Byte metadata = (Byte) RayIdFormat.GuidV7 << 6 | (Byte) Mask.Timestamp | (Byte) Mask.ServerGenerated | (Byte) Mask.UsingHash;
                Container container = new Container(metadata, NewId(timestamp), timestamp, hash & 0x00FFFFFF | (UInt32) flags << 24);
                return new RayId(&container);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static RayId Create(RayId parent, UInt64 span, RayIdFlags flags)
            {
                const Byte metadata = (Byte) RayIdFormat.GuidV7 << 6 | (Byte) Mask.ServerGenerated;
                Container container = new Container(metadata, parent.Id, span, flags);
                return new RayId(&container);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static RayId Create(RayId parent, DateTime timestamp, RayIdFlags flags)
            {
                const Byte metadata = (Byte) RayIdFormat.GuidV7 << 6 | (Byte) Mask.Timestamp | (Byte) Mask.ServerGenerated;
                Container container = new Container(metadata, parent.Id, timestamp, flags);
                return new RayId(&container);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static RayId Create(RayId parent, UInt64 span, RayIdFlags flags, UInt32 hash)
            {
                const Byte metadata = (Byte) RayIdFormat.GuidV7 << 6 | (Byte) Mask.ServerGenerated | (Byte) Mask.UsingHash;
                Container container = new Container(metadata, parent.Id, span, hash & 0x00FFFFFF | (UInt32) flags << 24);
                return new RayId(&container);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static RayId Create(RayId parent, DateTime timestamp, RayIdFlags flags, UInt32 hash)
            {
                const Byte metadata = (Byte) RayIdFormat.GuidV7 << 6 | (Byte) Mask.Timestamp | (Byte) Mask.ServerGenerated | (Byte) Mask.UsingHash;
                Container container = new Container(metadata, parent.Id, timestamp, hash & 0x00FFFFFF | (UInt32) flags << 24);
                return new RayId(&container);
            }
        }
    }
}