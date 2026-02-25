using System;
using System.Runtime.CompilerServices;
using NetExtender.Exceptions;
using NetExtender.Types.Monads.Interfaces;

namespace NetExtender.Monads
{
    public partial struct RayIdContext
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New()
        {
            return New(null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New(RayIdPayload? payload)
        {
            return New(RayIdFlags.None, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New(RayIdFlags flags)
        {
            return New(flags, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New(RayIdFlags flags, RayIdPayload? payload)
        {
            return New(RayId.Settings.@default, flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New(RayIdFormatType format)
        {
            return New(format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New(RayIdFormatType format, RayIdPayload? payload)
        {
            return New(format, RayIdFlags.None, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New(RayIdFormatType format, RayIdFlags flags)
        {
            return New(format, flags, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static RayIdContext New(RayIdFormatType format, RayIdFlags flags, RayIdPayload? payload)
        {
            return format switch
            {
                RayIdFormatType.GuidV4 => new RayIdContext(RayId.V4.Create(RayId.NewSpanId(), flags), default, default(UInt64)) { SafePayload = payload },
                RayIdFormatType.GuidV4T => new RayIdContext(RayId.V4.Create(RayId.Settings.time.Now, flags), default, default(DateTime)) { SafePayload = payload },
                RayIdFormatType.GuidV7 => new RayIdContext(RayId.V7.Create(RayId.NewSpanId(), flags), default, default(UInt64)) { SafePayload = payload },
                RayIdFormatType.GuidV7T => new RayIdContext(RayId.V7.Create(RayId.Settings.time.Now, flags), default, default(DateTime)) { SafePayload = payload },
                RayIdFormatType.W3C => new RayIdContext(RayId.W3C.Create(RayId.NewSpanId(), flags), default, default(UInt64)) { SafePayload = payload },
                RayIdFormatType.W3CT => new RayIdContext(RayId.W3C.Create(RayId.Settings.time.Now, flags), default, default(DateTime)) { SafePayload = payload },
                RayIdFormatType.Custom => RayId.Custom.Context(default, default, flags, payload),
                RayIdFormatType.TCustom => RayId.Custom.Context(default, RayId.Settings.time.Now, flags, payload),
                _ => throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(format, nameof(format), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New(RayId parent)
        {
            return New(parent, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New(RayId parent, RayIdPayload? payload)
        {
            return New(parent, RayIdFlags.None, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New(RayId parent, RayIdFlags flags)
        {
            return New(parent, flags, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static RayIdContext New(RayId parent, RayIdFlags flags, RayIdPayload? payload)
        {
            RayIdFormatType format;
            return (format = parent ? parent.FormatType : RayIdFormatType.Default) switch
            {
                RayIdFormatType.GuidV4 => new RayIdContext(RayId.V4.Create(RayId.NewSpanId(), flags), parent.Id, parent.SpanId) { SafePayload = payload },
                RayIdFormatType.GuidV4T => new RayIdContext(RayId.V4.Create(RayId.Settings.time.Now, flags), parent.Id, parent.Timestamp) { SafePayload = payload },
                RayIdFormatType.GuidV7 => new RayIdContext(RayId.V7.Create(RayId.NewSpanId(), flags), parent.Id, parent.SpanId) { SafePayload = payload },
                RayIdFormatType.GuidV7T => new RayIdContext(RayId.V7.Create(RayId.Settings.time.Now, flags), parent.Id, parent.Timestamp) { SafePayload = payload },
                RayIdFormatType.W3C => new RayIdContext(RayId.W3C.Create(RayId.NewSpanId(), flags), parent.Id, parent.SpanId) { SafePayload = payload },
                RayIdFormatType.W3CT => new RayIdContext(RayId.W3C.Create(RayId.Settings.time.Now, flags), parent.Id, parent.Timestamp) { SafePayload = payload },
                RayIdFormatType.Custom => RayId.Custom.Context(parent, default, flags, payload),
                RayIdFormatType.TCustom => RayId.Custom.Context(parent, RayId.Settings.time.Now, flags, payload),
                _ => throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(format, nameof(format), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next()
        {
            return Next(null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next()
        {
            return Next();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next(RayIdPayload? payload)
        {
            return New(RayId, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next(RayIdPayload? payload)
        {
            return Next(payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next(Boolean flags)
        {
            return Next(flags, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next(Boolean flags)
        {
            return Next(flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next(Boolean flags, RayIdPayload? payload)
        {
            return New(RayId, flags ? Flags : RayIdFlags.None, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next(Boolean flags, RayIdPayload? payload)
        {
            return Next(flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next(RayIdFlags flags)
        {
            return Next(flags, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next(RayIdFlags flags)
        {
            return Next(flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next(RayIdFlags flags, RayIdPayload? payload)
        {
            return New(RayId, flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next(RayIdFlags flags, RayIdPayload? payload)
        {
            return Next(flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private readonly RayIdContext Core(RayIdFlags flags, RayIdPayload? payload)
        {
            RayIdFormatType format;
            return (format = FormatType) switch
            {
                RayIdFormatType.GuidV4 => new RayIdContext(RayId.V4.Create(RayId, RayId.NewSpanId(), flags), Id, SpanId) { SafePayload = payload },
                RayIdFormatType.GuidV4T => new RayIdContext(RayId.V4.Create(RayId, RayId.Settings.time.Now, flags), Id, Timestamp) { SafePayload = payload },
                RayIdFormatType.GuidV7 => new RayIdContext(RayId.V7.Create(RayId, RayId.NewSpanId(), flags), Id, SpanId) { SafePayload = payload },
                RayIdFormatType.GuidV7T => new RayIdContext(RayId.V7.Create(RayId, RayId.Settings.time.Now, flags), Id, Timestamp) { SafePayload = payload },
                RayIdFormatType.W3C => new RayIdContext(RayId.W3C.Create(RayId, RayId.NewSpanId(), flags), Id, SpanId) { SafePayload = payload },
                RayIdFormatType.W3CT => new RayIdContext(RayId.W3C.Create(RayId, RayId.Settings.time.Now, flags), Id, Timestamp) { SafePayload = payload },
                RayIdFormatType.Custom => RayId.Custom.Continue(this, default, flags, payload),
                RayIdFormatType.TCustom => RayId.Custom.Continue(this, RayId.Settings.time.Now, flags, payload),
                _ => throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(format, nameof(format), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue()
        {
            return IsEmptyOrInvalid ? Next(Flags, SafePayload) : Core(Flags, SafePayload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue()
        {
            return Continue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue(RayIdPayload? payload)
        {
            return IsEmptyOrInvalid ? Next(Flags, payload) : Core(Flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue(RayIdPayload? payload)
        {
            return Continue(payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue(Boolean flags)
        {
            return IsEmptyOrInvalid ? Next(flags, SafePayload) : Core(flags ? Flags : RayIdFlags.None, SafePayload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue(Boolean flags)
        {
            return Continue(flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue(Boolean flags, RayIdPayload? payload)
        {
            return IsEmptyOrInvalid ? Next(flags, payload) : Core(flags ? Flags : RayIdFlags.None, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue(Boolean flags, RayIdPayload? payload)
        {
            return Continue(flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue(RayIdFlags flags)
        {
            return IsEmptyOrInvalid ? Next(flags, SafePayload) : Core(flags, SafePayload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue(RayIdFlags flags)
        {
            return Continue(flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue(RayIdFlags flags, RayIdPayload? payload)
        {
            return IsEmptyOrInvalid ? Next(flags, payload) : Core(flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue(RayIdFlags flags, RayIdPayload? payload)
        {
            return Continue(flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(T value) where T : notnull
        {
            return New(value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(T value, RayIdPayload? payload) where T : notnull
        {
            return New(value, RayIdFlags.None, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(in T value, RayIdPayload? payload) where T : notnull
        {
            return New(in value, RayIdFlags.None, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(T value, RayIdFlags flags) where T : notnull
        {
            return New(value, flags, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(in T value, RayIdFlags flags) where T : notnull
        {
            return New(in value, flags, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull
        {
            return New(RayId.Settings.@default, value, flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(in T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull
        {
            return New(RayId.Settings.@default, in value, flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(RayIdFormatType format, T value) where T : notnull
        {
            return New(format, value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(RayIdFormatType format, in T value) where T : notnull
        {
            return New(format, in value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(RayIdFormatType format, T value, RayIdPayload? payload) where T : notnull
        {
            return New(format, value, RayIdFlags.None, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(RayIdFormatType format, in T value, RayIdPayload? payload) where T : notnull
        {
            return New(format, in value, RayIdFlags.None, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(RayIdFormatType format, T value, RayIdFlags flags) where T : notnull
        {
            return New(format, value, flags, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(RayIdFormatType format, in T value, RayIdFlags flags) where T : notnull
        {
            return New(format, in value, flags, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static RayIdContext New<T>(RayIdFormatType format, T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull
        {
            return format switch
            {
                RayIdFormatType.GuidV4 => new RayIdContext(RayId.V4.Create(RayId.NewSpanId(), flags, RayId.Hashing(value)), default, default(UInt64)) { SafePayload = payload },
                RayIdFormatType.GuidV4T => new RayIdContext(RayId.V4.Create(RayId.Settings.time.Now, flags, RayId.Hashing(value)), default, default(DateTime)) { SafePayload = payload },
                RayIdFormatType.GuidV7 => new RayIdContext(RayId.V7.Create(RayId.NewSpanId(), flags, RayId.Hashing(value)), default, default(UInt64)) { SafePayload = payload },
                RayIdFormatType.GuidV7T => new RayIdContext(RayId.V7.Create(RayId.Settings.time.Now, flags, RayId.Hashing(value)), default, default(DateTime)) { SafePayload = payload },
                RayIdFormatType.W3C => new RayIdContext(RayId.W3C.Create(RayId.NewSpanId(), flags, RayId.Hashing(value)), default, default(UInt64)) { SafePayload = payload },
                RayIdFormatType.W3CT => new RayIdContext(RayId.W3C.Create(RayId.Settings.time.Now, flags, RayId.Hashing(value)), default, default(DateTime)) { SafePayload = payload },
                RayIdFormatType.Custom => RayId.Custom.Context(default, value, default, flags, payload),
                RayIdFormatType.TCustom => RayId.Custom.Context(default, value, RayId.Settings.time.Now, flags, payload),
                _ => throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(format, nameof(format), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static RayIdContext New<T>(RayIdFormatType format, in T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull
        {
            return format switch
            {
                RayIdFormatType.GuidV4 => new RayIdContext(RayId.V4.Create(RayId.NewSpanId(), flags, RayId.Hashing(in value)), default, default(UInt64)) { SafePayload = payload },
                RayIdFormatType.GuidV4T => new RayIdContext(RayId.V4.Create(RayId.Settings.time.Now, flags, RayId.Hashing(in value)), default, default(DateTime)) { SafePayload = payload },
                RayIdFormatType.GuidV7 => new RayIdContext(RayId.V7.Create(RayId.NewSpanId(), flags, RayId.Hashing(in value)), default, default(UInt64)) { SafePayload = payload },
                RayIdFormatType.GuidV7T => new RayIdContext(RayId.V7.Create(RayId.Settings.time.Now, flags, RayId.Hashing(in value)), default, default(DateTime)) { SafePayload = payload },
                RayIdFormatType.W3C => new RayIdContext(RayId.W3C.Create(RayId.NewSpanId(), flags, RayId.Hashing(in value)), default, default(UInt64)) { SafePayload = payload },
                RayIdFormatType.W3CT => new RayIdContext(RayId.W3C.Create(RayId.Settings.time.Now, flags, RayId.Hashing(in value)), default, default(DateTime)) { SafePayload = payload },
                RayIdFormatType.Custom => RayId.Custom.Context(default, in value, default, flags, payload),
                RayIdFormatType.TCustom => RayId.Custom.Context(default, in value, RayId.Settings.time.Now, flags, payload),
                _ => throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(format, nameof(format), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(RayId parent, T value) where T : notnull
        {
            return New(parent, value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(RayId parent, in T value) where T : notnull
        {
            return New(parent, value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(RayId parent, T value, RayIdPayload? payload) where T : notnull
        {
            return New(parent, value, RayIdFlags.None, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(RayId parent, in T value, RayIdPayload? payload) where T : notnull
        {
            return New(parent, value, RayIdFlags.None, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(RayId parent, T value, RayIdFlags flags) where T : notnull
        {
            return New(parent, value, flags, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext New<T>(RayId parent, in T value, RayIdFlags flags) where T : notnull
        {
            return New(parent, value, flags, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static RayIdContext New<T>(RayId parent, T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull
        {
            RayIdFormatType format;
            return (format = parent ? parent.FormatType : RayIdFormatType.Default) switch
            {
                RayIdFormatType.GuidV4 => new RayIdContext(RayId.V4.Create(RayId.NewSpanId(), flags, RayId.Hashing(value)), parent.Id, parent.SpanId) { SafePayload = payload },
                RayIdFormatType.GuidV4T => new RayIdContext(RayId.V4.Create(RayId.Settings.time.Now, flags, RayId.Hashing(value)), parent.Id, parent.SpanId) { SafePayload = payload },
                RayIdFormatType.GuidV7 => new RayIdContext(RayId.V7.Create(RayId.NewSpanId(), flags, RayId.Hashing(value)), parent.Id, parent.SpanId) { SafePayload = payload },
                RayIdFormatType.GuidV7T => new RayIdContext(RayId.V7.Create(RayId.Settings.time.Now, flags, RayId.Hashing(value)), parent.Id, parent.SpanId) { SafePayload = payload },
                RayIdFormatType.W3C => new RayIdContext(RayId.W3C.Create(RayId.NewSpanId(), flags, RayId.Hashing(value)), parent.Id, parent.SpanId) { SafePayload = payload },
                RayIdFormatType.W3CT => new RayIdContext(RayId.W3C.Create(RayId.Settings.time.Now, flags, RayId.Hashing(value)), parent.Id, parent.SpanId) { SafePayload = payload },
                RayIdFormatType.Custom => RayId.Custom.Context(parent, value, default, flags, payload),
                RayIdFormatType.TCustom => RayId.Custom.Context(parent, value, RayId.Settings.time.Now, flags, payload),
                _ => throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(format, nameof(format), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static RayIdContext New<T>(RayId parent, in T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull
        {
            RayIdFormatType format;
            return (format = parent ? parent.FormatType : RayIdFormatType.Default) switch
            {
                RayIdFormatType.GuidV4 => new RayIdContext(RayId.V4.Create(RayId.NewSpanId(), flags, RayId.Hashing(in value)), parent.Id, parent.SpanId) { SafePayload = payload },
                RayIdFormatType.GuidV4T => new RayIdContext(RayId.V4.Create(RayId.Settings.time.Now, flags, RayId.Hashing(in value)), parent.Id, parent.SpanId) { SafePayload = payload },
                RayIdFormatType.GuidV7 => new RayIdContext(RayId.V7.Create(RayId.NewSpanId(), flags, RayId.Hashing(in value)), parent.Id, parent.SpanId) { SafePayload = payload },
                RayIdFormatType.GuidV7T => new RayIdContext(RayId.V7.Create(RayId.Settings.time.Now, flags, RayId.Hashing(in value)), parent.Id, parent.SpanId) { SafePayload = payload },
                RayIdFormatType.W3C => new RayIdContext(RayId.W3C.Create(RayId.NewSpanId(), flags, RayId.Hashing(in value)), parent.Id, parent.SpanId) { SafePayload = payload },
                RayIdFormatType.W3CT => new RayIdContext(RayId.W3C.Create(RayId.Settings.time.Now, flags, RayId.Hashing(in value)), parent.Id, parent.SpanId) { SafePayload = payload },
                RayIdFormatType.Custom => RayId.Custom.Context(parent, in value, default, flags, payload),
                RayIdFormatType.TCustom => RayId.Custom.Context(parent, in value, RayId.Settings.time.Now, flags, payload),
                _ => throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(format, nameof(format), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next<T>(T value) where T : notnull
        {
            return Next(value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next<T>(T value)
        {
            return Next(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next<T>(in T value) where T : notnull
        {
            return Next(in value, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next<T>(in T value)
        {
            return Next(in value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next<T>(T value, RayIdPayload? payload) where T : notnull
        {
            return New(RayId, value, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next<T>(T value, RayIdPayload? payload)
        {
            return Next(value, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next<T>(in T value, RayIdPayload? payload) where T : notnull
        {
            return New(RayId, in value, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next<T>(in T value, RayIdPayload? payload)
        {
            return Next(in value, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next<T>(T value, Boolean flags) where T : notnull
        {
            return Next(value, flags, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next<T>(T value, Boolean flags)
        {
            return Next(value, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next<T>(in T value, Boolean flags) where T : notnull
        {
            return Next(in value, flags, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next<T>(in T value, Boolean flags)
        {
            return Next(in value, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next<T>(T value, Boolean flags, RayIdPayload? payload) where T : notnull
        {
            return New(RayId, value, flags ? Flags : RayIdFlags.None, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next<T>(T value, Boolean flags, RayIdPayload? payload)
        {
            return Next(value, flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next<T>(in T value, Boolean flags, RayIdPayload? payload) where T : notnull
        {
            return New(RayId, in value, flags ? Flags : RayIdFlags.None, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next<T>(in T value, Boolean flags, RayIdPayload? payload)
        {
            return Next(in value, flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next<T>(T value, RayIdFlags flags) where T : notnull
        {
            return Next(value, flags, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next<T>(T value, RayIdFlags flags)
        {
            return Next(value, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next<T>(in T value, RayIdFlags flags) where T : notnull
        {
            return Next(in value, flags, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next<T>(in T value, RayIdFlags flags)
        {
            return Next(in value, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next<T>(T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull
        {
            return New(RayId, value, flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next<T>(T value, RayIdFlags flags, RayIdPayload? payload)
        {
            return Next(value, flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Next<T>(in T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull
        {
            return New(RayId, in value, flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Next<T>(in T value, RayIdFlags flags, RayIdPayload? payload)
        {
            return Next(in value, flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private readonly RayIdContext Core<T>(T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull
        {
            RayIdFormatType format;
            return (format = FormatType) switch
            {
                RayIdFormatType.GuidV4 => new RayIdContext(RayId.V4.Create(RayId, RayId.NewSpanId(), flags, RayId.Hashing(value)), Id, SpanId) { SafePayload = payload },
                RayIdFormatType.GuidV4T => new RayIdContext(RayId.V4.Create(RayId, RayId.Settings.time.Now, flags, RayId.Hashing(value)), Id, Timestamp) { SafePayload = payload },
                RayIdFormatType.GuidV7 => new RayIdContext(RayId.V7.Create(RayId, RayId.NewSpanId(), flags, RayId.Hashing(value)), Id, SpanId) { SafePayload = payload },
                RayIdFormatType.GuidV7T => new RayIdContext(RayId.V7.Create(RayId, RayId.Settings.time.Now, flags, RayId.Hashing(value)), Id, Timestamp) { SafePayload = payload },
                RayIdFormatType.W3C => new RayIdContext(RayId.W3C.Create(RayId, RayId.NewSpanId(), flags, RayId.Hashing(value)), Id, SpanId) { SafePayload = payload },
                RayIdFormatType.W3CT => new RayIdContext(RayId.W3C.Create(RayId, RayId.Settings.time.Now, flags, RayId.Hashing(value)), Id, Timestamp) { SafePayload = payload },
                RayIdFormatType.Custom => RayId.Custom.Continue(this, value, default, flags, payload),
                RayIdFormatType.TCustom => RayId.Custom.Continue(this, value, RayId.Settings.time.Now, flags, payload),
                _ => throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(format, nameof(format), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private readonly RayIdContext Core<T>(in T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull
        {
            RayIdFormatType format;
            return (format = FormatType) switch
            {
                RayIdFormatType.GuidV4 => new RayIdContext(RayId.V4.Create(RayId, RayId.NewSpanId(), flags, RayId.Hashing(in value)), Id, SpanId) { SafePayload = payload },
                RayIdFormatType.GuidV4T => new RayIdContext(RayId.V4.Create(RayId, RayId.Settings.time.Now, flags, RayId.Hashing(in value)), Id, Timestamp) { SafePayload = payload },
                RayIdFormatType.GuidV7 => new RayIdContext(RayId.V7.Create(RayId, RayId.NewSpanId(), flags, RayId.Hashing(in value)), Id, SpanId) { SafePayload = payload },
                RayIdFormatType.GuidV7T => new RayIdContext(RayId.V7.Create(RayId, RayId.Settings.time.Now, flags, RayId.Hashing(in value)), Id, Timestamp) { SafePayload = payload },
                RayIdFormatType.W3C => new RayIdContext(RayId.W3C.Create(RayId, RayId.NewSpanId(), flags, RayId.Hashing(in value)), Id, SpanId) { SafePayload = payload },
                RayIdFormatType.W3CT => new RayIdContext(RayId.W3C.Create(RayId, RayId.Settings.time.Now, flags, RayId.Hashing(in value)), Id, Timestamp) { SafePayload = payload },
                RayIdFormatType.Custom => RayId.Custom.Continue(this, in value, default, flags, payload),
                RayIdFormatType.TCustom => RayId.Custom.Continue(this, in value, RayId.Settings.time.Now, flags, payload),
                _ => throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(format, nameof(format), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue<T>(T value) where T : notnull
        {
            return IsEmptyOrInvalid ? Next(value, Flags, SafePayload) : Core(value, Flags, SafePayload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue<T>(T value)
        {
            return Continue(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue<T>(in T value) where T : notnull
        {
            return IsEmptyOrInvalid ? Next(in value, Flags, SafePayload) : Core(in value, Flags, SafePayload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue<T>(in T value)
        {
            return Continue(in value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue<T>(T value, RayIdPayload? payload) where T : notnull
        {
            return IsEmptyOrInvalid ? Next(value, Flags, payload) : Core(value, Flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue<T>(T value, RayIdPayload? payload)
        {
            return Continue(value, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue<T>(in T value, RayIdPayload? payload) where T : notnull
        {
            return IsEmptyOrInvalid ? Next(in value, Flags, payload) : Core(in value, Flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue<T>(in T value, RayIdPayload? payload)
        {
            return Continue(in value, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue<T>(T value, Boolean flags) where T : notnull
        {
            return IsEmptyOrInvalid ? Next(value, flags, SafePayload) : Core(value, flags ? Flags : RayIdFlags.None, SafePayload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue<T>(T value, Boolean flags)
        {
            return Continue(value, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue<T>(in T value, Boolean flags) where T : notnull
        {
            return IsEmptyOrInvalid ? Next(in value, flags, SafePayload) : Core(in value, flags ? Flags : RayIdFlags.None, SafePayload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue<T>(in T value, Boolean flags)
        {
            return Continue(in value, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue<T>(T value, Boolean flags, RayIdPayload? payload) where T : notnull
        {
            return IsEmptyOrInvalid ? Next(value, flags, payload) : Core(value, flags ? Flags : RayIdFlags.None, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue<T>(T value, Boolean flags, RayIdPayload? payload)
        {
            return Continue(value, flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue<T>(in T value, Boolean flags, RayIdPayload? payload) where T : notnull
        {
            return IsEmptyOrInvalid ? Next(in value, flags, payload) : Core(in value, flags ? Flags : RayIdFlags.None, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue<T>(in T value, Boolean flags, RayIdPayload? payload)
        {
            return Continue(in value, flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue<T>(T value, RayIdFlags flags) where T : notnull
        {
            return IsEmptyOrInvalid ? Next(value, flags, SafePayload) : Core(value, flags, SafePayload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue<T>(T value, RayIdFlags flags)
        {
            return Continue(value, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue<T>(in T value, RayIdFlags flags) where T : notnull
        {
            return IsEmptyOrInvalid ? Next(in value, flags, SafePayload) : Core(in value, flags, SafePayload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue<T>(in T value, RayIdFlags flags)
        {
            return Continue(in value, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue<T>(T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull
        {
            return IsEmptyOrInvalid ? Next(value, flags, payload) : Core(value, flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue<T>(T value, RayIdFlags flags, RayIdPayload? payload)
        {
            return Continue(value, flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Continue<T>(in T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull
        {
            return IsEmptyOrInvalid ? Next(in value, flags, payload) : Core(in value, flags, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IRayId IRayId.Continue<T>(in T value, RayIdFlags flags, RayIdPayload? payload)
        {
            return Continue(in value, flags, payload);
        }
    }
}