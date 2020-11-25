// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if TOAST_OLD
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
#if TOAST
using Windows.UI.Notifications;
#else
using NetExtender.Notifications.Toasts.Mock;
#endif
using NetExtender.Notifications.Interfaces;
using NetExtender.Utils.Application.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;

namespace NetExtender.Notifications.Toasts
{
#pragma warning disable 618
    [ComSourceInterfaces(typeof(INotificationActivationCallback))]
#pragma warning restore 618
    public abstract class NotifyActivator : NotificationActivator, INotifyActivator
    {
        public static implicit operator ToastNotifier(NotifyActivator activator)
        {
            return activator.Notifier;
        }

        public event TypeHandler<NotifyMessage> Notify;

        public ToastNotifier Notifier { get; }

        public NotificationSetting Setting
        {
            get
            {
                return Notifier.Setting;
            }
        }

        protected NotifyActivator()
            : this(AppNotification.DefaultNotifier)
        {
        }

        protected NotifyActivator(ToastNotifier notifier)
        {
            Notifier = notifier;
        }
        
        public void Show(ToastNotification notification)
        {
            Notifier.Show(notification);
        }

        public Task<NotifyMessage> ShowAsync(ToastNotification notification, CancellationToken token)
        {
            return Notifier.ShowAsync(notification, token);
        }

        public void Hide(ToastNotification notification)
        {
            Notifier.Hide(notification);
        }

        public void AddToSchedule(ScheduledToastNotification toast)
        {
            Notifier.AddToSchedule(toast);
        }

        public void RemoveFromSchedule(ScheduledToastNotification toast)
        {
            Notifier.RemoveFromSchedule(toast);
        }

        public IReadOnlyList<ScheduledToastNotification> GetScheduledToastNotifications()
        {
            return Notifier.GetScheduledToastNotifications();
        }

        public sealed override void OnActivated(String arguments, NotificationUserInput input, String model)
        {
            Notify?.Invoke(new NotifyActivationMessage(arguments, input, model));
        }
    }
}

#endif