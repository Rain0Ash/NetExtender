using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using Mono.Collections.Generic;
using NetExtender.Types.Monads;

namespace NetExtender.Cecil
{
    internal class CecilReference : CecilMemberReference
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator CecilReference?(TypeReference? value)
        {
            return value switch
            {
                GenericInstanceType convert => (CecilGenericInstance) convert,
                GenericParameter convert => (CecilGenericParameter) convert,
                TypeDefinition convert => (CecilDefinition) convert,
                _ => Create(value)
            };
        }

        public String? Namespace
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Namespace;
            }
        }

        public String ScopeName
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.ScopeName;
            }
        }

        public MetadataType MetadataType
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.MetadataType;
            }
        }

        private readonly Int32? _rank;
        public Int32 ArrayRank
        {
            get
            {
                return _rank ?? throw new InvalidOperationException("Type is not an array.");
            }
            private init
            {
                _rank = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public Boolean IsArray
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _rank >= 0;
            }
        }

        public Boolean IsVector
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(3);
            }
            private init
            {
                Member.Set(3, value);
            }
        }

        public Boolean IsPinned
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(4);
            }
            private init
            {
                Member.Set(4, value);
            }
        }

        public Boolean IsByReference
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(5);
            }
            private init
            {
                Member.Set(5, value);
            }
        }

        public Boolean IsPointer
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(6);
            }
            private init
            {
                Member.Set(6, value);
            }
        }

        public Boolean IsFunctionPointer
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(7);
            }
            private init
            {
                Member.Set(7, value);
            }
        }

        public Boolean IsValueType
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(8);
            }
            private init
            {
                Member.Set(8, value);
            }
        }

        public Boolean IsPrimitive
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(9);
            }
            private init
            {
                Member.Set(9, value);
            }
        }

        public Boolean IsEnum
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(10);
            }
            private init
            {
                Member.Set(10, value);
            }
        }

        public Boolean IsSentinel
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(11);
            }
            private init
            {
                Member.Set(11, value);
            }
        }

        public Boolean IsNested
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return !DeclaringType.IsEmpty;
            }
        }

        public Boolean IsGenericInstance
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(12);
            }
            private init
            {
                Member.Set(12, value);
            }
        }

        public Boolean IsGenericParameter
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(13);
            }
            private init
            {
                Member.Set(13, value);
            }
        }

        public Boolean IsRequiredModifier
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(14);
            }
            private init
            {
                Member.Set(14, value);
            }
        }

        public Boolean IsOptionalModifier
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(15);
            }
            private init
            {
                Member.Set(15, value);
            }
        }

        public Boolean HasGenericParameters
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return GenericParameters.Length > 0;
            }
            private init
            {
                Member.Set(16, value);
            }
        }

        public ImmutableArray<CecilGenericParameter> GenericParameters { get; private set; }

        private Result<CecilReference> ElementType { get; set; }

        private Exception? _resolve;
        public virtual Result<CecilDefinition> Resolve
        {
            get
            {
                return _resolve is not null ? _resolve : this as CecilDefinition ?? default(Result<CecilDefinition>);
            }
        }

        private protected CecilReference(CecilMember member, TypeReference value)
            : base(member, value)
        {
            if (value is ArrayType array)
            {
                ArrayRank = array.Rank;
                IsVector = array.IsVector;
            }

            IsPinned = value.IsPinned;
            IsByReference = value.IsByReference;
            IsPointer = value.IsPointer;
            IsFunctionPointer = value.IsFunctionPointer;
            IsValueType = value.IsValueType;
            IsPrimitive = value.IsPrimitive;
            IsSentinel = value.IsSentinel;
            IsGenericInstance = value.IsGenericInstance;
            IsGenericParameter = value.IsGenericParameter;
            IsRequiredModifier = value.IsRequiredModifier;
            IsOptionalModifier = value.IsOptionalModifier;
            HasGenericParameters = value.HasGenericParameters;
        }

        [return: NotNullIfNotNull("value")]
        private static CecilReference? Create(TypeReference? value)
        {
            if (value is null)
            {
                return null;
            }

            Result<TypeDefinition?> resolve;

            try
            {
                resolve = value.Resolve();
            }
            catch (Exception exception)
            {
                resolve = exception;
            }

            if (resolve.Unwrap(out TypeDefinition? definition) && definition is not null)
            {
                CecilMember member = (CecilMember) definition;

                if (Storage.TryGetValue(member, out CecilMemberReference? wrapper) && wrapper is CecilDefinition result)
                {
                    return result;
                }

                result = new CecilDefinition(member, definition);
                Storage.AddOrUpdate(result);
                return result.Self(definition);
            }
            else
            {
                CecilMember member = (CecilMember) value;

                if (Storage.TryGetValue(member, out CecilMemberReference? wrapper))
                {
                    return (CecilReference) wrapper;
                }

                CecilReference result = new CecilReference(member, value)
                {
                    _resolve = resolve.Exception
                };

                Storage.AddOrUpdate(result);
                return result.Self(value);
            }
        }

        private protected override CecilReference Self(TypeReference value)
        {
            return InitializeDeclaring(value).InitializeElementType(value).Initialize(value.GenericParameters);
        }

        private protected override CecilReference InitializeDeclaring(TypeReference declaring)
        {
            return (CecilReference) base.InitializeDeclaring(declaring);
        }

        private protected virtual CecilReference Initialize(Collection<GenericParameter> parameters)
        {
            ImmutableArray<CecilGenericParameter>.Builder builder = ImmutableArray.CreateBuilder<CecilGenericParameter>(parameters.Count);

            foreach (GenericParameter parameter in parameters)
            {
                builder.Add(parameter);
            }

            GenericParameters = builder.MoveToImmutable();
            return this;
        }

        private protected virtual CecilReference InitializeElementType(TypeReference value)
        {
            try
            {
                TypeReference element = value.GetElementType();
                ElementType = element != value ? element : this;
            }
            catch (Exception exception)
            {
                ElementType = exception;
            }

            return this;
        }

        public CecilReference GetElementType()
        {
            return ElementType.Value;
        }

        public sealed override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }

        public sealed override Boolean Equals(Object? other)
        {
            return base.Equals(other);
        }
    }
}