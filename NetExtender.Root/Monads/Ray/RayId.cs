using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using NetExtender.Exceptions;
using NetExtender.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

#pragma warning disable CA2211

namespace NetExtender.Monads
{
    [SuppressMessage("Design", "CA1069")]
    public enum RayIdFormat : Byte
    {
        GuidV4 = 0,
        GuidV7 = 1,
        W3C = 2,
        Custom = 3
    }

#pragma warning disable CA1069
    [SuppressMessage("ReSharper", "ShiftExpressionZeroLeftOperand")]
    public enum RayIdFormatType : Byte
    {
#if NET9_0_OR_GREATER
        Default = GuidV7,
#else
        Default = GuidV4T,
#endif
        GuidV4 = RayIdFormat.GuidV4 << 1 | 0,
        GuidV4T = RayIdFormat.GuidV4 << 1 | 1,
        GuidV7 = RayIdFormat.GuidV7 << 1 | 0,
        GuidV7T = RayIdFormat.GuidV7 << 1 | 1,
        W3C = RayIdFormat.W3C << 1 | 0,
        W3CT = RayIdFormat.W3C << 1 | 1,
        Custom = RayIdFormat.Custom << 1 | 0,
        TCustom = RayIdFormat.Custom << 1 | 1
    }
#pragma warning restore CA1069

    public enum RayIdFlags : Byte
    {
        None = 0,
        Sampling = 1,
        RandomTrace = 2,
        Synthetic = 4,
        Retry = 8,
        Background = 16,
        Internal = 32,
        Canary = 64,
        Sensitive = 128
    }

    [StructLayout(LayoutKind.Explicit, Size = Settings.Size)]
    public unsafe partial struct RayId : IRayIdInfo, IEqualityStruct<RayId>, IEquality<RayIdContext>, IEquality<Guid>, IEquality<String>, IEquality<DateTime>, ICloneable<RayId>
    {
        public static implicit operator Boolean(RayId value)
        {
            return !value.IsEmptyOrInvalid;
        }

        public static implicit operator Guid(RayId value)
        {
            return value.Id;
        }

        public static Boolean operator ==(RayId first, String? second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(RayId first, String? second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(RayId first, RayId second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(RayId first, RayId second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(RayId first, RayIdContext second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(RayId first, RayIdContext second)
        {
            return !(first == second);
        }

        public static Boolean operator <(RayId first, String? second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(RayId first, String? second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(RayId first, String? second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(RayId first, String? second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(RayId first, RayId second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(RayId first, RayId second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(RayId first, RayId second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(RayId first, RayId second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(RayId first, RayIdContext second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(RayId first, RayIdContext second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(RayId first, RayIdContext second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(RayId first, RayIdContext second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static readonly RayId Empty = default;

        internal enum Mask : Byte
        {
            Format = 0b11000000,
            FormatType = 0b11100000,
            Timestamp = 0b00100000,
            ServerGenerated = 0b00010000,
            ServerMask = 0b00001110,
            UsingHash = 0b00000001
        }

        [FieldOffset(0)] internal readonly Byte Metadata = default;
        [FieldOffset(0)] internal readonly Container This;
        [FieldOffset(0)] private fixed Byte Token[Settings.Size];
        [FieldOffset(0)] private fixed UInt64 Data[Settings.Size / sizeof(UInt64)];

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly RayIdFormat Format
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return unchecked((RayIdFormat) ((Metadata & (Byte) Mask.Format) >> 6));
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly RayIdFormatType FormatType
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return unchecked((RayIdFormatType) ((Metadata & (Byte) Mask.FormatType) >> 5));
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        readonly String IRayIdInfo.Format
        {
            get
            {
                return FormatType.ToString();
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly Boolean HasTimestamp
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return (Metadata & (Byte) Mask.Timestamp) is not 0;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly Boolean IsServerGenerated
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return (Metadata & (Byte) Mask.ServerGenerated) is not 0;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly Boolean IsUsingHash
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return (Metadata & (Byte) Mask.UsingHash) is not 0;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly Byte Version
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get
            {
                switch (Format)
                {
                    case RayIdFormat.GuidV4 or RayIdFormat.GuidV7 or RayIdFormat.W3C:
                    {
                        return This.Version;
                    }
                    case RayIdFormat.Custom:
                    {
                        fixed (Byte* pointer = Token)
                        {
                            return Custom.Version(Unwrap(pointer));
                        }
                    }
                    case var format:
                    {
                        throw new EnumUndefinedOrNotSupportedException<RayIdFormat>(format, nameof(format), null);
                    }
                }
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly (UInt16 Server, UInt16 Service)? Service
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get
            {
                switch (Format)
                {
                    case RayIdFormat.GuidV4 or RayIdFormat.GuidV7 or RayIdFormat.W3C:
                    {
                        return This.GetService((Byte) ((Metadata & (Byte) Mask.ServerMask) >> 1));
                    }
                    case RayIdFormat.Custom:
                    {
                        fixed (Byte* pointer = Token)
                        {
                            return Custom.Service(Unwrap(pointer));
                        }
                    }
                    case var format:
                    {
                        throw new EnumUndefinedOrNotSupportedException<RayIdFormat>(format, nameof(format), null);
                    }
                }
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly Guid Id
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get
            {
                switch (Format)
                {
                    case RayIdFormat.GuidV4 or RayIdFormat.GuidV7 or RayIdFormat.W3C:
                    {
                        return This.Id;
                    }
                    case RayIdFormat.Custom:
                    {
                        fixed (Byte* pointer = Token)
                        {
                            return Custom.Id(Unwrap(pointer));
                        }
                    }
                    case var format:
                    {
                        throw new EnumUndefinedOrNotSupportedException<RayIdFormat>(format, nameof(format), null);
                    }
                }
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly UInt64 SpanId
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get
            {
                switch (FormatType)
                {
                    case RayIdFormatType.GuidV4 or RayIdFormatType.GuidV7 or RayIdFormatType.W3C:
                    {
                        return This.SpanId;
                    }
                    case RayIdFormatType.GuidV4T or RayIdFormatType.GuidV7T or RayIdFormatType.W3CT:
                    {
                        return ToSpanId(This.Timestamp);
                    }
                    case RayIdFormatType.Custom:
                    {
                        fixed (Byte* pointer = Token)
                        {
                            return Custom.SpanId(Unwrap(pointer));
                        }
                    }
                    case RayIdFormatType.TCustom:
                    {
                        fixed (Byte* pointer = Token)
                        {
                            return ToSpanId(Custom.Timestamp(Unwrap(pointer)));
                        }
                    }
                    case var format:
                    {
                        throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(format, nameof(format), null);
                    }
                }
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly DateTime Timestamp
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get
            {
                switch (FormatType)
                {
                    case RayIdFormatType.GuidV4 or RayIdFormatType.GuidV7 or RayIdFormatType.W3C:
                    {
                        return default;
                    }
                    case RayIdFormatType.GuidV4T or RayIdFormatType.GuidV7T or RayIdFormatType.W3CT:
                    {
                        return This.Timestamp;
                    }
                    case RayIdFormatType.Custom or RayIdFormatType.TCustom:
                    {
                        fixed (Byte* pointer = Token)
                        {
                            return Custom.Timestamp(Unwrap(pointer));
                        }
                    }
                    case var format:
                    {
                        throw new EnumUndefinedOrNotSupportedException<RayIdFormatType>(format, nameof(format), null);
                    }
                }
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        readonly Guid IRayIdInfo.ParentId
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get
            {
                switch (Format)
                {
                    case RayIdFormat.GuidV4 or RayIdFormat.GuidV7 or RayIdFormat.W3C:
                    {
                        return default;
                    }
                    case RayIdFormat.Custom:
                    {
                        fixed (Byte* pointer = Token)
                        {
                            return Custom.ParentId(Unwrap(pointer));
                        }
                    }
                    case var format:
                    {
                        throw new EnumUndefinedOrNotSupportedException<RayIdFormat>(format, nameof(format), null);
                    }
                }
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        readonly UInt64 IRayIdInfo.ParentSpanId
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get
            {
                switch (Format)
                {
                    case RayIdFormat.GuidV4 or RayIdFormat.GuidV7 or RayIdFormat.W3C:
                    {
                        return default;
                    }
                    case RayIdFormat.Custom:
                    {
                        fixed (Byte* pointer = Token)
                        {
                            return Custom.ParentSpanId(Unwrap(pointer));
                        }
                    }
                    case var format:
                    {
                        throw new EnumUndefinedOrNotSupportedException<RayIdFormat>(format, nameof(format), null);
                    }
                }
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        readonly DateTime IRayIdInfo.ParentTimestamp
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get
            {
                switch (Format)
                {
                    case RayIdFormat.GuidV4 or RayIdFormat.GuidV7 or RayIdFormat.W3C:
                    {
                        return default;
                    }
                    case RayIdFormat.Custom:
                    {
                        fixed (Byte* pointer = Token)
                        {
                            return Custom.ParentTimestamp(Unwrap(pointer));
                        }
                    }
                    case var format:
                    {
                        throw new EnumUndefinedOrNotSupportedException<RayIdFormat>(format, nameof(format), null);
                    }
                }
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly RayIdFlags Flags
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get
            {
                switch (Format)
                {
                    case RayIdFormat.GuidV4 or RayIdFormat.GuidV7 or RayIdFormat.W3C:
                    {
                        return This.Flags;
                    }
                    case RayIdFormat.Custom:
                    {
                        fixed (Byte* pointer = Token)
                        {
                            return Custom.Flags(Unwrap(pointer));
                        }
                    }
                    case var format:
                    {
                        throw new EnumUndefinedOrNotSupportedException<RayIdFormat>(format, nameof(format), null);
                    }
                }
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly UInt32? Hash
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get
            {
                switch (Format)
                {
                    case RayIdFormat.GuidV4 or RayIdFormat.GuidV7 or RayIdFormat.W3C:
                    {
                        return This.Info & 0x00FFFFFF;
                    }
                    case RayIdFormat.Custom:
                    {
                        fixed (Byte* pointer = Token)
                        {
                            return Custom.Hash(Unwrap(pointer));
                        }
                    }
                    case var format:
                    {
                        throw new EnumUndefinedOrNotSupportedException<RayIdFormat>(format, nameof(format), null);
                    }
                }
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly UInt32 Info
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get
            {
                switch (Format)
                {
                    case RayIdFormat.GuidV4 or RayIdFormat.GuidV7 or RayIdFormat.W3C:
                    {
                        return This.Info;
                    }
                    case RayIdFormat.Custom:
                    {
                        fixed (Byte* pointer = Token)
                        {
                            return Custom.Info(Unwrap(pointer));
                        }
                    }
                    case var format:
                    {
                        throw new EnumUndefinedOrNotSupportedException<RayIdFormat>(format, nameof(format), null);
                    }
                }
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public RayIdPayload? Payload
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return null;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly Boolean IsInvalid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return !IsEmpty && Id == default;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get
            {
                if (Avx2.IsSupported)
                {
                    fixed (Byte* pointer = Token)
                    {
                        Vector256<Byte> compare = Avx2.CompareEqual(Unsafe.ReadUnaligned<Vector256<Byte>>(pointer), Vector256<Byte>.Zero);
                        return Avx2.MoveMask(compare.AsSByte()) == -1;
                    }
                }

                return (Data[0] | Data[1] | Data[2] | Data[3]) is 0;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly Boolean IsEmptyOrInvalid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Id == default;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RayId(void* value)
            : this((Container*) value)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RayId(Container* container)
        {
            This = *container;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Span<Byte> Unwrap(Byte* token)
        {
            return new Span<Byte>(token + 1, Settings.Size - 1);
        }

        public static RayId From(ReadOnlySpan<Byte> token)
        {
            return token.Length == Settings.Size ? new RayId(Unsafe.AsPointer(ref MemoryMarshal.GetReference(token))) : throw new ArgumentException("Token length must be equal to 32 bytes.", nameof(token));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId From(ReadOnlySpan<Char> token)
        {
            return From(token, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId From(ReadOnlySpan<Char> token, IFormatProvider? provider)
        {
            return TryParse(token, false, provider, out RayId result) ? result : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId FromDirect(ReadOnlySpan<Char> token)
        {
            return FromDirect(token, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayId FromDirect(ReadOnlySpan<Char> token, IFormatProvider? provider)
        {
            return TryParse(token, true, provider, out RayId result) ? result : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> token, out RayId result)
        {
            return TryParse(token, null, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> token, IFormatProvider? provider, out RayId result)
        {
            return TryParse(token, false, provider, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParseDirect(ReadOnlySpan<Char> token, out RayId result)
        {
            return TryParseDirect(token, null, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParseDirect(ReadOnlySpan<Char> token, IFormatProvider? provider, out RayId result)
        {
            return TryParse(token, true, provider, out result);
        }

        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean TryParse(ReadOnlySpan<Char> token, Boolean direct, IFormatProvider? provider, out RayId result)
        {
            result = default;
            token = MemoryExtensions.Trim(token);
            if (MemoryExtensions.StartsWith(token, "ray-", StringComparison.InvariantCultureIgnoreCase))
            {
                token = token.Slice(4);

                if (token.Length < 32)
                {
                    return false;
                }

                ReadOnlySpan<Char> element = token.Slice(0, 32);
                if (!Guid.TryParseExact(element, "N", out Guid id))
                {
                    return false;
                }

                token = token.Slice(32);

                if (token.IsEmpty || token[0] != '<')
                {
                    return false;
                }

                token = token.Slice(1);

                Int32 end = MemoryExtensions.IndexOf(token, '>');
                if (end < 0)
                {
                    return false;
                }

                element = token.Slice(0, end);

                if (element.IsEmpty)
                {
                    return false;
                }

                DateTime? timestamp = default;
                UInt64 span = default;

                if (MemoryExtensions.IndexOfAny(element, '-', ':', 'T') >= 0)
                {
                    if (!DateTime.TryParse(element, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime value))
                    {
                        return false;
                    }

                    timestamp = value;
                }
                else
                {
                    if (!UInt64.TryParse(element, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out span))
                    {
                        return false;
                    }
                }

                token = token.Slice(end + 1);

                if (token.IsEmpty || token[0] != '-')
                {
                    return false;
                }

                token = token.Slice(1);

                if (token.IsEmpty || token[0] != 'V')
                {
                    return false;
                }

                token = token.Slice(1);

                end = 0;
                while (end < token.Length && Char.IsDigit(token[end]))
                {
                    end++;
                }

                if (end == 0)
                {
                    return false;
                }

                if (!Byte.TryParse(token.Slice(0, end), NumberStyles.None, CultureInfo.InvariantCulture, out Byte version))
                {
                    return false;
                }

                token = token.Slice(end);

                if (token.IsEmpty || token[0] != '/')
                {
                    return false;
                }

                token = token.Slice(1);

                if (token.Length < 2)
                {
                    return false;
                }

                if (!Byte.TryParse(token.Slice(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out Byte metadata))
                {
                    return false;
                }

                token = token.Slice(2);

                if (!direct)
                {
                    metadata &= (Byte) ~Mask.ServerGenerated;
                }

                (UInt16 Server, UInt16 Service)? service = null;
                if (token.Length >= 2 && token[0] == '-' && token[1] == 'S')
                {
                    token = token.Slice(2);

                    Int32 start = MemoryExtensions.IndexOf(token, ':');
                    if (start < 0)
                    {
                        return false;
                    }

                    if (!UInt16.TryParse(token.Slice(0, start), NumberStyles.None, CultureInfo.InvariantCulture, out UInt16 server))
                    {
                        return false;
                    }

                    token = token.Slice(start + 1);
                    start = MemoryExtensions.IndexOf(token, '-');

                    if (start < 0)
                    {
                        return false;
                    }

                    if (!UInt16.TryParse(token.Slice(0, start), NumberStyles.None, CultureInfo.InvariantCulture, out UInt16 value))
                    {
                        return false;
                    }

                    token = token.Slice(start);
                    service = (server, value);
                }

                if (token.IsEmpty || token[0] != '-')
                {
                    return false;
                }

                token = token.Slice(1);

                if (token.Length < 8)
                {
                    return false;
                }

                if (!UInt32.TryParse(token.Slice(0, 8), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out UInt32 info))
                {
                    return false;
                }

                token = token.Slice(8);

                if (!token.IsEmpty)
                {
                    return false;
                }

                Container container = timestamp.HasValue ? new Container(metadata, id, timestamp.Value, info) : new Container(metadata, id, span, info);
                container.Version = version;

                if (service.HasValue)
                {
                    container.Service = Container.GetService((Byte) ((metadata & (Byte) Mask.ServerMask) >> 1), service.Value.Server, service.Value.Service);
                }

                result = new RayId(&container);
                return true;
            }

            if (MemoryExtensions.StartsWith(token, "00-"))
            {
                token = token.Slice(3);

                if (token.Length < 32)
                {
                    return false;
                }

                ReadOnlySpan<Char> element = token.Slice(0, 32);
                if (!Guid.TryParseExact(element, "N", out Guid id))
                {
                    return false;
                }

                token = token.Slice(32);

                if (token.IsEmpty || token[0] != '-')
                {
                    return false;
                }

                token = token.Slice(1);

                if (token.Length < 16)
                {
                    return false;
                }

                element = token.Slice(0, 16);
                if (!UInt64.TryParse(element, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out UInt64 span))
                {
                    return false;
                }

                token = token.Slice(16);

                if (token.IsEmpty || token[0] != '-')
                {
                    return false;
                }

                token = token.Slice(1);

                if (token.Length < 2)
                {
                    return false;
                }

                element = token.Slice(0, 2);
                if (!Byte.TryParse(element, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out Byte flags))
                {
                    return false;
                }

                token = token.Slice(2);

                if (!token.IsEmpty)
                {
                    return false;
                }

                const Byte metadata = (Byte) RayIdFormat.W3C << 6;
                Container container = new Container(metadata, id, span)
                {
                    Flags = (RayIdFlags) flags
                };

                result = new RayId(&container);
                return true;
            }

            Maybe<RayIdPayload?> payload = null;
            result = Custom.TryParse(token, provider, ref payload);
            return !result.IsEmptyOrInvalid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Guid NewId()
        {
            return Guid.NewGuid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Guid NewId(DateTime timestamp)
        {
            return timestamp.NewGuid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static UInt64 NewSpanId()
        {
            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];

            start:
            Random.Shared.NextBytes(buffer);
            UInt64 result = Unsafe.As<Byte, UInt64>(ref MemoryMarshal.GetReference(buffer));

            if (result is UInt64.MinValue or UInt64.MaxValue)
            {
                goto start;
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static UInt64 ToSpanId(DateTime timestamp)
        {
            return unchecked((UInt64) timestamp.ToBinary());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static UInt32 Hashing<T>(T value) where T : notnull
        {
            const Int32 size = sizeof(UInt32);
            Span<Byte> buffer = stackalloc Byte[size];
            Settings.hasher.With(value, buffer.Slice(0, size - 1));
            return buffer.Read<UInt32>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static UInt32 Hashing<T>(in T value) where T : notnull
        {
            const Int32 size = sizeof(UInt32);
            Span<Byte> buffer = stackalloc Byte[size];
            Settings.hasher.With(in value, buffer.Slice(0, size - 1));
            return buffer.Read<UInt32>();
        }

        readonly Boolean IMonad.Unwrap(out Object? value)
        {
            if (IsEmptyOrInvalid)
            {
                value = null;
                return false;
            }

            Container container = This;
            Byte[] array = new Byte[Settings.Size];
            value = array;

            fixed (Byte* pointer = array)
            {
                Unsafe.WriteUnaligned(pointer, container);
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(Guid other)
        {
            return Id.CompareTo(other);
        }

        public readonly Int32 CompareTo(String? other)
        {
            return TryParse(other, out RayId result) ? CompareTo(result) : -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(DateTime other)
        {
            return Timestamp.CompareTo(other);
        }

        public readonly Int32 CompareTo(RayId other)
        {
            DateTime x = Timestamp;

            if (x == default)
            {
                return CompareTo(other.Id);
            }

            DateTime y = other.Timestamp;

            if (y == default)
            {
                return CompareTo(other.Id);
            }

            Int32 compare = x.CompareTo(y);
            return compare != 0 ? compare : CompareTo(other.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(RayIdContext other)
        {
            return CompareTo(other.RayId);
        }

        public override readonly Int32 GetHashCode()
        {
            HashCode code = new HashCode();

            fixed (Byte* pointer = Token)
            {
                code.Add(Unsafe.ReadUnaligned<UInt64>(pointer));
                code.Add(Unsafe.ReadUnaligned<UInt64>(pointer + 8));
                code.Add(Unsafe.ReadUnaligned<UInt64>(pointer + 16));
                code.Add(Unsafe.ReadUnaligned<UInt64>(pointer + 24));
            }

            return code.ToHashCode();
        }

        public override readonly Boolean Equals(Object? other)
        {
            return other switch
            {
                null => IsEmpty,
                Guid value => Equals(value),
                String value => Equals(value),
                DateTime value => Equals(value),
                DateTimeOffset value => Equals(value.DateTime),
                RayId value => Equals(value),
                RayIdContext value => Equals(value),
                _ => false
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(Guid other)
        {
            try
            {
                return Id.Equals(other);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public readonly Boolean Equals(String? other)
        {
            return TryParse(other, out RayId result) && Equals(result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(DateTime other)
        {
            try
            {
                return Timestamp.Equals(other);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public readonly Boolean Equals(RayId other)
        {
            if (Avx2.IsSupported)
            {
                fixed (Byte* pointer = Token)
                {
                    Vector256<Byte> compare = Avx2.CompareEqual(Unsafe.ReadUnaligned<Vector256<Byte>>(pointer), Unsafe.ReadUnaligned<Vector256<Byte>>(other.Token));
                    return Avx2.MoveMask(compare) == -1;
                }
            }

            for (Int32 i = 0; i < 4; i++)
            {
                if (Data[i] != other.Data[i])
                {
                    return false;
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(RayIdContext other)
        {
            return Equals(other.RayId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly String ToString()
        {
            return ToString(EscapeType.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayId Clone()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IMonad IMonad.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IMonad ICloneable<IMonad>.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly Object ICloneable.Clone()
        {
            return Clone();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private readonly String ToString(EscapeType escape)
        {
            return ToString(escape, null, Settings.format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly String ToString(String? format)
        {
            return ToString(EscapeType.None, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private readonly String ToString(EscapeType escape, String? format)
        {
            return ToString(escape, format, Settings.format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly String ToString(IFormatProvider? provider)
        {
            return ToString(EscapeType.None, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private readonly String ToString(EscapeType escape, IFormatProvider? provider)
        {
            return ToString(escape, null, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly String ToString(String? format, IFormatProvider? provider)
        {
            return ToString(EscapeType.None, format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static void WriteHex(Byte value, ReadOnlySpan<Char> hex, Span<Char> buffer, ref Int32 position)
        {
            Int32 nibble = (value >> 4) & 0xF;

            if (nibble != 0)
            {
                goto write4;
            }

            nibble = value & 0xF;
            buffer[position++] = hex[nibble];
            return;

            write4:
            Int32 index = position;

            buffer[index++] = hex[nibble];
            nibble = value & 0xF;
            buffer[index++] = hex[nibble];

            position = index;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static void WriteHex(UInt16 value, ReadOnlySpan<Char> hex, Span<Char> buffer, ref Int32 position)
        {
            Int32 index;
            Int32 nibble = (value >> 12) & 0xF;

            if (nibble != 0)
            {
                index = position;
                goto write12;
            }

            nibble = (value >> 8) & 0xF;
            if (nibble != 0)
            {
                index = position;
                goto write8;
            }

            nibble = (value >> 4) & 0xF;
            if (nibble != 0)
            {
                index = position;
                goto write4;
            }

            nibble = value & 0xF;
            buffer[position++] = hex[nibble];
            return;

            write12:
            buffer[index++] = hex[nibble];
            nibble = (value >> 8) & 0xF;
            write8:
            buffer[index++] = hex[nibble];
            nibble = (value >> 4) & 0xF;
            write4:
            buffer[index++] = hex[nibble];
            nibble = value & 0xF;
            buffer[index++] = hex[nibble];

            position = index;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static void WriteHex(Guid value, ReadOnlySpan<Char> hex, Span<Char> buffer, ref Int32 position)
        {
            if (!value.TryFormat(buffer.Slice(position), out Int32 written, "N"))
            {
                String guid = value.ToString("N", CultureInfo.InvariantCulture);
                guid.CopyTo(buffer.Slice(position));
                written = guid.Length;
            }

            Int32 start = position;
            Int32 end = start + written;

            if (!hex.IsEmpty)
            {
                for (Int32 i = start; i < end; i++)
                {
                    Char character = buffer[i];
                    Int32 index = character <= '9' ? character - '0' : (character | (Char) 0x20) - 'a' + 10;
                    buffer[i] = hex[index];
                }
            }

            position = end;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static void WriteDecimal(Byte value, Span<Char> buffer, ref Int32 position)
        {
            switch (value)
            {
                case < 10:
                {
                    buffer[position++] = (Char) ('0' + value);
                    return;
                }
                case < 100:
                {
                    Byte q = (Byte) (value / 10);
                    Byte r = (Byte) (value - q * 10);

                    buffer[position++] = (Char) ('0' + q);
                    buffer[position++] = (Char) ('0' + r);
                    return;
                }
                default:
                {
                    Byte a = (Byte) (value / 100);
                    Byte rem = (Byte) (value - a * 100);
                    Byte b = (Byte) (rem / 10);
                    Byte c = (Byte) (rem - b * 10);

                    buffer[position++] = (Char) ('0' + a);
                    buffer[position++] = (Char) ('0' + b);
                    buffer[position++] = (Char) ('0' + c);
                    return;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static void WriteDecimal(UInt16 value, Span<Char> buffer, ref Int32 position)
        {
            switch (value)
            {
                case < 10:
                {
                    buffer[position++] = (Char) ('0' + value);
                    return;
                }
                case < 100:
                {
                    UInt16 q = (UInt16) (value / 10);
                    UInt16 r = (UInt16) (value - q * 10);

                    buffer[position++] = (Char) ('0' + q);
                    buffer[position++] = (Char) ('0' + r);
                    return;
                }
                case < 1000:
                {
                    UInt16 a = (UInt16) (value / 100);
                    UInt16 rem = (UInt16) (value - a * 100);
                    UInt16 b = (UInt16) (rem / 10);
                    UInt16 c = (UInt16) (rem - b * 10);

                    buffer[position++] = (Char) ('0' + a);
                    buffer[position++] = (Char) ('0' + b);
                    buffer[position++] = (Char) ('0' + c);
                    return;
                }
                case < 10000:
                {
                    UInt16 a = (UInt16) (value / 1000);
                    UInt16 rem1 = (UInt16) (value - a * 1000);

                    UInt16 b = (UInt16) (rem1 / 100);
                    UInt16 rem2 = (UInt16) (rem1 - b * 100);

                    UInt16 c = (UInt16) (rem2 / 10);
                    UInt16 d = (UInt16) (rem2 - c * 10);

                    buffer[position++] = (Char) ('0' + a);
                    buffer[position++] = (Char) ('0' + b);
                    buffer[position++] = (Char) ('0' + c);
                    buffer[position++] = (Char) ('0' + d);
                    return;
                }
                default:
                {
                    UInt16 a = (UInt16) (value / 10000);
                    UInt16 rem1 = (UInt16) (value - a * 10000);

                    UInt16 b = (UInt16) (rem1 / 1000);
                    UInt16 rem2 = (UInt16) (rem1 - b * 1000);

                    UInt16 c = (UInt16) (rem2 / 100);
                    UInt16 rem3 = (UInt16) (rem2 - c * 100);

                    UInt16 d = (UInt16) (rem3 / 10);
                    UInt16 e = (UInt16) (rem3 - d * 10);

                    buffer[position++] = (Char) ('0' + a);
                    buffer[position++] = (Char) ('0' + b);
                    buffer[position++] = (Char) ('0' + c);
                    buffer[position++] = (Char) ('0' + d);
                    buffer[position++] = (Char) ('0' + e);
                    break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        private readonly String ToString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            switch (Format)
            {
                case RayIdFormat.GuidV4:
                case RayIdFormat.GuidV7:
                {
                    Boolean upper = format is { Length: 1 } && Char.IsUpper(format[0]);
                    ReadOnlySpan<Char> hex = upper ? "0123456789ABCDEF" : "0123456789abcdef";
                    Span<Char> buffer = stackalloc Char[128];
                    Int32 position = 0;

                    if (upper)
                    {
                        buffer[position++] = 'R';
                        buffer[position++] = 'A';
                        buffer[position++] = 'Y';
                    }
                    else
                    {
                        buffer[position++] = 'r';
                        buffer[position++] = 'a';
                        buffer[position++] = 'y';
                    }

                    buffer[position++] = '-';

                    WriteHex(Id, hex, buffer, ref position);

                    buffer[position++] = '<';

                    DateTime timestamp = Timestamp;
                    if (timestamp != default)
                    {
                        if (!timestamp.TryFormat(buffer.Slice(position), out Int32 written, "O", CultureInfo.InvariantCulture))
                        {
                            String time = timestamp.ToString("O", CultureInfo.InvariantCulture);
                            time.CopyTo(buffer.Slice(position));
                            written = time.Length;
                        }

                        position += written;
                    }
                    else
                    {
                        UInt64 span = SpanId;
                        buffer[position++] = hex[unchecked((Int32) ((span >> 60) & 0xF))];
                        buffer[position++] = hex[unchecked((Int32) ((span >> 56) & 0xF))];
                        buffer[position++] = hex[unchecked((Int32) ((span >> 52) & 0xF))];
                        buffer[position++] = hex[unchecked((Int32) ((span >> 48) & 0xF))];
                        buffer[position++] = hex[unchecked((Int32) ((span >> 44) & 0xF))];
                        buffer[position++] = hex[unchecked((Int32) ((span >> 40) & 0xF))];
                        buffer[position++] = hex[unchecked((Int32) ((span >> 36) & 0xF))];
                        buffer[position++] = hex[unchecked((Int32) ((span >> 32) & 0xF))];
                        buffer[position++] = hex[unchecked((Int32) ((span >> 28) & 0xF))];
                        buffer[position++] = hex[unchecked((Int32) ((span >> 24) & 0xF))];
                        buffer[position++] = hex[unchecked((Int32) ((span >> 20) & 0xF))];
                        buffer[position++] = hex[unchecked((Int32) ((span >> 16) & 0xF))];
                        buffer[position++] = hex[unchecked((Int32) ((span >> 12) & 0xF))];
                        buffer[position++] = hex[unchecked((Int32) ((span >> 8) & 0xF))];
                        buffer[position++] = hex[unchecked((Int32) ((span >> 4) & 0xF))];
                        buffer[position++] = hex[unchecked((Int32) ((span >> 0) & 0xF))];
                    }

                    buffer[position++] = '>';
                    buffer[position++] = '-';

                    buffer[position++] = 'V';
                    WriteDecimal(Version, buffer, ref position);
                    buffer[position++] = '/';
                    Byte metadata = Metadata;
                    buffer[position++] = hex[metadata >> 4];
                    buffer[position++] = hex[metadata & 0xF];

                    if (Service is { Server: var server, Service: var service })
                    {
                        buffer[position++] = '-';
                        buffer[position++] = 'S';
                        WriteDecimal(server, buffer, ref position);
                        buffer[position++] = ':';
                        WriteDecimal(service, buffer, ref position);
                    }

                    buffer[position++] = '-';

                    UInt32 info = Info;
                    buffer[position++] = hex[unchecked((Int32) ((info >> 28) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((info >> 24) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((info >> 20) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((info >> 16) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((info >> 12) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((info >> 8) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((info >> 4) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((info >> 0) & 0xF))];

                    return new String(buffer.Slice(0, position));
                }
                case RayIdFormat.W3C:
                {
                    ReadOnlySpan<Char> hex = format is { Length: 1 } && Char.IsUpper(format[0]) ? "0123456789ABCDEF" : "0123456789abcdef";
                    Span<Char> buffer = stackalloc Char[128];
                    Int32 position = 0;

                    buffer[position++] = '0';
                    buffer[position++] = '0';
                    buffer[position++] = '-';

                    WriteHex(Id, hex, buffer, ref position);

                    buffer[position++] = '-';

                    UInt64 span = SpanId;
                    buffer[position++] = hex[unchecked((Int32) ((span >> 60) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((span >> 56) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((span >> 52) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((span >> 48) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((span >> 44) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((span >> 40) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((span >> 36) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((span >> 32) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((span >> 28) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((span >> 24) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((span >> 20) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((span >> 16) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((span >> 12) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((span >> 8) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((span >> 4) & 0xF))];
                    buffer[position++] = hex[unchecked((Int32) ((span >> 0) & 0xF))];

                    buffer[position++] = '-';

                    Byte flags = (Byte) Flags;
                    buffer[position++] = hex[flags >> 4];
                    buffer[position++] = hex[flags & 0xF];

                    return new String(buffer.Slice(0, position));
                }
                case RayIdFormat.Custom:
                {
                    fixed (Byte* pointer = Token)
                    {
                        return Custom.Format(Unwrap(pointer), null, escape, format, provider);
                    }
                }
                default:
                {
                    throw new EnumUndefinedOrNotSupportedException<RayIdFormat>(Format, nameof(Format), null);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly String IStringable.GetString()
        {
            return ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly String IStringable.GetString(EscapeType escape)
        {
            return ToString(escape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly String IStringable.GetString(String? format)
        {
            return ToString(format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly String IStringable.GetString(EscapeType escape, String? format)
        {
            return ToString(escape, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly String IStringable.GetString(IFormatProvider? provider)
        {
            return ToString(provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly String IStringable.GetString(EscapeType escape, IFormatProvider? provider)
        {
            return ToString(escape, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly String IStringable.GetString(String? format, IFormatProvider? provider)
        {
            return ToString(format, provider);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly String IStringable.GetString(EscapeType escape, String? format, IFormatProvider? provider)
        {
            return ToString(escape, format, provider);
        }
    }
}