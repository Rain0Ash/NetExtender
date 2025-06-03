using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using NetExtender.CQRS.Interfaces;
using NetExtender.Types.Network.API.Interfaces;
using NetExtender.Utilities.Network;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Network.API
{
    //TODO:
    public class DynamicHttpApiEndpoint<TRequest, TResponse> : DynamicHttpApiEndpointAbstraction<TRequest, TResponse> where TRequest : IEntityCQRS
    { 
        public new virtual String? Controller
        {
            get
            {
                return base.Controller;
            }
            init
            {
                Unsafe.AsRef(_controller) = value;
            }
        }
        
        public new virtual String? Name
        {
            get
            {
                return base.Name;
            }
            init
            {
                Unsafe.AsRef(_name) = value;
            }
        }
        
        public new virtual HttpMethod Method
        {
            get
            {
                return base.Method;
            }
            init
            {
                Unsafe.AsRef(_method) = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
        
        public new virtual HttpMethodType MethodType
        {
            get
            {
                return base.MethodType;
            }
            init
            {
                Method = value.Method();
            }
        }
        
        public DynamicHttpApiEndpoint()
            : base(null, null, null!, null!)
        {
        }
        
        public DynamicHttpApiEndpoint(Uri endpoint, HttpMethod method)
            : this(null, null, endpoint, method)
        {
        }

        public DynamicHttpApiEndpoint(String? name, Uri endpoint, HttpMethod method)
            : this(null, name, endpoint, method)
        {
        }

        public DynamicHttpApiEndpoint(String? controller, String? name, Uri endpoint, HttpMethod method)
            : base(controller, name, endpoint, method)
        {
        }
    }
    
    public class DynamicHttpApiEndpointAbstraction<TRequest, TResponse> : HttpApiEndpoint<TRequest, TResponse> where TRequest : IEntityCQRS
    {
        private protected readonly String? _controller;
        public sealed override String? Controller
        {
            get
            {
                return _controller;
            }
        }
        
        private protected readonly String? _name;
        public sealed override String? Name
        {
            get
            {
                return _name;
            }
        }
        
        private protected readonly HttpMethod? _method;
        public sealed override HttpMethod Method
        {
            get
            {
                return _method!;
            }
        }

        public sealed override HttpMethodType MethodType
        {
            get
            {
                return base.MethodType;
            }
        }
        
        private protected readonly Uri? _endpoint;
        public sealed override Uri Endpoint
        {
            get
            {
                return _endpoint!;
            }
        }
        
        private protected readonly String? _description;
        public sealed override String? Description
        {
            get
            {
                return _description;
            }
        }

        internal override Boolean IsEmpty
        {
            get
            {
                return _method is null || _endpoint is null;
            }
        }

        public DynamicHttpApiEndpointAbstraction(String? controller, String? name, Uri endpoint, HttpMethod method)
        {
            _controller = controller;
            _name = name;
            _endpoint = endpoint;
            _method = method;
        }
    }
    
    public abstract class HttpApiEndpoint<TRequest, TResponse> : IHttpApiEndpoint<TRequest, TResponse> where TRequest : IEntityCQRS
    {
        public abstract String? Controller { get; }
        public abstract String? Name { get; }
        public abstract HttpMethod Method { get; }

        public virtual HttpMethodType MethodType
        {
            get
            {
                return Method.Method();
            }
        }

        public abstract Uri Endpoint { get; }

        public virtual HttpRequestHandler? Handler
        {
            get
            {
                return null;
            }
        }

        public abstract String? Description { get; }

        internal abstract Boolean IsEmpty { get; }

        Boolean IHttpApiEndpoint.IsEmpty
        {
            get
            {
                return IsEmpty;
            }
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Controller, Name, Method, Endpoint);
        }

        public override Boolean Equals(Object? other)
        {
            return Equals(other as IHttpApiEndpoint);
        }

        public Boolean Equals(IHttpApiEndpoint? other)
        {
            return other is not null && Controller == other.Controller && Name == other.Name && Method == other.Method && Endpoint == other.Endpoint;
        }

        public override String ToString()
        {
            return $"[{Method.Method}] {Controller ?? StringUtilities.NullString}/{Name ?? StringUtilities.NullString} - {Endpoint}";
        }
    }

    public static class HttpApiEndpoint
    {
        public static TEndpoint Verify<TEndpoint>(this TEndpoint endpoint) where TEndpoint : IHttpApiEndpoint
        {
            return endpoint switch
            {
                null => throw new ArgumentNullException(nameof(endpoint)),
                { IsEmpty: true } => throw new InvalidOperationException($"Endpoint '{endpoint.Controller ?? StringUtilities.NullString}/{endpoint.Name ?? StringUtilities.NullString}' is empty."),
                _ => endpoint
            };
        }
    }
}