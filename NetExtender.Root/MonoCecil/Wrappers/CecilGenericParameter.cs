using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using Mono.Collections.Generic;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Cecil
{
    internal sealed class CecilGenericParameter : CecilReference
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator CecilGenericParameter?(GenericParameter? value)
        {
            return Create(value);
        }

        public Int32 Position { get; }
        public GenericParameterType Type { get; }
        public Mono.Cecil.GenericParameterAttributes Attributes { get; }

        public Boolean IsNonVariant
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(50);
            }
            private init
            {
                Member.Set(50, value);
            }
        }

        public Boolean IsCovariant
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(51);
            }
            private init
            {
                Member.Set(51, value);
            }
        }

        public Boolean IsContravariant
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(52);
            }
            private init
            {
                Member.Set(52, value);
            }
        }

        public Boolean HasReferenceTypeConstraint
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(53);
            }
            private init
            {
                Member.Set(53, value);
            }
        }

        public Boolean HasNotNullableValueTypeConstraint
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(54);
            }
            private init
            {
                Member.Set(54, value);
            }
        }

        public Boolean HasDefaultConstructorConstraint
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(55);
            }
            private init
            {
                Member.Set(55, value);
            }
        }

        public Boolean AllowByRefLikeConstraint
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(56);
            }
            private init
            {
                Member.Set(56, value);
            }
        }

        public Boolean HasCustomAttributes
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return CustomAttributes.Length > 0;
            }
            private init
            {
                Member.Set(57, value);
            }
        }

        public ImmutableArray<CecilAttributeWrapper> CustomAttributes { get; private set; }

        public Boolean HasConstraints
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Constraints.Length > 0;
            }
            private init
            {
                Member.Set(58, value);
            }
        }

        public ImmutableArray<CecilGenericParameterConstraint> Constraints { get; private set; }

        private CecilGenericParameter(CecilMember member, GenericParameter value)
            : base(member, value)
        {
            Position = value.Position;
            Type = value.Type;
            Attributes = value.Attributes;
            HasCustomAttributes = value.HasCustomAttributes;
            HasConstraints = value.HasConstraints;
        }

        [return: NotNullIfNotNull("value")]
        private static CecilGenericParameter? Create(GenericParameter? value)
        {
            if (value is null)
            {
                return null;
            }

            CecilMember member = (CecilMember) value;
            if (Storage.TryGetValue(member, out CecilMemberReference? wrapper))
            {
                return (CecilGenericParameter) wrapper;
            }

            CecilGenericParameter result = new CecilGenericParameter(member, value);
            Storage.AddOrUpdate(result);
            return result.Self(value);
        }

        private protected override CecilGenericParameter Self(TypeReference value)
        {
            return Self((GenericParameter) value);
        }

        private CecilGenericParameter Self(GenericParameter value)
        {
            return InitializeDeclaring(value).InitializeElementType(value).Initialize(value.GenericParameters).Initialize(value.CustomAttributes).Initialize(value.Constraints);
        }

        private protected override CecilGenericParameter InitializeDeclaring(TypeReference declaring)
        {
            return (CecilGenericParameter) base.InitializeDeclaring(declaring);
        }

        private protected override CecilGenericParameter Initialize(Collection<GenericParameter> parameters)
        {
            return (CecilGenericParameter) base.Initialize(parameters);
        }

        private CecilGenericParameter Initialize(Collection<CustomAttribute> attributes)
        {
            ImmutableArray<CecilAttributeWrapper>.Builder builder = ImmutableArray.CreateBuilder<CecilAttributeWrapper>(attributes.Count);

            foreach (CustomAttribute attribute in attributes)
            {
                builder.Add(attribute);
            }

            CustomAttributes = builder.MoveToImmutable();
            return this;
        }

        private CecilGenericParameter Initialize(Collection<GenericParameterConstraint> constraints)
        {
            ImmutableArray<CecilGenericParameterConstraint>.Builder builder = ImmutableArray.CreateBuilder<CecilGenericParameterConstraint>(constraints.Count);

            foreach (GenericParameterConstraint constraint in constraints)
            {
                builder.Add(constraint);
            }

            Constraints = builder.MoveToImmutable();
            return this;
        }

        private protected override CecilGenericParameter InitializeElementType(TypeReference value)
        {
            return (CecilGenericParameter) base.InitializeElementType(value);
        }
    }

    internal readonly struct CecilGenericParameterConstraint : IEquatableStruct<CecilGenericParameterConstraint>
    {
        [ReflectionNaming(typeof(GenericParameterConstraint))]
        private static Func<Object?, Object?> generic_parameter { get; }

        static CecilGenericParameterConstraint()
        {
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            FieldInfo field = typeof(GenericParameterConstraint).GetField(nameof(generic_parameter), binding) ?? throw new MissingMemberException(nameof(GenericParameterConstraint), nameof(generic_parameter));
            generic_parameter = field.CreateDynamicGetDelegate();
        }

        public static implicit operator CecilGenericParameterConstraint(GenericParameterConstraint? value)
        {
            return value is not null ? new CecilGenericParameterConstraint(value) : default;
        }

        public CecilGenericParameter GenericParameter { get; }
        public CecilReference? ConstraintType { get; }
        public MetadataToken MetadataToken { get; }

        public Boolean IsEmpty
        {
            get
            {
                return GenericParameter is null;
            }
        }

        private CecilGenericParameterConstraint(GenericParameterConstraint value)
        {
            GenericParameter = (GenericParameter) generic_parameter(value)!;
            ConstraintType = value.ConstraintType;
            MetadataToken = value.MetadataToken;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(ConstraintType, MetadataToken);
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => IsEmpty,
                GenericParameterConstraint value => Equals(value),
                CecilGenericParameterConstraint value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(CecilGenericParameterConstraint other)
        {
            return MetadataToken == other.MetadataToken && EqualityComparer<CecilReference>.Default.Equals(GenericParameter, other.GenericParameter) && EqualityComparer<CecilReference>.Default.Equals(ConstraintType, other.ConstraintType);
        }

        public override String ToString()
        {
            return ConstraintType?.ToString() ?? String.Empty;
        }
    }
}