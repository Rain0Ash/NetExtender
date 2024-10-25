using System;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Reflection
{
    public enum ReflectionPatchState : Byte
    {
        None,
        Apply,
        Failed
    }
    
    public enum ReflectionPatchThrow : Byte
    {
        Throw,
        Ignore,
        Log,
        LogThrow,
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
        
        ReflectionPatchState IReflectionPatchInfo.State
        {
            get
            {
                return State;
            }
        }
        
        public static ReflectionPatchThrow IsThrow
        {
            get
            {
                return Instance.Value.IsThrow;
            }
        }
        
        ReflectionPatchThrow IReflectionPatchInfo.IsThrow
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
        
        public virtual ReflectionPatchThrow IsThrow
        {
            get
            {
                return ReflectionPatchThrow.Throw;
            }
        }
        
        internal void Initialize(Boolean factory)
        {
            lock (SyncRoot)
            {
                try
                {
                    if (State is not default(ReflectionPatchState) || Initialize())
                    {
                        return;
                    }
                    
                    State = ReflectionPatchState.Failed;
                    ReflectionPatchUtilities.Set(this);
                }
                catch (Exception exception)
                {
                    State = ReflectionPatchState.Failed;
                    switch (IsThrow)
                    {
                        case ReflectionPatchThrow.Throw:
                            throw;
                        case ReflectionPatchThrow.Ignore:
                            break;
                        case ReflectionPatchThrow.Log:
                            ReflectionPatchUtilities.Set(this, exception);
                            break;
                        case ReflectionPatchThrow.LogThrow:
                            ReflectionPatchUtilities.Set(this, exception);
                            throw;
                        default:
                            throw new EnumUndefinedOrNotSupportedException<ReflectionPatchThrow>(IsThrow, nameof(IsThrow), null);
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
                    
                    ReflectionPatchUtilities.Set(this);
                }
                catch (Exception exception)
                {
                    State = ReflectionPatchState.Failed;
                    switch (IsThrow)
                    {
                        case ReflectionPatchThrow.Throw:
                            throw;
                        case ReflectionPatchThrow.Ignore:
                            break;
                        case ReflectionPatchThrow.Log:
                            ReflectionPatchUtilities.Set(this, exception);
                            break;
                        case ReflectionPatchThrow.LogThrow:
                            ReflectionPatchUtilities.Set(this, exception);
                            throw;
                        default:
                            throw new EnumUndefinedOrNotSupportedException<ReflectionPatchThrow>(IsThrow, nameof(IsThrow), null);
                    }
                }

                return State == ReflectionPatchState.Apply;
            }
        }

        protected abstract ReflectionPatchState Make();
        
        public override String ToString()
        {
            Type type = GetType();
            return type.FullName ?? type.Name;
        }
        
        [Serializable]
        protected class ReflectionPatchSignatureMissingException : ReflectionOperationException
        {
            public ReflectionPatchSignatureMissingException()
            {
            }
            
            public ReflectionPatchSignatureMissingException(String? message)
                : base(message)
            {
            }
            
            public ReflectionPatchSignatureMissingException(String? message, Exception? exception)
                : base(message, exception)
            {
            }
            
            protected ReflectionPatchSignatureMissingException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
    }
}