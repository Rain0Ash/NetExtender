// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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