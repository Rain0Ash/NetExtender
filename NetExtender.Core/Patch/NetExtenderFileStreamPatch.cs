using System;
using NetExtender.Types.Reflection;

namespace NetExtender.Patch
{
    public partial class NetExtenderFileStreamPatch : AutoReflectionPatch<NetExtenderFileStreamPatch>
    {
        protected static Func<Patch> Factory { get; set; }

        static NetExtenderFileStreamPatch()
        {
            Factory = static () => new Patch();
        }

        protected sealed override Patch Create()
        {
            return Factory();
        }
    }
}