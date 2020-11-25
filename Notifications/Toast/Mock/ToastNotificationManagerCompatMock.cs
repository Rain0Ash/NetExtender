// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if !TOAST
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Notifications.Toasts.Mock
{
    [SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
    public static class ToastNotificationManagerCompat
    {
        public static ToastNotificationHistory History { get; } = new ToastNotificationHistory();
        
        private static ToastNotifier Notifier { get; } = new ToastNotifier();
        public static ToastNotifier CreateToastNotifier()
        {
            return Notifier;
        }
    }
}

#endif