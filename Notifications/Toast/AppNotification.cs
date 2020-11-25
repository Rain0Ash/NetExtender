// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Notifications.Toasts.Mock;

#if TOAST
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;
#else

#endif

#if TOAST_OLD
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using NetExtender.Apps.Domains;
#endif

namespace NetExtender.Notifications.Toasts
{
    public static class AppNotification
    {
        public static ToastNotifier DefaultNotifier { get; private set; }

        public static Boolean IsInitialized
        {
            get
            {
                return DefaultNotifier is not null;
            }
        }

#if !TOAST_OLD
        public static void Initialize()
        {
            DefaultNotifier ??= ToastNotificationManagerCompat.CreateToastNotifier();
        }
#else
        public static void Initialize<T>(String aumid = null) where T : NotificationActivator, new()
        {
            DefaultNotifier ??= AppNotification<T>.Initialize(aumid);
        }
#endif

        public static ToastNotifier AsDefaultNotifier(this ToastNotifier notifier)
        {
            DefaultNotifier = notifier;
            return notifier;
        }

        public static void Clear()
        {
            ToastNotificationManagerCompat.History.Clear();
        }
    }

#if TOAST_OLD
    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public static class AppNotification<T> where T : NotificationActivator, new()
    {
        private static Lazy<ToastNotifier> LazyNotifier { get; set; }
        
        public static Boolean IsLazyInitialized
        {
            get
            {
                return LazyNotifier is not null;
            }
        }

        public static ToastNotifier Notifier
        {
            get
            {
                return LazyNotifier?.Value;
            }
        }

        public static Boolean IsInitialized
        {
            get
            {
                return Notifier is not null;
            }
        }
        
        public static Guid Guid { get; private set; }

        private static ToastNotifier NotifierFactory(String aumid)
        {
            if (String.IsNullOrEmpty(aumid))
            {
                aumid = Domain.CurrentAppNameOrPath;
            }
            
            GuidAttribute attribute = typeof(T).GetCustomAttribute<GuidAttribute>();

            if (attribute is null)
            {
                throw new ArgumentException($"Type {typeof(T).FullName} don't include {typeof(GuidAttribute).FullName} attribute.");
            }

            if (String.IsNullOrEmpty(attribute.Value))
            {
                throw new ArgumentException($"{nameof(GuidAttribute)} contains null or empty GUID value.");
            }
            
            try
            {
                Guid = Guid.Parse(attribute.Value);
            }
            catch (FormatException)
            {
                throw new ArgumentException($"Invalid attribute GUID format: {attribute.Value}");
            }

            DesktopNotificationManagerCompat.RegisterAumidAndComServer<T>(aumid);
            DesktopNotificationManagerCompat.RegisterActivator<T>();
            
            return ToastNotificationManager.CreateToastNotifier(aumid);
        }
        
        public static ToastNotifier Initialize(String aumid = null)
        {
            if (IsLazyInitialized)
            {
                return Notifier;
            }

            Object sync = new Object();

            lock (sync)
            {
                LazyNotifier = new Lazy<ToastNotifier>(() => NotifierFactory(aumid));
            }

            return Notifier;
        }
    }
#endif
}