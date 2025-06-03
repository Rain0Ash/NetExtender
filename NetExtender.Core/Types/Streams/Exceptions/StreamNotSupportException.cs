using System;
using System.IO;
using System.Runtime.Serialization;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Streams
{
    // TODO: Replace stream exceptions
    [Serializable]
    public abstract class StreamNotSupportException : NotSupportedException
    {
        private new const String Message = "Stream not support operation.";
        private const String FormatMessage = "Stream '{0}' not support operation.";
        
        protected StreamNotSupportException()
            : base(Message)
        {
        }

        protected StreamNotSupportException(String? message)
            : base(message ?? Message)
        {
        }

        protected StreamNotSupportException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

        protected StreamNotSupportException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public abstract class StreamArgumentNotSupportException : ArgumentException
    {
        private new const String Message = "Stream not support operation.";
        private const String FormatMessage = "Stream '{0}' not support operation.";
        
        protected StreamArgumentNotSupportException()
            : base(Message)
        {
        }

        protected StreamArgumentNotSupportException(String? message)
            : base(message ?? Message)
        {
        }

        protected StreamArgumentNotSupportException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

        protected StreamArgumentNotSupportException(String? message, String? parameter)
            : base(message ?? Message, parameter)
        {
        }

        protected StreamArgumentNotSupportException(String? message, String? parameter, Exception? exception)
            : base(message ?? Message, parameter, exception)
        {
        }

        protected StreamArgumentNotSupportException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public class StreamNotSupportReadException : StreamNotSupportException
    {
        private new const String Message = "Stream not support reading.";
        private const String FormatMessage = "Stream '{0}' not support reading.";

        public StreamNotSupportReadException()
            : base(Message)
        {
        }

        public StreamNotSupportReadException(String? message)
            : base(message ?? Message)
        {
        }

        public StreamNotSupportReadException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

        public StreamNotSupportReadException(Stream stream)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)))
        {
        }

        public StreamNotSupportReadException(Stream stream, Exception? exception) 
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)), exception)
        {
        }

        protected StreamNotSupportReadException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static String Format(Stream? stream)
        {
            return stream switch
            {
                FileStream { Name: var name } when !String.IsNullOrEmpty(name) => FormatMessage.Format(name),
                Microsoft.IO.RecyclableMemoryStream memory => FormatMessage.Format(RecyclableMemoryStream.Handler.GetName(memory)),
                _ => Message
            };
        }
    }

    [Serializable]
    public class StreamArgumentNotSupportReadException : StreamArgumentNotSupportException
    {
        private new const String Message = "Stream not support reading.";
        private const String FormatMessage = "Stream '{0}' not support reading.";

        public StreamArgumentNotSupportReadException()
            : base(Message)
        {
        }

        public StreamArgumentNotSupportReadException(String? message)
            : base(message ?? Message)
        {
        }

        public StreamArgumentNotSupportReadException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

        public StreamArgumentNotSupportReadException(String? message, String? parameter)
            : base(message ?? Message, parameter)
        {
        }

        public StreamArgumentNotSupportReadException(String? message, String? parameter, Exception? exception)
            : base(message ?? Message, parameter, exception)
        {
        }

        public StreamArgumentNotSupportReadException(Stream stream)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)))
        {
        }

        public StreamArgumentNotSupportReadException(Stream stream, String? parameter)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)), parameter)
        {
        }

        public StreamArgumentNotSupportReadException(Stream stream, Exception? exception)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)), exception)
        {
        }

        public StreamArgumentNotSupportReadException(Stream stream, String? parameter, Exception? exception)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)), parameter, exception)
        {
        }

        protected StreamArgumentNotSupportReadException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static String Format(Stream? stream)
        {
            return stream switch
            {
                FileStream { Name: var name } when !String.IsNullOrEmpty(name) => FormatMessage.Format(name),
                Microsoft.IO.RecyclableMemoryStream memory => FormatMessage.Format(RecyclableMemoryStream.Handler.GetName(memory)),
                _ => Message
            };
        }
    }

    [Serializable]
    public class StreamNotSupportSeekException : StreamNotSupportException
    {
        private new const String Message = "Stream not support seeking.";
        private const String FormatMessage = "Stream '{0}' not support seeking.";

        public StreamNotSupportSeekException()
            : base(Message)
        {
        }

        public StreamNotSupportSeekException(String? message)
            : base(message ?? Message)
        {
        }

        public StreamNotSupportSeekException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

        public StreamNotSupportSeekException(Stream stream)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)))
        {
        }

        public StreamNotSupportSeekException(Stream stream, Exception? exception) 
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)), exception)
        {
        }

        protected StreamNotSupportSeekException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static String Format(Stream? stream)
        {
            return stream switch
            {
                FileStream { Name: var name } when !String.IsNullOrEmpty(name) => FormatMessage.Format(name),
                Microsoft.IO.RecyclableMemoryStream memory => FormatMessage.Format(RecyclableMemoryStream.Handler.GetName(memory)),
                _ => Message
            };
        }
    }

    [Serializable]
    public class StreamArgumentNotSupportSeekException : StreamArgumentNotSupportException
    {
        private new const String Message = "Stream not support seeking.";
        private const String FormatMessage = "Stream '{0}' not support seeking.";
        
        public StreamArgumentNotSupportSeekException()
            : base(Message)
        {
        }

        public StreamArgumentNotSupportSeekException(String? message)
            : base(message ?? Message)
        {
        }

        public StreamArgumentNotSupportSeekException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

        public StreamArgumentNotSupportSeekException(String? message, String? parameter)
            : base(message ?? Message, parameter)
        {
        }

        public StreamArgumentNotSupportSeekException(String? message, String? parameter, Exception? exception)
            : base(message ?? Message, parameter, exception)
        {
        }

        public StreamArgumentNotSupportSeekException(Stream stream)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)))
        {
        }

        public StreamArgumentNotSupportSeekException(Stream stream, String? parameter)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)), parameter)
        {
        }

        public StreamArgumentNotSupportSeekException(Stream stream, Exception? exception)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)), exception)
        {
        }

        public StreamArgumentNotSupportSeekException(Stream stream, String? parameter, Exception? exception)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)), parameter, exception)
        {
        }

        protected StreamArgumentNotSupportSeekException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static String Format(Stream? stream)
        {
            return stream switch
            {
                FileStream { Name: var name } when !String.IsNullOrEmpty(name) => FormatMessage.Format(name),
                Microsoft.IO.RecyclableMemoryStream memory => FormatMessage.Format(RecyclableMemoryStream.Handler.GetName(memory)),
                _ => Message
            };
        }
    }

    [Serializable]
    public class StreamNotSupportWriteException : StreamNotSupportException
    {
        private new const String Message = "Stream not support writing.";
        private const String FormatMessage = "Stream '{0}' not support writing.";

        public StreamNotSupportWriteException()
            : base(Message)
        {
        }

        public StreamNotSupportWriteException(String? message)
            : base(message ?? Message)
        {
        }

        public StreamNotSupportWriteException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

        public StreamNotSupportWriteException(Stream stream)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)))
        {
        }

        public StreamNotSupportWriteException(Stream stream, Exception? exception) 
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)), exception)
        {
        }

        protected StreamNotSupportWriteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static String Format(Stream? stream)
        {
            return stream switch
            {
                FileStream { Name: var name } when !String.IsNullOrEmpty(name) => FormatMessage.Format(name),
                Microsoft.IO.RecyclableMemoryStream memory => FormatMessage.Format(RecyclableMemoryStream.Handler.GetName(memory)),
                _ => Message
            };
        }
    }

    [Serializable]
    public class StreamArgumentNotSupportWriteException : StreamArgumentNotSupportException
    {
        private new const String Message = "Stream not support writing.";
        private const String FormatMessage = "Stream '{0}' not support writing.";
        
        public StreamArgumentNotSupportWriteException()
            : base(Message)
        {
        }

        public StreamArgumentNotSupportWriteException(String? message)
            : base(message ?? Message)
        {
        }

        public StreamArgumentNotSupportWriteException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

        public StreamArgumentNotSupportWriteException(String? message, String? parameter)
            : base(message ?? Message, parameter)
        {
        }

        public StreamArgumentNotSupportWriteException(String? message, String? parameter, Exception? exception)
            : base(message ?? Message, parameter, exception)
        {
        }

        public StreamArgumentNotSupportWriteException(Stream stream)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)))
        {
        }

        public StreamArgumentNotSupportWriteException(Stream stream, String? parameter)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)), parameter)
        {
        }

        public StreamArgumentNotSupportWriteException(Stream stream, Exception? exception)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)), exception)
        {
        }

        public StreamArgumentNotSupportWriteException(Stream stream, String? parameter, Exception? exception)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)), parameter, exception)
        {
        }

        protected StreamArgumentNotSupportWriteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static String Format(Stream? stream)
        {
            return stream switch
            {
                FileStream { Name: var name } when !String.IsNullOrEmpty(name) => FormatMessage.Format(name),
                Microsoft.IO.RecyclableMemoryStream memory => FormatMessage.Format(RecyclableMemoryStream.Handler.GetName(memory)),
                _ => Message
            };
        }
    }

    [Serializable]
    public class StreamNotSupportTimeoutException : StreamNotSupportException
    {
        private new const String Message = "Stream not support timeout.";
        private const String FormatMessage = "Stream '{0}' not support timeout.";

        public StreamNotSupportTimeoutException()
            : base(Message)
        {
        }

        public StreamNotSupportTimeoutException(String? message)
            : base(message ?? Message)
        {
        }

        public StreamNotSupportTimeoutException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

        public StreamNotSupportTimeoutException(Stream stream)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)))
        {
        }

        public StreamNotSupportTimeoutException(Stream stream, Exception? exception) 
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)), exception)
        {
        }

        protected StreamNotSupportTimeoutException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static String Format(Stream? stream)
        {
            return stream switch
            {
                FileStream { Name: var name } when !String.IsNullOrEmpty(name) => FormatMessage.Format(name),
                Microsoft.IO.RecyclableMemoryStream memory => FormatMessage.Format(RecyclableMemoryStream.Handler.GetName(memory)),
                _ => Message
            };
        }
    }

    [Serializable]
    public class StreamArgumentNotSupportTimeoutException : StreamArgumentNotSupportException
    {
        private new const String Message = "Stream not support timeout.";
        private const String FormatMessage = "Stream '{0}' not support timeout.";
        
        public StreamArgumentNotSupportTimeoutException()
            : base(Message)
        {
        }

        public StreamArgumentNotSupportTimeoutException(String? message)
            : base(message ?? Message)
        {
        }

        public StreamArgumentNotSupportTimeoutException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

        public StreamArgumentNotSupportTimeoutException(String? message, String? parameter)
            : base(message ?? Message, parameter)
        {
        }

        public StreamArgumentNotSupportTimeoutException(String? message, String? parameter, Exception? exception)
            : base(message ?? Message, parameter, exception)
        {
        }

        public StreamArgumentNotSupportTimeoutException(Stream stream)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)))
        {
        }

        public StreamArgumentNotSupportTimeoutException(Stream stream, String? parameter)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)), parameter)
        {
        }

        public StreamArgumentNotSupportTimeoutException(Stream stream, Exception? exception)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)), exception)
        {
        }

        public StreamArgumentNotSupportTimeoutException(Stream stream, String? parameter, Exception? exception)
            : base(stream is not null ? Format(stream) : throw new ArgumentNullException(nameof(stream)), parameter, exception)
        {
        }

        protected StreamArgumentNotSupportTimeoutException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static String Format(Stream? stream)
        {
            return stream switch
            {
                FileStream { Name: var name } when !String.IsNullOrEmpty(name) => FormatMessage.Format(name),
                Microsoft.IO.RecyclableMemoryStream memory => FormatMessage.Format(RecyclableMemoryStream.Handler.GetName(memory)),
                _ => Message
            };
        }
    }
}