// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Exceptions.Interfaces;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class BusinessException<T> : BusinessException, IBusinessException<T>
    {
        public static implicit operator BusinessException<T>((HttpStatusCode Status, T Code) value)
        {
            return Create(value.Status, value.Code);
        }
        
        public static implicit operator BusinessException<T>((HttpStatusCode Status, T Code, String? Message) value)
        {
            return Create(value.Status, value.Code, value.Message);
        }
        
        public static implicit operator BusinessException<T>((HttpStatusCode Status, T Code, String? Message, String? Description) value)
        {
            return Create(value.Status, value.Code, value.Message, value.Description);
        }
        
        public static implicit operator BusinessException<T>((HttpStatusCode Status, T Code, Exception? Exception) value)
        {
            return Create(value.Status, value.Code, value.Exception);
        }
        
        public static implicit operator BusinessException<T>((HttpStatusCode Status, T Code, String? Message, Exception? Exception) value)
        {
            return Create(value.Status, value.Code, value.Message, value.Exception);
        }
        
        public static implicit operator BusinessException<T>((HttpStatusCode Status, T Code, String? Message, String? Description, Exception? Exception) value)
        {
            return Create(value.Status, value.Code, value.Message, value.Description, value.Exception);
        }
        
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

        public BusinessException(T code, params BusinessException?[]? reason)
            : base(reason)
        {
            Code = code;
        }

        public BusinessException(HttpStatusCode status, T code)
            : base(status)
        {
            Code = code;
        }

        public BusinessException(HttpStatusCode status, T code, params BusinessException?[]? reason)
            : base(status, reason)
        {
            Code = code;
        }

        public BusinessException(HttpStatusCode status, T code, Exception? exception)
            : base(status, exception)
        {
            Code = code;
        }

        public BusinessException(HttpStatusCode status, T code, BusinessException? exception)
            : base(status, exception)
        {
            Code = code;
        }

        public BusinessException(HttpStatusCode status, T code, Exception? exception, params BusinessException?[]? reason)
            : base(status, exception, reason)
        {
            Code = code;
        }

        public BusinessException(HttpStatusCode status, T code, BusinessException? exception, params BusinessException?[]? reason)
            : base(status, exception, reason)
        {
            Code = code;
        }

        public BusinessException(T code, Exception? exception)
            : base(exception)
        {
            Code = code;
        }

        public BusinessException(T code, BusinessException? exception)
            : base(exception)
        {
            Code = code;
        }

        public BusinessException(T code, Exception? exception, params BusinessException?[]? reason)
            : base(exception, reason)
        {
            Code = code;
        }

        public BusinessException(T code, BusinessException? exception, params BusinessException?[]? reason)
            : base(exception, reason)
        {
            Code = code;
        }

        public BusinessException(String? message, T code)
            : base(message)
        {
            Code = code;
        }

        public BusinessException(String? message, T code, params BusinessException?[]? reason)
            : base(message, reason)
        {
            Code = code;
        }

        public BusinessException(String? message, T code, Exception? exception)
            : base(message, exception)
        {
            Code = code;
        }

        public BusinessException(String? message, T code, BusinessException? exception)
            : base(message, exception)
        {
            Code = code;
        }

        public BusinessException(String? message, T code, Exception? exception, params BusinessException?[]? reason)
            : base(message, exception, reason)
        {
            Code = code;
        }

        public BusinessException(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
            : base(message, exception, reason)
        {
            Code = code;
        }

        public BusinessException(String? message, HttpStatusCode status, T code)
            : base(message, status)
        {
            Code = code;
        }

        public BusinessException(String? message, HttpStatusCode status, T code, Exception? exception)
            : base(message, status, exception)
        {
            Code = code;
        }

        public BusinessException(String? message, HttpStatusCode status, T code, BusinessException? exception)
            : base(message, status, exception)
        {
            Code = code;
        }

        public BusinessException(String? message, HttpStatusCode status, T code, params BusinessException?[]? reason)
            : base(message, status, reason)
        {
            Code = code;
        }

        public BusinessException(String? message, HttpStatusCode status, T code, Exception? exception, params BusinessException?[]? reason)
            : base(message, status, exception, reason)
        {
            Code = code;
        }

        public BusinessException(String? message, HttpStatusCode status, T code, BusinessException? exception, params BusinessException?[]? reason)
            : base(message, status, exception, reason)
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

        protected override BusinessInfo ToBusinessInfo(BusinessException.BusinessInfo? inner, Boolean include)
        {
            return new BusinessInfo(Id, Message, Name, Code, Description, Status, inner, ToBusinessInfoReason(include)) { Business = true, Include = include, Data = Data };
        }

#if NET7_0_OR_GREATER
        [System.Text.Json.Serialization.JsonDerivedType(typeof(BusinessException.BusinessInfo))]
#endif
        public new record BusinessInfo : BusinessException.BusinessInfo
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include, Order = 3)]
            [System.Text.Json.Serialization.JsonPropertyOrder(3)]
            [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.Never)]
            public T Code { get; init; }

            protected BusinessInfo()
                : this(default(T)!)
            {
            }

            protected BusinessInfo(T code)
            {
                Code = code;
            }

            public BusinessInfo(Guid id, String? message, String? name, T code, String? description, HttpStatusCode? status, ImmutableList<BusinessException.BusinessInfo>? reason)
                : base(id, message, name, description, status, reason)
            {
                Code = code;
            }

            public BusinessInfo(Guid id, String? message, String? name, T code, String? description, HttpStatusCode? status, BusinessException.BusinessInfo? inner, ImmutableList<BusinessException.BusinessInfo>? reason)
                : base(id, message, name, description, status, inner, reason)
            {
                Code = code;
            }

            public void Deconstruct(out String? message, out String? name, out T code, out String? description, out HttpStatusCode? status)
            {
                Deconstruct(out message, out name, out code, out description, out status, out _);
            }

            public void Deconstruct(out String? message, out String? name, out T code, out String? description, out HttpStatusCode? status, out BusinessException.BusinessInfo? inner)
            {
                Deconstruct(out message, out name, out code, out description, out status, out inner, out _);
            }

            public void Deconstruct(out String? message, out String? name, out T code, out String? description, out HttpStatusCode? status, out BusinessException.BusinessInfo? inner, out ImmutableList<BusinessException.BusinessInfo>? reason)
            {
                code = Code;
                message = Message;
                name = Name;
                description = Description;
                status = Status;
                inner = Inner;
                reason = Reason;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessException<T> Create(HttpStatusCode status, T code)
        {
            return Create(status, code, null, (String?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessException<T> Create(HttpStatusCode status, T code, String? message)
        {
            return Create(status, code, message, (String?) null);
        }

        public static BusinessException<T> Create(HttpStatusCode status, T code, String? message, String? description)
        {
            return status switch
            {
                HttpStatusCode.Continue => new BusinessContinue100Exception<T>(message, code) { Description = description },
                HttpStatusCode.SwitchingProtocols => new BusinessSwitchingProtocols101Exception<T>(message, code) { Description = description },
                HttpStatusCode.Processing => new BusinessProcessing102Exception<T>(message, code) { Description = description },
                HttpStatusCode.EarlyHints => new BusinessEarlyHints103Exception<T>(message, code) { Description = description },
                HttpStatusCode.OK => new BusinessOK200Exception<T>(message, code) { Description = description },
                HttpStatusCode.Created => new BusinessCreated201Exception<T>(message, code) { Description = description },
                HttpStatusCode.Accepted => new BusinessAccepted202Exception<T>(message, code) { Description = description },
                HttpStatusCode.NonAuthoritativeInformation => new BusinessNonAuthoritativeInformation203Exception<T>(message, code) { Description = description },
                HttpStatusCode.NoContent => new BusinessNoContent204Exception<T>(message, code) { Description = description },
                HttpStatusCode.ResetContent => new BusinessResetContent205Exception<T>(message, code) { Description = description },
                HttpStatusCode.PartialContent => new BusinessPartialContent206Exception<T>(message, code) { Description = description },
                HttpStatusCode.MultiStatus => new BusinessMultiStatus207Exception<T>(message, code) { Description = description },
                HttpStatusCode.AlreadyReported => new BusinessAlreadyReported208Exception<T>(message, code) { Description = description },
                HttpStatusCode.IMUsed => new BusinessIMUsed226Exception<T>(message, code) { Description = description },
                HttpStatusCode.Ambiguous => new BusinessAmbiguous300Exception<T>(message, code) { Description = description },
                HttpStatusCode.Moved => new BusinessMoved301Exception<T>(message, code) { Description = description },
                HttpStatusCode.Found => new BusinessFound302Exception<T>(message, code) { Description = description },
                HttpStatusCode.RedirectMethod => new BusinessRedirectMethod303Exception<T>(message, code) { Description = description },
                HttpStatusCode.NotModified => new BusinessNotModified304Exception<T>(message, code) { Description = description },
                HttpStatusCode.UseProxy => new BusinessUseProxy305Exception<T>(message, code) { Description = description },
                HttpStatusCode.Unused => new BusinessUnused306Exception<T>(message, code) { Description = description },
                HttpStatusCode.RedirectKeepVerb => new BusinessRedirectKeepVerb307Exception<T>(message, code) { Description = description },
                HttpStatusCode.PermanentRedirect => new BusinessPermanentRedirect308Exception<T>(message, code) { Description = description },
                HttpStatusCode.BadRequest => new BusinessBadRequest400Exception<T>(message, code) { Description = description },
                HttpStatusCode.Unauthorized => new BusinessUnauthorized401Exception<T>(message, code) { Description = description },
                HttpStatusCode.PaymentRequired => new BusinessPaymentRequired402Exception<T>(message, code) { Description = description },
                HttpStatusCode.Forbidden => new BusinessForbidden403Exception<T>(message, code) { Description = description },
                HttpStatusCode.NotFound => new BusinessNotFound404Exception<T>(message, code) { Description = description },
                HttpStatusCode.MethodNotAllowed => new BusinessMethodNotAllowed405Exception<T>(message, code) { Description = description },
                HttpStatusCode.NotAcceptable => new BusinessNotAcceptable406Exception<T>(message, code) { Description = description },
                HttpStatusCode.ProxyAuthenticationRequired => new BusinessProxyAuthenticationRequired407Exception<T>(message, code) { Description = description },
                HttpStatusCode.RequestTimeout => new BusinessRequestTimeout408Exception<T>(message, code) { Description = description },
                HttpStatusCode.Conflict => new BusinessConflict409Exception<T>(message, code) { Description = description },
                HttpStatusCode.Gone => new BusinessGone410Exception<T>(message, code) { Description = description },
                HttpStatusCode.LengthRequired => new BusinessLengthRequired411Exception<T>(message, code) { Description = description },
                HttpStatusCode.PreconditionFailed => new BusinessPreconditionFailed412Exception<T>(message, code) { Description = description },
                HttpStatusCode.RequestEntityTooLarge => new BusinessRequestEntityTooLarge413Exception<T>(message, code) { Description = description },
                HttpStatusCode.RequestUriTooLong => new BusinessRequestUriTooLong414Exception<T>(message, code) { Description = description },
                HttpStatusCode.UnsupportedMediaType => new BusinessUnsupportedMediaType415Exception<T>(message, code) { Description = description },
                HttpStatusCode.RequestedRangeNotSatisfiable => new BusinessRequestedRangeNotSatisfiable416Exception<T>(message, code) { Description = description },
                HttpStatusCode.ExpectationFailed => new BusinessExpectationFailed417Exception<T>(message, code) { Description = description },
                HttpStatusCode.MisdirectedRequest => new BusinessMisdirectedRequest421Exception<T>(message, code) { Description = description },
                HttpStatusCode.UnprocessableEntity => new BusinessUnprocessableEntity422Exception<T>(message, code) { Description = description },
                HttpStatusCode.Locked => new BusinessLocked423Exception<T>(message, code) { Description = description },
                HttpStatusCode.FailedDependency => new BusinessFailedDependency424Exception<T>(message, code) { Description = description },
                HttpStatusCode.UpgradeRequired => new BusinessUpgradeRequired426Exception<T>(message, code) { Description = description },
                HttpStatusCode.PreconditionRequired => new BusinessPreconditionRequired428Exception<T>(message, code) { Description = description },
                HttpStatusCode.TooManyRequests => new BusinessTooManyRequests429Exception<T>(message, code) { Description = description },
                HttpStatusCode.RequestHeaderFieldsTooLarge => new BusinessRequestHeaderFieldsTooLarge431Exception<T>(message, code) { Description = description },
                HttpStatusCode.UnavailableForLegalReasons => new BusinessUnavailableForLegalReasons451Exception<T>(message, code) { Description = description },
                HttpStatusCode.InternalServerError => new BusinessInternalServerError500Exception<T>(message, code) { Description = description },
                HttpStatusCode.NotImplemented => new BusinessNotImplemented501Exception<T>(message, code) { Description = description },
                HttpStatusCode.BadGateway => new BusinessBadGateway502Exception<T>(message, code) { Description = description },
                HttpStatusCode.ServiceUnavailable => new BusinessServiceUnavailable503Exception<T>(message, code) { Description = description },
                HttpStatusCode.GatewayTimeout => new BusinessGatewayTimeout504Exception<T>(message, code) { Description = description },
                HttpStatusCode.HttpVersionNotSupported => new BusinessHttpVersionNotSupported505Exception<T>(message, code) { Description = description },
                HttpStatusCode.VariantAlsoNegotiates => new BusinessVariantAlsoNegotiates506Exception<T>(message, code) { Description = description },
                HttpStatusCode.InsufficientStorage => new BusinessInsufficientStorage507Exception<T>(message, code) { Description = description },
                HttpStatusCode.LoopDetected => new BusinessLoopDetected508Exception<T>(message, code) { Description = description },
                HttpStatusCode.NotExtended => new BusinessNotExtended510Exception<T>(message, code) { Description = description },
                HttpStatusCode.NetworkAuthenticationRequired => new BusinessNetworkAuthenticationRequired511Exception<T>(message, code) { Description = description },
                _ => new BusinessStatusException<T>(message, status, code)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessException<T> Create(HttpStatusCode status, T code, Exception? exception)
        {
            return Create(status, code, null, null, exception);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessException<T> Create(HttpStatusCode status, T code, String? message, Exception? exception)
        {
            return Create(status, code, message, null, exception);
        }

        public static BusinessException<T> Create(HttpStatusCode status, T code, String? message, String? description, Exception? exception)
        {
            return status switch
            {
                HttpStatusCode.Continue => new BusinessContinue100Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.SwitchingProtocols => new BusinessSwitchingProtocols101Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.Processing => new BusinessProcessing102Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.EarlyHints => new BusinessEarlyHints103Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.OK => new BusinessOK200Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.Created => new BusinessCreated201Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.Accepted => new BusinessAccepted202Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.NonAuthoritativeInformation => new BusinessNonAuthoritativeInformation203Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.NoContent => new BusinessNoContent204Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.ResetContent => new BusinessResetContent205Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.PartialContent => new BusinessPartialContent206Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.MultiStatus => new BusinessMultiStatus207Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.AlreadyReported => new BusinessAlreadyReported208Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.IMUsed => new BusinessIMUsed226Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.Ambiguous => new BusinessAmbiguous300Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.Moved => new BusinessMoved301Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.Found => new BusinessFound302Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.RedirectMethod => new BusinessRedirectMethod303Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.NotModified => new BusinessNotModified304Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.UseProxy => new BusinessUseProxy305Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.Unused => new BusinessUnused306Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.RedirectKeepVerb => new BusinessRedirectKeepVerb307Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.PermanentRedirect => new BusinessPermanentRedirect308Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.BadRequest => new BusinessBadRequest400Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.Unauthorized => new BusinessUnauthorized401Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.PaymentRequired => new BusinessPaymentRequired402Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.Forbidden => new BusinessForbidden403Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.NotFound => new BusinessNotFound404Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.MethodNotAllowed => new BusinessMethodNotAllowed405Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.NotAcceptable => new BusinessNotAcceptable406Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.ProxyAuthenticationRequired => new BusinessProxyAuthenticationRequired407Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.RequestTimeout => new BusinessRequestTimeout408Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.Conflict => new BusinessConflict409Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.Gone => new BusinessGone410Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.LengthRequired => new BusinessLengthRequired411Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.PreconditionFailed => new BusinessPreconditionFailed412Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.RequestEntityTooLarge => new BusinessRequestEntityTooLarge413Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.RequestUriTooLong => new BusinessRequestUriTooLong414Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.UnsupportedMediaType => new BusinessUnsupportedMediaType415Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.RequestedRangeNotSatisfiable => new BusinessRequestedRangeNotSatisfiable416Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.ExpectationFailed => new BusinessExpectationFailed417Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.MisdirectedRequest => new BusinessMisdirectedRequest421Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.UnprocessableEntity => new BusinessUnprocessableEntity422Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.Locked => new BusinessLocked423Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.FailedDependency => new BusinessFailedDependency424Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.UpgradeRequired => new BusinessUpgradeRequired426Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.PreconditionRequired => new BusinessPreconditionRequired428Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.TooManyRequests => new BusinessTooManyRequests429Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.RequestHeaderFieldsTooLarge => new BusinessRequestHeaderFieldsTooLarge431Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.UnavailableForLegalReasons => new BusinessUnavailableForLegalReasons451Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.InternalServerError => new BusinessInternalServerError500Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.NotImplemented => new BusinessNotImplemented501Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.BadGateway => new BusinessBadGateway502Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.ServiceUnavailable => new BusinessServiceUnavailable503Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.GatewayTimeout => new BusinessGatewayTimeout504Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.HttpVersionNotSupported => new BusinessHttpVersionNotSupported505Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.VariantAlsoNegotiates => new BusinessVariantAlsoNegotiates506Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.InsufficientStorage => new BusinessInsufficientStorage507Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.LoopDetected => new BusinessLoopDetected508Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.NotExtended => new BusinessNotExtended510Exception<T>(message, code, exception) { Description = description },
                HttpStatusCode.NetworkAuthenticationRequired => new BusinessNetworkAuthenticationRequired511Exception<T>(message, code, exception) { Description = description },
                _ => new BusinessStatusException<T>(message, status, code, exception)
            };
        }
    }

    [Serializable]
    [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
    public class BusinessException : AggregateException, IBusinessException
    {
        public static implicit operator BusinessException(HttpStatusCode value)
        {
            return Create(value);
        }
        
        public static implicit operator BusinessException((HttpStatusCode Status, String? Message) value)
        {
            return Create(value.Status, value.Message);
        }
        
        public static implicit operator BusinessException((HttpStatusCode Status, String? Message, String? Description) value)
        {
            return Create(value.Status, value.Message, value.Description);
        }
        
        public static implicit operator BusinessException((HttpStatusCode Status, Exception? Exception) value)
        {
            return Create(value.Status, value.Exception);
        }
        
        public static implicit operator BusinessException((HttpStatusCode Status, String? Message, Exception? Exception) value)
        {
            return Create(value.Status, value.Message, value.Exception);
        }
        
        public static implicit operator BusinessException((HttpStatusCode Status, String? Message, String? Description, Exception? Exception) value)
        {
            return Create(value.Status, value.Message, value.Description, value.Exception);
        }

        public Guid Id { get; init; } = DateTime.UtcNow.NewGuid();
        public String? Name { get; init; }
        public String? Description { get; init; }
        public HttpStatusCode? Status { get; init; }
        protected Exception? Inner { get; }

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
            : this(nameof(BusinessException))
        {
        }

        public BusinessException(params BusinessException?[]? reason)
            : this((Exception?) null, reason)
        {
        }

        public BusinessException(HttpStatusCode status)
            : this(status, (Exception?) null)
        {
        }

        public BusinessException(HttpStatusCode status, params BusinessException?[]? reason)
            : this(status, null, reason)
        {
        }

        public BusinessException(HttpStatusCode status, Exception? exception)
            : this(status, exception, null)
        {
        }

        public BusinessException(HttpStatusCode status, BusinessException? exception)
            : this(status, (Exception?) exception)
        {
        }

        public BusinessException(HttpStatusCode status, Exception? exception, params BusinessException?[]? reason)
            : this(null, status, exception, reason)
        {
        }

        public BusinessException(HttpStatusCode status, BusinessException? exception, params BusinessException?[]? reason)
            : this(status, (Exception?) exception, reason)
        {
        }

        public BusinessException(Exception? exception)
            : this(exception, null)
        {
        }

        public BusinessException(BusinessException? exception)
            : this((Exception?) exception)
        {
        }

        public BusinessException(Exception? exception, params BusinessException?[]? reason)
            : this(null, exception, reason)
        {
        }

        public BusinessException(BusinessException? exception, params BusinessException?[]? reason)
            : this((Exception?) exception, reason)
        {
        }

        public BusinessException(String? message)
            : this(message, (Exception?) null)
        {
        }

        public BusinessException(String? message, params BusinessException?[]? reason)
            : this(message, null, reason)
        {
        }

        public BusinessException(String? message, Exception? exception)
            : this(message, exception, null)
        {
        }

        public BusinessException(String? message, BusinessException? exception)
            : this(message, (Exception?) exception)
        {
        }

        public BusinessException(String? message, Exception? exception, params BusinessException?[]? reason)
            : base(message ?? nameof(BusinessException), Create(exception, reason))
        {
            Inner = exception;
        }

        public BusinessException(String? message, BusinessException? exception, params BusinessException?[]? reason)
            : this(message, (Exception?) exception, reason)
        {
        }

        public BusinessException(String? message, HttpStatusCode status)
            : this(message, status, (Exception?) null)
        {
        }

        public BusinessException(String? message, HttpStatusCode status, Exception? exception)
            : this(message, status, exception, null)
        {
        }

        public BusinessException(String? message, HttpStatusCode status, BusinessException? exception)
            : this(message, status, (Exception?) exception)
        {
        }

        public BusinessException(String? message, HttpStatusCode status, params BusinessException?[]? reason)
            : this(message, status, (Exception?) null, reason)
        {
        }

        public BusinessException(String? message, HttpStatusCode status, Exception? exception, params BusinessException?[]? reason)
            : base(Format(message, status), Create(exception, reason))
        {
            Status = status;
            Inner = exception;
        }

        public BusinessException(String? message, HttpStatusCode status, BusinessException? exception, params BusinessException?[]? reason)
            : this(message, status, (Exception?) exception, reason)
        {
        }

        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Description = info.GetString(nameof(Description));
            Status = info.GetValueOrDefault<HttpStatusCode?>(nameof(Status));
            Inner = info.GetValueOrDefault<Exception?>(nameof(Inner));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Description), Description);
            info.AddValue(nameof(Status), Status);
            info.AddValue(nameof(Inner), Inner);
        }

        private static String Format(String? message, HttpStatusCode status)
        {
            return message ?? $"{nameof(BusinessException)}: {(Int32) status} ({status})";
        }

        // ReSharper disable once CognitiveComplexity
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Exception[] Create(Exception? exception, params BusinessException?[]? reason)
        {
            Int32 count = 0;
            if (exception is not null)
            {
                ++count;
            }

            if (reason is not null)
            {
                for (Int32 i = 0; i < reason.Length; ++i)
                {
                    if (reason[i] is not null)
                    {
                        ++count;
                    }
                }
            }

            if (count <= 0)
            {
                return Array.Empty<Exception>();
            }
            
            Exception[] result = new Exception[count];

            Int32 index = 0;
            if (exception is not null)
            {
                result[index++] = exception;
            }

            if (reason is null)
            {
                return result;
            }

            for (Int32 i = 0; i < reason.Length; i++)
            {
                if (reason[i] is not null)
                {
                    result[index++] = reason[i]!;
                }
            }

            return result;
        }

        public virtual Object? GetBusinessCode()
        {
            return null;
        }

        protected virtual BusinessInfo ToBusinessInfo(Boolean include)
        {
            if (this.Inner is null)
            {
                return ToBusinessInfo(null, include);
            }

            Exception? exception = this.Inner;

            if (exception is null)
            {
                return ToBusinessInfo(null, include);
            }

            BusinessInfo? inner = null;

            static BusinessInfo Inner(Exception exception, BusinessInfo inner, Boolean include)
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
                    return include ? new BusinessInfo((exception as ITraceException)?.Id, exception.Message, null, null, null, inner, null) { Business = false, Include = include, Data = exception.Data } : inner;
                }

                if (include || inner.Business)
                {
                    return inner with { Inner = business.ToBusinessInfo(include) };
                }

                return business.ToBusinessInfo(include);
            }

            static BusinessInfo? Null(Exception exception, Boolean include)
            {
                return exception switch
                {
                    null => throw new ArgumentNullException(nameof(exception)),
                    BusinessException business => business.ToBusinessInfo(include),
                    _ => include ? new BusinessInfo((exception as ITraceException)?.Id, exception.Message, null, null, null, null) { Business = false, Include = include, Data = exception.Data } : null
                };
            }

            static Boolean HasNext([NotNullWhen(true)] ref Exception? exception)
            {
                return exception switch
                {
                    BusinessException inner => exception = inner.Inner,
                    AggregateException => exception = null,
                    _ => exception = exception?.InnerException,
                } is not null;
            }

            do
            {
                inner = inner is not null ? Inner(exception, inner, include) : Null(exception, include);
            } while (HasNext(ref exception));

            return ToBusinessInfo(inner, include);
        }

        protected virtual BusinessInfo ToBusinessInfo(BusinessInfo? inner, Boolean include)
        {
            return new BusinessInfo(Id, Message, Name, Description, Status, inner, ToBusinessInfoReason(include)) { Business = true, Include = include, Data = Data };
        }

        protected virtual ImmutableList<BusinessInfo>? ToBusinessInfoReason(Boolean include)
        {
            ImmutableList<BusinessInfo>.Builder builder = ImmutableList.CreateBuilder<BusinessInfo>();

            foreach (Exception exception in InnerExceptions)
            {
                if (ReferenceEquals(exception, Inner) || exception is not BusinessException business)
                {
                    continue;
                }
                
                builder.Add(business.ToBusinessInfo(include));
            }
            
            return builder.Count > 0 ? builder.ToImmutable() : null;
        }

        public record BusinessInfo
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore, Order = 0)]
            [System.Text.Json.Serialization.JsonPropertyOrder(0)]
            [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
            public Guid? Id { get; init; }
            
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 1)]
            [System.Text.Json.Serialization.JsonPropertyOrder(1)]
            [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
            public String? Message { get; init; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 2)]
            [System.Text.Json.Serialization.JsonPropertyOrder(2)]
            [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
            public String? Name { get; init; }
            
            [JsonProperty(PropertyName = nameof(Data), NullValueHandling = NullValueHandling.Ignore, Order = 4)]
            [System.Text.Json.Serialization.JsonPropertyName(nameof(Data))]
            [System.Text.Json.Serialization.JsonPropertyOrder(4)]
            [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
            private Dictionary<Object, Object?>? _data;
            
            [JsonIgnore]
            [System.Text.Json.Serialization.JsonIgnore]
            public IDictionary Data
            {
                get
                {
                    return (IDictionary?) _data ?? NoneDictionary.Empty;
                }
                init
                {
                    _data = value switch
                    {
                        null => null,
                        { Count: 0 } => null,
                        Dictionary<Object, Object?> dictionary => dictionary,
                        _ => value.ToDictionary()
                    };
                }
            }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 5)]
            [System.Text.Json.Serialization.JsonPropertyOrder(5)]
            [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
            public String? Description { get; init; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore, Order = 6)]
            [System.Text.Json.Serialization.JsonPropertyOrder(6)]
            [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
            public HttpStatusCode? Status { get; init; }

            [System.Text.Json.Serialization.JsonIgnore]
            [JsonIgnore]
            public Boolean Business { get; init; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = Int32.MaxValue - 2)]
            [System.Text.Json.Serialization.JsonInclude]
            [System.Text.Json.Serialization.JsonPropertyOrder(Int32.MaxValue - 2)]
            [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
            private Boolean? IsBusiness
            {
                get
                {
                    return Include ? Business : null;
                }
                init
                {
                    Business = value ?? false;
                }
            }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = Int32.MaxValue - 1)]
            [JsonConverter(typeof(Newtonsoft.PolymorphicJsonConverter))]
            [System.Text.Json.Serialization.JsonPropertyOrder(Int32.MaxValue - 1)]
            [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
            [System.Text.Json.Serialization.PolymorphicJsonConverter]
            public BusinessInfo? Inner { get; init; }
            
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = Int32.MaxValue)]
            [JsonConverter(typeof(Newtonsoft.PolymorphicJsonConverter))]
            [System.Text.Json.Serialization.JsonPropertyOrder(Int32.MaxValue)]
            [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
            [System.Text.Json.Serialization.PolymorphicJsonConverter]
            public ImmutableList<BusinessInfo>? Reason { get; init; }
            
            [JsonIgnore]
            [System.Text.Json.Serialization.JsonIgnore]
            protected internal Boolean Include { get; init; }

            protected BusinessInfo()
            {
            }

            public BusinessInfo(Guid? id, String? message, String? name, String? description, HttpStatusCode? status, ImmutableList<BusinessInfo>? reason)
                : this(id, message, name, description, status, null, reason)
            {
            }

            public BusinessInfo(Guid? id, String? message, String? name, String? description, HttpStatusCode? status, BusinessInfo? inner, ImmutableList<BusinessInfo>? reason)
            {
                Id = id;
                Message = message;
                Name = name;
                Description = description;
                Status = status;
                Inner = inner;
                Reason = reason;
            }

            public void Deconstruct(out String? message, out String? name, out String? description, out HttpStatusCode? status)
            {
                Deconstruct(out message, out name, out description, out status, out _);
            }

            public void Deconstruct(out String? message, out String? name, out String? description, out HttpStatusCode? status, out BusinessInfo? reason)
            {
                Deconstruct(out message, out name, out description, out status, out reason, out _);
            }

            public void Deconstruct(out String? message, out String? name, out String? description, out HttpStatusCode? status, out BusinessInfo? reason, out ImmutableList<BusinessInfo>? inner)
            {
                message = Message;
                name = Name;
                description = Description;
                status = Status;
                reason = Inner;
                inner = Reason;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessException Create(HttpStatusCode status)
        {
            return Create(status, null, (String?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessException Create(HttpStatusCode status, String? message)
        {
            return Create(status, message, (String?) null);
        }

        public static BusinessException Create(HttpStatusCode status, String? message, String? description)
        {
            return status switch
            {
                HttpStatusCode.Continue => new BusinessContinue100Exception(message) { Description = description },
                HttpStatusCode.SwitchingProtocols => new BusinessSwitchingProtocols101Exception(message) { Description = description },
                HttpStatusCode.Processing => new BusinessProcessing102Exception(message) { Description = description },
                HttpStatusCode.EarlyHints => new BusinessEarlyHints103Exception(message) { Description = description },
                HttpStatusCode.OK => new BusinessOK200Exception(message) { Description = description },
                HttpStatusCode.Created => new BusinessCreated201Exception(message) { Description = description },
                HttpStatusCode.Accepted => new BusinessAccepted202Exception(message) { Description = description },
                HttpStatusCode.NonAuthoritativeInformation => new BusinessNonAuthoritativeInformation203Exception(message) { Description = description },
                HttpStatusCode.NoContent => new BusinessNoContent204Exception(message) { Description = description },
                HttpStatusCode.ResetContent => new BusinessResetContent205Exception(message) { Description = description },
                HttpStatusCode.PartialContent => new BusinessPartialContent206Exception(message) { Description = description },
                HttpStatusCode.MultiStatus => new BusinessMultiStatus207Exception(message) { Description = description },
                HttpStatusCode.AlreadyReported => new BusinessAlreadyReported208Exception(message) { Description = description },
                HttpStatusCode.IMUsed => new BusinessIMUsed226Exception(message) { Description = description },
                HttpStatusCode.Ambiguous => new BusinessAmbiguous300Exception(message) { Description = description },
                HttpStatusCode.Moved => new BusinessMoved301Exception(message) { Description = description },
                HttpStatusCode.Found => new BusinessFound302Exception(message) { Description = description },
                HttpStatusCode.RedirectMethod => new BusinessRedirectMethod303Exception(message) { Description = description },
                HttpStatusCode.NotModified => new BusinessNotModified304Exception(message) { Description = description },
                HttpStatusCode.UseProxy => new BusinessUseProxy305Exception(message) { Description = description },
                HttpStatusCode.Unused => new BusinessUnused306Exception(message) { Description = description },
                HttpStatusCode.RedirectKeepVerb => new BusinessRedirectKeepVerb307Exception(message) { Description = description },
                HttpStatusCode.PermanentRedirect => new BusinessPermanentRedirect308Exception(message) { Description = description },
                HttpStatusCode.BadRequest => new BusinessBadRequest400Exception(message) { Description = description },
                HttpStatusCode.Unauthorized => new BusinessUnauthorized401Exception(message) { Description = description },
                HttpStatusCode.PaymentRequired => new BusinessPaymentRequired402Exception(message) { Description = description },
                HttpStatusCode.Forbidden => new BusinessForbidden403Exception(message) { Description = description },
                HttpStatusCode.NotFound => new BusinessNotFound404Exception(message) { Description = description },
                HttpStatusCode.MethodNotAllowed => new BusinessMethodNotAllowed405Exception(message) { Description = description },
                HttpStatusCode.NotAcceptable => new BusinessNotAcceptable406Exception(message) { Description = description },
                HttpStatusCode.ProxyAuthenticationRequired => new BusinessProxyAuthenticationRequired407Exception(message) { Description = description },
                HttpStatusCode.RequestTimeout => new BusinessRequestTimeout408Exception(message) { Description = description },
                HttpStatusCode.Conflict => new BusinessConflict409Exception(message) { Description = description },
                HttpStatusCode.Gone => new BusinessGone410Exception(message) { Description = description },
                HttpStatusCode.LengthRequired => new BusinessLengthRequired411Exception(message) { Description = description },
                HttpStatusCode.PreconditionFailed => new BusinessPreconditionFailed412Exception(message) { Description = description },
                HttpStatusCode.RequestEntityTooLarge => new BusinessRequestEntityTooLarge413Exception(message) { Description = description },
                HttpStatusCode.RequestUriTooLong => new BusinessRequestUriTooLong414Exception(message) { Description = description },
                HttpStatusCode.UnsupportedMediaType => new BusinessUnsupportedMediaType415Exception(message) { Description = description },
                HttpStatusCode.RequestedRangeNotSatisfiable => new BusinessRequestedRangeNotSatisfiable416Exception(message) { Description = description },
                HttpStatusCode.ExpectationFailed => new BusinessExpectationFailed417Exception(message) { Description = description },
                HttpStatusCode.MisdirectedRequest => new BusinessMisdirectedRequest421Exception(message) { Description = description },
                HttpStatusCode.UnprocessableEntity => new BusinessUnprocessableEntity422Exception(message) { Description = description },
                HttpStatusCode.Locked => new BusinessLocked423Exception(message) { Description = description },
                HttpStatusCode.FailedDependency => new BusinessFailedDependency424Exception(message) { Description = description },
                HttpStatusCode.UpgradeRequired => new BusinessUpgradeRequired426Exception(message) { Description = description },
                HttpStatusCode.PreconditionRequired => new BusinessPreconditionRequired428Exception(message) { Description = description },
                HttpStatusCode.TooManyRequests => new BusinessTooManyRequests429Exception(message) { Description = description },
                HttpStatusCode.RequestHeaderFieldsTooLarge => new BusinessRequestHeaderFieldsTooLarge431Exception(message) { Description = description },
                HttpStatusCode.UnavailableForLegalReasons => new BusinessUnavailableForLegalReasons451Exception(message) { Description = description },
                HttpStatusCode.InternalServerError => new BusinessInternalServerError500Exception(message) { Description = description },
                HttpStatusCode.NotImplemented => new BusinessNotImplemented501Exception(message) { Description = description },
                HttpStatusCode.BadGateway => new BusinessBadGateway502Exception(message) { Description = description },
                HttpStatusCode.ServiceUnavailable => new BusinessServiceUnavailable503Exception(message) { Description = description },
                HttpStatusCode.GatewayTimeout => new BusinessGatewayTimeout504Exception(message) { Description = description },
                HttpStatusCode.HttpVersionNotSupported => new BusinessHttpVersionNotSupported505Exception(message) { Description = description },
                HttpStatusCode.VariantAlsoNegotiates => new BusinessVariantAlsoNegotiates506Exception(message) { Description = description },
                HttpStatusCode.InsufficientStorage => new BusinessInsufficientStorage507Exception(message) { Description = description },
                HttpStatusCode.LoopDetected => new BusinessLoopDetected508Exception(message) { Description = description },
                HttpStatusCode.NotExtended => new BusinessNotExtended510Exception(message) { Description = description },
                HttpStatusCode.NetworkAuthenticationRequired => new BusinessNetworkAuthenticationRequired511Exception(message) { Description = description },
                _ => new BusinessStatusException(message, status)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessException Create(HttpStatusCode status, Exception? exception)
        {
            return Create(status, null, null, exception);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessException Create(HttpStatusCode status, String? message, Exception? exception)
        {
            return Create(status, message, null, exception);
        }

        public static BusinessException Create(HttpStatusCode status, String? message, String? description, Exception? exception)
        {
            return status switch
            {
                HttpStatusCode.Continue => new BusinessContinue100Exception(message, exception) { Description = description },
                HttpStatusCode.SwitchingProtocols => new BusinessSwitchingProtocols101Exception(message, exception) { Description = description },
                HttpStatusCode.Processing => new BusinessProcessing102Exception(message, exception) { Description = description },
                HttpStatusCode.EarlyHints => new BusinessEarlyHints103Exception(message, exception) { Description = description },
                HttpStatusCode.OK => new BusinessOK200Exception(message, exception) { Description = description },
                HttpStatusCode.Created => new BusinessCreated201Exception(message, exception) { Description = description },
                HttpStatusCode.Accepted => new BusinessAccepted202Exception(message, exception) { Description = description },
                HttpStatusCode.NonAuthoritativeInformation => new BusinessNonAuthoritativeInformation203Exception(message, exception) { Description = description },
                HttpStatusCode.NoContent => new BusinessNoContent204Exception(message, exception) { Description = description },
                HttpStatusCode.ResetContent => new BusinessResetContent205Exception(message, exception) { Description = description },
                HttpStatusCode.PartialContent => new BusinessPartialContent206Exception(message, exception) { Description = description },
                HttpStatusCode.MultiStatus => new BusinessMultiStatus207Exception(message, exception) { Description = description },
                HttpStatusCode.AlreadyReported => new BusinessAlreadyReported208Exception(message, exception) { Description = description },
                HttpStatusCode.IMUsed => new BusinessIMUsed226Exception(message, exception) { Description = description },
                HttpStatusCode.Ambiguous => new BusinessAmbiguous300Exception(message, exception) { Description = description },
                HttpStatusCode.Moved => new BusinessMoved301Exception(message, exception) { Description = description },
                HttpStatusCode.Found => new BusinessFound302Exception(message, exception) { Description = description },
                HttpStatusCode.RedirectMethod => new BusinessRedirectMethod303Exception(message, exception) { Description = description },
                HttpStatusCode.NotModified => new BusinessNotModified304Exception(message, exception) { Description = description },
                HttpStatusCode.UseProxy => new BusinessUseProxy305Exception(message, exception) { Description = description },
                HttpStatusCode.Unused => new BusinessUnused306Exception(message, exception) { Description = description },
                HttpStatusCode.RedirectKeepVerb => new BusinessRedirectKeepVerb307Exception(message, exception) { Description = description },
                HttpStatusCode.PermanentRedirect => new BusinessPermanentRedirect308Exception(message, exception) { Description = description },
                HttpStatusCode.BadRequest => new BusinessBadRequest400Exception(message, exception) { Description = description },
                HttpStatusCode.Unauthorized => new BusinessUnauthorized401Exception(message, exception) { Description = description },
                HttpStatusCode.PaymentRequired => new BusinessPaymentRequired402Exception(message, exception) { Description = description },
                HttpStatusCode.Forbidden => new BusinessForbidden403Exception(message, exception) { Description = description },
                HttpStatusCode.NotFound => new BusinessNotFound404Exception(message, exception) { Description = description },
                HttpStatusCode.MethodNotAllowed => new BusinessMethodNotAllowed405Exception(message, exception) { Description = description },
                HttpStatusCode.NotAcceptable => new BusinessNotAcceptable406Exception(message, exception) { Description = description },
                HttpStatusCode.ProxyAuthenticationRequired => new BusinessProxyAuthenticationRequired407Exception(message, exception) { Description = description },
                HttpStatusCode.RequestTimeout => new BusinessRequestTimeout408Exception(message, exception) { Description = description },
                HttpStatusCode.Conflict => new BusinessConflict409Exception(message, exception) { Description = description },
                HttpStatusCode.Gone => new BusinessGone410Exception(message, exception) { Description = description },
                HttpStatusCode.LengthRequired => new BusinessLengthRequired411Exception(message, exception) { Description = description },
                HttpStatusCode.PreconditionFailed => new BusinessPreconditionFailed412Exception(message, exception) { Description = description },
                HttpStatusCode.RequestEntityTooLarge => new BusinessRequestEntityTooLarge413Exception(message, exception) { Description = description },
                HttpStatusCode.RequestUriTooLong => new BusinessRequestUriTooLong414Exception(message, exception) { Description = description },
                HttpStatusCode.UnsupportedMediaType => new BusinessUnsupportedMediaType415Exception(message, exception) { Description = description },
                HttpStatusCode.RequestedRangeNotSatisfiable => new BusinessRequestedRangeNotSatisfiable416Exception(message, exception) { Description = description },
                HttpStatusCode.ExpectationFailed => new BusinessExpectationFailed417Exception(message, exception) { Description = description },
                HttpStatusCode.MisdirectedRequest => new BusinessMisdirectedRequest421Exception(message, exception) { Description = description },
                HttpStatusCode.UnprocessableEntity => new BusinessUnprocessableEntity422Exception(message, exception) { Description = description },
                HttpStatusCode.Locked => new BusinessLocked423Exception(message, exception) { Description = description },
                HttpStatusCode.FailedDependency => new BusinessFailedDependency424Exception(message, exception) { Description = description },
                HttpStatusCode.UpgradeRequired => new BusinessUpgradeRequired426Exception(message, exception) { Description = description },
                HttpStatusCode.PreconditionRequired => new BusinessPreconditionRequired428Exception(message, exception) { Description = description },
                HttpStatusCode.TooManyRequests => new BusinessTooManyRequests429Exception(message, exception) { Description = description },
                HttpStatusCode.RequestHeaderFieldsTooLarge => new BusinessRequestHeaderFieldsTooLarge431Exception(message, exception) { Description = description },
                HttpStatusCode.UnavailableForLegalReasons => new BusinessUnavailableForLegalReasons451Exception(message, exception) { Description = description },
                HttpStatusCode.InternalServerError => new BusinessInternalServerError500Exception(message, exception) { Description = description },
                HttpStatusCode.NotImplemented => new BusinessNotImplemented501Exception(message, exception) { Description = description },
                HttpStatusCode.BadGateway => new BusinessBadGateway502Exception(message, exception) { Description = description },
                HttpStatusCode.ServiceUnavailable => new BusinessServiceUnavailable503Exception(message, exception) { Description = description },
                HttpStatusCode.GatewayTimeout => new BusinessGatewayTimeout504Exception(message, exception) { Description = description },
                HttpStatusCode.HttpVersionNotSupported => new BusinessHttpVersionNotSupported505Exception(message, exception) { Description = description },
                HttpStatusCode.VariantAlsoNegotiates => new BusinessVariantAlsoNegotiates506Exception(message, exception) { Description = description },
                HttpStatusCode.InsufficientStorage => new BusinessInsufficientStorage507Exception(message, exception) { Description = description },
                HttpStatusCode.LoopDetected => new BusinessLoopDetected508Exception(message, exception) { Description = description },
                HttpStatusCode.NotExtended => new BusinessNotExtended510Exception(message, exception) { Description = description },
                HttpStatusCode.NetworkAuthenticationRequired => new BusinessNetworkAuthenticationRequired511Exception(message, exception) { Description = description },
                _ => new BusinessStatusException(message, status, exception)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessException<T> Create<T>(HttpStatusCode status, T code)
        {
            return BusinessException<T>.Create(status, code);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessException<T> Create<T>(HttpStatusCode status, T code, String? message)
        {
            return BusinessException<T>.Create(status, code, message);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessException<T> Create<T>(HttpStatusCode status, T code, String? message, String? description)
        {
            return BusinessException<T>.Create(status, code, message, description);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessException<T> Create<T>(HttpStatusCode status, T code, Exception? exception)
        {
            return BusinessException<T>.Create(status, code, exception);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessException<T> Create<T>(HttpStatusCode status, T code, String? message, Exception? exception)
        {
            return BusinessException<T>.Create(status, code, message, exception);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BusinessException<T> Create<T>(HttpStatusCode status, T code, String? message, String? description, Exception? exception)
        {
            return BusinessException<T>.Create(status, code, message, description, exception);
        }
    }

    [Serializable]
    public class BusinessStatusException : BusinessException
    {
        public BusinessStatusException(HttpStatusCode status)
            : base(status)
        {
        }

        public BusinessStatusException(String? message, HttpStatusCode status)
            : base(message, status)
        {
        }

        public BusinessStatusException(String? message, HttpStatusCode status, Exception? exception)
            : base(message, status, exception)
        {
        }

        public BusinessStatusException(String? message, HttpStatusCode status, BusinessException? exception)
            : base(message, status, exception)
        {
        }

        public BusinessStatusException(String? message, HttpStatusCode status, params BusinessException?[]? inner)
            : base(message, status, inner)
        {
        }

        public BusinessStatusException(String? message, HttpStatusCode status, Exception? exception, params BusinessException?[]? inner)
            : base(message, status, exception, inner)
        {
        }

        public BusinessStatusException(String? message, HttpStatusCode status, BusinessException? exception, params BusinessException?[]? inner)
            : base(message, status, exception, inner)
        {
        }

        protected BusinessStatusException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class BusinessStatusException<T> : BusinessException<T>
    {
        public BusinessStatusException(HttpStatusCode status, T code)
            : base(status, code)
        {
        }

        public BusinessStatusException(String? message, HttpStatusCode status, T code)
            : base(message, status, code)
        {
        }

        public BusinessStatusException(String? message, HttpStatusCode status, T code, Exception? exception)
            : base(message, status, code, exception)
        {
        }

        public BusinessStatusException(String? message, HttpStatusCode status, T code, BusinessException? exception)
            : base(message, status, code, exception)
        {
        }

        public BusinessStatusException(String? message, HttpStatusCode status, T code, params BusinessException?[]? inner)
            : base(message, status, code, inner)
        {
        }

        public BusinessStatusException(String? message, HttpStatusCode status, T code, Exception? exception, params BusinessException?[]? inner)
            : base(message, status, code, exception, inner)
        {
        }

        public BusinessStatusException(String? message, HttpStatusCode status, T code, BusinessException? exception, params BusinessException?[]? inner)
            : base(message, status, code, exception, inner)
        {
        }

        protected BusinessStatusException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class BusinessValidationException : BusinessStatusException
    {
        private new const HttpStatusCode Status = HttpStatusCode.UnprocessableEntity;

        public BusinessValidationException()
            : this(Status)
        {
        }

        public BusinessValidationException(HttpStatusCode status)
            : base(status)
        {
        }

        public BusinessValidationException(String? message)
            : this(message, Status)
        {
        }

        public BusinessValidationException(String? message, HttpStatusCode status)
            : base(message, status)
        {
        }

        public BusinessValidationException(String? message, Exception? exception)
            : this(message, Status, exception)
        {
        }

        public BusinessValidationException(String? message, HttpStatusCode status, Exception? exception)
            : base(message, status, exception)
        {
        }

        public BusinessValidationException(String? message, BusinessException? exception)
            : this(message, Status, exception)
        {
        }

        public BusinessValidationException(String? message, HttpStatusCode status, BusinessException? exception)
            : base(message, status, exception)
        {
        }

        public BusinessValidationException(String? message, params BusinessException?[]? inner)
            : this(message, Status, inner)
        {
        }

        public BusinessValidationException(String? message, HttpStatusCode status, params BusinessException?[]? inner)
            : base(message, status, inner)
        {
        }

        public BusinessValidationException(String? message, Exception? exception, params BusinessException?[]? inner)
            : this(message, Status, exception, inner)
        {
        }

        public BusinessValidationException(String? message, HttpStatusCode status, Exception? exception, params BusinessException?[]? inner)
            : base(message, status, exception, inner)
        {
        }

        public BusinessValidationException(String? message, BusinessException? exception, params BusinessException?[]? inner)
            : this(message, Status, exception, inner)
        {
        }

        public BusinessValidationException(String? message, HttpStatusCode status, BusinessException? exception, params BusinessException?[]? inner)
            : base(message, status, exception, inner)
        {
        }

        protected BusinessValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class BusinessValidationException<T> : BusinessStatusException<T>
    {
        private new const HttpStatusCode Status = HttpStatusCode.UnprocessableEntity;
        
        public BusinessValidationException(T code)
            : this(Status, code)
        {
        }

        public BusinessValidationException(HttpStatusCode status, T code)
            : base(status, code)
        {
        }

        public BusinessValidationException(String? message, T code)
            : this(message, Status, code)
        {
        }

        public BusinessValidationException(String? message, HttpStatusCode status, T code)
            : base(message, status, code)
        {
        }

        public BusinessValidationException(String? message, T code, Exception? exception)
            : this(message, Status, code, exception)
        {
        }

        public BusinessValidationException(String? message, HttpStatusCode status, T code, Exception? exception)
            : base(message, status, code, exception)
        {
        }

        public BusinessValidationException(String? message, T code, BusinessException? exception)
            : this(message, Status, code, exception)
        {
        }

        public BusinessValidationException(String? message, HttpStatusCode status, T code, BusinessException? exception)
            : base(message, status, code, exception)
        {
        }

        public BusinessValidationException(String? message, T code, params BusinessException?[]? inner)
            : this(message, Status, code, inner)
        {
        }

        public BusinessValidationException(String? message, HttpStatusCode status, T code, params BusinessException?[]? inner)
            : base(message, status, code, inner)
        {
        }

        public BusinessValidationException(String? message, T code, Exception? exception, params BusinessException?[]? inner)
            : this(message, Status, code, exception, inner)
        {
        }

        public BusinessValidationException(String? message, HttpStatusCode status, T code, Exception? exception, params BusinessException?[]? inner)
            : base(message, status, code, exception, inner)
        {
        }

        public BusinessValidationException(String? message, T code, BusinessException? exception, params BusinessException?[]? inner)
            : this(message, Status, code, exception, inner)
        {
        }

        public BusinessValidationException(String? message, HttpStatusCode status, T code, BusinessException? exception, params BusinessException?[]? inner)
            : base(message, status, code, exception, inner)
        {
        }

        protected BusinessValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}