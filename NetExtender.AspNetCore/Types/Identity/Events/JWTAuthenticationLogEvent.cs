// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.AspNetCore.Identity
{
    public sealed class JWTAuthenticationLogEvent : IJWTAuthenticationEvent
    {
        private static Action<ILogger, Exception?> DefaultSuccessTicketHandler { get; }
        private static Action<ILogger, String, Exception?> DefaultInvalidTicketHandler { get; }
        private static Action<ILogger, String, Exception?> DefaultFailTicketHandler { get; }
        private static Action<ILogger, Exception?> DefaultMissingHeaderHandler { get; }
        private static Action<ILogger, Exception?> DefaultInvalidHeaderHandler { get; }
        private static Action<ILogger, String, String, Exception?> DefaultInvalidSchemeHandler { get; }

        static JWTAuthenticationLogEvent()
        {
            DefaultSuccessTicketHandler = LoggerMessage.Define(LogLevel.Information, Events.SuccessTicket, "Successful decoded JWT. Return: 'Success'.");
            DefaultInvalidTicketHandler = LoggerMessage.Define<String>(LogLevel.Information, Events.InvalidTicket, "JWT invalid ticket: {Exception}. Return: 'Fail'.");
            DefaultFailTicketHandler = LoggerMessage.Define<String>(LogLevel.Information, Events.FailTicket, "JWT error: {Exception}. Return: 'Fail'.");
            DefaultMissingHeaderHandler = LoggerMessage.Define(LogLevel.Information, Events.MissingHeader, $"Header '{nameof(HeaderNames.Authorization)}' is missing. Return: 'Null'.");
            DefaultInvalidHeaderHandler = LoggerMessage.Define(LogLevel.Information, Events.InvalidHeader, $"Token in header '{nameof(HeaderNames.Authorization)}' is invalid. Return 'Null'.");
            DefaultInvalidSchemeHandler = LoggerMessage.Define<String, String>(LogLevel.Information, Events.InvalidScheme, $"Header '{nameof(HeaderNames.Authorization)}' scheme is {{Scheme}}, expected {{Expectation}}. Return: 'Null'.");
        }

        public Action<ILogger, Exception?>? SuccessTicketHandler { get; init; } = DefaultSuccessTicketHandler;
        public Action<ILogger, String, Exception?>? InvalidTicketHandler { get; init; } = DefaultInvalidTicketHandler;
        public Action<ILogger, String, Exception?>? FailTicketHandler { get; init; } = DefaultFailTicketHandler;
        public Action<ILogger, Exception?>? MissingHeaderHandler { get; init; } = DefaultMissingHeaderHandler;
        public Action<ILogger, Exception?>? InvalidHeaderHandler { get; init; } = DefaultInvalidHeaderHandler;
        public Action<ILogger, String, String, Exception?>? InvalidSchemeHandler { get; init; } = DefaultInvalidSchemeHandler;

        public ValueTask<AuthenticateResult> SuccessTicket(SuccessTicketContext context)
        {
            if (context.Logger is { } logger)
            {
                SuccessTicketHandler?.Invoke(logger, null);
            }
            
            return ValueTask.FromResult(AuthenticateResult.Success(context.Ticket));
        }

        public ValueTask<AuthenticateResult> InvalidTicket(InvalidTicketContext context)
        {
            if (context.Logger is { } logger)
            {
                InvalidTicketHandler?.Invoke(logger, context.Exception.Message, (Exception) context.Exception);
            }
            
            return ValueTask.FromResult(AuthenticateResult.Fail((Exception) context.Exception));
        }

        public ValueTask<AuthenticateResult> FailTicket(FailTicketContext context)
        {
            if (context.Logger is { } logger)
            {
                FailTicketHandler?.Invoke(logger, context.Exception.Message, context.Exception);
            }
            
            return ValueTask.FromResult(AuthenticateResult.Fail(context.Exception));
        }

        public ValueTask<AuthenticateResult> MissingHeader()
        {
            return MissingHeader(null);
        }

        public ValueTask<AuthenticateResult> MissingHeader(ILogger? logger)
        {
            if (logger is not null)
            {
                MissingHeaderHandler?.Invoke(logger, null);
            }
            
            return ValueTask.FromResult(AuthenticateResult.NoResult());
        }

        public ValueTask<AuthenticateResult> InvalidHeader(String header)
        {
            return InvalidHeader(null, header);
        }

        public ValueTask<AuthenticateResult> InvalidHeader(ILogger? logger, String header)
        {
            if (logger is not null)
            {
                InvalidHeaderHandler?.Invoke(logger, null);
            }
            
            return ValueTask.FromResult(AuthenticateResult.NoResult());
        }

        public ValueTask<AuthenticateResult> InvalidScheme(String scheme, String expect)
        {
            return InvalidScheme(null, scheme, expect);
        }

        public ValueTask<AuthenticateResult> InvalidScheme(ILogger? logger, String scheme, String expect)
        {
            if (logger is not null)
            {
                InvalidSchemeHandler?.Invoke(logger, scheme, expect, null);
            }

            return ValueTask.FromResult(AuthenticateResult.NoResult());
        }
        
        private static class Events
        {
            [ReflectionSignature]
            public static EventId SuccessTicket
            {
                get
                {
                    return 1;
                }
            }

            [ReflectionSignature]
            public static EventId InvalidTicket
            {
                get
                {
                    return 2;
                }
            }

            [ReflectionSignature]
            public static EventId FailTicket
            {
                get
                {
                    return 3;
                }
            }

            [ReflectionSignature]
            public static EventId MissingHeader
            {
                get
                {
                    return 10;
                }
            }

            [ReflectionSignature]
            public static EventId InvalidHeader
            {
                get
                {
                    return 12;
                }
            }

            [ReflectionSignature]
            public static EventId InvalidScheme
            {
                get
                {
                    return 11;
                }
            }
        }
    }
}
