using System;

namespace NetExtender.Patch
{
    internal interface IAutoPatch : IPatch
    {
        public Boolean Auto();
    }

    internal interface IPatch : IPatchInfo, IDisposable
    {
        public Object SyncRoot { get; }
        public Boolean IsAutoInit { get; }
        
        public Boolean Apply();
        public Boolean Apply(Boolean force);
    }
    
    internal interface IPatchInfo
    {
        public String Name { get; }
        public ReflectionPatchCategory Category { get; }
        public ReflectionPatchState State { get; }
        public ReflectionPatchThrow IsThrow { get; }
    }
}