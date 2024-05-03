// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Anonymous
{
    public record AnonymousTypeProperties : IReadOnlyList<AnonymousTypeProperty>
    {
        public Type Type { get; }
        public ImmutableArray<AnonymousTypeProperty> Properties { get; }

        public Int32 Count
        {
            get
            {
                return Properties.Length;
            }
        }

        private AnonymousTypeProperties(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsAnonymousType())
            {
                throw new NotSupportedException();
            }

            Type = type;
            Properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(property => new AnonymousTypeProperty(type, property)).ToImmutableArray();
        }

        public static AnonymousTypeProperties Create(Type type)
        {
            return new AnonymousTypeProperties(type);
        }

        public ImmutableArray<AnonymousTypeProperty>.Enumerator GetEnumerator()
        {
            return Properties.GetEnumerator();
        }

        IEnumerator<AnonymousTypeProperty> IEnumerable<AnonymousTypeProperty>.GetEnumerator()
        {
            return ((IEnumerable<AnonymousTypeProperty>) Properties).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<AnonymousTypeProperty>) this).GetEnumerator();
        }

        public AnonymousTypeProperty this[Int32 index]
        {
            get
            {
                return Properties[index];
            }
        }
    }

    public record AnonymousTypeProperty
    {
        public Type Type { get; }
        public Type Underlying { get; }
        public PropertyInfo Property { get; }
        public Func<Object, Object?> Getter { get; }

        public AnonymousTypeProperty(Type type, PropertyInfo property)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsAnonymousType())
            {
                throw new NotSupportedException();
            }

            Type = type;
            Property = property ?? throw new ArgumentNullException(nameof(property));
            Underlying = property.PropertyType;
            Getter = property.CreateGetExpression<Object, Object>().Compile();
        }

        public Object? Invoke(Object value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Getter(value);
        }
    }
}