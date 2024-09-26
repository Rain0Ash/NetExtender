using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Core;
using NetExtender.WindowsPresentation.Types.Scopes.Interfaces;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    public static class DependencyWindowsPresentationUtilities
    {
        static DependencyWindowsPresentationUtilities()
        {
            ReactiveWindowsPresentationServiceProviderUtilities.Initialize();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IServiceScope CreateScope(this IServiceProvider provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            return ReactiveWindowsPresentationServiceProviderUtilities.CreateScope(provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IAsyncServiceScope CreateAsyncScope(this IServiceProvider provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            return ReactiveWindowsPresentationServiceProviderUtilities.CreateAsyncScope(provider);
        }

        [ReflectionNaming]
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class ReactiveWindowsPresentationServiceProviderUtilities
        {
            private const String ReactiveUI = nameof(ReactiveUI);
            
            [ReflectionNaming]
            public static Func<IServiceProvider, IServiceScope> CreateScope { get; }
            
            [ReflectionNaming]
            public static Func<IServiceProvider, IAsyncServiceScope> CreateAsyncScope { get; }
            
            static ReactiveWindowsPresentationServiceProviderUtilities()
            {
                if (!Initialize(out MethodInfo? scope, out MethodInfo? async))
                {
                    throw new NotSupportedReflectionException($"Not supported. Please, check dependency '{nameof(NetExtender)}.{nameof(WindowsPresentation)}.{nameof(ReactiveUI)}.{nameof(DependencyInjection)}'.");
                }
                
                CreateScope = scope.CreateDelegate<Func<IServiceProvider, IServiceScope>>();
                CreateAsyncScope = async.CreateDelegate<Func<IServiceProvider, IAsyncServiceScope>>();
            }
            
            public static void Initialize()
            {
            }
            
            private static Boolean Initialize([MaybeNullWhen(false)] out MethodInfo scope, [MaybeNullWhen(false)] out MethodInfo async)
            {
                try
                {
                    Type? type = Type.GetType($"{nameof(NetExtender)}.{nameof(WindowsPresentation)}.{nameof(Utilities)}.{nameof(Types)}.{nameof(ReactiveWindowsPresentationServiceProviderUtilities)}, {nameof(NetExtender)}.{nameof(WindowsPresentation)}.{nameof(ReactiveUI)}.{nameof(DependencyInjection)}");
                    const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
                    scope = type?.GetMethod(nameof(CreateScope), binding, new []{ typeof(IServiceProvider) });
                    async = type?.GetMethod(nameof(CreateAsyncScope), binding, new []{ typeof(IServiceProvider) });
                    return scope is not null && async is not null;
                }
                catch (Exception)
                {
                    scope = default;
                    async = default;
                    return false;
                }
            }
        }
    }
}