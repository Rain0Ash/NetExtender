// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Windows.Threading;
using NetExtender.Types.Dispatchers.Interfaces;

namespace NetExtender.Types.Dispatchers
{
    public sealed class DispatcherWrapper : IDispatcher
    {
        public static implicit operator DispatcherWrapper?(Dispatcher? dispatcher)
        {
            return dispatcher is not null ? new DispatcherWrapper(dispatcher) : null;
        }
        
        public static implicit operator Dispatcher?(DispatcherWrapper? wrapper)
        {
            return wrapper?.Dispatcher;
        }

        private Dispatcher Dispatcher { get; }

        public event DispatcherShutdownStateEventHandler? Shutdown;
        
        public DispatcherWrapper(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            Dispatcher.ShutdownStarted += OnShutdownStarted;
            Dispatcher.ShutdownFinished += OnShutdownFinished;
        }

        private void OnShutdownStarted(Object? obj, EventArgs args)
        {
            Shutdown?.Invoke(obj, new DispatcherShutdownStateEventArgs(DispatcherShutdownState.Started));
        }
        
        private void OnShutdownFinished(Object? obj, EventArgs args)
        {
            Shutdown?.Invoke(obj, new DispatcherShutdownStateEventArgs(DispatcherShutdownState.Finished));
        }

        public Thread Thread
        {
            get
            {
                return Dispatcher.Thread;
            }
        }

        public Boolean IsShutdown
        {
            get
            {
                return Dispatcher.HasShutdownStarted || Dispatcher.HasShutdownFinished;
            }
        }

        public DispatcherShutdownState ShutdownState
        {
            get
            {
                return Dispatcher.HasShutdownFinished ? DispatcherShutdownState.Finished : Dispatcher.HasShutdownStarted ? DispatcherShutdownState.Started : DispatcherShutdownState.None;
            }
        }

        public void Invoke(Action callback)
        {
            Dispatcher.Invoke(callback);
        }

        public void Invoke(Action callback, DispatcherPriority priority)
        {
            Dispatcher.Invoke(callback, (System.Windows.Threading.DispatcherPriority) priority);
        }

        public void Invoke(Action callback, DispatcherPriority priority, CancellationToken token)
        {
            Dispatcher.Invoke(callback, (System.Windows.Threading.DispatcherPriority) priority, token);
        }

        public void Invoke(Action callback, DispatcherPriority priority, TimeSpan timeout, CancellationToken token)
        {
            Dispatcher.Invoke(callback, (System.Windows.Threading.DispatcherPriority) priority, token, timeout);
        }

        public T Invoke<T>(Func<T> callback)
        {
            return Dispatcher.Invoke(callback);
        }

        public T Invoke<T>(Func<T> callback, DispatcherPriority priority)
        {
            return Dispatcher.Invoke(callback, (System.Windows.Threading.DispatcherPriority) priority);
        }

        public T Invoke<T>(Func<T> callback, DispatcherPriority priority, CancellationToken token)
        {
            return Dispatcher.Invoke(callback, (System.Windows.Threading.DispatcherPriority) priority, token);
        }

        public T Invoke<T>(Func<T> callback, DispatcherPriority priority, TimeSpan timeout, CancellationToken token)
        {
            return Dispatcher.Invoke(callback, (System.Windows.Threading.DispatcherPriority) priority, token, timeout);
        }
    }
}