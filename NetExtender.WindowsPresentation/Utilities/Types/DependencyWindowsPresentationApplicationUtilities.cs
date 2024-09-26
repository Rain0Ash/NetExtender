using System;
using System.Runtime.CompilerServices;
using System.Windows;
using NetExtender.WindowsPresentation.Types;
using NetExtender.WindowsPresentation.Types.Applications.Interfaces;

namespace NetExtender.WindowsPresentation.Utilities.Types
{
    public static class DependencyWindowsPresentationApplicationUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WindowsPresentationServiceProvider? Provider(this Application application)
        {
            return application switch
            {
                null => throw new ArgumentNullException(nameof(application)),
                IDependencyApplication dependency => dependency.Provider,
                _ => null
            };
        }
    }
}