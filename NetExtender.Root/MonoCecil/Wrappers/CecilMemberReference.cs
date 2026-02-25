using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using NetExtender.Types.Monads;
using NetExtender.Types.Storages;

namespace NetExtender.Cecil
{
    internal abstract class CecilMemberReference : IEquatable<CecilMemberReference>
    {
        private protected static KeyWeakStorage<CecilMember, CecilMemberReference> Storage { get; } = new KeyWeakStorage<CecilMember, CecilMemberReference>(static wrapper => wrapper.Member);

        private protected CecilMember Member;

        public MonoCecilType.TypeKey Identifier
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Identifier;
            }
        }

        public String Name
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Name;
            }
        }

        public String? FullName
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.FullName;
            }
        }

        public CecilModule Module
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Module;
            }
        }

        public MetadataToken MetadataToken
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.MetadataToken;
            }
        }

        public Boolean IsDefinition
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(0);
            }
            private init
            {
                Member.Set(0, value);
            }
        }

        public Boolean ContainsGenericParameter
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(1);
            }
            private init
            {
                Member.Set(1, value);
            }
        }

        public Boolean IsWindowsRuntimeProjection
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(2);
            }
            private init
            {
                Member.Set(2, value);
            }
        }

        public Result<CecilReference> DeclaringType { get; private set; }

        private protected CecilMemberReference(CecilMember member, TypeReference value)
        {
            Member = member;

            IsDefinition = value.IsDefinition;
            ContainsGenericParameter = value.ContainsGenericParameter;
            IsWindowsRuntimeProjection = value.IsWindowsRuntimeProjection;
        }

        private protected abstract CecilMemberReference Self(TypeReference value);

        private protected virtual CecilMemberReference InitializeDeclaring(TypeReference declaring)
        {
            try
            {
                DeclaringType = (CecilReference) declaring.DeclaringType;
            }
            catch (Exception exception)
            {
                DeclaringType = exception;
            }

            return this;
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override Int32 GetHashCode()
        {
            return Member.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other is CecilMemberReference wrapper && Equals(wrapper);
        }

        public Boolean Equals(CecilMemberReference? other)
        {
            return other is not null && Member.Equals(other.Member);
        }

        public override String ToString()
        {
            return Member.ToString();
        }
    }

    internal struct CecilMember : IEquatableStruct<CecilMember>
    {
        public static implicit operator CecilMember(TypeReference? value)
        {
            return value is not null ? new CecilMember(value) : default;
        }

        private UInt64 Attributes = default;

        public MonoCecilType.TypeKey Identifier { get; } = default;
        public String Name { get; } = String.Empty;
        public String? FullName { get; } = null;
        public String? Namespace { get; } = null;
        public String ScopeName { get; }
        public CecilModule Module { get; }
        public MetadataType MetadataType { get; }
        public MetadataToken MetadataToken { get; }

        public readonly Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Identifier.IsEmpty;
            }
        }

        private CecilMember(TypeReference value)
        {
            Identifier = new MonoCecilType.TypeKey(value);

            Span<Char> buffer = stackalloc Char[MonoCecilType.Buffer];

            if (value.Name is { Length: > 0 } name)
            {
                ReadOnlySpan<Char> @new = MonoCecilType.TypeKey.Format(name, buffer, out Int32 format);
                Name = format > 0 ? MonoCecilType.TypeKey.TypeNamePool.GetOrAdd(@new) : MonoCecilType.TypeKey.TypeNamePool.GetOrAdd(name);
            }

            if (value.FullName is { Length: > 0 } fullname)
            {
                ReadOnlySpan<Char> @new = MonoCecilType.TypeKey.Format(fullname, buffer, out Int32 format);
                FullName = format > 0 ? MonoCecilType.TypeKey.TypeFullNamePool.GetOrAdd(@new) : MonoCecilType.TypeKey.TypeFullNamePool.GetOrAdd(fullname);
            }

            if (value.Namespace is { Length: > 0 } @namespace)
            {
                ReadOnlySpan<Char> @new = MonoCecilType.TypeKey.Format(@namespace, buffer, out Int32 format);
                Namespace = format > 0 ? MonoCecilType.TypeKey.NamespacePool.GetOrAdd(@new) : MonoCecilType.TypeKey.NamespacePool.GetOrAdd(@namespace);
            }

            ScopeName = MonoCecilType.TypeKey.ScopeNamePool.GetOrAdd(MonoCecilType.GetScopeName(value));

            Module = value.Module;
            MetadataType = value.MetadataType;
            MetadataToken = value.MetadataToken;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Get(Byte i)
        {
            return (Attributes & (1UL << i)) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(Byte i, Boolean value)
        {
            if (value)
            {
                Attributes |= 1UL << i;
                return;
            }

            Attributes &= ~(1UL << i);
        }

        public override readonly Int32 GetHashCode()
        {
            return Identifier.GetHashCode();
        }

        public override readonly Boolean Equals(Object? other)
        {
            return other switch
            {
                null => IsEmpty,
                CecilMemberReference value => Equals(value),
                CecilMember value => Equals(value),
                _ => false
            };
        }

        public readonly Boolean Equals(CecilMember other)
        {
            return Identifier.Equals(other.Identifier);
        }

        public override readonly String ToString()
        {
            return Identifier.ToString();
        }
    }
}