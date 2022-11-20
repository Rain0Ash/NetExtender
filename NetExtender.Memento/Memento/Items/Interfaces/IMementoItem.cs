// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Memento
{
    public interface IMementoItem<out TSource, TProperty> : IMementoItem<TSource> where TSource : class
    {
        public TProperty Value { get; }

        public new IMementoItem<TSource, TProperty> Swap();
        public new IMementoItem<TSource, TProperty> Update();
        public IMementoItem<TSource, TProperty> With(TProperty value);
    }

    public interface IMementoItem<out TSource> : IMementoItem where TSource : class
    {
        public TSource Source { get; }
        
        public new IMementoItem<TSource> Swap();
        public new IMementoItem<TSource> Update();
    }

    public interface IMementoItem
    {
        public Boolean HasValue { get; }
        
        public IMementoItem Swap();
        public IMementoItem Update();
    }
}