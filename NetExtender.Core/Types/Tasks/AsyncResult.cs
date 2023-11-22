// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NetExtender.Types.Tasks
{
    public readonly struct AsyncResult<T> : IEquatable<ValueTask<T>>, IEquatable<AsyncResult<T>>
    {
        public static implicit operator AsyncResult<T>(T value)
        {
            return new AsyncResult<T>(value);
        }

        public static implicit operator T(AsyncResult<T> value)
        {
            return value.Internal.Result;
        }

        public static implicit operator AsyncResult<T>(Task<T>? value)
        {
            return value is not null ? new AsyncResult<T>(value) : default;
        }

        public static implicit operator Task<T>(AsyncResult<T> value)
        {
            return value.Internal.AsTask();
        }

        public static implicit operator AsyncResult<T>(ValueTask<T> value)
        {
            return new AsyncResult<T>(value);
        }

        public static implicit operator ValueTask<T>(AsyncResult<T> value)
        {
            return value.Internal;
        }
        
        public static Boolean operator ==(AsyncResult<T> first, AsyncResult<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(AsyncResult<T> first, AsyncResult<T> second)
        {
            return !(first == second);
        }

        private ValueTask<T> Internal { get; }

        public Boolean IsCompleted
        {
            get
            {
                return Internal.IsCompleted;
            }
        }

        public Boolean IsCompletedSuccessfully
        {
            get
            {
                return Internal.IsCompletedSuccessfully;
            }
        }

        public Boolean IsCanceled
        {
            get
            {
                return Internal.IsCanceled;
            }
        }

        public Boolean IsFaulted
        {
            get
            {
                return Internal.IsFaulted;
            }
        }

        public AsyncResult(T value)
        {
            Internal = new ValueTask<T>(value);
        }

        public AsyncResult(Task<T> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Internal = new ValueTask<T>(value);
        }

        public AsyncResult(ValueTask<T> value)
        {
            Internal = value;
        }

        public T AsValue()
        {
            return this;
        }

        public Task<T> AsTask()
        {
            return this;
        }

        public ValueTask<T> AsValueTask()
        {
            return this;
        }
        
        public AsyncResult<T> Preserve()
        {
            return Internal.Preserve();
        }

        public ValueTaskAwaiter<T> GetAwaiter()
        {
            return Internal.GetAwaiter();
        }

        public ConfiguredValueTaskAwaitable<T> ConfigureAwait(Boolean continueOnCapturedContext)
        {
            return Internal.ConfigureAwait(continueOnCapturedContext);
        }
        
        public override Int32 GetHashCode()
        {
            return Internal.GetHashCode();
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                AsyncResult<T> result => Equals(result),
                ValueTask<T> result => Equals(result),
                _ => false
            };
        }

        public Boolean Equals(ValueTask<T> other)
        {
            return Internal.Equals(other);
        }

        public Boolean Equals(AsyncResult<T> other)
        {
            return Internal.Equals(other.Internal);
        }
    }
}