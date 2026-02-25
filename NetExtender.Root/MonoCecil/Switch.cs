using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using Mono.Cecil;
using NetExtender.Exceptions;
using NetExtender.Types.Comparers;

namespace NetExtender.Cecil
{
    public abstract partial class MonoCecilType
    {
        private sealed class TypeSwitch : MonoCecilType
        {
            private MonoCecilType Switch;

            public override String Name
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.Name;
                }
            }

            public override String? FullName
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.FullName;
                }
            }

            public override String? Namespace
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.Namespace;
                }
            }

            protected override TypeKey Key { get; }

            internal override Boolean HasAssembly
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.HasAssembly;
                }
            }

            internal override CecilAssembly AssemblyInfo
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.AssemblyInfo;
                }
            }

            public override Assembly? Assembly
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.Assembly;
                }
            }

            public override Int32 MetadataToken
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.MetadataToken;
                }
            }

            public override Int32 ModuleMetadataToken
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.ModuleMetadataToken;
                }
            }

            public override Type? Type
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Resolve();
                }
            }

            internal override CecilReference? Reference
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.Reference;
                }
            }

            internal override CecilDefinition? Definition
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.Definition;
                }
            }

            private protected override TypeKind Kind
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TypeKind.Switch;
                }
            }

            public override TypeKind State
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.State;
                }
            }

            public override Boolean IsPublic
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsPublic;
                }
            }

            public override Boolean IsNotPublic
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsNotPublic;
                }
            }

            public override Boolean IsNestedPublic
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsNestedPublic;
                }
            }

            public override Boolean IsNestedPrivate
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsNestedPrivate;
                }
            }

            public override Boolean IsNestedFamily
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsNestedFamily;
                }
            }

            public override Boolean IsNestedAssembly
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsNestedAssembly;
                }
            }

            public override Boolean IsNestedFamilyAndAssembly
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsNestedFamilyAndAssembly;
                }
            }

            public override Boolean IsNestedFamilyOrAssembly
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsNestedFamilyOrAssembly;
                }
            }

            public override Boolean IsAutoLayout
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsAutoLayout;
                }
            }

            public override Boolean IsSequentialLayout
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsSequentialLayout;
                }
            }

            public override Boolean IsExplicitLayout
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsExplicitLayout;
                }
            }

            public override Boolean IsTypeDefinition
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsTypeDefinition;
                }
            }

            public override Boolean IsArray
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsArray;
                }
            }

            public override Boolean IsByReference
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsByReference;
                }
            }

            public override Boolean IsPointer
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsPointer;
                }
            }

            public override Boolean IsValueType
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsValueType;
                }
            }

            public override Boolean IsPrimitive
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsPrimitive;
                }
            }

            public override Boolean IsEnum
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsEnum;
                }
            }

            public override Boolean IsClass
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsClass;
                }
            }

            public override Boolean IsInterface
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsInterface;
                }
            }

            public override Boolean IsAbstract
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsAbstract;
                }
            }

            public override Boolean IsSealed
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsSealed;
                }
            }

            public override Boolean IsNested
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsNested;
                }
            }

            public override Boolean IsGenericType
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsGenericType;
                }
            }

            public override Boolean IsGenericTypeDefinition
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsGenericTypeDefinition;
                }
            }

            public override Boolean IsGenericInstance
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsGenericInstance;
                }
            }

            public override Boolean IsGenericParameter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsGenericParameter;
                }
            }

            public override Int32 GenericParametersCount
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.GenericParametersCount;
                }
            }

            public override Boolean IsSpecialName
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsSpecialName;
                }
            }

            public override Boolean IsImport
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsImport;
                }
            }

#if NET8_0_OR_GREATER
            [Obsolete("Formatter-based serialization is obsolete and should not be used.", DiagnosticId = "SYSLIB0050", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
#endif
            public override Boolean IsSerializable
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsSerializable;
                }
            }

            public override Boolean IsAnsiClass
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsAnsiClass;
                }
            }

            public override Boolean IsUnicodeClass
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsUnicodeClass;
                }
            }

            public override Boolean IsAutoClass
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsAutoClass;
                }
            }

            public override Boolean IsCompilerGenerated
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsCompilerGenerated;
                }
            }

            public override Boolean IsScan
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.IsScan;
                }
            }

            public override MonoCecilType? BaseType
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.BaseType;
                }
            }

            public override MonoCecilType? DeclaringType
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.DeclaringType;
                }
            }

            public override TypeList Generics
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return GenericsStorage.GetOrAdd(this, static (_, @switch) => @switch.Generics, Switch);
                }
            }

            public override TypeSet Interfaces
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return InterfacesStorage.GetOrAdd(this, static (_, @switch) => @switch.Interfaces, Switch);
                }
            }

            public override TypeSet Attributes
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return AttributesStorage.GetOrAdd(this, static (_, @switch) => @switch.Attributes, Switch);
                }
            }

            protected override TypeSet InheritAttributes
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Switch.InheritAttributes;
                }
            }

            public TypeSwitch(Type type)
            {
                Switch = new TypeWrapper(type);
                Key = Switch.Key;
            }

            public TypeSwitch(Assembly assembly, CecilReference type)
            {
                Switch = new DefinitionWrapper(assembly, type);
                Key = Switch.Key;
            }

            public TypeSwitch(CecilReference type)
            {
                Switch = new DefinitionWrapper(type);
                Key = Switch.Key;
            }

            public override Type? Resolve()
            {
                return Switch.Kind switch
                {
                    TypeKind.Type => Switch.Type,
                    TypeKind.Definition or TypeKind.Reference when Switch is { } @switch && @switch.Resolve() is { } type => Exchange(type, @switch),
                    TypeKind.Definition or TypeKind.OnlyDefinition or TypeKind.Reference or TypeKind.OnlyReference => null,
                    var kind => throw new EnumUndefinedOrNotSupportedException<TypeKind>(kind, nameof(Kind), null)
                };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private Type Exchange(Type type, MonoCecilType @switch)
            {
                Interlocked.CompareExchange(ref Switch, TypeToWrapper.GetOrAdd(type, static type => new TypeWrapper(type)), @switch);
                return type;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override MonoCecilType GetGenericTypeDefinition()
            {
                return Switch.GetGenericTypeDefinition();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override MonoCecilType? TryGetGenericTypeDefinition()
            {
                return Switch.TryGetGenericTypeDefinition();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Int32 CompareTo(Type? other, TypeComparer? comparer)
            {
                return Switch.CompareTo(other, comparer);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Int32 CompareTo(TypeReference? other, TypeComparer? comparer)
            {
                return Switch.CompareTo(other, comparer);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal override Int32 CompareTo(CecilReference? other, TypeComparer? comparer)
            {
                return Switch.CompareTo(other, comparer);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Int32 CompareTo(TypeDefinition? other, TypeComparer? comparer)
            {
                return Switch.CompareTo(other, comparer);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal override Int32 CompareTo(CecilDefinition? other, TypeComparer? comparer)
            {
                return Switch.CompareTo(other, comparer);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Int32 CompareTo(MonoCecilType? other, TypeComparer? comparer)
            {
                return Switch.CompareTo(other, comparer);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Equals(Type? type)
            {
                return Switch.Equals(type);
            }
        }
    }
}