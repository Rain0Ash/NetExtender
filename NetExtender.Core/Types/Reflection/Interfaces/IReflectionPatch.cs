// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Patch;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Reflection.Interfaces
{
    public interface IAutoReflectionPatch : IReflectionPatch
    {
        public Boolean Auto();
    }

    public interface IReflectionPatch : IReflectionPatchInfo, IDisposable
    {
        internal new IPatch Patch
        {
            get
            {
                return this as IPatch ?? new ReflectionPatchWrapper(this);
            }
        }
        
        public SyncRoot SyncRoot { get; }
        public Boolean IsAutoInit { get; }
        
        public Boolean Apply();
        public Boolean Apply(Boolean force);
        public Exception? Require();
        
        private sealed class ReflectionPatchWrapper : IReflectionPatch, IPatch
        {
            private IReflectionPatch Patch { get; }
            
            IPatch IReflectionPatch.Patch
            {
                get
                {
                    return this;
                }
            }
            
            public SyncRoot SyncRoot
            {
                get
                {
                    return Patch.SyncRoot;
                }
            }
            
            Object IPatch.SyncRoot
            {
                get
                {
                    return SyncRoot;
                }
            }

            public String Name
            {
                get
                {
                    return Patch.Name;
                }
            }
            
            public ReflectionPatchCategory Category
            {
                get
                {
                    return Patch.Category;
                }
            }
            
            public ReflectionPatchState State
            {
                get
                {
                    return Patch.State;
                }
            }
            
            public ReflectionPatchThrow IsThrow
            {
                get
                {
                    return Patch.IsThrow;
                }
            }
            
            public Boolean IsAutoInit
            {
                get
                {
                    return Patch.IsAutoInit;
                }
            }
            
            public ReflectionPatchWrapper(IReflectionPatch patch)
            {
                Patch = patch ?? throw new ArgumentNullException(nameof(patch));
            }
            
            public Boolean Apply()
            {
                return Patch.Apply();
            }
            
            public Boolean Apply(Boolean force)
            {
                return Patch.Apply(force);
            }
            
            public Exception? Require()
            {
                return Patch.Require();
            }
            
            public override Int32 GetHashCode()
            {
                return Patch.GetHashCode();
            }
            
            public override Boolean Equals(Object? other)
            {
                return Patch.Equals(other);
            }
            
            public override String? ToString()
            {
                return Patch.ToString();
            }
            
            public void Dispose()
            {
                Patch.Dispose();
            }
        }
    }

    public interface IReflectionPatchInfo
    {
        internal IPatchInfo Patch
        {
            get
            {
                return this as IPatchInfo ?? new ReflectionPatchInfoWrapper(this);
            }
        }
        
        public String Name { get; }
        public ReflectionPatchCategory Category { get; }
        public ReflectionPatchState State { get; }
        public ReflectionPatchThrow IsThrow { get; }
        
        private sealed class ReflectionPatchInfoWrapper : IReflectionPatchInfo, IPatchInfo
        {
            private IReflectionPatchInfo Patch { get; }
            
            IPatchInfo IReflectionPatchInfo.Patch
            {
                get
                {
                    return this;
                }
            }

            public String Name
            {
                get
                {
                    return Patch.Name;
                }
            }
            
            public ReflectionPatchCategory Category
            {
                get
                {
                    return Patch.Category;
                }
            }
            
            public ReflectionPatchState State
            {
                get
                {
                    return Patch.State;
                }
            }
            
            public ReflectionPatchThrow IsThrow
            {
                get
                {
                    return Patch.IsThrow;
                }
            }
            
            public ReflectionPatchInfoWrapper(IReflectionPatchInfo patch)
            {
                Patch = patch ?? throw new ArgumentNullException(nameof(patch));
            }
            
            public override Int32 GetHashCode()
            {
                return Patch.GetHashCode();
            }
            
            public override Boolean Equals(Object? other)
            {
                return Patch.Equals(other);
            }
            
            public override String? ToString()
            {
                return Patch.ToString();
            }
        }
    }
}