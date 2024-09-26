using System;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    [ReflectionNaming(typeof(DependencyWindowUtilities))]
    public static class ReactiveWindowServiceProviderUtilities
    {
        [ReflectionSignature(typeof(DependencyWindowUtilities))]
        public static IServiceProvider Provider
        {
            get
            {
                return ServiceProviderUtilities.Provider;
            }
        }
    }
}