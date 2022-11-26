// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Memento
{
    public sealed record MementoArrayProperty<TSource, TProperty> : MementoProperty<TSource, TProperty[]> where TSource : class
    {
        public MementoArrayProperty(String name)
            : base(name)
        {
        }

        public MementoArrayProperty(String name, IEnumerable<TProperty>? values)
            : base(name)
        {
            Value = values?.ToArray() ?? Array.Empty<TProperty>();
        }

        public MementoArrayProperty(Expression<Func<TSource, TProperty[]>> expression)
            : base(expression)
        {
        }

        public MementoArrayProperty(Expression<Func<TSource, TProperty[]>> expression, IEnumerable<TProperty>? values)
            : base(expression)
        {
            Value = values?.ToArray() ?? Array.Empty<TProperty>();
        }

        public MementoArrayProperty(ReflectionProperty<TSource, TProperty[]> property)
            : base(property)
        {
        }

        public MementoArrayProperty(ReflectionProperty<TSource, TProperty[]> property, IEnumerable<TProperty>? values)
            : base(property)
        {
            Value = values?.ToArray() ?? Array.Empty<TProperty>();
        }

        public override MementoArrayProperty<TSource, TProperty> New()
        {
            return new MementoArrayProperty<TSource, TProperty>(Property);
        }

        public MementoArrayProperty<TSource, TProperty> New(IEnumerable<TProperty>? value)
        {
            return new MementoArrayProperty<TSource, TProperty>(Property, value);
        }

        public override MementoArrayProperty<TSource, TProperty> New(TProperty[] value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return New(value);
        }

        public override MementoArray<TSource, TProperty> New(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new MementoArray<TSource, TProperty>(source, Property);
        }

        public override MementoArrayProperty<TSource, TProperty> Item()
        {
            return HasValue ? new MementoArrayProperty<TSource, TProperty>(Property, Value) : New();
        }

        public override MementoArray<TSource, TProperty> Item(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return HasValue ? new MementoArray<TSource, TProperty>(source, Property, Value) : New(source);
        }

        public override MementoArrayProperty<TSource, TProperty> Swap(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TProperty[] value = GenericUtilities.Clone(Property.GetValue(source));
            Property.SetValue(source, Value);
            Value = value;
            return this;
        }

        public override MementoArrayProperty<TSource, TProperty> Update(TSource source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Value = GenericUtilities.Clone(Property.GetValue(source));
            return this;
        }

        public MementoArrayProperty<TSource, TProperty> With(IEnumerable<TProperty>? values)
        {
            Value = values?.ToArray() ?? Array.Empty<TProperty>();
            return this;
        }

        public override MementoArrayProperty<TSource, TProperty> With(TProperty[] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return With(values);
        }
    }
}