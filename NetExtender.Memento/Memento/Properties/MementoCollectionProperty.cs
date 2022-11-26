// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Memento
{
    public sealed record MementoCollectionProperty<TSource, TProperty, TCollection> : MementoProperty<TSource, TCollection> where TSource : class where TCollection : class, ICollection<TProperty>
    {
        private List<TProperty> Internal { get; }

        public MementoCollectionProperty(String name)
            : this(name, null)
        {
        }

        public MementoCollectionProperty(String name, IEnumerable<TProperty>? values)
            : base(name)
        {
            Internal = values is not null ? new List<TProperty>(values) : new List<TProperty>();
        }

        public MementoCollectionProperty(Expression<Func<TSource, TCollection>> expression)
            : this(expression, null)
        {
        }

        public MementoCollectionProperty(Expression<Func<TSource, TCollection>> expression, IEnumerable<TProperty>? values)
            : base(expression)
        {
            Internal = values is not null ? new List<TProperty>(values) : new List<TProperty>();
        }

        public MementoCollectionProperty(ReflectionProperty<TSource, TCollection> property)
            : this(property, null)
        {
        }

        public MementoCollectionProperty(ReflectionProperty<TSource, TCollection> property, IEnumerable<TProperty>? values)
            : base(property)
        {
            Internal = values is not null ? new List<TProperty>(values) : new List<TProperty>();
        }

        public override MementoCollectionProperty<TSource, TProperty, TCollection> New()
        {
            return new MementoCollectionProperty<TSource, TProperty, TCollection>(Property);
        }

        public MementoCollectionProperty<TSource, TProperty, TCollection> New(IEnumerable<TProperty>? value)
        {
            return new MementoCollectionProperty<TSource, TProperty, TCollection>(Property, value);
        }

        public override MementoCollectionProperty<TSource, TProperty, TCollection> New(TCollection value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return New(value);
        }

        public override MementoCollection<TSource, TProperty, TCollection> New(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new MementoCollection<TSource, TProperty, TCollection>(source, Property);
        }

        public override MementoCollectionProperty<TSource, TProperty, TCollection> Item()
        {
            return HasValue ? new MementoCollectionProperty<TSource, TProperty, TCollection>(Property, Value) : New();
        }

        public override MementoCollection<TSource, TProperty, TCollection> Item(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return HasValue ? new MementoCollection<TSource, TProperty, TCollection>(source, Property, Value) : New(source);
        }

        public override MementoCollectionProperty<TSource, TProperty, TCollection> Swap(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TProperty[] array = new TProperty[Value.Count];
            Value.CopyTo(array, 0);

            Value.Clear();
            Value.AddRange(Internal);

            Internal.Clear();
            Internal.AddRange(array);

            return this;
        }

        public override MementoCollectionProperty<TSource, TProperty, TCollection> Update(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Internal.Clear();
            Internal.AddRange(Value);

            return this;
        }

        public MementoCollectionProperty<TSource, TProperty, TCollection> With(IEnumerable<TProperty>? values)
        {
            Internal.Clear();

            if (values is not null)
            {
                Internal.AddRange(values);
            }

            return this;
        }

        public override MementoCollectionProperty<TSource, TProperty, TCollection> With(TCollection values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return With(values);
        }
    }
}