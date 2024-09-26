using System;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.Utilities.Core;
using NetExtender.WindowsPresentation.ReactiveUI.DependencyInjection.Types.Providers.Scopes;
using NetExtender.WindowsPresentation.Types.Scopes.Interfaces;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    [ReflectionNaming(typeof(DependencyWindowsPresentationUtilities))]
    public static class ReactiveWindowsPresentationServiceProviderUtilities
    {
        [ReflectionSignature(typeof(DependencyWindowsPresentationUtilities))]
        internal static WindowsPresentation.Types.Scopes.Interfaces.IServiceScope CreateScope(IServiceProvider provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            Microsoft.Extensions.DependencyInjection.IServiceScope scope = ServiceProviderServiceExtensions.CreateScope(provider);
            return new ServiceScopeWrapper(scope);
        }
        
        [ReflectionSignature(typeof(DependencyWindowsPresentationUtilities))]
        internal static IAsyncServiceScope CreateAsyncScope(IServiceProvider provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            AsyncServiceScope scope = ServiceProviderServiceExtensions.CreateAsyncScope(provider);
            return new AsyncServiceScopeWrapper(ref scope);
        }
    }
}