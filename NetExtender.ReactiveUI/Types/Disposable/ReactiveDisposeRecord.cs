// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Disposable;
using ReactiveUI;

namespace NetExtender.ReactiveUI
{
    public record ReactiveDisposeRecord<T> : ReactiveDisposeRecord where T : ReactiveDisposeRecord<T>
    {
        [return: NotNullIfNotNull("object")]
        public static T? operator +(ReactiveDisposeRecord<T>? @object, IDisposable? subscription)
        {
            if (@object is not null && subscription is not null)
            {
                @object.Subscriptions.Add(subscription);
            }
            
            return (T?) @object;
        }
    }
    
    public record ReactiveDisposeRecord : ReactiveRecord, IDisposable
    {
        [return: NotNullIfNotNull("record")]
        public static ReactiveDisposeRecord? operator +(ReactiveDisposeRecord? record, IDisposable? subscription)
        {
            if (record is not null && subscription is not null)
            {
                record.Subscriptions.Add(subscription);
            }
            
            return record;
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

        ~ReactiveDisposeRecord()
        {
            Dispose(false);
        }
    }
}