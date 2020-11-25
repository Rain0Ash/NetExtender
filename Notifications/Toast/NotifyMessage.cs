// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using NetExtender.Notifications.Toasts.Mock;

#if TOAST
using Windows.Foundation.Collections;
using Microsoft.Toolkit.Uwp.Notifications;
#else

#endif

namespace NetExtender.Notifications.Toasts
{
    public class NotifyActivationMessage : NotifyMessage
    {
        public String Model { get; }

        public NotifyActivationMessage(Exception exception)
            : base(exception)
        {
        }

        public NotifyActivationMessage(NotifyStatus status, Exception exception)
            : base(status, exception)
        {
        }

        public NotifyActivationMessage(String arguments, NotificationUserInput input, String model)
            : this(arguments is null || input is null || model is null ? NotifyStatus.Unknown : NotifyStatus.Activated, arguments, input, model)
        {
        }

        public NotifyActivationMessage(NotifyStatus status, String arguments = null, NotificationUserInput input = null, String model = null, Exception exception = null)
            : this(status, arguments, input?.ToImmutableDictionary(pair => pair.Key, pair => (Object) pair.Value), model, exception)
        {
        }
        
        public NotifyActivationMessage(NotifyStatus status, String arguments = null, IImmutableDictionary<String, Object> input = null, String model = null, Exception exception = null)
            : base(status, arguments, input, exception)
        {
            Model = model;
        }
    }
    
    public class NotifyMessage
    {
        public NotifyStatus Status { get; }
        public String Arguments { get; }
        
        public IImmutableDictionary<String, Object> Input { get; }
        
        public Exception Exception { get; }

        public NotifyMessage(Exception exception)
            : this(NotifyStatus.Failed, exception)
        {
        }
        
        public NotifyMessage(NotifyStatus status, Exception exception)
            : this(status, null, (ImmutableDictionary<String, Object>) null, exception)
        {
        }

        public NotifyMessage(String arguments, ValueSet input)
            : this(arguments, input?.ToImmutableDictionary())
        {
        }
        
        public NotifyMessage(String arguments, IImmutableDictionary<String, Object> input)
            : this(arguments is null || input is null ? NotifyStatus.Unknown : NotifyStatus.Activated, arguments, input)
        {
        }

        public NotifyMessage(NotifyStatus status, String arguments, ValueSet input, Exception exception = null)
            : this(status, arguments, input?.ToImmutableDictionary(), exception)
        {
        }

        public NotifyMessage(NotifyStatus status, String arguments = null, IImmutableDictionary<String, Object> input = null, Exception exception = null)
        {
            Status = status;

            Arguments = arguments;
            Input = input;
            
            Exception = exception;
        }
    }
}