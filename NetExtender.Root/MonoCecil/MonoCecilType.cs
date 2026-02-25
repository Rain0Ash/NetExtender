using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using NetExtender.Types.Comparers;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Core;

#if CECIL
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil.Rocks;
#endif

namespace NetExtender.Cecil
{
    [DebuggerDisplay("{Identifier}")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    [SuppressMessage("ReSharper", "CheckForReferenceEqualityInstead.1")]
    public abstract partial class MonoCecilType : IScannable, IEquatable<MonoCecilType.TypeKey>, IEquality<Type>, IEquality<TypeReference>, IEquality<TypeDefinition>, IEquality<MonoCecilType>
    {
        public static implicit operator Type?(MonoCecilType? value)
        {
            return value?.Type;
        }

        [return: NotNullIfNotNull("value")]
        public static implicit operator MonoCecilType?(Type? value)
        {
            return From(value);
        }

        public static implicit operator MonoCecilType?(TypeDefinition? value)
        {
            return From(value);
        }

        public static Boolean operator ==(MonoCecilType? first, Type? second)
        {
            if (first is null && second is null)
            {
                return true;
            }

            return first?.Equals(second) ?? false;
        }

        public static Boolean operator !=(MonoCecilType? first, Type? second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(MonoCecilType? first, TypeReference? second)
        {
            if (first is null && second is null)
            {
                return true;
            }

            return first?.Equals(second) ?? false;
        }

        public static Boolean operator !=(MonoCecilType? first, TypeReference? second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(MonoCecilType? first, MonoCecilType? second)
        {
            if (first is null && second is null)
            {
                return true;
            }

            return first?.Equals(second) ?? false;
        }

        public static Boolean operator !=(MonoCecilType? first, MonoCecilType? second)
        {
            return !(first == second);
        }

        public enum TypeKind : Byte
        {
            Switch,
            Type,
            Definition,
            OnlyDefinition,
            Reference,
            OnlyReference
        }

        private static ConcurrentDictionary<CecilAssembly, Assembly> Assemblies { get; } = new ConcurrentDictionary<CecilAssembly, Assembly>();
        private static ConcurrentDictionary<String, Assembly?> ScopeToAssembly { get; } = new ConcurrentDictionary<String, Assembly?>(StringComparer.OrdinalIgnoreCase);
        internal static ConcurrentDictionary<Assembly, TypeSet?> Storage { get; } = new ConcurrentDictionary<Assembly, TypeSet?>();
        private static ConcurrentDictionary<MonoCecilType, TypeList> GenericsStorage { get; } = new ConcurrentDictionary<MonoCecilType, TypeList>();
        private static ConcurrentDictionary<MonoCecilType, TypeSet> InterfacesStorage { get; } = new ConcurrentDictionary<MonoCecilType, TypeSet>();
        private static ConcurrentDictionary<MonoCecilType, TypeSet> AttributesStorage { get; } = new ConcurrentDictionary<MonoCecilType, TypeSet>();
        private static ConcurrentDictionary<Type, CecilReference> TypeToReference { get; } = new ConcurrentDictionary<Type, CecilReference>();
        private static ConcurrentDictionary<Type, MonoCecilType> TypeToWrapper { get; } = new ConcurrentDictionary<Type, MonoCecilType>();
        private static ConcurrentDictionary<CecilReference, MonoCecilType> ReferenceToWrapper { get; } = new ConcurrentDictionary<CecilReference, MonoCecilType>();

        public static event EventHandler<Exception>? Fail;

#if DEBUG
        static MonoCecilType()
        {
            Fail += static (sender, exception) =>
            {
                CecilReference? reference = (sender as MonoCecilType)?.Reference;
                Debug.WriteLine($"Can't resolve type: '{reference?.Name}' in module '{reference?.Module.Name}'. Exception: {exception.Message}.");
            };
        }
#endif

        public abstract String Name { get; }
        public abstract String? FullName { get; }
        public abstract String? Namespace { get; }

        public String Identifier
        {
            get
            {
                return FullName ?? Name;
            }
        }

        protected abstract TypeKey Key { get; }
        internal abstract Boolean HasAssembly { get; }
        internal abstract CecilAssembly AssemblyInfo { get; }
        public abstract Assembly? Assembly { get; }
        public abstract Int32 MetadataToken { get; }
        public abstract Int32 ModuleMetadataToken { get; }
        public abstract Type? Type { get; }
        internal abstract CecilReference? Reference { get; }
        internal abstract CecilDefinition? Definition { get; }
        private protected abstract TypeKind Kind { get; }
        public abstract TypeKind State { get; }

        public abstract Boolean IsPublic { get; }
        public abstract Boolean IsNotPublic { get; }
        public abstract Boolean IsNestedPublic { get; }
        public abstract Boolean IsNestedPrivate { get; }
        public abstract Boolean IsNestedFamily { get; }
        public abstract Boolean IsNestedAssembly { get; }
        public abstract Boolean IsNestedFamilyAndAssembly { get; }
        public abstract Boolean IsNestedFamilyOrAssembly { get; }
        public abstract Boolean IsAutoLayout { get; }
        public abstract Boolean IsSequentialLayout { get; }
        public abstract Boolean IsExplicitLayout { get; }
        public abstract Boolean IsTypeDefinition { get; }
        public abstract Boolean IsArray { get; }
        public abstract Boolean IsByReference { get; }
        public abstract Boolean IsPointer { get; }
        public abstract Boolean IsValueType { get; }
        public abstract Boolean IsPrimitive { get; }
        public abstract Boolean IsEnum { get; }
        public abstract Boolean IsClass { get; }
        public abstract Boolean IsInterface { get; }
        public abstract Boolean IsAbstract { get; }
        public abstract Boolean IsSealed { get; }
        public abstract Boolean IsNested { get; }
        public abstract Boolean IsGenericType { get; }
        public abstract Boolean IsGenericTypeDefinition { get; }
        public abstract Boolean IsGenericInstance { get; }
        public abstract Boolean IsGenericParameter { get; }
        public abstract Int32 GenericParametersCount { get; }
        public abstract Boolean IsSpecialName { get; }
        public abstract Boolean IsImport { get; }

#if NET8_0_OR_GREATER
        [Obsolete("Formatter-based serialization is obsolete and should not be used.", DiagnosticId = "SYSLIB0050", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
#endif
        public abstract Boolean IsSerializable { get; }

        public abstract Boolean IsAnsiClass { get; }
        public abstract Boolean IsUnicodeClass { get; }
        public abstract Boolean IsAutoClass { get; }
        public abstract Boolean IsCompilerGenerated { get; }
        public abstract Boolean IsScan { get; }
        public abstract MonoCecilType? BaseType { get; }
        public abstract MonoCecilType? DeclaringType { get; }
        public abstract TypeList Generics { get; }
        public abstract TypeSet Interfaces { get; }
        public abstract TypeSet Attributes { get; }
        protected abstract TypeSet InheritAttributes { get; }

        [return: NotNullIfNotNull("type")]
        public static MonoCecilType? From(Type? type)
        {
            return type is not null ? TypeToWrapper.GetOrAdd(type, static type => new TypeWrapper(type)) : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MonoCecilType? From(TypeReference? type)
        {
            return From((CecilReference?) type);
        }

        internal static MonoCecilType? From(CecilReference? type)
        {
            return type is not null ? ReferenceToWrapper.GetOrAdd(type, static type => new TypeSwitch(type)) : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TypeSet From(Assembly assembly)
        {
            return From(assembly, out TypeSet? result) ? result : TypeSet.Empty;
        }

        public static Boolean From(Assembly assembly, [MaybeNullWhen(false)] out TypeSet result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            result = Storage.GetOrAdd(assembly, static assembly =>
            {
#if !CECIL
                return TypeSet.Create(assembly.GetSafeTypesUnsafe());
#else
                AssemblyDefinition? definition = null;

                try
                {
                    if (assembly.Location is not { Length: > 0 } location)
                    {
                        return TypeSet.Create(assembly.GetSafeTypesUnsafe());
                    }

                    definition = AssemblyDefinition.ReadAssembly(location);
                    Assemblies[(definition, location)] = assembly;
                }
                catch (Exception)
                {
                    definition?.Dispose();
                    return TypeSet.Create(assembly.GetSafeTypesUnsafe());
                }

                HashSet<MonoCecilType?> result = new HashSet<MonoCecilType?>();

                foreach (TypeDefinition type in definition.Modules.SelectMany(static module => module.GetAllTypes()))
                {
                    if (type is not null)
                    {
                        result.Add(ReferenceToWrapper.GetOrAdd(type, static (type, assembly) => new TypeSwitch(assembly, type), assembly));
                    }
                }

                definition.Dispose();
                return TypeSet.Create(result);
#endif
            });

            return result is not null;
        }

        public abstract Type? Resolve();
        public abstract MonoCecilType GetGenericTypeDefinition();
        public abstract MonoCecilType? TryGetGenericTypeDefinition();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static String GetAssemblyName(Assembly assembly)
        {
            return assembly.FullName ?? assembly.GetName().FullName;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static String GetScopeName(TypeReference reference)
        {
            return reference.Scope switch
            {
                ModuleDefinition module when Assemblies.TryGetValue(module.Assembly, out Assembly? assembly) => GetAssemblyName(assembly),
                ModuleDefinition module => module.Assembly?.Name?.FullName ?? module.Name,
                ModuleReference module => module.Name,
                AssemblyNameReference assembly => assembly.FullName,
                _ => reference.Module.Assembly?.Name?.FullName ?? reference.Module.Name
            };
        }

        public Int32 CompareTo(Type? other)
        {
            return CompareTo(other, null);
        }

        public abstract Int32 CompareTo(Type? other, TypeComparer? comparer);

        public Int32 CompareTo(TypeReference? other)
        {
            return CompareTo(other, null);
        }

        public abstract Int32 CompareTo(TypeReference? other, TypeComparer? comparer);

        internal Int32 CompareTo(CecilReference? other)
        {
            return CompareTo(other, null);
        }

        internal abstract Int32 CompareTo(CecilReference? other, TypeComparer? comparer);

        public Int32 CompareTo(TypeDefinition? other)
        {
            return CompareTo(other, null);
        }

        public abstract Int32 CompareTo(TypeDefinition? other, TypeComparer? comparer);

        internal Int32 CompareTo(CecilDefinition? other)
        {
            return CompareTo(other, null);
        }

        internal abstract Int32 CompareTo(CecilDefinition? other, TypeComparer? comparer);

        public Int32 CompareTo(MonoCecilType? other)
        {
            return CompareTo(other, null);
        }

        public abstract Int32 CompareTo(MonoCecilType? other, TypeComparer? comparer);

        public sealed override Int32 GetHashCode()
        {
            return Key.GetHashCode();
        }

        public sealed override Boolean Equals(Object? other)
        {
            return other switch
            {
                MonoCecilType value => Equals(value),
                Type value => Equals(value),
                TypeKey value => Equals(value),
                TypeDefinition value => Equals(value),
                CecilDefinition value => Equals(value),
                TypeReference value => Equals(value),
                CecilReference value => Equals(value),
                _ => false
            };
        }

        internal const Int32 Buffer = 1536;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Boolean Equals(TypeKey other)
        {
            return Key.Equals(other);
        }

        Boolean IEquatable<TypeKey>.Equals(TypeKey other)
        {
            return Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Boolean Equals(Type? other)
        {
            return other is not null && Key.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(TypeReference? other)
        {
            return other is not null && Key.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Boolean Equals(CecilReference? other)
        {
            return other is not null && Key.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(TypeDefinition? other)
        {
            return other is not null && Key.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Boolean Equals(CecilDefinition? other)
        {
            return other is not null && Key.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(MonoCecilType? other)
        {
            return other is not null && Key.Equals(other.Key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public sealed override String ToString()
        {
            return Identifier;
        }
    }
}