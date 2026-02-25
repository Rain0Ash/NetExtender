using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using NetExtender.Types.Concurrent.Observable.Interfaces;

namespace NetExtender.Types.Concurrent.Observable
{
    public partial class ConcurrentObservableCollection<T>
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        internal sealed class Bridge : IList<T>, IList
        {
            private IConcurrentObservableList<T> Observable { get; }
            private ImmutableList<T> Source { get; set; }
            private volatile Thread? _thread;
            public Boolean Freeze { get; set; }

            public Int32 Count
            {
                get
                {
                    return Source.Count;
                }
            }

            Boolean ICollection<T>.IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            Boolean IList.IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            Boolean IList.IsFixedSize
            {
                get
                {
                    return false;
                }
            }

            Object ICollection.SyncRoot
            {
                get
                {
                    return Source;
                }
            }

            Boolean ICollection.IsSynchronized
            {
                get
                {
                    return false;
                }
            }

            public Bridge(IConcurrentObservableList<T> destination)
                : this(destination, ImmutableList<T>.Empty)
            {
            }

            public Bridge(IConcurrentObservableList<T> destination, ImmutableList<T>? source)
            {
                Observable = destination ?? throw new ArgumentNullException(nameof(destination));
                Source = source ?? ImmutableList<T>.Empty;
            }

            internal Bridge UpdateSource(ImmutableList<T> source)
            {
                Bridge bridge = Freeze || _thread == Thread.CurrentThread ? this : new Bridge(Observable, source);
                _thread = null;
                return bridge;
            }

            private void Sync(Action<Bridge> action)
            {
                _thread = Thread.CurrentThread;
                action(this);
                Source = Observable.Immutable;
                _thread = null;
            }

            private void Sync<TValue>(Action<Bridge, TValue> action, TValue value)
            {
                _thread = Thread.CurrentThread;
                action(this, value);
                Source = Observable.Immutable;
                _thread = null;
            }

            private void Sync<T1, T2>(Action<Bridge, T1, T2> action, T1 first, T2 second)
            {
                _thread = Thread.CurrentThread;
                action(this, first, second);
                Source = Observable.Immutable;
                _thread = null;
            }

            private TResult Sync<TResult>(Func<Bridge, TResult> action)
            {
                _thread = Thread.CurrentThread;
                TResult result = action(this);
                Source = Observable.Immutable;
                _thread = null;
                return result;
            }

            private TResult Sync<TValue, TResult>(Func<Bridge, TValue, TResult> action, TValue value)
            {
                _thread = Thread.CurrentThread;
                TResult result = action(this, value);
                Source = Observable.Immutable;
                _thread = null;
                return result;
            }

            private TResult Sync<T1, T2, TResult>(Func<Bridge, T1, T2, TResult> action, T1 first, T2 second)
            {
                _thread = Thread.CurrentThread;
                TResult result = action(this, first, second);
                Source = Observable.Immutable;
                _thread = null;
                return result;
            }

            Boolean IList.Contains(Object? value)
            {
                return ((IList) Source).Contains(value);
            }

            public Boolean Contains(T item)
            {
                return Source.Contains(item);
            }

            Int32 IList.IndexOf(Object? value)
            {
                return ((IList) Source).IndexOf(value);
            }

            public Int32 IndexOf(T item)
            {
                return Source.IndexOf(item);
            }

            Int32 IList.Add(Object? value)
            {
                return Sync(static (@this, value) => ((IList) @this.Observable).Add(value), value);
            }

            public void Add(T item)
            {
                Sync(static (@this, item) => @this.Observable.Add(item), item);
            }

            void IList.Insert(Int32 index, Object? value)
            {
                Sync(static (@this, index, value) => ((IList) @this.Observable).Insert(index, value), index, value);
            }

            public void Insert(Int32 index, T item)
            {
                Sync(static (@this, index, item) => @this.Observable.Insert(index, item), index, item);
            }

            void IList.Clear()
            {
                Sync(static @this => ((IList) @this.Observable).Clear());
            }

            public void Clear()
            {
                Sync(static @this => @this.Observable.Clear());
            }

            void IList.Remove(Object? value)
            {
                Sync(static (@this, value) => ((IList) @this.Observable).Remove(value), value);
            }

            public Boolean Remove(T item)
            {
                return Sync(static (@this, item) => @this.Observable.Remove(item), item);
            }

            void IList.RemoveAt(Int32 index)
            {
                Sync(static (@this, index) => ((IList) @this.Observable).RemoveAt(index), index);
            }

            public void RemoveAt(Int32 index)
            {
                Sync(static (@this, index) => @this.Observable.RemoveAt(index), index);
            }

            void ICollection.CopyTo(Array array, Int32 index)
            {
                ((IList) Source).CopyTo(array, index);
            }

            public void CopyTo(T[] array, Int32 index)
            {
                Source.CopyTo(array, index);
            }

            public IEnumerator<T> GetEnumerator()
            {
                return Source.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable) Source).GetEnumerator();
            }

            Object? IList.this[Int32 index]
            {
                get
                {
                    return ((IList) Source)[index];
                }
                set
                {
                    Sync(static (@this, index, value) => ((IList) @this.Observable)[index] = value, index, value);
                }
            }

            public T this[Int32 index]
            {
                get
                {
                    return Source[index];
                }
                set
                {
                    Sync(static (@this, index, value) => @this.Observable[index] = value, index, value);
                }
            }
        }
    }
}