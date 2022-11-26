// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NetExtender.Types.Reflection;

namespace NetExtender.Types.Memento
{
    public sealed class MementoArray<TSource, TProperty> : MementoItem<TSource, TProperty[]> where TSource : class
    {
        public MementoArray(TSource source, String name)
            : base(source, name is not null ? new MementoArrayProperty<TSource, TProperty>(name) : throw new ArgumentNullException(nameof(name)))
        {
        }

        public MementoArray(TSource source, String name, IEnumerable<TProperty>? values)
            : base(source, name is not null ? new MementoArrayProperty<TSource, TProperty>(name, values) : throw new ArgumentNullException(nameof(name)))
        {
        }

        public MementoArray(TSource source, Expression<Func<TSource, TProperty[]>> expression)
            : base(source, expression is not null ? new MementoArrayProperty<TSource, TProperty>(expression) : throw new ArgumentNullException(nameof(expression)))
        {
        }

        public MementoArray(TSource source, Expression<Func<TSource, TProperty[]>> expression, IEnumerable<TProperty>? values)
            : base(source, expression is not null ? new MementoArrayProperty<TSource, TProperty>(expression, values) : throw new ArgumentNullException(nameof(expression)))
        {
        }

        public MementoArray(TSource source, ReflectionProperty<TSource, TProperty[]> property)
            : base(source, new MementoArrayProperty<TSource, TProperty>(property))
        {
        }

        public MementoArray(TSource source, ReflectionProperty<TSource, TProperty[]> property, IEnumerable<TProperty>? values)
            : base(source, new MementoArrayProperty<TSource, TProperty>(property, values))
        {
        }

        public override MementoArray<TSource, TProperty> Swap()
        {
            Property.Swap(Source);
            return this;
        }

        public override MementoArray<TSource, TProperty> Update()
        {
            Property.Update(Source);
            return this;
        }

        public MementoArray<TSource, TProperty> With(IEnumerable<TProperty>? values)
        {
            if (Property is not MementoArrayProperty<TSource, TProperty> property)
            {
                throw new InvalidOperationException();
            }

            property.With(values);
            return this;
        }

        public override MementoArray<TSource, TProperty> With(TProperty[] values)
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
