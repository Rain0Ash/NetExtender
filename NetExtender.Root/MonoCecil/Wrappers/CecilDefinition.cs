using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using Mono.Collections.Generic;
using NetExtender.Exceptions;
using NetExtender.Types.Monads;

namespace NetExtender.Cecil
{
    internal sealed class CecilDefinition : CecilReference
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator CecilDefinition?(TypeDefinition? value)
        {
            return Create(value);
        }

        public TypeAttributes Attributes { get; }

        public Boolean HasLayoutInfo
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(17);
            }
            private init
            {
                Member.Set(17, value);
            }
        }

        public Int16 PackingSize { get; }
        public Int32 ClassSize { get; }

        public Boolean IsPublic
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(18);
            }
            private init
            {
                Member.Set(18, value);
            }
        }

        public Boolean IsNotPublic
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(19);
            }
            private init
            {
                Member.Set(19, value);
            }
        }

        public Boolean IsNestedPublic
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(20);
            }
            private init
            {
                Member.Set(20, value);
            }
        }

        public Boolean IsNestedPrivate
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(21);
            }
            private init
            {
                Member.Set(21, value);
            }
        }

        public Boolean IsNestedFamily
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(22);
            }
            private init
            {
                Member.Set(22, value);
            }
        }

        public Boolean IsNestedAssembly
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(23);
            }
            private init
            {
                Member.Set(23, value);
            }
        }

        public Boolean IsNestedFamilyAndAssembly
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(24);
            }
            private init
            {
                Member.Set(24, value);
            }
        }

        public Boolean IsNestedFamilyOrAssembly
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(25);
            }
            private init
            {
                Member.Set(25, value);
            }
        }

        public Boolean IsAutoLayout
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(26);
            }
            private init
            {
                Member.Set(26, value);
            }
        }

        public Boolean IsSequentialLayout
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(27);
            }
            private init
            {
                Member.Set(27, value);
            }
        }

        public Boolean IsExplicitLayout
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(28);
            }
            private init
            {
                Member.Set(28, value);
            }
        }

        public Boolean IsClass
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(29);
            }
            private init
            {
                Member.Set(29, value);
            }
        }

        public Boolean IsInterface
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(30);
            }
            private init
            {
                Member.Set(30, value);
            }
        }

        public Boolean IsAbstract
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(31);
            }
            private init
            {
                Member.Set(31, value);
            }
        }

        public Boolean IsSealed
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(32);
            }
            private init
            {
                Member.Set(32, value);
            }
        }

        public Boolean IsSpecialName
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(33);
            }
            private init
            {
                Member.Set(33, value);
            }
        }

        public Boolean IsRuntimeSpecialName
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(34);
            }
            private init
            {
                Member.Set(34, value);
            }
        }

        public Boolean IsBeforeFieldInit
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(35);
            }
            private init
            {
                Member.Set(35, value);
            }
        }

        public Boolean IsImport
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(36);
            }
            private init
            {
                Member.Set(36, value);
            }
        }

        public Boolean IsSerializable
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(37);
            }
            private init
            {
                Member.Set(37, value);
            }
        }

        public Boolean IsWindowsRuntime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(38);
            }
            private init
            {
                Member.Set(38, value);
            }
        }

        public Boolean IsAnsiClass
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(39);
            }
            private init
            {
                Member.Set(39, value);
            }
        }

        public Boolean IsUnicodeClass
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(40);
            }
            private init
            {
                Member.Set(40, value);
            }
        }

        public Boolean IsAutoClass
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(41);
            }
            private init
            {
                Member.Set(41, value);
            }
        }

        public Boolean HasSecurity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Member.Get(42);
            }
            private init
            {
                Member.Set(42, value);
            }
        }

        public Boolean HasInterfaces
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Interfaces.Length > 0;
            }
            private init
            {
                Member.Set(43, value);
            }
        }

        public ImmutableArray<CecilInterfaceWrapper> Interfaces { get; private set; }

        public Boolean HasCustomAttributes
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return CustomAttributes.Length > 0;
            }
            private init
            {
                Member.Set(44, value);
            }
        }

        public ImmutableArray<CecilAttributeWrapper> CustomAttributes { get; private set; }

        public new CecilDefinition? DeclaringType
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return (CecilDefinition?) base.DeclaringType.Value;
            }
        }

        public CecilReference? BaseType { get; private set; }

        public override Result<CecilDefinition> Resolve
        {
            get
            {
                return this;
            }
        }

        internal CecilDefinition(CecilMember member, TypeDefinition value)
            : base(member, value)
        {
            Attributes = value.Attributes;
            HasLayoutInfo = value.HasLayoutInfo;
            PackingSize = value.PackingSize;
            ClassSize = value.ClassSize;
            IsPublic = value.IsPublic;
            IsNotPublic = value.IsNotPublic;
            IsNestedPublic = value.IsNestedPublic;
            IsNestedPrivate = value.IsNestedPrivate;
            IsNestedFamily = value.IsNestedFamily;
            IsNestedAssembly = value.IsNestedAssembly;
            IsNestedFamilyAndAssembly = value.IsNestedFamilyAndAssembly;
            IsNestedFamilyOrAssembly = value.IsNestedFamilyOrAssembly;
            IsAutoLayout = value.IsAutoLayout;
            IsSequentialLayout = value.IsSequentialLayout;
            IsExplicitLayout = value.IsExplicitLayout;
            IsClass = value.IsClass;
            IsInterface = value.IsInterface;
            IsAbstract = value.IsAbstract;
            IsSealed = value.IsSealed;
            IsSpecialName = value.IsSpecialName;
            IsRuntimeSpecialName = value.IsRuntimeSpecialName;
            IsBeforeFieldInit = value.IsBeforeFieldInit;
            IsImport = value.IsImport;
            IsSerializable = value.IsSerializable;
            IsWindowsRuntime = value.IsWindowsRuntime;
            IsAnsiClass = value.IsAnsiClass;
            IsUnicodeClass = value.IsUnicodeClass;
            IsAutoClass = value.IsAutoClass;
            HasSecurity = value.HasSecurity;
            HasInterfaces = value.HasInterfaces;
            HasCustomAttributes = value.HasCustomAttributes;
        }

        [return: NotNullIfNotNull("value")]
        private static CecilDefinition? Create(TypeDefinition? value)
        {
            if (value is null)
            {
                return null;
            }

            CecilMember member = (CecilMember) value;
            if (Storage.TryGetValue(member, out CecilMemberReference? wrapper))
            {
                return wrapper as CecilDefinition ?? throw new NeverOperationException($"Member '{member}' is not a definition.");
            }

            CecilDefinition result = new CecilDefinition(member, value);
            Storage.AddOrUpdate(result);
            return result.Self(value);
        }

        private protected override CecilDefinition Self(TypeReference value)
        {
            return Self((TypeDefinition) value);
        }

        private CecilDefinition Self(TypeDefinition value)
        {
            return InitializeDeclaring(value).InitializeElementType(value).Initialize(value.GenericParameters).Initialize(value.Interfaces).Initialize(value.CustomAttributes).InitializeBaseType(value);
        }

        private protected override CecilDefinition InitializeDeclaring(TypeReference declaring)
        {
            return (CecilDefinition) base.InitializeDeclaring(declaring);
        }

        private protected override CecilDefinition Initialize(Collection<GenericParameter> parameters)
        {
            return (CecilDefinition) base.Initialize(parameters);
        }

        private CecilDefinition Initialize(Collection<InterfaceImplementation> interfaces)
        {
            ImmutableArray<CecilInterfaceWrapper>.Builder builder = ImmutableArray.CreateBuilder<CecilInterfaceWrapper>(interfaces.Count);

            foreach (InterfaceImplementation @interface in interfaces)
            {
                builder.Add(@interface);
            }

            Interfaces = builder.MoveToImmutable();
            return this;
        }

        private CecilDefinition Initialize(Collection<CustomAttribute> attributes)
        {
            ImmutableArray<CecilAttributeWrapper>.Builder builder = ImmutableArray.CreateBuilder<CecilAttributeWrapper>(attributes.Count);

            foreach (CustomAttribute attribute in attributes)
            {
                builder.Add(attribute);
            }

            CustomAttributes = builder.MoveToImmutable();
            return this;
        }

        private protected override CecilDefinition InitializeElementType(TypeReference value)
        {
            return (CecilDefinition) base.InitializeElementType(value);
        }

        private CecilDefinition InitializeBaseType(TypeDefinition value)
        {
            BaseType = value.BaseType;
            return this;
        }
    }
}