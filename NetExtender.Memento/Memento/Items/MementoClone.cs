// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq.Expressions;
using NetExtender.Types.Reflection;

namespace NetExtender.Types.Memento
{
    public sealed class MementoClone<TSource, TProperty> : MementoItem<TSource, TProperty> where TSource : class where TProperty : ICloneable
    {
        public MementoClone(TSource source, String name)
            : base(source, name is not null ? new MementoCloneProperty<TSource, TProperty>(name) : throw new ArgumentNullException(nameof(name)))
        {
        }

        public MementoClone(TSource source, String name, TProperty value)
            : base(source, name is not null ? new MementoCloneProperty<TSource, TProperty>(name, value) : throw new ArgumentNullException(nameof(name)))
        {
        }

        public MementoClone(TSource source, Expression<Func<TSource, TProperty>> expression)
            : base(source, expression is not null ? new MementoCloneProperty<TSource, TProperty>(expression) : throw new ArgumentNullException(nameof(expression)))
        {
        }

        public MementoClone(TSource source, Expression<Func<TSource, TProperty>> expression, TProperty value)
            : base(source, expression is not null ? new MementoCloneProperty<TSource, TProperty>(expression, value) : throw new ArgumentNullException(nameof(expression)))
        {
        }
        
        public MementoClone(TSource source, ReflectionProperty<TSource, TProperty> property)
            : base(source, new MementoCloneProperty<TSource, TProperty>(property))
        {
        }
        
        public MementoClone(TSource source, ReflectionProperty<TSource, TProperty> property, TProperty value)
            : base(source, new MementoCloneProperty<TSource, TProperty>(property, value))
        {
        }

        public override MementoClone<TSource, TProperty> Swap()
        {
            Property.Swap(Source);
            return this;
        }

        public override MementoClone<TSource, TProperty> Update()
        {
            Property.Update(Source);
            return this;
        }

        public override MementoClone<TSource, TProperty> With(TProperty value)
        {
            Property.With(value);
            return this;
        }
    }
}
