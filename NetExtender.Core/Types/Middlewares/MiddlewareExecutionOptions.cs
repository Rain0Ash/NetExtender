using System;

namespace NetExtender.Types.Middlewares
{
    public readonly struct MiddlewareExecutionOptions : IEquatable<MiddlewareExecutionOptions>
    {
        public static implicit operator MiddlewareExecutionOptions(MiddlewareExecutionContext value)
        {
            return new MiddlewareExecutionOptions(value);
        }

        public static Boolean operator ==(MiddlewareExecutionOptions first, MiddlewareExecutionOptions second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(MiddlewareExecutionOptions first, MiddlewareExecutionOptions second)
        {
            return !(first == second);
        }

        public MiddlewareExecutionContext Context { get; init; }
        public MiddlewareIdempotencyMode Idempotency { get; init; }
        
        public MiddlewareExecutionOptions(MiddlewareExecutionContext context)
            : this(context, MiddlewareIdempotencyMode.None)
        {
        }

        public MiddlewareExecutionOptions(MiddlewareExecutionContext context, MiddlewareIdempotencyMode idempotency)
        {
            Context = context;
            Idempotency = idempotency;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Context, Idempotency);
        }

        public override Boolean Equals(Object? other)
        {
            return other is MiddlewareExecutionOptions options && Equals(options);
        }

        public Boolean Equals(MiddlewareExecutionOptions other)
        {
            return Context == other.Context && Idempotency == other.Idempotency;
        }

        public override String ToString()
        {
            return $"{{ {nameof(Context)}: {Context}, {nameof(Idempotency)}: {Idempotency} }}";
        }
    }
}