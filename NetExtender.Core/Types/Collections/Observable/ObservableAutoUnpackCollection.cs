// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using NetExtender.Types.Collections.Interfaces;
using NetExtender.Types.Sets;
using NetExtender.Types.Storages;
using NetExtender.Types.Storages.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Collections
{
    public class ObservableAutoUnpackCollection<T>
    {
    }
    
    //TODO:
    // Подумать над ячейками длин с индексами и обновлениям по ячейкам. Подумать над множественным вхождением
    internal class ObservableAutoUnpackCollectionAbstraction<T> : ItemObservableCollection<ObservableAutoUnpackCollectionAbstraction<T>.Unpack>, IItemObservableCollection<T>, IReadOnlyItemObservableCollection<T> where T : class?
    {
        protected SelectorCollectionWrapper<Unpack, T> Wrapper { get; }
        protected OrderedSet<Select>? Selectors { get; }
#pragma warning disable CS8634
        protected IStorage<T, Dictionary<Select, List<T>>> Storage { get; } = new WeakStorage<T, Dictionary<Select, List<T>>>();
        protected IStorage<T, Dictionary<Select, NotifyCollectionChangedEventHandler>> CollectionChangedEventHandlers { get; } = new WeakStorage<T, Dictionary<Select, NotifyCollectionChangedEventHandler>>();
        protected IStorage<T, Dictionary<Select, PropertyChangedEventHandler>> PropertyChangedEventHandlers { get; } = new WeakStorage<T, Dictionary<Select, PropertyChangedEventHandler>>();
#pragma warning restore CS8634
        
        public Int32 MaxDepth { get; init; } = 1;
        
        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return ((ICollection<Unpack>) this).IsReadOnly;
            }
        }
        
        public ObservableAutoUnpackCollectionAbstraction(params Expression<Func<T, IEnumerable<T>>>?[]? selectors)
        {
            Wrapper = new SelectorCollectionWrapper<Unpack, T>(this, static unpack => unpack.Item);
            Selectors = ToSelectors(selectors);
        }
        
        public ObservableAutoUnpackCollectionAbstraction(IEnumerable<T> collection, params Expression<Func<T, IEnumerable<T>>>?[]? selectors)
            : base(collection is not null ? Convert(collection) : throw new ArgumentNullException(nameof(collection)))
        {
            Wrapper = new SelectorCollectionWrapper<Unpack, T>(this, static unpack => unpack.Item);
            Selectors = ToSelectors(selectors);
        }
        
        public ObservableAutoUnpackCollectionAbstraction(List<T> list, params Expression<Func<T, IEnumerable<T>>>?[]? selectors)
            : base(list is not null ? Convert(list) : throw new ArgumentNullException(nameof(list)))
        {
            Wrapper = new SelectorCollectionWrapper<Unpack, T>(this, static unpack => unpack.Item);
            Selectors = ToSelectors(selectors);
        }
        
        [return: NotNullIfNotNull("collection")]
        protected static IEnumerable<Unpack>? Convert(IEnumerable<T>? collection)
        {
            if (collection is null)
            {
                return null;
            }
            
            return collection switch
            {
                ICollection<T> convert => new SelectorCollectionWrapper<T, Unpack>(convert, Unpack.Create),
                IReadOnlyCollection<T> convert => new ReadOnlySelectorCollectionWrapper<T, Unpack>(convert, Unpack.Create),
                _ => collection.Select(Unpack.Create)
            };
        }
        
        protected OrderedSet<Select>? ToSelectors(params Expression<Func<T, IEnumerable<T>>>?[]? selectors)
        {
            return selectors?.WhereNotNull().Distinct()
                .Select(static selector => new KeyValuePair<String, Expression<Func<T, IEnumerable<T>>>>(selector.GetMemberName(), selector))
                .DistinctBy(static selector => selector.Key, StringComparison.Ordinal)
                .Select(static selector => new Select(selector.Key, selector.Value.Compile())).ToOrderedSet();
        }

        // TODO:
        protected virtual void SubCollectionChanged(Object? sender, NotifyCollectionChangedEventArgs args)
        {
            if (sender is not ValueTuple<Object?, Unpack> tuple)
            {
                return;
            }
            
            (sender, Unpack item) = tuple;
            if (sender is not INotifyCollectionChanged || sender is not IEnumerable<T> source)
            {
                return;
            }
            
            Int32 index = base.IndexOf(item);
            
            sender.ToConsole();
            item.ToConsole();
            args.ToConsole();
        }
        
        protected virtual void SubCollectionPropertyChanged(Object? sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(Count) && args.PropertyName != nameof(Array.Length) || sender is not ValueTuple<Object?, T> tuple)
            {
                return;
            }
            
            (sender, T item) = tuple;
            if (sender is INotifyCollectionChanged || sender is not IEnumerable<T> source)
            {
                return;
            }
            
            sender.ToConsole();
            item.ToConsole();
            args.ToConsole();
        }

        protected override void Subscribe<TItem>(TItem? item) where TItem : default
        {
            base.Subscribe(item);
            
            if (item is not Unpack unpack || Selectors is not { Count: > 0 })
            {
                return;
            }
            
            Boolean recursive = unpack.Depth < MaxDepth;

            foreach (Select? selector in Selectors)
            {
                IEnumerable<T> source = selector.Selector(unpack);

                if (recursive)
                {
                    AddRange(source.Select(pack => new Unpack(pack) { Parent = unpack }));
                }

                switch (source)
                {
                    case INotifyCollectionChanged notify:
                    {
                        Dictionary<Select, NotifyCollectionChangedEventHandler> storage = CollectionChangedEventHandlers.GetOrAdd(unpack, () => new Dictionary<Select, NotifyCollectionChangedEventHandler>());
                        NotifyCollectionChangedEventHandler handler = storage.GetOrAdd(selector, key => (sender, args) => SubCollectionChanged((sender, unpack, key), args));

                        notify.CollectionChanged += handler;
                        continue;
                    }
                    case INotifyPropertyChanged notify:
                    {
                        Dictionary<Select, PropertyChangedEventHandler> storage = PropertyChangedEventHandlers.GetOrAdd(unpack, () => new Dictionary<Select, PropertyChangedEventHandler>());
                        PropertyChangedEventHandler handler = storage.GetOrAdd(selector, key => (sender, args) => SubCollectionPropertyChanged((sender, unpack, key), args));

                        static List<T> Factory((T, Select) value)
                        {
                            (T unpack, Select select) = value;
                            return new List<T>(select.Selector(unpack));
                        }

                        notify.PropertyChanged += handler;
                        Dictionary<Select, List<T>> dictionary = Storage.GetOrAdd(unpack, () => new Dictionary<Select, List<T>>());
                        dictionary.GetOrAdd(selector, key => Factory((unpack, key)));
                        continue;
                    }
                }
            }
        }

        //TODO:
        protected override void Unsubscribe<TItem>(TItem? item) where TItem : default
        {
            base.Unsubscribe(item);

            if (item is not Unpack unpack || Selectors is not { Count: > 0 })
            {
                return;
            }
        }

        public Boolean Contains(T item)
        {
            return base.Contains(item);
        }
        
        public Int32 IndexOf(T item)
        {
            return base.IndexOf(item);
        }
        
        public void Add(T item)
        {
            base.Add(item);
        }
        
        public void AddRange(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            base.AddRange(source.Select(Unpack.Create));
        }
        
        public void Insert(Int32 index, T item)
        {
            base.Insert(index, item);
        }
        
        public Boolean Remove(T item)
        {
            return base.Remove(item);
        }
        
        public void RemoveRange(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            base.RemoveRange(source.Select(Unpack.Create));
        }
        
        public void CopyTo(T[] array, Int32 index)
        {
            Wrapper.CopyTo(array, index);
        }
        
        public new IEnumerator<T> GetEnumerator()
        {
            return Wrapper.GetEnumerator();
        }
        
        public new T this[Int32 index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
            }
        }
        
        public sealed record Unpack : IEquatable<T>
        {
            [return: NotNullIfNotNull("value")]
            public static implicit operator T?(Unpack? value)
            {
                return value?.Item;
            }
            
            public static implicit operator Unpack(T value)
            {
                return new Unpack(value);
            }
            
            public Unpack? Parent { get; init; }
            public T Item { get; }
            
            public Int32 Depth
            {
                get
                {
                    Int32 i = 0;
                    Unpack? parent = Parent;
                    while ((parent = parent?.Parent) is not null)
                    {
                        ++i;
                    }
                    
                    return i;
                }
            }
            
            public Unpack(T item)
            {
                Item = item;
            }
            
            public static Unpack Create(T item)
            {
                return new Unpack(item);
            }
            
            public override Int32 GetHashCode()
            {
                return HashCode.Combine(Parent, Item, Depth);
            }
            
            public Boolean Equals(T? other)
            {
                return EqualityComparer<T>.Default.Equals(Item, other);
            }
            
            public Boolean Equals(Unpack? other)
            {
                if (ReferenceEquals(null, other))
                {
                    return false;
                }
                
                if (ReferenceEquals(this, other))
                {
                    return true;
                }
                
                return Equals(Parent, other.Parent) && Equals(other.Item) && Depth == other.Depth;
            }
            
            public override String? ToString()
            {
                return Item?.ToString();
            }
        }
        
        protected sealed class Select : IEquatable<String>
        {
            public String Name { get; }
            public Func<T, IEnumerable<T>> Selector { get; }
            
            public Select(String name, Func<T, IEnumerable<T>> selector)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Selector = selector ?? throw new ArgumentNullException(nameof(selector));
            }
            
            public override Int32 GetHashCode()
            {
                return Name.GetHashCode();
            }
            
            public override Boolean Equals(Object? other)
            {
                return other switch
                {
                    Select value => Equals(value),
                    String value => Equals(value),
                    _ => false
                };
            }
            
            public Boolean Equals(String? other)
            {
                return Name == other;
            }
            
            public Boolean Equals(Select? other)
            {
                return Equals(other?.Name);
            }
            
            public override String ToString()
            {
                return Name;
            }
        }
    }
}