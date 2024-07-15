using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Interfaces.Notify;
using NetExtender.Types.Collections.Interfaces;
using NetExtender.Types.Handlers.Chain.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Collections
{
    public class ItemObservableChainHandlerValue<T> : ItemObservableChainHandlerValue<T, IChainHandler<T>>, IChainHandlerValue<T>
    {
        public ItemObservableChainHandlerValue(T value)
            : base(value)
        {
        }
        
        public ItemObservableChainHandlerValue(T value, Func<T, T> clone)
            : base(value, clone)
        {
        }
        
        public ItemObservableChainHandlerValue(T value, IEnumerable<IChainHandler<T>> collection)
            : base(value, collection)
        {
        }
        
        public ItemObservableChainHandlerValue(T value, Func<T, T> clone, IEnumerable<IChainHandler<T>> collection)
            : base(value, clone, collection)
        {
        }
        
        public ItemObservableChainHandlerValue(T value, List<IChainHandler<T>> list)
            : base(value, list)
        {
        }
        
        public ItemObservableChainHandlerValue(T value, Func<T, T> clone, List<IChainHandler<T>> list)
            : base(value, clone, list)
        {
        }
    }
    
    public class ItemObservableChainHandlerValue<T, THandler> : ItemObservableChainHandlerValue<T, THandler, ItemObservableCollection<THandler>> where THandler : IChainHandler<T>
    {
        public ItemObservableChainHandlerValue(T value)
            : base(value, new ItemObservableCollection<THandler>())
        {
        }
        
        public ItemObservableChainHandlerValue(T value, Func<T, T> clone)
            : base(value, clone, new ItemObservableCollection<THandler>())
        {
        }
        
        public ItemObservableChainHandlerValue(T value, IEnumerable<THandler> collection)
            : base(value, collection is not null ? new ItemObservableCollection<THandler>(collection) : throw new ArgumentNullException(nameof(collection)))
        {
        }
        
        public ItemObservableChainHandlerValue(T value, Func<T, T> clone, IEnumerable<THandler> collection)
            : base(value, clone, collection is not null ? new ItemObservableCollection<THandler>(collection) : throw new ArgumentNullException(nameof(collection)))
        {
        }
        
        public ItemObservableChainHandlerValue(T value, List<THandler> list)
            : base(value, list is not null ? new ItemObservableCollection<THandler>(list) : throw new ArgumentNullException(nameof(list)))
        {
        }
        
        public ItemObservableChainHandlerValue(T value, Func<T, T> clone, List<THandler> list)
            : base(value, clone, list is not null ? new ItemObservableCollection<THandler>(list) : throw new ArgumentNullException(nameof(list)))
        {
        }
    }
    
    public class SuppressObservableChainHandlerValue<T> : SuppressObservableChainHandlerValue<T, IChainHandler<T>>, IChainHandlerValue<T>
    {
        public SuppressObservableChainHandlerValue(T value)
            : base(value)
        {
        }
        
        public SuppressObservableChainHandlerValue(T value, Func<T, T> clone)
            : base(value, clone)
        {
        }
        
        public SuppressObservableChainHandlerValue(T value, IEnumerable<IChainHandler<T>> collection)
            : base(value, collection)
        {
        }
        
        public SuppressObservableChainHandlerValue(T value, Func<T, T> clone, IEnumerable<IChainHandler<T>> collection)
            : base(value, clone, collection)
        {
        }
        
        public SuppressObservableChainHandlerValue(T value, List<IChainHandler<T>> list)
            : base(value, list)
        {
        }
        
        public SuppressObservableChainHandlerValue(T value, Func<T, T> clone, List<IChainHandler<T>> list)
            : base(value, clone, list)
        {
        }
    }
    
    public class SuppressObservableChainHandlerValue<T, THandler> : SuppressObservableChainHandlerValue<T, THandler, SuppressObservableCollection<THandler>> where THandler : IChainHandler<T>
    {
        public SuppressObservableChainHandlerValue(T value)
            : base(value, new SuppressObservableCollection<THandler>())
        {
        }
        
        public SuppressObservableChainHandlerValue(T value, Func<T, T> clone)
            : base(value, clone, new SuppressObservableCollection<THandler>())
        {
        }
        
        public SuppressObservableChainHandlerValue(T value, IEnumerable<THandler> collection)
            : base(value, collection is not null ? new SuppressObservableCollection<THandler>(collection) : throw new ArgumentNullException(nameof(collection)))
        {
        }
        
        public SuppressObservableChainHandlerValue(T value, Func<T, T> clone, IEnumerable<THandler> collection)
            : base(value, clone, collection is not null ? new SuppressObservableCollection<THandler>(collection) : throw new ArgumentNullException(nameof(collection)))
        {
        }
        
        public SuppressObservableChainHandlerValue(T value, List<THandler> list)
            : base(value, list is not null ? new SuppressObservableCollection<THandler>(list) : throw new ArgumentNullException(nameof(list)))
        {
        }
        
        public SuppressObservableChainHandlerValue(T value, Func<T, T> clone, List<THandler> list)
            : base(value, clone, list is not null ? new SuppressObservableCollection<THandler>(list) : throw new ArgumentNullException(nameof(list)))
        {
        }
    }
    
    public abstract class ItemObservableChainHandlerValue<T, THandler, TCollection> : SuppressObservableChainHandlerValue<T, THandler, TCollection>, INotifyItemCollection where THandler : IChainHandler<T> where TCollection : class, ISuppressObservableCollection<THandler>, INotifyItemCollection
    {
        public event PropertyChangingEventHandler? ItemChanging;
        public event PropertyChangedEventHandler? ItemChanged;

        protected ItemObservableChainHandlerValue(T value, TCollection collection)
            : base(value, collection)
        {
            Internal.ItemChanging += OnItemChanging;
            Internal.ItemChanged += OnItemChanged;
        }

        protected ItemObservableChainHandlerValue(T value, Func<T, T> clone, TCollection collection)
            : base(value, clone, collection)
        {
            Internal.ItemChanging += OnItemChanging;
            Internal.ItemChanged += OnItemChanged;
        }
        
        private void OnItemChanging(Object? sender, PropertyChangingEventArgs args)
        {
            ItemChanging?.Invoke(this, args);
        }
        
        private void OnItemChanged(Object? sender, PropertyChangedEventArgs args)
        {
            ItemChanged?.Invoke(this, args);
            Reset();
        }
    }
    
    public abstract class SuppressObservableChainHandlerValue<T, THandler, TCollection> : ObservableChainHandlerValue<T, THandler, TCollection>, ISuppressChainHandlerValue<T, THandler> where THandler : IChainHandler<T> where TCollection : class, ISuppressObservableCollection<THandler>
    {
        public Boolean IsAllowSuppress
        {
            get
            {
                return Internal.IsAllowSuppress;
            }
        }
        
        public Boolean IsSuppressed
        {
            get
            {
                return Internal.IsSuppressed;
            }
        }
        
        public Int32 SuppressDepth
        {
            get
            {
                return Internal.SuppressDepth;
            }
        }
        
        public SuppressObservableChainHandlerValue(T value, TCollection collection)
            : base(value, collection)
        {
        }
        
        public SuppressObservableChainHandlerValue(T value, Func<T, T>? clone, TCollection collection)
            : base(value, clone, collection)
        {
        }
        
        public void Move(Int32 oldIndex, Int32 newIndex)
        {
            Internal.Move(oldIndex, newIndex);
        }
        
        public IDisposable? Suppress()
        {
            return Internal.Suppress();
        }
    }
    
    public abstract class ObservableChainHandlerValue<T, THandler, TCollection> : ObservableChainHandlerCollection<T, THandler, TCollection>, IChainHandlerValue<T, THandler> where THandler : IChainHandler<T> where TCollection : class, IObservableCollection<THandler>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator T?(ObservableChainHandlerValue<T, THandler, TCollection>? value)
        {
            return value is not null ? value.Value : default;
        }
        
        private T _initial;
        public T Initial
        {
            get
            {
                return _initial;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _initial, value);
                Reset();
            }
        }
        
        private Maybe<T> _value;
        public T Value
        {
            get
            {
                if (_value.HasValue)
                {
                    return _value.Value;
                }
                
                if (!Update(out T? result))
                {
                    throw new InvalidOperationException();
                }
                
                Value = result;
                return Value;
            }
            protected set
            {
                _value = value;
            }
        }

        protected Func<T, T> Clone { get; }
        
        protected ObservableChainHandlerValue(T value, TCollection collection)
            : this(value, null, collection)
        {
        }
        
        protected ObservableChainHandlerValue(T value, Func<T, T>? clone, TCollection collection)
            : base(collection)
        {
            _value = _initial = value;
            Clone = clone ?? GenericUtilities.MemberwiseClone;
            
            CollectionChanged += OnCollectionChanged;
        }
        
        private void OnCollectionChanged(Object? sender, NotifyCollectionChangedEventArgs args)
        {
            Reset();
        }
        
        public void Reset()
        {
            this.RaisePropertyChanging(nameof(Value));
            _value = default;
            this.RaisePropertyChanged(nameof(Value));
        }
        
        public Boolean Update()
        {
            if (!Update(out T? result))
            {
                return false;
            }
            
            Value = result;
            return true;
        }
        
        protected virtual Boolean Update([MaybeNullWhen(false)] out T result)
        {
            T clone = Clone(Initial);
            
            try
            {
                result = Handle(clone);
                return true;
            }
            catch (Exception exception)
            {
                switch (Handle(exception))
                {
                    case null:
                        result = default;
                        return false;
                    case true:
                        result = clone;
                        return true;
                    case false:
                        throw;
                }
            }
        }

        protected virtual Boolean? Handle(Exception? exception)
        {
            return exception is null;
        }
    }
}