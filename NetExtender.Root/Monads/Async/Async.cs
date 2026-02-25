using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using NetExtender.Exceptions;
using NetExtender.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Monads
{
    [AsyncMethodBuilder(typeof(AsyncBuilder<,>))]
    public readonly struct Async<T, TException> : IStruct<Async<T, TException>> where TException : Exception
    {
        public static implicit operator Async(Async<T, TException> value)
        {
            return value.Internal;
        }

        public static implicit operator Async<T, TException>(Async<T> value)
        {
            return new Async<T, TException>(value);
        }

        public static implicit operator Async<T>(Async<T, TException> value)
        {
            return value.Internal;
        }

        public static implicit operator Async<T, TException>(T value)
        {
            return new Async<T, TException>(value);
        }

        public static implicit operator Async<T, TException>(Result<T, TException> value)
        {
            return new Async<T, TException>((Result<T>) value);
        }

        public static implicit operator Async<T, TException>(Task<T>? value)
        {
            return new Async<T, TException>(value);
        }

        public static implicit operator Async<T, TException>(ValueTask<T> value)
        {
            return new Async<T, TException>(value);
        }

        public static implicit operator Async<T, TException>(Func<T>? value)
        {
            return new Async<T, TException>(value);
        }

        public static implicit operator Async<T, TException>(Func<Task<T>>? value)
        {
            return new Async<T, TException>(value);
        }

        public static implicit operator Async<T, TException>(Func<ValueTask<T>>? value)
        {
            return new Async<T, TException>(value);
        }

        public static implicit operator Async<T, TException>((IValueTaskSource<T> Source, Int16 Token) value)
        {
            return new Async<T, TException>(value);
        }

        public static implicit operator Async<T, TException>(Func<(IValueTaskSource<T> Source, Int16 Token)>? value)
        {
            return new Async<T, TException>(value);
        }

        [StackTraceHidden]
        public static implicit operator Async<T, TException>(Exception? value)
        {
            return new Async<T, TException>(value);
        }

        public static implicit operator Async<T, TException>(Func<Exception>? value)
        {
            return new Async<T, TException>(value);
        }

        private readonly Async<T> Internal;

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.IsEmpty;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Async(Async<T> value)
        {
            Internal = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncAwaiter<T, TException> GetAwaiter()
        {
            return new AsyncAwaiter<T, TException>(Internal.GetAwaiter());
        }
    }

    public readonly struct AsyncAwaiter<T, TException> : ICriticalNotifyCompletion where TException : Exception
    {
        private readonly AsyncAwaiter<T> _awaiter;

        public Boolean IsCompleted
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _awaiter.IsCompleted;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal AsyncAwaiter(AsyncAwaiter<T> awaiter)
        {
            _awaiter = awaiter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<T, TException> GetResult()
        {
            Result<T> result = _awaiter.GetResult();

            if (result.Unwrap(out T? value))
            {
                return value;
            }

            switch (result.Exception)
            {
                case null:
                    return default;
                case TException exception:
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

    public struct AsyncBuilder<T, TException> where TException : Exception
    {
        private AsyncBuilder<T> _builder;

        public Async<T, TException> Task
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new Async<T, TException>(_builder.Task);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncBuilder<T, TException> Create()
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

    [AsyncMethodBuilder(typeof(AsyncBuilder<>))]
    public readonly partial struct Async<T> : IStruct<Async<T>>, Async<T>.IAsync
    {
        internal interface IAsync : Async.IAsync
        {
            public new State Type { get; }
        }

        public static implicit operator Async(Async<T> value)
        {
            return !value.IsEmpty ? new Async(Async.Container.Convert<T>(value)) : default;
        }

        public static implicit operator Async<T>(T value)
        {
            return new Async<T>(value);
        }

        public static implicit operator Async<T>(Result<T> value)
        {
            return new Async<T>(value);
        }

        public static implicit operator Async<T>(Result<T, Exception> value)
        {
            return new Async<T>((Result<T>) value);
        }

        public static implicit operator Async<T>(BusinessResult<T> value)
        {
            return new Async<T>((Result<T>) value);
        }

        public static implicit operator Async<T>(Task<T>? value)
        {
            return new Async<T>(value);
        }

        public static implicit operator Async<T>(ValueTask<T> value)
        {
            return new Async<T>(value);
        }

        public static implicit operator Async<T>(Func<T>? value)
        {
            return new Async<T>(value);
        }

        public static implicit operator Async<T>(Func<Task<T>>? value)
        {
            return new Async<T>(value);
        }

        public static implicit operator Async<T>(Func<ValueTask<T>>? value)
        {
            return new Async<T>(value);
        }

        public static implicit operator Async<T>((IValueTaskSource<T> Source, Int16 Token) value)
        {
            return new Async<T>(value.Source, value.Token);
        }

        public static implicit operator Async<T>(Func<(IValueTaskSource<T> Source, Int16 Token)>? value)
        {
            return new Async<T>(value);
        }

        [StackTraceHidden]
        public static implicit operator Async<T>(Exception? value)
        {
            return new Async<T>(value.Throwable());
        }

        public static implicit operator Async<T>(Func<Exception>? value)
        {
            return new Async<T>(value);
        }

        internal readonly Container Storage;

        internal State Type
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.Type;
            }
        }

        State IAsync.Type
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Type;
            }
        }

        Async.State Async.IAsync.Type
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return (Async.State) Type;
            }
        }

        Object? Async.IAsync.Source
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.Source;
            }
        }

        Boolean Async.IAsync.HasResult
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.HasResult;
            }
        }

        Object? Async.IAsync.Result
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.Result;
            }
        }

        public Boolean IsSingleUse
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.IsSingleUse;
            }
        }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.IsEmpty;
            }
        }

        public Async(T value)
        {
            Storage = new Container(value);
        }

        public Async(Result<T> value)
        {
            Storage = value.Unwrap(out T? result) ? new Container(result) : new Container(value.Exception);
        }

        public Async(Func<T>? value)
            : this(value, Async.AllowMultiAwait)
        {
        }

        public Async(Func<T>? value, Boolean multi)
        {
            Storage = value is not null ? multi ? Multi(value) : Single(value) : default;
        }

        public Async(Func<T>? value, LazyThreadSafetyMode mode)
        {
            Storage = value is not null ? Multi(value, mode) : default;
        }

        public Async(Task<T>? value)
        {
            Storage = value is not null ? Multi(value) : default;
        }

        public Async(Func<Task<T>>? value)
            : this(value, Async.AllowMultiAwait)
        {
        }

        public Async(Func<Task<T>>? value, Boolean multi)
        {
            Storage = value is not null ? multi ? Multi(value) : Single(value) : default;
        }

        public Async(ValueTask<T> value)
            : this(value, Async.AllowMultiAwait)
        {
        }

        public Async(ValueTask<T> value, Boolean multi)
        {
            Storage = value.IsCompletedSuccessfully ? new Container(value.Result) : multi ? Multi(value) : Single(value);
        }

        public Async(Exception? value)
        {
            Storage = new Container(value);
        }

        public Async(Func<Exception>? value)
            : this(value, Async.AllowMultiAwait)
        {
        }

        public Async(Func<Exception>? value, Boolean multi)
        {
            Storage = value is not null ? multi ? Multi(value) : Single(value) : default;
        }

        public Async(Func<ValueTask<T>>? value)
            : this(value, Async.AllowMultiAwait)
        {
        }

        public Async(Func<ValueTask<T>>? value, Boolean multi)
        {
            Storage = value is not null ? multi ? Multi(value) : Single(value) : default;
        }

        public Async(IValueTaskSource<T>? source, Int16 token)
            : this(source, token, Async.AllowMultiAwait)
        {
        }

        public Async(IValueTaskSource<T>? source, Int16 token, Boolean multi)
        {
            if (source is null)
            {
                Storage = default;
                return;
            }

            ValueTask<T> value = new ValueTask<T>(source, token);

            if (value.IsCompletedSuccessfully)
            {
                Storage = new Container(value.Result);
                return;
            }

            Storage = multi ? Multi(value) : Single(value);
        }

        public Async(Func<(IValueTaskSource<T> Source, Int16 Token)>? value)
            : this(value, Async.AllowMultiAwait)
        {
        }

        public Async(Func<(IValueTaskSource<T> Source, Int16 Token)>? value, Boolean multi)
        {
            Storage = value is not null ? multi ? Multi(value) : Single(value) : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncAwaiter<T> GetAwaiter()
        {
            return new AsyncAwaiter<T>(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        AsyncAwaiter.IAsyncAwaiter Async.IAsync.GetAwaiter()
        {
            return GetAwaiter();
        }
    }

    public readonly struct AsyncAwaiter<T> : AsyncAwaiter.IAsyncAwaiter
    {
        private readonly Async<T> _container;
        private readonly TaskAwaiter<T>? _awaiter;
        private readonly Boolean _multi;

        public Boolean IsCompleted
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _container.Type is Async<T>.State.None or Async<T>.State.Result or Async<T>.State.FuncResult or Async<T>.State.Exception or Async<T>.State.FuncException || (_awaiter?.IsCompleted ?? true);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal AsyncAwaiter(Async<T> container)
        {
            _container = container;
            _multi = !_container.IsSingleUse;

            _awaiter = _container.Type switch
            {
                Async<T>.State.None or Async<T>.State.Result or Async<T>.State.FuncResult or Async<T>.State.Exception or Async<T>.State.FuncException => default,
                Async<T>.State.Task => Unsafe.As<Task<T>?>(_container.Storage.Source)?.GetAwaiter(),
                Async<T>.State.FuncTask when _multi => Unsafe.As<Lazy<Task<T>?>>(_container.Storage.Source!).Value?.GetAwaiter(),
                Async<T>.State.ValueTask or Async<T>.State.FuncValueTask => default,
                var state => throw new EnumUndefinedOrNotSupportedException<Async<T>.State>(state, nameof(Async<T>.State), null)
            };
        }

        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        public Result<T> GetResult()
        {
            switch (_container.Type)
            {
                case Async<T>.State.None:
                {
                    return default;
                }
                case Async<T>.State.Result:
                {
                    return _container.Storage.Result;
                }
                case Async<T>.State.FuncResult:
                {
                    try
                    {
                        return _multi ? Unsafe.As<Lazy<T>?>(_container.Storage.Source!).Value : Unsafe.As<Func<T>?>(_container.Storage.Source!).Invoke();
                    }
                    catch (Exception exception)
                    {
                        return RayIdContext.Scope.Set(exception);
                    }
                }
                case Async<T>.State.Exception:
                {
                    return RayIdContext.Scope.Set(Unsafe.As<Exception?>(_container.Storage.Source!));
                }
                case Async<T>.State.FuncException:
                {
                    try
                    {
                        return RayIdContext.Scope.Set(_multi ? Unsafe.As<Lazy<Exception>?>(_container.Storage.Source!).Value : Unsafe.As<Func<Exception>?>(_container.Storage.Source!).Invoke());
                    }
                    catch (Exception exception)
                    {
                        return RayIdContext.Scope.Set(exception);
                    }
                }
                case Async<T>.State.ValueTask:
                {
                    try
                    {
                        return _container.Storage.Single.GetAwaiter().GetResult();
                    }
                    catch (Exception exception)
                    {
                        return RayIdContext.Scope.Set(exception);
                    }
                }
                case Async<T>.State.FuncTask when !_multi:
                {
                    try
                    {
                        return Unsafe.As<Func<Task<T>>?>(_container.Storage.Source!).Invoke().GetAwaiter().GetResult();
                    }
                    catch (Exception exception)
                    {
                        return RayIdContext.Scope.Set(exception);
                    }
                }
                case Async<T>.State.FuncValueTask:
                {
                    try
                    {
                        return Unsafe.As<Func<ValueTask<T>>?>(_container.Storage.Source!).Invoke().GetAwaiter().GetResult();
                    }
                    catch (Exception exception)
                    {
                        return RayIdContext.Scope.Set(exception);
                    }
                }
                default:
                {
                    return GetResultAsync();
                }
            }
        }

        Result<Unit> AsyncAwaiter.IAsyncAwaiter.GetResult()
        {
            Result<T> result = GetResult();
            return result.Unwrap(out _) ? new Result<Unit>(Unit.Default) : result.Exception is not null ? new Result<Unit>(result.Exception) : default;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private Result<T> GetResultAsync()
        {
            try
            {
                return _awaiter is { } awaiter ? awaiter.GetResult() : default(Result<T>);
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        public void OnCompleted(Action continuation)
        {
            if (IsCompleted)
            {
                Task.CompletedTask.GetAwaiter().OnCompleted(continuation);
                return;
            }

            _awaiter?.OnCompleted(continuation);
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            if (IsCompleted)
            {
                Task.CompletedTask.GetAwaiter().UnsafeOnCompleted(continuation);
                return;
            }

            _awaiter?.UnsafeOnCompleted(continuation);
        }
    }

    public struct AsyncBuilder<T>
    {
        private AsyncValueTaskMethodBuilder<T> _builder;
        private RayIdContext _context;

        public Async<T> Task
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new Async<T>(_builder.Task);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncBuilder<T> Create()
        {
            AsyncBuilder<T> builder = default;
            builder._context = RayIdContext.Scope.Context;
            return builder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Start<TStateMachine>(ref TStateMachine state) where TStateMachine : IAsyncStateMachine
        {
            RayIdContext previous = RayIdContext.Scope.Context;
            RayIdContext.Scope.Context = _context;

            try
            {
                _builder.Start(ref state);
            }
            finally
            {
                RayIdContext.Scope.Context = previous;
            }
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
            _builder.SetException(RayIdContext.Scope.Set(exception, _context));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine state) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            RayIdContext context = RayIdContext.Scope.Context;
            RayIdContext.Scope.Context = _context;

            try
            {
                _builder.AwaitOnCompleted(ref awaiter, ref state);
            }
            finally
            {
                RayIdContext.Scope.Context = context;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine state) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            RayIdContext context = RayIdContext.Scope.Context;
            RayIdContext.Scope.Context = _context;

            try
            {
                _builder.AwaitUnsafeOnCompleted(ref awaiter, ref state);
            }
            finally
            {
                RayIdContext.Scope.Context = context;
            }
        }
    }

    [AsyncMethodBuilder(typeof(AsyncBuilder))]
    public readonly partial struct Async : IStruct<Async>, Async.IAsync
    {
        internal interface IAsync : IStruct
        {
            public State Type { get; }
            public Object? Source { get; }
            public Boolean HasResult { get; }
            public Object? Result { get; }
            public Boolean IsSingleUse { get; }

            public AsyncAwaiter.IAsyncAwaiter GetAwaiter();
        }

        public static implicit operator Async(Unit value)
        {
            return new Async(value);
        }

        public static implicit operator Async(Result<Unit> value)
        {
            return new Async(value);
        }

        public static implicit operator Async(Result<Unit, Exception> value)
        {
            return new Async((Result<Unit>) value);
        }

        public static implicit operator Async(BusinessResult value)
        {
            return new Async((Result<Unit>) value);
        }

        public static implicit operator Async(BusinessResult<Unit> value)
        {
            return new Async((Result<Unit>) value);
        }

        public static implicit operator Async(Task? value)
        {
            return new Async(value);
        }

        public static implicit operator Async(ValueTask value)
        {
            return new Async(value);
        }

        public static implicit operator Async(Action? value)
        {
            return new Async(value);
        }

        public static implicit operator Async(Func<Task>? value)
        {
            return new Async(value);
        }

        public static implicit operator Async(Func<ValueTask>? value)
        {
            return new Async(value);
        }

        public static implicit operator Async((IValueTaskSource Source, Int16 Token) value)
        {
            return new Async(value.Source, value.Token);
        }

        public static implicit operator Async(Func<(IValueTaskSource Source, Int16 Token)>? value)
        {
            return new Async(value);
        }

        [StackTraceHidden]
        public static implicit operator Async(Exception? value)
        {
            return new Async(value.Throwable());
        }

        public static implicit operator Async(Func<Exception>? value)
        {
            return new Async(value);
        }

        public static Boolean AllowMultiAwait { get; set; }

        internal readonly Container Storage;

        internal State Type
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.Type;
            }
        }

        State IAsync.Type
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Type;
            }
        }

        Boolean IAsync.HasResult
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return false;
            }
        }

        Object? IAsync.Source
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.Source;
            }
        }

        Object? IAsync.Result
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return null;
            }
        }

        public Boolean IsSingleUse
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.IsSingleUse;
            }
        }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Storage.IsEmpty;
            }
        }

        public Async(Unit value)
        {
            Storage = new Container(value);
        }

        public Async(Result<Unit> value)
        {
            Storage = value.Unwrap(out Unit result) ? new Container(result) : new Container(value.Exception);
        }

        public Async(Action? value)
            : this(value, AllowMultiAwait)
        {
        }

        public Async(Action? value, Boolean multi)
        {
            Storage = value is not null ? multi ? Multi(value) : Single(value) : default;
        }

        public Async(Action? value, LazyThreadSafetyMode mode)
        {
            Storage = value is not null ? Multi(value, mode) : default;
        }

        public Async(Task? value)
        {
            Storage = value is not null ? Multi(value) : default;
        }

        public Async(Func<Task>? value)
            : this(value, AllowMultiAwait)
        {
        }

        public Async(Func<Task>? value, Boolean multi)
        {
            Storage = value is not null ? multi ? Multi(value) : Single(value) : default;
        }

        public Async(ValueTask value)
            : this(value, AllowMultiAwait)
        {
        }

        public Async(ValueTask value, Boolean multi)
        {
            Storage = value.IsCompletedSuccessfully ? new Container(Unit.Default) : multi ? Multi(value) : Single(value);
        }

        public Async(Exception? value)
        {
            Storage = new Container(value);
        }

        public Async(Func<Exception>? value)
            : this(value, AllowMultiAwait)
        {
        }

        public Async(Func<Exception>? value, Boolean multi)
        {
            Storage = value is not null ? multi ? Multi(value) : Single(value) : default;
        }

        public Async(Func<ValueTask>? value)
            : this(value, AllowMultiAwait)
        {
        }

        public Async(Func<ValueTask>? value, Boolean multi)
        {
            Storage = value is not null ? multi ? Multi(value) : Single(value) : default;
        }

        public Async(IValueTaskSource? source, Int16 token)
            : this(source, token, AllowMultiAwait)
        {
        }

        public Async(IValueTaskSource? source, Int16 token, Boolean multi)
        {
            if (source is null)
            {
                Storage = default;
                return;
            }

            ValueTask value = new ValueTask(source, token);

            if (value.IsCompletedSuccessfully)
            {
                Storage = new Container(Unit.Default);
                return;
            }

            Storage = multi ? Multi(value) : Single(value);
        }

        public Async(Func<(IValueTaskSource Source, Int16 Token)>? value)
            : this(value, AllowMultiAwait)
        {
        }

        public Async(Func<(IValueTaskSource Source, Int16 Token)>? value, Boolean multi)
        {
            Storage = value is not null ? multi ? Multi(value) : Single(value) : default;
        }

        internal Async(Container container)
        {
            Storage = container;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncAwaiter GetAwaiter()
        {
            return new AsyncAwaiter(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        AsyncAwaiter.IAsyncAwaiter IAsync.GetAwaiter()
        {
            return GetAwaiter();
        }
    }

    public readonly struct AsyncAwaiter : AsyncAwaiter.IAsyncAwaiter
    {
        internal interface IAsyncAwaiter : ICriticalNotifyCompletion
        {
            public Result<Unit> GetResult();
        }

        private readonly Async _container;
        private readonly TaskAwaiter? _awaiter;
        private readonly Boolean _multi;
        private readonly IAsyncAwaiter? _async = default;

        public Boolean IsCompleted
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _container.Type is Async.State.None or Async.State.Successful or Async.State.Action or Async.State.Exception or Async.State.FuncException || (_awaiter?.IsCompleted ?? true);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal AsyncAwaiter(Async container)
        {
            _container = container;
            _multi = !_container.IsSingleUse;

            _awaiter = _container.Type switch
            {
                Async.State.None or Async.State.Successful or Async.State.Action or Async.State.Exception or Async.State.FuncException => default,
                Async.State.Task => Unsafe.As<Task?>(_container.Storage.Source)?.GetAwaiter(),
                Async.State.FuncTask when _multi => Unsafe.As<Lazy<Task?>>(_container.Storage.Source!).Value?.GetAwaiter(),
                Async.State.ValueTask or Async.State.FuncValueTask => default,
                Async.State.AsyncBox => (_async = Unsafe.As<Async.IAsync>(_container.Storage.Source!).GetAwaiter(), Awaiter: default(TaskAwaiter?)).Awaiter,
                var state => throw new EnumUndefinedOrNotSupportedException<Async.State>(state, nameof(Async.State), null)
            };
        }

        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        public Result<Unit> GetResult()
        {
            switch (_container.Type)
            {
                case Async.State.None:
                {
                    return default;
                }
                case Async.State.Successful:
                {
                    return Unit.Default;
                }
                case Async.State.Action:
                {
                    try
                    {
                        if (_multi)
                        {
                            return Unsafe.As<Lazy<Unit>?>(_container.Storage.Source!).Value;
                        }

                        Unsafe.As<Action?>(_container.Storage.Source!).Invoke();
                        return Unit.Default;
                    }
                    catch (Exception exception)
                    {
                        return RayIdContext.Scope.Set(exception);
                    }
                }
                case Async.State.Exception:
                {
                    return RayIdContext.Scope.Set(Unsafe.As<Exception?>(_container.Storage.Source!));
                }
                case Async.State.FuncException:
                {
                    try
                    {
                        return RayIdContext.Scope.Set(_multi ? Unsafe.As<Lazy<Exception>?>(_container.Storage.Source!).Value : Unsafe.As<Func<Exception>?>(_container.Storage.Source!).Invoke());
                    }
                    catch (Exception exception)
                    {
                        return RayIdContext.Scope.Set(exception);
                    }
                }
                case Async.State.ValueTask:
                {
                    try
                    {
                        _container.Storage.Single.GetAwaiter().GetResult();
                        return Unit.Default;
                    }
                    catch (Exception exception)
                    {
                        return RayIdContext.Scope.Set(exception);
                    }
                }
                case Async.State.FuncTask when !_multi:
                {
                    try
                    {
                        Unsafe.As<Func<Task>?>(_container.Storage.Source!).Invoke().GetAwaiter().GetResult();
                        return Unit.Default;
                    }
                    catch (Exception exception)
                    {
                        return RayIdContext.Scope.Set(exception);
                    }
                }
                case Async.State.FuncValueTask:
                {
                    try
                    {
                        Unsafe.As<Func<ValueTask>?>(_container.Storage.Source!).Invoke().GetAwaiter().GetResult();
                        return Unit.Default;
                    }
                    catch (Exception exception)
                    {
                        return RayIdContext.Scope.Set(exception);
                    }
                }
                case Async.State.AsyncBox when _async is not null:
                {
                    return _async.GetResult();
                }
                default:
                {
                    return GetResultAsync();
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private Result<Unit> GetResultAsync()
        {
            try
            {
                if (_awaiter is not { } awaiter)
                {
                    return default;
                }

                awaiter.GetResult();
                return Unit.Default;
            }
            catch (Exception exception)
            {
                return RayIdContext.Scope.Set(exception);
            }
        }

        public void OnCompleted(Action continuation)
        {
            if (IsCompleted)
            {
                Task.CompletedTask.GetAwaiter().OnCompleted(continuation);
                return;
            }

            if (_async is not null)
            {
                _async.OnCompleted(continuation);
                return;
            }

            _awaiter?.OnCompleted(continuation);
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            if (IsCompleted)
            {
                Task.CompletedTask.GetAwaiter().UnsafeOnCompleted(continuation);
                return;
            }

            if (_async is not null)
            {
                _async.UnsafeOnCompleted(continuation);
                return;
            }

            _awaiter?.UnsafeOnCompleted(continuation);
        }
    }

    public struct AsyncBuilder
    {
        private AsyncValueTaskMethodBuilder _builder;
        private RayIdContext _context;

        public Async Task
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return new Async(_builder.Task);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncBuilder Create()
        {
            AsyncBuilder builder = default;
            builder._context = RayIdContext.Scope.Context;
            return builder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Start<TStateMachine>(ref TStateMachine state) where TStateMachine : IAsyncStateMachine
        {
            RayIdContext previous = RayIdContext.Scope.Context;
            RayIdContext.Scope.Context = _context;

            try
            {
                _builder.Start(ref state);
            }
            finally
            {
                RayIdContext.Scope.Context = previous;
            }
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
            _builder.SetException(RayIdContext.Scope.Set(exception, _context));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine state) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            RayIdContext context = RayIdContext.Scope.Context;
            RayIdContext.Scope.Context = _context;

            try
            {
                _builder.AwaitOnCompleted(ref awaiter, ref state);
            }
            finally
            {
                RayIdContext.Scope.Context = context;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine state) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            RayIdContext context = RayIdContext.Scope.Context;
            RayIdContext.Scope.Context = _context;

            try
            {
                _builder.AwaitUnsafeOnCompleted(ref awaiter, ref state);
            }
            finally
            {
                RayIdContext.Scope.Context = context;
            }
        }
    }
}