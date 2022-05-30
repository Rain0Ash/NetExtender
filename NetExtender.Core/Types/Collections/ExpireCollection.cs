// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Times.Timers.Interfaces;
using NetExtender.Types.Events;
using NetExtender.Types.Timers;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Collections
{
    public class ExpireCollection<T> : ICollection<KeyValuePair<DateTime, T>>, ICollection<T>, IDisposable
    {
        protected SortedList<DateTime, T> Internal { get; }
        protected ITimer Timer { get; }

        private TimeSpan _expire;
        public TimeSpan Expire
        {
            get
            {
                return _expire;
            }
            set
            {
                _expire = value;
                Expiration(Timer, new TimeEventArgs(DateTime.Now));
            }
        }

        public TimeSpan Interval
        {
            get
            {
                return Timer.Interval;
            }
            set
            {
                Timer.Interval = value;
            }
        }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return ((ICollection<KeyValuePair<DateTime, T>>) Internal).IsReadOnly;
            }
        }

        public event TickHandler? Tick;

        public ExpireCollection(TimeSpan expire)
            : this(expire, new TimeSpan())
        {
        }

        public ExpireCollection(TimeSpan expire, TimeSpan interval)
        {
            Internal = new SortedList<DateTime, T>();
            _expire = expire;
            Timer = new TimerWrapper(interval);
            Timer.Tick += Expiration;
        }

        public void Expiration()
        {
            Expiration(Timer, new TimeEventArgs(DateTime.Now));
        }

        private void Expiration(Object? sender, TimeEventArgs args)
        {
            while (Internal.Count > 0 && Internal.Keys[0] <= args.SignalTime)
            {
                Internal.RemoveAt(0);
            }
            
            Tick?.Invoke(this, new TimeEventArgs(args.SignalTime));
        }
        
        public Boolean Contains(KeyValuePair<DateTime, T> item)
        {
            return ((ICollection<KeyValuePair<DateTime, T>>) Internal).Contains(item);
        }

        public Boolean Contains(T item)
        {
            return Internal.ContainsValue(item);
        }
        
        public void Add(KeyValuePair<DateTime, T> item)
        {
            Internal.Add(item.Key, item.Value);
        }
        
        public void Add(T item)
        {
            Add(item, Expire);
        }

        public void Add(T item, TimeSpan expire)
        {
            Internal.Add(DateTime.Now.Add(expire), item);
        }
        
        public Boolean Remove(KeyValuePair<DateTime, T> item)
        {
            return ((ICollection<KeyValuePair<DateTime, T>>) Internal).Remove(item);
        }

        public Boolean Remove(T item)
        {
            foreach (KeyValuePair<DateTime, T> pair in Internal.Where(pair => EqualityComparer<T>.Default.Equals(pair.Value, item)))
            {
                Internal.Remove(pair.Key);
                return true;
            }
            
            return false;
        }
        
        public void Clear()
        {
            Internal.Clear();
        }

        public void CopyTo(KeyValuePair<DateTime, T>[] array, Int32 arrayIndex)
        {
            ((ICollection<KeyValuePair<DateTime, T>>) Internal).CopyTo(array, arrayIndex);
        }
        
        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            Internal.Values.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Internal.Values.GetEnumerator();
        }

        IEnumerator<KeyValuePair<DateTime, T>> IEnumerable<KeyValuePair<DateTime, T>>.GetEnumerator()
        {
            return ((ICollection<KeyValuePair<DateTime, T>>) Internal).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(Boolean disposing)
        {
            Tick = null;
            Timer.Stop();
            Timer.Dispose();
        }
        
        ~ExpireCollection()
        {
            Dispose(false);
        }
    }
}