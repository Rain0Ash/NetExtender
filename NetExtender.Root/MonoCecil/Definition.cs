using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using NetExtender.Exceptions;
using NetExtender.Types.Comparers;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.Cecil
{
    public abstract partial class MonoCecilType
    {
        private sealed class DefinitionWrapper : MonoCecilType
        {
            public override String Name
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference.Name;
                }
            }

            public override String? FullName
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference.FullName;
                }
            }

            public override String? Namespace
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference.Namespace;
                }
            }

            protected override TypeKey Key
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference.Identifier;
                }
            }

            internal override Boolean HasAssembly
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Assembly is not null;
                }
            }

            internal override CecilAssembly AssemblyInfo { get; }

            private Assembly? _assembly;
            public override Assembly? Assembly
            {
                get
                {
                    if (_assembly is not null)
                    {
                        return _assembly;
                    }

                    if (!AssemblyInfo.IsEmpty && Assemblies.TryGetValue(AssemblyInfo, out Assembly? result))
                    {
                        return _assembly = result;
                    }

                    String scope = Reference.ScopeName;
                    if (String.IsNullOrWhiteSpace(scope))
                    {
                        return null;
                    }

                    if (ScopeToAssembly.TryGetValue(scope, out result))
                    {
                        return _assembly = result;
                    }

                    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        if (!String.Equals(GetAssemblyName(assembly), scope, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        _assembly = assembly;
                        ScopeToAssembly.TryAdd(scope, _assembly);

                        if (!AssemblyInfo.IsEmpty)
                        {
                            Assemblies.TryAdd(AssemblyInfo, _assembly);
                        }

                        return _assembly;
                    }

                    return null;
                }
            }

            public override Int32 MetadataToken
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference.MetadataToken.ToInt32();
                }
            }

            public override Int32 ModuleMetadataToken
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference.Module.MetadataToken.ToInt32();
                }
            }

            internal override CecilReference Reference { get; }

            private readonly CecilDefinition? _definition;
            internal override CecilDefinition? Definition
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _definition;
                }
            }

            private Type? _type;
            public override Type? Type
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _resolved is null ? _type ??= Resolve() : _type;
                }
            }

            private Boolean? _resolved;

            private protected override TypeKind Kind
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    if (_resolved is false)
                    {
                        return Definition is not null ? TypeKind.OnlyDefinition : TypeKind.OnlyReference;
                    }

                    return Definition is not null ? TypeKind.Definition : TypeKind.Reference;
                }
            }

            public override TypeKind State
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _resolved is true ? TypeKind.Type : Kind;
                }
            }

            public override Boolean IsPublic
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsPublic ?? false;
                }
            }

            public override Boolean IsNotPublic
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsNotPublic ?? false;
                }
            }

            public override Boolean IsNestedPublic
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsNestedPublic ?? false;
                }
            }

            public override Boolean IsNestedPrivate
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsNestedPrivate ?? false;
                }
            }

            public override Boolean IsNestedFamily
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsNestedFamily ?? false;
                }
            }

            public override Boolean IsNestedAssembly
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsNestedAssembly ?? false;
                }
            }

            public override Boolean IsNestedFamilyAndAssembly
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsNestedFamilyAndAssembly ?? false;
                }
            }

            public override Boolean IsNestedFamilyOrAssembly
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsNestedFamilyOrAssembly ?? false;
                }
            }

            public override Boolean IsAutoLayout
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsAutoLayout ?? false;
                }
            }

            public override Boolean IsSequentialLayout
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsSequentialLayout ?? false;
                }
            }

            public override Boolean IsExplicitLayout
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsExplicitLayout ?? false;
                }
            }

            public override Boolean IsTypeDefinition
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference.IsDefinition;
                }
            }

            public override Boolean IsArray
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference.IsArray;
                }
            }

            public override Boolean IsByReference
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference.IsByReference;
                }
            }

            public override Boolean IsPointer
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference.IsPointer;
                }
            }

            public override Boolean IsValueType
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference.IsValueType;
                }
            }

            public override Boolean IsPrimitive
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference.IsPrimitive;
                }
            }

            public override Boolean IsEnum
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsEnum ?? false;
                }
            }

            public override Boolean IsClass
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsClass ?? false;
                }
            }

            public override Boolean IsInterface
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsInterface ?? false;
                }
            }

            public override Boolean IsAbstract
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsAbstract ?? false;
                }
            }

            public override Boolean IsSealed
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsSealed ?? false;
                }
            }

            public override Boolean IsNested
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference.IsNested;
                }
            }

            public override Boolean IsGenericType
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference.IsGenericInstance || Reference is { HasGenericParameters: true, IsGenericParameter: false };
                }
            }

            public override Boolean IsGenericTypeDefinition
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference is { HasGenericParameters: true, IsGenericParameter: false, IsGenericInstance: false };
                }
            }

            public override Boolean IsGenericInstance
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference.IsGenericInstance;
                }
            }

            public override Boolean IsGenericParameter
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference.IsGenericParameter;
                }
            }

            public override Int32 GenericParametersCount
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Reference is CecilGenericInstance instance ? instance.GenericArguments.Length : Reference.GenericParameters.Length;
                }
            }

            public override Boolean IsSpecialName
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsSpecialName ?? false;
                }
            }

            public override Boolean IsImport
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsImport ?? false;
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
                    return Definition?.IsSerializable ?? false;
                }
            }

            public override Boolean IsAnsiClass
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsAnsiClass ?? false;
                }
            }

            public override Boolean IsUnicodeClass
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsUnicodeClass ?? false;
                }
            }

            public override Boolean IsAutoClass
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Definition?.IsAutoClass ?? false;
                }
            }

            public override Boolean IsCompilerGenerated
            {
                get
                {
                    if (Reference is CecilGenericInstance && Type is not null)
                    {
                        return Type.HasAttribute<CompilerGeneratedAttribute>(false);
                    }

                    return Definition is not null && FindCompilerGenerated(Definition.CustomAttributes) is not null;
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
                get
                {
                    if (Reference is CecilGenericInstance && Type is not null)
                    {
                        return From(Type.BaseType);
                    }

                    try
                    {
                        return Definition?.BaseType is { } reference ? From(reference) : null;
                    }
                    catch (NotSupportedException)
                    {
                        return Definition?.BaseType is { } @base ? From(@base) ?? From(Type?.BaseType) : From(Type?.BaseType);
                    }
                }
            }

            public override MonoCecilType? DeclaringType
            {
                get
                {
                    Result<CecilReference> result = Reference.DeclaringType;

                    if (result.IsEmpty)
                    {
                        return null;
                    }

                    if (result.Unwrap(out CecilReference? declaring))
                    {
                        return From(declaring) ?? From(Type?.DeclaringType);
                    }

                    return From(Type);
                }
            }

            public override TypeList Generics
            {
                get
                {
                    IEnumerable<MonoCecilType?>? result = Reference switch
                    {
                        CecilGenericInstance instance => instance.GenericArguments.Select(static type => From(type)),
                        { HasGenericParameters: true } => Reference.GenericParameters.Select(static parameter => From(parameter)),
                        _ => null
                    };

                    return TypeList.Create(result);
                }
            }

            public override TypeSet Interfaces
            {
                get
                {
                    if (Reference is CecilGenericInstance && Type is not null)
                    {
                        IEnumerable<MonoCecilType?> result = Type.GetSafeInterfacesUnsafe().Select(static type => From(type));
                        return TypeSet.Create(result);
                    }

                    HashSet<MonoCecilType?> interfaces = new HashSet<MonoCecilType?>();

                    if (Definition is not null)
                    {
                        foreach (CecilInterfaceWrapper wrapper in Definition.Interfaces)
                        {
                            MonoCecilType? @interface = From(wrapper.InterfaceType);

                            if (interfaces.Add(@interface))
                            {
                                CollectParentInterfaces(@interface, interfaces);
                            }
                        }
                    }

                    CollectParentInterfaces(BaseType, interfaces);
                    return TypeSet.Create(interfaces);
                }
            }

            public override TypeSet Attributes
            {
                get
                {
                    if (Reference is CecilGenericInstance && Type is not null)
                    {
                        IEnumerable<MonoCecilType?> result = Type.GetCustomAttributesData().Select(static type => From(type.AttributeType));
                        return TypeSet.Create(result);
                    }

                    HashSet<MonoCecilType?> attributes = new HashSet<MonoCecilType?>();

                    if (Definition is not null)
                    {
                        foreach (CecilAttributeWrapper attribute in Definition.CustomAttributes)
                        {
                            attributes.Add(From(attribute.AttributeType));
                        }
                    }

                    CollectParentAttributes(From(Definition), attributes);
                    return TypeSet.Create(attributes);
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

                    static Boolean IsReferenceInherited(CecilReference attribute)
                    {
                        attribute.Resolve.Unwrap(out CecilDefinition? definition);
                        CecilAttributeWrapper? usage = definition is not null ? FindAttributeUsage(definition.CustomAttributes) : null;
                        return IsPropertiesInherited(usage?.Properties ?? ImmutableArray<CecilAttributeNamedArgument>.Empty);
                    }

                    if (Reference is CecilGenericInstance && Type is not null)
                    {
                        return TypeSet.Create(Type.GetCustomAttributesData().Select(static type => type.AttributeType).Where(IsAttributeInherited).Select(From));
                    }

                    return TypeSet.Create(Definition?.CustomAttributes.Select(static attribute => attribute.AttributeType).Where(IsReferenceInherited).Select(From));
                }
            }

            public DefinitionWrapper(Assembly assembly, CecilReference type)
            {
                _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
                Reference = type ?? throw new ArgumentNullException(nameof(type));
                AssemblyInfo = Reference.Module.Assembly ?? (CecilAssembly) _assembly;

                Resolve(Reference).Unwrap(out _definition);
            }

            public DefinitionWrapper(CecilReference type)
            {
                Reference = type ?? throw new ArgumentNullException(nameof(type));
                AssemblyInfo = type.Module.Assembly;

                Resolve(Reference).Unwrap(out _definition);
            }

            private static Result<CecilDefinition> Resolve(CecilReference reference)
            {
                return reference is CecilGenericInstance instance ? instance.ElementType.Resolve : reference.Resolve;
            }

            [return: NotNullIfNotNull("resolve")]
            private Type? SetResolve(Type? resolve)
            {
                if (resolve is null)
                {
                    _resolved = false;
                    return null;
                }

                TypeToReference[resolve] = Reference;
                TypeToWrapper[resolve] = this;
                _type = resolve;
                _resolved = true;
                return resolve;
            }

            private Type? SetResolve(Exception? exception)
            {
                if (exception is not null)
                {
                    Fail?.Invoke(this, exception);
                }

                _resolved = false;
                return null;
            }

            private Boolean Resolve(Assembly assembly, String target, [MaybeNullWhen(false)] out Type result)
            {
                Module[] modules = assembly.GetModules();

                foreach (Module module in modules.OrderByDescending(module => module.MetadataToken == ModuleMetadataToken).ThenByDescending(module => String.Equals(module.Name, target, StringComparison.OrdinalIgnoreCase)))
                {
                    if (module.SafeResolveType(MetadataToken).Unwrap(out result))
                    {
                        return true;
                    }
                }

                result = null;
                return false;
            }

            public override Type? Resolve()
            {
                if (_resolved is not null)
                {
                    return _type;
                }

                lock (Reference)
                {
                    try
                    {
                        switch (Reference)
                        {
                            case { IsArray: true }:
                            {
                                Type? element = From(Reference.GetElementType())?.Resolve();
                                return element is not null ? SetResolve(Reference.IsVector ? element.MakeArrayType() : element.MakeArrayType(Reference.ArrayRank)) : SetResolve(element);
                            }
                            case { IsByReference: true }:
                            {
                                return SetResolve(From(Reference.GetElementType())?.Resolve()?.MakeByRefType());
                            }
                            case { IsPointer: true }:
                            {
                                return SetResolve(From(Reference.GetElementType())?.Resolve()?.MakePointerType());
                            }
                            case { IsFunctionPointer: true }:
                            {
                                CecilReference element = Reference.GetElementType();
                                return SetResolve(!Equals(element, Reference) ? From(element)?.Resolve() : null);
                            }
                            case { IsPinned: true }:
                            case { IsRequiredModifier: true }:
                            case { IsOptionalModifier: true }:
                            {
                                return SetResolve(From(Reference.GetElementType())?.Resolve());
                            }
                            case { IsGenericParameter: true }:
                            {
                                return SetResolve(Reference is CecilGenericParameter parameter ? ResolveGenericParameter(parameter) : null);
                            }
                            case { Name: "<Module>" }:
                            {
                                return SetResolve((Type?) null);
                            }
                            case CecilGenericInstance instance:
                            {
                                return SetResolve(ResolveGenericInstance(instance));
                            }
                            case not null when Assembly is { } assembly:
                            {
                                Type result = assembly.ManifestModule.ResolveType(MetadataToken);
                                return SetResolve(result);
                            }
                            default:
                            {
                                return null;
                            }
                        }
                    }
                    catch (ArgumentOutOfRangeException exception)
                    {
                        return SetResolve(exception);
                    }
                    catch (ArgumentException exception) when (Assembly is { } assembly)
                    {
                        return Resolve(assembly, Reference.Module.Name, out Type? result) ? SetResolve(result) : SetResolve(exception);
                    }
                    catch (ArgumentException)
                    {
                        return null;
                    }
                    catch (Exception exception)
                    {
                        return SetResolve(exception);
                    }
                }
            }

            private static Type? ResolveGenericInstance(CecilGenericInstance instance)
            {
                try
                {
                    if (From(instance.ElementType)?.Resolve() is not { IsGenericTypeDefinition: true } element)
                    {
                        return null;
                    }

                    Type?[] arguments = instance.GenericArguments.Select(static argument => argument is CecilGenericInstance generic ? ResolveGenericInstance(generic) : From(argument)?.Resolve()).ToArray();
                    return arguments.Any(static argument => argument is null) ? null : element.MakeGenericType(arguments!);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            private static Type? ResolveGenericParameter(CecilGenericParameter parameter)
            {
                if (!parameter.DeclaringType.Unwrap(out CecilReference? reference))
                {
                    return null;
                }

                if (From(reference)?.Resolve() is not { } declaring)
                {
                    return null;
                }

                if (parameter.Type is not GenericParameterType.Type)
                {
                    return null;
                }

                Type[] arguments = declaring.GetGenericArguments();
                return unchecked((UInt32) parameter.Position) < unchecked((UInt32) arguments.Length) ? arguments[parameter.Position] : null;
            }

            public override MonoCecilType GetGenericTypeDefinition()
            {
                if (IsGenericTypeDefinition)
                {
                    return this;
                }

                if (Reference is CecilGenericInstance instance)
                {
                    return From(instance.ElementType) ?? throw new InvalidOperationException($"Cannot get generic type definition for {Reference.FullName}");
                }

                return Reference is { HasGenericParameters: true, IsGenericParameter: false } ? this : throw new InvalidOperationException($"Type '{Reference.FullName}' is not a generic type.");
            }

            public override MonoCecilType? TryGetGenericTypeDefinition()
            {
                if (IsGenericTypeDefinition)
                {
                    return this;
                }

                if (Reference is CecilGenericInstance instance)
                {
                    return From(instance.ElementType);
                }

                return this;
            }

            private static void CollectParentInterfaces(MonoCecilType? @base, HashSet<MonoCecilType?> interfaces)
            {
                start:
                if (@base is null)
                {
                    return;
                }

                if (@base.State is TypeKind.Type)
                {
                    foreach (MonoCecilType @interface in @base.Interfaces)
                    {
                        interfaces.Add(@interface);
                    }

                    return;
                }

                foreach (MonoCecilType @interface in @base.Interfaces)
                {
                    if (interfaces.Add(@interface))
                    {
                        CollectParentInterfaces(@interface, interfaces);
                    }
                }

                @base = @base.BaseType;
                goto start;
            }

            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            private static void CollectParentAttributes(MonoCecilType? @base, HashSet<MonoCecilType?> attributes)
            {
                start:
                if (@base is null)
                {
                    return;
                }

                if (@base.State is TypeKind.Type)
                {
                    foreach (MonoCecilType @interface in @base.InheritAttributes)
                    {
                        attributes.Add(@interface);
                    }

                    return;
                }

                foreach (MonoCecilType attribute in @base.InheritAttributes)
                {
                    attributes.Add(attribute);
                }

                @base = @base.BaseType;
                goto start;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static Boolean IsPropertiesInherited(ImmutableArray<CecilAttributeNamedArgument> properties)
            {
                foreach (CecilAttributeNamedArgument wrapper in properties)
                {
                    if (wrapper is { Name: "Inherited", Value: Boolean value })
                    {
                        return value;
                    }
                }

                return true;
            }

            private const String AttributeUsageAttribute = $"{nameof(System)}.{nameof(System.AttributeUsageAttribute)}";

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
            private static CecilAttributeWrapper? FindAttributeUsage(ImmutableArray<CecilAttributeWrapper> attributes)
            {
                foreach (CecilAttributeWrapper wrapper in attributes)
                {
                    if (wrapper.AttributeType.FullName == AttributeUsageAttribute)
                    {
                        return wrapper;
                    }
                }

                return null;
            }

            private const String CompilerGeneratedAttribute = $"{nameof(System)}.{nameof(System.Runtime)}.{nameof(System.Runtime.CompilerServices)}.{nameof(System.Runtime.CompilerServices.CompilerGeneratedAttribute)}";

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
            private static CecilAttributeWrapper? FindCompilerGenerated(ImmutableArray<CecilAttributeWrapper> attributes)
            {
                foreach (CecilAttributeWrapper wrapper in attributes)
                {
                    if (wrapper.AttributeType.FullName == CompilerGeneratedAttribute)
                    {
                        return wrapper;
                    }
                }

                return null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Int32 CompareTo(Type? other, TypeComparer? comparer)
            {
                comparer ??= TypeComparer.FullNameOrdinal;
                return comparer.Compare(this, From(other));
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
                return _type is not null ? _type == type : Key.Equals(type);
            }
        }
    }
}