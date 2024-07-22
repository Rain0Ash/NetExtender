// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface IResettableLazy<T> : ILazy<T>
    {
        public IResettableLazy<T> Reset();
        public IResettableLazy<T> Reset(T value);
        public IResettableLazy<T> Reset(Func<T> factory);
    }
    
    public interface ILazy<out T>
    {
        public T Value { get; }
        public Boolean IsValueCreated { get; }
    }
}