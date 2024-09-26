using System;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Reflection
{
    public enum ReflectionPatchState : Byte
    {
        None,
        Apply,
        Failed
    }
    
    public abstract class ReflectionPatch<TFactory> : ReflectionPatch<ReflectionPatch, TFactory> where TFactory : ReflectionPatch<TFactory>, new()
    {
    }
    
    public abstract class ReflectionPatch<T, TFactory> : IReflectionPatch where T : ReflectionPatch where TFactory : ReflectionPatch<T, TFactory>, new()
    {
        protected static Lazy<T> Instance { get; } = new Lazy<T>(static () =>
        {
            T result = new TFactory().Create();
            result.Initialize(true);
            return result;
        });

        public static Object SyncRoot
        {
            get
            {
                return Instance.Value.SyncRoot;
            }
        }

        Object IReflectionPatch.SyncRoot
        {
            get
            {
                return SyncRoot;
            }
        }
        
        public static ReflectionPatchState State
        {
            get
            {
                return Instance.Value.State;
            }
        }
        
        ReflectionPatchState IReflectionPatch.State
        {
            get
            {
                return State;
            }
        }
        
        public static Boolean IsThrow
        {
            get
            {
                return Instance.Value.IsThrow;
            }
        }
        
        Boolean IReflectionPatch.IsThrow
        {
            get
            {
                return IsThrow;
            }
        }
        
        public static Boolean Apply()
        {
            return Instance.Value.Apply();
        }
        
        Boolean IReflectionPatch.Apply()
        {
            return Apply();
        }
        
        protected abstract T Create();
    }
    
    public abstract class ReflectionPatch : IReflectionPatch
    {
        public Object SyncRoot { get; } = ConcurrentUtilities.SyncRoot;
        public abstract ReflectionPatchState State { get; protected set; }
        
        public virtual Boolean IsThrow
        {
            get
            {
                return true;
            }
        }
        
        internal void Initialize(Boolean factory)
        {
            lock (SyncRoot)
            {
                try
                {
                    if (State is default(ReflectionPatchState) && !Initialize())
                    {
                        State = ReflectionPatchState.Failed;
                    }
                }
                catch (Exception)
                {
                    State = ReflectionPatchState.Failed;
                    
                    if (IsThrow)
                    {
                        throw;
                    }
                }
            }
        }
        
        protected virtual Boolean Initialize()
        {
            return true;
        }
        
        public Boolean Apply()
        {
            lock (SyncRoot)
            {
                try
                {
                    if (State is default(ReflectionPatchState))
                    {
                        State = Make();
                    }
                }
                catch (Exception)
                {
                    State = ReflectionPatchState.Failed;
                    
                    if (IsThrow)
                    {
                        throw;
                    }
                }

                return State == ReflectionPatchState.Apply;
            }
        }

        protected abstract ReflectionPatchState Make();
    }
}