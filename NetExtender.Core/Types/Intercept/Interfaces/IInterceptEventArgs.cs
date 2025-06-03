// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;

namespace NetExtender.Types.Intercept.Interfaces
{
    public interface ISimpleInterceptEventArgs<T> : ISimpleInterceptEventArgs
    {
        public new T Value { get; set; }
        public void Intercept(T value);
    }
    
    public interface ISimpleInterceptEventArgs : IInterceptEventArgs
    {
        public Object? Value { get; set; }
        public void Intercept(Exception exception);
    }

    public interface IInterceptEventArgs : IInterceptionEventArgs
    {
        public CancellationToken Token { get; }
        public Boolean? IsAsync { get; }
        public TimeSpan Wait { get; }
    }

    public interface IInterceptionEventArgs
    {
        public event EventHandler? Intercepted;
        
        public Exception? Exception { get; }
        public Boolean IsIntercept { get; }
        public Boolean IsCancel { get; }
    }
}