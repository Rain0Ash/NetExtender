// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if TOAST
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
#if TOAST
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.UI.Notifications;
#else
using System.Xml;
using NetExtender.Notifications.Toasts.Mock;
#endif
using NetExtender.Notifications.Toasts;
using NetExtender.Utils.Types;
using Microsoft.Toolkit.Uwp.Notifications;

namespace NetExtender.Utils.Application.Notifications
{
    public static class NotificationsUtils
    {
        public static XmlDocument GetToastXml(this ToastContentBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

#if TOAST
            return builder.GetToastContent().GetXml();
#else
            return new XmlDocument();
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification GetNotification(this ToastContentBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return new ToastNotification(builder.GetToastXml()).SetToastHideOnDismiss();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetData(this ToastContentBuilder builder, NotificationData data)
        {
            return SetData(builder.GetNotification(), data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetData(this ToastNotification notification, NotificationData data)
        {
            notification.Data = data;
            return notification;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetGroup(this ToastContentBuilder builder, String group)
        {
            return SetGroup(builder.GetNotification(), group);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetGroup(this ToastNotification notification, String group)
        {
            notification.Group = group;
            return notification;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetPriority(this ToastContentBuilder builder, ToastNotificationPriority priority)
        {
            return SetPriority(builder.GetNotification(), priority);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetPriority(this ToastNotification notification, ToastNotificationPriority priority)
        {
            notification.Priority = priority;
            return notification;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetTag(this ToastContentBuilder builder, String tag)
        {
            return SetTag(builder.GetNotification(), tag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetTag(this ToastNotification notification, String tag)
        {
            notification.Tag = tag;
            return notification;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetExpirationTime(this ToastContentBuilder builder, DateTimeOffset? offset)
        {
            return SetExpirationTime(builder.GetNotification(), offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetExpirationTime(this ToastNotification notification, DateTimeOffset? offset)
        {
            notification.ExpirationTime = offset;
            return notification;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetNotificationMirroring(this ToastContentBuilder builder, NotificationMirroring mirroring)
        {
            return SetNotificationMirroring(builder.GetNotification(), mirroring);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetNotificationMirroring(this ToastNotification notification, NotificationMirroring mirroring)
        {
            notification.NotificationMirroring = mirroring;
            return notification;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetRemoteId(this ToastContentBuilder builder, String id)
        {
            return SetRemoteId(builder.GetNotification(), id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetRemoteId(this ToastNotification notification, String id)
        {
            notification.RemoteId = id;
            return notification;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetSuppressPopup(this ToastContentBuilder builder, Boolean suppress)
        {
            return SetSuppressPopup(builder.GetNotification(), suppress);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetSuppressPopup(this ToastNotification notification, Boolean suppress)
        {
            notification.SuppressPopup = suppress;
            return notification;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetExpiresOnReboot(this ToastContentBuilder builder, Boolean expires)
        {
            return SetExpiresOnReboot(builder.GetNotification(), expires);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetExpiresOnReboot(this ToastNotification notification, Boolean expires)
        {
            notification.ExpiresOnReboot = expires;
            return notification;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetActivatedHandler(this ToastContentBuilder builder, TypedEventHandler<ToastNotification, Object> activated)
        {
            return SetActivatedHandler(builder.GetNotification(), activated);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetActivatedHandler(this ToastNotification notification, TypedEventHandler<ToastNotification, Object> activated)
        {
            notification.Activated += activated;
            return notification;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetDismissedHandler(this ToastContentBuilder builder, TypedEventHandler<ToastNotification, ToastDismissedEventArgs> dismissed)
        {
            return SetDismissedHandler(builder.GetNotification(), dismissed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetDismissedHandler(this ToastNotification notification, TypedEventHandler<ToastNotification, ToastDismissedEventArgs> dismissed)
        {
            notification.Dismissed += dismissed;
            return notification;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetFailedHandler(this ToastContentBuilder builder, TypedEventHandler<ToastNotification, ToastFailedEventArgs> failed)
        {
            return SetFailedHandler(builder.GetNotification(), failed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetFailedHandler(this ToastNotification notification, TypedEventHandler<ToastNotification, ToastFailedEventArgs> failed)
        {
            notification.Failed += failed;
            return notification;
        }

        private static ISet<ToastNotification> Hideable { get; } = new HashSet<ToastNotification>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ToastNotification SetToastHideOnDismiss(this ToastContentBuilder builder, Boolean hide = true)
        {
            return SetToastHideOnDismiss(builder.GetNotification(), hide);
        }

        public static ToastNotification SetToastHideOnDismiss(this ToastNotification notification, Boolean hide = true)
        {
            lock (Hideable)
            {
                Boolean contains = Hideable.Contains(notification);
                if (hide && !contains)
                {
                    notification.Dismissed += ToastHideOnDismiss;
                    Hideable.Add(notification);
                }
                else if (!hide && contains)
                {
                    notification.Dismissed -= ToastHideOnDismiss;
                    Hideable.Remove(notification);
                }
            }

            return notification;
        }

        public static void ToastHideOnDismiss(ToastNotification sender, ToastDismissedEventArgs args)
        {
            ToastHideOnDismiss(AppNotification.DefaultNotifier, sender, args);
        }

        public static void ToastHideOnDismiss(ToastNotifier notifier, ToastNotification sender, ToastDismissedEventArgs args)
        {
            if (args.Reason == ToastDismissalReason.UserCanceled || args.Reason == ToastDismissalReason.TimedOut)
            {
                notifier.Hide(sender);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<NotifyMessage> ShowAsync(this ToastContentBuilder builder, CancellationToken token)
        {
            return ShowAsync(builder.GetNotification(), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<NotifyMessage> ShowAsync(this ToastNotification notification, CancellationToken token)
        {
            return ShowAsync(AppNotification.DefaultNotifier, notification, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<NotifyMessage> ShowAsync(this ToastNotifier notifier, ToastContentBuilder builder, CancellationToken token)
        {
            return ShowAsync(notifier, builder.GetNotification(), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<NotifyMessage> ShowAsync(this ToastNotifier notifier, ToastNotification notification, CancellationToken token)
        {
            return ShowAsync(notifier, notification, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<NotifyMessage> ShowAsync(this ToastContentBuilder builder, TimeSpan timeout)
        {
            return ShowAsync(builder.GetNotification(), timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<NotifyMessage> ShowAsync(this ToastNotification notification, TimeSpan timeout)
        {
            return ShowAsync(AppNotification.DefaultNotifier, notification, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<NotifyMessage> ShowAsync(this ToastNotifier notifier, ToastContentBuilder builder, TimeSpan timeout)
        {
            return ShowAsync(notifier, builder.GetNotification(), timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<NotifyMessage> ShowAsync(this ToastNotifier notifier, ToastNotification notification, TimeSpan timeout)
        {
            return ShowAsync(notifier, notification, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<NotifyMessage> ShowAsync(this ToastContentBuilder builder, TimeSpan timeout, CancellationToken token)
        {
            return ShowAsync(builder.GetNotification(), timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<NotifyMessage> ShowAsync(this ToastNotification notification, TimeSpan timeout, CancellationToken token)
        {
            return ShowAsync(AppNotification.DefaultNotifier, notification, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<NotifyMessage> ShowAsync(this ToastNotifier notifier, ToastContentBuilder builder, TimeSpan timeout, CancellationToken token)
        {
            return ShowAsync(notifier, builder.GetNotification(), timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<NotifyMessage> ShowAsync(this ToastNotifier notifier, ToastNotification notification, TimeSpan timeout, CancellationToken token)
        {
            return ShowAsync(notifier, notification, new TimeSpan?(timeout), token);
        }

        public static async Task<NotifyMessage> ShowAsync(this ToastNotifier notifier, ToastNotification notification, TimeSpan? timeout, CancellationToken token)
        {
#if TOAST
            if (notifier is null)
            {
                throw new ArgumentNullException(nameof(notifier));
            }

            if (notification is null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            if (token.IsCancellationRequested)
            {
                return new NotifyMessage(NotifyStatus.TokenCanceled);
            }

            using CancellationTokenSource source = token.CreateLinkedSource(timeout);

            void Subscribe(ToastNotification sender)
            {
                sender.Activated += OnActivated;
                sender.Dismissed += OnDismissed;
                sender.Failed += OnFailed;
            }

            void Unsubscribe(ToastNotification sender)
            {
                sender.Activated -= OnActivated;
                sender.Dismissed -= OnDismissed;
                sender.Failed -= OnFailed;
            }

            NotifyMessage message = null;

            void SetMessage(NotifyMessage msg)
            {
                message = msg;
            }

            void OnActivated(ToastNotification sender, Object args)
            {
                OnActivatedArgs(sender, args as ToastActivatedEventArgs);
            }

            void OnActivatedArgs(ToastNotification sender, ToastActivatedEventArgs args)
            {
                Unsubscribe(sender);

                SetMessage(new NotifyMessage(NotifyStatus.Activated, args?.Arguments, args?.UserInput));

                // ReSharper disable once AccessToDisposedClosure
                source.Cancel();
            }

            void OnDismissed(ToastNotification sender, ToastDismissedEventArgs args)
            {
                Unsubscribe(sender);

                SetMessage(args.Reason switch
                {
                    ToastDismissalReason.UserCanceled => new NotifyMessage(NotifyStatus.UserCanceled),
                    ToastDismissalReason.ApplicationHidden => new NotifyMessage(NotifyStatus.ApplicationHidden),
                    ToastDismissalReason.TimedOut => new NotifyMessage(NotifyStatus.TimedOut),
                    _ => throw new NotSupportedException()
                });

                // ReSharper disable once AccessToDisposedClosure
                source.Cancel();
            }

            void OnFailed(ToastNotification sender, ToastFailedEventArgs args)
            {
                Unsubscribe(sender);

                SetMessage(new NotifyMessage(NotifyStatus.Failed, args.ErrorCode));

                // ReSharper disable once AccessToDisposedClosure
                source.Cancel();
            }

            void Show()
            {
                notifier.Show(notification);
            }

            void Hide()
            {
                message ??= new NotifyMessage(token.IsCancellationRequested ? NotifyStatus.TokenCanceled : timeout.HasValue ? NotifyStatus.TimedOut : NotifyStatus.ApplicationHidden);

                notifier.Hide(notification);
            }

            Subscribe(notification);

            CancellationToken stoken = source.Token;

            await using CancellationTokenRegistration _ = stoken.Register(Hide);
            await Task.Run(Show, stoken).ConfigureAwait(false);
            await stoken;

            return message;
#else
            return new NotifyMessage(null);
#endif
        }
    }
}
#endif