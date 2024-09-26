using System;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using NetExtender.Types.Network.Formatters.Interfaces;
using NetExtender.Utilities.Serialization;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Network.Formatters.Exceptions
{
    [Serializable]
    public class MediaTypeNotSupportedException<T> : MediaTypeNotSupportedException
    {
        public MediaTypeNotSupportedException()
            : base(typeof(T))
        {
        }
        
        public MediaTypeNotSupportedException(String? message)
            : base(typeof(T), message)
        {
        }
        
        public MediaTypeNotSupportedException(MediaTypeHeaderValue? media, String? message)
            : base(typeof(T), media, message)
        {
        }
        
        public MediaTypeNotSupportedException(String? message, Exception? innerException)
            : base(typeof(T), message, innerException)
        {
        }
        
        public MediaTypeNotSupportedException(MediaTypeHeaderValue? media, String? message, Exception? innerException)
            : base(typeof(T), media, message, innerException)
        {
        }
        
        protected MediaTypeNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public class MediaTypeNotSupportedException : NotSupportedException
    {
        private new const String Message = "Media type not supported.";
        private const String TypeMessage = "Media type for type '{0}' not supported.";
        private const String MediaMessage = "Media type '{0}' not supported.";
        private const String MediaTypeMessage = "Media type '{0}' for type '{1}' not supported.";
        
        public Type? Type { get; }
        public MediaTypeHeaderValue? MediaType { get; }
        public IReadOnlyMediaTypeFormatterCollection? Formatters { get; init; }

        public MediaTypeNotSupportedException()
            : base(Format(null, null, null))
        {
        }
        
        public MediaTypeNotSupportedException(Type? type)
            : base(Format(type, null, null))
        {
            Type = type;
        }
        
        public MediaTypeNotSupportedException(String? message)
            : base(Format(null, null, message))
        {
        }
        
        public MediaTypeNotSupportedException(MediaTypeHeaderValue? media, String? message)
            : base(Format(null, media, message))
        {
            MediaType = media;
        }
        
        public MediaTypeNotSupportedException(Type? type, String? message)
            : base(Format(type, null, message))
        {
            Type = type;
        }
        
        public MediaTypeNotSupportedException(Type? type, MediaTypeHeaderValue? media, String? message)
            : base(Format(type, media, message))
        {
            Type = type;
            MediaType = media;
        }
        
        public MediaTypeNotSupportedException(String? message, Exception? innerException)
            : base(Format(null, null, message), innerException)
        {
        }
        
        public MediaTypeNotSupportedException(MediaTypeHeaderValue? media, String? message, Exception? innerException)
            : base(Format(null, media, message), innerException)
        {
            MediaType = media;
        }
        
        public MediaTypeNotSupportedException(Type? type, String? message, Exception? innerException)
            : base(Format(type, null, message), innerException)
        {
            Type = type;
        }
        
        public MediaTypeNotSupportedException(Type? type, MediaTypeHeaderValue? media, String? message, Exception? innerException)
            : base(Format(type, media, message), innerException)
        {
            Type = type;
            MediaType = media;
        }
        
        protected MediaTypeNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Type = info.GetValueOrDefault<Type>(nameof(Type));
            MediaType = info.GetValueOrDefault<MediaTypeHeaderValue>(nameof(MediaType));
        }
        
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Type), Type);
            info.AddValue(nameof(MediaType), MediaType);
        }
        
        private static String Format(Type? type, MediaTypeHeaderValue? media, String? message)
        {
            if (message is not null)
            {
                return message;
            }
            
            return media?.MediaType?.ToLowerInvariant() switch
            {
                { Length: > 0 } value when type is not null => MediaTypeMessage.Format(value, type),
                { Length: > 0 } value => MediaMessage.Format(value),
                _ when type is not null => TypeMessage.Format(type),
                _ => Message
            };
        }
    }
}