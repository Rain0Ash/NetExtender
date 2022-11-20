// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NetExtender.Types.Reflection;

namespace NetExtender.Types.Memento
{
    public sealed class MementoCollection<TSource, TProperty, TCollection> : MementoItem<TSource, TCollection> where TSource : class where TCollection : class, ICollection<TProperty>
    {
        public MementoCollection(TSource source, String name)
            : base(source, name is not null ? new MementoCollectionProperty<TSource, TProperty, TCollection>(name) : throw new ArgumentNullException(nameof(name)))
        {
        }

        public MementoCollection(TSource source, String name, IEnumerable<TProperty>? values)
            : base(source, name is not null ? new MementoCollectionProperty<TSource, TProperty, TCollection>(name, values) : throw new ArgumentNullException(nameof(name)))
        {
        }

        public MementoCollection(TSource source, Expression<Func<TSource, TCollection>> expression)
            : base(source, expression is not null ? new MementoCollectionProperty<TSource, TProperty, TCollection>(expression) : throw new ArgumentNullException(nameof(expression)))
        {
        }

        public MementoCollection(TSource source, Expression<Func<TSource, TCollection>> expression, IEnumerable<TProperty>? values)
            : base(source, expression is not null ? new MementoCollectionProperty<TSource, TProperty, TCollection>(expression, values) : throw new ArgumentNullException(nameof(expression)))
        {
        }
        
        public MementoCollection(TSource source, ReflectionProperty<TSource, TCollection> property)
            : base(source, new MementoCollectionProperty<TSource, TProperty, TCollection>(property))
        {
        }
        
        public MementoCollection(TSource source, ReflectionProperty<TSource, TCollection> property, IEnumerable<TProperty>? values)
            : base(source, new MementoCollectionProperty<TSource, TProperty, TCollection>(property, values))
        {
        }
        
        public override MementoCollection<TSource, TProperty, TCollection> Swap()
        {
            Property.Swap(Source);
            return this;
        }

        public override MementoCollection<TSource, TProperty, TCollection> Update()
        {
            Property.Update(Source);
            return this;
        }

        public MementoCollection<TSource, TProperty, TCollection> With(IEnumerable<TProperty>? values)
        {
            if (Property is not MementoCollectionProperty<TSource, TProperty, TCollection> property)
            {
                throw new InvalidOperationException();
            }
            
            property.With(values);
            return this;
        }

        public override MementoCollection<TSource, TProperty, TCollection> With(TCollection values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            Property.With(values);
            return this;
        }
    }
}
