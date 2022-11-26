// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Reflection.Interfaces
{
    public interface IReflectionProperty<TSource, TProperty> where TSource : notnull
    {
        public TProperty GetValue(in TSource source);
        public void SetValue(in TSource source, TProperty value);
    }
}