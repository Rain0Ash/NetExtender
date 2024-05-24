// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using NetExtender.Types.Monads.Interfaces;

namespace NetExtender.Types.Monads
{
    public sealed class DynamicLazy<T> : ILazy<T>
    {
        private ILazy<T>? Internal { get; set; }

        private T _value = default!;
        public T Value
        {
            get
            {
                return Internal is not null ? Internal.Value : _value;
            }
        }

        public Boolean IsValueCreated
        {
            get
            {
                return Internal?.IsValueCreated ?? true;
            }
        }

        public DynamicLazy(Lazy<T> lazy)
            : this((ILazy<T>) new LazyWrapper<T>(lazy ?? throw new ArgumentNullException(nameof(lazy))))
        {
        }

        public DynamicLazy(ILazy<T> lazy)
        {
            Internal = lazy ?? throw new ArgumentNullException(nameof(lazy));
        }

        public DynamicLazy(Func<T> valueFactory)
        {
            Internal = new LazyWrapper<T>(valueFactory ?? throw new ArgumentNullException(nameof(valueFactory)));
        }

        public DynamicLazy(Func<T> valueFactory, Boolean isThreadSafe)
        {
            Internal = new LazyWrapper<T>(valueFactory ?? throw new ArgumentNullException(nameof(valueFactory)), isThreadSafe);
        }

        public DynamicLazy(Func<T> valueFactory, LazyThreadSafetyMode mode)
        {
            Internal = new LazyWrapper<T>(valueFactory ?? throw new ArgumentNullException(nameof(valueFactory)), mode);
        }

        public DynamicLazy<T> Reset(T value)
        {
            _value = value;
            Internal = null;
            return this;
        }
    }
}