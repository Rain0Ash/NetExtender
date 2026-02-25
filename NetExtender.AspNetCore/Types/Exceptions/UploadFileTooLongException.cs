// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.Serialization;
using NetExtender.Exceptions;

namespace NetExtender.AspNetCore.Identity
{
    [Serializable]
    public class UploadFileTooLongException : BusinessCustomException
    {
        public new static HttpStatusCode Status { get; set; } = HttpStatusCode.RequestEntityTooLarge;
        public new static String? Message { get; set; } = "Upload file too long.";
        public new static String? Name { get; set; } = "File.Upload.TooLong";

        public override String? Identity
        {
            get
            {
                return base.Name ?? Name;
            }
            init
            {
                base.Name = value;
            }
        }

        public String? FileName
        {
            get
            {
                return Get<String?>(nameof(FileName)).Internal;
            }
            init
            {
                Set(nameof(FileName), value);
            }
        }

        public Int64? FileSize
        {
            get
            {
                return Get<Int64?>(nameof(FileSize)).Internal;
            }
            init
            {
                Set(nameof(FileSize), value);
            }
        }

        public Int64? MaximumFileSize
        {
            get
            {
                return Get<Int64?>(nameof(MaximumFileSize)).Internal;
            }
            init
            {
                Set(nameof(MaximumFileSize), value);
            }
        }

        public UploadFileTooLongException()
            : base(Message, Status)
        {
        }

        public UploadFileTooLongException(String? message)
            : base(message ?? Message, Status)
        {
        }

        public UploadFileTooLongException(String? message, Exception? exception)
            : base(message ?? Message, Status, exception)
        {
        }

        public UploadFileTooLongException(String? message, params BusinessException?[]? reason)
            : base(message ?? Message, Status, reason)
        {
        }

        public UploadFileTooLongException(String? message, Exception? exception, params BusinessException?[]? reason)
            : base(message ?? Message, Status, exception, reason)
        {
        }

        public UploadFileTooLongException(String? message, BusinessException? exception, params BusinessException?[]? reason)
            : base(message ?? Message, Status, exception, reason)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected UploadFileTooLongException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}