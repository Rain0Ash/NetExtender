using System;
using NetExtender.Types.Reflection;

namespace NetExtender.Patch
{
    public partial class DependencyInjectionPatch : AutoReflectionPatch<DependencyInjectionPatch>
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