// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Interfaces
{
    public interface IEventIndexCollection<T> : IEventCollection<T>
    {
        public event IndexRTypeHandler<T> OnInsert;
        public event IndexRTypeHandler<T> OnChangeIndex;
    }
}