// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NetExtender.Types.Memento;
using NetExtender.Types.Memento.Interfaces;
using NetExtender.Types.Memento.Builder.Interfaces;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Memento
{
    public static class MementoUtilities
    {
        public static IEnumerable<T> Swap<T>(this IEnumerable<T> source) where T : IMementoItem
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            static void Internal(T item)
            {
                if (item.HasValue)
                {
                    item.Swap();
                }
            }

            return source.WhereNotNull().ForEach(Internal);
        }

        public static IEnumerable<T> Update<T>(this IEnumerable<T> source) where T : IMementoItem
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            static void Internal(T item)
            {
                item.Update();
            }

            return source.WhereNotNull().ForEach(Internal);
        }

        public static IMemento<TSource> Create<TSource>(this IMementoBuilder<TSource> builder) where TSource : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return new Memento<TSource>(builder);
        }

        public static Boolean Property<TSource, TProperty>(this IMemento<TSource> memento, TSource source, String name) where TSource : class
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MementoObject<TSource, TProperty> item = new MementoObject<TSource, TProperty>(source, name);
            return memento.Remember(item);
        }

        public static Boolean Property<TSource, TProperty>(this IMemento<TSource> memento, TSource source, String name, TProperty value) where TSource : class
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MementoObject<TSource, TProperty> item = new MementoObject<TSource, TProperty>(source, name, value);
            return memento.Remember(item);
        }

        public static Boolean Property<TSource, TProperty>(this IMemento<TSource> memento, TSource source, Expression<Func<TSource, TProperty>> expression) where TSource : class
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            MementoObject<TSource, TProperty> item = new MementoObject<TSource, TProperty>(source, expression);
            return memento.Remember(item);
        }

        public static Boolean Property<TSource, TProperty>(this IMemento<TSource> memento, TSource source, Expression<Func<TSource, TProperty>> expression, TProperty value) where TSource : class
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            MementoObject<TSource, TProperty> item = new MementoObject<TSource, TProperty>(source, expression, value);
            return memento.Remember(item);
        }

        public static Boolean Property<TSource, TProperty>(this IMemento<TSource> memento, TSource source, params ReflectionProperty<TSource, TProperty>[] properties) where TSource : class
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            static MementoObjectProperty<TSource, TProperty> Convert(ReflectionProperty<TSource, TProperty> property)
            {
                return new MementoObjectProperty<TSource, TProperty>(property);
            }

            IEnumerable<MementoObjectProperty<TSource, TProperty>> selector = properties.Select(Convert);
            MementoGroup<TSource> group = new MementoGroup<TSource>(source, selector);
            return memento.Remember(group);
        }

        public static Boolean Property<TSource, TProperty>(this IMemento<TSource> memento, TSource source, params KeyValuePair<ReflectionProperty<TSource, TProperty>, TProperty>[] properties) where TSource : class
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            static MementoObjectProperty<TSource, TProperty> Convert(KeyValuePair<ReflectionProperty<TSource, TProperty>, TProperty> property)
            {
                return new MementoObjectProperty<TSource, TProperty>(property.Key, property.Value);
            }

            IEnumerable<MementoObjectProperty<TSource, TProperty>> selector = properties.Select(Convert);
            MementoGroup<TSource> group = new MementoGroup<TSource>(source, selector);
            return memento.Remember(group);
        }

        public static Boolean Clone<TSource, TProperty>(this IMemento<TSource> memento, TSource source, String name) where TSource : class where TProperty : ICloneable
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MementoClone<TSource, TProperty> item = new MementoClone<TSource, TProperty>(source, name);
            return memento.Remember(item);
        }

        public static Boolean Clone<TSource, TProperty>(this IMemento<TSource> memento, TSource source, String name, TProperty value) where TSource : class where TProperty : ICloneable
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MementoClone<TSource, TProperty> item = new MementoClone<TSource, TProperty>(source, name, value);
            return memento.Remember(item);
        }

        public static Boolean Clone<TSource, TProperty>(this IMemento<TSource> memento, TSource source, Expression<Func<TSource, TProperty>> expression) where TSource : class where TProperty : ICloneable
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            MementoClone<TSource, TProperty> item = new MementoClone<TSource, TProperty>(source, expression);
            return memento.Remember(item);
        }

        public static Boolean Clone<TSource, TProperty>(this IMemento<TSource> memento, TSource source, Expression<Func<TSource, TProperty>> expression, TProperty value) where TSource : class where TProperty : ICloneable
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            MementoClone<TSource, TProperty> item = new MementoClone<TSource, TProperty>(source, expression, value);
            return memento.Remember(item);
        }

        public static Boolean Clone<TSource, TProperty>(this IMemento<TSource> memento, TSource source, params ReflectionProperty<TSource, TProperty>[] properties) where TSource : class where TProperty : ICloneable
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            static MementoCloneProperty<TSource, TProperty> Convert(ReflectionProperty<TSource, TProperty> property)
            {
                return new MementoCloneProperty<TSource, TProperty>(property);
            }

            IEnumerable<MementoCloneProperty<TSource, TProperty>> selector = properties.Select(Convert);
            MementoGroup<TSource> group = new MementoGroup<TSource>(source, selector);
            return memento.Remember(group);
        }

        public static Boolean Clone<TSource, TProperty>(this IMemento<TSource> memento, TSource source, params KeyValuePair<ReflectionProperty<TSource, TProperty>, TProperty>[] properties) where TSource : class where TProperty : ICloneable
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            static MementoCloneProperty<TSource, TProperty> Convert(KeyValuePair<ReflectionProperty<TSource, TProperty>, TProperty> property)
            {
                return new MementoCloneProperty<TSource, TProperty>(property.Key, property.Value);
            }

            IEnumerable<MementoCloneProperty<TSource, TProperty>> selector = properties.Select(Convert);
            MementoGroup<TSource> group = new MementoGroup<TSource>(source, selector);
            return memento.Remember(group);
        }

        public static Boolean Array<TSource, TProperty>(this IMemento<TSource> memento, TSource source, String name) where TSource : class
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MementoArray<TSource, TProperty> array = new MementoArray<TSource, TProperty>(source, name);
            return memento.Remember(array);
        }

        public static Boolean Array<TSource, TProperty>(this IMemento<TSource> memento, TSource source, String name, IEnumerable<TProperty>? values) where TSource : class
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MementoArray<TSource, TProperty> array = new MementoArray<TSource, TProperty>(source, name, values);
            return memento.Remember(array);
        }

        public static Boolean Array<TSource, TProperty>(this IMemento<TSource> memento, TSource source, Expression<Func<TSource, TProperty[]>> expression) where TSource : class
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            MementoArray<TSource, TProperty> array = new MementoArray<TSource, TProperty>(source, expression);
            return memento.Remember(array);
        }

        public static Boolean Array<TSource, TProperty>(this IMemento<TSource> memento, TSource source, Expression<Func<TSource, TProperty[]>> expression, IEnumerable<TProperty>? values) where TSource : class
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            MementoArray<TSource, TProperty> array = new MementoArray<TSource, TProperty>(source, expression, values);
            return memento.Remember(array);
        }

        public static Boolean Array<TSource, TProperty>(this IMemento<TSource> memento, TSource source, params ReflectionProperty<TSource, TProperty[]>[] properties) where TSource : class
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            static MementoArrayProperty<TSource, TProperty> Convert(ReflectionProperty<TSource, TProperty[]> property)
            {
                return new MementoArrayProperty<TSource, TProperty>(property);
            }

            IEnumerable<MementoArrayProperty<TSource, TProperty>> selector = properties.Select(Convert);
            MementoGroup<TSource> group = new MementoGroup<TSource>(source, selector);
            return memento.Remember(group);
        }

        public static Boolean Array<TSource, TProperty>(this IMemento<TSource> memento, TSource source, params KeyValuePair<ReflectionProperty<TSource, TProperty[]>, IEnumerable<TProperty>?>[] properties) where TSource : class
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            static MementoArrayProperty<TSource, TProperty> Convert(KeyValuePair<ReflectionProperty<TSource, TProperty[]>, IEnumerable<TProperty>?> property)
            {
                return new MementoArrayProperty<TSource, TProperty>(property.Key, property.Value);
            }

            IEnumerable<MementoArrayProperty<TSource, TProperty>> selector = properties.Select(Convert);
            MementoGroup<TSource> group = new MementoGroup<TSource>(source, selector);
            return memento.Remember(group);
        }

        public static Boolean Collection<TSource, TProperty, TCollection>(this IMemento<TSource> memento, TSource source, String name) where TSource : class where TCollection : class, ICollection<TProperty>
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MementoCollection<TSource, TProperty, TCollection> collection = new MementoCollection<TSource, TProperty, TCollection>(source, name);
            return memento.Remember(collection);
        }

        public static Boolean Collection<TSource, TProperty, TCollection>(this IMemento<TSource> memento, TSource source, String name, IEnumerable<TProperty>? values) where TSource : class where TCollection : class, ICollection<TProperty>
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MementoCollection<TSource, TProperty, TCollection> collection = new MementoCollection<TSource, TProperty, TCollection>(source, name, values);
            return memento.Remember(collection);
        }

        public static Boolean Collection<TSource, TProperty, TCollection>(this IMemento<TSource> memento, TSource source, Expression<Func<TSource, TCollection>> expression) where TSource : class where TCollection : class, ICollection<TProperty>
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            MementoCollection<TSource, TProperty, TCollection> collection = new MementoCollection<TSource, TProperty, TCollection>(source, expression);
            return memento.Remember(collection);
        }

        public static Boolean Collection<TSource, TProperty, TCollection>(this IMemento<TSource> memento, TSource source, Expression<Func<TSource, TCollection>> expression, IEnumerable<TProperty>? values) where TSource : class where TCollection : class, ICollection<TProperty>
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            MementoCollection<TSource, TProperty, TCollection> collection = new MementoCollection<TSource, TProperty, TCollection>(source, expression, values);
            return memento.Remember(collection);
        }

        public static Boolean Collection<TSource, TProperty, TCollection>(this IMemento<TSource> memento, TSource source, params ReflectionProperty<TSource, TCollection>[] properties) where TSource : class where TCollection : class, ICollection<TProperty>
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            static MementoCollectionProperty<TSource, TProperty, TCollection> Convert(ReflectionProperty<TSource, TCollection> property)
            {
                return new MementoCollectionProperty<TSource, TProperty, TCollection>(property);
            }

            IEnumerable<MementoCollectionProperty<TSource, TProperty, TCollection>> selector = properties.Select(Convert);
            MementoGroup<TSource> group = new MementoGroup<TSource>(source, selector);
            return memento.Remember(group);
        }

        public static Boolean Collection<TSource, TProperty, TCollection>(this IMemento<TSource> memento, TSource source, params KeyValuePair<ReflectionProperty<TSource, TCollection>, IEnumerable<TProperty>?>[] properties) where TSource : class where TCollection : class, ICollection<TProperty>
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            static MementoCollectionProperty<TSource, TProperty, TCollection> Convert(KeyValuePair<ReflectionProperty<TSource, TCollection>, IEnumerable<TProperty>?> property)
            {
                return new MementoCollectionProperty<TSource, TProperty, TCollection>(property.Key, property.Value);
            }

            IEnumerable<MementoCollectionProperty<TSource, TProperty, TCollection>> selector = properties.Select(Convert);
            MementoGroup<TSource> group = new MementoGroup<TSource>(source, selector);
            return memento.Remember(group);
        }
    }
}