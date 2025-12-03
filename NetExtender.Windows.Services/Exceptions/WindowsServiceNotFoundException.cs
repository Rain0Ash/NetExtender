// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Windows.Services.Exceptions
{
    [Serializable]
    public class WindowsServiceNotFoundException : SystemException
    {
        public const String ServiceNotFoundMessage = "Windows service not found.";

        public String? ServiceName { get; }

        public override String Message
        {
            get
            {
                return ServiceName is null ? ServiceNotFoundMessage : $"Windows service with name '{ServiceName}' not found.";
            }
        }

        public WindowsServiceNotFoundException()
            : base(ServiceNotFoundMessage)
        {
        }

        public WindowsServiceNotFoundException(String? message)
            : base(message)
        {
        }

        public WindowsServiceNotFoundException(String? message, String? serviceName)
            : base(message)
        {
            ServiceName = serviceName;
        }

        public WindowsServiceNotFoundException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public WindowsServiceNotFoundException(String? message, String? serviceName, Exception? exception)
            : base(message, exception)
        {
            ServiceName = serviceName;
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected WindowsServiceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ServiceName = info.GetString(nameof(ServiceName));
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(ServiceName), ServiceName);
        }
    }
}