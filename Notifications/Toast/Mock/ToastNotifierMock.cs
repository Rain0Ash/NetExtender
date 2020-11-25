// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if !TOAST
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Notifications.Toasts.Mock
{
    
    [SuppressMessage("ReSharper", "CA1822")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public class ToastNotifier
    {
        public NotificationSetting Setting { get; } = new NotificationSetting();

        public void Show(ToastNotification notification)
        {
        }
        
        public void Hide(ToastNotification notification)
        {
        }
        
        public void AddToSchedule(ScheduledToastNotification toast)
        {
        }

        public void RemoveFromSchedule(ScheduledToastNotification toast)
        {
        }

        public IReadOnlyList<ScheduledToastNotification> GetScheduledToastNotifications()
        {
            return ImmutableList<ScheduledToastNotification>.Empty;
        }
    }
}

#endif