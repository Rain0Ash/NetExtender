using System;
using System.Runtime.CompilerServices;
using NetExtender.Monads;

namespace NetExtender.CQRS
{
    public abstract partial class CQRS<TContext>
    {
        public new interface IContext : ICQRS.IContext, IEquatable<TContext>
        {
            public TContext Next();
            public TContext Next(RayIdPayload? payload);
            public TContext Next(Boolean flags);
            public TContext Next(Boolean flags, RayIdPayload? payload);
            public TContext Next(RayIdFlags flags);
            public TContext Next(RayIdFlags flags, RayIdPayload? payload);
            public TContext Next<T>(T value) where T : notnull;
            public TContext Next<T>(in T value) where T : notnull;
            public TContext Next<T>(T value, RayIdPayload? payload) where T : notnull;
            public TContext Next<T>(in T value, RayIdPayload? payload) where T : notnull;
            public TContext Next<T>(T value, Boolean flags) where T : notnull;
            public TContext Next<T>(in T value, Boolean flags) where T : notnull;
            public TContext Next<T>(T value, Boolean flags, RayIdPayload? payload) where T : notnull;
            public TContext Next<T>(in T value, Boolean flags, RayIdPayload? payload) where T : notnull;
            public TContext Next<T>(T value, RayIdFlags flags) where T : notnull;
            public TContext Next<T>(in T value, RayIdFlags flags) where T : notnull;
            public TContext Next<T>(T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull;
            public TContext Next<T>(in T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull;
            public TContext Continue();
            public TContext Continue(RayIdPayload? payload);
            public TContext Continue(Boolean flags);
            public TContext Continue(Boolean flags, RayIdPayload? payload);
            public TContext Continue(RayIdFlags flags);
            public TContext Continue(RayIdFlags flags, RayIdPayload? payload);
            public TContext Continue<T>(T value) where T : notnull;
            public TContext Continue<T>(in T value) where T : notnull;
            public TContext Continue<T>(T value, RayIdPayload? payload) where T : notnull;
            public TContext Continue<T>(in T value, RayIdPayload? payload) where T : notnull;
            public TContext Continue<T>(T value, Boolean flags) where T : notnull;
            public TContext Continue<T>(in T value, Boolean flags) where T : notnull;
            public TContext Continue<T>(T value, Boolean flags, RayIdPayload? payload) where T : notnull;
            public TContext Continue<T>(in T value, Boolean flags, RayIdPayload? payload) where T : notnull;
            public TContext Continue<T>(T value, RayIdFlags flags) where T : notnull;
            public TContext Continue<T>(in T value, RayIdFlags flags) where T : notnull;
            public TContext Continue<T>(T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull;
            public TContext Continue<T>(in T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull;
        }
    }

    public partial interface ICQRS
    {
        public interface IContext
        {
            public Boolean IsCQRS { get; }
            public RayIdContext RayId { get; set; }
            public RayIdPayload? Payload { get; }
            public Boolean IsEmpty { get; }
        }

        public struct Context : CQRS<Context>.IContext, IEquatableStruct<Context>, IEquatable<RayId>, IEquatable<RayIdContext>
        {
            public static implicit operator Context(RayIdContext value)
            {
                return new Context(value);
            }

            public static implicit operator Context(RayIdContext? value)
            {
                return new Context(value);
            }

            public static Boolean operator ==(Context first, Context second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(Context first, Context second)
            {
                return !(first == second);
            }

            private Boolean _init = false;
            public Boolean IsCQRS
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                readonly get
                {
                    return !_init;
                }
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                set
                {
                    _init = !value;
                }
            }

            private RayIdContext? _context;
            public readonly RayIdContext RayId
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _context ?? default;
                }
            }

            RayIdContext IContext.RayId
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                readonly get
                {
                    return RayId;
                }
                set
                {
                    _context = value;
                }
            }

            public readonly RayIdPayload? Payload
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _context?.Payload;
                }
            }

            public readonly Boolean IsEmpty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _context is null or { IsEmptyOrInvalid: true };
                }
            }

            private Context(RayIdContext value)
            {
                _context = value;
            }

            private Context(RayIdContext? value)
            {
                _context = value;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next()
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next();
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next(RayIdPayload? payload)
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next(payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next(Boolean flags)
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next(flags);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next(Boolean flags, RayIdPayload? payload)
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next(flags, payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next(RayIdFlags flags)
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next(flags);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next(RayIdFlags flags, RayIdPayload? payload)
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next(flags, payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next<T>(T value) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next(value);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next<T>(in T value) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next(in value);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next<T>(T value, RayIdPayload? payload) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next(value, payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next<T>(in T value, RayIdPayload? payload) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next(in value, payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next<T>(T value, Boolean flags) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next(value, flags);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next<T>(in T value, Boolean flags) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next(in value, flags);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next<T>(T value, Boolean flags, RayIdPayload? payload) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next(value, flags, payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next<T>(in T value, Boolean flags, RayIdPayload? payload) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next(in value, flags, payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next<T>(T value, RayIdFlags flags) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next(value, flags);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next<T>(in T value, RayIdFlags flags) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next(in value, flags);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next<T>(T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next(value, flags, payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Next<T>(in T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Next(in value, flags, payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue()
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue();
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue(RayIdPayload? payload)
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue(payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue(Boolean flags)
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue(flags);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue(Boolean flags, RayIdPayload? payload)
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue(flags, payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue(RayIdFlags flags)
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue(flags);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue(RayIdFlags flags, RayIdPayload? payload)
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue(flags, payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue<T>(T value) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue(value);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue<T>(in T value) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue(in value);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue<T>(T value, RayIdPayload? payload) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue(value, payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue<T>(in T value, RayIdPayload? payload) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue(in value, payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue<T>(T value, Boolean flags) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue(value, flags);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue<T>(in T value, Boolean flags) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue(in value, flags);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue<T>(T value, Boolean flags, RayIdPayload? payload) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue(value, flags, payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue<T>(in T value, Boolean flags, RayIdPayload? payload) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue(in value, flags, payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue<T>(T value, RayIdFlags flags) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue(value, flags);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue<T>(in T value, RayIdFlags flags) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue(in value, flags);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue<T>(T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue(value, flags, payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Context Continue<T>(in T value, RayIdFlags flags, RayIdPayload? payload) where T : notnull
            {
                Context result = this;
                if (result._context is { } context)
                {
                    result._context = context.Continue(in value, flags, payload);
                }

                return result;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override readonly Int32 GetHashCode()
            {
                return RayId.GetHashCode();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override readonly Boolean Equals(Object? other)
            {
                return other switch
                {
                    null => IsEmpty,
                    Context context => Equals(context),
                    RayId context => Equals(context),
                    RayIdContext context => Equals(context),
                    _ => false
                };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean Equals(Context other)
            {
                return Equals(other.RayId);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean Equals(RayId other)
            {
                return RayId.Equals(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly Boolean Equals(RayIdContext other)
            {
                return RayId.Equals(other);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override readonly String ToString()
            {
                return RayId.ToString();
            }
        }
    }
}