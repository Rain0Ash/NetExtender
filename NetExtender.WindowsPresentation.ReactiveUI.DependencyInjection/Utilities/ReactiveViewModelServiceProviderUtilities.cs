using System;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    [ReflectionNaming(typeof(DependencyViewModelUtilities))]
    public static class ReactiveViewModelServiceProviderUtilities
    {
        [ReflectionSignature(typeof(DependencyViewModelUtilities))]
        public static IServiceProvider Provider
        {
            get
            {
                return ServiceProviderUtilities.Provider;
            }
        }
    }
}