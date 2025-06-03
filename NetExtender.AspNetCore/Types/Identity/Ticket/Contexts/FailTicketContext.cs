// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NetExtender.AspNetCore.Identity.Interfaces;

namespace NetExtender.AspNetCore.Identity
{
    public readonly struct FailTicketContext : ITicketContext, IEquatableStruct<FailTicketContext>, IEquatable<InvalidTicketContext>, IEquatable<SuccessTicketContext>
    {
        public static implicit operator Boolean(FailTicketContext value)
        {
            return !value.IsEmpty;
        }

        public static implicit operator Exception(FailTicketContext value)
        {
            return value ? value.Exception : new IdentityNoContextException();
        }

        public static Boolean operator ==(FailTicketContext first, FailTicketContext second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(FailTicketContext first, FailTicketContext second)
        {
            return !(first == second);
        }

        public ILogger? Logger { get; private init; }
        public HttpContext Context { get; private init; }

        HttpContext ITicketContext.Context
        {
            get
            {
                return Context ?? throw new IdentityNoContextException();
            }
        }
        
        public JWTAuthenticationOptions? Options { get; private init; }
        public Exception Exception { get; private init; }

        Boolean ITicketContext.IsSuccess
        {
            get
            {
                return false;
            }
        }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Context is null;
            }
        }

        public FailTicketContext(HttpContext context, Exception exception)
            : this(context, null, exception)
        {
        }

        public FailTicketContext(HttpContext context, JWTAuthenticationOptions? options, Exception exception)
            : this(null, context, options, exception)
        {
        }

        public FailTicketContext(ILogger? logger, HttpContext context, Exception exception)
            : this(logger, context, null, exception)
        {
        }

        public FailTicketContext(ILogger? logger, HttpContext context, JWTAuthenticationOptions? options, Exception exception)
        {
            Logger = logger;
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Options = options;
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        internal static FailTicketContext From(SuccessTicketContext context, Exception exception)
        {
            return new FailTicketContext
            {
                Logger = context.Logger,
                Context = context.Context,
                Options = context.Options,
                Exception = exception ?? throw new ArgumentNullException(nameof(exception))
            };
        }

        internal static FailTicketContext From<T>(InvalidTicketContext<T> context)
        {
            return new FailTicketContext
            {
                Logger = context.Logger,
                Context = context.Context,
                Options = context.Options,
                Exception = context.Exception
            };
        }

        internal static FailTicketContext From(InvalidTicketContext context)
        {
            return new FailTicketContext
            {
                Logger = context.Logger,
                Context = context.Context,
                Options = context.Options,
                Exception = (Exception) context.Exception
            };
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Context, Options, Exception);
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => IsEmpty,
                HttpContext value => Context == value,
                FailTicketContext value => Equals(value),
                InvalidTicketContext value => Equals(value),
                SuccessTicketContext value => Equals(value),
                InvalidTicketContext.ITicketContext value => Equals(value.Ticket),
                ITicketContext value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(FailTicketContext other)
        {
            return Context == other.Context && Options == other.Options && Exception == other.Exception;
        }

        public Boolean Equals(InvalidTicketContext other)
        {
            return Context == other.Context && Options == other.Options && Exception == other.Exception;
        }

        public Boolean Equals(SuccessTicketContext other)
        {
            return false;
        }

        public Boolean Equals(ITicketContext? other)
        {
            return other switch
            {
                null => IsEmpty,
                FailTicketContext value => Equals(value),
                InvalidTicketContext value => Equals(value),
                SuccessTicketContext value => Equals(value),
                InvalidTicketContext.ITicketContext value => Equals(value.Ticket),
                _ => false
            };
        }
    }
}
