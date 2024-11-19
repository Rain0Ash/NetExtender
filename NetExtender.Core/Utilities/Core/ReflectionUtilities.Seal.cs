using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using NetExtender.Types.Assemblies;
using NetExtender.Types.Assemblies.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.Utilities.Core
{
    public static partial class ReflectionUtilities
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class SealStorage
        {
            private static IDynamicAssemblyUnsafeStorage Assembly { get; } = new DynamicAssemblyStorage($"{nameof(ReflectionUtilities)}<{nameof(Seal)}>", AssemblyBuilderAccess.Run);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Type Seal(Type type, Func<Type, String, String?>? name, Action<Type, TypeBuilder>? builder)
            {
                return ReflectionUtilities.Seal(Assembly, type, name, builder);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        [SuppressMessage("Usage", "CA2200")]
        internal static Type Seal(IDynamicAssemblyUnsafeStorage assembly, Type type, Func<Type, String, String?>? name, Action<Type, TypeBuilder>? builder)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
                
            try
            {
                return assembly.Storage.GetOrAdd(type, type is { IsAbstract: true, IsSealed: true } ? StaticStorage.Static(assembly, type, name, builder) : Initializer.Initializer.ReflectionUtilities.Seal(type, name, builder, assembly));
            }
            catch (Initializer.Initializer.ReflectionUtilities.TypeSealException exception)
            {
                throw new TypeNotSupportedException(exception.Type, exception.Message);
            }
            catch (Exception exception)
            {
                // ReSharper disable once PossibleIntendedRethrow
                throw exception;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type Seal(this Type type)
        {
            return Seal(type, (Func<Type, String, String?>?) null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type Seal(this Type type, Action<Type, TypeBuilder>? builder)
        {
            return Seal(type, (Func<Type, String, String?>?) null, builder);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type Seal(this Type type, String? name)
        {
            return Seal(type, name, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type Seal(this Type type, String? name, Action<Type, TypeBuilder>? builder)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                return Seal(type, builder);
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentWhiteSpaceStringException(nameof(name));
            }

            return Seal(type, (_, @namespace) => @namespace + name, builder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type Seal(this Type type, Func<Type, String, String?>? name)
        {
            return Seal(type, name, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type Seal(this Type type, Func<Type, String, String?>? name, Action<Type, TypeBuilder>? builder)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return type is { IsAbstract: true, IsSealed: true } ? StaticStorage.Static(type, name, builder) : SealStorage.Seal(type, name, builder);
        }
    }
}