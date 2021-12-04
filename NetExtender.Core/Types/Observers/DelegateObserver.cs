// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Core.Types.Observers
{
    public sealed class DelegateObserver<T> : IObserver<T>
    {
        private Action? Complete { get; }
        private Action<T>? Next { get; }
        private Action<Exception>? Error { get; }

        public DelegateObserver(Action? complete, Action<T>? next, Action<Exception>? error)
        {
            Complete = complete;
            Next = next;
            Error = error;
        }
        
        public void OnCompleted()
        {
            Complete?.Invoke();
        }
        
        public void OnNext(T value)
        {
            Next?.Invoke(value);
        }

        public void OnError(Exception error)
        {
            Error?.Invoke(error);
        }
    }
}