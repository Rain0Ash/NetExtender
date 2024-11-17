using System.Runtime.CompilerServices;

namespace NetExtender.DependencyInjection
{
    internal static class NetExtenderDomainInitializer
    {
#pragma warning disable CA2255
        [ModuleInitializer]
        public static void Initialize()
        {
            Initializer.Initializer.IsDomain = null;
        }
#pragma warning restore CA2255
    }
}