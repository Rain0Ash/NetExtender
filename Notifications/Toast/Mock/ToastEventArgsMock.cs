// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if !TOAST
using System;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Notifications.Toasts.Mock
{
    [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    public class ToastActivatedEventArgs : EventArgs
    {
        public String Arguments { get; }
        public ValueSet UserInput { get; }
    }
    
    [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    public class ToastDismissedEventArgs : EventArgs
    {
        public ToastDismissalReason Reason { get; }
    }
    
    [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    public class ToastFailedEventArgs : EventArgs
    {
        public Exception ErrorCode { get; }
    }
}

#endif