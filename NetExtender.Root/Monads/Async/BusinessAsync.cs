using System;
using System.Diagnostics;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using NetExtender.Exceptions;

namespace NetExtender.Types.Monads
{
    [AsyncMethodBuilder(typeof(BusinessAsyncBuilder<>))]
    public readonly struct BusinessAsync<T> : IStruct<BusinessAsync<T>>
    {
        public static implicit operator BusinessAsync<T>(Async<T> value)
        {
            return new BusinessAsync<T>(value);
        }

        public static implicit operator BusinessAsync<T>(Async<T, BusinessException> value)
        {
            return new BusinessAsync<T>(value);
        }

        public static implicit operator Async(BusinessAsync<T> value)
        {
            return value.Internal;
        }

        public static implicit operator Async<T>(BusinessAsync<T> value)
        {
            return value.Internal;
        }

        public static implicit operator Async<T, BusinessException>(BusinessAsync<T> value)
        {
            return value.Internal;
        }

        public static implicit operator BusinessAsync(BusinessAsync<T> value)
        {
            return new BusinessAsync(value);
        }

        public static implicit operator BusinessAsync<T>(T value)
        {
            return new BusinessAsync<T>(value);
        }

        public static implicit operator BusinessAsync<T>(Result<T, BusinessException> value)
        {
            return new BusinessAsync<T>(value);
        }

        public static implicit operator BusinessAsync<T>(BusinessResult<T> value)
        {
            return new BusinessAsync<T>((Result<T, BusinessException>) value);
        }

        public static implicit operator BusinessAsync<T>(Task<T>? value)
        {
            return new BusinessAsync<T>(value);
        }

        public static implicit operator BusinessAsync<T>(ValueTask<T> value)
        {
            return new BusinessAsync<T>(value);
        }

        public static implicit operator BusinessAsync<T>(Func<T>? value)
        {
            return new BusinessAsync<T>(value);
        }

        public static implicit operator BusinessAsync<T>(Func<Task<T>>? value)
        {
            return new BusinessAsync<T>(value);
        }

        public static implicit operator BusinessAsync<T>(Func<ValueTask<T>>? value)
        {
            return new BusinessAsync<T>(value);
        }

        public static implicit operator BusinessAsync<T>((IValueTaskSource<T> Source, Int16 Token) value)
        {
            return new BusinessAsync<T>(value);
        }

        public static implicit operator BusinessAsync<T>(Func<(IValueTaskSource<T> Source, Int16 Token)>? value)
        {
            return new BusinessAsync<T>(value);
        }

        [StackTraceHidden]
        public static implicit operator BusinessAsync<T>(BusinessException? value)
        {
            return new BusinessAsync<T>(value);
        }

        public static implicit operator BusinessAsync<T>(Func<BusinessException>? value)
        {
            return new BusinessAsync<T>(value);
        }

        private readonly Async<T, BusinessException> Internal;

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.IsEmpty;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal BusinessAsync(Async<T, BusinessException> value)
        {
            Internal = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BusinessAsyncAwaiter<T> GetAwaiter()
        {
            return new BusinessAsyncAwaiter<T>(Internal.GetAwaiter());
        }
    }

    public readonly struct BusinessAsyncAwaiter<T> : ICriticalNotifyCompletion
    {
        private readonly AsyncAwaiter<T, BusinessException> _awaiter;

        public Boolean IsCompleted
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _awaiter.IsCompleted;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal BusinessAsyncAwaiter(AsyncAwaiter<T, BusinessException> awaiter)
        {
            _awaiter = awaiter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BusinessResult<T> GetResult()
        {
            return _awaiter.GetResult();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnCompleted(Action continuation)
        {
            _awaiter.OnCompleted(continuation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UnsafeOnCompleted(Action continuation)
        {
            _awaiter.UnsafeOnCompleted(continuation);
        }
    }

    public struct BusinessAsyncBuilder<T>
    {
        private AsyncBuilder<T, BusinessException> _builder;

        public BusinessAsync<T> Task
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new BusinessAsync<T>(_builder.Task);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessAsyncBuilder<T> Create()
        {
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Start<TStateMachine>(ref TStateMachine state) where TStateMachine : IAsyncStateMachine
        {
            _builder.Start(ref state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetStateMachine(IAsyncStateMachine state)
        {
            _builder.SetStateMachine(state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetResult(T result)
        {
            _builder.SetResult(result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetException(Exception exception)
        {
            _builder.SetException(exception);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine state) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            _builder.AwaitOnCompleted(ref awaiter, ref state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine state) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            _builder.AwaitUnsafeOnCompleted(ref awaiter, ref state);
        }
    }

    [AsyncMethodBuilder(typeof(BusinessAsyncBuilder))]
    public readonly struct BusinessAsync : IStruct<BusinessAsync>
    {
        public static implicit operator BusinessAsync(Async value)
        {
            return new BusinessAsync(value);
        }

        public static implicit operator Async(BusinessAsync value)
        {
            return value.Internal;
        }

        public static implicit operator BusinessAsync(Unit value)
        {
            return new BusinessAsync(value);
        }

        public static implicit operator BusinessAsync(BusinessResult value)
        {
            return new BusinessAsync(value);
        }

        public static implicit operator BusinessAsync(Task? value)
        {
            return new BusinessAsync(value);
        }

        public static implicit operator BusinessAsync(ValueTask value)
        {
            return new BusinessAsync(value);
        }

        public static implicit operator BusinessAsync(Action? value)
        {
            return new BusinessAsync(value);
        }

        public static implicit operator BusinessAsync(Func<Task>? value)
        {
            return new BusinessAsync(value);
        }

        public static implicit operator BusinessAsync(Func<ValueTask>? value)
        {
            return new BusinessAsync(value);
        }

        public static implicit operator BusinessAsync((IValueTaskSource Source, Int16 Token) value)
        {
            return new BusinessAsync(value);
        }

        public static implicit operator BusinessAsync(Func<(IValueTaskSource Source, Int16 Token)>? value)
        {
            return new BusinessAsync(value);
        }

        [StackTraceHidden]
        public static implicit operator BusinessAsync(BusinessException? value)
        {
            return new BusinessAsync(value);
        }

        public static implicit operator BusinessAsync(Func<BusinessException>? value)
        {
            return new BusinessAsync(value);
        }

        private readonly Async Internal;

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.IsEmpty;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal BusinessAsync(Async value)
        {
            Internal = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BusinessAsyncAwaiter GetAwaiter()
        {
            return new BusinessAsyncAwaiter(Internal.GetAwaiter());
        }
    }

    public readonly struct BusinessAsyncAwaiter : ICriticalNotifyCompletion
    {
        private readonly AsyncAwaiter _awaiter;

        public Boolean IsCompleted
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _awaiter.IsCompleted;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal BusinessAsyncAwaiter(AsyncAwaiter awaiter)
        {
            _awaiter = awaiter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BusinessResult GetResult()
        {
            Result<Unit> result = _awaiter.GetResult();

            if (result.Unwrap(out Unit value))
            {
                return value;
            }

            switch (result.Exception)
            {
                case null:
                    return default;
                case BusinessException exception:
                    return exception;
                case var exception:
                    ExceptionDispatchInfo.Capture(exception).Throw();
                    return default;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnCompleted(Action continuation)
        {
            _awaiter.OnCompleted(continuation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UnsafeOnCompleted(Action continuation)
        {
            _awaiter.UnsafeOnCompleted(continuation);
        }
    }

    public struct BusinessAsyncBuilder
    {
        private AsyncBuilder _builder;

        public BusinessAsync Task
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new BusinessAsync(_builder.Task);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessAsyncBuilder Create()
        {
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Start<TStateMachine>(ref TStateMachine state) where TStateMachine : IAsyncStateMachine
        {
            _builder.Start(ref state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetStateMachine(IAsyncStateMachine state)
        {
            _builder.SetStateMachine(state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetResult()
        {
            _builder.SetResult();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetException(Exception exception)
        {
            _builder.SetException(exception);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine state) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            _builder.AwaitOnCompleted(ref awaiter, ref state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine state) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            _builder.AwaitUnsafeOnCompleted(ref awaiter, ref state);
        }
    }
}