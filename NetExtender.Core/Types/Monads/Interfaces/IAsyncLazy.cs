// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface IResettableAsyncLazy<T> : IAsyncLazy<T>, IResettableLazy<Task<T>>
    {
        public new IResettableLazy<T> Reset();
        public IResettableLazy<T> Reset(T value);
        public IResettableLazy<T> Reset(Func<T> factory);
    }
    
    public interface IAsyncLazy<T> : ILazy<Task<T>>
    {
        public Boolean IsTaskCreated { get; }

        public TaskAwaiter<T> GetAwaiter();
    }
}