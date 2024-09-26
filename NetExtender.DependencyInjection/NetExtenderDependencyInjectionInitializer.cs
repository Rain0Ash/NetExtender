using System.Runtime.CompilerServices;
using NetExtender.Utilities.Types;

namespace NetExtender.DependencyInjection
{
    internal static class NetExtenderDependencyInjectionInitializer
    {
#pragma warning disable CA2255
        [ModuleInitializer]
        public static void Initialize()
        {
            DependencyInjectionPatch.Apply();
        }
#pragma warning restore CA2255
    }
}