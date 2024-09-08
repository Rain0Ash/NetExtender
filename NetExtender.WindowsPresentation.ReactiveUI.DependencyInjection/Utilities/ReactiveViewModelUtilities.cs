using Microsoft.Extensions.DependencyInjection;
using NetExtender.DependencyInjection.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    [ReflectionNaming]
    public static class ReactiveViewModelUtilities
    {
        [ReflectionSignature(typeof(DependencyViewModelUtilities))]
        public static T? Get<T>() where T : IDependencyViewModel
        {
            return ServiceProviderUtilities.Provider.GetService<T>();
        }
    }
}