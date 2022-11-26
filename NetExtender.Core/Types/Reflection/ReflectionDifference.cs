// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetExtender.Utilities.Core;

namespace NetExtender.Initializer.Types.Reflection
{
    public readonly struct ReflectionDifference : IReadOnlyCollection<MemberInfo>
    {
        public static implicit operator ReflectionDifference<ConstructorInfo>(ReflectionDifference value)
        {
            return value.Constructors;
        }

        public static implicit operator ReflectionDifferenceItem<ConstructorInfo>[](ReflectionDifference value)
        {
            return value.Constructors;
        }

        public static implicit operator ReflectionDifference<FieldInfo>(ReflectionDifference value)
        {
            return value.Fields;
        }

        public static implicit operator ReflectionDifferenceItem<FieldInfo>[](ReflectionDifference value)
        {
            return value.Fields;
        }

        public static implicit operator ReflectionDifference<PropertyInfo>(ReflectionDifference value)
        {
            return value.Properties;
        }

        public static implicit operator ReflectionDifferenceItem<PropertyInfo>[](ReflectionDifference value)
        {
            return value.Properties;
        }

        public static implicit operator ReflectionDifference<MethodInfo>(ReflectionDifference value)
        {
            return value.Methods;
        }

        public static implicit operator ReflectionDifferenceItem<MethodInfo>[](ReflectionDifference value)
        {
            return value.Methods;
        }

        public static implicit operator ReflectionDifference<EventInfo>(ReflectionDifference value)
        {
            return value.Events;
        }

        public static implicit operator ReflectionDifferenceItem<EventInfo>[](ReflectionDifference value)
        {
            return value.Events;
        }

        public ReflectionDifference<ConstructorInfo> Constructors { get; }
        public ReflectionDifference<FieldInfo> Fields { get; }
        public ReflectionDifference<PropertyInfo> Properties { get; }
        public ReflectionDifference<MethodInfo> Methods { get; }
        public ReflectionDifference<EventInfo> Events { get; }

        public Int32 Count
        {
            get
            {
                return Constructors.Count + Fields.Count + Properties.Count + Methods.Count + Events.Count;
            }
        }

        public ReflectionDifference(ReflectionDifference<ConstructorInfo> constructors, ReflectionDifference<FieldInfo> fields, ReflectionDifference<PropertyInfo> properties, ReflectionDifference<MethodInfo> methods, ReflectionDifference<EventInfo> events)
        {
            Constructors = constructors;
            Fields = fields;
            Properties = properties;
            Methods = methods;
            Events = events;
        }


        public IEnumerator<MemberInfo> GetEnumerator()
        {
            foreach (ReflectionDifferenceItem<ConstructorInfo> constructor in Constructors)
            {
                yield return constructor;
            }

            foreach (ReflectionDifferenceItem<FieldInfo> field in Fields)
            {
                yield return field;
            }

            foreach (ReflectionDifferenceItem<PropertyInfo> property in Properties)
            {
                yield return property;
            }

            foreach (ReflectionDifferenceItem<MethodInfo> method in Methods)
            {
                yield return method;
            }

            foreach (ReflectionDifferenceItem<EventInfo> @event in Events)
            {
                yield return @event;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public readonly struct ReflectionDifference<T> : IReadOnlyList<ReflectionDifferenceItem<T>>
    {
        public static implicit operator ReflectionEqualityType(ReflectionDifference<T> value)
        {
            return value.Equality;
        }

        public static implicit operator ReflectionDifferenceItem<T>[](ReflectionDifference<T> value)
        {
            return value.Difference;
        }

        public ReflectionEqualityType Equality { get; }

        private readonly ReflectionDifferenceItem<T>[] _difference;
        public ReflectionDifferenceItem<T>[] Difference
        {
            get
            {
                return _difference ?? Array.Empty<ReflectionDifferenceItem<T>>();
            }
            private init
            {
                _difference = value;
            }
        }

        public Int32 Count
        {
            get
            {
                return Difference.Length;
            }
        }

        public ReflectionDifference(ReflectionEqualityType equality, IEnumerable<ReflectionDifferenceItem<T>>? difference)
        {
            if (difference is null)
            {
                throw new ArgumentNullException(nameof(difference));
            }

            Equality = equality;
            _difference = difference.ToArray();
        }

        public void Deconstruct(out ReflectionEqualityType equality, out ReflectionDifferenceItem<T>[] difference)
        {
            equality = Equality;
            difference = Difference;
        }

        public IEnumerator<ReflectionDifferenceItem<T>> GetEnumerator()
        {
            return ((IEnumerable<ReflectionDifferenceItem<T>>) Difference).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ReflectionDifferenceItem<T> this[Int32 index]
        {
            get
            {
                return Difference[index];
            }
        }
    }

    public readonly struct ReflectionDifferenceItem<T>
    {
        public static implicit operator ReflectionEqualityType(ReflectionDifferenceItem<T> value)
        {
            return value.Equality;
        }

        public static implicit operator T(ReflectionDifferenceItem<T> value)
        {
            return value.Current;
        }

        public T Current { get; }
        public T Other { get; }
        public ReflectionEqualityType Equality { get; }

        public ReflectionDifferenceItem(T current, T other, ReflectionEqualityType equality)
        {
            Current = current ?? throw new ArgumentNullException(nameof(current));
            Other = other ?? throw new ArgumentNullException(nameof(other));
            Equality = equality;
        }

        public void Deconstruct(out T current, out T other)
        {
            Deconstruct(out current, out other, out _);
        }

        public void Deconstruct(out T current, out T other, out ReflectionEqualityType equality)
        {
            current = Current;
            other = Other;
            equality = Equality;
        }
    }
}