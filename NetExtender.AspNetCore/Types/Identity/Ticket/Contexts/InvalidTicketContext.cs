// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.Types.Exceptions.Interfaces;

namespace NetExtender.AspNetCore.Identity
{
    public readonly struct InvalidTicketContext<T> : InvalidTicketContext.ITicketContext, IEquatableStruct<InvalidTicketContext<T>>, IEquatable<InvalidTicketContext>, IEquatable<FailTicketContext>, IEquatable<SuccessTicketContext>
    {
        public static implicit operator Boolean(InvalidTicketContext<T> value)
        {
            return !value.IsEmpty;
        }

        public static implicit operator IdentityException<T>(InvalidTicketContext<T> value)
        {
            return value ? value.Exception : typeof(T) == typeof(String) ? (IdentityNoContextException<T>) (Object) new IdentityNoContextException() : new IdentityNoContextException<T>(default!);
        }

        public static implicit operator Exception(InvalidTicketContext<T> value)
        {
            return value ? value.Exception : new IdentityNoContextException();
        }

        public static implicit operator InvalidTicketContext(InvalidTicketContext<T> value)
        {
            return InvalidTicketContext.From(value);
        }

        public static explicit operator FailTicketContext(InvalidTicketContext<T> value)
        {
            return FailTicketContext.From(value);
        }

        public static Boolean operator ==(InvalidTicketContext<T> first, InvalidTicketContext<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(InvalidTicketContext<T> first, InvalidTicketContext<T> second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(InvalidTicketContext<T> first, InvalidTicketContext second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(InvalidTicketContext<T> first, InvalidTicketContext second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(InvalidTicketContext first, InvalidTicketContext<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(InvalidTicketContext first, InvalidTicketContext<T> second)
        {
            return !(first == second);
        }
        
        InvalidTicketContext InvalidTicketContext.ITicketContext.Ticket
        {
            get
            {
                return this;
            }
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
        public IdentityException<T> Exception { get; private init; }

        IIdentityException InvalidTicketContext.ITicketContext.Exception
        {
            get
            {
                return Exception;
            }
        }

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

        public InvalidTicketContext(HttpContext context, IdentityException<T> exception)
            : this(context, null, exception)
        {
        }

        public InvalidTicketContext(HttpContext context, JWTAuthenticationOptions? options, IdentityException<T> exception)
            : this(null, context, options, exception)
        {
        }

        public InvalidTicketContext(ILogger? logger, HttpContext context, IdentityException<T> exception)
            : this(logger, context, null, exception)
        {
        }

        public InvalidTicketContext(ILogger? logger, HttpContext context, JWTAuthenticationOptions? options, IdentityException<T> exception)
        {
            Logger = logger;
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Options = options;
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        internal static InvalidTicketContext<T> From(SuccessTicketContext context, IdentityException<T> exception)
        {
            return new InvalidTicketContext<T>
            {
                Logger = context.Logger,
                Context = context.Context,
                Options = context.Options,
                Exception = exception ?? throw new ArgumentNullException(nameof(exception))
            };
        }

        internal static InvalidTicketContext<T> From(InvalidTicketContext context)
        {
            return new InvalidTicketContext<T>
            {
                Logger = context.Logger,
                Context = context.Context,
                Options = context.Options,
                Exception = context.Exception switch
                {
                    null => null!,
                    IdentityException<T> exception => exception,
                    IBusinessException<T> exception => new IdentityUnknownException<T>(exception.Message, exception.Code, (Exception) exception) { Status = exception.Status },
                    { } exception when typeof(T) == typeof(String) => (IdentityException<T>) (Object) new IdentityUnknownException(exception.Message, (Exception) exception) { Status = exception.Status },
                    { } exception => new IdentityUnknownException<T>(exception.Message, default!, (Exception) exception) { Status = exception.Status }
                }
            };
        }

        internal static InvalidTicketContext<T> From(FailTicketContext context)
        {
            return new InvalidTicketContext<T>
            {
                Logger = context.Logger,
                Context = context.Context,
                Options = context.Options,
                Exception = context.Exception switch
                {
                    null => null!,
                    IdentityException<T> exception => exception,
                    IBusinessException<T> exception => new IdentityUnknownException<T>(exception.Message, exception.Code, (Exception) exception) { Status = exception.Status },
                    IBusinessException exception when typeof(T) == typeof(String) => (IdentityException<T>) (Object) new IdentityUnknownException(exception.Message, (Exception) exception) { Status = exception.Status },
                    IBusinessException exception => new IdentityUnknownException<T>(exception.Message, default!, (Exception) exception) { Status = exception.Status },
                    { } exception when typeof(T) == typeof(String) => (IdentityException<T>) (Object) new IdentityUnknownException(exception.Message, exception),
                    { } exception => new IdentityUnknownException<T>(exception.Message, default!, exception)
                }
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
                InvalidTicketContext<T> value => Equals(value),
                InvalidTicketContext value => Equals(value),
                FailTicketContext value => Equals(value),
                SuccessTicketContext value => Equals(value),
                InvalidTicketContext.ITicketContext value => Equals(value.Ticket),
                ITicketContext value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(InvalidTicketContext<T> other)
        {
            return Context == other.Context && Options == other.Options && Exception == other.Exception;
        }

        public Boolean Equals(InvalidTicketContext other)
        {
            return Context == other.Context && Options == other.Options && Exception == other.Exception;
        }

        public Boolean Equals(FailTicketContext other)
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
                InvalidTicketContext<T> value => Equals(value),
                InvalidTicketContext value => Equals(value),
                FailTicketContext value => Equals(value),
                SuccessTicketContext value => Equals(value),
                InvalidTicketContext.ITicketContext value => Equals(value.Ticket),
                _ => false
            };
        }
    }

    public readonly struct InvalidTicketContext : InvalidTicketContext.ITicketContext, IEquatableStruct<InvalidTicketContext>, IEquatable<FailTicketContext>, IEquatable<SuccessTicketContext>
    {
        internal interface ITicketContext : Interfaces.ITicketContext
        {
            public InvalidTicketContext Ticket { get; }
            public IIdentityException Exception { get; }
        }

        public static implicit operator Boolean(InvalidTicketContext value)
        {
            return !value.IsEmpty;
        }

        public static implicit operator Exception(InvalidTicketContext value)
        {
            return value ? value.Exception.Exception : new IdentityNoContextException();
        }

        public static explicit operator FailTicketContext(InvalidTicketContext value)
        {
            return FailTicketContext.From(value);
        }

        public static Boolean operator ==(InvalidTicketContext first, InvalidTicketContext second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(InvalidTicketContext first, InvalidTicketContext second)
        {
            return !(first == second);
        }

        InvalidTicketContext ITicketContext.Ticket
        {
            get
            {
                return this;
            }
        }

        public ILogger? Logger { get; private init; }
        public HttpContext Context { get; private init; }

        HttpContext Interfaces.ITicketContext.Context
        {
            get
            {
                return Context ?? throw new IdentityNoContextException();
            }
        }
        
        public JWTAuthenticationOptions? Options { get; private init; }
        internal IIdentityException Exception { get; private init; }

        IIdentityException ITicketContext.Exception
        {
            get
            {
                return Exception;
            }
        }

        Boolean Interfaces.ITicketContext.IsSuccess
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

        public InvalidTicketContext(HttpContext context, IIdentityException exception)
            : this(context, null, exception)
        {
        }

        public InvalidTicketContext(HttpContext context, JWTAuthenticationOptions? options, IIdentityException exception)
            : this(null, context, options, exception)
        {
        }

        public InvalidTicketContext(ILogger? logger, HttpContext context, IIdentityException exception)
            : this(logger, context, null, exception)
        {
        }

        public InvalidTicketContext(ILogger? logger, HttpContext context, JWTAuthenticationOptions? options, IIdentityException exception)
        {
            Logger = logger;
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Options = options;
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        internal static InvalidTicketContext From(SuccessTicketContext context, IIdentityException exception)
        {
            return new InvalidTicketContext
            {
                Logger = context.Logger,
                Context = context.Context,
                Options = context.Options,
                Exception = exception ?? throw new ArgumentNullException(nameof(exception))
            };
        }

        internal static InvalidTicketContext From<T>(InvalidTicketContext<T> context)
        {
            return new InvalidTicketContext
            {
                Logger = context.Logger,
                Context = context.Context,
                Options = context.Options,
                Exception = context.Exception
            };
        }

        internal static InvalidTicketContext From(FailTicketContext context)
        {
            return new InvalidTicketContext
            {
                Logger = context.Logger,
                Context = context.Context,
                Options = context.Options,
                Exception = context.Exception switch
                {
                    null => null!,
                    IIdentityException exception => exception,
                    IBusinessException<String> exception => new IdentityUnknownException<String>(exception.Message, exception.Code, (Exception) exception) { Status = exception.Status },
                    IBusinessException exception => new IdentityUnknownException(exception.Message, (Exception) exception) { Status = exception.Status },
                    { } exception => new IdentityUnknownException(exception.Message, exception)
                }
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
                InvalidTicketContext value => Equals(value),
                FailTicketContext value => Equals(value),
                SuccessTicketContext value => Equals(value),
                Interfaces.ITicketContext value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(InvalidTicketContext other)
        {
            return Context == other.Context && Options == other.Options && Exception == other.Exception;
        }

        public Boolean Equals(FailTicketContext other)
        {
            return Context == other.Context && Options == other.Options && Exception == other.Exception;
        }

        public Boolean Equals(SuccessTicketContext other)
        {
            return false;
        }

        public Boolean Equals(Interfaces.ITicketContext? other)
        {
            return other switch
            {
                null => IsEmpty,
                InvalidTicketContext value => Equals(value),
                FailTicketContext value => Equals(value),
                SuccessTicketContext value => Equals(value),
                ITicketContext value => Equals(value.Ticket),
                _ => false
            };
        }
    }
}
