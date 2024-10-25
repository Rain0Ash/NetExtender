using System;

namespace NetExtender.Types.Reflection.Interfaces
{
    public interface IReflectionPatch : IReflectionPatchInfo
    {
        public Object SyncRoot { get; }
        
        public Boolean Apply();
    }
    
    public interface IReflectionPatchInfo
    {
        public ReflectionPatchState State { get; }
        public ReflectionPatchThrow IsThrow { get; }
    }
}