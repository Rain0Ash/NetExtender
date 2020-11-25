// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if !TOAST
using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

#pragma warning disable 67

namespace NetExtender.Notifications.Toasts.Mock
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public class ToastNotification
    {
        public event TypedEventHandler<ToastNotification, Object> Activated;
        public event TypedEventHandler<ToastNotification, ToastDismissedEventArgs> Dismissed;
        public event TypedEventHandler<ToastNotification, ToastFailedEventArgs> Failed;
        
        public NotificationData Data { get; set; }
        public String Group { get; set; }
        public ToastNotificationPriority Priority { get; set; }
        public String Tag { get; set; }
        public DateTimeOffset? ExpirationTime { get; set; }
        public NotificationMirroring NotificationMirroring { get; set; }
        public String RemoteId { get; set; }
        public Boolean SuppressPopup { get; set; }
        public Boolean ExpiresOnReboot { get; set; }
        
        public ToastNotification(XmlDocument document)
        {
        }
    }
}

#endif