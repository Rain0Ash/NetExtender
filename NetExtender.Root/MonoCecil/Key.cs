using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Mono.Cecil;
using NetExtender.Types.Strings;

namespace NetExtender.Cecil
{
    public abstract partial class MonoCecilType
    {
        //TODO: bug with closed generics type. Different FullName -> not equals between Reflection.Type and Mono.Cecil.TypeReference
        public readonly struct TypeKey : IEquatableStruct<TypeKey>, IEquatable<Type>, IEquatable<TypeReference>, IEquatable<CecilReference>
        {
            public static implicit operator String(TypeKey value)
            {
                return value.Key;
            }

            internal static StringPool AssemblyPool { get; } = new StringPool(200);
            internal static StringPool ModulePool { get; } = new StringPool(100);
            internal static StringPool TypeNamePool { get; } = new StringPool(10000);
            internal static StringPool TypeFullNamePool { get; } = new StringPool(10000);
            internal static StringPool NamespacePool { get; } = new StringPool(1000);
            internal static StringPool ScopeNamePool { get; } = new StringPool(1000);

            private String Assembly { get; }
            private String Type { get; }

            private String Key
            {
                get
                {
                    return !String.IsNullOrEmpty(Assembly) ? String.IsNullOrEmpty(Type) ? Assembly : $"{Assembly}|{Type}" : Type ?? String.Empty;
                }
            }

            public Boolean IsEmpty
            {
                get
                {
                    return Type is null;
                }
            }

            public TypeKey(Type type)
            {
                Span<Char> buffer = stackalloc Char[Buffer];

                Assembly = AssemblyPool.GetOrAdd(AssemblyOf(type, buffer));
                Type = TypeNamePool.GetOrAdd(TypeNameOf(type, buffer));
            }

            public TypeKey(TypeReference type)
            {
                Span<Char> buffer = stackalloc Char[Buffer];

                Assembly = AssemblyPool.GetOrAdd(AssemblyOf(type, buffer));
                Type = TypeNamePool.GetOrAdd(TypeNameOf(type, buffer));
            }

            public static ReadOnlySpan<Char> AssemblyOf(Type type, Span<Char> buffer)
            {
                if (TypeToReference.TryGetValue(type, out CecilReference? reference))
                {
                    return AssemblyOf(reference, buffer);
                }

                String assembly = GetAssemblyName(type.Assembly);
                Int32 length = assembly.Length;

                if (buffer.Length < length)
                {
                    buffer = new Char[length];
                }

                assembly.CopyTo(buffer);
                return buffer.Slice(0, length);
            }

            public static ReadOnlySpan<Char> AssemblyOf(TypeReference type, Span<Char> buffer)
            {
                String assembly = GetScopeName(type);
                Int32 length = assembly.Length;

                if (buffer.Length < length)
                {
                    buffer = new Char[length];
                }

                assembly.CopyTo(buffer);
                return buffer.Slice(0, length);
            }

            internal static ReadOnlySpan<Char> AssemblyOf(CecilReference type, Span<Char> buffer)
            {
                String assembly = type.ScopeName;
                Int32 length = assembly.Length;

                if (buffer.Length < length)
                {
                    buffer = new Char[length];
                }

                assembly.CopyTo(buffer);
                return buffer.Slice(0, length);
            }

            public static ReadOnlySpan<Char> TypeNameOf(Type type, Span<Char> buffer)
            {
                if (TypeToReference.TryGetValue(type, out CecilReference? reference))
                {
                    return TypeNameOf(reference, buffer);
                }

                String name = type.FullName ?? type.Name;
                Int32 length = name.Length;

                if (buffer.Length < length)
                {
                    buffer = new Char[length];
                }

                name.CopyTo(buffer);
                return buffer.Slice(0, length);
            }

            public static ReadOnlySpan<Char> TypeNameOf(TypeReference type, Span<Char> buffer)
            {
                String name = type.FullName ?? type.Name;
                Int32 length = name.Length;

                if (buffer.Length < length)
                {
                    buffer = new Char[length];
                }

                Format(name, buffer, out _);
                return buffer.Slice(0, length);
            }

            internal static ReadOnlySpan<Char> TypeNameOf(CecilReference type, Span<Char> buffer)
            {
                String name = type.FullName ?? type.Name;
                Int32 length = name.Length;

                if (buffer.Length < length)
                {
                    buffer = new Char[length];
                }

                Format(name, buffer, out _);
                return buffer.Slice(0, length);
            }

            public static ReadOnlySpan<Char> KeyOf(Type type, Span<Char> buffer)
            {
                if (TypeToReference.TryGetValue(type, out CecilReference? reference))
                {
                    return KeyOf(reference, buffer);
                }

                String name = type.FullName ?? type.Name;
                String assembly = GetAssemblyName(type.Assembly);

                Int32 length = assembly.Length + name.Length + 1;

                if (buffer.Length < length)
                {
                    buffer = new Char[length];
                }

                assembly.CopyTo(buffer);
                buffer[assembly.Length] = '|';
                name.CopyTo(buffer.Slice(assembly.Length + 1));
                return buffer.Slice(0, length);
            }

            public static ReadOnlySpan<Char> KeyOf(TypeReference type, Span<Char> buffer)
            {
                String name = type.FullName ?? type.Name;
                String assembly = GetScopeName(type);

                Int32 length = assembly.Length + name.Length + 1;

                if (buffer.Length < length)
                {
                    buffer = new Char[length];
                }

                assembly.CopyTo(buffer);
                buffer[assembly.Length] = '|';
                Format(name, buffer.Slice(assembly.Length + 1), out _);
                return buffer.Slice(0, length);
            }

            internal static ReadOnlySpan<Char> KeyOf(CecilReference type, Span<Char> buffer)
            {
                String name = type.FullName ?? type.Name;
                String assembly = type.ScopeName;

                Int32 length = assembly.Length + name.Length + 1;

                if (buffer.Length < length)
                {
                    buffer = new Char[length];
                }

                assembly.CopyTo(buffer);
                buffer[assembly.Length] = '|';
                Format(name, buffer.Slice(assembly.Length + 1), out _);
                return buffer.Slice(0, length);
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public static unsafe ReadOnlySpan<Char> Format(String source, Span<Char> buffer, out Int32 format)
            {
                format = 0;
                Int32 length = source.Length;

                fixed (Char* pointer = source)
                fixed (Char* destination = &MemoryMarshal.GetReference(buffer))
                {
                    Char* src = pointer;
                    Char* dst = destination;
                    Char* end = src + length;

                    while (src < end)
                    {
                        if (*src == '/')
                        {
                            ++format;
                            *dst = '+';
                        }
                        else
                        {
                            *dst = *src;
                        }

                        src++;
                        dst++;
                    }
                }

                return buffer.Length > length ? buffer.Slice(0, length) : buffer;
            }

            public override Int32 GetHashCode()
            {
                return HashCode.Combine(Assembly, Type);
            }

            public override Boolean Equals(Object? other)
            {
                return other switch
                {
                    TypeKey value => Equals(value),
                    Type value => Equals(value),
                    TypeReference value => Equals(value),
                    CecilReference value => Equals(value),
                    _ => false
                };
            }

            public Boolean Equals(Type? other)
            {
                if (other is null)
                {
                    return IsEmpty;
                }

                Span<Char> buffer = stackalloc Char[Buffer];
                return AssemblyOf(other, buffer).SequenceEqual(Assembly) && TypeNameOf(other, buffer).SequenceEqual(Type);
            }

            public Boolean Equals(TypeReference? other)
            {
                if (other is null)
                {
                    return IsEmpty;
                }

                Span<Char> buffer = stackalloc Char[Buffer];
                return AssemblyOf(other, buffer).SequenceEqual(Assembly) && TypeNameOf(other, buffer).SequenceEqual(Type);
            }

            internal Boolean Equals(CecilReference? other)
            {
                return other is not null ? Equals(other.Identifier) : IsEmpty;
            }

            Boolean IEquatable<CecilReference>.Equals(CecilReference? other)
            {
                return Equals(other);
            }

            public Boolean Equals(TypeKey other)
            {
                return Assembly == other.Assembly && Type == other.Type;
            }

            public override String ToString()
            {
                return Key;
            }
        }
    }
}