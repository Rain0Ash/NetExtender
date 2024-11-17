using System;

namespace NetExtender.Patch
{
    [Flags]
    public enum ReflectionPatchCategory
    {
        Unspecified = 0,
        Capability = 1,
        Special = 2,
        Aphilargyria = 4,
        All = Capability | Special | Aphilargyria
    }
}