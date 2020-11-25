// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
#if TOAST
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;
#else
#endif
using NetExtender.Notifications.Toasts;
using NetExtender.Notifications.Toasts.Mock;

namespace NetExtender.Notifications.Interfaces
{
    [ComImport]
    [Guid("53E31837-6600-4A81-9395-75CFFE746F94")]
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface INotifyActivator : NotificationActivator.INotificationActivationCallback
    {
        public event TypeHandler<NotifyMessage> Notify;
        public ToastNotifier Notifier { get; }

        public NotificationSetting Setting { get; }

        public void Show([In] ToastNotification notification);

        public Task<NotifyMessage> ShowAsync([In] ToastNotification notification, CancellationToken token);

        public void Hide([In] ToastNotification notification);

        public void AddToSchedule([In] ScheduledToastNotification toast);

        public void RemoveFromSchedule([In] ScheduledToastNotification toast);

        public IReadOnlyList<ScheduledToastNotification> GetScheduledToastNotifications();
    }
}