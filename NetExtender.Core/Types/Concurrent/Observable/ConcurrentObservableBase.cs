// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;
using NetExtender.Types.Concurrent.Observable.Interfaces;
using NetExtender.Types.Disposable;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Delegates;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Concurrent.Observable
{
    [Serializable]
    public abstract class ConcurrentObservableBase<T, TCollection, TSelf> : IConcurrentObservableBase<T>, ISerializable where TCollection : class where TSelf : ConcurrentObservableBase<T, TCollection, TSelf>
    {
        protected delegate FlowResult<TResult> Read<in TArgument, TResult>(TSelf @this, TCollection collection, TArgument argument);

        [return: NotNullIfNotNull("collection")]
        protected delegate TCollection? Write<in TArgument>(TSelf @this, TCollection collection, TArgument argument);

        [return: NotNullIfNotNull("collection")]
        protected delegate TCollection? Write<in TArgument, in TResult>(TSelf @this, TCollection collection, TArgument argument, TResult value);

        protected delegate NotifyCollectionChangedEventArgs? Args<in TArgument>(TSelf @this, TCollection @new, TCollection old, TArgument argument);
        protected delegate NotifyCollectionChangedEventArgs? Args<in TArgument, in TResult>(TSelf @this, TCollection @new, TCollection old, TArgument argument, TResult value);

        protected abstract TCollection Collection { get; set; }
        public abstract ICollection<T> View { get; }
        public abstract TCollection Immutable { get; }
        public abstract Int32 Count { get; }
        public abstract Boolean IsEmpty { get; }

        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        protected TSelf This
        {
            get
            {
                return (TSelf) this;
            }
        }

        public Boolean AllowDirectBindingToView { get; set; } = false;

        private event NotifyCollectionChangedEventHandler? _changed;
        public event NotifyCollectionChangedEventHandler? CollectionChanged
        {
            add
            {
                if (!AllowDirectBindingToView)
                {
                    String? name = value?.Target?.GetType().FullName;
                    if (name == "System.Windows.Data.CollectionView" || name == "System.Windows.Data.ListCollectionView")
                    {
                        throw new ApplicationException($"Collection type '{typeof(T).Name}' don't bind directly to {nameof(ConcurrentObservableCollection<T>)}, instead bind to {nameof(ConcurrentObservableCollection<T>)}.View.");
                    }
                }

                _changed += value;
            }
            remove
            {
                _changed -= value;
            }
        }

        private static readonly ThrottleAction<ConcurrentObservableBase<T, TCollection, TSelf>> view = new ThrottleAction<ConcurrentObservableBase<T, TCollection, TSelf>>(OnViewChanged, TimeSpan.FromMilliseconds(20));
        private readonly ThrottleAction<ConcurrentObservableBase<T, TCollection, TSelf>> _view = view;
        private ThrottleAction<ConcurrentObservableBase<T, TCollection, TSelf>> ViewChanged
        {
            get
            {
                return _view;
            }
            init
            {
                _view = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected ReaderWriterLockSlim? Lock { get; }
        public EventHandler<Exception>? ExceptionHandler { get; set; } = (_, _) => { };

        protected ConcurrentObservableBase(Boolean @lock)
        {
            Lock = @lock ? new ReaderWriterLockSlim() : null;
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [EditorBrowsable(EditorBrowsableState.Never)]
#endif
        protected ConcurrentObservableBase(SerializationInfo info, StreamingContext context)
            : this(info.GetBoolean(nameof(Lock)))
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("Formatter-based serialization is obsolete and should not be used.", DiagnosticId = "SYSLIB0050", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
#endif
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Lock), Lock is not null);
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            try
            {
                _changed?.Invoke(this, args);
            }
            catch (Exception exception) when (ExceptionHandler is { } handler)
            {
                handler.Invoke(this, exception);
            }
        }

        private static void OnViewChanged(ConcurrentObservableBase<T, TCollection, TSelf> collection)
        {
            collection.RaisePropertyChanged(nameof(View), nameof(Count));
        }

        protected void RaisePropertyChanging(String? property)
        {
            PropertyChanging?.Invoke(this, new PropertyChanging(property));
        }

        protected void RaisePropertyChanged(String? property)
        {
            PropertyChanged?.Invoke(this, new PropertyChanged(property));
        }

        public abstract Boolean Contains(T item);
        public abstract Boolean Add(T item);

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public abstract Boolean Remove(T item);

        public void Clear()
        {
            Clear(out _);
        }

        protected abstract void Clear(out Int32 count);

        public void CopyTo(T[] array)
        {
            CopyTo(array, 0);
        }

        public abstract void CopyTo(T[] array, Int32 index);

        public IDisposable FreezeUpdates()
        {
            if (Lock is null)
            {
                return AnonymousDisposable.Null;
            }

            Lock.EnterReadLock();
            return new AnonymousDisposable<ReaderWriterLockSlim>(Lock, static @lock => @lock.ExitReadLock());
        }

        protected abstract IEnumerator<T> Enumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return Enumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static IList Source(IEnumerable<T> source)
        {
            return (source as ICollection)?.AsCOW() ?? source.ToList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Notify<TArgument>(TSelf @this, TCollection @new, TCollection old, Args<TArgument>? notify, TArgument argument)
        {
            if (_changed is null)
            {
                return;
            }

            if (notify?.Invoke(@this, @new, old, argument) is { } args)
            {
                OnCollectionChanged(args);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Notify<TArgument, TResult>(TSelf @this, TCollection @new, TCollection old, Args<TArgument, TResult>? notify, TArgument argument, TResult value)
        {
            if (_changed is null)
            {
                return;
            }

            if (notify?.Invoke(@this, @new, old, argument, value) is { } args)
            {
                OnCollectionChanged(args);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Notify<TArgument>(TSelf @this, TCollection @new, TCollection old, Args<TArgument>? notify1, Args<TArgument>? notify2, TArgument argument)
        {
            if (_changed is null)
            {
                return;
            }

            if (notify1?.Invoke(@this, @new, old, argument) is { } args1)
            {
                OnCollectionChanged(args1);
            }

            if (notify2?.Invoke(@this, @new, old, argument) is { } args2)
            {
                OnCollectionChanged(args2);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Notify<TArgument, TResult>(TSelf @this, TCollection @new, TCollection old, Args<TArgument, TResult>? notify1, Args<TArgument, TResult>? notify2, TArgument argument, TResult value)
        {
            if (_changed is null)
            {
                return;
            }

            if (notify1?.Invoke(@this, @new, old, argument, value) is { } args1)
            {
                OnCollectionChanged(args1);
            }

            if (notify2?.Invoke(@this, @new, old, argument, value) is { } args2)
            {
                OnCollectionChanged(args2);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected Exception? Notify<TArgument>(TArgument argument, Write<TArgument> write, Args<TArgument>? notify)
        {
            TSelf @this = This;

            if (Lock is not null)
            {
                Lock.EnterUpgradeableReadLock();

                TCollection modify = Immutable;

                Lock.EnterWriteLock();

                try
                {
                    if (write(@this, modify, argument) is { } @new)
                    {
                        Notify(@this, Collection = @new, modify, notify, argument);
                    }
                }
                catch (Exception exception)
                {
                    return exception;
                }
                finally
                {
                    Lock.ExitWriteLock();
                    Lock.ExitUpgradeableReadLock();
                }
            }
            else
            {
                TCollection modify = Immutable;

                try
                {
                    if (write(@this, modify, argument) is { } @new)
                    {
                        Notify(@this, Collection = @new, modify, notify, argument);
                    }
                }
                catch (Exception exception)
                {
                    return exception;
                }
            }

            ViewChanged.Invoke(this);
            return null;
        }

        // ReSharper disable once CognitiveComplexity
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected FlowResult<TResult> Notify<TArgument, TResult>(TArgument argument, Read<TArgument, TResult> read, Write<TArgument, TResult> write, Args<TArgument, TResult>? notify)
        {
            TSelf @this = This;
            FlowResult<TResult> result;

            if (Lock is not null)
            {
                Lock.EnterUpgradeableReadLock();

                TCollection modify = Immutable;
                TResult? value;

                try
                {
                    result = read(@this, modify, argument);

                    if (!result.HasNext)
                    {
                        Lock.ExitUpgradeableReadLock();
                        return result;
                    }

                    value = result;
                }
                catch (Exception exception)
                {
                    Lock.ExitUpgradeableReadLock();
                    return exception;
                }

                Lock.EnterWriteLock();

                try
                {
                    if (write(@this, modify, argument, value) is { } @new)
                    {
                        Notify(@this, Collection = @new, modify, notify, argument, value);
                    }
                }
                catch (Exception exception)
                {
                    return exception;
                }
                finally
                {
                    Lock.ExitWriteLock();
                    Lock.ExitUpgradeableReadLock();
                }
            }
            else
            {
                TCollection modify = Immutable;
                TResult? value;

                try
                {
                    result = read(@this, modify, argument);

                    if (!result.HasNext)
                    {
                        return result;
                    }

                    value = result;
                }
                catch (Exception exception)
                {
                    return exception;
                }

                try
                {
                    if (write(@this, modify, argument, value) is { } @new)
                    {
                        Notify(@this, Collection = @new, modify, notify, argument, value);
                    }
                }
                catch (Exception exception)
                {
                    return exception;
                }
            }

            ViewChanged.Invoke(this);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected Exception? Notify<TArgument>(TArgument argument, Write<TArgument> write, Args<TArgument>? notify1, Args<TArgument>? notify2)
        {
            TSelf @this = This;

            if (Lock is not null)
            {
                Lock.EnterUpgradeableReadLock();

                TCollection modify = Immutable;

                Lock.EnterWriteLock();

                try
                {
                    if (write(@this, modify, argument) is { } @new)
                    {
                        Notify(@this, Collection = @new, modify, notify1, notify2, argument);
                    }
                }
                catch (Exception exception)
                {
                    return exception;
                }
                finally
                {
                    Lock.ExitWriteLock();
                    Lock.ExitUpgradeableReadLock();
                }
            }
            else
            {
                TCollection modify = Immutable;

                try
                {
                    if (write(@this, modify, argument) is { } @new)
                    {
                        Notify(@this, Collection = @new, modify, notify1, notify2, argument);
                    }
                }
                catch (Exception exception)
                {
                    return exception;
                }
            }

            ViewChanged.Invoke(this);
            return null;
        }

        // ReSharper disable once CognitiveComplexity
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected FlowResult<TResult> Notify<TArgument, TResult>(TArgument argument, Read<TArgument, TResult> read, Write<TArgument, TResult> write, Args<TArgument, TResult>? notify1, Args<TArgument, TResult>? notify2)
        {
            TSelf @this = This;
            FlowResult<TResult> result;

            if (Lock is not null)
            {
                Lock.EnterUpgradeableReadLock();

                TCollection modify = Immutable;
                TResult? value;

                try
                {
                    result = read(@this, modify, argument);

                    if (!result.HasNext)
                    {
                        Lock.ExitUpgradeableReadLock();
                        return result;
                    }

                    value = result;
                }
                catch (Exception exception)
                {
                    Lock.ExitUpgradeableReadLock();
                    return exception;
                }

                Lock.EnterWriteLock();

                try
                {
                    if (write(@this, modify, argument, value) is { } @new)
                    {
                        Notify(@this, Collection = @new, modify, notify1, notify2, argument, value);
                    }
                }
                catch (Exception exception)
                {
                    return exception;
                }
                finally
                {
                    Lock.ExitWriteLock();
                    Lock.ExitUpgradeableReadLock();
                }
            }
            else
            {
                TCollection modify = Immutable;
                TResult? value;

                try
                {
                    result = read(@this, modify, argument);

                    if (!result.HasNext)
                    {
                        return result;
                    }

                    value = result;
                }
                catch (Exception exception)
                {
                    return exception;
                }

                try
                {
                    if (write(@this, modify, argument, value) is { } @new)
                    {
                        Notify(@this, Collection = @new, modify, notify1, notify2, argument, value);
                    }
                }
                catch (Exception exception)
                {
                    return exception;
                }
            }

            ViewChanged.Invoke(this);
            return result;
        }

        // ReSharper disable once CognitiveComplexity
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected Exception? Notify<TArgument>(TArgument argument, Predicate<TSelf, TCollection, TArgument> predicate, (Write<TArgument> Write, Args<TArgument>? Notify) @true, (Write<TArgument> Write, Args<TArgument>? Notify) @false)
        {
            TSelf @this = This;

            if (Lock is not null)
            {
                Lock.EnterUpgradeableReadLock();

                TCollection modify = Immutable;
                (Write<TArgument> write, Args<TArgument>? notify) = predicate(@this, modify, argument) ? @true : @false;

                Lock.EnterWriteLock();

                try
                {
                    if (write(@this, modify, argument) is { } @new)
                    {
                        Notify(@this, Collection = @new, modify, notify, argument);
                    }
                }
                catch (Exception exception)
                {
                    return exception;
                }
                finally
                {
                    Lock.ExitWriteLock();
                    Lock.ExitUpgradeableReadLock();
                }
            }
            else
            {
                TCollection modify = Immutable;
                (Write<TArgument> write, Args<TArgument>? notify) = predicate(@this, modify, argument) ? @true : @false;

                try
                {
                    if (write(@this, modify, argument) is { } @new)
                    {
                        Notify(@this, Collection = @new, modify, notify, argument);
                    }
                }
                catch (Exception exception)
                {
                    return exception;
                }
            }

            ViewChanged.Invoke(this);
            return null;
        }

        // ReSharper disable once CognitiveComplexity
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected FlowResult<TResult> Notify<TArgument, TResult>(TArgument argument, Predicate<TSelf, TCollection, TArgument> predicate, (Read<TArgument, TResult> Read, Write<TArgument, TResult> Write, Args<TArgument, TResult>? Notify) @true, (Read<TArgument, TResult> Read, Write<TArgument, TResult> Write, Args<TArgument, TResult>? Notify) @false)
        {
            TSelf @this = This;
            FlowResult<TResult> result;

            if (Lock is not null)
            {
                Lock.EnterUpgradeableReadLock();

                TResult? value;
                TCollection modify = Immutable;
                (Read<TArgument, TResult> read, Write<TArgument, TResult> write, Args<TArgument, TResult>? notify) = predicate(@this, modify, argument) ? @true : @false;

                try
                {
                    result = read(@this, modify, argument);

                    if (!result.HasNext)
                    {
                        Lock.ExitUpgradeableReadLock();
                        return result;
                    }

                    value = result;
                }
                catch (Exception exception)
                {
                    Lock.ExitUpgradeableReadLock();
                    return exception;
                }

                Lock.EnterWriteLock();

                try
                {
                    if (write(@this, modify, argument, value) is { } @new)
                    {
                        Notify(@this, Collection = @new, modify, notify, argument, value);
                    }
                }
                catch (Exception exception)
                {
                    return exception;
                }
                finally
                {
                    Lock.ExitWriteLock();
                    Lock.ExitUpgradeableReadLock();
                }
            }
            else
            {
                TResult? value;
                TCollection modify = Immutable;
                (Read<TArgument, TResult> read, Write<TArgument, TResult> write, Args<TArgument, TResult>? notify) = predicate(@this, modify, argument) ? @true : @false;

                try
                {
                    result = read(@this, modify, argument);

                    if (!result.HasNext)
                    {
                        return result;
                    }

                    value = result;
                }
                catch (Exception exception)
                {
                    return exception;
                }

                try
                {
                    if (write(@this, modify, argument, value) is { } @new)
                    {
                        Notify(@this, Collection = @new, modify, notify, argument, value);
                    }
                }
                catch (Exception exception)
                {
                    return exception;
                }
            }

            ViewChanged.Invoke(this);
            return result;
        }
    }
}