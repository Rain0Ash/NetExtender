using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Exceptions.Handlers;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Network;
using Newtonsoft.Json;

namespace NetExtender.Types.Network
{
    [SuppressMessage("ReSharper", "CognitiveComplexity")]
    public abstract class HttpRequestHandler : ExceptionHandler
    {
        public override Boolean Invoke<T>(Func<T> request, [MaybeNullWhen(false)] out T result)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                result = request.Invoke();
                return true;
            }
            catch (HttpRequestException exception) when (exception.IsSocketException(out SocketException? socket))
            {
                ExceptionHandlerAction action = Handle(socket);
                start:
                switch (action)
                {
                    case ExceptionHandlerAction.Successful:
                    case ExceptionHandlerAction.Ignore:
                        result = default;
                        return false;
                    case ExceptionHandlerAction.Default:
                        action = Handle((Exception) exception) switch
                        {
                            ExceptionHandlerAction.Default => ExceptionHandlerAction.Ignore,
                            var value => value
                        };
                        
                        goto start;
                    case ExceptionHandlerAction.Throw:
                        throw socket;
                    case ExceptionHandlerAction.Rethrow:
                        throw;
                    default:
                        throw new EnumUndefinedOrNotSupportedThrowableException<ExceptionHandlerAction>(action, nameof(action), null);
                }
            }
            catch (HttpRequestException exception) when (exception.IsIOException(out IOException? io))
            {
                ExceptionHandlerAction action = Handle(io);
                start:
                switch (action)
                {
                    case ExceptionHandlerAction.Successful:
                    case ExceptionHandlerAction.Ignore:
                        result = default;
                        return false;
                    case ExceptionHandlerAction.Default:
                        action = Handle((Exception) exception) switch
                        {
                            ExceptionHandlerAction.Default => ExceptionHandlerAction.Ignore,
                            var value => value
                        };
                        
                        goto start;
                    case ExceptionHandlerAction.Throw:
                        throw io;
                    case ExceptionHandlerAction.Rethrow:
                        throw;
                    default:
                        throw new EnumUndefinedOrNotSupportedThrowableException<ExceptionHandlerAction>(action, nameof(action), null);
                }
            }
            catch (HttpRequestException exception)
            {
                ExceptionHandlerAction action = Handle(exception);
                start:
                switch (action)
                {
                    case ExceptionHandlerAction.Successful:
                    case ExceptionHandlerAction.Ignore:
                        result = default;
                        return false;
                    case ExceptionHandlerAction.Default:
                        action = Handle((Exception) exception) switch
                        {
                            ExceptionHandlerAction.Default => ExceptionHandlerAction.Ignore,
                            var value => value
                        };
                        
                        goto start;
                    case ExceptionHandlerAction.Throw:
                    case ExceptionHandlerAction.Rethrow:
                        throw;
                    default:
                        throw new EnumUndefinedOrNotSupportedThrowableException<ExceptionHandlerAction>(action, nameof(action), null);
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlerAction action = Handle(exception);

                switch (action)
                {
                    case ExceptionHandlerAction.Successful:
                    case ExceptionHandlerAction.Ignore:
                    case ExceptionHandlerAction.Default:
                        result = default;
                        return false;
                    case ExceptionHandlerAction.Throw:
                    case ExceptionHandlerAction.Rethrow:
                        throw;
                    default:
                        throw new EnumUndefinedOrNotSupportedThrowableException<ExceptionHandlerAction>(action, nameof(action), null);
                }
            }
            finally
            {
                Finally();
            }
        }

        public override async ValueTask<Maybe<T>> Invoke<T>(Func<Task<T>> request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                return await request.Invoke().ConfigureAwait(false);
            }
            catch (HttpRequestException exception) when (exception.IsSocketException(out SocketException? socket))
            {
                ExceptionHandlerAction action = Handle(socket);
                start:
                switch (action)
                {
                    case ExceptionHandlerAction.Successful:
                    case ExceptionHandlerAction.Ignore:
                        return default;
                    case ExceptionHandlerAction.Default:
                        action = Handle((Exception) exception) switch
                        {
                            ExceptionHandlerAction.Default => ExceptionHandlerAction.Ignore,
                            var value => value
                        };

                        goto start;
                    case ExceptionHandlerAction.Throw:
                        throw socket;
                    case ExceptionHandlerAction.Rethrow:
                        throw;
                    default:
                        throw new EnumUndefinedOrNotSupportedThrowableException<ExceptionHandlerAction>(action, nameof(action), null);
                }
            }
            catch (HttpRequestException exception) when (exception.IsIOException(out IOException? io))
            {
                ExceptionHandlerAction action = Handle(io);
                start:
                switch (action)
                {
                    case ExceptionHandlerAction.Successful:
                    case ExceptionHandlerAction.Ignore:
                        return default;
                    case ExceptionHandlerAction.Default:
                        action = Handle((Exception) exception) switch
                        {
                            ExceptionHandlerAction.Default => ExceptionHandlerAction.Ignore,
                            var value => value
                        };

                        goto start;
                    case ExceptionHandlerAction.Throw:
                        throw io;
                    case ExceptionHandlerAction.Rethrow:
                        throw;
                    default:
                        throw new EnumUndefinedOrNotSupportedThrowableException<ExceptionHandlerAction>(action, nameof(action), null);
                }
            }
            catch (HttpRequestException exception)
            {
                ExceptionHandlerAction action = Handle(exception);
                start:
                switch (action)
                {
                    case ExceptionHandlerAction.Successful:
                    case ExceptionHandlerAction.Ignore:
                        return default;
                    case ExceptionHandlerAction.Default:
                        action = Handle((Exception) exception) switch
                        {
                            ExceptionHandlerAction.Default => ExceptionHandlerAction.Ignore,
                            var value => value
                        };

                        goto start;
                    case ExceptionHandlerAction.Throw:
                    case ExceptionHandlerAction.Rethrow:
                        throw;
                    default:
                        throw new EnumUndefinedOrNotSupportedThrowableException<ExceptionHandlerAction>(action, nameof(action), null);
                }
            }
            catch (JsonException exception)
            {
                ExceptionHandlerAction action = Handle(exception);
                start:
                switch (action)
                {
                    case ExceptionHandlerAction.Successful:
                    case ExceptionHandlerAction.Ignore:
                        return default;
                    case ExceptionHandlerAction.Default:
                        action = Handle((Exception) exception) switch
                        {
                            ExceptionHandlerAction.Default => ExceptionHandlerAction.Ignore,
                            var value => value
                        };

                        goto start;
                    case ExceptionHandlerAction.Throw:
                    case ExceptionHandlerAction.Rethrow:
                        throw;
                    default:
                        throw new EnumUndefinedOrNotSupportedThrowableException<ExceptionHandlerAction>(action, nameof(action), null);
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlerAction action = Handle(exception);

                switch (action)
                {
                    case ExceptionHandlerAction.Successful:
                    case ExceptionHandlerAction.Ignore:
                    case ExceptionHandlerAction.Default:
                        return default;
                    case ExceptionHandlerAction.Throw:
                    case ExceptionHandlerAction.Rethrow:
                        throw;
                    default:
                        throw new EnumUndefinedOrNotSupportedThrowableException<ExceptionHandlerAction>(action, nameof(action), null);
                }
            }
            finally
            {
                Finally();
            }
        }

        protected virtual ExceptionHandlerAction Handle(SocketException? exception)
        {
            return exception is null ? ExceptionHandlerAction.Ignore : ExceptionHandlerAction.Default;
        }

        protected virtual ExceptionHandlerAction Handle(IOException? exception)
        {
            return exception is null ? ExceptionHandlerAction.Ignore : ExceptionHandlerAction.Default;
        }

        protected virtual ExceptionHandlerAction Handle(HttpRequestException? exception)
        {
            return exception is null ? ExceptionHandlerAction.Ignore : ExceptionHandlerAction.Default;
        }

        protected virtual ExceptionHandlerAction Handle(JsonException? exception)
        {
            return exception is null ? ExceptionHandlerAction.Ignore : ExceptionHandlerAction.Default;
        }
    }

    public class DynamicHttpRequestHandler : HttpRequestHandler
    {
        public Func<SocketException, ExceptionHandlerAction>? SocketHandler { get; init; }
        public Func<IOException, ExceptionHandlerAction>? IOHandler { get; init; }
        public Func<HttpRequestException, ExceptionHandlerAction>? BadRequestHandler { get; init; }
        public Func<HttpRequestException, ExceptionHandlerAction>? ForbiddenHandler { get; init; }
        public Func<HttpRequestException, ExceptionHandlerAction>? NotFoundHandler { get; init; }
        public Func<HttpRequestException, ExceptionHandlerAction>? InternalErrorHandler { get; init; }
        public Func<HttpRequestException, ExceptionHandlerAction>? ServiceUnavailableHandler { get; init; }
        public Func<HttpRequestException, ExceptionHandlerAction>? ClientErrorHandler { get; init; }
        public Func<HttpRequestException, ExceptionHandlerAction>? ServerErrorHandler { get; init; }
        public Func<HttpRequestException, ExceptionHandlerAction>? HttpHandler { get; init; }
        public Func<JsonException, ExceptionHandlerAction>? JsonHandler { get; init; }
        public Func<Exception, ExceptionHandlerAction>? DefaultHandler { get; init; }
        public Action? FinallyHandler { get; init; }
        
        protected override ExceptionHandlerAction Handle(SocketException? exception)
        {
            return exception switch
            {
                null => ExceptionHandlerAction.Ignore,
                _ when SocketHandler is not null => SocketHandler(exception),
                _ => ExceptionHandlerAction.Default
            };
        }

        protected override ExceptionHandlerAction Handle(IOException? exception)
        {
            return exception switch
            {
                null => ExceptionHandlerAction.Ignore,
                _ when IOHandler is not null => IOHandler(exception),
                _ => ExceptionHandlerAction.Default
            };
        }

        // ReSharper disable once CognitiveComplexity
        protected override ExceptionHandlerAction Handle(HttpRequestException? exception)
        {
            switch (exception)
            {
                case null:
                    return ExceptionHandlerAction.Ignore;
                case var _ when exception.StatusCode == HttpStatusCode.BadRequest && BadRequestHandler is not null:
                {
                    ExceptionHandlerAction result = BadRequestHandler(exception);
                    if (result == ExceptionHandlerAction.Default)
                    {
                        goto ClientError;
                    }

                    return result;
                }
                case var _ when exception.StatusCode == HttpStatusCode.Forbidden && ForbiddenHandler is not null:
                {
                    ExceptionHandlerAction result = ForbiddenHandler(exception);
                    if (result == ExceptionHandlerAction.Default)
                    {
                        goto ClientError;
                    }

                    return result;
                }
                case var _ when exception.StatusCode == HttpStatusCode.NotFound && NotFoundHandler is not null:
                {
                    ExceptionHandlerAction result = NotFoundHandler(exception);
                    if (result == ExceptionHandlerAction.Default)
                    {
                        goto ClientError;
                    }

                    return result;
                }
                case var _ when exception.StatusCode.IsClientError(): ClientError:
                {
                    ExceptionHandlerAction result = ClientErrorHandler?.Invoke(exception) ?? ExceptionHandlerAction.Default;
                    if (result == ExceptionHandlerAction.Default)
                    {
                        goto Default;
                    }

                    return result;
                }
                case var _ when exception.StatusCode == HttpStatusCode.InternalServerError && InternalErrorHandler is not null:
                {
                    ExceptionHandlerAction result = InternalErrorHandler(exception);
                    if (result == ExceptionHandlerAction.Default)
                    {
                        goto ServerError;
                    }

                    return result;
                }
                case var _ when exception.StatusCode == HttpStatusCode.ServiceUnavailable && ServiceUnavailableHandler is not null:
                {
                    ExceptionHandlerAction result = ServiceUnavailableHandler(exception);
                    if (result == ExceptionHandlerAction.Default)
                    {
                        goto ServerError;
                    }

                    return result;
                }
                case var _ when exception.StatusCode.IsServerError(): ServerError:
                {
                    ExceptionHandlerAction result = ServerErrorHandler?.Invoke(exception) ?? ExceptionHandlerAction.Default;
                    if (result == ExceptionHandlerAction.Default)
                    {
                        goto Default;
                    }

                    return result;
                }
                default: Default:
                    return HttpHandler?.Invoke(exception) ?? ExceptionHandlerAction.Default;
            }
        }

        protected override ExceptionHandlerAction Handle(JsonException? exception)
        {
            return exception switch
            {
                null => ExceptionHandlerAction.Ignore,
                _ when JsonHandler is not null => JsonHandler(exception),
                _ => ExceptionHandlerAction.Default
            };
        }

        protected override ExceptionHandlerAction Handle(Exception? exception)
        {
            return exception switch
            {
                null => ExceptionHandlerAction.Ignore,
                _ when DefaultHandler is not null => DefaultHandler(exception),
                _ => ExceptionHandlerAction.Rethrow
            };
        }

        protected override void Finally()
        {
            FinallyHandler?.Invoke();
        }
    }
}