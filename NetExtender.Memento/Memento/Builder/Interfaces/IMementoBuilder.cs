// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Memento.Builder.Interfaces
{
    public interface IMementoBuilder<TSource> where TSource : class
    {
        public IMementoBuilder<TSource> Remember(IMementoProperty<TSource> property);

        public IMementoProperty<TSource>[] Build();
        public IMementoItem<TSource> Build(TSource source);
    }
}