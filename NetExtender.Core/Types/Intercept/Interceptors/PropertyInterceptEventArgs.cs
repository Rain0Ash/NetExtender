// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Intercept.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.Types.Intercept
{
    public enum PropertyInterceptAccessor : Byte
    {
        Unknown,
        Get,
        Set,
        Init
    }
    
    public class PropertyInterceptEventArgs<T> : MemberInterceptEventArgs<PropertyInfo, PropertyInterceptEventArgs<T>.Information>, IPropertyInterceptEventArgs<T>
    {
        public readonly struct Information : IMemberInterceptArgumentInfo<PropertyInfo, T>
        {
            public PropertyInfo Member { get; }
            public PropertyInterceptAccessor Accessor { get; }
            public Maybe<T> Value { get; }
            public Exception? Exception { get; }

            public Information(PropertyInfo property, PropertyInterceptAccessor accessor, Maybe<T> value, Exception? exception)
            {
                Member = property ?? throw new ArgumentNullException(nameof(property));
                Accessor = accessor;
                Value = value;
                Exception = exception;
            }
        }

        public PropertyInterceptAccessor Accessor
        {
            get
            {
                return Info.Accessor;
            }
        }

        public PropertyInfo Property
        {
            get
            {
                return Member;
            }
        }

        private protected Maybe<T> _value;
        public virtual T Value
        {
            get
            {
                return _value.HasValue ? _value.Value : Info.Value.HasValue ? Info.Value.Value : throw new InvalidOperationException("Cannot get value when property is not set.");
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
                Value = (T) value!;
            }
        }

        public PropertyInterceptEventArgs(PropertyInfo property, PropertyInterceptAccessor accessor)
            : this(new Information(property, accessor, default, null))
        {
        }

        public PropertyInterceptEventArgs(PropertyInfo property, PropertyInterceptAccessor accessor, T value)
            : this(new Information(property, accessor, value, null))
        {
        }

        public PropertyInterceptEventArgs(PropertyInfo property, PropertyInterceptAccessor accessor, Exception exception)
            : this(new Information(property, accessor, default, exception))
        {
        }

        protected PropertyInterceptEventArgs(Information value)
            : base(IsAllow(value.Accessor) ? value : throw new EnumUndefinedOrNotSupportedException<PropertyInterceptAccessor>(value.Accessor, nameof(value), null))
        {
        }

        protected static Boolean IsAllow(PropertyInterceptAccessor value)
        {
            return value is PropertyInterceptAccessor.Get or PropertyInterceptAccessor.Set or PropertyInterceptAccessor.Init;
        }

        protected internal virtual void Intercept(T value)
        {
            Intercept();
            Info = new Information(Info.Member, Info.Accessor, value, null);
            Invoke();
        }

        void ISimpleInterceptEventArgs<T>.Intercept(T value)
        {
            Intercept(value);
        }

        protected internal override void Intercept(Exception exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            Intercept();
            Info = new Information(Info.Member, Info.Accessor, default, exception);
            Invoke();
        }

        public override void Ignore()
        {
            if (Accessor is PropertyInterceptAccessor.Get)
            {
                throw new InvalidOperationException($"Cannot ignore when intercept action is '{nameof(PropertyInterceptAccessor.Get)}'.");
            }

            base.Ignore();
        }

        protected override void Clear()
        {
            Unseal();
            _value = default;
            base.Clear();
        }
    }
}