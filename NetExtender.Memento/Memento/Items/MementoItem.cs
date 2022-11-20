// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Memento
{
    public abstract class MementoItem<TSource, TProperty> : MementoItem<TSource>, IMementoItem<TSource, TProperty> where TSource : class
    {
        protected sealed override MementoProperty<TSource, TProperty> Property { get; }
        
        public TProperty Value
        {
            get
            {
                return Property.Value;
            }
        }

        protected MementoItem(TSource source, MementoProperty<TSource, TProperty> property)
            : base(source)
        {
            Property = property;
        }

        public abstract override MementoItem<TSource, TProperty> Swap();

        IMementoItem<TSource, TProperty> IMementoItem<TSource, TProperty>.Swap()
        {
            return Swap();
        }

        IMementoItem<TSource> IMementoItem<TSource>.Swap()
        {
            return Swap();
        }

        IMementoItem IMementoItem.Swap()
        {
            return Swap();
        }

        public abstract override MementoItem<TSource, TProperty> Update();
        
        IMementoItem<TSource, TProperty> IMementoItem<TSource, TProperty>.Update()
        {
            return Update();
        }
        
        IMementoItem<TSource> IMementoItem<TSource>.Update()
        {
            return Update();
        }
        
        IMementoItem IMementoItem.Update()
        {
            return Update();
        }

        public abstract MementoItem<TSource, TProperty> With(TProperty value);
        
        IMementoItem<TSource, TProperty> IMementoItem<TSource, TProperty>.With(TProperty value)
        {
            return With(value);
        }
    }

    public abstract class MementoItem<TSource> : IMementoItem<TSource> where TSource : class
    {
        public TSource Source { get; }
        protected abstract IMementoProperty<TSource> Property { get; }

        public Boolean HasValue
        {
            get
            {
                return Property.HasValue;
            }
        }

        protected MementoItem(TSource source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }
        
        public abstract MementoItem<TSource> Swap();

        IMementoItem<TSource> IMementoItem<TSource>.Swap()
        {
            return Swap();
        }

        IMementoItem IMementoItem.Swap()
        {
            return Swap();
        }

        public abstract MementoItem<TSource> Update();
        
        IMementoItem<TSource> IMementoItem<TSource>.Update()
        {
            return Update();
        }
        
        IMementoItem IMementoItem.Update()
        {
            return Update();
        }
    }
}
