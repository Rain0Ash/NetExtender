// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.Utilities.Serialization;
using Newtonsoft.Json;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class BusinessException<T> : BusinessException
    {
        public T Code { get; }

        public override Type Type
        {
            get
            {
                return typeof(T);
            }
        }

        public override BusinessInfo Info
        {
            get
            {
                return ToBusinessInfo(true);
            }
        }

        public override BusinessInfo Business
        {
            get
            {
                return ToBusinessInfo(false);
            }
        }

        public BusinessException(T code)
        {
            Code = code;
        }

        public BusinessException(T code, HttpStatusCode status)
            : base(status)
        {
            Code = code;
        }

        public BusinessException(String? message, T code)
            : base(message)
        {
            Code = code;
        }

        public BusinessException(String? message, T code, HttpStatusCode status)
            : base(message, status)
        {
            Code = code;
        }

        public BusinessException(String? message, T code, Exception? innerException)
            : base(message, innerException)
        {
            Code = code;
        }

        public BusinessException(String? message, T code, HttpStatusCode status, Exception? innerException)
            : base(message, status, innerException)
        {
            Code = code;
        }

        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Code = info.GetValue<T>(nameof(Code));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Code), Code);
        }

        public override Object? GetBusinessCode()
        {
            return Code;
        }

        protected override BusinessInfo ToBusinessInfo(Boolean include)
        {
            return (BusinessInfo) base.ToBusinessInfo(include);
        }

        protected override BusinessInfo ToBusinessInfo(BusinessException.BusinessInfo? inner)
        {
            return inner is not null ? new BusinessInfo(Code, Message, Description, Status, inner) { Business = true } : new BusinessInfo(Code, Message, Description, Status) { Business = true };
        }

        [SuppressMessage("ReSharper", "StructMemberCanBeMadeReadOnly")]
        [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
        public new record BusinessInfo : BusinessException.BusinessInfo
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Include, Order = 3)]
            public T Code { get; init; }

            protected BusinessInfo(T code)
            {
                Code = code;
            }

            public BusinessInfo(T code, String? message, String? description, HttpStatusCode? status)
                : base(message, description, status)
            {
                Code = code;
            }

            public BusinessInfo(T code, String? message, String? description, HttpStatusCode? status, BusinessException.BusinessInfo? inner)
                : base(message, description, status, inner)
            {
                Code = code;
            }

            public void Deconstruct(out T code, out String? message, out String? description, out HttpStatusCode? status)
            {
                Deconstruct(out code, out message, out description, out status, out _);
            }

            public void Deconstruct(out T code, out String? message, out String? description, out HttpStatusCode? status, out BusinessException.BusinessInfo? inner)
            {
                code = Code;
                message = Message;
                description = Description;
                status = Status;
                inner = Inner;
            }
        }
    }

    [Serializable]
    public class BusinessException : Exception
    {
        public String? Description { get; init; }
        public HttpStatusCode? Status { get; init; }

        public virtual Type? Type
        {
            get
            {
                return null;
            }
        }

        public virtual BusinessInfo Info
        {
            get
            {
                return ToBusinessInfo(true);
            }
        }

        public virtual BusinessInfo Business
        {
            get
            {
                return ToBusinessInfo(false);
            }
        }

        public BusinessException()
        {
        }

        public BusinessException(HttpStatusCode status)
        {
            Status = status;
        }

        public BusinessException(String? message)
            : base(message)
        {
        }

        public BusinessException(String? message, HttpStatusCode status)
            : base(message)
        {
            Status = status;
        }

        public BusinessException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public BusinessException(String? message, HttpStatusCode status, Exception? innerException)
            : base(message, innerException)
        {
            Status = status;
        }

        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Description = info.GetString(nameof(Description));
            Status = info.GetValueOrDefault<HttpStatusCode?>(nameof(Status));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Description), Description);
            info.AddValue(nameof(Status), Status);
        }

        public virtual Object? GetBusinessCode()
        {
            return null;
        }

        protected virtual BusinessInfo ToBusinessInfo(Boolean include)
        {
            Exception? exception = InnerException;

            if (exception is null)
            {
                return ToBusinessInfo(null);
            }

            BusinessInfo? inner = null;

            static BusinessInfo HandleInner(Exception exception, BusinessInfo inner, Boolean include)
            {
                if (exception is null)
                {
                    throw new ArgumentNullException(nameof(exception));
                }

                if (inner is null)
                {
                    throw new ArgumentNullException(nameof(inner));
                }

                if (exception is not BusinessException business)
                {
                    return include ? new BusinessInfo(exception.Message, null, null, inner) : inner;
                }

                if (include || inner.Business)
                {
                    return inner with { Inner = business.ToBusinessInfo(include) };
                }

                return business.ToBusinessInfo(include);
            }

            static BusinessInfo? HandleNull(Exception exception, Boolean include)
            {
                return exception switch
                {
                    null => throw new ArgumentNullException(nameof(exception)),
                    BusinessException business => business.ToBusinessInfo(include),
                    _ => include ? new BusinessInfo(exception.Message, null, null) : null
                };
            }

            do
            {
                inner = inner is not null ? HandleInner(exception, inner, include) : HandleNull(exception, include);
            } while ((exception = exception?.InnerException) is not null);

            return ToBusinessInfo(inner);
        }

        protected virtual BusinessInfo ToBusinessInfo(BusinessInfo? inner)
        {
            return inner is not null ? new BusinessInfo(Message, Description, Status, inner) { Business = true } : new BusinessInfo(Message, Description, Status) { Business = true };
        }

        [SuppressMessage("ReSharper", "StructMemberCanBeMadeReadOnly")]
        [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
        public record BusinessInfo
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 0)]
            public String? Message { get; init; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 1)]
            public String? Description { get; init; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 2)]
            public HttpStatusCode? Status { get; init; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = Int32.MaxValue - 1)]
            public Boolean Business { get; init; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = Int32.MaxValue)]
            public BusinessInfo? Inner { get; init; }

            protected BusinessInfo()
            {
            }

            public BusinessInfo(String? message, String? description, HttpStatusCode? status)
                : this(message, description, status, null)
            {
            }

            public BusinessInfo(String? message, String? description, HttpStatusCode? status, BusinessInfo? inner)
            {
                Message = message;
                Description = description;
                Status = status;
                Inner = inner;
            }

            public void Deconstruct(out String? message, out String? description, out HttpStatusCode? status)
            {
                Deconstruct(out message, out description, out status, out _);
            }

            public void Deconstruct(out String? message, out String? description, out HttpStatusCode? status, out BusinessInfo? inner)
            {
                message = Message;
                description = Description;
                status = Status;
                inner = Inner;
            }
        }
    }

    [Serializable]
    public abstract class BusinessStatusException : BusinessException
    {
        protected BusinessStatusException(HttpStatusCode status)
            : base(status)
        {
        }

        protected BusinessStatusException(String? message, HttpStatusCode status)
            : base(message, status)
        {
        }

        protected BusinessStatusException(String? message, HttpStatusCode status, Exception? innerException)
            : base(message, status, innerException)
        {
        }

        protected BusinessStatusException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public abstract class BusinessStatusException<T> : BusinessException<T>
    {
        protected BusinessStatusException(T code, HttpStatusCode status)
            : base(code, status)
        {
        }

        protected BusinessStatusException(String? message, T code, HttpStatusCode status)
            : base(message, code, status)
        {
        }

        protected BusinessStatusException(String? message, T code, HttpStatusCode status, Exception? innerException)
            : base(message, code, status, innerException)
        {
        }

        protected BusinessStatusException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}