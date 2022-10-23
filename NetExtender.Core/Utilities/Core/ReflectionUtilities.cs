// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
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
using NetExtender.Types.Attributes;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Core
{
    [Flags]
    public enum PrimitiveType : Byte
    {
        Default = 0,
        String = 1,
        Decimal = 2,
        Complex = 4,
        TimeSpan = 8,
        DateTime = 16,
        DateTimeOffset = 32,
        All = String | Decimal | Complex | TimeSpan | DateTime | DateTimeOffset
    }

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

    public static class ReflectionUtilities
    {
        private static HashSet<Type> VarArgTypes { get; }

        private static Predicate<Type> IsByRefLikePredicate { get; }

        public static Boolean AssemblyLoadCallStaticContructor { get; set; }

        static ReflectionUtilities()
        {
            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;
            VarArgTypes = new[] { Type.GetType("System.ArgIterator"), Type.GetType("System.RuntimeArgumentHandle"), Type.GetType("System.TypedReference") }.OfType<Type>().ToHashSet();
            IsByRefLikePredicate = typeof(Type).GetProperty("IsByRefLike")?.GetMethod?.CreateDelegate(typeof(Predicate<Type>)) as Predicate<Type> ?? (_ => false);
        }

        private static void OnAssemblyLoad(Object? sender, AssemblyLoadEventArgs args)
        {
            CallStaticInitializerAttributeInternal<StaticInitializerRequiredAttribute>(args.LoadedAssembly);

            if (AssemblyLoadCallStaticContructor)
            {
                CallStaticInitializerAttribute(args.LoadedAssembly);
            }
        }
        
        public static Boolean IsVarArgType(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return VarArgTypes.Contains(type);
        }
        
        public static Boolean IsStackOnly(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsByRef || type.IsVarArgType() || IsByRefLikePredicate.Invoke(type);
        }

        public static Boolean IsBoxable(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return !type.IsPointer && !type.IsStackOnly();
        }

        public static Boolean IsILBoxable(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsBoxable() || type.IsByRef || type.IsPointer;
        }

        public static Boolean IsAssignableFrom<T>(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsAssignableFrom(typeof(T));
        }

        public static Boolean IsAssignableFrom<T>(this TypeInfo type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsAssignableFrom(typeof(T).GetTypeInfo());
        }

        public static Boolean IsAssignableTo<T>(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsAssignableTo(typeof(T));
        }

        public static Boolean IsAssignableTo<T>(this TypeInfo type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsAssignableTo(typeof(T).GetTypeInfo());
        }

        public static Boolean IsSameAsOrSubclassOf(this Type type, Type other)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return type == other || type.IsSubclassOf(other);
        }

        public static Boolean IsSameAsOrSubclassOf(this TypeInfo type, Type other)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return type.AsType() == other || type.IsSubclassOf(other);
        }

        public static Boolean IsSameAsOrSubclassOf<T>(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsSameAsOrSubclassOf(typeof(T));
        }

        public static Boolean IsSameAsOrSubclassOf<T>(this TypeInfo type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsSameAsOrSubclassOf(typeof(T));
        }

        public static Boolean IsGenericTypeDefinedAs(this Type type, Type? other)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (other is null || !type.IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == other;
        }

        public static Boolean IsGenericTypeDefinedAs(this TypeInfo type, Type? other)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (other is null || !type.IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == other;
        }

        public static Int32 GetGenericArgumentsCount(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsGenericType ? type.GetGenericArguments().Length : 0;
        }

        public static Boolean HasInterface<T>(this Type type) where T : class
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.HasInterface(typeof(T));
        }

        public static Boolean HasInterface<T>(this TypeInfo type) where T : class
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.HasInterface(typeof(T));
        }

        public static Boolean HasInterface(this Type type, Type @interface)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (@interface is null)
            {
                throw new ArgumentNullException(nameof(@interface));
            }

            return type.GetInterfaces().Contains(@interface);
        }

        public static Boolean HasInterface(this TypeInfo type, Type @interface)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (@interface is null)
            {
                throw new ArgumentNullException(nameof(@interface));
            }

            return type.ImplementedInterfaces.Contains(@interface);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type TryGetGenericTypeDefinition(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsGenericType ? type.GetGenericTypeDefinition() : type;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] TryGetGenericTypeDefinitionInterfaces(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Type[] interfaces = type.GetInterfaces();
            interfaces.InnerChange(TryGetGenericTypeDefinition);

            return interfaces;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo TryGetGenericMethodDefinition(this MethodInfo method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.IsGenericMethod ? method.GetGenericMethodDefinition() : method;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[]? TryGetGenericArguments(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsGenericType ? type.GetGenericArguments() : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[]? TryGetGenericArguments(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.IsGenericMethod ? method.GetGenericArguments() : null;
        }
        
        public static Boolean IsInheritable(this MethodBase method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            Type? declaring = method.DeclaringType;

            if (declaring is null)
            {
                throw new TypeAccessException();
            }

            return declaring.IsVisible && !declaring.IsSealed && !method.IsStatic && (method.IsPublic || method.IsFamily || method.IsFamilyOrAssembly);
        }
        
        public static Boolean IsOverridable(this MethodInfo method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.IsInheritable() && method.IsVirtual && !method.IsFinal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasAttribute<T>(this MemberInfo info) where T : Attribute
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.GetCustomAttribute<T>() is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasAttribute<T>(this MemberInfo info, Boolean inherit) where T : Attribute
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.GetCustomAttribute<T>(inherit) is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasAttribute(this MemberInfo info, Type attribute)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            return info.GetCustomAttribute(attribute) is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasAttribute(this MemberInfo info, Type attribute, Boolean inherit)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            return info.GetCustomAttribute(attribute, inherit) is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetCustomAttribute<T>(this MemberInfo info) where T : Attribute
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.GetCustomAttribute(typeof(T)) as T;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetCustomAttribute<T>(this MemberInfo info, Boolean inherit) where T : Attribute
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.GetCustomAttribute(typeof(T), inherit) as T;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo info) where T : Attribute
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.GetCustomAttributes(typeof(T)).OfType<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo info, Boolean inherit) where T : Attribute
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.GetCustomAttributes(typeof(T), inherit).OfType<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type GetEntryPointType()
        {
            return TryGetEntryPointType(out Type? type) ? type : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type GetEntryPointType(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return TryGetEntryPointType(assembly, out Type? type) ? type : throw new InvalidOperationException();
        }

        public static Boolean TryGetEntryPointType([MaybeNullWhen(false)] out Type result)
        {
            Assembly? assembly = Assembly.GetEntryAssembly();

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

        public static Assembly LoadAssembly(this AssemblyName assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return Assembly.Load(assembly);
        }

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

        public static Assembly LoadAssemblyFrom(String assembly, Byte[]? hash, AssemblyHashAlgorithm algorithm)
        {
            return LoadAssemblyFrom(assembly, hash, algorithm switch
            {
                AssemblyHashAlgorithm.None => System.Configuration.Assemblies.AssemblyHashAlgorithm.None,
                AssemblyHashAlgorithm.MD5 => System.Configuration.Assemblies.AssemblyHashAlgorithm.MD5,
                AssemblyHashAlgorithm.Sha1 => System.Configuration.Assemblies.AssemblyHashAlgorithm.SHA1,
                AssemblyHashAlgorithm.Sha256 => System.Configuration.Assemblies.AssemblyHashAlgorithm.SHA256,
                AssemblyHashAlgorithm.Sha384 => System.Configuration.Assemblies.AssemblyHashAlgorithm.SHA384,
                AssemblyHashAlgorithm.Sha512 => System.Configuration.Assemblies.AssemblyHashAlgorithm.SHA512,
                _ => throw new NotSupportedException()
            });
        }

        public static Assembly LoadAssemblyFrom(String assembly, Byte[]? hash, System.Configuration.Assemblies.AssemblyHashAlgorithm algorithm)
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

        public static Boolean TryLoadAssemblyFrom(String assembly, Byte[]? hash, System.Configuration.Assemblies.AssemblyHashAlgorithm algorithm,
            [MaybeNullWhen(false)] out Assembly result)
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
                throw new ArgumentException("Value cannot be null or empty.", nameof(resource));
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

        public static String GetEntryTypeNamespace(Boolean root)
        {
            String? @namespace = GetEntryPointType().Namespace;

            if (@namespace is null)
            {
                throw new InvalidOperationException();
            }
            
            return root ? @namespace.Split('.')[0] : @namespace;
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
        public static Type[] GetTypesWithoutNamespace(String name)
        {
            return GetTypesWithoutNamespace(name, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetTypesWithoutNamespace(String name, Boolean insensitive)
        {
            return GetTypesWithoutNamespace(Assembly.GetEntryAssembly() ?? throw new InvalidOperationException(), name, insensitive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetTypesWithoutNamespace(this Assembly assembly, String name)
        {
            return GetTypesWithoutNamespace(assembly, name, false);
        }

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
            
            StringComparison comparison = insensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            return assembly.GetTypes().Where(type => String.Equals(type.Name, name, comparison)).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type? GetTypeWithoutNamespace(String name)
        {
            return GetTypeWithoutNamespace(name, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type? GetTypeWithoutNamespace(String name, Boolean insensitive)
        {
            return GetTypeWithoutNamespace(Assembly.GetEntryAssembly() ?? throw new InvalidOperationException(), name, insensitive);
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
            return TryGetTypeWithoutNamespace(Assembly.GetEntryAssembly() ?? throw new InvalidOperationException(), name, insensitive, out result);
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

        /// <summary>
        /// Gets all the namespaces in this assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public static IEnumerable<String?> GetNamespaces(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return assembly.GetTypes().Select(type => type.Namespace);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypesInNamespace(String @namespace)
        {
            return GetTypesInNamespace(@namespace, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypesInNamespace(String @namespace, Boolean insensitive)
        {
            return GetTypesInNamespace(Assembly.GetEntryAssembly() ?? throw new InvalidOperationException(), @namespace, insensitive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypesInNamespace(this Assembly assembly, String? @namespace)
        {
            return GetTypesInNamespace(assembly, @namespace, false);
        }

        public static IEnumerable<Type> GetTypesInNamespace(this Assembly assembly, String? @namespace, Boolean insensitive)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            StringComparison comparison = insensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            return assembly.GetTypes().Where(type => String.Equals(type.Namespace, @namespace, comparison)).ToArray();
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
                throw new ArgumentOutOfRangeException(nameof(levels));
            }

            StackFrame frame = new StackFrame(levels, true);
            MethodBase method = frame.GetMethod() ?? throw new InvalidOperationException();

            return $"{Path.GetFileName(frame.GetFileName())}::{method.DeclaringType?.FullName ?? String.Empty}.{method.Name} - Line {frame.GetFileLineNumber()}";
        }

        public static IEnumerable<Assembly> DomainCustomAssemblies
        {
            get
            {
                Assembly calling = Assembly.GetCallingAssembly();
                Assembly? entry = Assembly.GetEntryAssembly();
                return AppDomain.CurrentDomain.GetAssemblies().WhereNot(IsSystemAssembly).Append(entry).Append(calling).WhereNotNull().Distinct()
                    .OrderByDescending(assembly => assembly == Assembly.GetExecutingAssembly())
                    .ThenByDescending(assembly => assembly.GetName().FullName.StartsWith(nameof(NetExtender)))
                    .ThenByDescending(assembly => assembly == calling)
                    .ThenByDescending(assembly => assembly == entry)
                    .ThenBy(assembly => assembly.GetName().FullName);
            }
        }

        /// <summary>
        /// Calls the static constructor of this type.
        /// </summary>
        /// <param name="type">The type of which to call the static constructor.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type CallStaticConstructor(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            CallStaticConstructor(type.TypeHandle);
            return type;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RuntimeTypeHandle CallStaticConstructor(this RuntimeTypeHandle handle)
        {
            RuntimeHelpers.RunClassConstructor(handle);
            return handle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> CallStaticConstructor(this IEnumerable<Type> source)
        {
            return CallStaticConstructor(source, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> CallStaticConstructor(this IEnumerable<Type> source, Boolean lazy)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            static void Call(Type type)
            {
                CallStaticConstructor(type);
            }

            return source.ForEach(Call).MaterializeIfNot(lazy);
        }

        private static Assembly CallStaticInitializerAttributeInternal<TAttribute>(this Assembly assembly) where TAttribute : StaticInitializerAttribute, new()
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            static IEnumerable<TAttribute> Handler(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                IEnumerable<TAttribute> attributes = GetCustomAttributes<TAttribute>(type);

                foreach (TAttribute attribute in attributes)
                {
                    if (!attribute.Active || !attribute.Platform.IsOSPlatform())
                    {
                        continue;
                    }

                    if (attribute.Type is null)
                    {
                        yield return new TAttribute { Type = type, Priority = attribute.Priority, Active = attribute.Active, Platform = attribute.Platform };
                        continue;
                    }

                    yield return attribute;
                }
            }

            IEnumerable<Type> types = assembly.GetTypes()
                .SelectMany(Handler)
                .OrderByDescending(item => item.Priority)
                .ThenBy(item => item.Type?.FullName)
                .Select(item => item.Type)
                .WhereNotNull()
                .Distinct();

            foreach (Type type in types)
            {
                type.CallStaticConstructor();
            }

            return assembly;
        }

        /// <summary>
        /// Calls the static constructor of types with <see cref="StaticInitializerAttribute"/> in assembly.
        /// </summary>
        /// <param name="assembly">The assembly of which to call the static constructor.</param>
        public static Assembly CallStaticInitializerAttribute(this Assembly assembly)
        {
            return CallStaticInitializerAttributeInternal<StaticInitializerAttribute>(assembly);
        }

        public static IEnumerable<Assembly> CallStaticInitializerAttribute(this IEnumerable<Assembly> assemblies)
        {
            return CallStaticInitializerAttribute(assemblies, false);
        }

        public static IEnumerable<Assembly> CallStaticInitializerAttribute(this IEnumerable<Assembly> assemblies, Boolean lazy)
        {
            if (assemblies is null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            static void Call(Assembly assembly)
            {
                CallStaticInitializerAttribute(assembly);
            }

            return assemblies.ForEach(Call).MaterializeIfNot(lazy);
        }

        public static IEnumerable<Assembly> CallStaticInitializerAttribute()
        {
            return CallStaticInitializerAttribute(DomainCustomAssemblies);
        }

        internal static IEnumerable<Assembly> CallStaticInitializerRequiredAttribute()
        {
            static void Call(Assembly assembly)
            {
                CallStaticInitializerAttributeInternal<StaticInitializerRequiredAttribute>(assembly);
            }

            return DomainCustomAssemblies.ForEach(Call).Materialize();
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

        /// <summary>
        /// Returns all the public properties of this object whose property type is <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the properties.</typeparam>
        /// <param name="obj">The object.</param>
        public static IEnumerable<PropertyInfo> GetAllPropertiesOfType<T>(this Object obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return GetAllPropertiesOfType<T>(obj.GetType());
        }

        /// <summary>
        /// Returns all the public properties of this type whose property type is <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the properties.</typeparam>
        /// <param name="type">The type of which to get the properties.</param>
        public static IEnumerable<PropertyInfo> GetAllPropertiesOfType<T>(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.GetProperties().Where(info => info.PropertyType == typeof(T));
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="obj">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        public static Object? GetPropertyValue(this Object obj, String name)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (GetPropertyValue(obj, name, out Object? result))
            {
                return result;
            }

            throw new Exception($"'{name}' is neither a property or a field of type '{result}'.");
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="obj">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        /// <param name="result">Object if successful else <see cref="obj"/> <see cref="Type"/></param>
        public static Boolean GetPropertyValue(this Object obj, String name, out Object? result)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (obj is not Type type)
            {
                type = obj.GetType();
            }

            PropertyInfo? property = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (property is not null)
            {
                result = property.GetValue(obj);
                return true;
            }

            FieldInfo? field = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (field is not null)
            {
                result = field.GetValue(obj);
                return true;
            }

            result = type;
            return false;
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="obj">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        public static T? GetPropertyValue<T>(this Object obj, String name)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Object? value = GetPropertyValue(obj, name);
            return value switch
            {
                null => default,
                T result => result,
                _ => value.TryConvert(out T? result) ? result : throw new InvalidCastException()
            };
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="obj">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        /// <param name="converter">Converter function</param>
        public static T GetPropertyValue<T>(this Object obj, String name, ParseHandler<Object?, T> converter)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return converter(GetPropertyValue(obj, name));
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="obj">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        /// <param name="result">Result value</param>
        public static Boolean TryGetPropertyValue<T>(this Object obj, String name, out T? result)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (GetPropertyValue(obj, name, out Object? value))
            {
                if (value is T resval)
                {
                    result = resval;
                    return true;
                }

                try
                {
                    return value.TryConvert(out result);
                }
                catch (Exception)
                {
                    result = default;
                    return false;
                }
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Gets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="obj">The object or type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        /// <param name="converter">Converter function</param>
        /// <param name="result">Result value</param>
        public static Boolean TryGetPropertyValue<T>(this Object obj, String name, TryParseHandler<Object?, T> converter, out T? result)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (GetPropertyValue(obj, name, out Object? value) && converter(value, out result))
            {
                return true;
            }

            result = default;
            return false;
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
        public static Type GetPropertyTypeReal(this Type type, String name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
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
        public static Type GetPropertyTypeReal(this Type type, String name, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            PropertyInfo property = GetPropertyInfo(type, name, binding);
            return property.PropertyType;
        }

        /// <summary>
        /// Gets the property information by name for the type of the object.
        /// </summary>
        /// <param name="obj">Object with a type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        public static PropertyInfo GetPropertyInfo(this Object obj, String name)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return GetPropertyInfo(obj.GetType(), name);
        }

        /// <summary>
        /// Gets the property information by name for the type of the object.
        /// </summary>
        /// <param name="obj">Object with a type that has the specified property.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static PropertyInfo GetPropertyInfo(this Object obj, String name, BindingFlags binding)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return GetPropertyInfo(obj.GetType(), name, binding);
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

        public static Boolean IsIndexer(this PropertyInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.GetIndexParameters().Length > 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodVisibilityType IsRead(this PropertyInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (!info.CanRead)
            {
                return MethodVisibilityType.Unavailable;
            }

            return info.GetMethod?.IsPublic switch
            {
                null => MethodVisibilityType.Unavailable,
                false => MethodVisibilityType.Private,
                true => MethodVisibilityType.Public
            };
        }
                
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodVisibilityType IsWrite(this PropertyInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (!info.CanWrite)
            {
                return MethodVisibilityType.Unavailable;
            }

            return info.SetMethod?.IsPublic switch
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
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

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
        /// <param name="obj">Object with a type that has the specified field.</param>
        /// <param name="name">The name of the field.</param>
        public static FieldInfo GetFieldInfo(this Object obj, String name)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return GetFieldInfo(obj.GetType(), name);
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
        public static IEnumerable<FieldInfo> GetConstants(this Type type)
        {
            return GetConstants(type, true);
        }

        /// <summary>
        /// Gets the constants defined in this type.
        /// </summary>
        /// <param name="type">The type from which to get the constants.</param>
        /// <param name="inherited">Determines whether or not to include inherited constants.</param>
        public static IEnumerable<FieldInfo> GetConstants(this Type type, Boolean inherited)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            BindingFlags binding = BindingFlags.Public | BindingFlags.Static | (inherited ? BindingFlags.FlattenHierarchy : BindingFlags.DeclaredOnly);
            return type.GetFields(binding).Where(field => field.IsLiteral && !field.IsInitOnly);
        }
        
        public static IEnumerable<EventInfo> GetAccessibleEvents(this Type type)
        {
            return GetAccessibleMembers(type, RuntimeReflectionExtensions.GetRuntimeEvents);
        }

        public static IEnumerable<FieldInfo> GetAccessibleFields(this Type type)
        {
            return GetAccessibleMembers(type, RuntimeReflectionExtensions.GetRuntimeFields);
        }

        public static IEnumerable<MethodInfo> GetAccessibleMethods(this Type type)
        {
            return GetAccessibleMembers(type, RuntimeReflectionExtensions.GetRuntimeMethods);
        }

        public static IEnumerable<PropertyInfo> GetAccessibleProperties(this Type type)
        {
            return GetAccessibleMembers(type, RuntimeReflectionExtensions.GetRuntimeProperties);
        }

        private static IEnumerable<T> GetAccessibleMembers<T>(this Type type, Func<Type, IEnumerable<T>> finder) where T : MemberInfo
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (finder is null)
            {
                throw new ArgumentNullException(nameof(finder));
            }

            List<T> result = new List<T>(finder(type));

            if (!type.IsInterface)
            {
                return result.ToArray();
            }

            foreach (Type @interface in type.GetInterfaces())
            {
                if (@interface.IsVisible)
                {
                    result.AddRange(finder.Invoke(@interface));
                }
            }
            
            result.AddRange(finder.Invoke(typeof(Object)));
            return result.ToArray();
        }

        /// <summary>
        /// Sets the value of the property or field with the specified name in this object or type.
        /// </summary>
        /// <param name="obj">The objector type that has the property or field.</param>
        /// <param name="name">The name of the property or field.</param>
        /// <param name="value">The value to set.</param>
        public static T SetValue<T>(this T obj, String name, Object? value)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (obj is not Type type)
            {
                type = obj.GetType();
            }

            PropertyInfo? property = type.GetProperty(name);
            if (property is not null)
            {
                property.SetValue(obj, value);
                return obj;
            }

            FieldInfo? field = type.GetField(name);

            if (field is null)
            {
                throw new Exception($"'{name}' is neither a property or a field of type '{type}'.");
            }

            field.SetValue(obj, value);
            return obj;
        }

        /// <summary>
        /// Sets the specified field to the provided value in the object.
        /// </summary>
        /// <param name="obj">The object with the field.</param>
        /// <param name="name">The name of the field to set.</param>
        /// <param name="value">The value to set the field to.</param>
        public static T SetField<T>(this T obj, String name, Object value)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            FieldInfo field = GetFieldInfo(obj, name);
            field.SetValue(obj, value);
            return obj;
        }

        /// <summary>
        /// Sets the specified property to the provided value in the object.
        /// </summary>
        /// <param name="obj">The object with the property.</param>
        /// <param name="name">The name of the property to set.</param>
        /// <param name="value">The value to set the property to.</param>
        public static T SetProperty<T>(this T obj, String name, Object value)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            PropertyInfo property = GetPropertyInfo(obj, name);
            property.SetValue(obj, value);
            return obj;
        }

        /// <summary>
        /// Sets the specified property to the provided value in the object.
        /// </summary>
        /// <param name="obj">The object with the property.</param>
        /// <param name="name">The name of the property to set.</param>
        /// <param name="value">The value to set the property to.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static T SetProperty<T>(this T obj, String name, Object value, BindingFlags binding)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            PropertyInfo property = GetPropertyInfo(obj, name, binding);
            property.SetValue(obj, value);
            return obj;
        }

        /// <summary>
        /// Sets the specified property to a value that will be extracted from the provided string value using the <see cref="TypeDescriptor.GetConverter(Type)"/> and <see cref="TypeConverter.ConvertFromString(string)"/>.
        /// </summary>
        /// <param name="obj">The object with the property.</param>
        /// <param name="name">The name of the property to set.</param>
        /// <param name="value">The string representation of the value to set to the property.</param>
        public static T SetPropertyFromString<T>(this T obj, String name, String value)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            PropertyInfo property = GetPropertyInfo(obj, name);
            TypeConverter converter = TypeDescriptor.GetConverter(property.PropertyType);
            Object? result = converter.ConvertFromString(value);
            property.SetValue(obj, result);
            return obj;
        }

        /// <summary>
        /// Sets the specified property to a value that will be extracted from the provided string value using the <see cref="TypeDescriptor.GetConverter(Type)"/> and <see cref="TypeConverter.ConvertFromString(string)"/>.
        /// </summary>
        /// <param name="obj">The object with the property.</param>
        /// <param name="name">The name of the property to set.</param>
        /// <param name="value">The string representation of the value to set to the property.</param>
        /// <param name="binding">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. -or- Zero, to return null.</param>
        public static T SetPropertyFromString<T>(this T obj, String name, String value, BindingFlags binding)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            PropertyInfo property = GetPropertyInfo(obj, name, binding);
            TypeConverter converter = TypeDescriptor.GetConverter(property.PropertyType);
            Object? result = converter.ConvertFromString(value);
            property.SetValue(obj, result);
            return obj;
        }

        /// <summary>
        /// Sets all fields and properties of the specified type in the provided object to the specified value.
        /// <para>Internal, protected and private fields are included, static are not.</para>
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <typeparam name="TValue">The type of the properties.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value to set the properties to.</param>
        public static T SetAllPropertiesOfType<T, TValue>(this T obj, TValue value)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                if (field.FieldType == typeof(TValue))
                {
                    field.SetValue(obj, value);
                }
            }

            return obj;
        }

        /// <summary>
        /// Determines whether or not this object has a property with the specified name.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="name">The name of the property.</param>
        public static Boolean HasProperty(this Object obj, String name)
        {
            return HasProperty(obj, name, true);
        }

        /// <summary>
        /// Determines whether or not this object has a property with the specified name.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="inherited">Determines whether of not to include inherited properties.</param>
        public static Boolean HasProperty(this Object obj, String name, Boolean inherited)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            Type type = obj.GetType();
            return HasProperty(type, name, inherited);
        }

        /// <summary>
        /// Determines whether or not this type has a property with the specified name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name of the property.</param>
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

            BindingFlags binding = BindingFlags.Public | BindingFlags.Instance | (inherited ? BindingFlags.FlattenHierarchy : BindingFlags.DeclaredOnly);

            PropertyInfo? property = type.GetProperty(name, binding);
            return property is not null;
        }

        private static Boolean IsMulticastDelegateFieldType(this Type? type)
        {
            return type is not null && (type == typeof(MulticastDelegate) || type.IsSubclassOf(typeof(MulticastDelegate)));
        }

        // ReSharper disable once CognitiveComplexity
        public static FieldInfo? GetEventField(this Type type, String eventname)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (eventname is null)
            {
                throw new ArgumentNullException(nameof(eventname));
            }

            FieldInfo? field = null;

            while (type is not null)
            {
                field = type.GetField(eventname, BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);

                if (field is not null && field.FieldType.IsMulticastDelegateFieldType())
                {
                    break;
                }

                field = type.GetField("EVENT_" + eventname.ToUpper(), BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);

                if (field is not null)
                {
                    break;
                }

                if (type.BaseType is null)
                {
                    break;
                }

                type = type.BaseType;
            }

            return field;
        }

        public static Boolean ClearEventInvocations(Object value, String eventname)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (eventname is null)
            {
                throw new ArgumentNullException(nameof(eventname));
            }

            try
            {
                FieldInfo? info = value.GetType().GetEventField(eventname);

                if (info is null)
                {
                    return false;
                }

                info.SetValue(value, null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
                result = type.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == @interface);

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

            Type? baseType = type.BaseType;
            return baseType is not null && baseType.ImplementsGeneric(@interface, out result);
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
                    type = type.Replace(value, String.Empty);
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

        /// <summary>
        /// Determines whether the specified type to check derives from this generic type.
        /// </summary>
        /// <param name="generic">The parent generic type.</param>
        /// <param name="check">The type to check if it derives from the specified generic type.</param>
        public static Boolean IsSubclassOfRawGeneric(Type generic, Type? check)
        {
            if (generic is null)
            {
                throw new ArgumentNullException(nameof(generic));
            }

            while (check is not null && check != typeof(Object))
            {
                Type current = check.IsGenericType ? check.GetGenericTypeDefinition() : check;
                if (generic == current)
                {
                    return true;
                }

                check = check.BaseType;
            }

            return false;
        }

        /// <inheritdoc cref="IsPrimitive(System.Type,PrimitiveType)"/>
        public static Boolean IsPrimitive(this Type type)
        {
            return IsPrimitive(type, PrimitiveType.All);
        }

        /// <summary>
        /// Determines whether this type is a primitive.
        /// <para><see cref="string"/> is considered a primitive.</para>
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="primitive">What type is primitive</param>
        public static Boolean IsPrimitive(this Type type, PrimitiveType primitive)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsPrimitive ||
                   primitive.HasFlag(PrimitiveType.String) && type == typeof(String) ||
                   primitive.HasFlag(PrimitiveType.Decimal) && type == typeof(Decimal) ||
                   primitive.HasFlag(PrimitiveType.Complex) && type == typeof(Complex) ||
                   primitive.HasFlag(PrimitiveType.TimeSpan) && type == typeof(TimeSpan) ||
                   primitive.HasFlag(PrimitiveType.DateTime) && type == typeof(DateTime) ||
                   primitive.HasFlag(PrimitiveType.DateTimeOffset) && type == typeof(DateTimeOffset);
        }

        public static Boolean IsCompilerGenerated(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsDefined(typeof(CompilerGeneratedAttribute));
        }

        private static class SizeCache<T> where T : struct
        {
            // ReSharper disable once StaticMemberInGenericType
            public static Int32 Size { get; }

            static SizeCache()
            {
                Size = GetSize(typeof(T));
            }
        }

        private static class SizeCache
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

                DynamicMethod method = new DynamicMethod("SizeOfType", typeof(Int32), Array.Empty<Type>());
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
        public static Int32 GetSize<T>(this T _) where T : struct
        {
            return GetSize<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetSize<T>(this T[] array) where T : struct
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return SizeCache<T>.Size * array.Length;
        }

        public static Boolean GetSize<T>(this T[] array, out Int64 size) where T : struct
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            try
            {
                size = SizeCache<T>.Size * array.LongLength;
                return true;
            }
            catch (OverflowException)
            {
                size = default;
                return false;
            }
        }

        public static Boolean GetSize<T>(this T[] array, out BigInteger size) where T : struct
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            try
            {
                size = SizeCache<T>.Size * (BigInteger) array.LongLength;
                return true;
            }
            catch (OverflowException)
            {
                size = default;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GetSize<T>() where T : struct
        {
            return SizeCache<T>.Size;
        }

        public static Int32 GetSize(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsValueType)
            {
                throw new ArgumentException(@"Is not value type", nameof(type));
            }

            return SizeCache.GetTypeSize(type);
        }

        private static class GenericDefaultCache
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

                static ValueType Create(Type type)
                {
                    return (ValueType) Activator.CreateInstance(type)!;
                }

                return type.IsValueType ? Values.GetOrAdd(type, Create) : null;
            }
        }

        public static IEnumerator<StackFrame> GetEnumerator(this StackTrace stack)
        {
            if (stack is null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            Int32 i = 0;
            while (i < stack.FrameCount && stack.GetFrame(i++) is StackFrame frame)
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
                if (frame.GetMethod()?.DeclaringType is Type type)
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
            return GenericDefaultCache.Default(type);
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