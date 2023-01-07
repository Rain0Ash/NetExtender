// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
	[Serializable]
	public class BusinessContinue100Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.Continue;

		public BusinessContinue100Exception()
			: base(Generic)
		{
		}

		public BusinessContinue100Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessContinue100Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessContinue100Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessContinue100Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.Continue;

		public BusinessContinue100Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessContinue100Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessContinue100Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessContinue100Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessSwitchingProtocols101Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.SwitchingProtocols;

		public BusinessSwitchingProtocols101Exception()
			: base(Generic)
		{
		}

		public BusinessSwitchingProtocols101Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessSwitchingProtocols101Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessSwitchingProtocols101Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessSwitchingProtocols101Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.SwitchingProtocols;

		public BusinessSwitchingProtocols101Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessSwitchingProtocols101Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessSwitchingProtocols101Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessSwitchingProtocols101Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessOK200Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.OK;

		public BusinessOK200Exception()
			: base(Generic)
		{
		}

		public BusinessOK200Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessOK200Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessOK200Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessOK200Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.OK;

		public BusinessOK200Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessOK200Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessOK200Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessOK200Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessCreated201Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.Created;

		public BusinessCreated201Exception()
			: base(Generic)
		{
		}

		public BusinessCreated201Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessCreated201Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessCreated201Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessCreated201Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.Created;

		public BusinessCreated201Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessCreated201Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessCreated201Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessCreated201Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessAccepted202Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.Accepted;

		public BusinessAccepted202Exception()
			: base(Generic)
		{
		}

		public BusinessAccepted202Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessAccepted202Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessAccepted202Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessAccepted202Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.Accepted;

		public BusinessAccepted202Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessAccepted202Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessAccepted202Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessAccepted202Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessNonAuthoritativeInformation203Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.NonAuthoritativeInformation;

		public BusinessNonAuthoritativeInformation203Exception()
			: base(Generic)
		{
		}

		public BusinessNonAuthoritativeInformation203Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessNonAuthoritativeInformation203Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessNonAuthoritativeInformation203Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessNonAuthoritativeInformation203Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.NonAuthoritativeInformation;

		public BusinessNonAuthoritativeInformation203Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessNonAuthoritativeInformation203Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessNonAuthoritativeInformation203Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessNonAuthoritativeInformation203Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessNoContent204Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.NoContent;

		public BusinessNoContent204Exception()
			: base(Generic)
		{
		}

		public BusinessNoContent204Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessNoContent204Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessNoContent204Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessNoContent204Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.NoContent;

		public BusinessNoContent204Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessNoContent204Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessNoContent204Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessNoContent204Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessResetContent205Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.ResetContent;

		public BusinessResetContent205Exception()
			: base(Generic)
		{
		}

		public BusinessResetContent205Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessResetContent205Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessResetContent205Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessResetContent205Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.ResetContent;

		public BusinessResetContent205Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessResetContent205Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessResetContent205Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessResetContent205Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessPartialContent206Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.PartialContent;

		public BusinessPartialContent206Exception()
			: base(Generic)
		{
		}

		public BusinessPartialContent206Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessPartialContent206Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessPartialContent206Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessPartialContent206Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.PartialContent;

		public BusinessPartialContent206Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessPartialContent206Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessPartialContent206Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessPartialContent206Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessMultipleChoices300Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.MultipleChoices;

		public BusinessMultipleChoices300Exception()
			: base(Generic)
		{
		}

		public BusinessMultipleChoices300Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessMultipleChoices300Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessMultipleChoices300Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessMultipleChoices300Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.MultipleChoices;

		public BusinessMultipleChoices300Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessMultipleChoices300Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessMultipleChoices300Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessMultipleChoices300Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessAmbiguous300Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.Ambiguous;

		public BusinessAmbiguous300Exception()
			: base(Generic)
		{
		}

		public BusinessAmbiguous300Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessAmbiguous300Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessAmbiguous300Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessAmbiguous300Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.Ambiguous;

		public BusinessAmbiguous300Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessAmbiguous300Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessAmbiguous300Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessAmbiguous300Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessMovedPermanently301Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.MovedPermanently;

		public BusinessMovedPermanently301Exception()
			: base(Generic)
		{
		}

		public BusinessMovedPermanently301Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessMovedPermanently301Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessMovedPermanently301Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessMovedPermanently301Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.MovedPermanently;

		public BusinessMovedPermanently301Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessMovedPermanently301Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessMovedPermanently301Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessMovedPermanently301Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessMoved301Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.Moved;

		public BusinessMoved301Exception()
			: base(Generic)
		{
		}

		public BusinessMoved301Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessMoved301Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessMoved301Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessMoved301Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.Moved;

		public BusinessMoved301Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessMoved301Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessMoved301Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessMoved301Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessFound302Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.Found;

		public BusinessFound302Exception()
			: base(Generic)
		{
		}

		public BusinessFound302Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessFound302Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessFound302Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessFound302Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.Found;

		public BusinessFound302Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessFound302Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessFound302Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessFound302Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessRedirect302Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.Redirect;

		public BusinessRedirect302Exception()
			: base(Generic)
		{
		}

		public BusinessRedirect302Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessRedirect302Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessRedirect302Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessRedirect302Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.Redirect;

		public BusinessRedirect302Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessRedirect302Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessRedirect302Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessRedirect302Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessSeeOther303Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.SeeOther;

		public BusinessSeeOther303Exception()
			: base(Generic)
		{
		}

		public BusinessSeeOther303Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessSeeOther303Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessSeeOther303Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessSeeOther303Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.SeeOther;

		public BusinessSeeOther303Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessSeeOther303Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessSeeOther303Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessSeeOther303Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessRedirectMethod303Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.RedirectMethod;

		public BusinessRedirectMethod303Exception()
			: base(Generic)
		{
		}

		public BusinessRedirectMethod303Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessRedirectMethod303Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessRedirectMethod303Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessRedirectMethod303Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.RedirectMethod;

		public BusinessRedirectMethod303Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessRedirectMethod303Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessRedirectMethod303Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessRedirectMethod303Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessNotModified304Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.NotModified;

		public BusinessNotModified304Exception()
			: base(Generic)
		{
		}

		public BusinessNotModified304Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessNotModified304Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessNotModified304Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessNotModified304Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.NotModified;

		public BusinessNotModified304Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessNotModified304Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessNotModified304Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessNotModified304Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessUseProxy305Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.UseProxy;

		public BusinessUseProxy305Exception()
			: base(Generic)
		{
		}

		public BusinessUseProxy305Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessUseProxy305Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessUseProxy305Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessUseProxy305Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.UseProxy;

		public BusinessUseProxy305Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessUseProxy305Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessUseProxy305Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessUseProxy305Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessUnused306Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.Unused;

		public BusinessUnused306Exception()
			: base(Generic)
		{
		}

		public BusinessUnused306Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessUnused306Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessUnused306Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessUnused306Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.Unused;

		public BusinessUnused306Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessUnused306Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessUnused306Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessUnused306Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessTemporaryRedirect307Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.TemporaryRedirect;

		public BusinessTemporaryRedirect307Exception()
			: base(Generic)
		{
		}

		public BusinessTemporaryRedirect307Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessTemporaryRedirect307Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessTemporaryRedirect307Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessTemporaryRedirect307Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.TemporaryRedirect;

		public BusinessTemporaryRedirect307Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessTemporaryRedirect307Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessTemporaryRedirect307Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessTemporaryRedirect307Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessRedirectKeepVerb307Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.RedirectKeepVerb;

		public BusinessRedirectKeepVerb307Exception()
			: base(Generic)
		{
		}

		public BusinessRedirectKeepVerb307Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessRedirectKeepVerb307Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessRedirectKeepVerb307Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessRedirectKeepVerb307Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.RedirectKeepVerb;

		public BusinessRedirectKeepVerb307Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessRedirectKeepVerb307Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessRedirectKeepVerb307Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessRedirectKeepVerb307Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessBadRequest400Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.BadRequest;

		public BusinessBadRequest400Exception()
			: base(Generic)
		{
		}

		public BusinessBadRequest400Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessBadRequest400Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessBadRequest400Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessBadRequest400Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.BadRequest;

		public BusinessBadRequest400Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessBadRequest400Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessBadRequest400Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessBadRequest400Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessUnauthorized401Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.Unauthorized;

		public BusinessUnauthorized401Exception()
			: base(Generic)
		{
		}

		public BusinessUnauthorized401Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessUnauthorized401Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessUnauthorized401Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessUnauthorized401Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.Unauthorized;

		public BusinessUnauthorized401Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessUnauthorized401Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessUnauthorized401Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessUnauthorized401Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessPaymentRequired402Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.PaymentRequired;

		public BusinessPaymentRequired402Exception()
			: base(Generic)
		{
		}

		public BusinessPaymentRequired402Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessPaymentRequired402Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessPaymentRequired402Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessPaymentRequired402Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.PaymentRequired;

		public BusinessPaymentRequired402Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessPaymentRequired402Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessPaymentRequired402Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessPaymentRequired402Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessForbidden403Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.Forbidden;

		public BusinessForbidden403Exception()
			: base(Generic)
		{
		}

		public BusinessForbidden403Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessForbidden403Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessForbidden403Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessForbidden403Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.Forbidden;

		public BusinessForbidden403Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessForbidden403Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessForbidden403Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessForbidden403Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessNotFound404Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.NotFound;

		public BusinessNotFound404Exception()
			: base(Generic)
		{
		}

		public BusinessNotFound404Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessNotFound404Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessNotFound404Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessNotFound404Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.NotFound;

		public BusinessNotFound404Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessNotFound404Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessNotFound404Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessNotFound404Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessMethodNotAllowed405Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.MethodNotAllowed;

		public BusinessMethodNotAllowed405Exception()
			: base(Generic)
		{
		}

		public BusinessMethodNotAllowed405Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessMethodNotAllowed405Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessMethodNotAllowed405Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessMethodNotAllowed405Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.MethodNotAllowed;

		public BusinessMethodNotAllowed405Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessMethodNotAllowed405Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessMethodNotAllowed405Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessMethodNotAllowed405Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessNotAcceptable406Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.NotAcceptable;

		public BusinessNotAcceptable406Exception()
			: base(Generic)
		{
		}

		public BusinessNotAcceptable406Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessNotAcceptable406Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessNotAcceptable406Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessNotAcceptable406Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.NotAcceptable;

		public BusinessNotAcceptable406Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessNotAcceptable406Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessNotAcceptable406Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessNotAcceptable406Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessProxyAuthenticationRequired407Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.ProxyAuthenticationRequired;

		public BusinessProxyAuthenticationRequired407Exception()
			: base(Generic)
		{
		}

		public BusinessProxyAuthenticationRequired407Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessProxyAuthenticationRequired407Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessProxyAuthenticationRequired407Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessProxyAuthenticationRequired407Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.ProxyAuthenticationRequired;

		public BusinessProxyAuthenticationRequired407Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessProxyAuthenticationRequired407Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessProxyAuthenticationRequired407Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessProxyAuthenticationRequired407Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessRequestTimeout408Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.RequestTimeout;

		public BusinessRequestTimeout408Exception()
			: base(Generic)
		{
		}

		public BusinessRequestTimeout408Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessRequestTimeout408Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessRequestTimeout408Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessRequestTimeout408Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.RequestTimeout;

		public BusinessRequestTimeout408Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessRequestTimeout408Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessRequestTimeout408Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessRequestTimeout408Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessConflict409Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.Conflict;

		public BusinessConflict409Exception()
			: base(Generic)
		{
		}

		public BusinessConflict409Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessConflict409Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessConflict409Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessConflict409Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.Conflict;

		public BusinessConflict409Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessConflict409Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessConflict409Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessConflict409Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessGone410Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.Gone;

		public BusinessGone410Exception()
			: base(Generic)
		{
		}

		public BusinessGone410Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessGone410Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessGone410Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessGone410Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.Gone;

		public BusinessGone410Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessGone410Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessGone410Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessGone410Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessLengthRequired411Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.LengthRequired;

		public BusinessLengthRequired411Exception()
			: base(Generic)
		{
		}

		public BusinessLengthRequired411Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessLengthRequired411Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessLengthRequired411Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessLengthRequired411Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.LengthRequired;

		public BusinessLengthRequired411Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessLengthRequired411Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessLengthRequired411Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessLengthRequired411Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessPreconditionFailed412Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.PreconditionFailed;

		public BusinessPreconditionFailed412Exception()
			: base(Generic)
		{
		}

		public BusinessPreconditionFailed412Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessPreconditionFailed412Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessPreconditionFailed412Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessPreconditionFailed412Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.PreconditionFailed;

		public BusinessPreconditionFailed412Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessPreconditionFailed412Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessPreconditionFailed412Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessPreconditionFailed412Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessRequestEntityTooLarge413Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.RequestEntityTooLarge;

		public BusinessRequestEntityTooLarge413Exception()
			: base(Generic)
		{
		}

		public BusinessRequestEntityTooLarge413Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessRequestEntityTooLarge413Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessRequestEntityTooLarge413Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessRequestEntityTooLarge413Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.RequestEntityTooLarge;

		public BusinessRequestEntityTooLarge413Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessRequestEntityTooLarge413Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessRequestEntityTooLarge413Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessRequestEntityTooLarge413Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessRequestUriTooLong414Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.RequestUriTooLong;

		public BusinessRequestUriTooLong414Exception()
			: base(Generic)
		{
		}

		public BusinessRequestUriTooLong414Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessRequestUriTooLong414Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessRequestUriTooLong414Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessRequestUriTooLong414Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.RequestUriTooLong;

		public BusinessRequestUriTooLong414Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessRequestUriTooLong414Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessRequestUriTooLong414Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessRequestUriTooLong414Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessUnsupportedMediaType415Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.UnsupportedMediaType;

		public BusinessUnsupportedMediaType415Exception()
			: base(Generic)
		{
		}

		public BusinessUnsupportedMediaType415Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessUnsupportedMediaType415Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessUnsupportedMediaType415Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessUnsupportedMediaType415Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.UnsupportedMediaType;

		public BusinessUnsupportedMediaType415Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessUnsupportedMediaType415Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessUnsupportedMediaType415Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessUnsupportedMediaType415Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessRequestedRangeNotSatisfiable416Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.RequestedRangeNotSatisfiable;

		public BusinessRequestedRangeNotSatisfiable416Exception()
			: base(Generic)
		{
		}

		public BusinessRequestedRangeNotSatisfiable416Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessRequestedRangeNotSatisfiable416Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessRequestedRangeNotSatisfiable416Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessRequestedRangeNotSatisfiable416Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.RequestedRangeNotSatisfiable;

		public BusinessRequestedRangeNotSatisfiable416Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessRequestedRangeNotSatisfiable416Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessRequestedRangeNotSatisfiable416Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessRequestedRangeNotSatisfiable416Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessExpectationFailed417Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.ExpectationFailed;

		public BusinessExpectationFailed417Exception()
			: base(Generic)
		{
		}

		public BusinessExpectationFailed417Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessExpectationFailed417Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessExpectationFailed417Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessExpectationFailed417Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.ExpectationFailed;

		public BusinessExpectationFailed417Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessExpectationFailed417Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessExpectationFailed417Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessExpectationFailed417Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessIamATeapot418Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 418;

		public BusinessIamATeapot418Exception()
			: base(Generic)
		{
		}

		public BusinessIamATeapot418Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessIamATeapot418Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessIamATeapot418Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessIamATeapot418Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 418;

		public BusinessIamATeapot418Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessIamATeapot418Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessIamATeapot418Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessIamATeapot418Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessUpgradeRequired426Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.UpgradeRequired;

		public BusinessUpgradeRequired426Exception()
			: base(Generic)
		{
		}

		public BusinessUpgradeRequired426Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessUpgradeRequired426Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessUpgradeRequired426Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessUpgradeRequired426Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.UpgradeRequired;

		public BusinessUpgradeRequired426Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessUpgradeRequired426Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessUpgradeRequired426Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessUpgradeRequired426Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessInternalServerError500Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.InternalServerError;

		public BusinessInternalServerError500Exception()
			: base(Generic)
		{
		}

		public BusinessInternalServerError500Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessInternalServerError500Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessInternalServerError500Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessInternalServerError500Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.InternalServerError;

		public BusinessInternalServerError500Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessInternalServerError500Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessInternalServerError500Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessInternalServerError500Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessNotImplemented501Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.NotImplemented;

		public BusinessNotImplemented501Exception()
			: base(Generic)
		{
		}

		public BusinessNotImplemented501Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessNotImplemented501Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessNotImplemented501Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessNotImplemented501Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.NotImplemented;

		public BusinessNotImplemented501Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessNotImplemented501Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessNotImplemented501Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessNotImplemented501Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessBadGateway502Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.BadGateway;

		public BusinessBadGateway502Exception()
			: base(Generic)
		{
		}

		public BusinessBadGateway502Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessBadGateway502Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessBadGateway502Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessBadGateway502Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.BadGateway;

		public BusinessBadGateway502Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessBadGateway502Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessBadGateway502Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessBadGateway502Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessServiceUnavailable503Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.ServiceUnavailable;

		public BusinessServiceUnavailable503Exception()
			: base(Generic)
		{
		}

		public BusinessServiceUnavailable503Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessServiceUnavailable503Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessServiceUnavailable503Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessServiceUnavailable503Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.ServiceUnavailable;

		public BusinessServiceUnavailable503Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessServiceUnavailable503Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessServiceUnavailable503Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessServiceUnavailable503Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessGatewayTimeout504Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.GatewayTimeout;

		public BusinessGatewayTimeout504Exception()
			: base(Generic)
		{
		}

		public BusinessGatewayTimeout504Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessGatewayTimeout504Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessGatewayTimeout504Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessGatewayTimeout504Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.GatewayTimeout;

		public BusinessGatewayTimeout504Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessGatewayTimeout504Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessGatewayTimeout504Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessGatewayTimeout504Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessHttpVersionNotSupported505Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = HttpStatusCode.HttpVersionNotSupported;

		public BusinessHttpVersionNotSupported505Exception()
			: base(Generic)
		{
		}

		public BusinessHttpVersionNotSupported505Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessHttpVersionNotSupported505Exception(String? message, Exception? innerException)
			: base(message, Generic, innerException)
		{
		}

		protected BusinessHttpVersionNotSupported505Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessHttpVersionNotSupported505Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = HttpStatusCode.HttpVersionNotSupported;

		public BusinessHttpVersionNotSupported505Exception(T code)
			: base(code, Generic)
		{
		}

		public BusinessHttpVersionNotSupported505Exception(String? message, T code)
			: base(message, code, Generic)
		{
		}

		public BusinessHttpVersionNotSupported505Exception(String? message, T code, Exception? innerException)
			: base(message, code, Generic, innerException)
		{
		}

		protected BusinessHttpVersionNotSupported505Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}