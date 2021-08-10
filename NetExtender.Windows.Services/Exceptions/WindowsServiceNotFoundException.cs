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
        
        public WindowsServiceNotFoundException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public WindowsServiceNotFoundException(String? message, String? serviceName, Exception? innerException)
            : base(message, innerException)
        {
            ServiceName = serviceName;
        }
        
        protected WindowsServiceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ServiceName = info.GetString("WindowsServiceNotFound_ServiceName");
        }
        
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("WindowsServiceNotFound_ServiceName", ServiceName, typeof(String));
        }
    }
}