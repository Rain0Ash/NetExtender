using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Interfaces.Notify;
using NetExtender.Types.Collections;
using NetExtender.Types.Exceptions;

namespace NetExtender.Types.Dictionaries
{
    public abstract class AutoKeyDictionary<TKey, TValue, TDictionary> : IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue> where TKey : notnull where TValue : class where TDictionary : class, IDictionary<TKey, AutoKeyDictionary<TKey, TValue, TDictionary>.Box>
    {
        protected TDictionary Internal { get; }
        protected abstract String Property { get; }
        protected abstract Func<TValue, TKey> Getter { get; }
        protected abstract Action<TValue, TKey>? Setter { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public virtual ICollection<TKey> Keys
        {
            get
            {
                return Internal.Keys;
            }
        }

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get
            {
                return Keys;
            }
        }

        private readonly SelectorCollectionWrapper<Box, TValue> _values;
        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get
            {
                return _values;
            }
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get
            {
                return _values;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Internal.IsReadOnly;
            }
        }

        protected AutoKeyDictionary(TDictionary dictionary)
        {
            Internal = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
            _values = new SelectorCollectionWrapper<Box, TValue>(Internal.Values, static key => key.Value);
        }

        private void BoxPropertyChanging(Object? sender, PropertyChangingEventArgs args)
        {
            if (sender is Box box)
            {
                BoxPropertyChanging(box);
            }
        }

        protected virtual void BoxPropertyChanging(TValue value)
        {
        }

        private void BoxPropertyChanged(Object? sender, PropertyChangedEventArgs args)
        {
            if (sender is Box box)
            {
                BoxPropertyChanged(box);
            }
        }
        
        protected virtual void BoxPropertyChanged(TValue value)
        {
            
        }

        protected Box New(TValue value)
        {
            return new Box(this, value);
        }

        protected virtual void Resync()
        {
            lock (Internal)
            {
                KeyValuePair<TKey, Box>[] entries = new KeyValuePair<TKey, Box>[Internal.Count];
                Internal.CopyTo(entries, 0);
                Internal.Clear();

                List<Exception> exceptions = new List<Exception>(4);
                
                foreach ((_, Box box) in entries)
                {
                    try
                    {
                        if (Getter(box.Value) is not { } key)
                        {
                            continue;
                        }

                        Internal.Add(key, box);
                    }
                    catch (Exception exception)
                    {
                        box.Dispose();
                        exceptions.Add(exception);
                    }
                }

                switch (exceptions.Count)
                {
                    case <= 0:
                        return;
                    case 1:
                        throw exceptions[0];
                    default:
                        throw new AggregateException(exceptions);
                }
            }
        }

        Boolean ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return ContainsCore(item.Key, item.Value);
        }

        protected virtual Boolean ContainsCore(TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return TryGetValueCore(key, out Box? box) && box.Equals(value);
        }

        public virtual Boolean ContainsKey(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Internal.ContainsKey(key);
        }

        protected virtual Boolean TryGetValueCore(TKey key, [MaybeNullWhen(false)] out Box box)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Internal.TryGetValue(key, out box);
        }

        protected virtual Boolean TryGetValueCore(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            if (!TryGetValueCore(key, out Box? box))
            {
                value = null;
                return false;
            }

            TKey getter = Getter(box.Value);
            if (!EqualityComparer<TKey>.Default.Equals(key, getter))
            {
                throw new CollectionSynchronizationException();
            }
            
            value = box.Value;
            return true;
        }

        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            try
            {
                return TryGetValueCore(key, out value);
            }
            catch (CollectionSynchronizationException)
            {
                value = null;
                return false;
            }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            ((IDictionary<TKey, TValue>) this).Add(item.Key, item.Value);
        }

        protected virtual void AddCore(TKey key, TValue value)
        {
            Internal.Add(key, New(value));
        }

        public TKey Add(TValue value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (Getter(value) is not { } key)
            {
                throw new ArgumentException($"The value '{value}' property '{Property}' is null.", nameof(value));
            }

            AddCore(key, value);
            return key;
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            TKey getter = Getter(value);
            if (!EqualityComparer<TKey>.Default.Equals(key, getter))
            {
                throw new ArgumentException($"Key is '{key}', but value '{value}' property '{Property}' has key '{getter}'.", nameof(key));
            }

            if ((key = getter) is null)
            {
                throw new ArgumentException($"The value '{value}' property '{Property}' is null.", nameof(value));
            }
            
            AddCore(key, value);
        }

        Boolean ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return RemoveCore(item.Key, item.Value);
        }

        protected virtual Boolean RemoveCore(TKey key)
        {
            if (!TryGetValueCore(key, out Box? box))
            {
                return false;
            }

            TKey getter = Getter(box.Value);
            if (!EqualityComparer<TKey>.Default.Equals(key, getter))
            {
                Resync();
            }
            
            return Internal.Remove(getter);
        }

        protected virtual Boolean RemoveCore(TValue value)
        {
            if (Getter(value) is not { } key)
            {
                return false;
            }

            if (!Internal.TryGetValue(key, out Box? box))
            {
                return false;
            }

            if (box.Equals(value))
            {
                return Internal.Remove(key);
            }

            throw new CollectionSynchronizationException();
        }

        protected virtual Boolean RemoveCore(TKey key, TValue value)
        {
            if (!TryGetValueCore(key, out Box? box))
            {
                return false;
            }

            if (!box.Equals(value))
            {
                throw new CollectionSynchronizationException();
            }

            return Remove(key);
        }

        public Boolean Remove(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return RemoveCore(key);
        }

        public Boolean Remove(TValue value)
        {
            try
            {
                return RemoveCore(value);
            }
            catch (CollectionSynchronizationException)
            {
                Resync();
                return RemoveCore(value);
            }
        }

        protected virtual Boolean Remove(TKey key, TValue value)
        {
            try
            {
                return RemoveCore(key, value);
            }
            catch (CollectionSynchronizationException)
            {
                Resync();
                return RemoveCore(Getter(value), value);
            }
        }

        public virtual void Clear()
        {
            Box[] array = Internal.Values.ToArray();
            Internal.Clear();

            foreach (Box box in array)
            {
                box.Dispose();
            }
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 index)
        {
            
        }

        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual TValue this[TKey key]
        {
            get
            {
                return Internal[key];
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                TKey getter = Getter(value);
                if (!EqualityComparer<TKey>.Default.Equals(key, getter))
                {
                    throw new ArgumentException($"Key is '{key}', but value '{value}' property '{Property}' has key '{getter}'.", nameof(key));
                }

                lock (Internal)
                {
                    if (Internal.TryGetValue(key = getter, out Box? box))
                    {
                        box.Dispose();
                    }

                    Internal[key] = New(value);
                }
            }
        }

        public sealed class Box : IEquatable<TValue>, IEquatable<Box>, INotifyProperty, IDisposable
        {
            [return: NotNullIfNotNull("value")]
            public static implicit operator TValue?(Box? value)
            {
                return value?.Value;
            }
            
            public event PropertyChangingEventHandler? PropertyChanging;
            public event PropertyChangedEventHandler? PropertyChanged;
            private AutoKeyDictionary<TKey, TValue, TDictionary> Internal { get; }
            public readonly TValue Value;

            internal Box(AutoKeyDictionary<TKey, TValue, TDictionary> @internal, TValue value)
            {
                Internal = @internal ?? throw new ArgumentNullException(nameof(@internal));
                Value = value;

                try
                {
                    PropertyChanging += Internal.BoxPropertyChanging;
                    PropertyChanged += Internal.BoxPropertyChanged;

                    if (value is INotifyPropertyChanging changing)
                    {
                        changing.PropertyChanging += OnPropertyChanging;
                    }

                    if (value is INotifyPropertyChanged changed)
                    {
                        changed.PropertyChanged += OnPropertyChanged;
                    }
                }
                catch (Exception)
                {
                    Dispose();
                    throw;
                }
            }

            private void OnPropertyChanging(Object? sender, PropertyChangingEventArgs args)
            {
                if (args.PropertyName == Internal.Property)
                {
                    PropertyChanging?.Invoke(this, args);
                }
            }

            private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs args)
            {
                if (args.PropertyName == Internal.Property)
                {
                    PropertyChanged?.Invoke(this, args);
                }
            }

            internal TKey Get()
            {
                return Internal.Getter(Value);
            }

            internal Boolean Set(TKey key)
            {
                if (Internal.Setter is not { } setter)
                {
                    return false;
                }

                setter(Value, key);
                return true;
            }

            public override Int32 GetHashCode()
            {
                return Value.GetHashCode();
            }

            public override Boolean Equals(Object? other)
            {
                return ReferenceEquals(this, other) || other switch
                {
                    TValue value => Equals(value),
                    Box box => Equals(box),
                    _ => false
                };
            }

            public Boolean Equals(TValue? other)
            {
                return EqualityComparer<TValue>.Default.Equals(Value, other);
            }

            public Boolean Equals(Box? other)
            {
                return ReferenceEquals(this, other);
            }

            public override String? ToString()
            {
                return Value.ToString();
            }

            public void Dispose()
            {
                PropertyChanging -= Internal.BoxPropertyChanging;
                PropertyChanged -= Internal.BoxPropertyChanged;

                if (Value is INotifyPropertyChanging changing)
                {
                    changing.PropertyChanging -= OnPropertyChanging;
                }

                if (Value is INotifyPropertyChanged changed)
                {
                    changed.PropertyChanged -= OnPropertyChanged;
                }
            }
        }
    }
}