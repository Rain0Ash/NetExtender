// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;

namespace NetExtender.Types.Dispatchers.Interfaces
{
    public interface IDispatcher
    {
        public event EventHandler<DispatcherShutdownState> Shutdown;
        public Thread Thread { get; }
        public Boolean IsShutdown { get; }
        public DispatcherShutdownState ShutdownState { get; }
        public void Invoke(Action callback);
        public void Invoke(Action callback, DispatcherPriority priority);
        public void Invoke(Action callback, DispatcherPriority priority, CancellationToken token);
        public void Invoke(Action callback, DispatcherPriority priority, TimeSpan timeout, CancellationToken token);
        public TResult Invoke<TResult>(Func<TResult> callback);
        public TResult Invoke<TResult>(Func<TResult> callback, DispatcherPriority priority);
        public TResult Invoke<TResult>(Func<TResult> callback, DispatcherPriority priority, CancellationToken token);
        public TResult Invoke<TResult>(Func<TResult> callback, DispatcherPriority priority, TimeSpan timeout, CancellationToken token);
    }
}