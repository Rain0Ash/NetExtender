using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using Mono.Collections.Generic;

namespace NetExtender.Cecil
{
    internal sealed class CecilGenericInstance : CecilReference
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator CecilGenericInstance?(GenericInstanceType? value)
        {
            return Create(value);
        }

        public CecilReference ElementType { get; private set; } = null!;

        public Boolean HasGenericArguments
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return GenericArguments.Length > 0;
            }
            private init
            {
                Member.Set(60, value);
            }
        }

        public ImmutableArray<CecilReference> GenericArguments { get; private set; }

        public CecilGenericInstance(CecilMember member, GenericInstanceType value)
            : base(member, value)
        {
            HasGenericArguments = value.HasGenericArguments;
        }

        [return: NotNullIfNotNull("value")]
        private static CecilGenericInstance? Create(GenericInstanceType? value)
        {
            if (value is null)
            {
                return null;
            }

            CecilMember member = (CecilMember) value;
            if (Storage.TryGetValue(member, out CecilMemberReference? wrapper))
            {
                return (CecilGenericInstance) wrapper;
            }

            CecilGenericInstance result = new CecilGenericInstance(member, value);
            Storage.AddOrUpdate(result);
            return result.Self(value);
        }

        private protected override CecilGenericInstance Self(TypeReference value)
        {
            return Self((GenericInstanceType) value);
        }

        private CecilGenericInstance Self(GenericInstanceType value)
        {
            return InitializeDeclaring(value).InitializeElementType(value).Initialize(value.GenericParameters).Initialize(value.GenericArguments);
        }

        private protected override CecilGenericInstance InitializeDeclaring(TypeReference declaring)
        {
            return (CecilGenericInstance) base.InitializeDeclaring(declaring);
        }

        private protected override CecilGenericInstance Initialize(Collection<GenericParameter> parameters)
        {
            return (CecilGenericInstance) base.Initialize(parameters);
        }

        private CecilGenericInstance Initialize(Collection<TypeReference> arguments)
        {
            ImmutableArray<CecilReference>.Builder builder = ImmutableArray.CreateBuilder<CecilReference>(arguments.Count);

            foreach (TypeReference argument in arguments)
            {
                builder.Add(argument);
            }

            GenericArguments = builder.MoveToImmutable();
            return this;
        }

        private protected override CecilGenericInstance InitializeElementType(TypeReference value)
        {
            return (CecilGenericInstance) base.InitializeElementType(value);
        }

        private CecilGenericInstance InitializeElementType(GenericInstanceType value)
        {
            ElementType = value.ElementType;
            return InitializeElementType((TypeReference) value);
        }
    }
}