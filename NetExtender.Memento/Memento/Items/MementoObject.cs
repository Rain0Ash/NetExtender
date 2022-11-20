// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq.Expressions;
using NetExtender.Types.Reflection;

namespace NetExtender.Types.Memento
{
    public sealed class MementoObject<TSource, TProperty> : MementoItem<TSource, TProperty> where TSource : class
    {
        public MementoObject(TSource source, String name)
            : base(source, name is not null ? new MementoObjectProperty<TSource, TProperty>(name) : throw new ArgumentNullException(nameof(name)))
        {
        }

        public MementoObject(TSource source, String name, TProperty value)
            : base(source, name is not null ? new MementoObjectProperty<TSource, TProperty>(name, value) : throw new ArgumentNullException(nameof(name)))
        {
        }

        public MementoObject(TSource source, Expression<Func<TSource, TProperty>> expression)
            : base(source, expression is not null ? new MementoObjectProperty<TSource, TProperty>(expression) : throw new ArgumentNullException(nameof(expression)))
        {
        }

        public MementoObject(TSource source, Expression<Func<TSource, TProperty>> expression, TProperty value)
            : base(source, expression is not null ? new MementoObjectProperty<TSource, TProperty>(expression, value) : throw new ArgumentNullException(nameof(expression)))
        {
        }
        
        public MementoObject(TSource source, ReflectionProperty<TSource, TProperty> property)
            : base(source, new MementoObjectProperty<TSource, TProperty>(property))
        {
        }
        
        public MementoObject(TSource source, ReflectionProperty<TSource, TProperty> property, TProperty value)
            : base(source, new MementoObjectProperty<TSource, TProperty>(property, value))
        {
        }

        public override MementoObject<TSource, TProperty> Swap()
        {
            Property.Swap(Source);
            return this;
        }

        public override MementoObject<TSource, TProperty> Update()
        {
            Property.Update(Source);
            return this;
        }

        public override MementoObject<TSource, TProperty> With(TProperty value)
        {
            Property.With(value);
            return this;
        }
    }
}
