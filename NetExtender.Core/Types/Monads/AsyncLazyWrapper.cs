// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Monads.Interfaces;

namespace NetExtender.Types.Monads
{
    public class AsyncLazyWrapper<T> : IAsyncLazy<T>
    {
        private ILazy<Task<T>> Internal { get; }

        public Task<T> Value
        {
            get
            {
                return Internal.Value;
            }
        }

        public Boolean IsValueCreated
        {
            get
            {
                return IsTaskCreated && Value.IsCompleted;
            }
        }

        public Boolean IsTaskCreated
        {
            get
            {
                return Internal.IsValueCreated;
            }
        }

        public AsyncLazyWrapper(Lazy<Task<T>> lazy)
            : this((ILazy<Task<T>>) new LazyWrapper<Task<T>>(lazy ?? throw new ArgumentNullException(nameof(lazy))))
        {
        }

        public AsyncLazyWrapper(ILazy<Task<T>> lazy)
        {
            Internal = lazy ?? throw new ArgumentNullException(nameof(lazy));
        }

        public AsyncLazyWrapper(Func<Task<T>> valueFactory)
        {
            Internal = new LazyWrapper<Task<T>>(valueFactory ?? throw new ArgumentNullException(nameof(valueFactory)));
        }

        public AsyncLazyWrapper(Boolean isThreadSafe)
        {
            Internal = new LazyWrapper<Task<T>>(isThreadSafe);
        }

        public AsyncLazyWrapper(LazyThreadSafetyMode mode)
        {
            Internal = new LazyWrapper<Task<T>>(mode);
        }

        public AsyncLazyWrapper(Func<Task<T>> valueFactory, Boolean isThreadSafe)
        {
            Internal = new LazyWrapper<Task<T>>(valueFactory ?? throw new ArgumentNullException(nameof(valueFactory)), isThreadSafe);
        }

        public AsyncLazyWrapper(Func<Task<T>> valueFactory, LazyThreadSafetyMode mode)
        {
            Internal = new LazyWrapper<Task<T>>(valueFactory ?? throw new ArgumentNullException(nameof(valueFactory)), mode);
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return Value.GetAwaiter();
        }
    }
}