using System;
using NetExtender.Types.Reflection;

namespace NetExtender.Patch
{
    public partial class EntityFrameworkCoreZPatch : AutoReflectionPatch<EntityFrameworkCoreZPatch>
    {
        protected static Func<Patch> Factory { get; set; }

        static EntityFrameworkCoreZPatch()
        {
            Factory = static () => new Patch();
        }

        protected sealed override Patch Create()
        {
            return Factory();
        }
    }
}