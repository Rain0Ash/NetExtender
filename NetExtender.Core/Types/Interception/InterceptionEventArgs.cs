using System;
using NetExtender.Types.Interception.Interfaces;

namespace NetExtender.Types.Interception
{
    public abstract class InterceptionEventArgs<T> : InterceptionEventArgs where T : IInterceptArgumentInfo
    {
        protected T Info { get; set; }

        protected InterceptionEventArgs(T value)
        {
            Info = value;
        }
    }
    
    public abstract class InterceptionEventArgs : EventArgs, IInterceptionEventArgs, IDisposable
    {
        public event EventHandler? Intercepted;

        private Exception? _exception = InterceptEventArgs.Default.Exception;
        public virtual Exception? Exception
        {
            get
            {
                return !ReferenceEquals(_exception, InterceptEventArgs.Default.Exception) ? _exception : null;
            }
            set
            {
                _exception = value;
            }
        }

        public Boolean IsIntercept { get; private set; }
        public abstract Boolean IsCancel { get; }

        protected virtual void Invoke()
        {
            Intercepted?.Invoke(this, this);
        }

        protected virtual void Intercept()
        {
            if (IsIntercept)
            {
                throw new InvalidOperationException();
            }
            
            IsIntercept = true;
            Clear();
        }

        protected virtual void Clear()
        {
            Exception = InterceptEventArgs.Default.Exception;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(Boolean disposing)
        {
            Intercepted = null;
        }

        ~InterceptionEventArgs()
        {
            Dispose(false);
        }
    }
}