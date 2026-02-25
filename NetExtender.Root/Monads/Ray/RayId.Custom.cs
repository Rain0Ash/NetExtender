using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Exceptions;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Monads
{
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public unsafe partial struct RayId
    {
        private static T Default<T>(ReadOnlySpan<Char> _)
        {
            return default!;
        }

        private static T Default<T>(ReadOnlySpan<Char> _, IFormatProvider? provider, ref Maybe<RayIdPayload?> payload)
        {
            return default!;
        }

        private static T Default<T>(Span<Byte> _)
        {
            return default!;
        }

        private static String Default(Span<Byte> value, RayIdPayload? payload, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return value.ToHexString();
        }

        [SuppressMessage("Usage", "CA2211")]
        [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
        public abstract class Custom
        {
            protected internal enum Mask : Byte
            {
                Timestamp = RayId.Mask.Timestamp,
                UsingHash = RayId.Mask.UsingHash
            }

            private static Custom Factory = new Throw();
            public static delegate*<Span<Byte>, Byte> Version = &Default<Byte>;
            public static delegate*<Span<Byte>, (UInt16 Server, UInt16 Service)?> Service = &Default<(UInt16, UInt16)?>;
            public static delegate*<Span<Byte>, Guid> Id = &Default<Guid>;
            public static delegate*<Span<Byte>, UInt64> SpanId = &Default<UInt64>;
            public static delegate*<Span<Byte>, DateTime> Timestamp = &Default<DateTime>;
            public static delegate*<Span<Byte>, Guid> ParentId = &Default<Guid>;
            public static delegate*<Span<Byte>, UInt64> ParentSpanId = &Default<UInt64>;
            public static delegate*<Span<Byte>, DateTime> ParentTimestamp = &Default<DateTime>;
            public static delegate*<Span<Byte>, RayIdFlags> Flags = &Default<RayIdFlags>;
            public static delegate*<Span<Byte>, UInt32?> Hash = &Default<UInt32?>;
            public static delegate*<Span<Byte>, UInt32> Info = &CustomInfo;
            public static delegate*<Span<Byte>, RayIdPayload?, EscapeType, String?, IFormatProvider?, String> Format = &Default;
            public static delegate*<ReadOnlySpan<Char>, IFormatProvider?, ref Maybe<RayIdPayload?>, RayIdContext> TryParse = &Default<RayIdContext>;

            protected static void Initialize<T>() where T : Custom
            {
                Factory = Activator.CreateInstance<T>();
            }

            protected abstract Mask Create(Span<Byte> container, RayId parent, DateTime timestamp, RayIdFlags flags);
            protected abstract Mask Create<T>(Span<Byte> container, RayId parent, in T value, DateTime timestamp, RayIdFlags flags) where T : notnull;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Mask Fill(Span<Byte> container, RayId parent, DateTime timestamp, RayIdFlags flags)
            {
                return Factory.Create(container, parent, timestamp, flags);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Mask Fill<T>(Span<Byte> container, RayId parent, in T value, DateTime timestamp, RayIdFlags flags) where T : notnull
            {
                return Factory.Create(container, parent, value, timestamp, flags);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static RayId New(RayId parent, DateTime timestamp, RayIdFlags flags)
            {
                const Byte metadata = (Byte) RayIdFormat.Custom << 6 | (Byte) RayId.Mask.ServerGenerated;
                Container container = new Container(metadata, default, default(UInt64));
                Mask mask = Fill(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref container, 1)).Slice(1), parent, timestamp, flags);
                container.Metadata |= (Byte) mask;
                return new RayId(&container);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static RayId New<T>(RayId parent, in T value, DateTime timestamp, RayIdFlags flags) where T : notnull
            {
                const Byte metadata = (Byte) RayIdFormat.Custom << 6 | (Byte) RayId.Mask.ServerGenerated;
                Container container = new Container(metadata, default, default(UInt64));
                Mask mask = Fill(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref container, 1)).Slice(1), parent, value, timestamp, flags);
                container.Metadata |= (Byte) mask;
                return new RayId(&container);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static RayIdContext Context(RayId parent, DateTime timestamp, RayIdFlags flags, RayIdPayload? payload)
            {
                const Byte metadata = (Byte) RayIdFormat.Custom << 6 | (Byte) RayId.Mask.ServerGenerated;
                RayIdContext.Container container = new RayIdContext.Container(metadata);
                Fill(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref container, 1)).Slice(1), parent, timestamp, flags);
                return new RayIdContext(&container) { SafePayload = payload };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static RayIdContext Context<T>(RayId parent, in T value, DateTime timestamp, RayIdFlags flags, RayIdPayload? payload) where T : notnull
            {
                const Byte metadata = (Byte) RayIdFormat.Custom << 6 | (Byte) RayId.Mask.ServerGenerated;
                RayIdContext.Container container = new RayIdContext.Container(metadata);
                Fill(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref container, 1)).Slice(1), parent, value, timestamp, flags);
                return new RayIdContext(&container) { SafePayload = payload };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static RayIdContext Continue(RayIdContext context, DateTime timestamp, RayIdFlags flags, RayIdPayload? payload)
            {
                const Byte metadata = (Byte) RayIdFormat.Custom << 6 | (Byte) RayId.Mask.ServerGenerated;
                RayIdContext.Container @new = new RayIdContext.Container(metadata);
                RayIdContext.Container container = context.This;
                container.Metadata = @new.Metadata;

                Fill(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref container, 1)).Slice(1), context.RayId, timestamp, flags);
                return new RayIdContext(&container) { SafePayload = payload };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static RayIdContext Continue<T>(RayIdContext context, in T value, DateTime timestamp, RayIdFlags flags, RayIdPayload? payload) where T : notnull
            {
                const Byte metadata = (Byte) RayIdFormat.Custom << 6 | (Byte) RayId.Mask.ServerGenerated | (Byte) Mask.UsingHash;
                RayIdContext.Container @new = new RayIdContext.Container(metadata);
                RayIdContext.Container container = context.This;
                container.Metadata = @new.Metadata;

                Fill(MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref container, 1)).Slice(1), context.RayId, value, timestamp, flags);
                return new RayIdContext(&container) { SafePayload = payload };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static UInt32 CustomInfo(Span<Byte> value)
            {
                return (UInt32) Flags(value) << 24;
            }

            private sealed class Throw : Custom
            {
                protected override Mask Create(Span<Byte> container, RayId parent, DateTime timestamp, RayIdFlags flags)
                {
                    throw new EnumUndefinedOrNotSupportedException<RayIdFormat>(RayIdFormat.Custom, nameof(Custom), null);
                }

                protected override Mask Create<T>(Span<Byte> container, RayId parent, in T value, DateTime timestamp, RayIdFlags flags)
                {
                    return Create(container, parent, timestamp, flags);
                }
            }
        }
    }
}