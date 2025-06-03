// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using NetExtender.Types.Intercept.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Intercept
{
    public class MethodInterceptEventArgs : MethodInterceptAbstractionEventArgs<MethodInterceptEventArgs.Information>
    {
        public readonly struct Information : IMemberInterceptArgumentInfo<MethodInfo>
        {
            public MethodInfo Member { get; }
            public ImmutableArray<Object?> Arguments { get; }
            public Exception? Exception { get; }

            public Information(MethodInfo method, IEnumerable<Object?>? arguments, Exception? exception)
                : this(method, arguments?.ToImmutableArray() ?? ImmutableArray<Object?>.Empty, exception)
            {
            }

            public Information(MethodInfo method, ImmutableArray<Object?> arguments, Exception? exception)
            {
                Member = method ?? throw new ArgumentNullException(nameof(method));
                Arguments = arguments;
                Exception = exception;
            }
        }
        
        public sealed override ImmutableArray<Object?> Arguments
        {
            get
            {
                return Info.Arguments;
            }
        }

        public MethodInterceptEventArgs(ValueDelegateUtilities.Info @delegate)
            : this(@delegate.Method ?? throw new ArgumentNullException(nameof(@delegate)), @delegate.Arguments)
        {
        }

        public MethodInterceptEventArgs(MethodInfo method, IEnumerable<Object?>? arguments)
            : this(new Information(method, arguments, null))
        {
        }

        public MethodInterceptEventArgs(MethodInfo method, ImmutableArray<Object?> arguments)
            : this(new Information(method, arguments, null))
        {
        }

        public MethodInterceptEventArgs(ValueDelegateUtilities.Info @delegate, Exception exception)
            : this(@delegate.Method ?? throw new ArgumentNullException(nameof(@delegate)), @delegate.Arguments, exception)
        {
        }

        public MethodInterceptEventArgs(MethodInfo method, IEnumerable<Object?>? arguments, Exception exception)
            : this(new Information(method, arguments, exception))
        {
        }

        public MethodInterceptEventArgs(MethodInfo method, ImmutableArray<Object?> arguments, Exception exception)
            : this(new Information(method, arguments, exception))
        {
        }

        protected MethodInterceptEventArgs(Information value)
            : base(value)
        {
        }

        protected internal override void Intercept(Exception exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            Intercept();
            Info = new Information(Info.Member, Info.Arguments, exception);
            Invoke();
        }

        protected override void Clear()
        {
            Unseal();
            base.Clear();
        }
    }
    
    public class MethodInterceptEventArgs<T> : MethodInterceptAbstractionEventArgs<MethodInterceptEventArgs<T>.Information, T>
    {
        public readonly struct Information : IMemberInterceptArgumentInfo<MethodInfo, T>
        {
            public MethodInfo Member { get; }
            public Maybe<T> Value { get; }
            public ImmutableArray<Object?> Arguments { get; }
            public Exception? Exception { get; }

            public Information(MethodInfo method, Maybe<T> value, IEnumerable<Object?>? arguments, Exception? exception)
                : this(method, value, arguments?.ToImmutableArray() ?? ImmutableArray<Object?>.Empty, exception)
            {
            }

            public Information(MethodInfo method, Maybe<T> value, ImmutableArray<Object?> arguments, Exception? exception)
            {
                Member = method ?? throw new ArgumentNullException(nameof(method));
                Value = value;
                Arguments = arguments;
                Exception = exception;
            }
        }

        public sealed override ImmutableArray<Object?> Arguments
        {
            get
            {
                return Info.Arguments;
            }
        }

        public MethodInterceptEventArgs(ValueDelegateUtilities.Info @delegate)
            : this(@delegate.Method ?? throw new ArgumentNullException(nameof(@delegate)), @delegate.Arguments)
        {
        }

        public MethodInterceptEventArgs(MethodInfo method, IEnumerable<Object?>? arguments)
            : this(new Information(method, default, arguments, null))
        {
        }

        public MethodInterceptEventArgs(MethodInfo method, ImmutableArray<Object?> arguments)
            : this(new Information(method, default, arguments, null))
        {
        }

        public MethodInterceptEventArgs(ValueDelegateUtilities.Info @delegate, T value)
            : this(@delegate.Method ?? throw new ArgumentNullException(nameof(@delegate)), value, @delegate.Arguments)
        {
        }

        public MethodInterceptEventArgs(MethodInfo method, T value, IEnumerable<Object?>? arguments)
            : this(new Information(method, value, arguments, null))
        {
        }

        public MethodInterceptEventArgs(MethodInfo method, T value, ImmutableArray<Object?> arguments)
            : this(new Information(method, value, arguments, null))
        {
        }

        public MethodInterceptEventArgs(ValueDelegateUtilities.Info @delegate, Exception exception)
            : this(@delegate.Method ?? throw new ArgumentNullException(nameof(@delegate)), @delegate.Arguments, exception)
        {
        }

        public MethodInterceptEventArgs(MethodInfo method, IEnumerable<Object?>? arguments, Exception exception)
            : this(new Information(method, default, arguments, exception))
        {
        }

        public MethodInterceptEventArgs(MethodInfo method, ImmutableArray<Object?> arguments, Exception exception)
            : this(new Information(method, default, arguments, exception))
        {
        }

        protected MethodInterceptEventArgs(Information value)
            : base(value)
        {
        }

        protected internal override void Intercept(T value)
        {
            Intercept();
            Info = new Information(Info.Member, value, Info.Arguments, null);
            Invoke();
        }

        protected internal override void Intercept(Exception exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            Intercept();
            Info = new Information(Info.Member, default, Info.Arguments, exception);
            Invoke();
        }

        protected override void Clear()
        {
            Unseal();
            _value = default;
            base.Clear();
        }
    }

    public abstract class MethodInterceptAbstractionEventArgs<T, TResult> : MethodInterceptAbstractionEventArgs<T>, IMethodInterceptEventArgs<TResult> where T : IMemberInterceptArgumentInfo<MethodInfo, TResult>
    {
        private protected Maybe<TResult> _value;
        public virtual TResult Value
        {
            get
            {
                return _value.HasValue ? _value.Value : Info.Value.HasValue ? Info.Value.Value : throw new InvalidOperationException("Cannot get value when method result is not set.");
            }
            set
            {
                if (IsSeal)
                {
                    throw new InvalidOperationException("Cannot change value when intercept is seal.");
                }
                
                _value = value;
                Seal();
            }
        }

        Object? ISimpleInterceptEventArgs.Value
        {
            get
            {
                return Value;
            }
            set
            {
                Value = (TResult) value!;
            }
        }
        
        protected MethodInterceptAbstractionEventArgs(T value)
            : base(value)
        {
        }

        protected internal abstract void Intercept(TResult value);

        void ISimpleInterceptEventArgs<TResult>.Intercept(TResult value)
        {
            Intercept(value);
        }
    }

    public abstract class MethodInterceptAbstractionEventArgs<T> : MemberInterceptEventArgs<MethodInfo, T>, IMethodInterceptEventArgs where T : IMemberInterceptArgumentInfo<MethodInfo>
    {
        public MethodInfo Method
        {
            get
            {
                return Member;
            }
        }
        
        Object? ISimpleInterceptEventArgs.Value
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public abstract ImmutableArray<Object?> Arguments { get; }

        protected MethodInterceptAbstractionEventArgs(T value)
            : base(value)
        {
        }

        protected override void Clear()
        {
            Unseal();
            base.Clear();
        }
    }
}