using System;

namespace NetExtender.Patch
{
    public enum ReflectionPatchState : Byte
    {
        None,
        Apply,
        Failed,
        NotRequired
    }
}