// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;

namespace NetExtender.Types.Memento
{
    public sealed class MementoGroup<TSource> : MementoItem<TSource>, ICollection<IMementoProperty<TSource>> where TSource : class
    {
        protected override MementoGroupProperty<TSource> Property { get; }

        public Int32 Count
        {
            get
            {
                return Property.Count;
            }
        }

        Boolean ICollection<IMementoProperty<TSource>>.IsReadOnly
        {
            get
            {
                return ((ICollection<IMementoProperty<TSource>>) Property).IsReadOnly;
            }
        }

        public MementoGroup(TSource source)
            : base(source)
        {
            Property = new MementoGroupProperty<TSource>();
        }

        public MementoGroup(TSource source, params IMementoProperty<TSource>[]? properties)
            : base(source)
        {
            Property = new MementoGroupProperty<TSource>(properties);
        }

        public MementoGroup(TSource source, IEnumerable<IMementoProperty<TSource>>? properties)
            : base(source)
        {
            Property = new MementoGroupProperty<TSource>(properties);
        }

        public Boolean Contains(IMementoProperty<TSource> property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return Property.Contains(property);
        }

        public Boolean Add(IMementoProperty<TSource> property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return Property.Add(property);
        }

        void ICollection<IMementoProperty<TSource>>.Add(IMementoProperty<TSource> item)
        {
            Add(item);
        }

        public void AddRange(params IMementoProperty<TSource>[] properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            Property.AddRange(properties);
        }

        public void AddRange(IEnumerable<IMementoProperty<TSource>> properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            Property.AddRange(properties);
        }

        public Boolean Remove(IMementoProperty<TSource> property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return Property.Remove(property);
        }

        public override MementoItem<TSource> Swap()
        {
            Property.Swap(Source);
            return this;
        }

        public override MementoItem<TSource> Update()
        {
            Property.Update(Source);
            return this;
        }

        public MementoGroup<TSource> Clear()
        {
            Property.Clear();
            return this;
        }

        void ICollection<IMementoProperty<TSource>>.Clear()
        {
            Clear();
        }

        void ICollection<IMementoProperty<TSource>>.CopyTo(IMementoProperty<TSource>[] array, Int32 arrayIndex)
        {
            ((ICollection<IMementoProperty<TSource>>) Property).CopyTo(array, arrayIndex);
        }

        public IEnumerator<IMementoProperty<TSource>> GetEnumerator()
        {
            return Property.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}