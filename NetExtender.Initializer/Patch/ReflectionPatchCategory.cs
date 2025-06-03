// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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