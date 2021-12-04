// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;

namespace NetExtender.Types.Dispatchers.Interfaces
{
    public interface IDispatcher
    {
        public event DispatcherShutdownStateEventHandler Shutdown;
        public Thread Thread { get; }
        public Boolean IsShutdown { get; }
        public DispatcherShutdownState ShutdownState { get; }
        public void Invoke(Action callback);
        public void Invoke(Action callback, DispatcherPriority priority);
        public void Invoke(Action callback, DispatcherPriority priority, CancellationToken token);
        public void Invoke(Action callback, DispatcherPriority priority, TimeSpan timeout, CancellationToken token);
        public T Invoke<T>(Func<T> callback);
        public T Invoke<T>(Func<T> callback, DispatcherPriority priority);
        public T Invoke<T>(Func<T> callback, DispatcherPriority priority, CancellationToken token);
        public T Invoke<T>(Func<T> callback, DispatcherPriority priority, TimeSpan timeout, CancellationToken token);
    }
}