// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.EntityFrameworkCore;

namespace NetExtender.DependencyInjection.Context.Interfaces
{
    public interface ILazyDbContext : ISetDbContext, ISaveDbContext
    {
    }
}