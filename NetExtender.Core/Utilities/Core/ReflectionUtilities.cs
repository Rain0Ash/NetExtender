// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using NetExtender.Types.Anonymous;
using NetExtender.Exceptions;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Types;
using AssembliesHashAlgorithm = System.Configuration.Assemblies.AssemblyHashAlgorithm;

namespace NetExtender.Utilities.Core
{
    [Flags]
    public enum AssemblyNameType : Byte
    {
        Default = 0,
        Version = 1,
        Culture = 2,
        PublicKeyToken = 4,
        All = Version | Culture | PublicKeyToken
    }

    public enum MethodVisibilityType : Byte
    {
        Unavailable,
        Private,
        Public
    }

    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    [SuppressMessage("ReSharper", "PossibleIntendedRethrow")]
    public static partial class ReflectionUtilities
    {
        private static class AnonymousTypeStorage
        {
            private static ConcurrentDictionary<Type, AnonymousTypeProperties> Storage { get; } = new ConcurrentDictionary<Type, AnonymousTypeProperties>();

            public static AnonymousTypeProperties? Get(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                if (!type.IsAnonymousType())
                {
                    throw new NotSupportedException();
                }

                try
                {
                    return Storage.GetOrAdd(type, AnonymousTypeProperties.Create);
                }
                catch (Exception)
                {
                    return default;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AnonymousTypeProperties? GetAnonymousProperties(this Object? value)
        {
            return value is not null ? GetAnonymousProperties(value.GetType()) : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AnonymousTypeProperties? GetAnonymousProperties(this Type type)
        {
            return AnonymousTypeStorage.Get(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined<T>(this MemberInfo member) where T : Attribute
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            return member.IsDefined(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefined<T>(this MemberInfo member, Boolean inherit) where T : Attribute
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            return member.IsDefined(typeof(T), inherit);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAbstract(this MemberInfo member)
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            return Initializer.Initializer.ReflectionUtilities.IsAbstract(member);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAbstract(this PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return Initializer.Initializer.ReflectionUtilities.IsAbstract(property);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAbstract(this EventInfo @event)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            return Initializer.Initializer.ReflectionUtilities.IsAbstract(@event);
        }

        public static List<Object> FilterOnBrowsableAttribute<T>(T source) where T : IEnumerable
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            List<Object> result = new List<Object>();

            foreach (Object? @object in source)
            {
                if (@object is null)
                {
                    continue;
                }

                const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField;
                if (@object.ToString() is not { Length: > 0 } name || @object.GetType().GetField(name, binding) is not { } field)
                {
                    result.Add(@object);
                    continue;
                }

                if (field.GetCustomAttributes<BrowsableAttribute>(true).FirstOrDefault() is not { } browsable || browsable.Browsable)
                {
                    result.Add(@object);
                }
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetFirstAttributeOrDefault<T>(this PropertyDescriptor descriptor) where T : Attribute
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            return descriptor.Attributes.OfType<T>().FirstOrDefault();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? GetAttributeValue<T, TResult>(this PropertyDescriptor descriptor, Func<T, TResult> selector) where T : Attribute
        {
            return GetAttributeValue(descriptor, selector, default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult? GetAttributeValue<T, TResult>(this PropertyDescriptor descriptor, Func<T, TResult> selector, TResult? @default) where T : Attribute
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return descriptor.Attributes.OfType<T>().FirstOrDefault() is { } result ? selector(result) : @default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Attribute? GetFirstAttributeOrDefault(this PropertyDescriptor descriptor, Type attribute)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            return descriptor.Attributes.Cast<Attribute>().FirstOrDefault(item => item.GetType().IsAssignableFrom(attribute));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsReadOnly(this PropertyDescriptor descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            return descriptor.GetFirstAttributeOrDefault<ReadOnlyAttribute>() is { } attribute ? attribute.IsReadOnly : descriptor.IsReadOnly;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String AttributeCategory(this PropertyDescriptor dependency)
        {
            if (dependency is null)
            {
                throw new ArgumentNullException(nameof(dependency));
            }

            return dependency.GetFirstAttributeOrDefault<CategoryAttribute>() is { } attribute ? attribute.Category : dependency.Category;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String AttributeDescription(this PropertyDescriptor dependency)
        {
            if (dependency is null)
            {
                throw new ArgumentNullException(nameof(dependency));
            }

            return dependency.GetFirstAttributeOrDefault<DescriptionAttribute>() is { } attribute ? attribute.Description : dependency.Description;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String AttributeDisplayName(this PropertyDescriptor dependency)
        {
            if (dependency is null)
            {
                throw new ArgumentNullException(nameof(dependency));
            }

            return dependency.GetFirstAttributeOrDefault<DisplayNameAttribute>() is { } attribute ? attribute.DisplayName : dependency.DisplayName;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSafe(this MethodBase method)
        {
            return IsSafe(method, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSafe(this MethodBase method, [MaybeNullWhen(false)] out ParameterInfo[] parameters)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            parameters = method.GetSafeParameters();
            return parameters is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type GetEntryPointType()
        {
            return TryGetEntryPointType(out Type? type) ? type : throw new EntryPointNotFoundException("Entry point type not found.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type GetEntryPointType(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return TryGetEntryPointType(assembly, out Type? type) ? type : throw new EntryPointNotFoundException("Entry point type not found.");
        }

        public static Boolean TryGetEntryPointType([MaybeNullWhen(false)] out Type result)
        {
            Assembly? assembly = GetEntryAssembly();

            if (assembly is not null)
            {
                return TryGetEntryPointType(assembly, out result);
            }

            result = default;
            return false;
        }

        public static Boolean TryGetEntryPointType(this Assembly assembly, [MaybeNullWhen(false)] out Type result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            Type? type = assembly.EntryPoint?.DeclaringType;

            if (type is null)
            {
                result = default;
                return false;
            }

            result = type;
            return true;
        }

        public static FileInfo GetAssemblyFile(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return new FileInfo(assembly.Location);
        }

        [Obsolete]
        public static FileInfo GetAssemblyFileFromCodeBase(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            String? code = assembly.EscapedCodeBase;
            if (code is null)
            {
                throw new InvalidOperationException();
            }

            Uri uri = new Uri(code);

            if (!uri.IsFile)
            {
                throw new InvalidOperationException();
            }

            return new FileInfo(uri.LocalPath);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Assembly? GetEntryAssembly()
        {
            return GetEntryAssembly(true);
        }

        [SuppressMessage("Usage", "CA2200")]
        public static Assembly? GetEntryAssembly(Boolean search)
        {
            try
            {
                return Initializer.Initializer.ReflectionUtilities.GetEntryAssembly(search);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Assembly LoadAssembly(this AssemblyName assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return Assembly.Load(assembly);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Assembly LoadAssembly(String assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return Assembly.Load(assembly);
        }

        public static Boolean TryLoadAssembly(this AssemblyName assembly, [MaybeNullWhen(false)] out Assembly result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssembly(assembly);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryLoadAssembly(String assembly, [MaybeNullWhen(false)] out Assembly result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssembly(assembly);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Assembly LoadAssemblyFile(String assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return Assembly.LoadFile(assembly);
        }

        public static Boolean TryLoadAssemblyFile(String assembly, [MaybeNullWhen(false)] out Assembly result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyFile(assembly);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Assembly LoadAssemblyFrom(String assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return Assembly.LoadFrom(assembly);
        }

#if NET9_0_OR_GREATER
        [Obsolete("LoadFrom with a custom AssemblyHashAlgorithm is obsolete. Use overloads without an AssemblyHashAlgorithm.", DiagnosticId = "SYSLIB0056", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
#endif
        public static Assembly LoadAssemblyFrom(String assembly, Byte[]? hash, AssemblyHashAlgorithm algorithm)
        {
            return LoadAssemblyFrom(assembly, hash, algorithm switch
            {
                AssemblyHashAlgorithm.None => AssembliesHashAlgorithm.None,
                AssemblyHashAlgorithm.MD5 => AssembliesHashAlgorithm.MD5,
                AssemblyHashAlgorithm.Sha1 => AssembliesHashAlgorithm.SHA1,
                AssemblyHashAlgorithm.Sha256 => AssembliesHashAlgorithm.SHA256,
                AssemblyHashAlgorithm.Sha384 => AssembliesHashAlgorithm.SHA384,
                AssemblyHashAlgorithm.Sha512 => AssembliesHashAlgorithm.SHA512,
                _ => throw new EnumUndefinedOrNotSupportedException<AssemblyHashAlgorithm>(algorithm, nameof(algorithm), null)
            });
        }

#if NET9_0_OR_GREATER
        [Obsolete("LoadFrom with a custom AssemblyHashAlgorithm is obsolete. Use overloads without an AssemblyHashAlgorithm.", DiagnosticId = "SYSLIB0056", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Assembly LoadAssemblyFrom(String assembly, Byte[]? hash, AssembliesHashAlgorithm algorithm)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return Assembly.LoadFrom(assembly, hash, algorithm);
        }

        public static Assembly LoadAssemblyFrom(String assembly, Byte[]? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (token is null)
            {
                return LoadAssemblyFrom(assembly);
            }

            AssemblyName name = AssemblyName.GetAssemblyName(assembly);

            if (token.SequenceEqual(name.GetPublicKeyToken() ?? throw new CryptographicException()) ||
                token.SequenceEqual(name.GetPublicKey() ?? throw new CryptographicException()))
            {
                return LoadAssemblyFrom(assembly);
            }

            throw new CryptographicException();
        }

        public static Assembly LoadAssemblyWithPublicKeyFrom(String assembly, Byte[]? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (token is null)
            {
                return LoadAssemblyFrom(assembly);
            }

            AssemblyName name = AssemblyName.GetAssemblyName(assembly);
            return token.SequenceEqual(name.GetPublicKey() ?? throw new CryptographicException()) ? LoadAssemblyFrom(assembly) : throw new CryptographicException();
        }

        public static Assembly LoadAssemblyWithPublicKeyTokenFrom(String assembly, Byte[]? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (token is null)
            {
                return LoadAssemblyFrom(assembly);
            }

            AssemblyName name = AssemblyName.GetAssemblyName(assembly);
            return token.SequenceEqual(name.GetPublicKeyToken() ?? throw new CryptographicException()) ? LoadAssemblyFrom(assembly) : throw new CryptographicException();
        }

        public static Assembly LoadAssemblyFrom(String assembly, String? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (String.IsNullOrEmpty(token))
            {
                return LoadAssemblyFrom(assembly);
            }

            AssemblyName name = AssemblyName.GetAssemblyName(assembly);

            String hextoken = name.GetPublicKeyToken()?.ToHexString() ?? throw new CryptographicException();
            if (String.Equals(hextoken, token, StringComparison.OrdinalIgnoreCase))
            {
                return LoadAssemblyFrom(assembly);
            }

            hextoken = name.GetPublicKey()?.ToHexString() ?? throw new CryptographicException();
            if (String.Equals(hextoken, token, StringComparison.OrdinalIgnoreCase))
            {
                return LoadAssemblyFrom(assembly);
            }

            throw new CryptographicException();
        }

        public static Assembly LoadAssemblyWithPublicKeyFrom(String assembly, String? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (String.IsNullOrEmpty(token))
            {
                return LoadAssemblyFrom(assembly);
            }

            String hextoken = AssemblyName.GetAssemblyName(assembly).GetPublicKey()?.ToHexString() ?? throw new CryptographicException();
            return String.Equals(hextoken, token, StringComparison.OrdinalIgnoreCase) ? LoadAssemblyFrom(assembly) : throw new CryptographicException();
        }

        public static Assembly LoadAssemblyWithPublicKeyTokenFrom(String assembly, String? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (String.IsNullOrEmpty(token))
            {
                return LoadAssemblyFrom(assembly);
            }

            String hextoken = AssemblyName.GetAssemblyName(assembly).GetPublicKeyToken()?.ToHexString() ?? throw new CryptographicException();
            return String.Equals(hextoken, token, StringComparison.OrdinalIgnoreCase) ? LoadAssemblyFrom(assembly) : throw new CryptographicException();
        }

        public static Boolean TryLoadAssemblyFrom(String assembly, [MaybeNullWhen(false)] out Assembly result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyFrom(assembly);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

#if NET9_0_OR_GREATER
        [Obsolete("LoadFrom with a custom AssemblyHashAlgorithm is obsolete. Use overloads without an AssemblyHashAlgorithm.", DiagnosticId = "SYSLIB0056", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
#endif
        public static Boolean TryLoadAssemblyFrom(String assembly, Byte[]? hash, AssemblyHashAlgorithm algorithm, [MaybeNullWhen(false)] out Assembly result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyFrom(assembly, hash, algorithm);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

#if NET9_0_OR_GREATER
        [Obsolete("LoadFrom with a custom AssemblyHashAlgorithm is obsolete. Use overloads without an AssemblyHashAlgorithm.", DiagnosticId = "SYSLIB0056", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
#endif
        public static Boolean TryLoadAssemblyFrom(String assembly, Byte[]? hash, AssembliesHashAlgorithm algorithm, [MaybeNullWhen(false)] out Assembly result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyFrom(assembly, hash, algorithm);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryLoadAssemblyFrom(String assembly, [MaybeNullWhen(false)] out Assembly result, Byte[]? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyFrom(assembly, token);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryLoadAssemblyWithPublicKeyFrom(String assembly, [MaybeNullWhen(false)] out Assembly result, Byte[]? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyWithPublicKeyFrom(assembly, token);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryLoadAssemblyWithPublicKeyTokenFrom(String assembly, [MaybeNullWhen(false)] out Assembly result, Byte[]? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyWithPublicKeyTokenFrom(assembly, token);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryLoadAssemblyFrom(String assembly, [MaybeNullWhen(false)] out Assembly result, String? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyFrom(assembly, token);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryLoadAssemblyWithPublicKeyFrom(String assembly, [MaybeNullWhen(false)] out Assembly result, String? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyWithPublicKeyFrom(assembly, token);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryLoadAssemblyWithPublicKeyTokenFrom(String assembly, [MaybeNullWhen(false)] out Assembly result, String? token)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                result = LoadAssemblyWithPublicKeyTokenFrom(assembly, token);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static String? GetManifestResourceString(this Assembly assembly, String resource)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (String.IsNullOrEmpty(resource))
            {
                throw new ArgumentNullOrEmptyStringException(resource, nameof(resource));
            }

            try
            {
                using Stream? stream = assembly.GetManifestResourceStream(resource);

                if (stream is null)
                {
                    return null;
                }

                using StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetEntryTypeNamespace()
        {
            return GetEntryTypeNamespace(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetEntryTypeNamespace(Boolean root)
        {
            return GetEntryPointType().Namespace is { } @namespace ? root ? @namespace.Split('.')[0] : @namespace : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetEntryTypeNamespace(this Assembly assembly)
        {
            return GetEntryTypeNamespace(assembly, false);
        }

        public static String GetEntryTypeNamespace(this Assembly assembly, Boolean root)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            String? @namespace = GetEntryPointType(assembly).Namespace;

            if (@namespace is null)
            {
                throw new InvalidOperationException();
            }

            return root ? @namespace.Split('.')[0] : @namespace;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetEntryAssemblyNamespace()
        {
            return GetEntryAssemblyNamespace(out _, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetEntryAssemblyNamespace(out Type type)
        {
            return GetEntryAssemblyNamespace(out _, out type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetEntryAssemblyNamespace(out Assembly assembly)
        {
            return GetEntryAssemblyNamespace(out assembly, out _);
        }

        public static String GetEntryAssemblyNamespace(out Assembly assembly, out Type type)
        {
            if (!TryGetEntryPointType(out Type? entry))
            {
                throw new EntryPointNotFoundException("Entry point type not found.");
            }

            type = entry;
            assembly = type.Assembly;
            if (!assembly.TryGetEntryTypeNamespace(out String? @namespace))
            {
                throw new EntryPointNotFoundException($"Entry point type namespace not found at '{assembly.FullName}'.");
            }

            return @namespace;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetEntryTypeNamespace([MaybeNullWhen(false)] out String result)
        {
            return TryGetEntryTypeNamespace(false, out result);
        }

        public static Boolean TryGetEntryTypeNamespace(Boolean root, [MaybeNullWhen(false)] out String result)
        {
            if (!TryGetEntryPointType(out Type? type))
            {
                result = null;
                return false;
            }

            result = root ? type.Namespace?.Split('.')[0] : type.Namespace;
            return result is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetEntryTypeNamespace(this Assembly assembly, [MaybeNullWhen(false)] out String result)
        {
            return TryGetEntryTypeNamespace(assembly, false, out result);
        }

        public static Boolean TryGetEntryTypeNamespace(this Assembly assembly, Boolean root, [MaybeNullWhen(false)] out String result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (!TryGetEntryPointType(assembly, out Type? type))
            {
                result = null;
                return false;
            }

            result = root ? type.Namespace?.Split('.')[0] : type.Namespace;
            return result is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetTypesWithoutNamespace(this IEnumerable<Type> source, String name)
        {
            return GetTypesWithoutNamespace(source, name, false);
        }

        public static Type[] GetTypesWithoutNamespace(this IEnumerable<Type> source, String name, Boolean insensitive)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            StringComparison comparison = insensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            return source.Where(type => String.Equals(type.Name, name, comparison)).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetTypesWithoutNamespace(String name)
        {
            return GetTypesWithoutNamespace(name, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetTypesWithoutNamespace(String name, Boolean insensitive)
        {
            return GetTypesWithoutNamespace(GetEntryAssembly() ?? throw new InvalidOperationException(), name, insensitive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetTypesWithoutNamespace(this Assembly assembly, String name)
        {
            return GetTypesWithoutNamespace(assembly, name, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetTypesWithoutNamespace(this Assembly assembly, String name, Boolean insensitive)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return GetTypesWithoutNamespace(assembly.GetSafeTypesUnsafe(), name, insensitive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type? GetTypeWithoutNamespace(String name)
        {
            return GetTypeWithoutNamespace(name, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type? GetTypeWithoutNamespace(String name, Boolean insensitive)
        {
            return GetTypeWithoutNamespace(GetEntryAssembly() ?? throw new InvalidOperationException(), name, insensitive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type? GetTypeWithoutNamespace(this Assembly assembly, String name)
        {
            return GetTypeWithoutNamespace(assembly, name, false);
        }

        public static Type? GetTypeWithoutNamespace(this Assembly assembly, String name, Boolean insensitive)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Type[] type = GetTypesWithoutNamespace(assembly, name, insensitive);

            return type.Length switch
            {
                0 => null,
                1 => type[0],
                _ => throw new AmbiguousMatchException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetTypeWithoutNamespace(String name, [MaybeNullWhen(false)] out Type result)
        {
            return TryGetTypeWithoutNamespace(name, false, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetTypeWithoutNamespace(String name, Boolean insensitive, [MaybeNullWhen(false)] out Type result)
        {
            return TryGetTypeWithoutNamespace(GetEntryAssembly() ?? throw new InvalidOperationException(), name, insensitive, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetTypeWithoutNamespace(this Assembly assembly, String name, [MaybeNullWhen(false)] out Type result)
        {
            return TryGetTypeWithoutNamespace(assembly, name, false, out result);
        }

        public static Boolean TryGetTypeWithoutNamespace(this Assembly assembly, String name, Boolean insensitive, [MaybeNullWhen(false)] out Type result)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Type[] type = GetTypesWithoutNamespace(assembly, name, insensitive);

            if (type.Length == 1)
            {
                result = type[0];
                return true;
            }

            result = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypesInNamespace(this IEnumerable<Type> source, String? @namespace)
        {
            return GetTypesInNamespace(source, @namespace, false);
        }

        public static IEnumerable<Type> GetTypesInNamespace(this IEnumerable<Type> source, String? @namespace, Boolean insensitive)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            StringComparison comparison = insensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            return source.Where(type => String.Equals(type.Namespace, @namespace, comparison)).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypesInNamespace(String @namespace)
        {
            return GetTypesInNamespace(@namespace, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypesInNamespace(String @namespace, Boolean insensitive)
        {
            return GetTypesInNamespace(GetEntryAssembly() ?? throw new InvalidOperationException(), @namespace, insensitive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypesInNamespace(this Assembly assembly, String? @namespace)
        {
            return GetTypesInNamespace(assembly, @namespace, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypesInNamespace(this Assembly assembly, String? @namespace, Boolean insensitive)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return GetTypesInNamespace(assembly.GetSafeTypesUnsafe(), @namespace, insensitive);
        }

        public static Boolean IsJITOptimized(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            foreach (Object attribute in assembly.GetCustomAttributes(typeof(DebuggableAttribute), false))
            {
                if (attribute is DebuggableAttribute debuggable)
                {
                    return !debuggable.IsJITOptimizerDisabled;
                }
            }

            return true;
        }

        private static Boolean IsSystemAssembly(String? assembly)
        {
            if (assembly is null)
            {
                return false;
            }

            return assembly.StartsWith("System.") || assembly.StartsWith("Microsoft.") || assembly.StartsWith("netstandard");
        }

        public static Boolean IsSystemAssembly(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return IsSystemAssembly(assembly.FullName);
        }

        public static Boolean IsSystemAssembly(this AssemblyName assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return IsSystemAssembly(assembly.FullName);
        }

        /// <inheritdoc cref="GetStackInfo(Int32)"/>
        public static String GetStackInfo()
        {
            return GetStackInfo(2);
        }

        /// <summary>
        /// Get a String containing stack information from zero or more levels up the call stack.
        /// </summary>
        /// <param name="levels">
        /// A <see cref="System.Int32"/> which indicates how many levels up the stack
        /// the information should be retrieved from.  This value must be zero or greater.
        /// </param>
        /// <returns>
        /// A String in this format:
        /// <para>
        /// file name::namespace.[one or more type names].method name
        /// </para>
        /// </returns>
        public static String GetStackInfo(Int32 levels)
        {
            if (levels < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(levels), levels, null);
            }

            StackFrame frame = new StackFrame(levels, true);
            MethodBase method = frame.GetMethod() ?? throw new InvalidOperationException();

            return $"{Path.GetFileName(frame.GetFileName())}::{method.DeclaringType?.FullName ?? String.Empty}.{method.Name} - Line {frame.GetFileLineNumber()}";
        }

        /// <summary>
        /// Gets the method that called the current method where this method is used. This does not work when used in async methods.
        /// <para>
        /// Note that because of compiler optimization, you should add <see cref="MethodImplAttribute"/> to the method where this method is used and use the <see cref="MethodImplOptions.NoInlining"/> value.
        /// </para>
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static MethodBase? GetCallingMethod()
        {
            return new StackFrame(2, false).GetMethod();
        }

        /// <summary>
        /// Gets the type that called the current method where this method is used.
        /// <para>
        /// Note that because of compiler optimization, you should add <see cref="MethodImplAttribute"/> to the method where this method is used and use the <see cref="MethodImplOptions.NoInlining"/> value.
        /// </para>
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Type? GetCallingType()
        {
            return new StackFrame(2, false).GetMethod()?.DeclaringType;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo? GetProperty(this Type type, String name, BindingFlags binding, Type? returnType)
        {
            return type is not null ? type.GetProperty(name, binding, null, returnType, Type.EmptyTypes, null) : throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        /// Returns all the public properties of this object whose property type is <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the properties.</typeparam>
        /// <param name="object">The object.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetAllPropertiesOfType<T>(this Object @object)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            return GetAllPropertiesOfType<T>(@object.GetType());
        }

        /// <summary>
        /// Returns all the public properties of this type whose property type is <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the properties.</typeparam>
        /// <param name="type">The type of which to get the properties.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetAllPropertiesOfType<T>(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.GetProperties().Where(static info => info.PropertyType == typeof(T));
        }

        /// <summary>
        /// Gets the type of the specified property in the type.
        /// <para>
        /// If the type is nullable, this function gets its generic definition."/>.
        /// </para>
        /// </summary>
        /// <param name="type">The type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        public static Type GetPropertyType(this Type type, String name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Type property = GetPropertyTypeReal(type, name);

            // get the generic type of nullable, not THE nullable
            if (property.IsGenericType && property.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                property = FirstGenericArgument(property);
            }

            return property;
        }

        /// <summary>
        /// Gets the type of the specified property in the type.
        /// <para>
        /// If the type is nullable, this function gets its generic definition."/>.
        /// </para>
        /// </summary>
        /// <param name="type">The type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static Type GetPropertyType(this Type type, String name, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Type property = GetPropertyTypeReal(type, name, binding);

            // get the generic type of nullable, not THE nullable
            if (property.IsGenericType && property.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                property = FirstGenericArgument(property);
            }

            return property;
        }

        /// <summary>
        /// Gets the type of the specified property in the type.
        /// </summary>
        /// <param name="type">The type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type GetPropertyTypeReal(this Type type, String name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            PropertyInfo property = GetPropertyInfo(type, name);
            return property.PropertyType;
        }

        /// <summary>
        /// Gets the type of the specified property in the type.
        /// </summary>
        /// <param name="type">The type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type GetPropertyTypeReal(this Type type, String name, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            PropertyInfo property = GetPropertyInfo(type, name, binding);
            return property.PropertyType;
        }

        /// <summary>
        /// Gets the property information by name for the type of the object.
        /// </summary>
        /// <param name="object">Object with a type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo GetPropertyInfo(this Object @object, String name)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return GetPropertyInfo(@object.GetType(), name);
        }

        /// <summary>
        /// Gets the property information by name for the type of the object.
        /// </summary>
        /// <param name="object">Object with a type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo GetPropertyInfo(this Object @object, String name, BindingFlags binding)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return GetPropertyInfo(@object.GetType(), name, binding);
        }

        /// <summary>
        /// Gets the property information by name for the type.
        /// </summary>
        /// <param name="type">Type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        public static PropertyInfo GetPropertyInfo(this Type type, String name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            PropertyInfo? property = type.GetProperty(name);

            if (property is null)
            {
                throw new Exception($"The provided property name ({name}) does not exist in type '{type}'.");
            }

            return property;
        }

        /// <summary>
        /// Gets the property information by name for the type.
        /// </summary>
        /// <param name="type">Type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static PropertyInfo GetPropertyInfo(this Type type, String name, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            PropertyInfo? property = type.GetProperty(name, binding);

            if (property is null)
            {
                throw new Exception($"The provided property name ({name}) does not exist in type '{type}'.");
            }

            return property;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsIndexer(this PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return property.GetIndexParameters().Length > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsOverridden(this MethodInfo method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.GetBaseDefinition().DeclaringType != method.DeclaringType;
        }

        public static FieldAttributes Access(this FieldAttributes attributes)
        {
            return attributes & FieldAttributes.FieldAccessMask;
        }

        public static MethodAttributes Access(this MethodAttributes attributes)
        {
            return attributes & MethodAttributes.MemberAccessMask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodVisibilityType IsRead(this PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (!property.CanRead)
            {
                return MethodVisibilityType.Unavailable;
            }

            return property.GetMethod?.IsPublic switch
            {
                null => MethodVisibilityType.Unavailable,
                false => MethodVisibilityType.Private,
                true => MethodVisibilityType.Public
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodVisibilityType IsWrite(this PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (!property.CanWrite)
            {
                return MethodVisibilityType.Unavailable;
            }

            return property.SetMethod?.IsPublic switch
            {
                null => MethodVisibilityType.Unavailable,
                false => MethodVisibilityType.Private,
                true => MethodVisibilityType.Public
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean? ToBoolean(this MethodVisibilityType type)
        {
            return type switch
            {
                MethodVisibilityType.Unavailable => null,
                MethodVisibilityType.Private => false,
                MethodVisibilityType.Public => true,
                _ => throw new EnumUndefinedOrNotSupportedException<MethodVisibilityType>(type, nameof(type), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryCreateDelegate(this MethodInfo? info, [MaybeNullWhen(false)] out Delegate result)
        {
            return TryCreateDelegate<Delegate>(info, out result);
        }

        public static Boolean TryCreateDelegate<T>(this MethodInfo? info, [MaybeNullWhen(false)] out T result) where T : Delegate
        {
            if (info is null)
            {
                result = default;
                return false;
            }

            try
            {
                result = info.CreateDelegate<T>();
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        /// <summary>
        /// Gets the field information by name for the type of the object.
        /// </summary>
        /// <param name="object">Object with a type that has the specified field.</param>
        /// <param name="name">The name of the field.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FieldInfo GetFieldInfo(this Object @object, String name)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return GetFieldInfo(@object.GetType(), name);
        }

        /// <summary>
        /// Gets the field information by name for the type.
        /// </summary>
        /// <param name="type">Type that has the specified field.</param>
        /// <param name="name">The name of the field.</param>
        public static FieldInfo GetFieldInfo(this Type type, String name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            FieldInfo? field = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (field is null)
            {
                throw new Exception($"The provided property name ({name}) does not exist in type '{type}'.");
            }

            return field;
        }

        /// <summary>
        /// Returns the first definition of generic type of this generic type.
        /// </summary>
        /// <param name="type">The type from which to get the generic type.</param>
        public static Type FirstGenericArgument(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Type[] arguments = type.GetGenericArguments();

            if (arguments.Length <= 0)
            {
                throw new ArgumentException($"The provided type ({type}) is not a generic type.", nameof(type));
            }

            return arguments[0];
        }

        /// <summary>
        /// Gets the constants defined in this type.
        /// </summary>
        /// <param name="type">The type from which to get the constants.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> GetConstants(this Type type)
        {
            return GetConstants(type, true);
        }

        /// <summary>
        /// Gets the constants defined in this type.
        /// </summary>
        /// <param name="type">The type from which to get the constants.</param>
        /// <param name="inherited">Determines whether or not to include inherited constants.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> GetConstants(this Type type, Boolean inherited)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            BindingFlags binding = BindingFlags.Public | BindingFlags.Static | (inherited ? BindingFlags.FlattenHierarchy : BindingFlags.DeclaredOnly);
            return type.GetFields(binding).Where(static field => field is { IsLiteral: true, IsInitOnly: false });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> GetAccessibleFields(this Type type)
        {
            return GetAccessibleMembers(type, RuntimeReflectionExtensions.GetRuntimeFields);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetAccessibleProperties(this Type type)
        {
            return GetAccessibleMembers(type, RuntimeReflectionExtensions.GetRuntimeProperties);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<EventInfo> GetAccessibleEvents(this Type type)
        {
            return GetAccessibleMembers(type, RuntimeReflectionExtensions.GetRuntimeEvents);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> GetAccessibleMethods(this Type type)
        {
            return GetAccessibleMembers(type, RuntimeReflectionExtensions.GetRuntimeMethods);
        }

        private static IEnumerable<T> GetAccessibleMembers<T>(this Type type, Func<Type, IEnumerable<T>> selector) where T : MemberInfo
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            List<T> result = new List<T>(selector(type));

            if (!type.IsInterface)
            {
                return result.ToArray();
            }

            foreach (Type @interface in type.GetSafeInterfacesUnsafe())
            {
                if (@interface.IsVisible)
                {
                    result.AddRange(selector.Invoke(@interface));
                }
            }

            result.AddRange(selector.Invoke(typeof(Object)));
            return result.ToArray();
        }

        // ReSharper disable once CognitiveComplexity
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean HasSignature(this Type type, TypeSignature signature)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return signature is TypeSignature.Any ||
                   !(
                       signature.HasFlag(TypeSignature.Public) && !type.IsPublic ||
                       signature.HasFlag(TypeSignature.NotPublic) && !type.IsNotPublic ||
                       signature.HasFlag(TypeSignature.AutoLayout) && !type.IsAutoLayout ||
                       signature.HasFlag(TypeSignature.SequentialLayout) && !type.IsLayoutSequential ||
                       signature.HasFlag(TypeSignature.ExplicitLayout) && !type.IsExplicitLayout ||
                       signature.HasFlag(TypeSignature.Array) && !type.IsArray ||
                       signature.HasFlag(TypeSignature.SZArray) && !type.IsSZArray ||
                       signature.HasFlag(TypeSignature.VariableBoundArray) && !type.IsVariableBoundArray ||
                       signature.HasFlag(TypeSignature.ElementType) && !type.HasElementType ||
                       signature.HasFlag(TypeSignature.SignatureType) && !type.IsSignatureType ||
                       signature.HasFlag(TypeSignature.ByRef) && !type.IsByRef ||
                       signature.HasFlag(TypeSignature.MarshalByRef) && !type.IsMarshalByRef ||
                       signature.HasFlag(TypeSignature.ByRefLike) && !type.IsByRefLike ||
                       signature.HasFlag(TypeSignature.Pointer) && !type.IsPointer ||
                       signature.HasFlag(TypeSignature.Value) && !type.IsValueType ||
                       signature.HasFlag(TypeSignature.Primitive) && !type.IsPrimitive ||
                       signature.HasFlag(TypeSignature.Enum) && !type.IsEnum ||
                       signature.HasFlag(TypeSignature.Class) && !type.IsClass ||
                       signature.HasFlag(TypeSignature.Interface) && !type.IsInterface ||
                       signature.HasFlag(TypeSignature.Abstract) && !type.IsAbstract ||
                       signature.HasFlag(TypeSignature.Sealed) && !type.IsSealed ||
                       signature.HasFlag(TypeSignature.Nested) && !type.IsNested ||
                       signature.HasFlag(TypeSignature.GenericType) && !type.IsGenericType ||
                       signature.HasFlag(TypeSignature.GenericTypeDefinition) && !type.IsGenericTypeDefinition ||
                       signature.HasFlag(TypeSignature.ConstructedGenericType) && !type.IsConstructedGenericType ||
                       signature.HasFlag(TypeSignature.GenericParameter) && !type.IsGenericParameter ||
                       signature.HasFlag(TypeSignature.GenericTypeParameter) && !type.IsGenericTypeParameter ||
                       signature.HasFlag(TypeSignature.GenericMethodParameter) && !type.IsGenericMethodParameter ||
                       signature.HasFlag(TypeSignature.SpecialName) && !type.IsSpecialName ||
                       signature.HasFlag(TypeSignature.RTSpecialName) && !type.Attributes.HasFlag(TypeAttributes.RTSpecialName) ||
                       signature.HasFlag(TypeSignature.Import) && !type.IsImport ||
#pragma warning disable SYSLIB0050
                       signature.HasFlag(TypeSignature.Serializable) && !type.IsSerializable ||
#pragma warning restore SYSLIB0050
                       signature.HasFlag(TypeSignature.Contextful) && !type.IsContextful ||
                       signature.HasFlag(TypeSignature.AnsiClass) && !type.IsAnsiClass ||
                       signature.HasFlag(TypeSignature.UnicodeClass) && !type.IsUnicodeClass ||
                       signature.HasFlag(TypeSignature.AutoClass) && !type.IsAutoClass ||
                       signature.HasFlag(TypeSignature.COMObject) && !type.IsCOMObject ||
                       signature.HasFlag(TypeSignature.IsSecurityCritical) && !type.IsSecurityCritical ||
                       signature.HasFlag(TypeSignature.IsSecuritySafeCritical) && !type.IsSecuritySafeCritical ||
                       signature.HasFlag(TypeSignature.IsSecurityTransparent) && !type.IsSecurityTransparent
                   );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasSignature(this MethodBase method)
        {
            return HasSignature(method, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasSignature(this MethodBase method, params ReflectionParameterSignature[]? signature)
        {
            return HasSignature(method, default, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasSignature(this MethodBase method, ReflectionParameterSignature @return)
        {
            return HasSignature(method, @return, ReflectionParameterSignature.Any);
        }

        // ReSharper disable once CognitiveComplexity
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean HasSignature(this MethodBase method, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            MethodInfo? info = method as MethodInfo;
            if (signature is null)
            {
                return info is null || @return.Equals(info.ReturnParameter);
            }

            if (method.GetSafeParameters() is not { } parameters || info is not null && !@return.Equals(info.ReturnParameter))
            {
                return false;
            }

            if (ReferenceEquals(signature, ReflectionParameterSignature.NotEmpty))
            {
                return parameters.Length > 0;
            }

            if (signature.Length != parameters.Length)
            {
                return false;
            }

            // ReSharper disable once LoopCanBeConvertedToQuery
            for (Int32 i = 0; i < signature.Length; i++)
            {
                if (signature[i].IsEmpty)
                {
                    continue;
                }

                if (!signature[i].Equals(parameters[i]))
                {
                    return false;
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> WithSignature<T>(this IEnumerable<T> source) where T : MethodBase
        {
            return WithSignature(source, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> WithSignature<T>(this IEnumerable<T> source, params ReflectionParameterSignature[]? signature) where T : MethodBase
        {
            return WithSignature(source, default, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<T> WithSignature<T>(this IEnumerable<T> source, ReflectionParameterSignature @return) where T : MethodBase
        {
            return WithSignature(source, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<T> WithSignature<T>(this IEnumerable<T> source, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature) where T : MethodBase
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T method in source)
            {
                if (HasSignature(method, @return, signature))
                {
                    yield return method;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T[] WithSignature<T>(T[] methods, params ReflectionParameterSignature[]? signature) where T : MethodBase
        {
            return WithSignature(methods, default, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static T[] WithSignature<T>(T[] methods, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature) where T : MethodBase
        {
            if (methods is null)
            {
                throw new ArgumentNullException(nameof(methods));
            }

            if (signature is null && @return.IsEmpty)
            {
                return methods;
            }

            List<T> result = new List<T>(methods.Length);

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (T method in methods)
            {
                if (HasSignature(method, @return, signature))
                {
                    result.Add(method);
                }
            }

            return result.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasMethods(this Type type)
        {
            return HasMethods(type, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasMethods(this Type type, params ReflectionParameterSignature[]? signature)
        {
            return HasMethods(type, default(ReflectionParameterSignature), signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasMethods(this Type type, ReflectionParameterSignature @return)
        {
            return HasMethods(type, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasMethods(this Type type, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            return GetMethods(type, @return, signature).Length > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasMethods(this Type type, BindingFlags binding)
        {
            return HasMethods(type, binding, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasMethods(this Type type, BindingFlags binding, params ReflectionParameterSignature[]? signature)
        {
            return HasMethods(type, binding, default, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasMethods(this Type type, BindingFlags binding, ReflectionParameterSignature @return)
        {
            return HasMethods(type, binding, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasMethods(this Type type, BindingFlags binding, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            return GetMethods(type, binding, @return, signature).Length > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo[] GetMethods(this Type type)
        {
            return GetMethods(type, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo[] GetMethods(this Type type, params ReflectionParameterSignature[]? signature)
        {
            return GetMethods(type, default(ReflectionParameterSignature), signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo[] GetMethods(this Type type, ReflectionParameterSignature @return)
        {
            return GetMethods(type, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo[] GetMethods(this Type type, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return WithSignature(type.GetMethods(), @return, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo[] GetMethods(this Type type, BindingFlags binding)
        {
            return GetMethods(type, binding, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo[] GetMethods(this Type type, BindingFlags binding, params ReflectionParameterSignature[]? signature)
        {
            return GetMethods(type, binding, default, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo[] GetMethods(this Type type, BindingFlags binding, ReflectionParameterSignature @return)
        {
            return GetMethods(type, binding, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo[] GetMethods(this Type type, BindingFlags binding, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return WithSignature(type.GetMethods(binding), @return, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IEnumerable<KeyValuePair<Type, T[]>> Where<T>(this IEnumerable<KeyValuePair<Type, T[]>> source, Func<T, Boolean> predicate) where T : MethodBase
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            List<T> capacitor = new List<T>(16);
            foreach (KeyValuePair<Type, T[]> pair in source)
            {
                capacitor.AddRange(pair.Value.Where(predicate));

                if (capacitor.Count <= 0)
                {
                    continue;
                }

                KeyValuePair<Type, T[]> result = new KeyValuePair<Type, T[]>(pair.Key, capacitor.ToArray());
                capacitor.Clear();
                yield return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source)
        {
            return WithMethods(source, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, params ReflectionParameterSignature[]? signature)
        {
            return WithMethods(source, default(ReflectionParameterSignature), signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, ReflectionParameterSignature @return)
        {
            return WithMethods(source, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (Type? type in source)
            {
                if (type?.GetMethods(@return, signature) is { Length: > 0 } methods)
                {
                    yield return new KeyValuePair<Type, MethodInfo[]>(type, methods);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, Func<MethodInfo, Boolean>? predicate)
        {
            return WithMethods(source, predicate, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, Func<MethodInfo, Boolean>? predicate, params ReflectionParameterSignature[]? signature)
        {
            return WithMethods(source, predicate, default, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, Func<MethodInfo, Boolean>? predicate, ReflectionParameterSignature @return)
        {
            return WithMethods(source, predicate, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, Func<MethodInfo, Boolean>? predicate, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IEnumerable<KeyValuePair<Type, MethodInfo[]>> methods = WithMethods(source, @return, signature);
            return predicate is not null ? methods.Where(predicate) : methods;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, BindingFlags binding)
        {
            return WithMethods(source, binding, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, BindingFlags binding, params ReflectionParameterSignature[]? signature)
        {
            return WithMethods(source, binding, default(ReflectionParameterSignature), signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, BindingFlags binding, ReflectionParameterSignature @return)
        {
            return WithMethods(source, binding, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, BindingFlags binding, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (Type? type in source)
            {
                if (type?.GetMethods(binding, @return, signature) is { Length: > 0 } methods)
                {
                    yield return new KeyValuePair<Type, MethodInfo[]>(type, methods);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, BindingFlags binding, Func<MethodInfo, Boolean>? predicate)
        {
            return WithMethods(source, binding, predicate, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, BindingFlags binding, Func<MethodInfo, Boolean>? predicate, params ReflectionParameterSignature[]? signature)
        {
            return WithMethods(source, binding, predicate, default, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, BindingFlags binding, Func<MethodInfo, Boolean>? predicate, ReflectionParameterSignature @return)
        {
            return WithMethods(source, binding, predicate, @return, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, MethodInfo[]>> WithMethods(this IEnumerable<Type?> source, BindingFlags binding, Func<MethodInfo, Boolean>? predicate, ReflectionParameterSignature @return, params ReflectionParameterSignature[]? signature)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IEnumerable<KeyValuePair<Type, MethodInfo[]>> methods = WithMethods(source, binding, @return, signature);
            return predicate is not null ? methods.Where(predicate) : methods;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorInfo? GetStaticConstructor(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.GetConstructor(BindingFlags.Static | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasConstructors(this Type type)
        {
            return HasConstructors(type, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasConstructors(this Type type, params ReflectionParameterSignature[]? signature)
        {
            return HasConstructors(type, default, signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasConstructors(this Type type, BindingFlags binding)
        {
            return HasConstructors(type, binding, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasConstructors(this Type type, BindingFlags binding, params ReflectionParameterSignature[]? signature)
        {
            return GetConstructors(type, binding, signature).Length > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorInfo[] GetConstructors(this Type type)
        {
            return GetConstructors(type, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorInfo[] GetConstructors(this Type type, params ReflectionParameterSignature[]? signature)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return WithSignature(type.GetConstructors(), signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> WithConstructors(this IEnumerable<Type?> source)
        {
            return WithConstructors(source, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> WithConstructors(this IEnumerable<Type?> source, params ReflectionParameterSignature[]? signature)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (Type? type in source)
            {
                if (type?.GetConstructors(signature) is { Length: > 0 } constructors)
                {
                    yield return new KeyValuePair<Type, ConstructorInfo[]>(type, constructors);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> WithConstructors(this IEnumerable<Type?> source, Func<ConstructorInfo, Boolean>? predicate)
        {
            return WithConstructors(source, predicate, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> WithConstructors(this IEnumerable<Type?> source, Func<ConstructorInfo, Boolean>? predicate, params ReflectionParameterSignature[]? signature)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> constructors = WithConstructors(source, signature);
            return predicate is not null ? constructors.Where(predicate) : constructors;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorInfo[] GetConstructors(this Type type, BindingFlags binding)
        {
            return GetConstructors(type, binding, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorInfo[] GetConstructors(this Type type, BindingFlags binding, params ReflectionParameterSignature[]? signature)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return WithSignature(type.GetConstructors(binding), signature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> WithConstructors(this IEnumerable<Type?> source, BindingFlags binding)
        {
            return WithConstructors(source, binding, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> WithConstructors(this IEnumerable<Type?> source, BindingFlags binding, params ReflectionParameterSignature[]? signature)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (Type? type in source)
            {
                if (type?.GetConstructors(binding, signature) is { Length: > 0 } constructors)
                {
                    yield return new KeyValuePair<Type, ConstructorInfo[]>(type, constructors);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> WithConstructors(this IEnumerable<Type?> source, BindingFlags binding, Func<ConstructorInfo, Boolean>? predicate)
        {
            return WithConstructors(source, binding, predicate, ReflectionParameterSignature.Any);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> WithConstructors(this IEnumerable<Type?> source, BindingFlags binding, Func<ConstructorInfo, Boolean>? predicate, params ReflectionParameterSignature[]? signature)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IEnumerable<KeyValuePair<Type, ConstructorInfo[]>> constructors = WithConstructors(source, binding, signature);
            return predicate is not null ? constructors.Where(predicate) : constructors;
        }

        public static MethodInfo[] SignatureEqualityMethodAnalyzer(this Type type, Type other)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.CreateInstance;
            MethodInfo[] available = type.GetMethods(binding);
            List<MethodInfo> result = new List<MethodInfo>(available.Length);
            //TODO:
            return result.ToArray();
        }

        /// <summary>
        /// Sets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="object">The objector type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        /// <param name="value">The value to set.</param>
        public static T SetValue<T>(this T @object, String name, Object? value)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (@object is not Type type)
            {
                type = @object.GetType();
            }

            PropertyInfo? property = type.GetProperty(name);
            if (property is not null)
            {
                property.SetValue(@object, value);
                return @object;
            }

            FieldInfo? field = type.GetField(name);

            if (field is null)
            {
                throw new Exception($"'{name}' is neither a property or a field of type '{type}'.");
            }

            field.SetValue(@object, value);
            return @object;
        }

        /// <summary>
        /// Sets the specified field to the provided value in the object.
        /// </summary>
        /// <param name="object">The object with the field.</param>
        /// <param name="name">The name of the field to set.</param>
        /// <param name="value">The value to set the field to.</param>
        public static T SetField<T>(this T @object, String name, Object? value)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            FieldInfo field = GetFieldInfo(@object, name);
            field.SetValue(@object, value);
            return @object;
        }

        /// <summary>
        /// Sets the specified property to the provided value in the object.
        /// </summary>
        /// <param name="object">The object with the property.</param>
        /// <param name="name">The name of the property to set.</param>
        /// <param name="value">The value to set the property to.</param>
        public static T SetProperty<T>(this T @object, String name, Object? value)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            PropertyInfo property = GetPropertyInfo(@object, name);
            property.SetValue(@object, value);
            return @object;
        }

        /// <summary>
        /// Sets the specified property to the provided value in the object.
        /// </summary>
        /// <param name="object">The object with the property.</param>
        /// <param name="name">The name of the property to set.</param>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static T SetProperty<T>(this T @object, String name, Object? value, BindingFlags binding)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            PropertyInfo property = GetPropertyInfo(@object, name, binding);
            property.SetValue(@object, value);
            return @object;
        }

        /// <summary>
        /// Sets the specified property to a value that will be extracted from the provided string value using the <see cref="TypeDescriptor.GetConverter(Type)"/> and <see cref="TypeConverter.ConvertFromString(string)"/>.
        /// </summary>
        /// <param name="object">The object with the property.</param>
        /// <param name="name">The name of the property to set.</param>
        /// <param name="value">The string representation of the value to set to the property.</param>
        public static T SetPropertyFromString<T>(this T @object, String name, String? value)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            PropertyInfo property = GetPropertyInfo(@object, name);
            TypeConverter converter = TypeDescriptor.GetConverter(property.PropertyType);
            Object? result = converter.ConvertFromString(value!);
            property.SetValue(@object, result);
            return @object;
        }

        /// <summary>
        /// Sets the specified property to a value that will be extracted from the provided string value using the <see cref="TypeDescriptor.GetConverter(Type)"/> and <see cref="TypeConverter.ConvertFromString(string)"/>.
        /// </summary>
        /// <param name="object">The object with the property.</param>
        /// <param name="name">The name of the property to set.</param>
        /// <param name="value">The string representation of the value to set to the property.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static T SetPropertyFromString<T>(this T @object, String name, String? value, BindingFlags binding)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            PropertyInfo property = GetPropertyInfo(@object, name, binding);
            TypeConverter converter = TypeDescriptor.GetConverter(property.PropertyType);
            Object? result = converter.ConvertFromString(value!);
            property.SetValue(@object, result);
            return @object;
        }

        /// <summary>
        /// Sets all fields and properties of the specified type in the provided object to the specified value.
        /// <para>Internal, protected and private fields are included, static are not.</para>
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <typeparam name="TValue">The type of the properties.</typeparam>
        /// <param name="object">The object.</param>
        /// <param name="value">The value to set the properties to.</param>
        public static T SetAllPropertiesOfType<T, TValue>(this T @object, TValue? value)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            FieldInfo[] fields = @object.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                if (field.FieldType == typeof(TValue))
                {
                    field.SetValue(@object, value);
                }
            }

            return @object;
        }

        /// <summary>
        /// Determines whether or not this object has a property with the specified name.
        /// </summary>
        /// <param name="object">The object.</param>
        /// <param name="name">The name of the property.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasProperty(this Object @object, String name)
        {
            return HasProperty(@object, name, true);
        }

        /// <summary>
        /// Determines whether or not this object has a property with the specified name.
        /// </summary>
        /// <param name="object">The object.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="inherited">Determines whether of not to include inherited properties.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasProperty(this Object @object, String name, Boolean inherited)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Type type = @object.GetType();
            return HasProperty(type, name, inherited);
        }

        /// <summary>
        /// Determines whether or not this type has a property with the specified name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name of the property.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasProperty(this Type type, String name)
        {
            return HasProperty(type, name, true);
        }

        /// <summary>
        /// Determines whether or not this type has a property with the specified name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="inherited">Determines whether of not to include inherited properties.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasProperty(this Type type, String name, Boolean inherited)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            const BindingFlags binding = BindingFlags.Public | BindingFlags.Instance;
            PropertyInfo? property = type.GetProperty(name, binding | (inherited ? BindingFlags.FlattenHierarchy : BindingFlags.DeclaredOnly));
            return property is not null;
        }

        public static Boolean Implements<TValue>(Object? value)
        {
            return Implements(value, typeof(TValue));
        }

        public static Boolean Implements(Object? value, Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return value is not null && Implements(value.GetType(), type);
        }

        public static Boolean Implements<TValue>(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Implements(type, typeof(TValue));
        }

        public static Boolean Implements(this Type type, Type? other)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (other is null)
            {
                return false;
            }

            return type.IsGenericTypeDefinition ? type.ImplementsGeneric(type) : type.IsAssignableFrom(type);
        }

        public static Boolean ImplementsGeneric(this Type type, Type @interface)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (@interface is null)
            {
                throw new ArgumentNullException(nameof(@interface));
            }

            return type.ImplementsGeneric(@interface, out _);
        }

        public static Boolean ImplementsGeneric(this Type type, Type @interface, out Type? result)
        {
            result = null;

            if (@interface.IsInterface)
            {
                result = type.GetSafeInterfacesUnsafe().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == @interface);

                if (result is not null)
                {
                    return true;
                }
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == @interface)
            {
                result = type;
                return true;
            }

            Type? @base = type.BaseType;
            return @base is not null && @base.ImplementsGeneric(@interface, out result);
        }

#if NET8_0_OR_GREATER
        [Obsolete("Formatter-based serialization is obsolete and should not be used.", DiagnosticId = "SYSLIB0050", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
#endif
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSerializable<T>(this IEnumerable<T?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return typeof(T).IsValueType ? typeof(T).IsSerializable : source.All(static value => value?.GetType().IsSerializable is not false);
        }

        public static String? StripQualifier(this Type type)
        {
            return StripQualifier(type, AssemblyNameType.Default);
        }

        public static String? StripQualifier(this Type type, AssemblyNameType assemblyname)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            String? name = type.AssemblyQualifiedName;
            return name is not null ? StripQualifier(name, assemblyname) : null;
        }

        public static String StripQualifier(String type)
        {
            return StripQualifier(type, AssemblyNameType.Default);
        }

        public static String StripQualifier(String type, AssemblyNameType assemblyname)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            const String version = ", Version=";
            const String culture = ", Culture=";
            const String publickey = ", PublicKeyToken=";

            NameValueCollection tokens = new NameValueCollection();

            type = $"[{type.Trim('[', ']')}]";

            foreach (String token in new[] { version, culture, publickey })
            {
                Int32 index = type.IndexOf(token, StringComparison.InvariantCulture);

                while (index > 0)
                {
                    Int32 lastindex = type.IndexOfAny(new[] { ']', ',' }, index + 2);
                    String value = type.Substring(index, lastindex - index);
                    tokens[token] = value;
                    type = type.Remove(value);
                    index = type.IndexOf(token, StringComparison.InvariantCulture);
                }
            }

            type = type.TrimStart('[').TrimEnd(']');

            if (assemblyname.HasFlag(AssemblyNameType.Version))
            {
                type += tokens[version];
            }

            if (assemblyname.HasFlag(AssemblyNameType.Culture))
            {
                type += tokens[culture];
            }

            if (assemblyname.HasFlag(AssemblyNameType.PublicKeyToken))
            {
                type += tokens[publickey];
            }

            return type;
        }

        private static class SizeStorage<T> where T : struct
        {
            // ReSharper disable once StaticMemberInGenericType
            public static Int32 Size { get; }

            static SizeStorage()
            {
                Size = SizeOf(typeof(T));
            }
        }

        private static class SizeStorage
        {
            private static ConcurrentDictionary<Type, Int32> Cache { get; } = new ConcurrentDictionary<Type, Int32>();

            public static Int32 GetTypeSize(Type type)
            {
                if (Cache.TryGetValue(type, out Int32 size))
                {
                    return size;
                }

                if (!type.IsValueType)
                {
                    throw new ArgumentException(@"Is not value type", nameof(type));
                }

                DynamicMethod method = new DynamicMethod("SizeOfType", typeof(Int32), Type.EmptyTypes);
                ILGenerator il = method.GetILGenerator();
                il.Emit(OpCodes.Sizeof, type);
                il.Emit(OpCodes.Ret);

                Object? value = method.Invoke(null, null);

                if (value is null)
                {
                    return 0;
                }

                size = (Int32) value;
                Cache.TryAdd(type, size);
                return size;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 SizeOf<T>(this T _) where T : struct
        {
            return SizeOf<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 SizeOf<T>(this T[] array) where T : struct
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return SizeStorage<T>.Size * array.Length;
        }

        public static Boolean SizeOf<T>(this T[] array, out Int64 size) where T : struct
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            try
            {
                size = SizeStorage<T>.Size * array.LongLength;
                return true;
            }
            catch (OverflowException)
            {
                size = default;
                return false;
            }
        }

        public static Boolean SizeOf<T>(this T[] array, out BigInteger size) where T : struct
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            try
            {
                size = SizeStorage<T>.Size * (BigInteger) array.LongLength;
                return true;
            }
            catch (OverflowException)
            {
                size = default;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 SizeOf<T>() where T : struct
        {
            return SizeStorage<T>.Size;
        }

        public static Int32 SizeOf(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsValueType)
            {
                throw new ArgumentException(@"Is not value type", nameof(type));
            }

            return SizeStorage.GetTypeSize(type);
        }

        private static class GenericDefaultStorage
        {
            private static ConcurrentDictionary<Type, ValueType> Values { get; } = new ConcurrentDictionary<Type, ValueType>();

            // ReSharper disable once MemberHidesStaticFromOuterClass
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public static Object? Default(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                return type.IsValueType ? Values.GetOrAdd(type, static type => (ValueType) Activator.CreateInstance(type)!) : null;
            }
        }

        public static IEnumerator<StackFrame> GetEnumerator(this StackTrace stack)
        {
            if (stack is null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            Int32 i = 0;
            while (i < stack.FrameCount && stack.GetFrame(i++) is { } frame)
            {
                yield return frame;
            }
        }

        public static IEnumerable<Type> GetStackTypes(this StackTrace stack)
        {
            if (stack is null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            foreach (StackFrame frame in stack)
            {
                if (frame.GetMethod()?.DeclaringType is { } type)
                {
                    yield return type;
                }
            }
        }

        public static IEnumerable<Type> GetStackTypesUnique(this StackTrace stack)
        {
            if (stack is null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            return stack.GetStackTypes().Distinct();
        }

        /// <summary>
        /// Gets the default value of this type.
        /// </summary>
        /// <param name="type">The type for which to get the default value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object? Default(Type type)
        {
            return GenericDefaultStorage.Default(type);
        }

        /// <summary>
        /// Gets the default value of this type.
        /// </summary>
        /// <param name="type">The type for which to get the default value.</param>
        /// <param name="nullable"><see cref="Nullable{T}"/> is value or null.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Object? Default(Type type, Boolean nullable)
        {
            return nullable || Nullable.GetUnderlyingType(type) is null ? Default(type) : null;
        }

        /// <summary>
        /// Gets the default value of the specified type.
        /// </summary>
        /// <typeparam name="T">The type for which to get the default value.</typeparam>
        public static T? Default<T>()
        {
            return default;
        }
    }
}