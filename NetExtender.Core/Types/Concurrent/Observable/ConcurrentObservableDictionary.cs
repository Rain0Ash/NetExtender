using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using NetExtender.Types.Comparers;
using NetExtender.Types.Concurrent.Observable.Interfaces;
using NetExtender.Types.Enumerators;
using NetExtender.Types.Immutable.Dictionaries;
using NetExtender.Types.Monads;
using NetExtender.Types.Sizes;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Concurrent.Observable
{
    [Serializable]
    public class ConcurrentObservableDictionary<TKey, TValue> : ConcurrentObservableDictionary<TKey, TValue, ConcurrentObservableDictionary<TKey, TValue>>
    {
        public sealed override Int32 Count
        {
            get
            {
                return base.Count;
            }
        }

        public sealed override Boolean IsEmpty
        {
            get
            {
                return base.IsEmpty;
            }
        }
        
        protected ConcurrentObservableDictionary()
        {
        }

        protected ConcurrentObservableDictionary(Boolean @lock)
            : base(@lock)
        {
        }

        protected ConcurrentObservableDictionary(IEqualityComparer<TKey>? comparer)
            : base(comparer)
        {
        }

        protected ConcurrentObservableDictionary(IEqualityComparer<TKey>? comparer, Boolean @lock)
            : base(comparer, @lock)
        {
        }

        protected ConcurrentObservableDictionary(IEqualityComparer<NullMaybe<TKey>>? comparer)
            : base(comparer)
        {
        }

        protected ConcurrentObservableDictionary(IEqualityComparer<NullMaybe<TKey>>? comparer, Boolean @lock)
            : base(comparer, @lock)
        {
        }

        protected ConcurrentObservableDictionary(IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
            : base(keyComparer, valueComparer)
        {
        }

        protected ConcurrentObservableDictionary(IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer, Boolean @lock)
            : base(keyComparer, valueComparer, @lock)
        {
        }

        protected ConcurrentObservableDictionary(IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer)
            : base(keyComparer, valueComparer)
        {
        }

        protected ConcurrentObservableDictionary(IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer, Boolean @lock)
            : base(keyComparer, valueComparer, @lock)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source)
            : base(source)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, Boolean @lock)
            : base(source, @lock)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer)
            : base(source, comparer)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer, Boolean @lock)
            : base(source, comparer, @lock)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<NullMaybe<TKey>>? comparer)
            : base(source, comparer)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<NullMaybe<TKey>>? comparer, Boolean @lock)
            : base(source, comparer, @lock)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
            : base(source, keyComparer, valueComparer)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer, Boolean @lock)
            : base(source, keyComparer, valueComparer, @lock)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer)
            : base(source, keyComparer, valueComparer)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer, Boolean @lock)
            : base(source, keyComparer, valueComparer, @lock)
        {
        }

        protected ConcurrentObservableDictionary(ImmutableNullableDictionary<TKey, TValue>? source)
            : base(source)
        {
        }

        protected ConcurrentObservableDictionary(ImmutableNullableDictionary<TKey, TValue>? source, Boolean @lock)
            : base(source, @lock)
        {
        }

        protected ConcurrentObservableDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public sealed override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        protected sealed override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            base.OnCollectionChanged(args);
        }

        public sealed override Boolean Contains(KeyValuePair<TKey, TValue> item)
        {
            return base.Contains(item);
        }

        public sealed override Boolean ContainsKey(TKey key)
        {
            return base.ContainsKey(key);
        }

        public sealed override Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return base.TryGetValue(key, out value);
        }

        public sealed override Boolean Add(KeyValuePair<TKey, TValue> pair)
        {
            return base.Add(pair);
        }

        protected sealed override Int32? Add(IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            return base.Add(items);
        }

        public sealed override TValue GetOrAdd(TKey key, Func<TValue> factory)
        {
            return base.GetOrAdd(key, factory);
        }

        public sealed override TValue GetOrAdd(TKey key, Func<TKey, TValue> factory)
        {
            return base.GetOrAdd(key, factory);
        }

        public sealed override Boolean Remove(KeyValuePair<TKey, TValue> pair)
        {
            return base.Remove(pair);
        }

        public sealed override Boolean Remove(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return base.Remove(key, out value);
        }

        public sealed override void RemoveRange(IEnumerable<TKey>? items)
        {
            base.RemoveRange(items);
        }

        public sealed override void RemoveRange(IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            base.RemoveRange(items);
        }

        protected sealed override void Clear(out Int32 count)
        {
            base.Clear(out count);
        }

        public sealed override void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 index)
        {
            base.CopyTo(array, index);
        }

        public sealed override TValue this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                base[key] = value;
            }
        }
    }

    [Serializable]
    public class ConcurrentObservableDictionary<TKey, TValue, TSelf> : ConcurrentObservableBase<KeyValuePair<TKey, TValue>, ImmutableNullableDictionary<TKey, TValue>, TSelf>, IConcurrentObservableHashDictionary<TKey, TValue>, IDictionary where TSelf : ConcurrentObservableDictionary<TKey, TValue, TSelf>
    {
        protected sealed override ImmutableNullableDictionary<TKey, TValue> Collection { get; set; }

        public sealed override IDictionary<TKey, TValue> View
        {
            get
            {
                return Collection;
            }
        }

        private IEqualityComparer<TKey>? _comparer;
        public IEqualityComparer<TKey> Comparer
        {
            get
            {
                return _comparer ??= new NullableEqualityComparerUnwrapper<ConcurrentObservableDictionary<TKey, TValue, TSelf>, TKey>(this, static source => source.Collection.KeyComparer);
            }
        }

        public IEqualityComparer<TKey> KeyComparer
        {
            get
            {
                return Comparer;
            }
        }

        public IEqualityComparer<TValue> ValueComparer
        {
            get
            {
                return Collection.ValueComparer;
            }
        }

        public sealed override ImmutableNullableDictionary<TKey, TValue> Immutable
        {
            get
            {
                return Collection;
            }
        }

        IImmutableDictionary<TKey, TValue> IConcurrentObservableDictionary<TKey, TValue>.Immutable
        {
            get
            {
                return Immutable;
            }
        }

        public override Int32 Count
        {
            get
            {
                return Immutable.Count;
            }
        }

        public override Boolean IsEmpty
        {
            get
            {
                return Immutable.IsEmpty;
            }
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                return Immutable.Keys;
            }
        }

        ICollection IDictionary.Keys
        {
            get
            {
                return ((IDictionary) Immutable).Keys;
            }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get
            {
                return ((IDictionary<TKey, TValue>) Immutable).Keys;
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                return Immutable.Values;
            }
        }
        
        ICollection IDictionary.Values
        {
            get
            {
                return ((IDictionary) Immutable).Values;
            }
        }
        
        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get
            {
                return ((IDictionary<TKey, TValue>) Immutable).Values;
            }
        }

        Boolean IDictionary.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        Boolean ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        Boolean IDictionary.IsFixedSize
        {
            get
            {
                return ((IDictionary) Collection).IsFixedSize;
            }
        }
        
        Object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection) Collection).SyncRoot;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return ((ICollection) Collection).IsSynchronized;
            }
        }

        protected ConcurrentObservableDictionary()
            : this(true)
        {
        }

        protected ConcurrentObservableDictionary(Boolean @lock)
            : this(ImmutableNullableDictionary<TKey, TValue>.Empty, @lock)
        {
        }

        protected ConcurrentObservableDictionary(IEqualityComparer<TKey>? comparer)
            : this(comparer, true)
        {
        }

        protected ConcurrentObservableDictionary(IEqualityComparer<TKey>? comparer, Boolean @lock)
            : this(ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(comparer), @lock)
        {
        }

        protected ConcurrentObservableDictionary(IEqualityComparer<NullMaybe<TKey>>? comparer)
            : this(comparer, true)
        {
        }

        protected ConcurrentObservableDictionary(IEqualityComparer<NullMaybe<TKey>>? comparer, Boolean @lock)
            : this(ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(comparer), @lock)
        {
        }

        protected ConcurrentObservableDictionary(IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
            : this(keyComparer, valueComparer, true)
        {
        }

        protected ConcurrentObservableDictionary(IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer, Boolean @lock)
            : this(ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer), @lock)
        {
        }

        protected ConcurrentObservableDictionary(IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer)
            : this(keyComparer, valueComparer, true)
        {
        }

        protected ConcurrentObservableDictionary(IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer, Boolean @lock)
            : this(ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer), @lock)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source)
            : this(source, true)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, Boolean @lock)
            : this(source, (IEqualityComparer<NullMaybe<TKey>>?) null, @lock)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer)
            : this(source, true)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? comparer, Boolean @lock)
            : this(source is not null ? ImmutableNullableDictionary.CreateRange(comparer, source) : ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(comparer), @lock)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<NullMaybe<TKey>>? comparer)
            : this(source, true)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<NullMaybe<TKey>>? comparer, Boolean @lock)
            : this(source is not null ? ImmutableNullableDictionary.CreateRange(comparer, source) : ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(comparer), @lock)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
            : this(source, true)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer, Boolean @lock)
            : this(source is not null ? ImmutableNullableDictionary.CreateRange(keyComparer, valueComparer, source) : ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer), @lock)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer)
            : this(source, true)
        {
        }

        protected ConcurrentObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>>? source, IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<TValue>? valueComparer, Boolean @lock)
            : this(source is not null ? ImmutableNullableDictionary.CreateRange(keyComparer, valueComparer, source) : ImmutableNullableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer), @lock)
        {
        }

        protected ConcurrentObservableDictionary(ImmutableNullableDictionary<TKey, TValue>? source)
            : this(source, true)
        {
        }

        protected ConcurrentObservableDictionary(ImmutableNullableDictionary<TKey, TValue>? source, Boolean @lock)
            : base(@lock)
        {
            Collection = source ?? ImmutableNullableDictionary<TKey, TValue>.Empty;
            PropertyChanging += OnViewChanging;
            PropertyChanged += OnViewChanged;
        }

        protected ConcurrentObservableDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Collection = info.GetValue(nameof(Collection), typeof(KeyValuePair<TKey, TValue>[])) is KeyValuePair<TKey, TValue>[] array ? ImmutableNullableDictionary.CreateRange(array) : ImmutableNullableDictionary<TKey, TValue>.Empty;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Collection), Immutable.ToArray());
        }

        private void OnViewChanging(Object? sender, PropertyChangingEventArgs args)
        {
            if (args.PropertyName != nameof(View))
            {
                return;
            }

            RaisePropertyChanging(nameof(Keys));
            RaisePropertyChanging(nameof(Values));
        }

        private void OnViewChanged(Object? sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(View))
            {
                return;
            }

            RaisePropertyChanged(nameof(Keys));
            RaisePropertyChanged(nameof(Values));
        }

        Boolean IDictionary.Contains(Object? key)
        {
            return ContainsKey((TKey) key!);
        }

        public override Boolean Contains(KeyValuePair<TKey, TValue> item)
        {
            return Immutable.Contains(item);
        }

        public virtual Boolean ContainsKey(TKey key)
        {
            return Immutable.ContainsKey(key);
        }

        public virtual Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return Immutable.TryGetValue(key, out value);
        }

        public override Boolean Add(KeyValuePair<TKey, TValue> pair)
        {
            FlowResult<Boolean> result = Notify(pair,
                static (_, modify, pair) => modify.ContainsKey(pair.Key) ? false.Result().Flow() : true.Flow(),
                static (_, modify, pair, _) => modify.Add(pair.Key, pair.Value),
                static (_, _, _, pair, _) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, pair)
            );

            return result.Exception is not { } exception ? result.Unwrap(out Boolean state) && state : throw exception;
        }

        void IDictionary.Add(Object? key, Object? value)
        {
            if (!Add((TKey) key!, (TValue) value!))
            {
                throw new ArgumentException(DictionaryUtilities.SR.Argument_AddingDuplicateWithKey.Format(key), nameof(key));
            }
        }

        public Boolean Add(TKey key, TValue value)
        {
            return Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            if (!Add(key, value))
            {
                throw new ArgumentException(DictionaryUtilities.SR.Argument_AddingDuplicateWithKey.Format(key), nameof(key));
            }
        }

        protected virtual unsafe Int32? Add(IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            if (items is null)
            {
                return null;
            }

            if (items.CountIfMaterialized() <= 0)
            {
                return 0;
            }
            
            Int32 count = 0;

            Exception? exception = Notify((Items: items, Count: new UnsafePointer<Int32>(&count)),
                static (_, modify, argument) =>
                {
                    ImmutableNullableDictionary<TKey, TValue> @new = modify.AddRange(argument.Items);
                    argument.Count.Value = @new.Count - modify.Count;
                    return @new;
                },
                static (_, @new, old, argument) => argument.Count > 0 ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, Source(@new.Except(old))) : null
            );

            return exception is null ? count : throw exception;
        }

        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            Add(items);
        }

        public virtual TValue GetOrAdd(TKey key, Func<TValue> factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            FlowResult<TValue> result = Notify((Key: key, Factory: factory),
                static (_, modify, argument) => modify.TryGetValue(argument.Key, out TValue? value) ? value.Result().Flow() : argument.Factory().Flow(),
                static (_, modify, argument, value) => modify.Add(argument.Key, value),
                static (_, _, _, argument, value) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey,TValue>(argument.Key, value))
            );

            return result.Exception is not { } exception ? result.Value : throw exception;
        }

        public virtual TValue GetOrAdd(TKey key, Func<TKey, TValue> factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            FlowResult<TValue> result = Notify((Key: key, Factory: factory),
                static (_, modify, argument) => modify.TryGetValue(argument.Key, out TValue? value) ? value.Result().Flow() : argument.Factory(argument.Key).Flow(),
                static (_, modify, argument, value) => modify.Add(argument.Key, value),
                static (_, _, _, argument, value) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey,TValue>(argument.Key, value))
            );

            return result.Exception is not { } exception ? result.Value : throw exception;
        }

        public override Boolean Remove(KeyValuePair<TKey, TValue> pair)
        {
            FlowResult<Boolean> result = Notify(pair,
                static (_, modify, pair) => modify.Contains(pair) ? true.Flow() : false.Result().Flow(),
                static (_, modify, pair, _) => modify.Remove(pair.Key),
                static (_, _, _, pair, _) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, pair)
            );

            return result.Exception is not { } exception ? result.Unwrap(out Boolean state) && state : throw exception;
        }

        void IDictionary.Remove(Object? key)
        {
            Remove((TKey) key!);
        }

        public Boolean Remove(TKey key)
        {
            return Remove(key, out _);
        }

        public virtual Boolean Remove(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            FlowResult<TValue> result = Notify(key,
                static (_, modify, key) => modify.TryGetValue(key, out TValue? value) ? value.Flow() : default,
                static (_, modify, key, _) => modify.Remove(key),
                static (_, _, _, key, value) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue>(key, value))
            );

            return result.Exception is not { } exception ? result.Unwrap(out value) : throw exception;
        }

        public virtual void RemoveRange(IEnumerable<TKey>? items)
        {
            if (items is null)
            {
                return;
            }

            if (items.CountIfMaterialized() <= 0)
            {
                return;
            }
            
            Exception? exception = Notify(items,
                static (_, modify, items) => modify.RemoveRange(items),
                static (_, @new, old, _) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, Source(@new.RemoveRange(old)))
            );

            if (exception is not null)
            {
                throw exception;
            }
        }

        public virtual void RemoveRange(IEnumerable<KeyValuePair<TKey, TValue>>? items)
        {
            if (items is null)
            {
                return;
            }

            if (items.CountIfMaterialized() <= 0)
            {
                return;
            }
            
            Exception? exception = Notify(items,
                static (_, modify, items) => modify.RemoveRange(items),
                static (_, @new, old, _) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, Source(@new.RemoveRange(old)))
            );

            if (exception is not null)
            {
                throw exception;
            }
        }

        protected override unsafe void Clear(out Int32 count)
        {
            Exception? exception;
            
            fixed (Int32* pointer = &count)
            {
                exception = Notify(new UnsafePointer<Int32>(pointer),
                    static (_, modify, pointer) =>
                    {
                        pointer.Value = modify.Count;
                        return modify.Clear();
                    },
                    static (_, _, old, pointer) => pointer.Value > 0 ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, Source(old), 0) : null
                );
            }

            if (exception is not null)
            {
                throw exception;
            }
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            ((ICollection) Immutable).CopyTo(array, index);
        }

        public override void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 index)
        {
            Immutable.CopyTo(array, index);
        }

        public ImmutableNullableDictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            return Immutable.GetEnumerator();
        }

        protected sealed override IEnumerator<KeyValuePair<TKey, TValue>> Enumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return new DictionaryEnumerator<TKey, TValue, ImmutableNullableDictionary<TKey, TValue>.Enumerator>(GetEnumerator());
        }

        Object? IDictionary.this[Object? key]
        {
            get
            {
                return this[(TKey) key!];
            }
            set
            {
                this[(TKey) key!] = (TValue) value!;
            }
        }

        public virtual TValue this[TKey key]
        {
            get
            {
                return Immutable[key];
            }
            set
            {
                Exception? exception = Notify(new KeyValuePair<TKey, TValue>(key, value),
                    static (_, modify, pair) => modify.ContainsKey(pair.Key),
                    (
                        static (_, modify, pair) => modify.SetItem(pair.Key, pair.Value),
                        static (_, _, old, pair) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, pair, new KeyValuePair<TKey, TValue>(pair.Key, old[pair.Key]))
                    ),
                    (
                        static (_, modify, pair) => modify.Add(pair.Key, pair.Value),
                        static (_, _, _, pair) => new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, pair)
                    )
                );
                
                if (exception is not null)
                {
                    throw exception;
                }
            }
        }
    }
}
