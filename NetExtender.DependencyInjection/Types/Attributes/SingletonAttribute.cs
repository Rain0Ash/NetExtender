// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class SingletonAttribute : DependencyInfoAttribute
    {
        public override ServiceLifetime Lifetime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ServiceLifetime.Singleton;
            }
        }

        public override ServiceStrategy Strategy { get; }
        protected internal override Boolean? IsSingle { get; protected init; }
        public override Int32 Order { get; init; }

        public SingletonAttribute()
            : this(ServiceStrategy.New)
        {
        }

        public SingletonAttribute(Boolean multiple)
            : this(ToStrategy(multiple))
        {
        }

        public SingletonAttribute(ServiceStrategy strategy)
        {
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public SingletonAttribute(params Type[]? after)
            : this(ServiceStrategy.New, after)
        {
        }

        public SingletonAttribute(Boolean multiple, params Type[]? after)
            : this(ToStrategy(multiple), after)
        {
        }

        public SingletonAttribute(ServiceStrategy strategy, params Type[]? after)
            : base(after)
        {
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public SingletonAttribute(Object? key)
            : this(key, ServiceStrategy.New)
        {
        }

        public SingletonAttribute(Object? key, Boolean multiple)
            : this(key, ToStrategy(multiple))
        {
        }

        public SingletonAttribute(Object? key, ServiceStrategy strategy)
            : base(key)
        {
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public SingletonAttribute(Object? key, params Type[]? after)
            : this(key, ServiceStrategy.New, after)
        {
        }

        public SingletonAttribute(Object? key, Boolean multiple, params Type[]? after)
            : this(key, ToStrategy(multiple), after)
        {
        }

        public SingletonAttribute(Object? key, ServiceStrategy strategy, params Type[]? after)
            : base(key, after)
        {
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public SingletonAttribute(Type @interface)
            : this(@interface, ServiceStrategy.New)
        {
        }

        public SingletonAttribute(Type @interface, Boolean multiple)
            : this(@interface, ToStrategy(multiple))
        {
        }

        public SingletonAttribute(Type @interface, ServiceStrategy strategy)
            : base(@interface)
        {
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public SingletonAttribute(Type @interface, params Type[]? after)
            : this(@interface, ServiceStrategy.New, after)
        {
        }

        public SingletonAttribute(Type @interface, Boolean multiple, params Type[]? after)
            : this(@interface, ToStrategy(multiple), after)
        {
        }

        public SingletonAttribute(Type @interface, ServiceStrategy strategy, params Type[]? after)
            : base(@interface, after)
        {
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public SingletonAttribute(Object? key, Type @interface)
            : this(key, @interface, ServiceStrategy.New)
        {
        }

        public SingletonAttribute(Object? key, Type @interface, Boolean multiple)
            : this(key, @interface, ToStrategy(multiple))
        {
        }

        public SingletonAttribute(Object? key, Type @interface, ServiceStrategy strategy)
            : base(key, @interface)
        {
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public SingletonAttribute(Object? key, Type @interface, params Type[]? after)
            : this(key, @interface, ServiceStrategy.New, after)
        {
        }

        public SingletonAttribute(Object? key, Type @interface, Boolean multiple, params Type[]? after)
            : this(key, @interface, ToStrategy(multiple), after)
        {
        }

        public SingletonAttribute(Object? key, Type @interface, ServiceStrategy strategy, params Type[]? after)
            : base(key, @interface, after)
        {
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }
    }
}