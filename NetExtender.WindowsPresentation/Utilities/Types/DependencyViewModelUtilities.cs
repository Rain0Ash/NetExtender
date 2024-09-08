using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.DependencyInjection.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Core;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    public static class DependencyViewModelUtilities
    {
        static DependencyViewModelUtilities()
        {
            ReactiveViewModelUtilities.Initialize();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? Get<T>() where T : IDependencyViewModel
        {
            return ReactiveViewModelUtilities.Get<T>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Get<T>([MaybeNullWhen(false)] out T result) where T : IDependencyViewModel
        {
            result = Get<T>();
            return result is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>(Func<T> alternate) where T : IDependencyViewModel
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }
            
            return Get<T>() ?? alternate();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T, TAlternate>() where T : IDependencyViewModel where TAlternate : T, new()
        {
            return Get<T>() ?? new TAlternate();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Require<T>() where T : IDependencyViewModel
        {
            return Get<T>() ?? throw new InvalidOperationException($"Dependency model '{typeof(T)}' not found.");
        }
        
        [ReflectionNaming]
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class ReactiveViewModelUtilities
        {
            private static MethodInfo? DIGetMethod { get; }
            
            static ReactiveViewModelUtilities()
            {
                if (Initialize(out MethodInfo? method))
                {
                    DIGetMethod = method;
                }
            }
            
            public static void Initialize()
            {
            }
            
            private static Boolean Initialize([MaybeNullWhen(false)] out MethodInfo method)
            {
                try
                {
                    Assembly? assembly = Type.GetType(nameof(ReactiveViewModelUtilities))?.Assembly;
                    Type? type = assembly?.GetType(nameof(ReactiveViewModelUtilities));
                    
                    const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
                    method = type?.GetMethod(nameof(Get), binding, Type.EmptyTypes);
                    return method is not null;
                }
                catch (Exception)
                {
                    method = default;
                    return false;
                }
            }
            
            [ReflectionSignature]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static T? Get<T>() where T : IDependencyViewModel
            {
                return Storage<T>.Get();
            }
            
            private static class Storage<T> where T : IDependencyViewModel
            {
                private static Func<T?> Getter { get; }
                
                static Storage()
                {
                    Func<T?>? getter = DIGetMethod?.MakeGenericMethod(typeof(T)).CreateDelegate<Func<T?>>();
                    Getter = getter ?? throw new NotSupportedReflectionException();
                }
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static T? Get()
                {
                    return Getter.Invoke();
                }
            }
        }
    }
}