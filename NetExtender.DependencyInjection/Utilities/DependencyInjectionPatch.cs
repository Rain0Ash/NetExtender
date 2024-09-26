using System;
using NetExtender.Types.Reflection;

namespace NetExtender.Utilities.Types
{
    public partial class DependencyInjectionPatch : ReflectionPatch<DependencyInjectionPatch>
    {
        protected static Func<Patch> Factory { get; set; }

        static DependencyInjectionPatch()
        {
            Factory = static () => new Patch();
        }

        protected sealed override Patch Create()
        {
            return Factory();
        }
    }
}