using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Exceptions.Handlers;
using NetExtender.Types.Monads;
using NetExtender.Types.Network.Exceptions;
using NetExtender.Types.Network.Formatters.Exceptions;
using NetExtender.Utilities.Network;
using Newtonsoft.Json;

namespace NetExtender.Types.Network
{
    public delegate ExceptionHandlerAction HttpExceptionHandler(HttpRequestException? exception);
    
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
            catch (HttpMessageParsingException parsing)
            {
                ExceptionHandlerAction action = Handle(parsing);
                start:
                switch (action)
                {
                    case ExceptionHandlerAction.Successful:
                    case ExceptionHandlerAction.Ignore:
                        result = default;
                        return false;
                    case ExceptionHandlerAction.Default:
                        action = Handle((Exception) parsing) switch
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
            catch (MediaTypeNotSupportedException media)
            {
                ExceptionHandlerAction action = Handle(media);
                start:
                switch (action)
                {
                    case ExceptionHandlerAction.Successful:
                    case ExceptionHandlerAction.Ignore:
                        result = default;
                        return false;
                    case ExceptionHandlerAction.Default:
                        action = Handle((Exception) media) switch
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
            catch (HttpMessageParsingException parsing)
            {
                ExceptionHandlerAction action = Handle(parsing);
                start:
                switch (action)
                {
                    case ExceptionHandlerAction.Successful:
                    case ExceptionHandlerAction.Ignore:
                        return default;
                    case ExceptionHandlerAction.Default:
                        action = Handle((Exception) parsing) switch
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
            catch (MediaTypeNotSupportedException media)
            {
                ExceptionHandlerAction action = Handle(media);
                start:
                switch (action)
                {
                    case ExceptionHandlerAction.Successful:
                    case ExceptionHandlerAction.Ignore:
                        return default;
                    case ExceptionHandlerAction.Default:
                        action = Handle((Exception) media) switch
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

        protected virtual ExceptionHandlerAction Handle(HttpMessageParsingException? exception)
        {
            return exception is null ? ExceptionHandlerAction.Ignore : ExceptionHandlerAction.Default;
        }

        protected virtual ExceptionHandlerAction Handle(MediaTypeNotSupportedException? exception)
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
}