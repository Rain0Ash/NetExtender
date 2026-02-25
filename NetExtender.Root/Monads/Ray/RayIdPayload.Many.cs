using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Utilities.Core;

namespace NetExtender.Monads
{
    public abstract partial class RayIdPayload
    {
        [Serializable]
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private sealed class Many : RayIdPayload
        {
            internal new static StringComparer Comparer
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return StringComparer.OrdinalIgnoreCase;
                }
            }

            private Dictionary<String, Element> Container { get; }
            private Int32 _version;

            public override Int32 Count
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Container.Count;
                }
            }

            public override IEnumerable<String> Keys
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Container.OrderByDescending(static pair => pair.Value.Version).Select(static pair => pair.Key);
                }
            }

            public override IEnumerable<Element> Values
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Container.Select(static pair => pair.Value).OrderByDescending(static element => element.Version);
                }
            }

            public Many(StringComparer? comparer)
                : this(2, comparer)
            {
            }

            public Many(Int32 capacity, StringComparer? comparer)
            {
                Container = new Dictionary<String, Element>(capacity, comparer ?? Comparer);
            }

            private Many(Dictionary<String, Element> container)
            {
                Container = container;
            }

    #if NET8_0_OR_GREATER
            [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    #endif
            internal Many(SerializationInfo info, StreamingContext context)
            {
                Container = TypeUtilities.New<Dictionary<String, Element>, SerializationInfo, StreamingContext>().Invoke(info, context);
            }

#if NET8_0_OR_GREATER
            [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                Container.GetObjectData(info, context);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean ContainsKey([NotNullWhen(true)] String? key)
            {
                return key is not null && Container.ContainsKey(key);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetValue([NotNullWhen(true)] String? key, [MaybeNullWhen(false)] out Element value)
            {
                if (key is not null)
                {
                    return Container.TryGetValue(key, out value);
                }

                value = default;
                return false;
            }

            public override Boolean Unsafe([NotNullWhen(true)] String? key, Element element)
            {
                if (key is null || !ValidateKey(key))
                {
                    return false;
                }

                Container[key] = element;
                element.Version = _version++;
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            protected override Boolean AddCore(String key, Element element)
            {
                if (!Container.TryAdd(key, element))
                {
                    return false;
                }

                element.Version = _version++;
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            protected override Boolean SetCore(String key, Element element)
            {
                Container[key] = element;
                element.Version = _version++;
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Remove([NotNullWhen(true)] String? key)
            {
                return key is not null && Container.Remove(key);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Remove([NotNullWhen(true)] String? key, [MaybeNullWhen(false)] out Element value)
            {
                if (key is not null)
                {
                    return Container.Remove(key, out value);
                }

                value = default;
                return false;
            }

            protected override Boolean Reverse()
            {
                if (Container.Count == 0)
                {
                    return false;
                }

                Int32 version = _version - 1;
                foreach (Element element in Container.Values)
                {
                    element.Version = version - element.Version;
                }

                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override RayIdPayload Clone()
            {
                Dictionary<String, Element> container = new Dictionary<String, Element>(Container.Count, Container.Comparer);

                foreach ((String key, Element value) in Container)
                {
                    container.Add(key, value.Clone());
                }

                return new Many(container);
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public override Boolean TryFormat(Span<Char> destination, out Int32 written, Int32 count, ReadOnlySpan<Char> format, IFormatProvider? provider)
            {
                count = Math.Min(count, Container.Count);

                if (count <= 0)
                {
                    written = 0;
                    return true;
                }

                KeyValuePair<String, Element>[] array = ArrayPool<KeyValuePair<String, Element>>.Shared.Rent(count);

                try
                {
                    ((ICollection<KeyValuePair<String, Element>>) Container).CopyTo(array, 0);

                    Comparer comparer = new Comparer(Container.Comparer as StringComparer);
                    Span<KeyValuePair<String, Element>> span = array.AsSpan(0, count);
                    span.Sort(comparer);

                    Boolean comma = false;
                    written = 0;

                    foreach (KeyValuePair<String, Element> pair in span)
                    {
                        if (!Write(destination, ref written, comma, pair, format, provider))
                        {
                            written = 0;
                            return false;
                        }

                        comma = true;
                    }

                    return true;
                }
                finally
                {
                    ArrayPool<KeyValuePair<String, Element>>.Shared.Return(array, true);
                }

                [MethodImpl(MethodImplOptions.AggressiveOptimization)]
                static Boolean Write(Span<Char> destination, ref Int32 written, Boolean comma, KeyValuePair<String, Element> pair, [SuppressMessage("ReSharper", "UnusedParameter.Local")] ReadOnlySpan<Char> format, IFormatProvider? provider)
                {
                    (String key, Element element) = pair;
                    Int32 prefix = (comma ? 1 : 0) + key.Length + 1;

                    Span<Char> buffer = stackalloc Char[256];
                    if (!element.TryFormat(buffer, out Int32 write, default, provider) || write <= 0)
                    {
                        return false;
                    }

                    ReadOnlySpan<Char> value = buffer.Slice(0, write);
                    if (!ValidateValue(value))
                    {
                        return false;
                    }

                    Int32 offset = written + prefix;
                    Int32 total = prefix + write;

                    if (offset < 0 || total < 0 || written + total > destination.Length)
                    {
                        return false;
                    }

                    value.CopyTo(destination.Slice(offset));
                    prefix = written;

                    if (comma)
                    {
                        destination[prefix++] = ',';
                    }

                    key.CopyTo(destination.Slice(prefix));
                    prefix += key.Length;

                    destination[prefix] = '=';

                    written = offset + write;
                    return true;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            public override String ToString(Int32 count, String? format, IFormatProvider? provider)
            {
                count = Math.Min(count, Container.Count);

                if (count <= 0)
                {
                    return String.Empty;
                }

                KeyValuePair<String, Element>[] array = ArrayPool<KeyValuePair<String, Element>>.Shared.Rent(count);
                Char[]? rent = null;

                try
                {
                    ((ICollection<KeyValuePair<String, Element>>) Container).CopyTo(array, 0);

                    Comparer comparer = new Comparer(Container.Comparer as StringComparer);
                    Span<KeyValuePair<String, Element>> span = array.AsSpan(0, count);
                    span.Sort(comparer);

                    Int32 written = 0;
                    Span<Char> buffer = stackalloc Char[Buffer];

                    Boolean comma = false;
                    foreach (KeyValuePair<String, Element> pair in span)
                    {
                        Write(ref buffer, ref rent, ref written, comma, pair, format, provider);
                        comma = true;
                    }

                    return new String(buffer.Slice(0, written));
                }
                finally
                {
                    ArrayPool<KeyValuePair<String, Element>>.Shared.Return(array, true);

                    if (rent is not null)
                    {
                        ArrayPool<Char>.Shared.Return(rent, true);
                    }
                }

                [MethodImpl(MethodImplOptions.AggressiveOptimization)]
                static void EnsureCapacity(ref Span<Char> buffer, ref Char[]? rent, Int32 written, Int32 overwrite)
                {
                    Int32 require = written + overwrite;
                    if (require <= buffer.Length)
                    {
                        return;
                    }

                    Int32 size = buffer.IsEmpty ? Buffer : buffer.Length;
                    while (size < require)
                    {
                        size *= 2;
                    }

                    Char[] @new = ArrayPool<Char>.Shared.Rent(size);

                    if (written > 0)
                    {
                        buffer.Slice(0, written).CopyTo(@new);
                    }

                    if (rent is not null)
                    {
                        ArrayPool<Char>.Shared.Return(rent, true);
                    }

                    rent = @new;
                    buffer = @new;
                }

                [MethodImpl(MethodImplOptions.AggressiveOptimization)]
                static void Write(ref Span<Char> buffer, ref Char[]? rent, ref Int32 written, Boolean comma, KeyValuePair<String, Element> pair, [SuppressMessage("ReSharper", "UnusedParameter.Local")] String? format, IFormatProvider? provider)
                {
                    (String key, Element element) = pair;

                    Int32 prefix = (comma ? 1 : 0) + key.Length + 1;
                    Int32 offset = written + prefix;

                    Int32 write;
                    EnsureCapacity(ref buffer, ref rent, written, prefix + 256);

                    Int32 attempts = 0;
                    while (true)
                    {
                        Span<Char> destination = buffer.Slice(offset);
                        if (element.TryFormat(destination, out write, null, provider))
                        {
                            break;
                        }

                        attempts++;
                        if (attempts >= 3)
                        {
                            write = -1;
                            break;
                        }

                        EnsureCapacity(ref buffer, ref rent, offset, 256);
                    }

                    if (write < 0)
                    {
                        String value = element.ToString(null, provider);
                        if (!ValidateValue(value))
                        {
                            throw new FormatException($"Value for key '{key}' is not a valid W3C tracestate value.");
                        }

                        EnsureCapacity(ref buffer, ref rent, written, prefix + value.Length);
                        value.CopyTo(buffer.Slice(offset));
                        write = value.Length;
                    }
                    else
                    {
                        if (write <= 0)
                        {
                            throw new FormatException($"Value for key '{key}' is empty, which is not allowed for W3C tracestate value.");
                        }

                        ReadOnlySpan<Char> result = buffer.Slice(offset, write);
                        if (!ValidateValue(result))
                        {
                            throw new FormatException($"Formatted value for key '{key}' is not a valid W3C tracestate value.");
                        }
                    }

                    prefix = written;

                    if (comma)
                    {
                        buffer[prefix++] = ',';
                    }

                    key.CopyTo(buffer.Slice(prefix));
                    prefix += key.Length;

                    buffer[prefix] = '=';
                    written = offset + write;
                }
            }

            public override IEnumerator<KeyValuePair<String, Element>> GetEnumerator()
            {
                Int32 count;
                KeyValuePair<String, Element>[] array = ArrayPool<KeyValuePair<String, Element>>.Shared.Rent(count = Container.Count);

                try
                {
                    ((ICollection<KeyValuePair<String, Element>>) Container).CopyTo(array, 0);

                    Comparer comparer = new Comparer(Container.Comparer as StringComparer);
                    Memory<KeyValuePair<String, Element>> memory = array.AsMemory(0, count);
                    memory.Span.Sort(comparer);

                    for (Int32 i = 0; i < memory.Length; i++)
                    {
                        yield return memory.Span[i];
                    }
                }
                finally
                {
                    ArrayPool<KeyValuePair<String, Element>>.Shared.Return(array, true);
                }
            }
        }
    }
}