// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Disposable;
using ReactiveUI;

namespace NetExtender.ReactiveUI
{
    public class ReactiveDisposeObject<T> : ReactiveDisposeObject where T : ReactiveDisposeObject<T>
    {
        [return: NotNullIfNotNull("object")]
        public static T? operator +(ReactiveDisposeObject<T>? @object, IDisposable? subscription)
        {
            if (@object is not null && subscription is not null)
            {
                @object.Subscriptions.Add(subscription);
            }
            
            return (T?) @object;
        }
    }
    
    public class ReactiveDisposeObject : ReactiveObject, IDisposable
    {
        [return: NotNullIfNotNull("object")]
        public static ReactiveDisposeObject? operator +(ReactiveDisposeObject? @object, IDisposable? subscription)
        {
            if (@object is not null && subscription is not null)
            {
                @object.Subscriptions.Add(subscription);
            }
            
            return @object;
        }
        
        private DisposeCollection? _subscriptions;
        private protected DisposeCollection Subscriptions
        {
            get
            {
                return _subscriptions ??= new DisposeCollection();
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            _subscriptions?.Dispose();
        }

        ~ReactiveDisposeObject()
        {
            Dispose(false);
        }
    }
}