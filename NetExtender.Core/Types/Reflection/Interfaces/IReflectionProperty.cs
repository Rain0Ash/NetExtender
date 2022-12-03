// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Reflection.Interfaces
{
    public interface IReflectionProperty<TSource, TProperty> : IReflectionProperty<TSource> where TSource : notnull
    {
        public new TProperty GetValue(in TSource source);
        public void SetValue(in TSource source, TProperty value);
    }
    
    public interface IReflectionProperty<TSource> : IReflectionProperty where TSource : notnull
    {
        public Object? GetValue(in TSource source);
        public void SetValue(in TSource source, Object? value);
    }
    
    public interface IReflectionProperty
    {
        public Type Source { get; }
        public Type Property { get; }
        
        public Object? GetValue(in Object source);
        public void SetValue(in Object source, Object? value);
    }
}