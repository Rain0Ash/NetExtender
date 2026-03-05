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

    [AttributeUsage(AttributeTargets.Constructor)]
    public sealed class DependencyConstructorAttribute : ActivatorUtilitiesConstructorAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class DependencyAttribute : DependencyInfoAttribute
    {
        public static Boolean IsStrict { get; set; }
        public override ServiceLifetime Lifetime { get; }
        public override ServiceStrategy Strategy { get; }
        protected internal override Boolean? IsSingle { get; protected init; }
        public override Int32 Order { get; init; }

        public DependencyAttribute(ServiceLifetime lifetime)
            : this(lifetime, ServiceStrategy.New)
        {
        }

        public DependencyAttribute(ServiceLifetime lifetime, Boolean multiple)
            : this(lifetime, ToStrategy(multiple))
        {
        }

        public DependencyAttribute(ServiceLifetime lifetime, ServiceStrategy strategy)
        {
            Lifetime = lifetime;
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public DependencyAttribute(ServiceLifetime lifetime, params Type[]? after)
            : this(lifetime, ServiceStrategy.New, after)
        {
        }

        public DependencyAttribute(ServiceLifetime lifetime, Boolean multiple, params Type[]? after)
            : this(lifetime, ToStrategy(multiple), after)
        {
        }

        public DependencyAttribute(ServiceLifetime lifetime, ServiceStrategy strategy, params Type[]? after)
            : base(after)
        {
            Lifetime = lifetime;
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public DependencyAttribute(Object? key, ServiceLifetime lifetime)
            : this(key, lifetime, ServiceStrategy.New)
        {
        }

        public DependencyAttribute(Object? key, ServiceLifetime lifetime, Boolean multiple)
            : this(key, lifetime, ToStrategy(multiple))
        {
        }

        public DependencyAttribute(Object? key, ServiceLifetime lifetime, ServiceStrategy strategy)
            : base(key)
        {
            Lifetime = lifetime;
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public DependencyAttribute(Object? key, ServiceLifetime lifetime, params Type[]? after)
            : this(key, lifetime, ServiceStrategy.New, after)
        {
        }

        public DependencyAttribute(Object? key, ServiceLifetime lifetime, Boolean multiple, params Type[]? after)
            : this(key, lifetime, ToStrategy(multiple), after)
        {
        }

        public DependencyAttribute(Object? key, ServiceLifetime lifetime, ServiceStrategy strategy, params Type[]? after)
            : base(key, after)
        {
            Lifetime = lifetime;
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public DependencyAttribute(Type @interface, ServiceLifetime lifetime)
            : this(@interface, lifetime, ServiceStrategy.New)
        {
        }

        public DependencyAttribute(Type @interface, ServiceLifetime lifetime, Boolean multiple)
            : this(@interface, lifetime, ToStrategy(multiple))
        {
        }

        public DependencyAttribute(Type @interface, ServiceLifetime lifetime, ServiceStrategy strategy)
            : base(@interface)
        {
            Lifetime = lifetime;
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public DependencyAttribute(Type @interface, ServiceLifetime lifetime, params Type[]? after)
            : this(@interface, lifetime, ServiceStrategy.New, after)
        {
        }

        public DependencyAttribute(Type @interface, ServiceLifetime lifetime, Boolean multiple, params Type[]? after)
            : this(@interface, lifetime, ToStrategy(multiple), after)
        {
        }

        public DependencyAttribute(Type @interface, ServiceLifetime lifetime, ServiceStrategy strategy, params Type[]? after)
            : base(@interface, after)
        {
            Lifetime = lifetime;
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public DependencyAttribute(Object? key, Type @interface, ServiceLifetime lifetime)
            : this(key, @interface, lifetime, ServiceStrategy.New)
        {
        }

        public DependencyAttribute(Object? key, Type @interface, ServiceLifetime lifetime, Boolean multiple)
            : this(key, @interface, lifetime, ToStrategy(multiple))
        {
        }

        public DependencyAttribute(Object? key, Type @interface, ServiceLifetime lifetime, ServiceStrategy strategy)
            : base(key, @interface)
        {
            Lifetime = lifetime;
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public DependencyAttribute(Object? key, Type @interface, ServiceLifetime lifetime, params Type[]? after)
            : this(key, @interface, lifetime, ServiceStrategy.New, after)
        {
        }

        public DependencyAttribute(Object? key, Type @interface, ServiceLifetime lifetime, Boolean multiple, params Type[]? after)
            : this(key, @interface, lifetime, ToStrategy(multiple), after)
        {
        }

        public DependencyAttribute(Object? key, Type @interface, ServiceLifetime lifetime, ServiceStrategy strategy, params Type[]? after)
            : base(key, @interface, after)
        {
            Lifetime = lifetime;
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public abstract class DependencyInfoAttribute : Attribute
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
                return _strict ?? DependencyAttribute.IsStrict;
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

        protected DependencyInfoAttribute()
            : this((Type[]?) null)
        {
        }

        protected DependencyInfoAttribute(params Type[]? after)
            : this((Object?) null, after)
        {
        }

        protected DependencyInfoAttribute(Object? key)
            : this(key, (Type[]?) null)
        {
        }

        protected DependencyInfoAttribute(Object? key, params Type[]? after)
        {
            Key = key;

            if (after is not null)
            {
                After = after;
            }
        }

        protected DependencyInfoAttribute(Type @interface)
            : this(@interface, (Type[]?) null)
        {
        }

        protected DependencyInfoAttribute(Type @interface, params Type[]? after)
            : this(null, @interface, after)
        {
        }

        protected DependencyInfoAttribute(Object? key, Type @interface)
            : this(key, @interface, (Type[]?) null)
        {
        }

        protected DependencyInfoAttribute(Object? key, Type @interface, params Type[]? after)
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

        internal static ServiceStrategy ToStrategy(Boolean? multiple)
        {
            return multiple switch
            {
                null => ServiceStrategy.New,
                true => ServiceStrategy.Multi,
                false => ServiceStrategy.Replace
            };
        }

        internal static Boolean? ToSingle(ServiceStrategy strategy)
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