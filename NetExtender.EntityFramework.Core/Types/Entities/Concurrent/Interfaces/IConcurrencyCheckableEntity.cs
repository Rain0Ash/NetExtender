// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.EntityFrameworkCore.Entities.Concurrent.Interfaces
{
    public interface IConcurrencyCheckableEntity<T>
    {
        public T RowVersion { get; set; }
    }
}