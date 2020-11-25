// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if !TOAST
using System;

namespace NetExtender.Notifications.Toasts.Mock
{
    public static class ToastNotificationManager
    {
        public static ToastNotificationHistory History { get; } = new ToastNotificationHistory();

        public static ToastNotifier CreateToastNotifier(String aumid)
        {
            return new ToastNotifier();
        }
    }
}

#endif