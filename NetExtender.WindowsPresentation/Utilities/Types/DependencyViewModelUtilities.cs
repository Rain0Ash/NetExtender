// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Core;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public static class DependencyViewModelUtilities
    {
        static DependencyViewModelUtilities()
        {
            ReactiveViewModelServiceProviderUtilities.Initialize();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IServiceProvider GetProvider()
        {
            return ReactiveViewModelServiceProviderUtilities.Provider();
        }
        
        [ReflectionNaming]
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class ReactiveViewModelServiceProviderUtilities
        {
            private const String ReactiveUI = nameof(ReactiveUI);
            
            [ReflectionNaming]
            public static Func<IServiceProvider> Provider { get; }
            
            static ReactiveViewModelServiceProviderUtilities()
            {
                if (!Initialize(out MethodInfo? provider))
                {
                    throw new NotSupportedReflectionException($"Not supported. Please, check dependency '{nameof(NetExtender)}.{nameof(WindowsPresentation)}.{nameof(ReactiveUI)}.{nameof(DependencyInjection)}'.");
                }
                
                Provider = provider.CreateDelegate<Func<IServiceProvider>>();
            }
            
            public static void Initialize()
            {
            }
            
            private static Boolean Initialize([MaybeNullWhen(false)] out MethodInfo provider)
            {
                try
                {
                    Type? type = Type.GetType($"{nameof(NetExtender)}.{nameof(WindowsPresentation)}.{nameof(Utilities)}.{nameof(Types)}.{nameof(ReactiveViewModelServiceProviderUtilities)}, {nameof(NetExtender)}.{nameof(WindowsPresentation)}.{nameof(ReactiveUI)}.{nameof(DependencyInjection)}");
                    const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
                    provider = type?.GetProperty(nameof(Provider), binding)?.GetMethod;
                    return provider is not null;
                }
                catch (Exception)
                {
                    provider = default;
                    return false;
                }
            }
        }
    }
}