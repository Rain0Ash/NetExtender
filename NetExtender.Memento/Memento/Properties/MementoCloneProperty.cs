// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq.Expressions;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Memento
{
    public sealed record MementoCloneProperty<TSource, TProperty> : MementoProperty<TSource, TProperty> where TSource : class where TProperty : ICloneable
    {
        public MementoCloneProperty(String name)
            : this(name, default!)
        {
        }

        public MementoCloneProperty(String name, TProperty? value)
            : base(name)
        {
            Value = value is not null ? (TProperty) value.Clone() : default!;
        }

        public MementoCloneProperty(Expression<Func<TSource, TProperty>> expression)
            : base(expression)
        {
        }

        public MementoCloneProperty(Expression<Func<TSource, TProperty>> expression, TProperty? value)
            : base(expression)
        {
            Value = value is not null ? (TProperty) value.Clone() : default!;
        }

        public MementoCloneProperty(ReflectionProperty<TSource, TProperty> property)
            : base(property)
        {
        }

        public MementoCloneProperty(ReflectionProperty<TSource, TProperty> property, TProperty? value)
            : base(property)
        {
            Value = value is not null ? (TProperty) value.Clone() : default!;
        }

        public override MementoCloneProperty<TSource, TProperty> New()
        {
            return new MementoCloneProperty<TSource, TProperty>(Property);
        }

        public override MementoCloneProperty<TSource, TProperty> New(TProperty value)
        {
            return new MementoCloneProperty<TSource, TProperty>(Property, value);
        }

        public override MementoClone<TSource, TProperty> New(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return new MementoClone<TSource, TProperty>(source, Property);
        }

        public override MementoCloneProperty<TSource, TProperty> Item()
        {
            return HasValue ? new MementoCloneProperty<TSource, TProperty>(Property, Value) : New();
        }

        public override MementoClone<TSource, TProperty> Item(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return HasValue ? new MementoClone<TSource, TProperty>(source, Property, Value) : New(source);
        }

        public override MementoCloneProperty<TSource, TProperty> Swap(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!HasValue)
            {
                throw new InvalidOperationException();
            }

            TProperty value = GenericUtilities.Clone(Property.GetValue(source));
            Property.SetValue(source, Value);
            Value = value;
            return this;
        }

        public override MementoCloneProperty<TSource, TProperty> Update(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            Value = GenericUtilities.Clone(Property.GetValue(source));
            return this;
        }

        public override MementoCloneProperty<TSource, TProperty> With(TProperty value)
        {
            Value = GenericUtilities.Clone(value);
            return this;
        }
    }
}