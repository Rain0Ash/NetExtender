using System;

namespace NetExtender.Types.Reflection.Interfaces
{
    public interface IReflectionPatch
    {
        public Object SyncRoot { get; }
        public ReflectionPatchState State { get; }
        public Boolean IsThrow { get; }
        
        public Boolean Apply();
    }
}