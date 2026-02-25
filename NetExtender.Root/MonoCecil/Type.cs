using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using NetExtender.Exceptions;
using NetExtender.Types.Comparers;
using NetExtender.Utilities.Core;

namespace NetExtender.Cecil
{
    public abstract partial class MonoCecilType
    {
        private sealed class TypeWrapper : MonoCecilType
        {
            public override String Name
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.Name;
                }
            }

            public override String? FullName
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.FullName;
                }
            }

            public override String? Namespace
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.Namespace;
                }
            }

            protected override TypeKey Key { get; }

            internal override Boolean HasAssembly
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return true;
                }
            }

            internal override CecilAssembly AssemblyInfo
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Assembly;
                }
            }

            public override Assembly Assembly
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.Assembly;
                }
            }

            public override Int32 MetadataToken
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.MetadataToken;
                }
            }

            public override Int32 ModuleMetadataToken
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.Module.MetadataToken;
                }
            }

            public override Type Type { get; }

            internal override CecilReference? Reference
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TypeToReference.TryGetValue(Type, out CecilReference? value) ? value : null;
                }
            }

            internal override CecilDefinition? Definition
            {
                get
                {
                    return Reference switch
                    {
                        null => null,
                        CecilGenericInstance => null,
                        CecilDefinition definition => definition,
                        var reference when reference.Resolve.Unwrap(out CecilDefinition? resolve) => resolve,
                        _ => null
                    };
                }
            }

            private protected override TypeKind Kind
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TypeKind.Type;
                }
            }

            public override TypeKind State
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Kind;
                }
            }

            public override Boolean IsPublic
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsPublic;
                }
            }

            public override Boolean IsNotPublic
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsNotPublic;
                }
            }

            public override Boolean IsNestedPublic
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsNestedPublic;
                }
            }

            public override Boolean IsNestedPrivate
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsNestedPrivate;
                }
            }

            public override Boolean IsNestedFamily
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsNestedFamily;
                }
            }

            public override Boolean IsNestedAssembly
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsNestedAssembly;
                }
            }

            public override Boolean IsNestedFamilyAndAssembly
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsNestedFamANDAssem;
                }
            }

            public override Boolean IsNestedFamilyOrAssembly
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsNestedFamORAssem;
                }
            }

            public override Boolean IsAutoLayout
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsAutoLayout;
                }
            }

            public override Boolean IsSequentialLayout
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsLayoutSequential;
                }
            }

            public override Boolean IsExplicitLayout
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsExplicitLayout;
                }
            }

            public override Boolean IsTypeDefinition
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsTypeDefinition;
                }
            }

            public override Boolean IsArray
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsArray;
                }
            }

            public override Boolean IsByReference
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsByRef;
                }
            }

            public override Boolean IsPointer
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsPointer;
                }
            }

            public override Boolean IsValueType
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsValueType;
                }
            }

            public override Boolean IsPrimitive
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsPrimitive;
                }
            }

            public override Boolean IsEnum
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsEnum;
                }
            }

            public override Boolean IsClass
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsClass;
                }
            }

            public override Boolean IsInterface
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsInterface;
                }
            }

            public override Boolean IsAbstract
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsAbstract;
                }
            }

            public override Boolean IsSealed
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsSealed;
                }
            }

            public override Boolean IsNested
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsNested;
                }
            }

            public override Boolean IsGenericType
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsGenericType;
                }
            }

            public override Boolean IsGenericTypeDefinition
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsGenericTypeDefinition;
                }
            }

            public override Boolean IsGenericInstance
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type is { IsGenericType: true, IsGenericTypeDefinition: false };
                }
            }

            public override Boolean IsGenericParameter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsGenericParameter;
                }
            }

            public override Int32 GenericParametersCount
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.GetGenericArgumentsCount();
                }
            }

            public override Boolean IsSpecialName
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsSpecialName;
                }
            }

            public override Boolean IsImport
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsImport;
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
                    return Type.IsSerializable;
                }
            }

            public override Boolean IsAnsiClass
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsAnsiClass;
                }
            }

            public override Boolean IsUnicodeClass
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsUnicodeClass;
                }
            }

            public override Boolean IsAutoClass
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.IsAutoClass;
                }
            }

            public override Boolean IsCompilerGenerated
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.HasAttribute<CompilerGeneratedAttribute>(false);
                }
            }

            public override Boolean IsScan
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return !IsCompilerGenerated;
                }
            }

            public override MonoCecilType? BaseType
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return From(Type.BaseType);
                }
            }

            public override MonoCecilType? DeclaringType
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return From(Type.DeclaringType);
                }
            }

            public override TypeList Generics
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.TryGetGenericArguments() is { Length: > 0 } generics ? TypeList.Create(generics.Select(From)) : TypeList.Empty;
                }
            }

            public override TypeSet Interfaces
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TypeSet.Create(Type.GetSafeInterfacesUnsafe().Select(From));
                }
            }

            public override TypeSet Attributes
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return TypeSet.Create(Type.GetCustomAttributesData().Select(static data => data.AttributeType).Select(From));
                }
            }

            protected override TypeSet InheritAttributes
            {
                get
                {
                    static Boolean IsAttributeInherited(Type attribute)
                    {
                        return AttributeUtilities.GetCustomAttribute<AttributeUsageAttribute>(attribute)?.Inherited is not false;
                    }

                    return TypeSet.Create(Type.GetCustomAttributesData().Select(static data => data.AttributeType).Where(IsAttributeInherited).Select(From));
                }
            }

            public TypeWrapper(Type type)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
                Key = new TypeKey(type);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Type Resolve()
            {
                return Type;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override MonoCecilType GetGenericTypeDefinition()
            {
                return From(Type.GetGenericTypeDefinition());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override MonoCecilType TryGetGenericTypeDefinition()
            {
                return From(Type.TryGetGenericTypeDefinition());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Int32 CompareTo(Type? other, TypeComparer? comparer)
            {
                comparer ??= TypeComparer.FullNameOrdinal;
                return comparer.Compare(Type, other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Int32 CompareTo(TypeReference? other, TypeComparer? comparer)
            {
                comparer ??= TypeComparer.FullNameOrdinal;
                return comparer.Compare(this, From(other));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal override Int32 CompareTo(CecilReference? other, TypeComparer? comparer)
            {
                comparer ??= TypeComparer.FullNameOrdinal;
                return comparer.Compare(this, From(other));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Int32 CompareTo(TypeDefinition? other, TypeComparer? comparer)
            {
                comparer ??= TypeComparer.FullNameOrdinal;
                return comparer.Compare(this, From(other));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal override Int32 CompareTo(CecilDefinition? other, TypeComparer? comparer)
            {
                comparer ??= TypeComparer.FullNameOrdinal;
                return comparer.Compare(this, From(other));
            }

            public override Int32 CompareTo(MonoCecilType? other, TypeComparer? comparer)
            {
                return other?.Kind switch
                {
                    null => -1,
                    TypeKind.Switch => -other.CompareTo((MonoCecilType) this, comparer),
                    TypeKind.Type => CompareTo((MonoCecilType?) other.Type, comparer),
                    TypeKind.Definition or TypeKind.OnlyDefinition => CompareTo(other.Definition, comparer),
                    TypeKind.Reference or TypeKind.OnlyReference => CompareTo(other.Reference, comparer),
                    { } kind => throw new EnumUndefinedOrNotSupportedException<TypeKind>(kind, nameof(Kind), null)
                };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Boolean Equals(Type? type)
            {
                return Type == type;
            }
        }
    }
}