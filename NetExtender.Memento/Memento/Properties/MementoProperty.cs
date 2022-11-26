// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq.Expressions;
using NetExtender.Types.Monads;
using NetExtender.Types.Reflection;

namespace NetExtender.Types.Memento
{
    public abstract record MementoProperty<TSource, TProperty> : IMementoProperty<TSource, TProperty> where TSource : class
    {
        private Maybe<TProperty> Internal { get; set; }

        public TProperty Value
        {
            get
            {
                return Internal.Value;
            }
            protected set
            {
                Internal = value;
            }
        }

        public Boolean HasValue
        {
            get
            {
                return Internal.HasValue;
            }
        }

        protected ReflectionProperty<TSource, TProperty> Property { get; }

        protected MementoProperty(String name)
        {
            Property = name is not null ? new ReflectionProperty<TSource, TProperty>(name) : throw new ArgumentNullException(nameof(name));
        }

        protected MementoProperty(Expression<Func<TSource, TProperty>> expression)
        {
            Property = expression is not null ? new ReflectionProperty<TSource, TProperty>(expression) : throw new ArgumentNullException(nameof(expression));
        }

        protected MementoProperty(ReflectionProperty<TSource, TProperty> property)
        {
            Property = property;
        }

        public abstract MementoProperty<TSource, TProperty> New();

        IMementoProperty<TSource> IMementoProperty<TSource>.New()
        {
            return New();
        }

        public abstract MementoProperty<TSource, TProperty> New(TProperty value);

        IMementoProperty<TSource, TProperty> IMementoProperty<TSource, TProperty>.New(TProperty value)
        {
            return New(value);
        }

        public abstract MementoItem<TSource, TProperty> New(TSource source);

        IMementoItem<TSource, TProperty> IMementoProperty<TSource, TProperty>.New(TSource source)
        {
            return New(source);
        }

        IMementoItem<TSource> IMementoProperty<TSource>.New(TSource source)
        {
            return New(source);
        }

        public abstract MementoProperty<TSource, TProperty> Item();

        IMementoProperty<TSource> IMementoProperty<TSource>.Item()
        {
            return Item();
        }

        public abstract MementoItem<TSource, TProperty> Item(TSource source);

        IMementoItem<TSource, TProperty> IMementoProperty<TSource, TProperty>.Item(TSource source)
        {
            return Item(source);
        }

        IMementoItem<TSource> IMementoProperty<TSource>.Item(TSource source)
        {
            return Item(source);
        }

        public abstract MementoProperty<TSource, TProperty> Swap(TSource source);

        IMementoProperty<TSource, TProperty> IMementoProperty<TSource, TProperty>.Swap(TSource source)
        {
            return Swap(source);
        }

        IMementoProperty<TSource> IMementoProperty<TSource>.Swap(TSource source)
        {
            return Swap(source);
        }

        public abstract MementoProperty<TSource, TProperty> Update(TSource source);

        IMementoProperty<TSource, TProperty> IMementoProperty<TSource, TProperty>.Update(TSource source)
        {
            return Update(source);
        }

        IMementoProperty<TSource> IMementoProperty<TSource>.Update(TSource source)
        {
            return Update(source);
        }

        public abstract MementoProperty<TSource, TProperty> With(TProperty value);

        IMementoProperty<TSource, TProperty> IMementoProperty<TSource, TProperty>.With(TProperty value)
        {
            return With(value);
        }

        public MementoProperty<TSource, TProperty> Clear()
        {
            Internal = new Maybe<TProperty>();
            return this;
        }

        IMementoProperty<TSource> IMementoProperty<TSource>.Clear()
        {
            return Clear();
        }
    }
}