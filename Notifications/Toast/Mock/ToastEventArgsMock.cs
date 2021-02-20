// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if !TOAST
using System;

namespace NetExtender.Notifications.Toasts.Mock
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    public class ToastActivatedEventArgs : EventArgs
    {
        public String Arguments { get; }
        public ValueSet UserInput { get; }
    }
    
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    public class ToastDismissedEventArgs : EventArgs
    {
        public ToastDismissalReason Reason { get; }
    }
    
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    public class ToastFailedEventArgs : EventArgs
    {
        public Exception ErrorCode { get; }
    }
}

#endif