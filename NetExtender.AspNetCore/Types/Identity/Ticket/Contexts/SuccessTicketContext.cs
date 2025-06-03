// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NetExtender.AspNetCore.Identity.Interfaces;

namespace NetExtender.AspNetCore.Identity
{
    public readonly struct SuccessTicketContext : ITicketContext, IEquatableStruct<SuccessTicketContext>, IEquatable<InvalidTicketContext>, IEquatable<FailTicketContext>
    {
        public static implicit operator Boolean(SuccessTicketContext value)
        {
            return !value.IsEmpty;
        }

        public static Boolean operator ==(SuccessTicketContext first, SuccessTicketContext second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(SuccessTicketContext first, SuccessTicketContext second)
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

        public AuthenticationTicket Ticket { get; private init; }
        public JWTAuthenticationOptions? Options { get; private init; }

        Boolean ITicketContext.IsSuccess
        {
            get
            {
                return this;
            }
        }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Context is null || Ticket is null;
            }
        }

        public SuccessTicketContext(HttpContext context, AuthenticationTicket ticket)
            : this(context, ticket, null)
        {
        }

        public SuccessTicketContext(HttpContext context, AuthenticationTicket ticket, JWTAuthenticationOptions? options)
            : this(null, context, ticket, options)
        {
        }

        public SuccessTicketContext(ILogger? logger, HttpContext context, AuthenticationTicket ticket)
            : this(logger, context, ticket, null)
        {
        }

        public SuccessTicketContext(ILogger? logger, HttpContext context, AuthenticationTicket ticket, JWTAuthenticationOptions? options)
        {
            Logger = logger;
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Ticket = ticket ?? throw new ArgumentNullException(nameof(ticket));
            Options = options;
        }
        
        public static SuccessTicketContext From<T>(InvalidTicketContext<T> context, AuthenticationTicket ticket)
        {
            return new SuccessTicketContext
            {
                Logger = context.Logger,
                Context = context.Context,
                Ticket = ticket ?? throw new ArgumentNullException(nameof(ticket)),
                Options = context.Options
            };
        }

        public static SuccessTicketContext From(InvalidTicketContext context, AuthenticationTicket ticket)
        {
            return new SuccessTicketContext
            {
                Logger = context.Logger,
                Context = context.Context,
                Ticket = ticket ?? throw new ArgumentNullException(nameof(ticket)),
                Options = context.Options
            };
        }

        public static SuccessTicketContext From(FailTicketContext context, AuthenticationTicket ticket)
        {
            return new SuccessTicketContext
            {
                Logger = context.Logger,
                Context = context.Context,
                Ticket = ticket ?? throw new ArgumentNullException(nameof(ticket)),
                Options = context.Options
            };
        }

        public InvalidTicketContext<T> Fail<T>(IdentityException<T> exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return InvalidTicketContext<T>.From(this, exception);
        }

        public InvalidTicketContext Fail(IIdentityException exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return InvalidTicketContext.From(this, exception);
        }

        public FailTicketContext Fail(Exception exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return FailTicketContext.From(this, exception);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Context, Ticket, Options);
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => IsEmpty,
                HttpContext value => Context == value,
                SuccessTicketContext value => Equals(value),
                InvalidTicketContext value => Equals(value),
                FailTicketContext value => Equals(value),
                InvalidTicketContext.ITicketContext value => Equals(value.Ticket),
                ITicketContext value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(SuccessTicketContext other)
        {
            return Context == other.Context && Ticket == other.Ticket && Options == other.Options;
        }

        public Boolean Equals(InvalidTicketContext other)
        {
            return false;
        }

        public Boolean Equals(FailTicketContext other)
        {
            return false;
        }

        public Boolean Equals(ITicketContext? other)
        {
            return other switch
            {
                null => IsEmpty,
                SuccessTicketContext value => Equals(value),
                InvalidTicketContext value => Equals(value),
                FailTicketContext value => Equals(value),
                InvalidTicketContext.ITicketContext value => Equals(value.Ticket),
                _ => false
            };
        }
    }
}
