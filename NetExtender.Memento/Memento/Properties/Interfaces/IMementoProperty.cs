// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Memento
{
    public interface IMementoProperty<TSource, TProperty> : IMementoProperty<TSource> where TSource : class
    {
        public TProperty Value { get; }

        public IMementoProperty<TSource, TProperty> New(TProperty value);
        public new IMementoItem<TSource, TProperty> New(TSource source);
        public new IMementoItem<TSource, TProperty> Item(TSource source);
        public new IMementoProperty<TSource, TProperty> Swap(TSource source);
        public new IMementoProperty<TSource, TProperty> Update(TSource source);
        public IMementoProperty<TSource, TProperty> With(TProperty value);
    }

    public interface IMementoProperty<TSource> where TSource : class
    {
        public Boolean HasValue { get; }

        public IMementoProperty<TSource> New();
        public IMementoItem<TSource> New(TSource source);
        public IMementoProperty<TSource> Item();
        public IMementoItem<TSource> Item(TSource source);
        public IMementoProperty<TSource> Swap(TSource source);
        public IMementoProperty<TSource> Update(TSource source);
        public IMementoProperty<TSource> Clear();
    }
}