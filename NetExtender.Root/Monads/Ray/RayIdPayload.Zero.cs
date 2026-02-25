using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace NetExtender.Monads
{
    public abstract partial class RayIdPayload
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private sealed class Zero : RayIdPayload
        {
            private Many? Container { get; set; }
            private new StringComparer? Comparer { get; }

            public override Int32 Count
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Container?.Count ?? 0;
                }
            }

            public override IEnumerable<String> Keys
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    if (Container is null)
                    {
                        yield break;
                    }

                    foreach (String key in Container.Keys)
                    {
                        yield return key;
                    }
                }
            }

            public override IEnumerable<Element> Values
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    if (Container is null)
                    {
                        yield break;
                    }

                    foreach (Element element in Container.Values)
                    {
                        yield return element;
                    }
                }
            }

            public Zero(StringComparer? comparer)
            {
                Comparer = comparer;
            }

#if NET8_0_OR_GREATER
            [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                if (Container is null)
                {
                    new Many(0, null).GetObjectData(info, context);
                    return;
                }

                Container.GetObjectData(info, context);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean ContainsKey([NotNullWhen(true)] String? key)
            {
                return Container is not null && Container.ContainsKey(key);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryGetValue([NotNullWhen(true)] String? key, [MaybeNullWhen(false)] out Element value)
            {
                if (Container is not null)
                {
                    return Container.TryGetValue(key, out value);
                }

                value = default;
                return false;
            }

            public override Boolean Unsafe([NotNullWhen(true)] String? key, Element element)
            {
                if (Container is not null)
                {
                    return Container.Unsafe(key, element);
                }

                Many container = new Many(Comparer);
                if (!container.Unsafe(key, element))
                {
                    return false;
                }

                Container = container;
                return true;
            }

            protected override Boolean AddCore(String key, Element element)
            {
                if (Container is not null)
                {
                    return Container.AddCore(key, element);
                }

                Many container = new Many(Comparer);
                if (!container.AddCore(key, element))
                {
                    return false;
                }

                Container = container;
                return true;
            }

            protected override Boolean SetCore(String key, Element element)
            {
                if (Container is not null)
                {
                    return Container.SetCore(key, element);
                }

                Many container = new Many(Comparer);
                if (!container.SetCore(key, element))
                {
                    return false;
                }

                Container = container;
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Remove([NotNullWhen(true)] String? key)
            {
                return Container is not null && Container.Remove(key);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Remove([NotNullWhen(true)] String? key, [MaybeNullWhen(false)] out Element value)
            {
                if (Container is not null)
                {
                    return Container.Remove(key, out value);
                }

                value = default;
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            protected override Boolean Reverse()
            {
                return Container is not null && Container.Reverse();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override RayIdPayload Clone()
            {
                return Container is not null ? Container.Clone() : new Zero(Comparer);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean TryFormat(Span<Char> destination, out Int32 written, Int32 count, ReadOnlySpan<Char> format, IFormatProvider? provider)
            {
                if (Container is not null)
                {
                    return Container.TryFormat(destination, out written, count, format, provider);
                }

                written = 0;
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override String ToString(Int32 count, String? format, IFormatProvider? provider)
            {
                return Container is not null ? Container.ToString(count, format, provider) : String.Empty;
            }

            public override IEnumerator<KeyValuePair<String, Element>> GetEnumerator()
            {
                if (Container is null)
                {
                    yield break;
                }

                foreach (KeyValuePair<String, Element> item in Container)
                {
                    yield return item;
                }
            }
        }
    }
}