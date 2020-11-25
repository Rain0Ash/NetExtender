// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Interfaces
{
    public interface IEventCollection<T> : IEventType
    {
        public event RTypeHandler<T> OnAdd;

        public event RTypeHandler<T> OnSet;

        public event RTypeHandler<T> OnRemove;

        public event RTypeHandler<T> OnChange;
    }
}