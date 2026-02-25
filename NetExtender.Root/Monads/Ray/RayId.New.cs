using System.Runtime.CompilerServices;
using NetExtender.Exceptions;

namespace NetExtender.Monads
{
    public partial struct RayId
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New()
        {
            return New(RayIdFlags.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New(RayIdFlags flags)
        {
            return New(Settings.@default, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New(RayIdFormatType format)
        {
            return New(format, RayIdFlags.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static RayId New(RayIdFormatType format, RayIdFlags flags)
        {
            return format switch
            {
                RayIdFormatType.GuidV4 => V4.Create(NewSpanId(), flags),
                RayIdFormatType.GuidV4T => V4.Create(Settings.time.Now, flags),
                RayIdFormatType.GuidV7 => V7.Create(NewSpanId(), flags),
                RayIdFormatType.GuidV7T => V7.Create(Settings.time.Now, flags),
                RayIdFormatType.W3C => W3C.Create(NewSpanId(), flags),
                RayIdFormatType.W3CT => W3C.Create(Settings.time.Now, flags),
                RayIdFormatType.Custom => Custom.New(default, default, flags),
                RayIdFormatType.TCustom => Custom.New(default, Settings.time.Now, flags),
                _ => throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(format, nameof(format), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New(RayId parent)
        {
            return New(parent, RayIdFlags.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New(RayId parent, RayIdFlags flags)
        {
            return New(parent.Format, parent, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New(RayIdFormatType format, RayId parent)
        {
            return New(format, parent, RayIdFlags.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static RayId New(RayIdFormatType format, RayId parent, RayIdFlags flags)
        {
            return format switch
            {
                RayIdFormatType.GuidV4 => V4.Create(parent, NewSpanId(), flags),
                RayIdFormatType.GuidV4T => V4.Create(parent, Settings.time.Now, flags),
                RayIdFormatType.GuidV7 => V7.Create(parent, NewSpanId(), flags),
                RayIdFormatType.GuidV7T => V7.Create(parent, Settings.time.Now, flags),
                RayIdFormatType.W3C => W3C.Create(parent, NewSpanId(), flags),
                RayIdFormatType.W3CT => W3C.Create(parent, Settings.time.Now, flags),
                RayIdFormatType.Custom => Custom.New(parent, default, flags),
                RayIdFormatType.TCustom => Custom.New(parent, Settings.time.Now, flags),
                _ => throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(format, nameof(format), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New<T>(T value) where T : notnull
        {
            return New(value, RayIdFlags.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New<T>(in T value) where T : notnull
        {
            return New(in value, RayIdFlags.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New<T>(T value, RayIdFlags flags) where T : notnull
        {
            return New(Settings.@default, value, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New<T>(in T value, RayIdFlags flags) where T : notnull
        {
            return New(Settings.@default, in value, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New<T>(RayIdFormatType format, T value) where T : notnull
        {
            return New(format, value, RayIdFlags.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New<T>(RayIdFormatType format, in T value) where T : notnull
        {
            return New(format, in value, RayIdFlags.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static RayId New<T>(RayIdFormatType format, T value, RayIdFlags flags) where T : notnull
        {
            return format switch
            {
                RayIdFormatType.GuidV4 => V4.Create(NewSpanId(), flags, Hashing(value)),
                RayIdFormatType.GuidV4T => V4.Create(Settings.time.Now, flags, Hashing(value)),
                RayIdFormatType.GuidV7 => V7.Create(NewSpanId(), flags, Hashing(value)),
                RayIdFormatType.GuidV7T => V7.Create(Settings.time.Now, flags, Hashing(value)),
                RayIdFormatType.W3C => W3C.Create(NewSpanId(), flags, Hashing(value)),
                RayIdFormatType.W3CT => W3C.Create(Settings.time.Now, flags, Hashing(value)),
                RayIdFormatType.Custom => Custom.New(default, value, default, flags),
                RayIdFormatType.TCustom => Custom.New(default, value, Settings.time.Now, flags),
                _ => throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(format, nameof(format), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static RayId New<T>(RayIdFormatType format, in T value, RayIdFlags flags) where T : notnull
        {
            return format switch
            {
                RayIdFormatType.GuidV4 => V4.Create(NewSpanId(), flags, Hashing(in value)),
                RayIdFormatType.GuidV4T => V4.Create(Settings.time.Now, flags, Hashing(in value)),
                RayIdFormatType.GuidV7 => V7.Create(NewSpanId(), flags, Hashing(in value)),
                RayIdFormatType.GuidV7T => V7.Create(Settings.time.Now, flags, Hashing(in value)),
                RayIdFormatType.W3C => W3C.Create(NewSpanId(), flags, Hashing(in value)),
                RayIdFormatType.W3CT => W3C.Create(Settings.time.Now, flags, Hashing(in value)),
                RayIdFormatType.Custom => Custom.New(default, in value, default, flags),
                RayIdFormatType.TCustom => Custom.New(default, in value, Settings.time.Now, flags),
                _ => throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(format, nameof(format), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New<T>(T value, RayId parent) where T : notnull
        {
            return New(value, parent, RayIdFlags.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New<T>(in T value, RayId parent) where T : notnull
        {
            return New(in value, parent, RayIdFlags.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New<T>(T value, RayId parent, RayIdFlags flags) where T : notnull
        {
            return New(parent.FormatType, value, parent, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New<T>(in T value, RayId parent, RayIdFlags flags) where T : notnull
        {
            return New(parent.FormatType, in value, parent, flags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New<T>(RayIdFormatType format, T value, RayId parent) where T : notnull
        {
            return New(format, value, parent, RayIdFlags.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId New<T>(RayIdFormatType format, in T value, RayId parent) where T : notnull
        {
            return New(format, value, parent, RayIdFlags.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static RayId New<T>(RayIdFormatType format, T value, RayId parent, RayIdFlags flags) where T : notnull
        {
            return format switch
            {
                RayIdFormatType.GuidV4 => V4.Create(parent, NewSpanId(), flags, Hashing(value)),
                RayIdFormatType.GuidV4T => V4.Create(parent, Settings.time.Now, flags, Hashing(value)),
                RayIdFormatType.GuidV7 => V7.Create(parent, NewSpanId(), flags, Hashing(value)),
                RayIdFormatType.GuidV7T => V7.Create(parent, Settings.time.Now, flags, Hashing(value)),
                RayIdFormatType.W3C => W3C.Create(parent, NewSpanId(), flags, Hashing(value)),
                RayIdFormatType.W3CT => W3C.Create(parent, Settings.time.Now, flags, Hashing(value)),
                RayIdFormatType.Custom => Custom.New(parent, value, default, flags),
                RayIdFormatType.TCustom => Custom.New(parent, value, Settings.time.Now, flags),
                _ => throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(format, nameof(format), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static RayId New<T>(RayIdFormatType format, in T value, RayId parent, RayIdFlags flags) where T : notnull
        {
            return format switch
            {
                RayIdFormatType.GuidV4 => V4.Create(parent, NewSpanId(), flags, Hashing(in value)),
                RayIdFormatType.GuidV4T => V4.Create(parent, Settings.time.Now, flags, Hashing(in value)),
                RayIdFormatType.GuidV7 => V7.Create(parent, NewSpanId(), flags, Hashing(in value)),
                RayIdFormatType.GuidV7T => V7.Create(parent, Settings.time.Now, flags, Hashing(in value)),
                RayIdFormatType.W3C => W3C.Create(parent, NewSpanId(), flags, Hashing(in value)),
                RayIdFormatType.W3CT => W3C.Create(parent, Settings.time.Now, flags, Hashing(in value)),
                RayIdFormatType.Custom => Custom.New(parent, in value, default, flags),
                RayIdFormatType.TCustom => Custom.New(parent, in value, Settings.time.Now, flags),
                _ => throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(format, nameof(format), null)
            };
        }
    }
}