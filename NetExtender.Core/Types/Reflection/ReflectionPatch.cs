using System;
using System.Runtime.Serialization;
using NetExtender.Patch;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Reflection
{
    public abstract class AutoReflectionPatch<TFactory> : AutoReflectionPatch<AutoReflectionPatch, TFactory> where TFactory : AutoReflectionPatch<TFactory>, new()
    {
    }

    public abstract class AutoReflectionPatch<T, TFactory> : ReflectionPatch<T, TFactory>, IAutoReflectionPatch, IAutoPatch where T : AutoReflectionPatch where TFactory : AutoReflectionPatch<T, TFactory>, new()
    {
        public static Boolean Auto()
        {
            return Instance.Value.Auto();
        }
        
        Boolean IAutoReflectionPatch.Auto()
        {
            return Auto();
        }
        
        Boolean IAutoPatch.Auto()
        {
            return Auto();
        }
    }

    public abstract class ReflectionPatch<TFactory> : ReflectionPatch<ReflectionPatch, TFactory> where TFactory : ReflectionPatch<TFactory>, new()
    {
    }
    
    public abstract class ReflectionPatch<T, TFactory> : IReflectionPatch, IPatch where T : ReflectionPatch where TFactory : ReflectionPatch<T, TFactory>, new()
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

        Object IPatch.SyncRoot
        {
            get
            {
                return SyncRoot;
            }
        }
        
        public static String Name
        {
            get
            {
                return Instance.Value.Name;
            }
        }
        
        String IReflectionPatchInfo.Name
        {
            get
            {
                return Name;
            }
        }
        
        String IPatchInfo.Name
        {
            get
            {
                return Name;
            }
        }
        
        public static ReflectionPatchCategory Category
        {
            get
            {
                return Instance.Value.Category;
            }
        }
        
        ReflectionPatchCategory IReflectionPatchInfo.Category
        {
            get
            {
                return Category;
            }
        }
        
        ReflectionPatchCategory IPatchInfo.Category
        {
            get
            {
                return Category;
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
        
        ReflectionPatchState IPatchInfo.State
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
        
        ReflectionPatchThrow IPatchInfo.IsThrow
        {
            get
            {
                return IsThrow;
            }
        }
        
        public static Boolean IsAutoInit
        {
            get
            {
                return Instance.Value.IsAutoInit;
            }
        }
        
        Boolean IReflectionPatch.IsAutoInit
        {
            get
            {
                return IsAutoInit;
            }
        }
        
        Boolean IPatch.IsAutoInit
        {
            get
            {
                return IsAutoInit;
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
        
        Boolean IPatch.Apply()
        {
            return Apply();
        }
        
        public static Boolean Apply(Boolean force)
        {
            return Instance.Value.Apply(force);
        }
        
        Boolean IReflectionPatch.Apply(Boolean force)
        {
            return Apply(force);
        }
        
        Boolean IPatch.Apply(Boolean force)
        {
            return Apply(force);
        }
        
        public static Exception? Require()
        {
            return Instance.Value.Require();
        }
        
        Exception? IReflectionPatch.Require()
        {
            return Require();
        }
        
        protected abstract T Create();

        public override Int32 GetHashCode()
        {
            return Instance.Value.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Instance.Value.Equals(other);
        }

        public sealed override String ToString()
        {
            return Instance.Value.ToString();
        }

        public static void Dispose()
        {
            if (Instance.IsValueCreated)
            {
                Instance.Value.Dispose();
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                Dispose();
            }
        }

        ~ReflectionPatch()
        {
            Dispose(false);
        }
    }

    public abstract class AutoReflectionPatch : ReflectionPatch, IAutoReflectionPatch, IAutoPatch
    {
        public virtual Boolean Auto()
        {
            return Apply(false);
        }
    }

    public abstract class ReflectionPatch : IReflectionPatch, IPatch
    {
        public Object SyncRoot { get; } = ConcurrentUtilities.SyncRoot;

        public virtual String Name
        {
            get
            {
                return GetName(this);
            }
        }
        
        public abstract ReflectionPatchCategory Category { get; }
        
        public Boolean IsCapability
        {
            get
            {
                return Category.HasFlag(ReflectionPatchCategory.Capability);
            }
        }
        
        public Boolean IsSpecial
        {
            get
            {
                return Category.HasFlag(ReflectionPatchCategory.Special);
            }
        }
        
        public Boolean IsAphilargyria
        {
            get
            {
                return Category.HasFlag(ReflectionPatchCategory.Aphilargyria);
            }
        }
        
        public abstract ReflectionPatchState State { get; protected set; }
        
        public virtual ReflectionPatchThrow IsThrow
        {
            get
            {
                return ReflectionPatchThrow.Throw;
            }
        }
        
        public virtual Boolean IsAutoInit
        {
            get
            {
                return ReflectionPatchUtilities.IsAllowAutoInit(this);
            }
        }

        protected internal static String GetName(Type patch)
        {
            if (patch is null)
            {
                throw new ArgumentNullException(nameof(patch));
            }

            return patch.Name;
        }

        protected internal static String GetName(IReflectionPatch patch)
        {
            if (patch is null)
            {
                throw new ArgumentNullException(nameof(patch));
            }

            return GetName(patch.GetType());
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
            return Apply(true);
        }
        
        public Boolean Apply(Boolean force)
        {
            lock (SyncRoot)
            {
                try
                {
                    if (force)
                    {
                        State = Make();
                    }
                    else if (State is default(ReflectionPatchState))
                    {
                        State = IsAutoInit ? Make() : ReflectionPatchState.NotRequired;
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
                finally
                {
                    Dispose();
                }

                return State is ReflectionPatchState.Apply or ReflectionPatchState.NotRequired;
            }
        }

        public Exception? Require()
        {
            return ReflectionPatchUtilities.Require(this);
        }

        protected abstract ReflectionPatchState Make();
        
        public sealed override String ToString()
        {
            return Name;
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected abstract void Dispose(Boolean disposing);
        
        ~ReflectionPatch()
        {
            Dispose(false);
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
        
        [Serializable]
        protected class ReflectionPatchNoModuleException : ReflectionOperationException
        {
            public ReflectionPatchNoModuleException()
            {
            }
            
            public ReflectionPatchNoModuleException(String? message)
                : base(message)
            {
            }
            
            public ReflectionPatchNoModuleException(String? message, Exception? exception)
                : base(message, exception)
            {
            }
            
            protected ReflectionPatchNoModuleException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
    }
}