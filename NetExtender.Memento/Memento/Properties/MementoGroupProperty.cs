// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Sets;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Memento
{
    public record MementoGroupProperty<TSource> : IMementoProperty<TSource>, ICollection<IMementoProperty<TSource>> where TSource : class
    {
        private OrderedSet<IMementoProperty<TSource>> Internal { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        Boolean ICollection<IMementoProperty<TSource>>.IsReadOnly
        {
            get
            {
                return ((ICollection<IMementoProperty<TSource>>) Internal).IsReadOnly;
            }
        }

        public Boolean HasValue
        {
            get
            {
                return Internal.Any(item => item.HasValue);
            }
        }

        public MementoGroupProperty()
            : this((IEnumerable<IMementoProperty<TSource>>?) null)
        {
        }

        public MementoGroupProperty(params IMementoProperty<TSource>[]? properties)
            : this((IEnumerable<IMementoProperty<TSource>>?) properties)
        {
        }

        public MementoGroupProperty(IEnumerable<IMementoProperty<TSource>>? properties)
        {
            Internal = properties is not null ? new OrderedSet<IMementoProperty<TSource>>(properties) : new OrderedSet<IMementoProperty<TSource>>();
        }

        public Boolean Contains(IMementoProperty<TSource> property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return Internal.Contains(property);
        }

        public Boolean Add(IMementoProperty<TSource> property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return Internal.Add(property);
        }

        void ICollection<IMementoProperty<TSource>>.Add(IMementoProperty<TSource> item)
        {
            Add(item);
        }

        public void AddRange(params IMementoProperty<TSource>[] properties)
        {
            AddRange((IEnumerable<IMementoProperty<TSource>>) properties);
        }

        public void AddRange(IEnumerable<IMementoProperty<TSource>> properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            Internal.AddRange(properties.WhereNotNull());
        }

        public Boolean Remove(IMementoProperty<TSource> property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return Internal.Remove(property);
        }

        public MementoGroupProperty<TSource> New()
        {
            IEnumerable<IMementoProperty<TSource>> selector = this.Select(property => property.New());
            return new MementoGroupProperty<TSource>(selector);
        }

        IMementoProperty<TSource> IMementoProperty<TSource>.New()
        {
            return New();
        }

        public MementoGroup<TSource> New(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IEnumerable<IMementoProperty<TSource>> selector = this.Select(property => property.New());
            return new MementoGroup<TSource>(source, selector);
        }

        IMementoItem<TSource> IMementoProperty<TSource>.New(TSource source)
        {
            return New(source);
        }

        public MementoGroupProperty<TSource> Item()
        {
            IEnumerable<IMementoProperty<TSource>> selector = this.Select(property => property.Item());
            return new MementoGroupProperty<TSource>(selector);
        }

        IMementoProperty<TSource> IMementoProperty<TSource>.Item()
        {
            return Item();
        }

        public MementoGroup<TSource> Item(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IEnumerable<IMementoProperty<TSource>> selector = this.Select(property => property.Item());
            return new MementoGroup<TSource>(source, selector);
        }

        IMementoItem<TSource> IMementoProperty<TSource>.Item(TSource source)
        {
            return Item(source);
        }

        public MementoGroupProperty<TSource> Swap(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (IMementoProperty<TSource> property in Internal)
            {
                property.Swap(source);
            }

            return this;
        }

        IMementoProperty<TSource> IMementoProperty<TSource>.Swap(TSource source)
        {
            return Swap(source);
        }

        public MementoGroupProperty<TSource> Update(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (IMementoProperty<TSource> property in Internal)
            {
                property.Update(source);
            }

            return this;
        }

        IMementoProperty<TSource> IMementoProperty<TSource>.Update(TSource source)
        {
            return Update(source);
        }

        public MementoGroupProperty<TSource> Clear()
        {
            Internal.Clear();
            return this;
        }

        IMementoProperty<TSource> IMementoProperty<TSource>.Clear()
        {
            return Clear();
        }

        void ICollection<IMementoProperty<TSource>>.Clear()
        {
            Clear();
        }

        void ICollection<IMementoProperty<TSource>>.CopyTo(IMementoProperty<TSource>[] array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }

        public IEnumerator<IMementoProperty<TSource>> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}