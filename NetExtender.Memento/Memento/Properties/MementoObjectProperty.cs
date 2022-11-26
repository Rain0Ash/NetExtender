// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq.Expressions;
using NetExtender.Types.Reflection;

namespace NetExtender.Types.Memento
{
    public sealed record MementoObjectProperty<TSource, TProperty> : MementoProperty<TSource, TProperty> where TSource : class
    {
        public MementoObjectProperty(String name)
            : this(name, default!)
        {
        }

        public MementoObjectProperty(String name, TProperty value)
            : base(name)
        {
            Value = value;
        }

        public MementoObjectProperty(Expression<Func<TSource, TProperty>> expression)
            : base(expression)
        {
        }

        public MementoObjectProperty(Expression<Func<TSource, TProperty>> expression, TProperty value)
            : base(expression)
        {
            Value = value;
        }

        public MementoObjectProperty(ReflectionProperty<TSource, TProperty> property)
            : base(property)
        {
        }

        public MementoObjectProperty(ReflectionProperty<TSource, TProperty> property, TProperty value)
            : base(property)
        {
            Value = value;
        }

        public override MementoObjectProperty<TSource, TProperty> New()
        {
            return new MementoObjectProperty<TSource, TProperty>(Property);
        }

        public override MementoObjectProperty<TSource, TProperty> New(TProperty value)
        {
            return new MementoObjectProperty<TSource, TProperty>(Property, value);
        }

        public override MementoObject<TSource, TProperty> New(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new MementoObject<TSource, TProperty>(source, Property);
        }

        public override MementoObjectProperty<TSource, TProperty> Item()
        {
            return HasValue ? new MementoObjectProperty<TSource, TProperty>(Property, Value) : New();
        }

        public override MementoObject<TSource, TProperty> Item(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return HasValue ? new MementoObject<TSource, TProperty>(source, Property, Value) : New(source);
        }

        public override MementoObjectProperty<TSource, TProperty> Swap(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!HasValue)
            {
                throw new InvalidOperationException();
            }

            TProperty value = Property.GetValue(source);
            Property.SetValue(source, Value);
            Value = value;
            return this;
        }

        public override MementoObjectProperty<TSource, TProperty> Update(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Value = Property.GetValue(source);
            return this;
        }

        public override MementoObjectProperty<TSource, TProperty> With(TProperty value)
        {
            Value = value;
            return this;
        }
    }
}