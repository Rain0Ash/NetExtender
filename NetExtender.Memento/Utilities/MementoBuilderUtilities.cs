// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NetExtender.Types.Memento;
using NetExtender.Types.Memento.Builder.Interfaces;
using NetExtender.Types.Reflection;

namespace NetExtender.Utilities.Memento
{
    public static class MementoBuilderUtilities
    {
        public static IMementoBuilder<TSource> Property<TSource, TProperty>(this IMementoBuilder<TSource> builder, String name) where TSource : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MementoObjectProperty<TSource, TProperty> property = new MementoObjectProperty<TSource, TProperty>(name);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Property<TSource, TProperty>(this IMementoBuilder<TSource> builder, String name, TProperty value) where TSource : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MementoObjectProperty<TSource, TProperty> property = new MementoObjectProperty<TSource, TProperty>(name, value);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Property<TSource, TProperty>(this IMementoBuilder<TSource> builder, Expression<Func<TSource, TProperty>> expression) where TSource : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            MementoObjectProperty<TSource, TProperty> property = new MementoObjectProperty<TSource, TProperty>(expression);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Property<TSource, TProperty>(this IMementoBuilder<TSource> builder, Expression<Func<TSource, TProperty>> expression, TProperty value) where TSource : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            MementoObjectProperty<TSource, TProperty> property = new MementoObjectProperty<TSource, TProperty>(expression, value);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Property<TSource, TProperty>(this IMementoBuilder<TSource> builder, params ReflectionProperty<TSource, TProperty>[] properties) where TSource : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
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
            MementoGroupProperty<TSource> property = new MementoGroupProperty<TSource>(selector);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Property<TSource, TProperty>(this IMementoBuilder<TSource> builder, params KeyValuePair<ReflectionProperty<TSource, TProperty>, TProperty>[] properties) where TSource : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
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
            MementoGroupProperty<TSource> property = new MementoGroupProperty<TSource>(selector);
            builder.Remember(property);
            return builder;
        }
        
        public static IMementoBuilder<TSource> Clone<TSource, TProperty>(this IMementoBuilder<TSource> builder, String name) where TSource : class where TProperty : ICloneable
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MementoCloneProperty<TSource, TProperty> property = new MementoCloneProperty<TSource, TProperty>(name);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Clone<TSource, TProperty>(this IMementoBuilder<TSource> builder, String name, TProperty value) where TSource : class where TProperty : ICloneable
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MementoCloneProperty<TSource, TProperty> property = new MementoCloneProperty<TSource, TProperty>(name, value);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Clone<TSource, TProperty>(this IMementoBuilder<TSource> builder, Expression<Func<TSource, TProperty>> expression) where TSource : class where TProperty : ICloneable
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            MementoCloneProperty<TSource, TProperty> property = new MementoCloneProperty<TSource, TProperty>(expression);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Clone<TSource, TProperty>(this IMementoBuilder<TSource> builder, Expression<Func<TSource, TProperty>> expression, TProperty value) where TSource : class where TProperty : ICloneable
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            MementoCloneProperty<TSource, TProperty> property = new MementoCloneProperty<TSource, TProperty>(expression, value);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Clone<TSource, TProperty>(this IMementoBuilder<TSource> builder, params ReflectionProperty<TSource, TProperty>[] properties) where TSource : class where TProperty : ICloneable
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
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
            MementoGroupProperty<TSource> property = new MementoGroupProperty<TSource>(selector);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Clone<TSource, TProperty>(this IMementoBuilder<TSource> builder, params KeyValuePair<ReflectionProperty<TSource, TProperty>, TProperty>[] properties) where TSource : class where TProperty : ICloneable
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
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
            MementoGroupProperty<TSource> property = new MementoGroupProperty<TSource>(selector);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Array<TSource, TProperty>(this IMementoBuilder<TSource> builder, String name) where TSource : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MementoArrayProperty<TSource, TProperty> property = new MementoArrayProperty<TSource, TProperty>(name);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Array<TSource, TProperty>(this IMementoBuilder<TSource> builder, String name, IEnumerable<TProperty>? values) where TSource : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MementoArrayProperty<TSource, TProperty> property = new MementoArrayProperty<TSource, TProperty>(name, values);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Array<TSource, TProperty>(this IMementoBuilder<TSource> builder, Expression<Func<TSource, TProperty[]>> expression) where TSource : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            MementoArrayProperty<TSource, TProperty> property = new MementoArrayProperty<TSource, TProperty>(expression);
            builder.Remember(property);
            return builder;
        }
        
        public static IMementoBuilder<TSource> Array<TSource, TProperty>(this IMementoBuilder<TSource> builder, Expression<Func<TSource, TProperty[]>> expression, IEnumerable<TProperty>? values) where TSource : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            MementoArrayProperty<TSource, TProperty> property = new MementoArrayProperty<TSource, TProperty>(expression, values);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Array<TSource, TProperty>(this IMementoBuilder<TSource> builder, params ReflectionProperty<TSource, TProperty[]>[] properties) where TSource : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
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
            MementoGroupProperty<TSource> property = new MementoGroupProperty<TSource>(selector);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Array<TSource, TProperty>(this IMementoBuilder<TSource> builder, params KeyValuePair<ReflectionProperty<TSource, TProperty[]>, IEnumerable<TProperty>?>[] properties) where TSource : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
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
            MementoGroupProperty<TSource> property = new MementoGroupProperty<TSource>(selector);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Collection<TSource, TProperty, TCollection>(this IMementoBuilder<TSource> builder, String name) where TSource : class where TCollection : class, ICollection<TProperty>
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MementoCollectionProperty<TSource, TProperty, TCollection> property = new MementoCollectionProperty<TSource, TProperty, TCollection>(name);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Collection<TSource, TProperty, TCollection>(this IMementoBuilder<TSource> builder, String name, IEnumerable<TProperty>? values) where TSource : class where TCollection : class, ICollection<TProperty>
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            MementoCollectionProperty<TSource, TProperty, TCollection> property = new MementoCollectionProperty<TSource, TProperty, TCollection>(name, values);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Collection<TSource, TProperty, TCollection>(this IMementoBuilder<TSource> builder, Expression<Func<TSource, TCollection>> expression) where TSource : class where TCollection : class, ICollection<TProperty>
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            MementoCollectionProperty<TSource, TProperty, TCollection> property = new MementoCollectionProperty<TSource, TProperty, TCollection>(expression);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Collection<TSource, TProperty, TCollection>(this IMementoBuilder<TSource> builder, Expression<Func<TSource, TCollection>> expression, IEnumerable<TProperty>? values) where TSource : class where TCollection : class, ICollection<TProperty>
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            MementoCollectionProperty<TSource, TProperty, TCollection> property = new MementoCollectionProperty<TSource, TProperty, TCollection>(expression, values);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Collection<TSource, TProperty, TCollection>(this IMementoBuilder<TSource> builder, params ReflectionProperty<TSource, TCollection>[] properties) where TSource : class where TCollection : class, ICollection<TProperty>
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
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
            MementoGroupProperty<TSource> property = new MementoGroupProperty<TSource>(selector);
            builder.Remember(property);
            return builder;
        }

        public static IMementoBuilder<TSource> Collection<TSource, TProperty, TCollection>(this IMementoBuilder<TSource> builder, params KeyValuePair<ReflectionProperty<TSource, TCollection>, IEnumerable<TProperty>?>[] properties) where TSource : class where TCollection : class, ICollection<TProperty>
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
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
            MementoGroupProperty<TSource> property = new MementoGroupProperty<TSource>(selector);
            builder.Remember(property);
            return builder;
        }
    }
}