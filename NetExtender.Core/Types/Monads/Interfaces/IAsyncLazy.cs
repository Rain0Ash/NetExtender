// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface IAsyncLazy<T> : ILazy<Task<T>>
    {
        public Boolean IsTaskCreated { get; }
        
        public TaskAwaiter<T> GetAwaiter();
    }
}