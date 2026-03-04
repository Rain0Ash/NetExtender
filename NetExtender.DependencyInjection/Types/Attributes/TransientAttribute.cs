// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class TransientAttribute : DependencyInfoAttribute
    {
        public override ServiceLifetime Lifetime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ServiceLifetime.Transient;
            }
        }

        public override ServiceStrategy Strategy { get; }
        protected internal override Boolean? IsSingle { get; protected init; }
        public override Int32 Order { get; init; }

        public TransientAttribute()
            : this(ServiceStrategy.New)
        {
        }

        public TransientAttribute(Boolean multiple)
            : this(ToStrategy(multiple))
        {
        }

        public TransientAttribute(ServiceStrategy strategy)
        {
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public TransientAttribute(params Type[]? after)
            : this(ServiceStrategy.New, after)
        {
        }

        public TransientAttribute(Boolean multiple, params Type[]? after)
            : this(ToStrategy(multiple), after)
        {
        }

        public TransientAttribute(ServiceStrategy strategy, params Type[]? after)
            : base(after)
        {
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public TransientAttribute(Object? key)
            : this(key, ServiceStrategy.New)
        {
        }

        public TransientAttribute(Object? key, Boolean multiple)
            : this(key, ToStrategy(multiple))
        {
        }

        public TransientAttribute(Object? key, ServiceStrategy strategy)
            : base(key)
        {
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public TransientAttribute(Object? key, params Type[]? after)
            : this(key, ServiceStrategy.New, after)
        {
        }

        public TransientAttribute(Object? key, Boolean multiple, params Type[]? after)
            : this(key, ToStrategy(multiple), after)
        {
        }

        public TransientAttribute(Object? key, ServiceStrategy strategy, params Type[]? after)
            : base(key, after)
        {
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public TransientAttribute(Type @interface)
            : this(@interface, ServiceStrategy.New)
        {
        }

        public TransientAttribute(Type @interface, Boolean multiple)
            : this(@interface, ToStrategy(multiple))
        {
        }

        public TransientAttribute(Type @interface, ServiceStrategy strategy)
            : base(@interface)
        {
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public TransientAttribute(Type @interface, params Type[]? after)
            : this(@interface, ServiceStrategy.New, after)
        {
        }

        public TransientAttribute(Type @interface, Boolean multiple, params Type[]? after)
            : this(@interface, ToStrategy(multiple), after)
        {
        }

        public TransientAttribute(Type @interface, ServiceStrategy strategy, params Type[]? after)
            : base(@interface, after)
        {
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public TransientAttribute(Object? key, Type @interface)
            : this(key, @interface, ServiceStrategy.New)
        {
        }

        public TransientAttribute(Object? key, Type @interface, Boolean multiple)
            : this(key, @interface, ToStrategy(multiple))
        {
        }

        public TransientAttribute(Object? key, Type @interface, ServiceStrategy strategy)
            : base(key, @interface)
        {
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }

        public TransientAttribute(Object? key, Type @interface, params Type[]? after)
            : this(key, @interface, ServiceStrategy.New, after)
        {
        }

        public TransientAttribute(Object? key, Type @interface, Boolean multiple, params Type[]? after)
            : this(key, @interface, ToStrategy(multiple), after)
        {
        }

        public TransientAttribute(Object? key, Type @interface, ServiceStrategy strategy, params Type[]? after)
            : base(key, @interface, after)
        {
            Strategy = strategy;
            IsSingle = ToSingle(strategy);
        }
    }
}