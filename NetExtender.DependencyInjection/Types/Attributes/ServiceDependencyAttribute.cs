// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.Exceptions;

namespace NetExtender.DependencyInjection
{
    public enum ServiceStrategy : Byte
    {
        New = 1,
        Multi = 2,
        Single = 3,
        Replace = 4,
        MultiReplace = 5
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class ServiceDependencyAttribute : ServiceDependencyInfoAttribute
    {
        public static Boolean IsStrict { get; set; }
        public override ServiceLifetime Lifetime { get; }
        public override ServiceStrategy Strategy { get; }
        protected internal override Boolean? IsSingle { get; protected init; }
        public override Int32 Order { get; init; }

        public ServiceDependencyAttribute(ServiceLifetime lifetime)
            : this(lifetime, ServiceStrategy.New)
        {
        }

        public ServiceDependencyAttribute(ServiceLifetime lifetime, Boolean multiple)
            : this(lifetime, ToStrategy(multiple))
        {
        }

        public ServiceDependencyAttribute(ServiceLifetime lifetime, ServiceStrategy strategy)
        {
            Lifetime = lifetime;
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public ServiceDependencyAttribute(ServiceLifetime lifetime, params Type[]? after)
            : this(lifetime, ServiceStrategy.New, after)
        {
        }

        public ServiceDependencyAttribute(ServiceLifetime lifetime, Boolean multiple, params Type[]? after)
            : this(lifetime, ToStrategy(multiple), after)
        {
        }

        public ServiceDependencyAttribute(ServiceLifetime lifetime, ServiceStrategy strategy, params Type[]? after)
            : base(after)
        {
            Lifetime = lifetime;
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public ServiceDependencyAttribute(Object? key, ServiceLifetime lifetime)
            : this(key, lifetime, ServiceStrategy.New)
        {
        }

        public ServiceDependencyAttribute(Object? key, ServiceLifetime lifetime, Boolean multiple)
            : this(key, lifetime, ToStrategy(multiple))
        {
        }

        public ServiceDependencyAttribute(Object? key, ServiceLifetime lifetime, ServiceStrategy strategy)
            : base(key)
        {
            Lifetime = lifetime;
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public ServiceDependencyAttribute(Object? key, ServiceLifetime lifetime, params Type[]? after)
            : this(key, lifetime, ServiceStrategy.New, after)
        {
        }

        public ServiceDependencyAttribute(Object? key, ServiceLifetime lifetime, Boolean multiple, params Type[]? after)
            : this(key, lifetime, ToStrategy(multiple), after)
        {
        }

        public ServiceDependencyAttribute(Object? key, ServiceLifetime lifetime, ServiceStrategy strategy, params Type[]? after)
            : base(key, after)
        {
            Lifetime = lifetime;
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public ServiceDependencyAttribute(Type @interface, ServiceLifetime lifetime)
            : this(@interface, lifetime, ServiceStrategy.New)
        {
        }

        public ServiceDependencyAttribute(Type @interface, ServiceLifetime lifetime, Boolean multiple)
            : this(@interface, lifetime, ToStrategy(multiple))
        {
        }

        public ServiceDependencyAttribute(Type @interface, ServiceLifetime lifetime, ServiceStrategy strategy)
            : base(@interface)
        {
            Lifetime = lifetime;
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public ServiceDependencyAttribute(Type @interface, ServiceLifetime lifetime, params Type[]? after)
            : this(@interface, lifetime, ServiceStrategy.New, after)
        {
        }

        public ServiceDependencyAttribute(Type @interface, ServiceLifetime lifetime, Boolean multiple, params Type[]? after)
            : this(@interface, lifetime, ToStrategy(multiple), after)
        {
        }

        public ServiceDependencyAttribute(Type @interface, ServiceLifetime lifetime, ServiceStrategy strategy, params Type[]? after)
            : base(@interface, after)
        {
            Lifetime = lifetime;
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public ServiceDependencyAttribute(Object? key, Type @interface, ServiceLifetime lifetime)
            : this(key, @interface, lifetime, ServiceStrategy.New)
        {
        }

        public ServiceDependencyAttribute(Object? key, Type @interface, ServiceLifetime lifetime, Boolean multiple)
            : this(key, @interface, lifetime, ToStrategy(multiple))
        {
        }

        public ServiceDependencyAttribute(Object? key, Type @interface, ServiceLifetime lifetime, ServiceStrategy strategy)
            : base(key, @interface)
        {
            Lifetime = lifetime;
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public ServiceDependencyAttribute(Object? key, Type @interface, ServiceLifetime lifetime, params Type[]? after)
            : this(key, @interface, lifetime, ServiceStrategy.New, after)
        {
        }

        public ServiceDependencyAttribute(Object? key, Type @interface, ServiceLifetime lifetime, Boolean multiple, params Type[]? after)
            : this(key, @interface, lifetime, ToStrategy(multiple), after)
        {
        }

        public ServiceDependencyAttribute(Object? key, Type @interface, ServiceLifetime lifetime, ServiceStrategy strategy, params Type[]? after)
            : base(key, @interface, after)
        {
            Lifetime = lifetime;
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }
    }

    public abstract class ServiceDependencyInfoAttribute : Attribute
    {
        public abstract ServiceLifetime Lifetime { get; }
        public Type? Interface { get; }
        public Object? Key { get; }
        public abstract ServiceStrategy Strategy { get; }

        public Boolean Single
        {
            get
            {
                return IsSingle ?? false;
            }
            init
            {
                IsSingle = value;
            }
        }

        protected internal abstract Boolean? IsSingle { get; protected init; }

        public abstract Int32 Order { get; init; }

        private readonly Boolean? _strict;
        public Boolean Strict
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _strict ?? ServiceDependencyAttribute.IsStrict;
            }
            init
            {
                _strict = value;
            }
        }

        private readonly Type[]? _before;
        public Type[] Before
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _before ?? Type.EmptyTypes;
            }
            init
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _before = Array.IndexOf(value, default) < 0 ? value : throw new ArgumentException("Array contains 'null' element.");
            }
        }

        private readonly Type[]? _after;
        public Type[] After
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _after ?? Type.EmptyTypes;
            }
            init
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _after = Array.IndexOf(value, default) < 0 ? value : throw new ArgumentException("Array contains 'null' element.");
            }
        }

        public Boolean HasDependency
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _before is { Length: > 0 } || _after is { Length: > 0 };
            }
        }

        protected ServiceDependencyInfoAttribute()
            : this((Type[]?) null)
        {
        }

        protected ServiceDependencyInfoAttribute(params Type[]? after)
            : this((Object?) null, after)
        {
        }

        protected ServiceDependencyInfoAttribute(Object? key)
            : this(key, (Type[]?) null)
        {
        }

        protected ServiceDependencyInfoAttribute(Object? key, params Type[]? after)
        {
            Key = key;

            if (after is not null)
            {
                After = after;
            }
        }

        protected ServiceDependencyInfoAttribute(Type @interface)
            : this(@interface, (Type[]?) null)
        {
        }

        protected ServiceDependencyInfoAttribute(Type @interface, params Type[]? after)
            : this(null, @interface, after)
        {
        }

        protected ServiceDependencyInfoAttribute(Object? key, Type @interface)
            : this(key, @interface, (Type[]?) null)
        {
        }

        protected ServiceDependencyInfoAttribute(Object? key, Type @interface, params Type[]? after)
        {
            if (@interface is null)
            {
                throw new ArgumentNullException(nameof(@interface));
            }

            if (!@interface.IsInterface)
            {
                throw new TypeNotSupportedException(@interface, $"Type '{@interface.Name}' must be interface for dependency injection registration.");
            }

            Interface = @interface;
            Key = key;

            if (after is not null)
            {
                After = after;
            }
        }

        private protected static ServiceStrategy ToStrategy(Boolean? multiple)
        {
            return multiple switch
            {
                null => ServiceStrategy.New,
                true => ServiceStrategy.Multi,
                false => ServiceStrategy.Replace
            };
        }

        private protected static Boolean? ToSingle(ServiceStrategy strategy)
        {
            return strategy switch
            {
                default(ServiceStrategy) => null,
                ServiceStrategy.New => null,
                ServiceStrategy.Multi => false,
                ServiceStrategy.Single => true,
                ServiceStrategy.Replace => true,
                ServiceStrategy.MultiReplace => false,
                _ => throw new EnumUndefinedOrNotSupportedException<ServiceStrategy>(strategy, nameof(strategy), null)
            };
        }
    }
}