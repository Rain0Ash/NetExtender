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

		public BusinessContinue100Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessContinue100Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessContinue100Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessContinue100Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessContinue100Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessContinue100Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessContinue100Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessContinue100Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessContinue100Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessContinue100Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessSwitchingProtocols101Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessSwitchingProtocols101Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessSwitchingProtocols101Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessSwitchingProtocols101Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessSwitchingProtocols101Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessSwitchingProtocols101Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessSwitchingProtocols101Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessSwitchingProtocols101Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessSwitchingProtocols101Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessSwitchingProtocols101Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessSwitchingProtocols101Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessProcessing102Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 102;

		public BusinessProcessing102Exception()
			: base(Generic)
		{
		}

		public BusinessProcessing102Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessProcessing102Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessProcessing102Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessProcessing102Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessProcessing102Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessProcessing102Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessProcessing102Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessProcessing102Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 102;

		public BusinessProcessing102Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessProcessing102Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessProcessing102Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessProcessing102Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessProcessing102Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessProcessing102Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessProcessing102Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessEarlyHints103Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 103;

		public BusinessEarlyHints103Exception()
			: base(Generic)
		{
		}

		public BusinessEarlyHints103Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessEarlyHints103Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessEarlyHints103Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessEarlyHints103Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessEarlyHints103Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessEarlyHints103Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessEarlyHints103Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessEarlyHints103Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 103;

		public BusinessEarlyHints103Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessEarlyHints103Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessEarlyHints103Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessEarlyHints103Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessEarlyHints103Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessEarlyHints103Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessEarlyHints103Exception(SerializationInfo info, StreamingContext context)
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

		public BusinessOK200Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessOK200Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessOK200Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessOK200Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessOK200Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessOK200Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessOK200Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessOK200Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessOK200Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessOK200Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessCreated201Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessCreated201Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessCreated201Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessCreated201Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessCreated201Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessCreated201Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessCreated201Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessCreated201Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessCreated201Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessCreated201Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessAccepted202Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessAccepted202Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessAccepted202Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessAccepted202Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessAccepted202Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessAccepted202Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessAccepted202Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessAccepted202Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessAccepted202Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessAccepted202Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessNonAuthoritativeInformation203Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessNonAuthoritativeInformation203Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessNonAuthoritativeInformation203Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessNonAuthoritativeInformation203Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessNonAuthoritativeInformation203Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessNonAuthoritativeInformation203Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessNonAuthoritativeInformation203Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessNonAuthoritativeInformation203Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessNonAuthoritativeInformation203Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessNonAuthoritativeInformation203Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessNoContent204Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessNoContent204Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessNoContent204Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessNoContent204Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessNoContent204Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessNoContent204Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessNoContent204Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessNoContent204Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessNoContent204Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessNoContent204Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessResetContent205Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessResetContent205Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessResetContent205Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessResetContent205Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessResetContent205Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessResetContent205Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessResetContent205Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessResetContent205Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessResetContent205Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessResetContent205Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessPartialContent206Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessPartialContent206Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessPartialContent206Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessPartialContent206Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessPartialContent206Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessPartialContent206Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessPartialContent206Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessPartialContent206Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessPartialContent206Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessPartialContent206Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessPartialContent206Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessMultiStatus207Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 207;

		public BusinessMultiStatus207Exception()
			: base(Generic)
		{
		}

		public BusinessMultiStatus207Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessMultiStatus207Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessMultiStatus207Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessMultiStatus207Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessMultiStatus207Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessMultiStatus207Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessMultiStatus207Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessMultiStatus207Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 207;

		public BusinessMultiStatus207Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessMultiStatus207Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessMultiStatus207Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessMultiStatus207Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessMultiStatus207Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessMultiStatus207Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessMultiStatus207Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessAlreadyReported208Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 208;

		public BusinessAlreadyReported208Exception()
			: base(Generic)
		{
		}

		public BusinessAlreadyReported208Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessAlreadyReported208Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessAlreadyReported208Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessAlreadyReported208Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessAlreadyReported208Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessAlreadyReported208Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessAlreadyReported208Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessAlreadyReported208Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 208;

		public BusinessAlreadyReported208Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessAlreadyReported208Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessAlreadyReported208Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessAlreadyReported208Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessAlreadyReported208Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessAlreadyReported208Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessAlreadyReported208Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessIMUsed226Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 226;

		public BusinessIMUsed226Exception()
			: base(Generic)
		{
		}

		public BusinessIMUsed226Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessIMUsed226Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessIMUsed226Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessIMUsed226Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessIMUsed226Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessIMUsed226Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessIMUsed226Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessIMUsed226Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 226;

		public BusinessIMUsed226Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessIMUsed226Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessIMUsed226Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessIMUsed226Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessIMUsed226Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessIMUsed226Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessIMUsed226Exception(SerializationInfo info, StreamingContext context)
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

		public BusinessMultipleChoices300Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessMultipleChoices300Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessMultipleChoices300Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessMultipleChoices300Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessMultipleChoices300Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessMultipleChoices300Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessMultipleChoices300Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessMultipleChoices300Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessMultipleChoices300Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessMultipleChoices300Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessAmbiguous300Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessAmbiguous300Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessAmbiguous300Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessAmbiguous300Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessAmbiguous300Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessAmbiguous300Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessAmbiguous300Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessAmbiguous300Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessAmbiguous300Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessAmbiguous300Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessMovedPermanently301Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessMovedPermanently301Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessMovedPermanently301Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessMovedPermanently301Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessMovedPermanently301Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessMovedPermanently301Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessMovedPermanently301Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessMovedPermanently301Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessMovedPermanently301Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessMovedPermanently301Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessMoved301Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessMoved301Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessMoved301Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessMoved301Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessMoved301Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessMoved301Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessMoved301Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessMoved301Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessMoved301Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessMoved301Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessFound302Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessFound302Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessFound302Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessFound302Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessFound302Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessFound302Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessFound302Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessFound302Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessFound302Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessFound302Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessRedirect302Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessRedirect302Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessRedirect302Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessRedirect302Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessRedirect302Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessRedirect302Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessRedirect302Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessRedirect302Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessRedirect302Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessRedirect302Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessSeeOther303Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessSeeOther303Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessSeeOther303Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessSeeOther303Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessSeeOther303Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessSeeOther303Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessSeeOther303Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessSeeOther303Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessSeeOther303Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessSeeOther303Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessRedirectMethod303Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessRedirectMethod303Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessRedirectMethod303Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessRedirectMethod303Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessRedirectMethod303Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessRedirectMethod303Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessRedirectMethod303Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessRedirectMethod303Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessRedirectMethod303Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessRedirectMethod303Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessNotModified304Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessNotModified304Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessNotModified304Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessNotModified304Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessNotModified304Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessNotModified304Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessNotModified304Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessNotModified304Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessNotModified304Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessNotModified304Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessUseProxy305Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessUseProxy305Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessUseProxy305Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessUseProxy305Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessUseProxy305Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessUseProxy305Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessUseProxy305Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessUseProxy305Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessUseProxy305Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessUseProxy305Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessUnused306Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessUnused306Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessUnused306Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessUnused306Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessUnused306Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessUnused306Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessUnused306Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessUnused306Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessUnused306Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessUnused306Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessTemporaryRedirect307Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessTemporaryRedirect307Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessTemporaryRedirect307Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessTemporaryRedirect307Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessTemporaryRedirect307Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessTemporaryRedirect307Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessTemporaryRedirect307Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessTemporaryRedirect307Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessTemporaryRedirect307Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessTemporaryRedirect307Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessRedirectKeepVerb307Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessRedirectKeepVerb307Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessRedirectKeepVerb307Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessRedirectKeepVerb307Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessRedirectKeepVerb307Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessRedirectKeepVerb307Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessRedirectKeepVerb307Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessRedirectKeepVerb307Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessRedirectKeepVerb307Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessRedirectKeepVerb307Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessRedirectKeepVerb307Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessPermanentRedirect308Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 308;

		public BusinessPermanentRedirect308Exception()
			: base(Generic)
		{
		}

		public BusinessPermanentRedirect308Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessPermanentRedirect308Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessPermanentRedirect308Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessPermanentRedirect308Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessPermanentRedirect308Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessPermanentRedirect308Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessPermanentRedirect308Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessPermanentRedirect308Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 308;

		public BusinessPermanentRedirect308Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessPermanentRedirect308Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessPermanentRedirect308Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessPermanentRedirect308Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessPermanentRedirect308Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessPermanentRedirect308Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessPermanentRedirect308Exception(SerializationInfo info, StreamingContext context)
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

		public BusinessBadRequest400Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessBadRequest400Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessBadRequest400Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessBadRequest400Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessBadRequest400Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessBadRequest400Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessBadRequest400Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessBadRequest400Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessBadRequest400Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessBadRequest400Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessUnauthorized401Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessUnauthorized401Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessUnauthorized401Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessUnauthorized401Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessUnauthorized401Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessUnauthorized401Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessUnauthorized401Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessUnauthorized401Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessUnauthorized401Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessUnauthorized401Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessPaymentRequired402Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessPaymentRequired402Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessPaymentRequired402Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessPaymentRequired402Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessPaymentRequired402Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessPaymentRequired402Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessPaymentRequired402Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessPaymentRequired402Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessPaymentRequired402Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessPaymentRequired402Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessForbidden403Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessForbidden403Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessForbidden403Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessForbidden403Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessForbidden403Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessForbidden403Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessForbidden403Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessForbidden403Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessForbidden403Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessForbidden403Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessNotFound404Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessNotFound404Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessNotFound404Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessNotFound404Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessNotFound404Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessNotFound404Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessNotFound404Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessNotFound404Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessNotFound404Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessNotFound404Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessMethodNotAllowed405Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessMethodNotAllowed405Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessMethodNotAllowed405Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessMethodNotAllowed405Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessMethodNotAllowed405Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessMethodNotAllowed405Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessMethodNotAllowed405Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessMethodNotAllowed405Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessMethodNotAllowed405Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessMethodNotAllowed405Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessNotAcceptable406Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessNotAcceptable406Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessNotAcceptable406Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessNotAcceptable406Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessNotAcceptable406Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessNotAcceptable406Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessNotAcceptable406Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessNotAcceptable406Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessNotAcceptable406Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessNotAcceptable406Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessProxyAuthenticationRequired407Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessProxyAuthenticationRequired407Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessProxyAuthenticationRequired407Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessProxyAuthenticationRequired407Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessProxyAuthenticationRequired407Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessProxyAuthenticationRequired407Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessProxyAuthenticationRequired407Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessProxyAuthenticationRequired407Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessProxyAuthenticationRequired407Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessProxyAuthenticationRequired407Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessRequestTimeout408Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessRequestTimeout408Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessRequestTimeout408Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessRequestTimeout408Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessRequestTimeout408Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessRequestTimeout408Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessRequestTimeout408Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessRequestTimeout408Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessRequestTimeout408Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessRequestTimeout408Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessConflict409Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessConflict409Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessConflict409Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessConflict409Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessConflict409Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessConflict409Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessConflict409Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessConflict409Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessConflict409Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessConflict409Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessGone410Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessGone410Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessGone410Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessGone410Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessGone410Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessGone410Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessGone410Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessGone410Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessGone410Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessGone410Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessLengthRequired411Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessLengthRequired411Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessLengthRequired411Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessLengthRequired411Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessLengthRequired411Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessLengthRequired411Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessLengthRequired411Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessLengthRequired411Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessLengthRequired411Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessLengthRequired411Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessPreconditionFailed412Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessPreconditionFailed412Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessPreconditionFailed412Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessPreconditionFailed412Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessPreconditionFailed412Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessPreconditionFailed412Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessPreconditionFailed412Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessPreconditionFailed412Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessPreconditionFailed412Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessPreconditionFailed412Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessRequestEntityTooLarge413Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessRequestEntityTooLarge413Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessRequestEntityTooLarge413Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessRequestEntityTooLarge413Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessRequestEntityTooLarge413Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessRequestEntityTooLarge413Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessRequestEntityTooLarge413Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessRequestEntityTooLarge413Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessRequestEntityTooLarge413Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessRequestEntityTooLarge413Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessRequestUriTooLong414Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessRequestUriTooLong414Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessRequestUriTooLong414Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessRequestUriTooLong414Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessRequestUriTooLong414Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessRequestUriTooLong414Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessRequestUriTooLong414Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessRequestUriTooLong414Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessRequestUriTooLong414Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessRequestUriTooLong414Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessUnsupportedMediaType415Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessUnsupportedMediaType415Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessUnsupportedMediaType415Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessUnsupportedMediaType415Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessUnsupportedMediaType415Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessUnsupportedMediaType415Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessUnsupportedMediaType415Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessUnsupportedMediaType415Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessUnsupportedMediaType415Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessUnsupportedMediaType415Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessRequestedRangeNotSatisfiable416Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessRequestedRangeNotSatisfiable416Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessRequestedRangeNotSatisfiable416Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessRequestedRangeNotSatisfiable416Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessRequestedRangeNotSatisfiable416Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessRequestedRangeNotSatisfiable416Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessRequestedRangeNotSatisfiable416Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessRequestedRangeNotSatisfiable416Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessRequestedRangeNotSatisfiable416Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessRequestedRangeNotSatisfiable416Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessExpectationFailed417Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessExpectationFailed417Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessExpectationFailed417Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessExpectationFailed417Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessExpectationFailed417Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessExpectationFailed417Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessExpectationFailed417Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessExpectationFailed417Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessExpectationFailed417Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessExpectationFailed417Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessIamATeapot418Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessIamATeapot418Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessIamATeapot418Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessIamATeapot418Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessIamATeapot418Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessIamATeapot418Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessIamATeapot418Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessIamATeapot418Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessIamATeapot418Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessIamATeapot418Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessIamATeapot418Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessMisdirectedRequest421Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 421;

		public BusinessMisdirectedRequest421Exception()
			: base(Generic)
		{
		}

		public BusinessMisdirectedRequest421Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessMisdirectedRequest421Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessMisdirectedRequest421Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessMisdirectedRequest421Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessMisdirectedRequest421Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessMisdirectedRequest421Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessMisdirectedRequest421Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessMisdirectedRequest421Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 421;

		public BusinessMisdirectedRequest421Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessMisdirectedRequest421Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessMisdirectedRequest421Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessMisdirectedRequest421Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessMisdirectedRequest421Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessMisdirectedRequest421Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessMisdirectedRequest421Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessUnprocessableEntity422Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 422;

		public BusinessUnprocessableEntity422Exception()
			: base(Generic)
		{
		}

		public BusinessUnprocessableEntity422Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessUnprocessableEntity422Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessUnprocessableEntity422Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessUnprocessableEntity422Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessUnprocessableEntity422Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessUnprocessableEntity422Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessUnprocessableEntity422Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessUnprocessableEntity422Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 422;

		public BusinessUnprocessableEntity422Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessUnprocessableEntity422Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessUnprocessableEntity422Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessUnprocessableEntity422Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessUnprocessableEntity422Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessUnprocessableEntity422Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessUnprocessableEntity422Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessLocked423Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 423;

		public BusinessLocked423Exception()
			: base(Generic)
		{
		}

		public BusinessLocked423Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessLocked423Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessLocked423Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessLocked423Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessLocked423Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessLocked423Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessLocked423Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessLocked423Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 423;

		public BusinessLocked423Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessLocked423Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessLocked423Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessLocked423Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessLocked423Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessLocked423Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessLocked423Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessFailedDependency424Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 424;

		public BusinessFailedDependency424Exception()
			: base(Generic)
		{
		}

		public BusinessFailedDependency424Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessFailedDependency424Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessFailedDependency424Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessFailedDependency424Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessFailedDependency424Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessFailedDependency424Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessFailedDependency424Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessFailedDependency424Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 424;

		public BusinessFailedDependency424Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessFailedDependency424Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessFailedDependency424Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessFailedDependency424Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessFailedDependency424Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessFailedDependency424Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessFailedDependency424Exception(SerializationInfo info, StreamingContext context)
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

		public BusinessUpgradeRequired426Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessUpgradeRequired426Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessUpgradeRequired426Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessUpgradeRequired426Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessUpgradeRequired426Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessUpgradeRequired426Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessUpgradeRequired426Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessUpgradeRequired426Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessUpgradeRequired426Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessUpgradeRequired426Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessUpgradeRequired426Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessPreconditionRequired428Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 428;

		public BusinessPreconditionRequired428Exception()
			: base(Generic)
		{
		}

		public BusinessPreconditionRequired428Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessPreconditionRequired428Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessPreconditionRequired428Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessPreconditionRequired428Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessPreconditionRequired428Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessPreconditionRequired428Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessPreconditionRequired428Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessPreconditionRequired428Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 428;

		public BusinessPreconditionRequired428Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessPreconditionRequired428Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessPreconditionRequired428Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessPreconditionRequired428Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessPreconditionRequired428Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessPreconditionRequired428Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessPreconditionRequired428Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessTooManyRequests429Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 429;

		public BusinessTooManyRequests429Exception()
			: base(Generic)
		{
		}

		public BusinessTooManyRequests429Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessTooManyRequests429Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessTooManyRequests429Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessTooManyRequests429Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessTooManyRequests429Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessTooManyRequests429Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessTooManyRequests429Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessTooManyRequests429Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 429;

		public BusinessTooManyRequests429Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessTooManyRequests429Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessTooManyRequests429Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessTooManyRequests429Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessTooManyRequests429Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessTooManyRequests429Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessTooManyRequests429Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessRequestHeaderFieldsTooLarge431Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 431;

		public BusinessRequestHeaderFieldsTooLarge431Exception()
			: base(Generic)
		{
		}

		public BusinessRequestHeaderFieldsTooLarge431Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessRequestHeaderFieldsTooLarge431Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessRequestHeaderFieldsTooLarge431Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessRequestHeaderFieldsTooLarge431Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessRequestHeaderFieldsTooLarge431Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessRequestHeaderFieldsTooLarge431Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessRequestHeaderFieldsTooLarge431Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessRequestHeaderFieldsTooLarge431Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 431;

		public BusinessRequestHeaderFieldsTooLarge431Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessRequestHeaderFieldsTooLarge431Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessRequestHeaderFieldsTooLarge431Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessRequestHeaderFieldsTooLarge431Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessRequestHeaderFieldsTooLarge431Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessRequestHeaderFieldsTooLarge431Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessRequestHeaderFieldsTooLarge431Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessUnavailableForLegalReasons451Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 451;

		public BusinessUnavailableForLegalReasons451Exception()
			: base(Generic)
		{
		}

		public BusinessUnavailableForLegalReasons451Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessUnavailableForLegalReasons451Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessUnavailableForLegalReasons451Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessUnavailableForLegalReasons451Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessUnavailableForLegalReasons451Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessUnavailableForLegalReasons451Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessUnavailableForLegalReasons451Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessUnavailableForLegalReasons451Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 451;

		public BusinessUnavailableForLegalReasons451Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessUnavailableForLegalReasons451Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessUnavailableForLegalReasons451Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessUnavailableForLegalReasons451Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessUnavailableForLegalReasons451Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessUnavailableForLegalReasons451Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessUnavailableForLegalReasons451Exception(SerializationInfo info, StreamingContext context)
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

		public BusinessInternalServerError500Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessInternalServerError500Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessInternalServerError500Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessInternalServerError500Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessInternalServerError500Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessInternalServerError500Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessInternalServerError500Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessInternalServerError500Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessInternalServerError500Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessInternalServerError500Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessNotImplemented501Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessNotImplemented501Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessNotImplemented501Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessNotImplemented501Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessNotImplemented501Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessNotImplemented501Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessNotImplemented501Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessNotImplemented501Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessNotImplemented501Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessNotImplemented501Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessBadGateway502Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessBadGateway502Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessBadGateway502Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessBadGateway502Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessBadGateway502Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessBadGateway502Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessBadGateway502Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessBadGateway502Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessBadGateway502Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessBadGateway502Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessServiceUnavailable503Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessServiceUnavailable503Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessServiceUnavailable503Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessServiceUnavailable503Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessServiceUnavailable503Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessServiceUnavailable503Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessServiceUnavailable503Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessServiceUnavailable503Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessServiceUnavailable503Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessServiceUnavailable503Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessGatewayTimeout504Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessGatewayTimeout504Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessGatewayTimeout504Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessGatewayTimeout504Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessGatewayTimeout504Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessGatewayTimeout504Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessGatewayTimeout504Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessGatewayTimeout504Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessGatewayTimeout504Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessGatewayTimeout504Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
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

		public BusinessHttpVersionNotSupported505Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessHttpVersionNotSupported505Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessHttpVersionNotSupported505Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessHttpVersionNotSupported505Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessHttpVersionNotSupported505Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
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
			: base(Generic, code)
		{
		}

		public BusinessHttpVersionNotSupported505Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessHttpVersionNotSupported505Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessHttpVersionNotSupported505Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessHttpVersionNotSupported505Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessHttpVersionNotSupported505Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessHttpVersionNotSupported505Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessVariantAlsoNegotiates506Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 506;

		public BusinessVariantAlsoNegotiates506Exception()
			: base(Generic)
		{
		}

		public BusinessVariantAlsoNegotiates506Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessVariantAlsoNegotiates506Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessVariantAlsoNegotiates506Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessVariantAlsoNegotiates506Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessVariantAlsoNegotiates506Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessVariantAlsoNegotiates506Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessVariantAlsoNegotiates506Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessVariantAlsoNegotiates506Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 506;

		public BusinessVariantAlsoNegotiates506Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessVariantAlsoNegotiates506Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessVariantAlsoNegotiates506Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessVariantAlsoNegotiates506Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessVariantAlsoNegotiates506Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessVariantAlsoNegotiates506Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessVariantAlsoNegotiates506Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessInsufficientStorage507Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 507;

		public BusinessInsufficientStorage507Exception()
			: base(Generic)
		{
		}

		public BusinessInsufficientStorage507Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessInsufficientStorage507Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessInsufficientStorage507Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessInsufficientStorage507Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessInsufficientStorage507Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessInsufficientStorage507Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessInsufficientStorage507Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessInsufficientStorage507Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 507;

		public BusinessInsufficientStorage507Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessInsufficientStorage507Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessInsufficientStorage507Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessInsufficientStorage507Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessInsufficientStorage507Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessInsufficientStorage507Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessInsufficientStorage507Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessLoopDetected508Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 508;

		public BusinessLoopDetected508Exception()
			: base(Generic)
		{
		}

		public BusinessLoopDetected508Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessLoopDetected508Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessLoopDetected508Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessLoopDetected508Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessLoopDetected508Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessLoopDetected508Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessLoopDetected508Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessLoopDetected508Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 508;

		public BusinessLoopDetected508Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessLoopDetected508Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessLoopDetected508Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessLoopDetected508Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessLoopDetected508Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessLoopDetected508Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessLoopDetected508Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessNotExtended510Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 510;

		public BusinessNotExtended510Exception()
			: base(Generic)
		{
		}

		public BusinessNotExtended510Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessNotExtended510Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessNotExtended510Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessNotExtended510Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessNotExtended510Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessNotExtended510Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessNotExtended510Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessNotExtended510Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 510;

		public BusinessNotExtended510Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessNotExtended510Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessNotExtended510Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessNotExtended510Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessNotExtended510Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessNotExtended510Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessNotExtended510Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessNetworkAuthenticationRequired511Exception : BusinessStatusException
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 511;

		public BusinessNetworkAuthenticationRequired511Exception()
			: base(Generic)
		{
		}

		public BusinessNetworkAuthenticationRequired511Exception(String? message)
			: base(message, Generic)
		{
		}

		public BusinessNetworkAuthenticationRequired511Exception(String? message, Exception? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessNetworkAuthenticationRequired511Exception(String? message, BusinessException? exception)
			: base(message, Generic, exception)
		{
		}

		public BusinessNetworkAuthenticationRequired511Exception(String? message, params BusinessException?[]? reason)
			: base(message, Generic, reason)
		{
		}

		public BusinessNetworkAuthenticationRequired511Exception(String? message, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		public BusinessNetworkAuthenticationRequired511Exception(String? message, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, exception, reason)
		{
		}

		protected BusinessNetworkAuthenticationRequired511Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}

	[Serializable]
	public class BusinessNetworkAuthenticationRequired511Exception<T> : BusinessStatusException<T>
	{
		private const HttpStatusCode Generic = (HttpStatusCode) 511;

		public BusinessNetworkAuthenticationRequired511Exception(T code)
			: base(Generic, code)
		{
		}

		public BusinessNetworkAuthenticationRequired511Exception(String? message, T code)
			: base(message, Generic, code)
		{
		}

		public BusinessNetworkAuthenticationRequired511Exception(String? message, T code, Exception? exception)
			: base(message, Generic, code, exception)
		{
		}

		public BusinessNetworkAuthenticationRequired511Exception(String? message, T code, params BusinessException?[]? reason)
			: base(message, Generic, code, reason)
		{
		}

		public BusinessNetworkAuthenticationRequired511Exception(String? message, T code, Exception? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		public BusinessNetworkAuthenticationRequired511Exception(String? message, T code, BusinessException? exception, params BusinessException?[]? reason)
			: base(message, Generic, code, exception, reason)
		{
		}

		protected BusinessNetworkAuthenticationRequired511Exception(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}