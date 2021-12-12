// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using NetExtender.Types.Monads.Interfaces;

namespace NetExtender.Types.Monads
{
    public sealed class LazyWrapper<T> : ILazy<T>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator LazyWrapper<T>?(Lazy<T>? value)
        {
            return value is not null ? new LazyWrapper<T>(value) : null;
        }
        
        [return: NotNullIfNotNull("wrapper")]
        public static implicit operator Lazy<T>?(LazyWrapper<T>? wrapper)
        {
            return wrapper?.Internal;
        }

        private Lazy<T> Internal { get; }

        public T Value
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
                return Internal.IsValueCreated;
            }
        }
        
        public LazyWrapper(Lazy<T> lazy)
        {
            Internal = lazy ?? throw new ArgumentNullException(nameof(lazy));
        }
        
        public LazyWrapper(Func<T> valueFactory)
        {
            Internal = new Lazy<T>(valueFactory ?? throw new ArgumentNullException(nameof(valueFactory)));
        }

        public LazyWrapper(Boolean isThreadSafe)
        {
            Internal = new Lazy<T>(isThreadSafe);
        }

        public LazyWrapper(LazyThreadSafetyMode mode)
        {
            Internal = new Lazy<T>(mode);
        }

        public LazyWrapper(Func<T> valueFactory, Boolean isThreadSafe)
        {
            Internal = new Lazy<T>(valueFactory ?? throw new ArgumentNullException(nameof(valueFactory)), isThreadSafe);
        }

        public LazyWrapper(Func<T> valueFactory, LazyThreadSafetyMode mode)
        {
            Internal = new Lazy<T>(valueFactory ?? throw new ArgumentNullException(nameof(valueFactory)), mode);
        }
    }
}