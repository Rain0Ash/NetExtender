using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using NetExtender.Exceptions;
using NetExtender.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Monads
{
    [StructLayout(LayoutKind.Explicit, Size = Settings.Size + 8)]
    public unsafe partial struct RayIdContext : IRayId<RayIdContext>, IEquality<RayId>, IEquality<Guid>, IEquality<String>, IEquality<DateTime>, ICloneable<RayIdContext>
    {
        public static implicit operator Boolean(RayIdContext value)
        {
            return !value.IsEmptyOrInvalid;
        }

        public static implicit operator Guid(RayIdContext value)
        {
            return value.Id;
        }

        public static implicit operator RayId(RayIdContext value)
        {
            return value.RayId;
        }

        public static Boolean operator ==(RayIdContext first, String? second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(RayIdContext first, String? second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(RayIdContext first, RayId second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(RayIdContext first, RayId second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(RayIdContext first, RayIdContext second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(RayIdContext first, RayIdContext second)
        {
            return !(first == second);
        }

        public static Boolean operator <(RayIdContext first, String? second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(RayIdContext first, String? second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(RayIdContext first, String? second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(RayIdContext first, String? second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(RayIdContext first, RayId second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(RayIdContext first, RayId second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(RayIdContext first, RayId second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(RayIdContext first, RayId second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator <(RayIdContext first, RayIdContext second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(RayIdContext first, RayIdContext second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(RayIdContext first, RayIdContext second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(RayIdContext first, RayIdContext second)
        {
            return first.CompareTo(second) >= 0;
        }

        [FieldOffset(0)] internal readonly Byte Metadata = default;
        [FieldOffset(0)] internal readonly Container This;
        [FieldOffset(0)] private fixed Byte Token[Settings.Size];
        [field: FieldOffset(Settings.Size)] private RayIdPayload? _payload = null;

        public readonly Boolean HasPayload
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _payload is not null;
            }
        }

        public RayIdPayload Payload
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _payload ??= RayIdPayload.New();
            }
            internal init
            {
                _payload = value;
            }
        }

        public readonly RayIdPayload? SafePayload
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _payload;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal init
            {
                _payload = value;
            }
        }

        readonly RayIdPayload? IRayIdInfo.Payload
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return SafePayload;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly RayId RayId
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return This.Id;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly RayIdFormat Format
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return RayId.Format;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly RayIdFormatType FormatType
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return RayId.FormatType;
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
                return RayId.HasTimestamp;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly Boolean IsServerGenerated
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return RayId.IsServerGenerated;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly Boolean IsUsingHash
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return RayId.IsUsingHash;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly Byte Version
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return RayId.Version;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly (UInt16 Server, UInt16 Service)? Service
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return RayId.Service;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly Guid Id
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return RayId.Id;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly UInt64 SpanId
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get
            {
                return RayId.SpanId;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly DateTime Timestamp
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return RayId.Timestamp;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly Guid ParentId
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get
            {
                switch (Format)
                {
                    case RayIdFormat.GuidV4 or RayIdFormat.GuidV7 or RayIdFormat.W3C:
                    {
                        return This.ParentId;
                    }
                    case RayIdFormat.Custom:
                    {
                        fixed (Byte* pointer = Token)
                        {
                            return RayId.Custom.ParentId(Unwrap(pointer));
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
        public readonly UInt64 ParentSpanId
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get
            {
                switch (Format)
                {
                    case RayIdFormat.GuidV4 or RayIdFormat.GuidV7 or RayIdFormat.W3C:
                    {
                        return This.ParentSpanId;
                    }
                    case RayIdFormat.Custom:
                    {
                        fixed (Byte* pointer = Token)
                        {
                            return RayId.Custom.ParentSpanId(Unwrap(pointer));
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
        public readonly DateTime ParentTimestamp
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get
            {
                switch (Format)
                {
                    case RayIdFormat.GuidV4 or RayIdFormat.GuidV7 or RayIdFormat.W3C:
                    {
                        return This.ParentTimestamp;
                    }
                    case RayIdFormat.Custom:
                    {
                        fixed (Byte* pointer = Token)
                        {
                            return RayId.Custom.ParentTimestamp(Unwrap(pointer));
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
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return RayId.Flags;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly UInt32? Hash
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return RayId.Hash;
            }
        }

        [JsonIgnore, IgnoreDataMember, XmlIgnore, SoapIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public readonly UInt32 Info
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return RayId.Info;
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
                return RayId.IsEmpty;
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
        internal RayIdContext(void* value)
            : this((Container*) value)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RayIdContext(Container* container)
        {
            This = *container;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RayIdContext(RayId id, Guid parent, UInt64 span)
            : this((RayId.Container*) &id, parent, span)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RayIdContext(RayId.Container* container, Guid parent, UInt64 span)
        {
            This = new Container(*container, parent, span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RayIdContext(RayId id, Guid parent, DateTime timestamp)
            : this((RayId.Container*) &id, parent, timestamp)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RayIdContext(RayId.Container* container, Guid parent, DateTime timestamp)
        {
            This = new Container(*container, parent, timestamp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Span<Byte> Unwrap(Byte* token)
        {
            return new Span<Byte>(token + 1, Settings.Size - 1);
        }

        public static RayIdContext From(ReadOnlySpan<Byte> token, RayIdPayload? payload = null)
        {
            return token.Length switch
            {
                Settings.Size => new RayIdContext(Unsafe.AsPointer(ref MemoryMarshal.GetReference(token))) { SafePayload = payload },
                RayId.Settings.Size => new RayIdContext(new RayId(Unsafe.AsPointer(ref MemoryMarshal.GetReference(token))), default, default(UInt64)) { SafePayload = payload },
                _ => throw new ArgumentException($"Token length must be equal to {RayId.Settings.Size} ({nameof(NetExtender.Monads.RayId)}) or {Settings.Size} ({nameof(RayIdContext)}) bytes.", nameof(token))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext From(ReadOnlySpan<Char> token, RayIdPayload? payload)
        {
            return From(token, new Maybe<RayIdPayload?>(payload));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext From(ReadOnlySpan<Char> token, Maybe<RayIdPayload?> payload = default)
        {
            return From(token, null, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext From(ReadOnlySpan<Char> token, IFormatProvider? provider, RayIdPayload? payload)
        {
            return From(token, provider, new Maybe<RayIdPayload?>(payload));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext From(ReadOnlySpan<Char> token, IFormatProvider? provider, Maybe<RayIdPayload?> payload = default)
        {
            return TryParse(token, false, provider, out RayIdContext result, payload) ? result : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext FromDirect(ReadOnlySpan<Char> token, RayIdPayload? payload)
        {
            return FromDirect(token, new Maybe<RayIdPayload?>(payload));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext FromDirect(ReadOnlySpan<Char> token, Maybe<RayIdPayload?> payload = default)
        {
            return FromDirect(token, null, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext FromDirect(ReadOnlySpan<Char> token, IFormatProvider? provider, RayIdPayload? payload)
        {
            return FromDirect(token, provider, new Maybe<RayIdPayload?>(payload));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RayIdContext FromDirect(ReadOnlySpan<Char> token, IFormatProvider? provider, Maybe<RayIdPayload?> payload = default)
        {
            return TryParse(token, true, provider, out RayIdContext result, payload) ? result : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> token, out RayIdContext result, RayIdPayload? payload)
        {
            return TryParse(token, out result, new Maybe<RayIdPayload?>(payload));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> token, out RayIdContext result, Maybe<RayIdPayload?> payload = default)
        {
            return TryParse(token, null, out result, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> token, IFormatProvider? provider, out RayIdContext result, RayIdPayload? payload)
        {
            return TryParse(token, provider, out result, new Maybe<RayIdPayload?>(payload));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> token, IFormatProvider? provider, out RayIdContext result, Maybe<RayIdPayload?> payload = default)
        {
            return TryParse(token, false, provider, out result, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParseDirect(ReadOnlySpan<Char> token, out RayIdContext result, RayIdPayload? payload)
        {
            return TryParseDirect(token, out result, new Maybe<RayIdPayload?>(payload));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParseDirect(ReadOnlySpan<Char> token, out RayIdContext result, Maybe<RayIdPayload?> payload = default)
        {
            return TryParseDirect(token, null, out result, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParseDirect(ReadOnlySpan<Char> token, IFormatProvider? provider, out RayIdContext result, RayIdPayload? payload)
        {
            return TryParseDirect(token, provider, out result, new Maybe<RayIdPayload?>(payload));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParseDirect(ReadOnlySpan<Char> token, IFormatProvider? provider, out RayIdContext result, Maybe<RayIdPayload?> payload = default)
        {
            return TryParse(token, true, provider, out result, payload);
        }

        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean TryParse(ReadOnlySpan<Char> token, Boolean direct, IFormatProvider? provider, out RayIdContext result, Maybe<RayIdPayload?> payload = default)
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

                DateTime? timestamp = null;
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

                Container container = new Container();

                if (!token.IsEmpty)
                {
                    if (token[0] == '<')
                    {
                        container.ParentId = id;

                        token = token.Slice(1);
                        end = MemoryExtensions.IndexOf(token, '>');

                        if (end < 0)
                        {
                            return false;
                        }

                        element = token.Slice(0, end);
                        if (MemoryExtensions.IndexOfAny(element, '-', ':', 'T') >= 0)
                        {
                            if (!DateTime.TryParse(element, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out container.ParentTimestamp))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (!UInt64.TryParse(element, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out container.ParentSpanId))
                            {
                                return false;
                            }
                        }

                        token = token.Slice(end + 1);
                    }
                    else
                    {
                        if (token.Length < 32)
                        {
                            return false;
                        }

                        element = token.Slice(0, 32);
                        if (!Guid.TryParseExact(element, "N", out container.ParentId))
                        {
                            return false;
                        }

                        token = token.Slice(32);

                        if (!token.IsEmpty && token[0] == '<')
                        {
                            token = token.Slice(1);
                            end = MemoryExtensions.IndexOf(token, '>');

                            if (end < 0)
                            {
                                return false;
                            }

                            element = token.Slice(0, end);
                            if (MemoryExtensions.IndexOfAny(element, '-', ':', 'T') >= 0)
                            {
                                if (!DateTime.TryParse(element, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out container.ParentTimestamp))
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                if (!UInt64.TryParse(element, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out container.ParentSpanId))
                                {
                                    return false;
                                }
                            }

                            token = token.Slice(end + 1);
                        }
                    }
                }

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
                    metadata &= (Byte) ~RayId.Mask.ServerGenerated;
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
                    if (token[0] == '?')
                    {
                        token = token.Slice(1);
                        if (payload.IsEmpty && RayIdPayload.TryParse(token, out RayIdPayload? value))
                        {
                            payload = value;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                RayId.Container ray = timestamp.HasValue ? new RayId.Container(metadata, id, timestamp.Value, info) : new RayId.Container(metadata, id, span, info);
                ray.Version = version;

                if (service.HasValue)
                {
                    ray.Service = RayId.Container.GetService((Byte) ((metadata & (Byte) RayId.Mask.ServerMask) >> 1), service.Value.Server, service.Value.Service);
                }

                container.Id = new RayId(&ray);
                result = new RayIdContext(&container) { SafePayload = payload.Unwrap() };
                return true;
            }

            if (MemoryExtensions.StartsWith(token, "00-", StringComparison.InvariantCultureIgnoreCase))
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
                    if (token[0] == '?')
                    {
                        token = token.Slice(1);
                        if (payload.IsEmpty && RayIdPayload.TryParse(token, out RayIdPayload? value))
                        {
                            payload = value;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                const Byte metadata = (Byte) RayIdFormat.W3C << 6;
                RayId.Container ray = new RayId.Container(metadata, id, span)
                {
                    Flags = (RayIdFlags) flags
                };

                Container container = new Container(ray);
                result = new RayIdContext(&container) { SafePayload = payload.Unwrap() };
                return true;
            }

            result = RayId.Custom.TryParse(token, provider, ref payload);
            return !result.IsEmptyOrInvalid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext WithPayload(RayIdPayload? payload)
        {
            Container container = This;
            return new RayIdContext(&container) { SafePayload = payload };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean? ValidateParentTimestamp(ref DateTime timestamp, DateTime @unsafe)
        {
            try
            {
                if (timestamp == default)
                {
                    return null;
                }

                if (@unsafe >= timestamp || timestamp - @unsafe > Time.Day.One)
                {
                    return false;
                }

                timestamp = @unsafe;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Unwrap(out Object? value)
        {
            value = IsEmptyOrInvalid ? null : this;
            return value is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(Guid other)
        {
            return RayId.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(String? other)
        {
            return RayId.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(DateTime other)
        {
            return RayId.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(RayId other)
        {
            return RayId.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(RayIdContext other)
        {
            return RayId.CompareTo(other.RayId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly Int32 GetHashCode()
        {
            return RayId.GetHashCode();
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
            return RayId.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(String? other)
        {
            return RayId.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(DateTime other)
        {
            return RayId.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(RayId other)
        {
            return RayId.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(RayIdContext other)
        {
            return RayId.Equals(other.RayId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly RayIdContext Clone()
        {
            RayIdContext context = this;
            context._payload = _payload?.Clone();
            return context;
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
        public override readonly String ToString()
        {
            return ToString(EscapeType.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private readonly String ToString(EscapeType escape)
        {
            return ToString(escape, null, RayId.Settings.format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly String ToString(String? format)
        {
            return ToString(EscapeType.None, format);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private readonly String ToString(EscapeType escape, String? format)
        {
            return ToString(escape, format, RayId.Settings.format);
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
                    RayIdPayload? payload = format is not "d" and not "D" ? _payload : null;
                    Span<Char> buffer = stackalloc Char[160 + (payload is not null ? RayIdPayload.Buffer : 0)];
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

                    Guid id = Id;
                    RayId.WriteHex(id, hex, buffer, ref position);

                    buffer[position++] = '<';

                    Int32 written;
                    DateTime timestamp = Timestamp;
                    if (timestamp != default)
                    {
                        if (!timestamp.TryFormat(buffer.Slice(position), out written, "O", CultureInfo.InvariantCulture))
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

                    Boolean has = false;
                    Guid parent = ParentId;
                    if (parent != default && parent != id)
                    {
                        RayId.WriteHex(parent, hex, buffer, ref position);
                        has = true;
                    }

                    switch (ValidateParentTimestamp(ref timestamp, ParentTimestamp))
                    {
                        case true:
                        {
                            buffer[position++] = '<';

                            if (!timestamp.TryFormat(buffer.Slice(position), out written, "O", CultureInfo.InvariantCulture))
                            {
                                String time = timestamp.ToString("O", CultureInfo.InvariantCulture);
                                time.CopyTo(buffer.Slice(position));
                                written = time.Length;
                            }

                            position += written;
                            buffer[position++] = '>';
                            has = true;
                            break;
                        }
                        case false:
                        {
                            UInt64 span = ParentSpanId;
                            if (span == default)
                            {
                                break;
                            }

                            buffer[position++] = '<';

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

                            buffer[position++] = '>';
                            has = true;
                            break;
                        }
                        default:
                        {
                            break;
                        }
                    }

                    if (has)
                    {
                        buffer[position++] = '-';
                    }

                    buffer[position++] = 'V';
                    RayId.WriteDecimal(Version, buffer, ref position);
                    buffer[position++] = '/';
                    Byte metadata = Metadata;
                    buffer[position++] = hex[metadata >> 4];
                    buffer[position++] = hex[metadata & 0xF];

                    if (Service is { Server: var server, Service: var service })
                    {
                        buffer[position++] = '-';
                        buffer[position++] = 'S';
                        RayId.WriteDecimal(server, buffer, ref position);
                        buffer[position++] = ':';
                        RayId.WriteDecimal(service, buffer, ref position);
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

                    if (payload is null)
                    {
                        return new String(buffer.Slice(0, position));
                    }

                    buffer[position++] = '?';

                    if (payload.TryFormat(buffer.Slice(position), out written))
                    {
                        return new String(buffer.Slice(0, position + written));
                    }

                    return new String(buffer.Slice(0, position)) + payload;
                }
                case RayIdFormat.W3C:
                {
                    ReadOnlySpan<Char> hex = format is { Length: 1 } && Char.IsUpper(format[0]) ? "0123456789ABCDEF" : "0123456789abcdef";
                    RayIdPayload? payload = format is not "d" and not "D" ? _payload : null;
                    Span<Char> buffer = stackalloc Char[160 + (payload is not null ? RayIdPayload.Buffer : 0)];
                    Int32 position = 0;

                    buffer[position++] = '0';
                    buffer[position++] = '0';
                    buffer[position++] = '-';

                    RayId.WriteHex(Id, hex, buffer, ref position);

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

                    if (payload is null)
                    {
                        return new String(buffer.Slice(0, position));
                    }

                    buffer[position++] = '?';

                    if (payload.TryFormat(buffer.Slice(position), out Int32 written))
                    {
                        return new String(buffer.Slice(0, position + written));
                    }

                    return new String(buffer.Slice(0, position)) + payload;
                }
                case RayIdFormat.Custom:
                {
                    fixed (Byte* pointer = Token)
                    {
                        return RayId.Custom.Format(Unwrap(pointer), _payload, escape, format, provider);
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