using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Monads
{
    public readonly partial struct Async<T>
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        internal readonly struct Container
        {
            private static readonly Func<ValueTask<T>, T> GetResult = typeof(ValueTask<T>).GetField("_result", BindingFlags.Instance | BindingFlags.NonPublic)?.CreateGetDelegate<Func<ValueTask<T>, T>>() ?? throw new MissingMemberException($"{nameof(ValueTask<T>)}._result");
            private static readonly Func<ValueTask<T>, Object?> GetSource = typeof(ValueTask<T>).GetField("_obj", BindingFlags.Instance | BindingFlags.NonPublic)?.CreateGetDelegate<Func<ValueTask<T>, Object?>>() ?? throw new MissingMemberException($"{nameof(ValueTask<T>)}._obj");
            private static readonly Func<Object?, T?, Int16, Boolean, ValueTask<T>> New = TypeUtilities.New<ValueTask<T>, Object?, T?, Int16, Boolean>();

            private readonly State State;
            public readonly ValueTask<T> Single = default;

            public Object? Source
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return GetSource(Single);
                }
            }

            public Boolean HasResult
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Source is null && Result is not null && Single != default;
                }
            }

            public T Result
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return GetResult(Single);
                }
            }

            public State Type
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return State & ~State.Single;
                }
            }

            public Boolean IsSingleUse
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return (State & State.Single) is not State.None;
                }
            }

            public Boolean IsEmpty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return State is State.None;
                }
            }

            public Container(State state, Object? source)
            {
                State = state;
                Single = New.Invoke(source, default, default, default);
            }

            public Container(T result)
                : this(State.Result, result)
            {
            }

            public Container(State state, T result)
            {
                State = state;
                Single = New.Invoke(default, result, default, default);
            }

            public Container(Exception? exception)
            {
                Single = New.Invoke(exception, default, default, default);
                State = exception is not null ? State.Exception : State.None;
            }

            public Container(ValueTask<T> result)
                : this(State.ValueTask, result)
            {
            }

            public Container(State state, ValueTask<T> result)
            {
                State = state;
                Single = result;
            }

            public override Int32 GetHashCode()
            {
                return Single.GetHashCode();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Single(Func<T> value)
        {
            return new Container(State.FuncResult | State.Single, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Single(Func<Task<T>> value)
        {
            return new Container(State.FuncTask | State.Single, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Single(ValueTask<T> value)
        {
            return new Container(State.ValueTask | State.Single, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Single(Func<Exception> value)
        {
            return new Container(State.FuncException | State.Single, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Single(Func<ValueTask<T>> value)
        {
            return new Container(State.FuncValueTask | State.Single, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Single(Func<(IValueTaskSource<T> Source, Int16 Token)> value)
        {
            return new Container(State.FuncValueTask | State.Single, () =>
            {
                (IValueTaskSource<T> source, Int16 token) = value();
                return new ValueTask<T>(source, token);
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Multi(Func<T> value)
        {
            return new Container(State.FuncResult, new Lazy<T>(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Multi(Func<T> value, LazyThreadSafetyMode mode)
        {
            return new Container(State.FuncResult, new Lazy<T>(value, mode));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Multi(Task<T> value)
        {
            return new Container(State.Task, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Multi(Func<Task<T>> value)
        {
            return new Container(State.FuncTask, new Lazy<Task<T>>(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Multi(ValueTask<T> value)
        {
            return new Container(State.Task, value.AsTask());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Multi(Func<ValueTask<T>> value)
        {
            return new Container(State.FuncTask, new Lazy<Task<T>>(() => value().AsTask()));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Multi(Func<Exception> value)
        {
            return new Container(State.FuncException, new Lazy<Exception>(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Multi(Func<(IValueTaskSource<T> Source, Int16 Token)> value)
        {
            return new Container(State.FuncTask, new Lazy<Task<T>>(() =>
            {
                (IValueTaskSource<T> source, Int16 token) = value();
                return new ValueTask<T>(source, token).AsTask();
            }));
        }

        [Flags]
        internal enum State : Byte
        {
            None = Async.State.None,
            Single = Async.State.Single,
            Result = Async.State.Successful,
            Func = Async.State.Func,
            FuncResult = Result | Func,
            Exception = Async.State.Exception,
            FuncException = Exception | Func,
            Task = Async.State.Task,
            ValueTask = Async.State.ValueTask,
            FuncTask = Task | Func,
            FuncValueTask = ValueTask | Func
        }
    }

    public readonly partial struct Async
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        internal readonly struct Container
        {
            private static readonly Func<ValueTask, Object?> GetSource = typeof(ValueTask).GetField("_obj", BindingFlags.Instance | BindingFlags.NonPublic)?.CreateGetDelegate<Func<ValueTask, Object?>>() ?? throw new MissingMemberException($"{nameof(ValueTask)}._obj");
            private static readonly Func<Object?, Int16, Boolean, ValueTask> New = TypeUtilities.New<ValueTask, Object?, Int16, Boolean>();

            private readonly State State;
            public readonly ValueTask Single = default;

            public Object? Source
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return GetSource(Single);
                }
            }

            public State Type
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return State & ~State.Single switch
                    {
                        State.AsyncBox when Source is IAsync source => source.Type,
                        var state => state
                    };
                }
            }

            public Boolean IsSingleUse
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return (State & State.Single) is not State.None;
                }
            }

            public Boolean IsEmpty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return State is State.None;
                }
            }

            public Container(State state, Object? source)
            {
                State = state;
                Single = New.Invoke(source, default, default);
            }

            public Container(Unit result)
                : this(State.Successful, result)
            {
            }

            [SuppressMessage("ReSharper", "UnusedParameter.Local")]
            public Container(State state, Unit result)
            {
                State = state;
                Single = New.Invoke(default, default, default);
            }

            public Container(Exception? exception)
            {
                Single = New.Invoke(exception, default, default);
                State = exception is not null ? State.Exception : State.Successful;
            }

            public Container(ValueTask result)
                : this(State.ValueTask, result)
            {
            }

            public Container(State state, ValueTask result)
            {
                State = state;
                Single = result;
            }

            private static Boolean TryConvert<T>(Object? source, State single, out Container container)
            {
                container = source switch
                {
                    Task @this => new Container(State.Task | single, @this),
                    Exception @this => new Container(@this),
                    Func<Exception> @this => Single(@this),
                    Action @this => Single(@this),
                    Func<Task> @this => Single(@this),
                    Func<T> @this => Single(() => @this()),
                    Func<Object?> @this => Single(@this),
                    Lazy<Task> @this => new Container(State.FuncTask | single, @this),
                    _ => default
                };

                return !container.IsEmpty;
            }

            public static Boolean TryConvert<T>(Async<T>.Container value, out Container container)
            {
                State single = value.IsSingleUse ? State.Single : State.None;

                container = value.Source switch
                {
                    _ when value.HasResult => new Container(Unit.Default),
                    ValueTask @this => new Container((State) value.Type, @this),
                    { } source when TryConvert<T>(source, single, out container) => container,
                    _ => default
                };

                return !container.IsEmpty;
            }

            public static Container Convert<T>(IAsync value)
            {
                while (value.Source is IAsync inner)
                {
                    value = inner;
                }

                State single = value.IsSingleUse ? State.Single : State.None;

                return value.Source switch
                {
                    _ when value.HasResult => new Container(Unit.Default),
                    null => default,
                    ValueTask @this => new Container(value.Type, @this),
                    var source when TryConvert<T>(source, single, out Container container) => container,
                    _ => new Container(State.AsyncBox | single, value)
                };
            }

            public override Int32 GetHashCode()
            {
                return Single.GetHashCode();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Single(Action value)
        {
            return new Container(State.Action | State.Single, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Single(Func<Object?> value)
        {
            void Action()
            {
                value();
            }

            return Single(Action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Single(Func<Task> value)
        {
            return new Container(State.FuncTask | State.Single, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Single(ValueTask value)
        {
            return new Container(State.ValueTask | State.Single, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Single(Func<Exception> value)
        {
            return new Container(State.FuncException | State.Single, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Single(Func<ValueTask> value)
        {
            return new Container(State.FuncValueTask | State.Single, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Single(Func<(IValueTaskSource Source, Int16 Token)> value)
        {
            return new Container(State.FuncValueTask | State.Single, () =>
            {
                (IValueTaskSource source, Int16 token) = value();
                return new ValueTask(source, token);
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Multi(Action value)
        {
            return new Container(State.Action, new Lazy<Unit>(() => { value(); return Unit.Default; }));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Multi(Action value, LazyThreadSafetyMode mode)
        {
            return new Container(State.Action, new Lazy<Unit>(() => { value(); return Unit.Default; }, mode));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Multi(Task value)
        {
            return new Container(State.Task, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Multi(Func<Task> value)
        {
            return new Container(State.FuncTask, new Lazy<Task>(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Multi(ValueTask value)
        {
            return new Container(State.Task, value.AsTask());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Multi(Func<ValueTask> value)
        {
            return new Container(State.FuncTask, new Lazy<Task>(() => value().AsTask()));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Multi(Func<Exception> value)
        {
            return new Container(State.FuncException, new Lazy<Exception>(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Container Multi(Func<(IValueTaskSource Source, Int16 Token)> value)
        {
            return new Container(State.FuncTask, new Lazy<Task>(() =>
            {
                (IValueTaskSource source, Int16 token) = value();
                return new ValueTask(source, token).AsTask();
            }));
        }

        [Flags]
        internal enum State : Byte
        {
            None = 0,
            Single = 1,
            Successful = 2,
            Func = 4,
            Action = Successful | Func,
            Exception = 8,
            FuncException = Exception | Func,
            Task = 16,
            ValueTask = 32,
            FuncTask = Task | Func,
            FuncValueTask = ValueTask | Func,
            AsyncBox = Byte.MaxValue & ~Single
        }
    }
}