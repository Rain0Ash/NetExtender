using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using NetExtender.Types.Exceptions.Handlers;
using NetExtender.Types.Network.Exceptions;
using NetExtender.Types.Network.Formatters.Exceptions;
using NetExtender.Types.Network.Interfaces;
using NetExtender.Utilities.Network;
using Newtonsoft.Json;

namespace NetExtender.Types.Network
{
    public class DynamicHttpRequestHandler : HttpRequestHandler, IDynamicHttpRequestHandler
    {
        protected ConcurrentDictionary<HttpStatusCode, HttpExceptionHandler> Handlers { get; } = new ConcurrentDictionary<HttpStatusCode, HttpExceptionHandler>();
        
        public Int32 Count
        {
            get
            {
                return Handlers.Count;
            }
        }
        
        public ICollection<HttpStatusCode> Keys
        {
            get
            {
                return Handlers.Keys;
            }
        }
        
        public ICollection<HttpExceptionHandler> Values
        {
            get
            {
                return Handlers.Values;
            }
        }
        
        ICollection<HttpExceptionHandler?> IDictionary<HttpStatusCode, HttpExceptionHandler?>.Values
        {
            get
            {
                return Values!;
            }
        }
        
        Boolean ICollection<KeyValuePair<HttpStatusCode, HttpExceptionHandler?>>.IsReadOnly
        {
            get
            {
                return ((IDynamicHttpRequestHandler) this).IsReadOnly;
            }
        }
        
        public Func<SocketException, ExceptionHandlerAction>? SocketHandler { get; set; }
        public Func<IOException, ExceptionHandlerAction>? IOHandler { get; set; }
        public Func<HttpMessageParsingException, ExceptionHandlerAction>? ParsingHandler { get; set; }
        public Func<MediaTypeNotSupportedException, ExceptionHandlerAction>? MediaTypeHandler { get; set; }
        public HttpExceptionHandler? ClientErrorHandler { get; set; }
        public HttpExceptionHandler? ServerErrorHandler { get; set; }
        public HttpExceptionHandler? HttpHandler { get; set; }
        public Func<JsonException, ExceptionHandlerAction>? JsonHandler { get; set; }
        public Func<Exception, ExceptionHandlerAction>? DefaultHandler { get; set; }
        public Action? FinallyHandler { get; set; }
        
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
        
        protected override ExceptionHandlerAction Handle(HttpMessageParsingException? exception)
        {
            return exception switch
            {
                null => ExceptionHandlerAction.Ignore,
                _ when ParsingHandler is not null => ParsingHandler(exception),
                _ => ExceptionHandlerAction.Default
            };
        }
        
        protected override ExceptionHandlerAction Handle(MediaTypeNotSupportedException? exception)
        {
            return exception switch
            {
                null => ExceptionHandlerAction.Ignore,
                _ when MediaTypeHandler is not null => MediaTypeHandler(exception),
                _ => ExceptionHandlerAction.Default
            };
        }
        
        // ReSharper disable once CognitiveComplexity
        protected override ExceptionHandlerAction Handle(HttpRequestException? exception)
        {
            switch (exception)
            {
                case null:
                {
                    return ExceptionHandlerAction.Ignore;
                }
                case var _ when exception.StatusCode is { } code && code.IsClientError() && Handlers.TryGetValue(code, out HttpExceptionHandler? handler):
                {
                    ExceptionHandlerAction result = handler(exception);
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
                case var _ when exception.StatusCode is { } code && code.IsServerError() && Handlers.TryGetValue(code, out HttpExceptionHandler? handler):
                {
                    ExceptionHandlerAction result = handler(exception);
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
                {
                    return HttpHandler?.Invoke(exception) ?? ExceptionHandlerAction.Default;
                }
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
        
        public Boolean Contains(HttpStatusCode code)
        {
            return ContainsKey(code);
        }
        
        public Boolean ContainsKey(HttpStatusCode code)
        {
            return Handlers.ContainsKey(code);
        }
        
        Boolean ICollection<KeyValuePair<HttpStatusCode, HttpExceptionHandler?>>.Contains(KeyValuePair<HttpStatusCode, HttpExceptionHandler?> item)
        {
            return ((ICollection<KeyValuePair<HttpStatusCode, HttpExceptionHandler?>>) Handlers).Contains(item);
        }
        
        public Boolean TryGetValue(HttpStatusCode code, [MaybeNullWhen(false)] out HttpExceptionHandler result)
        {
            return Handlers.TryGetValue(code, out result);
        }
        
        public void Add(HttpStatusCode code, HttpExceptionHandler? handler)
        {
            if (handler is not null && !TryAdd(code, handler))
            {
                throw new ArgumentException($"Duplicate handler for status code '{code}'.");
            }
        }
        
        public Boolean TryAdd(HttpStatusCode code, HttpExceptionHandler? handler)
        {
            return handler is not null && Handlers.TryAdd(code, handler);
        }
        
        void ICollection<KeyValuePair<HttpStatusCode, HttpExceptionHandler?>>.Add(KeyValuePair<HttpStatusCode, HttpExceptionHandler?> item)
        {
            ((ICollection<KeyValuePair<HttpStatusCode, HttpExceptionHandler?>>) Handlers).Add(item);
        }
        
        public Boolean Remove(HttpStatusCode code)
        {
            return Remove(code, out _);
        }
        
        public Boolean Remove(HttpStatusCode code, [MaybeNullWhen(false)] out HttpExceptionHandler result)
        {
            return Handlers.TryRemove(code, out result);
        }
        
        Boolean ICollection<KeyValuePair<HttpStatusCode, HttpExceptionHandler?>>.Remove(KeyValuePair<HttpStatusCode, HttpExceptionHandler?> item)
        {
            return ((ICollection<KeyValuePair<HttpStatusCode, HttpExceptionHandler?>>) Handlers).Remove(item);
        }
        
        public void Clear()
        {
            Handlers.Clear();
        }
        
        public void CopyTo(HttpStatusCode[] array, Int32 arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            
            Handlers.Keys.CopyTo(array, arrayIndex);
        }
        
        public void CopyTo(KeyValuePair<HttpStatusCode, HttpExceptionHandler>[] array, Int32 arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            
            ((ICollection<KeyValuePair<HttpStatusCode, HttpExceptionHandler>>) Handlers).CopyTo(array, arrayIndex);
        }
        
        void ICollection<KeyValuePair<HttpStatusCode, HttpExceptionHandler?>>.CopyTo(KeyValuePair<HttpStatusCode, HttpExceptionHandler?>[] array, Int32 arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            
            ((ICollection<KeyValuePair<HttpStatusCode, HttpExceptionHandler?>>) Handlers).CopyTo(array, arrayIndex);
        }
        
        public IEnumerator<KeyValuePair<HttpStatusCode, HttpExceptionHandler>> GetEnumerator()
        {
            return Handlers.GetEnumerator();
        }
        
        IEnumerator<KeyValuePair<HttpStatusCode, HttpExceptionHandler?>> IEnumerable<KeyValuePair<HttpStatusCode, HttpExceptionHandler?>>.GetEnumerator()
        {
            return GetEnumerator()!;
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Handlers).GetEnumerator();
        }
        
        public HttpExceptionHandler? this[HttpStatusCode code]
        {
            get
            {
                return Handlers.TryGetValue(code, out HttpExceptionHandler? handler) ? handler : null;
            }
            set
            {
                if (value is null)
                {
                    Handlers.TryRemove(code, out _);
                    return;
                }
                
                Handlers[code] = value;
            }
        }
    }
}