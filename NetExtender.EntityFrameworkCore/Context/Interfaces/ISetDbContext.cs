// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.EntityFrameworkCore;
using NetExtender.Types.Entities.Interfaces;

namespace NetExtender.DependencyInjection.Context.Interfaces
{
    public interface ISetDbContext
    {
        public DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity;
    }
}